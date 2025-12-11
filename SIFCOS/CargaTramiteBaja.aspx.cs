using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Xml;
using AjaxControlToolkit;
using BL_SIFCOS;
using CryptoManagerV4._0.Clases;
using CryptoManagerV4._0.Excepciones;
using CryptoManagerV4._0.General;
using Newtonsoft.Json;
using DA_SIFCOS.Entities.CDDAutorizador;
using DA_SIFCOS.Entities.CDDPost;
using DA_SIFCOS.Entidades;
using DA_SIFCOS;
using DA_SIFCOS.Utils;
using DA_SIFCOS.Entities.CDDResponse;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Entities.Errores;
using DA_SIFCOS.Entities.Excepcion;
using DA_SIFCOS.Models;


namespace SIFCOS
{
    public partial class CargaTramiteBaja : System.Web.UI.Page
    {
		private Autorizador ObjAutorizador { get; set; }
		private CDDPost RequestPost { get; set; }
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();

        protected DataTable DtGrilla = new DataTable();
        public Int64 pExiste;
        public Int64 pVencida;
        public Principal master;
        public String Fecha_baja_municipal;
        protected void Page_Load(object sender, EventArgs e)
        {
            Fecha_baja_municipal = (String)Session["VENGO_DESDE_BAJA_FECHA_CESE"];
			
            /* Agregar a cada pagina web, para heredar el comportamiento del usuario CIDI.*/
            
            master = (Principal)Page.Master;
            var lstRolesNoAutorizados = new List<string>();
            //lstRolesNoAutorizados.Add("Administrador General");
            //lstRolesNoAutorizados.Add("Boca de Recepcion");
            //lstRolesNoAutorizados.Add("Secretaria de comercio");
            //lstRolesNoAutorizados.Add("Gestor");//usuario comun;
            lstRolesNoAutorizados.Add("Sin Asignar");

            if (lstRolesNoAutorizados.Contains(master.RolUsuario))
            {
                Response.Redirect("Inscripcion.aspx");
            }

            divMensajeErrorVentanaEncabezado.Visible = false;
            divMensajeError.Visible = false;
            divMensajeExito.Visible = false;
            divMjeErrorRepLegal.Visible = false;
            divMjeExitoRepLegal.Visible = false;
            //if (Request.Cookies["CiDi"] != null)
            //{

                if (!Page.IsPostBack)
                {
                    LimpiarSessionesEnMemoria();
                    
                    divVentanaPasosTramite.Visible = false;

                    divEncabezadoDatosEmpresa.Visible = false;
                    ObjetoInscripcion = new InscripcionSifcos();
                    //ObtenerUsuario();
                    CargarDatosUsuarioLogueado();
                    NumeroDePasoActual = NumeroPasoEnum.PrimerPaso;
                    HabilitarBotonesFooter(NumeroDePasoActual);

                    lblTituloTramite.Text = "Trámite Baja de Comercio";
                    
                    divMensaejeErrorModal.Visible = true;
                    divMensaejeErrorModal.Visible = false;
                    txtCuit.Text = (string) Session["VENGO_DESDE_BAJA_CUIT"];
                    IniciarTramite();
                }

          
        }

        private void IniciarTramite()
        {
            ContadorClicksEnBotonConfirmar = 0;

            if (ObjetoInscripcion == null)
                Response.Redirect("Inscripcion.aspx");
            if (!ValidarDatosRequeridos(NumeroPasoEnum.PasoIniciarTramite))
                return;

            //limpio campo
            txtNroSifcos.Text = (string)Session["VENGO_DESDE_BAJA_NRO_SIFCOS"] ; //campo que se carga en la pantalla BajaDeComercio.aspx

            if (!buscarEmpresaEnBDComunes())
            {
                //si no está en BD Comunes o no tiene Nro DGR
                if (!buscarEmpresaEnRENTAS())
                {
                    lblMensajeErrorVentanaEncabezado.Text = "EL NÚMERO DE CUIT QUE INGRESÓ NO ES VÁLIDO. VERIFIQUE QUE SE INGRESÓ CORRECTAMENTE (SIN GUIONES) ó QUE ESTÉ DADO DE ALTA EN INGRESOS BRUTOS (RENTAS). ";
                    AgregarCssClass("campoRequerido", txtCuit);
                    divMensajeErrorVentanaEncabezado.Visible = true;

                    return;
                }
            }


            divEncabezadoDatosEmpresa.Visible = true;
            divVentanaInicioTramite.Visible = false;

            ObtenerTramiteSegunCuit(); // carga los tramites del cuit.
            ObjetoInscripcion.TipoTramiteNum = TipoTramiteEnum.Baja_de_Comercio;
                
            ObjetoInscripcion.CUIT = txtCuit.Text;

            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Baja_de_Comercio)
            {
                ObjetoInscripcion.TipoTramite = 2;
	            divCDD.Visible = true;
                //lblTituloTramite.Text = "REEMPADRONAMIENTO - SIFCoS ";
                lblTituloVentanaModalPrincipal.Text = "BAJA DE COMERCIO - SIFCoS ";
                divSeccionReempadronamiento.Visible = true;
                divSeccionInscripcionTramite.Visible = false;
                divBotonImpirmirTRS.Visible = false;
                divCheckNuevaSucursal.Visible = true;


            }
            else
            {
                ObjetoInscripcion.TipoTramite = 1;
                lblTituloTramite.Text = "ALTA DE TRÁMITE";
                lblTituloVentanaModalPrincipal.Text = "ALTA DE TRÁMITE";
                divSeccionReempadronamiento.Visible = false;
                //Si el cuit se trae de RENTAS no se muestra la opción de seleccionar sucursal ya que no va a tener sede.
                divSeccionInscripcionTramite.Visible = !EsCuitDeRentas;
                divBotonImpirmirTRS.Visible = false;
                divCheckNuevaSucursal.Visible = false;
            }


            divVentanaPasosTramite.Visible = true;


            AceptarTramiteARealizar();
        }


        private void CargarDatosUsuarioLogueado()
        {
            txtNombreCidi.Text = master.UsuarioCidiLogueado.Nombre;
            txtApellidoCidi.Text = master.UsuarioCidiLogueado.Apellido;
            txtCuilCidi.Text = master.UsuarioCidiLogueado.CuilFormateado;

            //datos del gestor que se muestran en el paso 4

            txtNomApeConta.Text = master.UsuarioCidiLogueado.Nombre + " " + master.UsuarioCidiLogueado.Apellido;
            txtTelConta.Text = master.UsuarioCidiLogueado.CelFormateado;
            txtDniConta.Text = master.UsuarioCidiLogueado.NroDocumento;
            txtEmailConta.Text = master.UsuarioCidiLogueado.Email;
            txtSexoConta.Text = master.UsuarioCidiLogueado.Id_Sexo == "01" ? "MASCULINO" : "FEMENINO";

            ObjetoInscripcion.CuilUsuarioCidi = master.UsuarioCidiLogueado.CUIL;
        }

        #region Propiedades


        public string IdOrganismoUsuarioLogueado
        {
            get
            {
                return Session["IdOrganismoUsuarioLogueado"] == null ? "" : (string)Session["IdOrganismoUsuarioLogueado"];
            }
            set
            {
                Session["IdOrganismoUsuarioLogueado"] = value;
            }
        }

        public string TipoTramite
        {
            get
            {
                return Session["TipoTramite"] == null ? "" : (string)Session["TipoTramite"];
            }
            set
            {
                Session["TipoTramite"] = value;
            }
        }

        public int CantidadTRS_A_Imprimir
        {
            get
            {
                return  (int)Session["CantidadTRS_A_Imprimir"];

            }
            set
            {
                Session["CantidadTRS_A_Imprimir"] = value;
            }
        }

        public PestaniaActivaEnum PestaniaActiva_Establecimiento
        {
            get
            {
                return Session["PestaniaActiva_Establecimiento"] == null ? PestaniaActivaEnum.DomicilioDelEstablecimiento : (PestaniaActivaEnum)Session["PestaniaActiva_Establecimiento"];

            }
            set
            {
                Session["PestaniaActiva_Establecimiento"] = value;
            }
        }


        public string idSedeSeleccionada
        {
            get
            {
                return Session["idSedeSeleccionada"] == null ? "" : (string)Session["idSedeSeleccionada"];

            }
            set
            {
                Session["idSedeSeleccionada"] = value;
            }
        }

        public PestaniaActivaEnum PestaniaActiva_InfGeneral
        {
            get
            {
                return Session["PestaniaActiva_InfGeneral"] == null ? PestaniaActivaEnum.ProductosActPrimariaSecundaria : (PestaniaActivaEnum)Session["PestaniaActiva_InfGeneral"];

            }
            set
            {
                Session["PestaniaActiva_InfGeneral"] = value;
            }
        }

        

        public Usuario UsuarioCidiRep
        {
            get
            {
                return Session["UsuarioCidiRep"] == null ? new Usuario() : (Usuario)Session["UsuarioCidiRep"];

            }
            set
            {
                Session["UsuarioCidiRep"] = value;
            }
        }
        public Usuario UsuarioCidiGestor
        {
            get
            {
                return Session["UsuarioCidiGestor"] == null ? new Usuario() : (Usuario)Session["UsuarioCidiGestor"];

            }
            set
            {
                Session["UsuarioCidiGestor"] = value;
            }
        }
        public InscripcionSifcos ObjetoInscripcion
        {
            get
            {
                return Session["ObjetoInscripcion"] == null ? null : (InscripcionSifcos)Session["ObjetoInscripcion"];
            }
            set
            {
                Session["ObjetoInscripcion"] = value;
            }
        }
        public DataTable DtEmpresa
        {
            get
            {
                return Session["DtEmpresa"] == null ? new DataTable() : (DataTable)Session["DtEmpresa"];
            }
            set
            {
                Session["DtEmpresa"] = value;
            }
        }
        public List<Trs> ListaTrs
        {
            get
            {
                return Session["ListaTrs"] == null ? new List<Trs>() : (List<Trs>)Session["ListaTrs"];
            }
            set
            {
                Session["ListaTrs"] = value;
            }
        }

        #endregion 

		
        public void HabilitarBotonesFooter(NumeroPasoEnum numeroPasoEnum)
        {
            switch (numeroPasoEnum)
            {
                case NumeroPasoEnum.PrimerPaso:
                    divPanel_1.Visible = true;
                    btnFinalizar.Visible = true;
                    break;
               
                
            }
        }

        public NumeroPasoEnum NumeroDePasoActual
        {
            get
            {
                return Session["NumeroPaso"]== null ? NumeroPasoEnum.PrimerPaso : (NumeroPasoEnum) Session["NumeroPaso"];
            }
            set
            {
                Session["NumeroPaso"] = value;
            }
        }

        protected void btnAtras_OnClick(object sender, EventArgs e)
        {
            CorregirPasoActual();
            switch (NumeroDePasoActual) //PASO ACTUAL
            {
                case NumeroPasoEnum.SegundoPaso:
                    HabilitarBotonesFooter(NumeroPasoEnum.PrimerPaso);
                    NumeroDePasoActual = NumeroPasoEnum.PrimerPaso;
                   
                   // PintarTabBotonAnterior(li_tab_2, li_tab_1);

                    //limpiar las propiedades en sessión y controles.
                    idSedeSeleccionada = null;
                    chkNuevaSucursal.Checked = false;
                    break;
                
            }
        }

        private bool buscarEmpresaEnRENTAS()
        {
            var pCuit = txtCuit.Text;
            
            DtEmpresa = Bl.BlGetEmpresaEnRentas(pCuit);
            if (DtEmpresa.Rows.Count == 0)
                return false;


            txtRazonSocial.Text = DtEmpresa.Rows[0]["RAZON_SOCIAL"].ToString();
            txtModalRazonSocial.Text = DtEmpresa.Rows[0]["RAZON_SOCIAL"].ToString();
            txtRazonSocial.Enabled = false;
            txtNomFantasia.Text = DtEmpresa.Rows[0]["FANTASIA"].ToString();
            txtModalNombreFantasia.Text = DtEmpresa.Rows[0]["FANTASIA"].ToString();
            txtNomFantasia.Enabled = false;
            txtCuitLeido.Text = DtEmpresa.Rows[0]["CUIT"].ToString();
            txtModalCuit.Text = DtEmpresa.Rows[0]["CUIT"].ToString();

            txtCuitLeido.Enabled = false;

            EsCuitDeRentas = true;
            
            return true;
             
        }
		
        private bool buscarEmpresaEnBDComunes()
        {
            var pCuit = txtCuit.Text;
            

            //valida que tenga nro de DGR y  que exista en BD T_Comunes
            DtEmpresa = Bl.BlGetEmpresa(pCuit);
            if (DtEmpresa.Rows.Count == 0)
                return false;
            
           
            txtRazonSocial.Text = DtEmpresa.Rows[0]["RAZON_SOCIAL"].ToString();
            txtModalRazonSocial.Text = DtEmpresa.Rows[0]["RAZON_SOCIAL"].ToString();
            txtRazonSocial.Enabled = false;
            txtNomFantasia.Text = DtEmpresa.Rows[0]["NOMBRE_FANTASIA"].ToString();
            txtModalNombreFantasia.Text = DtEmpresa.Rows[0]["NOMBRE_FANTASIA"].ToString();
            txtNomFantasia.Enabled = false;
            txtCuitLeido.Text = DtEmpresa.Rows[0]["CUIT"].ToString();
            txtModalCuit.Text = DtEmpresa.Rows[0]["CUIT"].ToString();

            txtCuitLeido.Enabled = false;

            EsCuitDeRentas = false;
            return true;
             
        }

        public bool EsCuitDeRentas
        {
            get
            {
                return Session["EsCuitDeRentas"] == null ? false : (bool)Session["EsCuitDeRentas"];
            }
            set
            {
                Session["EsCuitDeRentas"] = value;
            }
        }
		
        protected void btnSiguiente_OnClick(object sender, EventArgs e)
        {
            try
            {
                CorregirPasoActual();
                switch (NumeroDePasoActual) //PASO ACTUAL
                {
                    case NumeroPasoEnum.PrimerPaso:
                        if (!ValidarDatosRequeridos(NumeroPasoEnum.PrimerPaso))
                            break;
                        //if(ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento)
                        //CargarCamposFormulario();
                        HabilitarBotonesFooter(NumeroPasoEnum.SegundoPaso);
                        NumeroDePasoActual = NumeroPasoEnum.SegundoPaso;
                       // PintarTabBotonSiguiente(li_tab_1, li_tab_2);
                        break;
                    
                }
            }
            catch (NullReferenceException ex)
            {
                if (ObjetoInscripcion == null)
                {
                    ObjetoInscripcion = new InscripcionSifcos();
                }
            }
            catch (Exception exception)
            {
                //ocurrió una excepción porque se perdió la sessión NumerpoPaso.
                lblMensajeErrorModal.Text = "ERROR BOTON SIGUIENTE:   txtFechaIniAct.Text :";
            }
        }

        private void CorregirPasoActual()
        {
            if(divPanel_1.Visible)
                NumeroDePasoActual = NumeroPasoEnum.PrimerPaso;
            //if (divPanel_2.Visible  )
            //    NumeroDePasoActual = NumeroPasoEnum.SegundoPaso;
        }

        private void CargarCamposFormulario()
        {
            //PARA EL REEMPADRONAMIENTO SOLO SE MUESTRAN LOS DATOS.. HAY QUE CARGARLOS.
            //habilitarCampoFormularioContacto(false);
            //habilitarCampoFormularioDomicilio1(false);
            //habilitarCampoFormularioDomicilio2(false);
            //habilitarCampoFormularioInformacionGeneral(false);
        }

        private void CargarDatosObjInscripcion(NumeroPasoEnum paso)
        {
            switch (paso)
            {
                case NumeroPasoEnum.SegundoPaso: 
                    if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
                    {
                        ObjetoInscripcion.FechaVencimiento = DateTime.Now.AddYears(1);//en el ALTA la fecha de vencimiento es el próximo año.
                        ObjetoInscripcion.CantidadReempadranamientos = 0;
                    }
                    break;
                
                case NumeroPasoEnum.CuartoPaso:
                    if (ObjetoInscripcion == null)
                        Response.Redirect("MisTramites.aspx?Exito=1");
                    ObjetoInscripcion.CuilGestor =   master.UsuarioCidiLogueado.CUIL;
                    ObjetoInscripcion.EmailGestor = txtEmailConta.Text;
                    ObjetoInscripcion.TelGestor = txtTelConta.Text;
                    switch (ObjetoInscripcion.TipoTramiteNum)
                    {
                        case TipoTramiteEnum.Instripcion_Sifcos:
                            ObjetoInscripcion.IdEstado = 1;//TRÁMITE estado: CARGADO
                            
                            break;
                        case TipoTramiteEnum.Baja_de_Comercio:
						//-lt
							int IdEntidad = int.Parse(Bl.BlGetIdEntidad(ObjetoInscripcion.NroTramite));
							string RegistrarEntidad;
							ObjetoInscripcion.DomComercio = new ComercioDto();

		                    

                            if (IdEntidad == 0)
                            {
                                var Entidad = Bl.BlRegistrarEntidadMigracion(ObjetoInscripcion, out IdEntidad);
                                if (Entidad == "OK")
                                {
                                    ObjetoInscripcion.DomComercio.ID_ENTIDAD = IdEntidad;
                                }
                                
                            }
                            else
                            {
                                ObjetoInscripcion.DomComercio.ID_ENTIDAD = IdEntidad;
                            }

                           
		                    ObjetoInscripcion.Id_Documento1_CDD = IdDocumentoCDD1;
		                    ObjetoInscripcion.Id_Documento2_CDD = IdDocumentoCDD2;

							ObjetoInscripcion.IdEstado = 1; //ESTADO DEL TRAMITE "CARGADO" PARA  BAJA DE COMERCIO
                            break;
                        default:
                            ObjetoInscripcion.IdEstado = 6;//TRÁMITE estado: AUTORIZADO POR MINISTERIO
                            break;
                    }

                    ObjetoInscripcion.IdCargo =string.IsNullOrEmpty( ddlCargoOcupa.SelectedValue) ? 1 : long.Parse( ddlCargoOcupa.SelectedValue) ;
                    ObjetoInscripcion.IdTipoGestor = string.IsNullOrEmpty(ddlTipoGestor.SelectedValue)
                        ? 1
                        : long.Parse(ddlTipoGestor.SelectedValue);
                    switch (ddlTipoGestor.SelectedValue)
                    {
                        case "1": // "Titular y Representante Legal de la Empresa"
                            ObjetoInscripcion.CuilRepLegal = master.UsuarioCidiLogueado.CUIL;
                            break;
                        case "2": // "Gestor que realiza el Trámite"
                            ObjetoInscripcion.CuilRepLegal = txtCuilRepresentante.Text;
                            break;
                        case "3": // "Contador de la Empresa"
                            ObjetoInscripcion.CuilRepLegal = txtCuilRepresentante.Text;
                            break;
                    }
                    
                    
                    break;
                }

            }

        /// <summary>
        /// Limpia los controles TextBox, DropDownList, checkbox que están marcados como requerido(en rojo) y los cambia al color por defecto.
        /// </summary>
        private void LimpiarControlesRequeridos()
        {
            var controles = new List<Control>();


            //Listado de controles que nos Requeridos.
            controles.Add(txtCuit);
            
            //domicilio 1
            //controles.Add(ddlDeptos);
            //controles.Add(ddlLocalidad);
            //controles.Add(ddlBarrios);
            //controles.Add(txtBarrio);
            //controles.Add(txtCodPos);
            //controles.Add(txtNroCalle);
            //controles.Add(txtNroDepto);
            //controles.Add(txtCalle);
           
            ////domicilio 2
            //controles.Add(ddlDeptoLegal);
            //controles.Add(ddlLocalidadLegal);
            //controles.Add(ddlBarriosLegal);
            //controles.Add(txtBarrioLegal);
            //controles.Add(txtCodPosLegal);
            //controles.Add(txtNroCalleLegal);
            //controles.Add(txtCalleLegal);


            
            foreach (Control control in controles)
            {
                if (control is TextBox)
                    QuitarCssClass("campoRequerido", ((TextBox)control));
                
                else if (control is DropDownList)
                    QuitarCssClass("campoRequerido", ((DropDownList)control));
                
                else if (control is CheckBox)
                    QuitarCssClass("campoRequerido", ((CheckBox)control));
                else if (control is Label)
                    QuitarCssClass("campoRequerido", ((Label)control));
            }
        }

        private bool ValidarDatosRequeridos(NumeroPasoEnum paso)
        {
            var validado = false;
            LimpiarControlesRequeridos();
            switch (paso)
            {
                case NumeroPasoEnum.PasoIniciarTramite:
                    
                    if (string.IsNullOrEmpty(txtCuit.Text))
                    {
                        //mensaje de error.
                        lblMensajeErrorVentanaEncabezado.Text = "Debe ingresar el número de cuit sin guiones para buscar la empresa.";
                        AgregarCssClass("campoRequerido",txtCuit);
                        divMensajeErrorVentanaEncabezado.Visible = true;
                        return false;
                    }
                    if (txtCuit.Text.Trim().Length != 11)
                    {
                        //mensaje de error.
                        lblMensajeErrorVentanaEncabezado.Text = "El formato de CUIT no es correcto.";
                        AgregarCssClass("campoRequerido", txtCuit);
                        divMensajeErrorVentanaEncabezado.Visible = true;
                        return false;
                    }
                    validado = true;
                    break;
                //case NumeroPasoEnum.PrimerPaso: //UBICACION EN EL MAPA


                //    if (string.IsNullOrEmpty(txtLatitud.Text) || string.IsNullOrEmpty(txtLongitud.Text))
                //    {
                //        lblMensajeError.Text = "Debe ubicar su establecimiento en el Mapa.";
                //        AgregarCssClass("campoRequerido", txtLatitud);
                //        divMensajeError.Visible = true;
                //        return false;
                //    }
                //    return true;
                    
                //    break;
                case NumeroPasoEnum.PrimerPaso: //ESTABILECIMIENTO

                    // 1) DOMICILIOS 1

                    //if (ddlDeptos.SelectedValue == "0")
                    //{
                    //    lblMensajeError.Text = "Debe seleccionar el Departamento del Domicilio del Establecimiento.";
                    //    AgregarCssClass("campoRequerido", ddlDeptos);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //if (ddlLocalidad.SelectedValue == "0")
                    //{
                    //    lblMensajeError.Text = "Debe seleccionar el Localidad del Domicilio del Establecimiento.";
                    //    AgregarCssClass("campoRequerido", ddlLocalidad);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //if (chkBarrioNoExiste.Checked)
                    //{
                    //    if (string.IsNullOrEmpty(txtBarrio.Text))
                    //    {
                    //        lblMensajeError.Text = "Debe ingresar el Barrio del Establecimiento.";
                    //        AgregarCssClass("campoRequerido", txtBarrio);
                    //        divMensajeError.Visible = true;
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                    //    if (ddlBarrios.SelectedValue == "")
                    //    {
                    //        lblMensajeError.Text = "Debe seleccionar el Barrio del Establecimiento.";
                    //        AgregarCssClass("campoRequerido", ddlBarrios);
                    //        divMensajeError.Visible = true;
                    //        return false;
                    //    }
                    //}

                    //if (string.IsNullOrEmpty(txtCalle.Text))
                    //{
                    //    lblMensajeError.Text = "Debe ingresar la Calle del Domicilio del Establecimiento. .";
                    //    AgregarCssClass("campoRequerido", txtCalle);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}
                    //if (string.IsNullOrEmpty(txtCodPos.Text))
                    //{
                    //    lblMensajeError.Text = "Debe ingresar el Código Postal del Establecimiento. Si ya tiene seleccionado un domicilio por defecto, puede agregar habilitar los campos haciendo click en el botón 'Habilitar para Cargar Nuevo Domicilio'.";
                    //    AgregarCssClass("campoRequerido", txtCodPos);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //if(txtCodPos.Text.Length != 4)
                    //{
                    //    lblMensajeError.Text = "El Código Postal debe contener 4 números.";
                    //    AgregarCssClass("campoRequerido", txtCodPos);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //if (string.IsNullOrEmpty(txtNroCalle.Text))
                    //{
                    //    lblMensajeError.Text = "La Altura de la Calle es un campo obligatorio (*). Si ya tiene seleccionado un domicilio por defecto, puede agregar habilitar los campos haciendo click en el botón 'Habilitar para Cargar Nuevo Domicilio'.";
                    //    AgregarCssClass("campoRequerido", txtNroCalle);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    


                    // 2) CONTACTO
                    //if (string.IsNullOrEmpty(txtEmail_Establecimiento.Text))
                    //{
                    //    lblMensajeError.Text = "Debe ingresar el Email con que trabaja el establecimiento.";
                    //    AgregarCssClass("campoRequerido", txtEmail_Establecimiento);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //if( (string.IsNullOrEmpty(txtTelFijoCodArea.Text) || string.IsNullOrEmpty(txtTelFijo.Text)) &&
                    //    (string.IsNullOrEmpty(txtCelularCodArea.Text) || string.IsNullOrEmpty(txtCelular.Text)) )
                    //{
                    //    lblMensajeError.Text = "Debe ingresar al menos Telefono Fijo ó Celular.";
                    //    if (string.IsNullOrEmpty(txtTelFijoCodArea.Text) || string.IsNullOrEmpty(txtTelFijo.Text))
                    //    {
                    //        AgregarCssClass("campoRequerido",txtTelFijoCodArea);
                    //        AgregarCssClass("campoRequerido", txtTelFijo);
                    //    }
                    //    if(string.IsNullOrEmpty(txtCelularCodArea.Text) || string.IsNullOrEmpty(txtCelular.Text))
                    //    {
                    //        AgregarCssClass("campoRequerido", txtCelular);
                    //        AgregarCssClass("campoRequerido", txtCelularCodArea);
                    //    }

                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}


                    // 3) DOMICILIOS 2

                    // if (ddlDeptoLegal.SelectedValue == "0")
                    //{
                    //    lblMensajeError.Text = "Debe seleccionar el Departamento del Domicilio Legal.";
                    //    AgregarCssClass("campoRequerido", ddlDeptoLegal);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //if (ddlLocalidadLegal.SelectedValue == "0")
                    //{
                    //    lblMensajeError.Text = "Debe seleccionar el Localidad del Domicilio Legal.";
                    //    AgregarCssClass("campoRequerido", ddlLocalidadLegal);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //if (chkBarrioLegalNoExiste.Checked)
                    //{
                    //    if (string.IsNullOrEmpty(txtBarrioLegal.Text))
                    //    {
                    //        lblMensajeError.Text = "Debe ingresar el Barrio del Domicilio Legal.";
                    //        AgregarCssClass("campoRequerido", txtBarrioLegal);
                    //        divMensajeError.Visible = true;
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                    //    if (ddlBarriosLegal.SelectedValue == "")
                    //    {
                    //        lblMensajeError.Text = "Debe seleccionar el Barrio del Domicilio Legal.";
                    //        AgregarCssClass("campoRequerido", ddlBarriosLegal);
                    //        divMensajeError.Visible = true;
                    //        return false;
                    //    }
                    //}

                    //if (string.IsNullOrEmpty(txtCalleLegal.Text))
                    //{
                    //    lblMensajeError.Text = "Debe ingresar la Calle del Domicilio Legal.";
                    //    AgregarCssClass("campoRequerido", txtCalleLegal);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}


                    //if ( string.IsNullOrEmpty(txtCodPosLegal.Text))
                    //{
                    //    lblMensajeError.Text = "Debe ingresar el Código Postal del Domicilio Legal. Si ya tiene seleccionado un domicilio por defecto, puede agregar habilitar los campos haciendo click en el botón 'Habilitar para Cargar Nuevo Domicilio'.";
                    //    AgregarCssClass("campoRequerido", txtCodPosLegal);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //if (txtCodPosLegal.Text.Length != 4)
                    //{
                    //    lblMensajeError.Text = "El Código Postal debe contener 4 números.";
                    //    AgregarCssClass("campoRequerido", txtCodPosLegal);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //if (string.IsNullOrEmpty(txtNroCalleLegal.Text)  )
                    //{
                    //    lblMensajeError.Text = "La Altura de la Calle es un campo obligatorio (*). Si ya tiene seleccionado un domicilio por defecto, puede agregar habilitar los campos haciendo click en el botón 'Habilitar para Cargar Nuevo Domicilio'.";
                    //    AgregarCssClass("campoRequerido", txtNroCalleLegal);
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    
                    return true;

                //case NumeroPasoEnum.TercerPaso: //INFORMACIÓN ADICIONAL
                //    //Rubros y Productos
                //    if (string.IsNullOrEmpty(ddlRubroPrimario.SelectedValue) ||
                //        ddlRubroPrimario.SelectedValue == "0" )
                //        //||ddlRubroSecundario.SelectedValue == "0") LA ACTIVIDAD SECUNDARIA PUEDE SER SIN ASIGNAR.
                //    {
                //        lblMensajeError.Text = "Debe seleccionar Actividad Primaria.";
                //        AgregarCssClass("campoRequerido",ddlRubroPrimario);
                //        divMensajeError.Visible = true;
                //        return false;
                //    }
                //    validado = true;

                //    //INFORMACIÓN GENERAL

                //    if (string.IsNullOrEmpty(txtFechaIniAct.Text))
                //    {
                //        lblMensajeError.Text = "Debe ingresar la Fecha de Inicio de Actividad.";
                //        AgregarCssClass("campoRequerido", txtFechaIniAct);
                //        divMensajeError.Visible = true;
                //        return false;
                //    }

                //    if (string.IsNullOrEmpty(txtNroHabMun.Text))
                //    {
                //        lblMensajeError.Text = "Debe ingresar el Nro de Habilitación Municipal.";
                //        AgregarCssClass("campoRequerido", txtNroHabMun);
                //        divMensajeError.Visible = true;
                //        return false;
                //    }
                //    if (string.IsNullOrEmpty(txtM2Venta.Text))
                //    {
                //        lblMensajeError.Text = "Debe ingresar el valor la Superficie de venta.";
                //        AgregarCssClass("campoRequerido", txtM2Venta);
                //        divMensajeError.Visible = true;
                //        return false;
                //    }
                //    else
                //    {
                //        if (double.Parse(txtM2Venta.Text) < 1)
                //        {
                //            lblMensajeError.Text = "La Superficie de venta debe ser mayor o igual a 1.";
                //            AgregarCssClass("campoRequerido", txtM2Venta);
                //            divMensajeError.Visible = true;
                //            return false;
                //        }
                //    }

                //    if (string.IsNullOrEmpty(txtCantPersTotal.Text))
                //    {
                //        lblMensajeError.Text = "La Cantidad de Personal Total es un campo obligatorio.";
                //        AgregarCssClass("campoRequerido", txtCantPersTotal);
                //        divMensajeError.Visible = true;
                //        return false;
                //    }
                //    else
                //    {
                //        if (double.Parse(txtCantPersTotal.Text) < 1)
                //        {
                //            lblMensajeError.Text = "La Cantidad de Personal Total debe ser un valor superior ó igual a 1.";
                //            AgregarCssClass("campoRequerido", txtCantPersTotal);
                //            divMensajeError.Visible = true;
                //            return false;
                //        }
                //    }
                //    if (!string.IsNullOrEmpty(txtCantPersRelDep.Text))
                //    {
                //        if (double.Parse(txtCantPersRelDep.Text) > double.Parse(txtCantPersTotal.Text))
                //        {
                //            lblMensajeError.Text = "La Cantidad de Personal en Relación de Dependencia no puede superar a la Cantidad de Personal Total.";
                //            AgregarCssClass("campoRequerido", txtCantPersRelDep);
                //            divMensajeError.Visible = true;
                //            return false;     
                //        }
                //    }

                //    if (!string.IsNullOrEmpty(txtM2Admin.Text))
                //    {
                //          if (long.Parse(txtM2Admin.Text) < 1)
                //            {
                //                lblMensajeError.Text = "Los valores de las Superficies Administración debe ser 1 o mayor.";
                //                divMensajeError.Visible = true;
                //                return false;
                //            }
                //    }
                //    if (!string.IsNullOrEmpty(txtM2Venta.Text))
                //    {
                //        if (long.Parse(txtM2Venta.Text) < 1)
                //        {
                //            lblMensajeError.Text = "Los valores de las Superficies de Venta debe ser 1 o mayor.";
                //            divMensajeError.Visible = true;
                //            return false;
                //        }
                //    }
                //    if (!string.IsNullOrEmpty(txtM2Dep.Text))
                //    {
                //        if (long.Parse(txtM2Dep.Text) < 1)
                //        {
                //            lblMensajeError.Text = "Los valores de las Superficies de Deposito debe ser 1 o mayor.";
                //            divMensajeError.Visible = true;
                //            return false;
                //        }
                //    }
                  

                //    if (!rbInquilino.Checked && !rbPropietario.Checked)
                //    {
                //        lblMensajeError.Text = "Debe indicar si es Propietario o Inquilino.";
                //        divMensajeError.Visible = true;
                        
                //        return false;
                //    }

                //    if (rbInquilino.Checked)
                //    {
                //        if (!rb1015.Checked && !rb1520.Checked && !rb5.Checked && !rb510.Checked)
                //        {
                //            lblMensajeError.Text = "Si es Inquilino, debe seleccionar un monto de alquiler.";
                //            divMensajeError.Visible = true;
                //            return false;
                //        }
                //    }
                //    //CANTIDAD DE PERSONAL CON RELACIÓN DE DEPENDENCIA
                //    if (string.IsNullOrEmpty(txtCantPersRelDep.Text) || string.IsNullOrEmpty(txtCantPersTotal.Text))
                //    {
                //        lblMensajeError.Text = "Debe completar la Cantidad de Personal.";
                //        divMensajeError.Visible = true;
                //        return false;
                //    }

                //    //INFORMACIÓN ADICIONAL
                //    if (!rbSiCap.Checked && !rbNoCap.Checked)
                //    {
                //        lblMensajeError.Text = "Debe indicar si realizó Capacitación o no.";
                //        divMensajeError.Visible = true;
                //        return false;
                //    }
                //    if (!rbSiCobertura.Checked && !rbNoCobertura.Checked)
                //    {
                //        lblMensajeError.Text = "Debe indicar si posee Cobertura o no.";
                //        divMensajeError.Visible = true;
                //        return false;
                //    }
                     
                //    if (!rbSiSeg.Checked && !rbNoSeg.Checked)
                //    {
                //        lblMensajeError.Text = "Debe indicar si posee Seguro o no.";
                //        divMensajeError.Visible = true;
                //        return false;
                //    }
                //    if (!chkprov.Checked && !ChkInter.Checked && !ChkNacional.Checked)
                //    {
                //        lblMensajeError.Text = "Debe indicar al menos una opción de 'Origen proveedor'.";
                //        divMensajeError.Visible = true;
                //        return false;
                //    }
                    
                   

                //    break;

                   
                case NumeroPasoEnum.SegundoPaso:
                    if (!chkAceptarTerminosYCondiciones.Checked)
                    {
                        lblMensajeError.Text = "Para finalizar con el trámite de ACEPTAR la Declaración Jurada.";
                        divMensajeError.Visible = true;
                        return false;
                    }

                    switch (ddlTipoGestor.SelectedValue)
                    {
                        case "1":// "Titular y Representante Legal de la Empresa"

                            break;
                        case "2":// "Gestor que realiza el Trámite"
                            if (string.IsNullOrEmpty(txtCuilRepresentante.Text) || string.IsNullOrEmpty(txtNombreRepresentante.Text) || string.IsNullOrEmpty(txtApellidoRepresentante.Text))
                            {
                                lblMensajeError.Text = "Debe Cargar los datos de la persona que incia el trámite ingresando el cuil para obtener los mismos desde CiDi.";
                                divMensajeError.Visible = true;
                                return false;
                            }
                            //if (txtEmailConta.Text.Trim() == txtEmail_Establecimiento.Text.Trim())
                            //{
                            //    lblMensajeError.Text = "EL EMAIL DE USTED (" + txtEmailConta.Text  + ") NO PUEDE SER UTILIZADO COMO 'EMAIL DEL ESTABLECIMIENTO' (INGRESADO EN EL PASO 2), YA QUE ES UN GESTOR DEL TRÁMITE.";
                            //    divMensajeError.Visible = true;
                            //    return false;
                            //}
                            break;
                        case "3":// "Contador de la Empresa"
                            if (string.IsNullOrEmpty(txtNroMatricula.Text))
                            {
                                lblMensajeError.Text = "Contador, debe ingresar su Nro de Matrícula.";
                                divMensajeError.Visible = true;
                                return false;
                            }
                            
                            if (string.IsNullOrEmpty(txtCuilRepresentante.Text) || string.IsNullOrEmpty(txtNombreRepresentante.Text) || string.IsNullOrEmpty(txtApellidoRepresentante.Text))
                            {
                                lblMensajeError.Text = "Debe Cargar los datos de la persona que incia el trámite ingresando el cuil para obtener los mismos desde CiDi.";
                                divMensajeError.Visible = true;
                                return false;
                            }

                            //if (txtEmailConta.Text.Trim() == txtEmail_Establecimiento.Text.Trim())
                            //{
                            //    lblMensajeError.Text = "EL EMAIL DE USTED (" + txtEmailConta.Text + ") NO PUEDE SER UTILIZADO COMO 'EMAIL DEL ESTABLECIMIENTO' (INGRESADO EN EL PASO 2), YA QUE ESTÁ REALIZANDO EL TRÁMITE EN CARACTER DE CONTADOR.";
                            //    divMensajeError.Visible = true;
                            //    return false;
                            //}
                            break;
                    }

					  if (IdDocumentoCDD1 != null)
			            {
				            if (IdDocumentoCDD1 == 0)
				            {
					            divMensajeError.Visible = true;
					            lblMensajeError.Text = "Es obligatorio adjuntar el Cese Municipal.";
					            return false;
				            }
			            }
						
			            if (IdDocumentoCDD2 != null)
			            {
				            if (IdDocumentoCDD2 == 0)
				            {
					            divMensajeError.Visible = true;
					            lblMensajeError.Text = "Es obligatorio adjuntar Denuncia policial o foto de oblea.";
					            return false;
				            }
			            }





					//-*
                    
                    validado = true;
                    break;
            }
            return validado;
        }
		
        /// <summary>
        /// AGregar una CssClass a un control web de ASP.NET.
        /// </summary>
        /// <param name="nombreClass"></param>
        /// <param name="control"></param>
        private void AgregarCssClass(string nombreClass, WebControl control)
        {
            List<string> classes;
            if (!string.IsNullOrWhiteSpace(control.CssClass))
            {
                classes = control.CssClass.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (!classes.Contains(nombreClass))
                    classes.Add(nombreClass);
            }
            else
            {
                classes = new List<string> { nombreClass };
            }
            control.CssClass = string.Join(" ", classes.ToArray());
        }
		
        private void QuitarCssClass( string nombreClass,WebControl control)
        {
            List<string> classes = new List<string>();
            if (!string.IsNullOrWhiteSpace(control.CssClass))
            {
                classes = control.CssClass.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            classes.Remove(nombreClass);
            control.CssClass = string.Join(" ", classes.ToArray());
        }

        private void MostrarOcultarModalFinalizacionTramite(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarModalFinalizacionTramite";
                string[] listaStrings = modalFinalizacionTramite.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalFinalizacionTramite.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalFinalizacionTramite.Attributes.Add("class", String.Join(" ", modalFinalizacionTramite
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarModalFinalizacionTramite" })
                          .ToArray()
                  ));
            }
        }

        //private void MostrarOcultarModalDomicilio2(bool mostrar)
        //{

        //    if (mostrar)
        //    {
        //        var classname = "mostrarModalFinalizacionTramite";
        //        string[] listaStrings = modalDomicilio2.Attributes["class"].Split(' ');
        //        var listaStr = String.Join(" ", listaStrings
        //            .Except(new string[] { "", classname })
        //            .Concat(new string[] { classname })
        //            .ToArray()
        //            );
        //        modalDomicilio2.Attributes.Add("class", listaStr);
        //    }
        //    else
        //    {
        //        //oculta la Modal 
        //        modalDomicilio2.Attributes.Add("class", String.Join(" ", modalDomicilio2
        //                  .Attributes["class"]
        //                  .Split(' ')
        //                  .Except(new string[] { "", "mostrarModalFinalizacionTramite" })
        //                  .ToArray()
        //          ));
        //    }
        //}

        private void MostrarOcultarModalVentanaSalirDelTramite(bool mostrar)
        {
            
            if (mostrar)
            {
                var classname = "mostrarModalFinalizacionTramite";
                string[] listaStrings = divModalSalirDelTramite.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                divModalSalirDelTramite.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                divModalSalirDelTramite.Attributes.Add("class", String.Join(" ", divModalSalirDelTramite
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarModalFinalizacionTramite" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultarModalVentanaMantSistema(bool mostrar)
        {

            //if (mostrar)
            //{
            //    var classname = "mostrarModalMantSistema";
            //    string[] listaStrings = divModalMantSistema.Attributes["class"].Split(' ');
            //    var listaStr = String.Join(" ", listaStrings
            //        .Except(new string[] { "", classname })
            //        .Concat(new string[] { classname })
            //        .ToArray()
            //        );
            //    divModalMantSistema.Attributes.Add("class", listaStr);
            //}
            //else
            //{
            //    //oculta la Modal 
            //    divModalMantSistema.Attributes.Add("class", String.Join(" ", divModalMantSistema
            //              .Attributes["class"]
            //              .Split(' ')
            //              .Except(new string[] { "", "mostrarModalMantSistema" })
            //              .ToArray()
            //      ));
            //}
        }
        
        private void MostrarOcultarModalFelicitacionesFin(bool mostrar)
        {
            
            if (mostrar)
            {
                var classname = "mostrarModalFinalizacionTramite";
                string[] listaStrings = divModalFelicitacionesFin.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                divModalFelicitacionesFin.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                divModalFelicitacionesFin.Attributes.Add("class", String.Join(" ", divModalFelicitacionesFin
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarModalFinalizacionTramite" })
                          .ToArray()
                  ));
            }
        }


        private void MostrarOcultarModalTramiteSifcos(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarModalTramite";
                string[] listaStrings = modalInformacionTituloTramite.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalInformacionTituloTramite.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalInformacionTituloTramite.Attributes.Add("class", String.Join(" ", modalInformacionTituloTramite
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarModalTramite" })
                          .ToArray()
                  ));
            }
        }

        private TipoTramiteEnum ObtenerTramiteSegunCuit()
        {
            // se verifica si viene del viejo sifcos 
            var viejoSifcos = Bl.BlGetTramitesSifcosViejo(null, ObjetoInscripcion.NroSifcos, null);

            if (viejoSifcos.Rows.Count > 0)
            {
                lblTituloTramite.Text = "REEMPADRONAMIENTO MIGRADO - SIFCoS ";
                lblTituloVentanaModalPrincipal.Text = "REEMPADRONAMIENTO MIGRADO - SIFCoS ";
                return TipoTramiteEnum.Reempadronamiento_ViejoSifcos;
            }

            SessionTramitesDelCuit = Bl.BlGetEntidadTramite(txtCuit.Text);
            if (SessionTramitesDelCuit.Count == 0)
            {
                //modalInformacionTituloTramite.Visible = false;
                //MostrarOcultarModalVentanaMantSistema(true);
                return TipoTramiteEnum.Instripcion_Sifcos;
            }

            return TipoTramiteEnum.Reempadronamiento;


        }

        /// <summary>
        /// Contiene una lista de trámites del cuit (empresa) que ingresó. En la misma aparecen todos las sucursales de esa empresa, cada una con su Nro Sifcos.
        /// </summary>
        public List<consultaTramite> SessionTramitesDelCuit
        {
            get
            {
                return (List<consultaTramite>) Session["SessionTramitesDelCuit"];
            }
            set
            {
                Session["SessionTramitesDelCuit"] = value;
            }
        }

        //private void habilitarCampoFormularioDomicilio1(bool valor)
        //{
        //    ddlDeptos.Enabled = valor;
        //    ddlLocalidad.Enabled = valor;
        //    ddlBarrios.Enabled = valor;
        //    txtCalle.Enabled = valor;
        //    txtNroCalle.Enabled = valor;
        //    txtPiso.Enabled = valor;
        //    txtNroDepto.Enabled = valor;
        //    txtCodPos.Enabled = valor;
        //    //txtStand.Enabled = valor;
        //    //txtLocal.Enabled = valor;
        //    txtBarrio.Enabled = valor;
        //    //txtOficina.Enabled = valor;
        //    chkBarrioNoExiste.Enabled = valor;
           

        //    btnCargarDomicilio1.Enabled = !valor;
        //}

        //private void habilitarCampoFormularioDomicilio2(bool valor)
        //{
        //    ddlProvinciaLegal.Enabled = valor;
        //    ddlDeptoLegal.Enabled = valor;
        //    ddlLocalidadLegal.Enabled = valor;
        //    ddlBarriosLegal.Enabled = valor;
        //    txtCalleLegal.Enabled = valor;
        //    txtNroCalleLegal.Enabled = valor;
        //    txtPisoLegal.Enabled = valor;
        //    txtNroDeptoLegal.Enabled = valor;
        //    txtCodPosLegal.Enabled = valor;
        //    txtBarrioLegal.Enabled = valor;
        //    chkBarrioLegalNoExiste.Enabled = valor;

        //    btnCargarDomicilio2.Enabled = !valor;
        //}

        //private void habilitarCampoFormularioContacto(bool valor)
        //{
        //    txtCelularCodArea.Enabled = valor;
        //    txtCelular.Enabled = valor;
        //    txtTelFijoCodArea.Enabled = valor;
        //    txtTelFijo.Enabled = valor;
        //    txtRedSocial.Enabled = valor;
        //    txtEmail_Establecimiento.Enabled = valor;
        //    txtWebPage.Enabled = valor;
            
        //}

        public DataTable DtDomicilios1
        {
            get { return Session["DtDomicilios1"] == null ? null : (DataTable)Session["DtDomicilios1"]; }
            set { Session["DtDomicilios1"] = value; }
        }

        public DataTable DtComunicaciones
        {
            get { return Session["DtComunicaciones"] == null ? null : (DataTable)Session["DtComunicaciones"]; }
            set { Session["DtComunicaciones"] = value; }
        }

        public DataTable DtDomicilios2
        {
            get { return Session["DtDomicilios2"] == null ? null : (DataTable)Session["DtDomicilios2"]; }
            set { Session["DtDomicilios2"] = value; }
        }

        private void PintarTabBotonSiguiente(HtmlGenericControl li_tab_anterior, HtmlGenericControl li_tab_actual)
        {
            var classname = "active done";
            string[] listaStrings = li_tab_anterior.Attributes["class"].Split(' ');
            var listaStr = String.Join(" ", listaStrings
                .Except(new string[] { "", classname })
                .Concat(new string[] { classname })
                .ToArray()
                );
            li_tab_anterior.Attributes.Add("class", listaStr);

            var classname2 = "active";
            string[] listaStrings2 = li_tab_actual.Attributes["class"].Split(' ');
            var listaStr2 = String.Join(" ", listaStrings2
                .Except(new string[] { "", classname2 })
                .Concat(new string[] { classname2 })
                .ToArray()
                );
            li_tab_actual.Attributes.Add("class", listaStr2);

        }

        private void PintarTabBotonAnterior(HtmlGenericControl li_tab_anterior, HtmlGenericControl li_tab_actual)
        {
            //remove a class
            li_tab_anterior.Attributes.Add("class", String.Join(" ", li_tab_anterior
                      .Attributes["class"]
                      .Split(' ')
                      .Except(new string[] { "", "active" })
                      .ToArray()
              ));
            //remove a class
            li_tab_actual.Attributes.Add("class", String.Join(" ", li_tab_actual
                      .Attributes["class"]
                      .Split(' ')
                      .Except(new string[] { "", "done" })
                      .ToArray()
              ));
        }
        
        private void CargarHtml_EncabezadoModalFinalizacion()
        {
            StringBuilder html = new StringBuilder();

            if (ObjetoInscripcion == null)
            {
                // es porque volvio para atras y desea guardar de nuevo. De esta forma evita que se guarden varios tramites.
                Response.Redirect("Inscripcion.aspx");
                return;
            }
            //cargo titulo segun tipo de tramite.
            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Baja_de_Comercio)
            {
                html.Append("");
                html.AppendLine(" <p> ");

                html.AppendLine(master.UsuarioCidiLogueado.Apellido + ", " + master.UsuarioCidiLogueado.Nombre);
                html.AppendLine(" . Ud está finalizando con el trámite de BAJA DE COMERCIO.  ");
                html.AppendLine(" </p> ");
                divHtml_EncabezadoModalFinalizacion.InnerHtml = html.ToString();
            }
            
                     
        }

        protected void ObtenerUsuarioRepresentante()
        {
            if (txtCuilRepresentante.Text.Trim().Length != 11)
            {
                //mensaje de error.
                lblErrorRepLegal.Text = "El formato de CUIT no es correcto.";
                AgregarCssClass("campoRequerido", txtCuilRepresentante);
                divMjeErrorRepLegal.Visible = true;
                return;
            }

            if (string.IsNullOrEmpty(txtCuilRepresentante.Text))
            {
                lblErrorRepLegal.Text = "Debe ingresar cuil del Representante";
                AgregarCssClass("campoRequerido", txtCuilRepresentante);
                divMjeErrorRepLegal.Visible = true;
                return;
            }

            //buscar en RCIVIL
            string DNI;
            string digitoVerificador = txtCuilRepresentante.Text.Substring(2, 1);
            if (digitoVerificador == "0")
            {
                DNI = txtCuilRepresentante.Text.Substring(3, 7);
            }
            else
            {
                DNI = txtCuilRepresentante.Text.Substring(2, 8);
            }
            var resultado = Bl.BlGetPersonasRcivil2(DNI, ddlSexoRP.SelectedValue);

            if (resultado.Rows.Count != 0)
            {
                divMensaejeErrorModal.Visible = false;
                txtNombreRepresentante.Text = resultado.Rows[0]["nombre"].ToString();
                txtApellidoRepresentante.Text = resultado.Rows[0]["apellido"].ToString();
                txtSexoRepresentante.Text = resultado.Rows[0]["tipo_sexo"].ToString();
            }
            else
            {

                String PSexo = ddlSexoRP.SelectedValue;

                var dtConsultaPersonas = Bl.BlGetPersonasRcivil2(DNI, PSexo);
                if (dtConsultaPersonas.Rows.Count > 0)
                {
                    string Pcuil = txtCuilRepresentante.Text;
                    string Pdni = DNI;
                    string Pidnumero = dtConsultaPersonas.Rows[0]["id_numero"].ToString();
                    string Pcod_pais = dtConsultaPersonas.Rows[0]["pai_cod_pais"].ToString();
                    var InsertarCUIL = Bl.BlGenerarCUIL(Pcuil, Int64.Parse(Pdni), PSexo, Int64.Parse(Pidnumero), Pcod_pais);
                    if (InsertarCUIL == "OK-CUIL")
                    {
                        divMjeErrorRepLegal.Visible = false;
                        divMjeExitoRepLegal.Visible = true;
                        lblExitoRepLegal.Text =
                            "La persona no estaba cargada en base de datos y fue agregada automaticamente.";
                        dtConsultaPersonas = Bl.BlGetPersonasRcivil2(DNI, PSexo);

                        if (dtConsultaPersonas.Rows.Count != 0)
                        {
                            divMensaejeErrorModal.Visible = false;
                            txtNombreRepresentante.Text = dtConsultaPersonas.Rows[0]["nombre"].ToString();
                            txtApellidoRepresentante.Text = dtConsultaPersonas.Rows[0]["apellido"].ToString();
                            txtSexoRepresentante.Text = dtConsultaPersonas.Rows[0]["tipo_sexo"].ToString();
                        }
                    }



                }
                else
                {
                    divMjeErrorRepLegal.Visible = true;
                    divMjeExitoRepLegal.Visible = false;
                    lblErrorRepLegal.Text = "La persona no ha sido encontrada";
                    txtApellidoRepresentante.Text = "";
                    txtNombreRepresentante.Text = "";
                    txtSexoRepresentante.Text = "";


                }
                return;
            }

            //buscar en CIDI
            string urlapi = WebConfigurationManager.AppSettings["CiDiUrl"].ToString();
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["CiDi"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);
            entrada.Cuil = txtCuilRepresentante.Text;
            UsuarioCidiRep = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Basicos_CUIL, entrada);

        }
        
        protected void btnFinalizar_OnClick(object sender, EventArgs e)
        {
            if(!ValidarDatosRequeridos(NumeroPasoEnum.SegundoPaso))
                return;

            //if (Bl.BlGetPersonasRcivil_CUIL(txtCuilRepresentante.Text.Trim()).Rows.Count == 0)
            //{
            //    lblMensajeErrorModal.Text =
            //        "NO SE VA A PODER REGISTRAR EL TRÁMITE PORQUE SU CUIL (" + txtCuilRepresentante.Text.Trim() + ") QUE UTILIZA COMO USUARIO LOGUEADO NO ESTÁ EN RCIVIL. COMUNÍQUESE CON SISTEMAS.";
            //    divMensaejeErrorModal.Visible = true;
            //    return;
                
            //}
            
                CargarInformacionModalFinalizacionTramite();
                MostrarOcultarModalFinalizacionTramite(true);   
           
           
        }

        private void CargarInformacionModalFinalizacionTramite()
        {
            CargarHtml_EncabezadoModalFinalizacion();

            lblTituloEmpresaModalConfirmacion.Text = "EMPRESA : " + txtRazonSocial.Text + " | CUIT : " + txtCuit.Text;

            //gvProductosActividades_ModalFinalizacion.DataSource = DtProductos;
            //gvProductosActividades_ModalFinalizacion.DataBind();

            //lblActividadPrimaria.Text = ddlRubroPrimario.SelectedItem.Text;
            //lblActividadSecundaria.Text = ddlRubroSecundario.SelectedItem.Text;

            //if (ddlDeptos != null)
            //    if (ddlDeptos.SelectedItem != null) 
            //        lblDepartamento.Text = ddlDeptos.SelectedItem.Text;
            //if (ddlLocalidad != null)
            //    if (ddlLocalidad.SelectedItem != null) 
            //        lblLocalidad.Text = ddlLocalidad.SelectedItem.Text;
            //if (chkBarrioNoExiste.Checked)
            //    lblBarrio.Text = txtBarrio.Text;
            //else
            //{
            //    if (ddlBarrios != null)
            //        if (ddlBarrios.SelectedItem != null) 
            //            lblBarrio.Text =  ddlBarrios.SelectedItem.Text;
            //}
          
            //lblCalle.Text = txtCalle.Text;
            //lblNroCalle.Text = txtNroCalle.Text;
            //lblCodPos.Text = txtCodPos.Text;
            //lblPiso.Text = txtPiso.Text;
            //lblNroDepto.Text = txtNroDepto.Text;
            //lblOficina.Text = txtOficina.Text;
            //lblStand.Text = txtStand.Text;
            //lblLocal.Text = txtLocal.Text;
            //lblEmailC.Text = txtEmail_Establecimiento.Text;
            //lblCelular.Text = !string.IsNullOrEmpty(txtCelular.Text) ? "(" + txtCelularCodArea.Text + ")" + txtCelular.Text : " - ";
            //lblTelFijo.Text = !string.IsNullOrEmpty(txtTelFijo.Text) ? "(" + txtTelFijoCodArea.Text + ")" + txtTelFijo.Text : " - ";
            //lblWebPag.Text = !string.IsNullOrEmpty(txtWebPage.Text) ? txtWebPage.Text : " - ";
            //lblFacebook.Text = !string.IsNullOrEmpty(txtRedSocial.Text) ? txtRedSocial.Text : " - ";
            //if (ddlDeptoLegal != null)
            //    if (ddlDeptoLegal.SelectedItem != null) 
            //        lblDepartamentoLegal.Text = ddlDeptoLegal.SelectedItem.Text;
            //if (ddlLocalidadLegal != null)
            //    if (ddlLocalidadLegal.SelectedItem != null)
            //        lblLocalidadLegal.Text = ddlLocalidadLegal.SelectedItem.Text;

            //if (chkBarrioLegalNoExiste.Checked)
            //    lblBarrioLegal.Text = txtBarrioLegal.Text;
            //else
            //{
            //    if (ddlBarriosLegal != null)
            //        if (ddlBarriosLegal.SelectedItem != null)
            //            lblBarrioLegal.Text = ddlBarriosLegal.SelectedItem.Text;
            //}
            
            //lblCalleLegal.Text = txtCalleLegal.Text;
            //lblNroCalleLegal.Text = txtNroCalleLegal.Text;
            //lblCodPosLegal.Text = txtCodPosLegal.Text;
            //lblPisoLegal.Text = txtPisoLegal.Text;
            //lblNroDptoLegal.Text = txtNroDeptoLegal.Text;


            //lblFecInicioActividad.Text = txtFechaIniAct.Text;
            //lblNroHabilitacionMunicipal.Text = txtNroHabMun.Text;
            //lblNroDGR.Text = txtNroDGR.Text;
            //lblSuperficieVenta.Text = txtM2Venta.Text;
            //lblSuperficieAdministracion.Text = txtM2Admin.Text;
            //lblSuperficioDeposito.Text = txtM2Dep.Text;
            //lblInmueble_PropietarioInquil.Text = rbInquilino.Checked ? "Inquilino" : "Propietario";

            //if (rbInquilino.Checked)
            //{
            //    lblInmueble_RangoAlquiler.Text = rb5.Checked ? "$5000" : "";
            //    if (string.IsNullOrEmpty(lblInmueble_RangoAlquiler.Text))
            //        lblInmueble_RangoAlquiler.Text = rb510.Checked ? "$5000 a $10000" : "";
            //    if (string.IsNullOrEmpty(lblInmueble_RangoAlquiler.Text))
            //        lblInmueble_RangoAlquiler.Text = rb1015.Checked ? "$10000 a $15000" : "";
            //    if (string.IsNullOrEmpty(lblInmueble_RangoAlquiler.Text))
            //        lblInmueble_RangoAlquiler.Text = rb1520.Checked ? "más de $15000" : "";
            //}
            //else
            //{
            //    lblInmueble_RangoAlquiler.Text = "No Posee";
            //}

            //lblCantPersRel.Text = txtCantPersRelDep.Text;
            //lblCantPersTotal.Text = txtCantPersTotal.Text;
            //lblPoseeCobert_SiNo.Text = rbSiCobertura.Checked ? "Si posee cobertura médica" : "No posee cobertura médica";
            //lblCapacitacionUltAnio.Text = rbSiCap.Checked ? "Si se capacitó." : "No se capacitó.";
            //lblPoseeSeguro.Text = rbSiSeg.Checked ? "Si posee seguro social" : "No posee seguro social";
            //var seg = chkprov.Checked ? "Provincial" : "";
            //seg = ChkNacional.Checked ? seg + " - " + "Nacional" : seg + "";
            //seg = ChkInter.Checked ? seg + " - " + "Internacional" : seg + "";
            //lblOrigenProveedor.Text = seg;

        }

        private void  GuardarInscripcionSifcos()
        {
            //if(!GuardarDomicilios())
            //    return;
            
            ObjetoInscripcion.IdSede = idSedeSeleccionada;

            //ObjetoInscripcion.IdVinDomLegal = IdVinDomicilio2;
            //ObjetoInscripcion.IdVinDomLocal = IdVinDomicilio1;
            ObjetoInscripcion.ListaTrs = ListaTrs;
            ObjetoInscripcion.FechaVencimiento = DateTime.Parse(Fecha_baja_municipal);


            string nuevoNroTramite = "";
            var resultado = Bl.BlRegistrarTramiteParaBajaComercio(ObjetoInscripcion, out nuevoNroTramite);
            
            
            switch (resultado)
            {
                case "OK":
                    lblMensajeExito.Text =
                        "SE REGISTRÓ CON ÉXITO SU TRÁMITE. CONSULTE EN 'MIS TRAMITES' EL ESTADO DEL MISMO EN CUALQUIER MOMENTO. MUCHAS GRACIAS";
                    divMensajeExito.Visible = true;
                    break;
                case "ERROR":
                    Response.Redirect("Inscripcion.aspx");
                    //lblMensajeError.Text = "Sr. Usuario, ocurrió un error al intentar guardar el trámite. Verifique los datos ingresados ó inténtelo mas luego. Para más información comuníquese al telefojo 0-800-xxxx.";
                    //divMensajeError.Visible = false;
                    break;
                case "EXISTE":
                    Response.Redirect("Inscripcion.aspx");
                    break;
                 default :
                    MostrarOcultarModalFinalizacionTramite(false);
                    lblMensajeError.Text =
                        "OCURRIÓ UN ERROR AL GUARDAR EL TRÁMITE. COMUNÍQUESE CON SIFCoS PARA DARLE SOPORTE. MUCHAS GRACIAS.";
                    divMensajeError.Visible = true;
                    break;
                    

            }
        }
		
        protected void btnImprimirTRS_Click(object sender, EventArgs e)
        {
            ImprimirTRS();
           
        }
		
        private void ImprimirTRS()
        {
            try
            {
                // CONCEPTO
                //ID : 76010000  | NOMBRE : Art. 76 - Inc. 1 - Tasa retributiva por derecho de inscripcion en SIFCoS (Inscripcion)
                //ID : 76020000  | NOMBRE : Art. 76 - Inc. 2 - Por renovacion anual del derecho de inscripcion en el SIFCoS  (Reempadronamiento)

                int IdTipoTramiteTrs = 4;

                string strIdConcepto = SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;
                String fecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoReempadronamiento;
                string urlTrs = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;

                switch (ObjetoInscripcion.TipoTramite.ToString())
                {
                    case "1":
                        IdTipoTramiteTrs = 1;
                        break;
                    case "2":
                        IdTipoTramiteTrs = 4;
                        break;
                    case "4":
                        IdTipoTramiteTrs = 4;
                        break;
                    case "5":
                        IdTipoTramiteTrs = 1;
                        break;
                    case "7":
                        IdTipoTramiteTrs = 1;
                        break;
                    case "9":
                        IdTipoTramiteTrs = 1;
                        break;
                    default:
                        lblMensajeError.Text = "EL TRÁMITE NO DEBE IMPRIMIR LA TASA";
                        divMensajeError.Visible = true;
                        return;

                }
                string oFechaVenc;
                string oHashTrx;
                string oIdTransaccion;
                string oNroLiqOriginal;
                string strConcpeto = "";
                string strMonto = "";

                //var conceptoTasa = new ConceptoTasa { id_concepto = idConcepto, fec_desde = DateTime.Parse(fecDesdeConcepto) };
                //string nroTransaccion = null;


                string resultado = "";                
                

                var cantTrsImprimir = CantidadTRS_A_Imprimir; 
                var lstAux = new List<Trs>();
                if (cantTrsImprimir > 0)
                {
                    for (int i = 0; i < cantTrsImprimir; i++)
                    {
                        //genero 
                        /*
                        resultado = Bl.GenerarTransaccionTRS(ObjetoInscripcion.CUIT, master.UsuarioCidiLogueado.Id_Sexo,
                                   master.UsuarioCidiLogueado.NroDocumento, "ARG",
                                   master.UsuarioCidiLogueado.Id_Numero, Int64.Parse(conceptoTasa.id_concepto), conceptoTasa.fec_desde,
                                   "057", 1, importeConcepto, "", DateTime.Now.Year.ToString(), out nroTransaccion);
                        */


                        resultado = Bl.BlSolicitarTrs(IdTipoTramiteTrs, ObjetoInscripcion.CUIT, master.UsuarioCidiLogueado.CUIL,
                        out oFechaVenc, out oHashTrx, out oIdTransaccion, out oNroLiqOriginal, out strIdConcepto, out fecDesdeConcepto, out strMonto, out strConcpeto);


                        //Agrego una trs a la lista auxiliar. Esta lista se utiliza en la pantalla siguiente donde se muestran una guilla con las tasas a imprimir.
                        lstAux.Add(new Trs
                        {
                            Concepto = strConcpeto,
                            Importe = strMonto,
                            NroTransaccion = oIdTransaccion,
                            Link = urlTrs + oHashTrx + "/" + oIdTransaccion,
                            NroLiquidacion = oNroLiqOriginal

                        });

                        if (resultado != "OK")
                        {
                            lblMensajeError.Text = resultado;
                            divMensajeError.Visible = true;
                            return;
                        }
                    }
                }



                ListaTrs = null;
                ListaTrs = lstAux;
                //EN EL REEMPADRONAMIENTO SE IMPRIME LA TRS Y NO PUEDE SEGUIR CON EL TRAMITE.
                    
                Response.Redirect("ListaTrsReempa.aspx");
                
            }
            catch (Exception e)
            {
                lblMensajeError.Text = "Ocurrió un Error al Imprimir la TRS. Por favor intentelo más tarde.";
                divMensajeError.Visible = false;
            }
            
            

        }
        
        private void ImprimirTRSAlta()
        {
            try
            {
                // CONCEPTO
                //ID : 92010000  | NOMBRE : Art. 76 - Inc. 1 - Tasa retributiva por derecho de inscripcion en SIFCoS (Inscripcion)
                //ID : 92020000  | NOMBRE : Art. 76 - Inc. 2 - Por renovacion anual del derecho de inscripcion en el SIFCoS  (Reempadronamiento)
                
                //var idConcepto = ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos
                //    ? SingletonParametroGeneral.GetInstance().IdConceptoTasaAlta
                 //   : SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;

                 

                //imprimo la cantidad de Tasas que corresponden según la grilla.
                /*
                string resultado=""; 
                

                var fecDesdeConcepto = ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos
                 ? SingletonParametroGeneral.GetInstance().FecDesdeConceptoAlta
                 : SingletonParametroGeneral.GetInstance().FecDesdeConceptoReempadronamiento;
                var importeConcepto = ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos
                    ? 500
                    : 250;

                var conceptoTasa = new ConceptoTasa { id_concepto = idConcepto, fec_desde = DateTime.Parse(fecDesdeConcepto) };
                string nroTransaccion = null;
                */
                var lstAux = new List<Trs>();
                string urlTrs = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;

                int IdTipoTramiteTrs = 1;
                if (ObjetoInscripcion.TipoTramite == 4 || ObjetoInscripcion.TipoTramite == 2)
                    IdTipoTramiteTrs = 4;

                var dtExisteIPJ = Bl.BlGetEmpresa(ObjetoInscripcion.CUIT);
                if (dtExisteIPJ.Rows.Count == 0)
                {
                    /*Si el CUIT no pertenece a la tabla T_PERS_JURIDICA, el generar Transacción de la TRS daría error. Por eso se da de alta en la tabla al cuit.*/
                    var res = Bl.RegistrarEntidadPerJur(ObjetoInscripcion.CUIT, txtModalRazonSocial.Text,"");
                     
                }

                /*
                 resultado = Bl.GenerarTransaccionTRS(ObjetoInscripcion.CUIT, master.UsuarioCidiLogueado.Id_Sexo,
                    master.UsuarioCidiLogueado.NroDocumento, "ARG",
                    master.UsuarioCidiLogueado.Id_Numero, Int64.Parse(conceptoTasa.id_concepto), conceptoTasa.fec_desde,
                    "057", 1, importeConcepto, "", DateTime.Now.Year.ToString(), out nroTransaccion);
                  */
                string oFechaVenc;
                string oHashTrx;
                string oIdTransaccion;
                string oNroLiqOriginal;
                string strIdConcepto;
                string fecDesdeConcepto;
                string strMonto;
                string strConcpeto;

                var resultado = Bl.BlSolicitarTrs(IdTipoTramiteTrs, ObjetoInscripcion.CUIT, master.UsuarioCidiLogueado.CUIL,
                    out oFechaVenc, out oHashTrx, out oIdTransaccion, out oNroLiqOriginal, out strIdConcepto, out fecDesdeConcepto, out strMonto, out strConcpeto);
      
                //Agrego una trs a la lista auxiliar.
                //lstAux.Add(new Trs { NroTransaccion = oIdTransaccion, NroLiquidacion= oNroLiqOriginal });
                lstAux.Add(new Trs
                {
                    Concepto = strConcpeto,
                    Importe = strMonto,
                    NroTransaccion = oIdTransaccion,
                    Link = urlTrs + oHashTrx + "/" + oIdTransaccion,
                    NroLiquidacion = oNroLiqOriginal

                });



                if (resultado != "OK")
                {
                    lblMensajeError.Text = resultado;
                    divMensajeError.Visible = true;
                    return;
                }
                     

                /*
                if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
                    NroTransaccionTasa_Inscripcion = oIdTransaccion;
                ListaTrs = lstAuxoIdTransaccion
       */
                    if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
                    NroTransaccionTasa_Inscripcion = oIdTransaccion;
                    ListaTrs = lstAux;
     
                //PARA EL ALTA, SIGUE CON EL TRAMITE.
            }
            catch (Exception e)
            {
                lblMensajeError.Text = "Ocurrió un Error al Imprimir la TRS. Por favor intentelo más tarde.";
                divMensajeError.Visible = false;
            }
            
            

        }
		
   
        protected void btnAceptarTramiteARealizar_OnClick(object sender, EventArgs e)
        {
           // AceptarTramiteARealizar();
        }

        private void AceptarTramiteARealizar()
        {
            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Baja_de_Comercio)
            {
                //validar si existe el NroSifcos ingresado

                var bandTieneTramitesEnSifcosNuevo = false;


                foreach (var tramite in SessionTramitesDelCuit)
                {
                    if (tramite.Nro_Sifcos == txtNroSifcos.Text.Trim())
                    {
                        ObjetoInscripcion.NroSifcos = tramite.Nro_Sifcos;

                        //verifico que los tramites del nro sifcos encontrado sea del SIFCOS NUEVO.
                        foreach (var tramite2 in SessionTramitesDelCuit)
                        {
                            if (tramite2.Nro_Sifcos == ObjetoInscripcion.NroSifcos)
                            {
                                if (tramite2.Origen == "SIFCOS_NUEVO")
                                {
                                    ObjetoInscripcion.IdEntidad = tramite2.idEntidad;
                                    bandTieneTramitesEnSifcosNuevo = true;
                                    break;
                                }
                            }
                        }

                        break; //va a salir del loop por completo
                    }
                }


                //NRO SIFCoS SI EXISTE, Y ESTÁ POR REALIZAR EL REEMPADRONAMIENTO.
                //asigno FECHA DE VENCIMIENTO al tramite.
                int anios_debe = 0;
                DateTime? fecVto;
                DateTime? fecAux = null;
                    // es la fecha de presentación que se utiliza en el sifcos viejo.Representa que el PROX. VTO = FECHA_PRESENTACIÓN + 1 AÑO.
                DateTime fecUltVto;
                if (bandTieneTramitesEnSifcosNuevo)
                {
                    /*
                     *  1 -- ES UN TRAMITE DE REEMPADRONAMIENTO CON TRAMITES YA REALIZADOS EN EL SIFCOS NUEVO
                     */

                    fecVto = Bl.BlGetFechaUltimoTramiteSifcosNuevo(txtNroSifcos.Text.Trim());
                    fecAux = fecVto.Value.AddYears(-1);
                    TimeSpan ts = DateTime.Now - fecAux.Value;
                    anios_debe = ts.Days/365;
                    // NO DEBE REEMPADRONARSE, ESTÁ AL DÍA.
                    if (anios_debe == 0)
                    {
                        lblFechaUltimoTramite.Text = "Fecha del próximo vencimiento : " + fecVto.Value.Day + "/" +
                                                     fecVto.Value.Month + "/" + fecVto.Value.Year;
                        lblCantTrsPagas.Text =
                            "USTED SE ENCUENTRA AL DÍA. DEBE REALIZAR EL REEMPADRONAMIENTO POSTERIOR A LA FECHA DE VENCIMIENTO.";
                        divBotonImpirmirTRS.Visible = true;
                        divSeccionDebeTrs.Visible = false;
                        return;
                    }

                    ObjetoInscripcion.FechaVencimiento = fecVto.Value.AddYears(anios_debe);
                    fecUltVto = ObjetoInscripcion.FechaVencimiento.AddYears(-1);
                    PreCargarCamposParaReempadronamiento();
                }
                else
                {
                    /*  2 -- ES UN TRAMITE DE REEMPADRONAMIENTO POR PRIMERA VEZ. ES DECIR VIENE LOS DATOS DEL SIFCOS VIEJO.
                        */

                    fecVto = Bl.BlGetFechaUltimoTramiteSifcosViejo(txtNroSifcos.Text.Trim()).Value;
                        // me va a traer la fecha de presentacion.
                    fecAux = fecVto.Value.AddYears(1);
                    TimeSpan ts = DateTime.Now - fecAux.Value;
                    anios_debe = ts.Days/365;
                    ObjetoInscripcion.FechaVencimiento = fecAux.Value.AddYears(anios_debe + 1);
                    if (anios_debe == 0)
                    {
                        if (ts.Days >= 0 && ts.Days < 365)
                            anios_debe = 1;
                    }
                    else
                    {
                        anios_debe = anios_debe + 1;
                    }
                    fecUltVto = fecVto.Value.AddYears(1);
                }

                ObjetoInscripcion.CantidadReempadranamientos = anios_debe;

                //ObjetoInscripcion.FechaVencimiento --> contiene la fecha del proximo vencimiento. 
            }
        }

        private void PreCargarCamposParaReempadronamiento()
        {
            DataTable dt = Bl.BlGetUltimoTramiteSifcosNuevo(ObjetoInscripcion.NroSifcos);


            var _Id_tramite =int.Parse(dt.Rows[0]["NRO_TRAMITE_SIFCOS"].ToString());

           // dt.Rows[0]["ID_LOCALIDAD_CERTIFICA_RESP"].ToString();
            //dt.Rows[0]["FEC_CERTIFICACION_RESP"].ToString();
             
            //dt.Rows[0]["ID_ENTIDAD"].ToString();
           //dt.Rows[0]["CUIL_USUARIO_CIDI"].ToString();
            //int.Parse(dt.Rows[0]["ID_TIPO_TRAMITE"].ToString());
            //tramite.TipoTramiteNum = tramite.TipoTramite == 1 ? TipoTramiteEnum.Instripcion_Sifcos :
            //TipoTramiteEnum.Reempadronamiento;
             
           
            // int.Parse(dt.Rows[0]["ID_CARGO_ENTIDAD"].ToString());

            
          
            //dt.Rows[0]["FEC_INI_ACT_PPAL"].ToString();

            //
            //dt.Rows[0]["FEC_INI_ACT_SRIA"].ToString();
            // dt.Rows[0]["FEC_ALTA"].ToString();
             
            //Parse(dt.Rows[0]["FEC_VENCIMIENTO"].ToString());
            //tramite.IdEntidad = dt.Rows[0]["ID_ENTIDAD"].ToString();
            // dt.Rows[0]["CUIT"].ToString();
            //dt.Rows[0]["NRO_SIFCOS"].ToString();
           
            //txtLongitud.Text = dt.Rows[0]["LATITUD_UBI"].ToString();
            //txtLatitud.Text = dt.Rows[0]["LONGITUD_UBI"].ToString();
       
           // dt.Rows[0]["ID_SEDE"].ToString();
            

            //#region DOMICILIO DEL ESTABLECIMIENTO
            //var domicilio1 = consultarDomicilioByIdVin(dt.Rows[0]["ID_VIN_DOM_LOCAL"].ToString());

            //ddlDeptos.SelectedValue = domicilio1.IdDepartamento;
            //CargarLocalidades();

            //ddlLocalidad.SelectedValue = domicilio1.IdLocalidad;
            //CargarBarrios();
            //ddlBarrios.SelectedValue = string.IsNullOrEmpty(domicilio1.IdBarrio) ? "0" : domicilio1.IdBarrio;
            //txtCalle.Text = domicilio1.Calle;
            //txtNroCalle.Text = domicilio1.Altura;
            //txtPiso.Text = domicilio1.Piso;
            //txtNroDepto.Text = domicilio1.Depto;
            //txtCodPos.Text = domicilio1.CodigoPostal;
            
            //txtBarrio.Text = domicilio1.Barrio;
            ////txtOficina.Text =  dt.Rows[0]["OFICINA"].ToString();
            ////txtStand.Text = dt.Rows[0]["STAND"].ToString();
            ////txtLocal.Text = dt.Rows[0]["LOCAL"].ToString();
            //chkBarrioNoExiste.Enabled = !string.IsNullOrEmpty(domicilio1.Barrio);
            //#endregion // DOMICILIO DEL ESTABLECIMIENTO
            //#region CONTACTO
            //DataTable dtContacto = Bl.BlGetComEmpresa(_Id_tramite.ToString());

            //if (dtContacto.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dtContacto.Rows)
            //    {
            //        switch (row["ID_TIPO_COMUNICACION"].ToString())
            //        {
            //            case "01": //TELEFONO FIJO
            //                txtTelFijoCodArea.Text = row["COD_AREA"].ToString();
            //                txtTelFijo.Text = row["NRO_MAIL"].ToString();
            //                break;
            //            case "07": //CELULAR
            //                txtCelularCodArea.Text = row["COD_AREA"].ToString();
            //                txtCelular.Text= row["NRO_MAIL"].ToString();
            //                break;
            //            case "11": //EMAIL
            //                txtEmail_Establecimiento.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
            //                break;
            //            case "12": //PAGINA WEB
            //                txtWebPage.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
            //                break;
            //            case "17": //FACEBOOK
            //                txtRedSocial.Text  = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
            //                break;
            //        }
            //    }
            //}
            
            //#endregion //CONTACTO
            //#region DOMICILIO LEGAL
            //var domicilio2 = consultarDomicilioByIdVin(dt.Rows[0]["ID_VIN_DOM_LEGAL"].ToString());

            //ddlProvinciaLegal.SelectedValue = domicilio2.IdProvincia;
            //CargarDeptosLegal();
            //ddlDeptoLegal.SelectedValue = domicilio2.IdDepartamento;
            //CargarLocalidadesLegal();
            //ddlLocalidadLegal.SelectedValue = domicilio2.IdLocalidad;
            //CargarBarriosLegal();
            //ddlBarriosLegal.SelectedValue = string.IsNullOrEmpty(domicilio2.IdBarrio) ? "0" : domicilio2.IdBarrio;
            //txtBarrioLegal.Text = domicilio2.Barrio;
            //chkBarrioLegalNoExiste.Checked = !string.IsNullOrEmpty(domicilio2.Barrio);
            //txtCalleLegal.Text = domicilio2.Calle;
            //txtNroCalleLegal.Text = domicilio2.Altura;
            //txtCodPosLegal.Text = domicilio2.CodigoPostal;
            
            //#endregion //DOMICILIO LEGAL


            //#region PRODUCTO - ACTIVIDAD

            //DtProductos = Bl.BlGetProductosTramite(_Id_tramite.ToString());
            //RefrescarGrillaProductos();
            //chkConfirmarListaDeProducto.Checked = true; //no entra al EVENTO DEL CHECKBOX

            //ddlRubroPrimario.Enabled = true;
            //ddlRubroSecundario.Enabled = true;
            //CargarRubrosSegunProductos();

            //ddlRubroPrimario.SelectedValue = dt.Rows[0]["ID_ACTIVIDAD_PPAL"].ToString();
            //ddlRubroSecundario.SelectedValue = dt.Rows[0]["ID_ACTIVIDAD_SRIA"].ToString();

            //#endregion // PRODUCTO - ACTIVIDAD

            //#region INFO GENERAL
            //txtFechaIniAct.Text = dt.Rows[0]["FEC_INI_TRAMITE"].ToString();

            ////txtNroHabMun.Text = ""; // viene precargado antes.
            ////txtNroDGR.Text = "";// viene precargado antes.
            //var dtSuperficie = Bl.BlGetSuperficeByNroTramite(_Id_tramite);
            //foreach (DataRow row in dtSuperficie.Rows)
            //{
            //    switch (row["id_tipo_superficie"].ToString())
            //    {
            //        case "1": //SUP. ADMINISTRACION 
            //            txtM2Admin.Text = row["superficie"].ToString();
            //            break;
            //        case "2"://SUP. VENTAS
            //            txtM2Venta.Text = row["superficie"].ToString();
            //            break;
            //        case "3"://SUP. DEPOSITO
            //            txtM2Dep.Text = row["superficie"].ToString();
            //            break;
            //    }
            //}
            //txtCantPersRelDep.Text = dt.Rows[0]["CANT_PERS_REL_DEPENDENCIA"].ToString();
            //txtCantPersTotal.Text = dt.Rows[0]["CANT_PERS_TOTAL"].ToString();
            //rbPropietario.Checked = dt.Rows[0]["PROPIETARIO"].ToString() == "S";
            //rbInquilino.Checked = dt.Rows[0]["PROPIETARIO"].ToString() == "N";
            //rb1015.Checked = dt.Rows[0]["RANGO_ALQ"].ToString() == "$10000 a $15000";
            //rb1520.Checked = dt.Rows[0]["RANGO_ALQ"].ToString() == "mas de $15000";
            //rb5.Checked = dt.Rows[0]["RANGO_ALQ"].ToString() == "$5000";
            //rb510.Checked = dt.Rows[0]["RANGO_ALQ"].ToString() == "$5000 a $10000";
            //rbSiCap.Checked = dt.Rows[0]["CAPACITACION_ULT_ANIO"].ToString() == "S";
            //rbNoCap.Checked = dt.Rows[0]["CAPACITACION_ULT_ANIO"].ToString() == "N";
            //rbSiCobertura.Checked = dt.Rows[0]["COBERTURA_MEDICA"].ToString() == "S";
            //rbNoCobertura.Checked = dt.Rows[0]["COBERTURA_MEDICA"].ToString() == "N";
            //rbSiSeg.Checked = dt.Rows[0]["SEGURO_LOCAL"].ToString() == "S";
            //rbNoSeg.Checked = dt.Rows[0]["SEGURO_LOCAL"].ToString() == "N";

            //switch (dt.Rows[0]["ID_ORIGEN_PROVEEDOR"].ToString())
            //{
            //    case "1":
            //        chkprov.Checked = true;
            //        ChkNacional.Checked = false;
            //        ChkInter.Checked = false;
            //        break;
            //    case "2":
            //        chkprov.Checked = false;
            //        ChkNacional.Checked = true;
            //        ChkInter.Checked = false;
            //        break;
            //    case "3":
            //        chkprov.Checked = false;
            //        ChkNacional.Checked = false;
            //        ChkInter.Checked = true;
            //        break;
            //    case "12":
            //        chkprov.Checked = true;
            //        ChkNacional.Checked = true;
            //        ChkInter.Checked = false;
            //        break;
            //    case "123":
            //        chkprov.Checked = true;
            //        ChkNacional.Checked = true;
            //        ChkInter.Checked = true;
            //        break;
            //    case "23":
            //        chkprov.Checked = false;
            //        ChkNacional.Checked = true;
            //        ChkInter.Checked = true;
            //        break;
            //    case "13":
            //        chkprov.Checked = true;
            //        ChkNacional.Checked = false;
            //        ChkInter.Checked = true;
            //        break;
            //}



            //#endregion  //INFO GRAL

            //GESTOR DEL TRAMITE
            //var dtGestor = Bl.BlGetGestor(int.Parse(dt.Rows[0]["ID_GESTOR_ENTIDAD"].ToString()));
            //dtGestor.Rows[0]["CUIL"].ToString();
             
            //REPRESENTANTE LEGAL
        }
		
        protected void btnVolverTramiteARealizar_OnClick(object sender, EventArgs e)
        {
            if (NumeroDePasoActual == NumeroPasoEnum.PrimerPaso)    
            {
                divEncabezadoDatosEmpresa.Visible = false;
                HabilitarBotonesFooter(NumeroPasoEnum.PrimerPaso);
                NumeroDePasoActual = NumeroPasoEnum.PrimerPaso;
                //PintarTabBotonAnterior(li_tab_2, li_tab_1);
                MostrarOcultarModalTramiteSifcos(false);
                chkNuevaSucursal.Checked = false;

                divVentanaPasosTramite.Visible = false;
                divVentanaInicioTramite.Visible = true;
                txtCuit.Focus();
            }
        }
	
      
        protected void btnVolverFinalizacionTramite_OnClick(object sender, EventArgs e)
        {
           MostrarOcultarModalFinalizacionTramite(false);
        }


        private int conteoClicks = 0;
        protected void btnConfirmarImprimirTramite_OnClick(object sender, EventArgs e)
        {
            conteoClicks = ContadorClicksEnBotonConfirmar;
            
            if (ContadorClicksEnBotonConfirmar > 0)
            {
                //TRUE: existe el tramite en BD guadado , FALSE : no existe guardado en BD.
                bool existeTramite = Bl.BlExisteTramite(ObjetoInscripcion);

                if (existeTramite)
                {
                    return;
                }
                
            }

            if (ContadorClicksEnBotonConfirmar == 0)
            {
                 conteoClicks++;
                ContadorClicksEnBotonConfirmar = conteoClicks;
               
            }
           
            if (NumeroDePasoActual != NumeroPasoEnum.SegundoPaso)
            {
                return;
            }
            CargarDatosObjInscripcion(NumeroPasoEnum.SegundoPaso);

            //1 - ImprimirTRS();  
            // ImprimirTRS();

            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
            {
                //Si es un ALTA imprimo. si es Reempa.. no hace falta porque ya se utilizó las tasas al inicio.
                ImprimirTRSAlta();
                ObjetoInscripcion.NroSifcos = "0"; //DEJAR EL CERO!! NO CAMBIAR!! SINO SE ROMPE LA INSCRIPCIÓN. limpio el nro sifcos. por si estuvo en una sesión anterior realizando reempadronamiento.
            }

            //2 - GuardarInscripcionSifcos();
            GuardarInscripcionSifcos();
            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Baja_de_Comercio)
            {
                //Solo se actualiza el ID_ORGANISMO_ALTA si el tramite es de reempadronamiento.
                if (ObjetoInscripcion.NroSifcos != null)
                {
                    if (ObjetoInscripcion.NroTramite != null)
                    {
                        var res = Bl.BlActualizarOrganismoAltaPorNroSifcos(int.Parse(ObjetoInscripcion.NroTramite),
                            int.Parse(ObjetoInscripcion.NroSifcos));
                    }
                }
            }
            //3 - mostrar modal de mensaje de Felicitaciones Finalización del trámite.

            //4 - ImprimirReporte. reportViewer
            ImprimirReporteTramite();


            //MostrarOcultarModalFelicitacionesFin(true);
            //Sólo en producción.
            //5 - 

            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
            {
                Session["NroTransaccionParaImprimir"] = NroTransaccionTasa_Inscripcion;
            }
            else
            {
                Session["NroTransaccionParaImprimir"] = "noImprimir";
            }


            divVentanaInicioTramite.Visible = true;
            divVentanaPasosTramite.Visible = false;
            //MostrarOcultarModalFinalizacionTramite(false); //no me lo toma porque está fuera del updatePanel del boton Cnfirmar.
            MostrarOcultarModalFelicitacionesFin(true);
            LimpiarSessionesEnMemoria();

            Response.Redirect("MisTramites.aspx?Exito=1");
        }

        public int ContadorClicksEnBotonConfirmar
        {
            get
            {
                return Session["ContadorClicksEnBotonConfirmar"] == null
                    ? 0
                    : (int) Session["ContadorClicksEnBotonConfirmar"];
            } 
            set
            {
                Session["ContadorClicksEnBotonConfirmar"] = value;
            }
        }

        private void LimpiarSessionesEnMemoria()
        {
            //IdVinDomicilio1 = null;
            //IdVinDomicilio2 = null;
            NroTramiteAImprimir = 0;
            Session["NroTransaccionTasa_Inscripcion"] = null;
	        //Session["ReporteGeneral"] = null;
            ObjetoInscripcion = null;
	        IdDocumentoCDD1 = 0;
			IdDocumentoCDD2 = 0;
            //DtDomicilios1 = null;
            //DtDomicilios2 = null;
            //DtActividades = null;
            //DtBarrios = null;
            //DtComunicaciones = null;
            DtEmpresa = null;
            DtGrilla = null;
            //SessionBarrios = null;
            //SessionDepartamentos = null;
            //SessionLocalidades = null;
            SessionTramitesDelCuit = null;
        }

        public string NroTransaccionTasa_Inscripcion
        {
            get
            {
                return (string) Session["NroTransaccionTasa_Inscripcion"];
            }
            set
            {
                Session["NroTransaccionTasa_Inscripcion"] = value;
            }
        }

        private void ImprimirReporteTramite()
        {
            //var tramiteDto = new InscripcionSifcosDto();
            var lista = Bl.GetInscripcionSifcosDto(NroTramiteAImprimir).ToList();
            if (lista.Count == 0)
                return;
            InscripcionSifcosDto tramiteDto = lista[0];
          

            //tramiteDto.ActividadPrimaria = lblActividadPrimaria.Text;
            //tramiteDto.ActividadSecundaria = lblActividadSecundaria.Text;
            //tramiteDto.NroHabMunicipal = txtNroHabMun.Text;
            
            

            var lis = new List<Producto>();
           // DataTable dtProductosTramite = Bl.BlGetProductosTramite(NroTramiteAImprimir.ToString());
            //foreach (DataRow row in dtProductosTramite.Rows)
            //{
            //    lis.Add(new Producto { IdProducto = row["idproducto"].ToString(), NProducto = row["nproducto"].ToString() });
            //}

            DataTable dtContacto = Bl.BlGetComEmpresa(NroTramiteAImprimir.ToString());

            if (dtContacto.Rows.Count > 0)
            {
                foreach (DataRow row in dtContacto.Rows)
                {
                    switch (row["ID_TIPO_COMUNICACION"].ToString())
                                    {
                                        case "01": //TELEFONO FIJO
                                            tramiteDto.CodAreaTelFijo = row["COD_AREA"].ToString();
                                            tramiteDto.TelFijo = row["NRO_MAIL"].ToString();
                                            break;
                                        case "07": //CELULAR
                                            tramiteDto.CodAreaCelular = row["COD_AREA"].ToString();
                                           tramiteDto.Celular = row["NRO_MAIL"].ToString();
                                            break;
                                        case "11": //EMAIL
                                           tramiteDto.EmailEstablecimiento = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - "; 
                                           break;
                                        case "12": //PAGINA WEB
                                            tramiteDto.WebPage =!string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - "; 
                                            break;
                                        case "17": //FACEBOOK
                                            tramiteDto.Facebook =!string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - "; 
                                            break;
                        
                                    }
                }
                
            }


            tramiteDto.CelularGestor = UsuarioCidiGestor != null ? UsuarioCidiGestor.CelFormateado : " - ";  
            tramiteDto.CelularRepLegal = UsuarioCidiRep != null ? UsuarioCidiRep.CelFormateado : " - ";
            
            //var domicilio1 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLocal.Value.ToString());
            //var domicilio2 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLegal.Value.ToString());

            //var dtSuperficie = Bl.BlGetSuperficeByNroTramite(Convert.ToInt64(tramiteDto.NroTramiteSifcos));  
            
            //foreach (DataRow row in dtSuperficie.Rows)
            //{
            //    switch (row["id_tipo_superficie"].ToString())
            //    {
            //        case "1": //SUP. ADMINISTRACION 
            //            tramiteDto.SupAdministracion = string.IsNullOrEmpty(row["superficie"].ToString()) ? 0 : long.Parse(row["superficie"].ToString());
            //            break;
            //        case "2"://SUP. VENTAS
            //            tramiteDto.SupVentas = string.IsNullOrEmpty(row["superficie"].ToString()) ? 0 : long.Parse(row["superficie"].ToString());
            //            break;
            //        case "3"://SUP. DEPOSITO
            //            tramiteDto.SupDeposito = string.IsNullOrEmpty(row["superficie"].ToString()) ? 0 : long.Parse(row["superficie"].ToString());
            //            break;
            //    }
            //}
            // Creo el nuevo reporte con NOMBRE DEL REPORTE

            var nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSBaja.rdlc";
            if(ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Baja_de_Comercio)
                nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSBaja.rdlc";

            var reporte = new ReporteGeneral(nombreReporteRdlc, lis, TipoArchivoEnum.Pdf);

            reporte.AddParameter("parametro_fecha_baja", Fecha_baja_municipal.ToString());
            reporte.AddParameter("parametro_Titulo_reporte", "Comprobante de Trámite - " + "TRAMITE DE BAJA");
            reporte.AddParameter("nroTramiteSifcos", tramiteDto.NroTramiteSifcos);
            reporte.AddParameter("paramatro_dom1_departamento", null);
            reporte.AddParameter("paramatro_dom1_localidad", null);
            reporte.AddParameter("paramatro_dom1_barrio", null);
            reporte.AddParameter("paramatro_dom1_calle", null);
            reporte.AddParameter("paramatro_dom1_nroCalle", null);
            reporte.AddParameter("paramatro_dom1_dpto", null);
            reporte.AddParameter("paramatro_dom1_piso", null);
            reporte.AddParameter("paramatro_dom1_codPos", null);
            reporte.AddParameter("paramatro_dom1_oficina", tramiteDto.Oficina);
            reporte.AddParameter("paramatro_dom1_local", tramiteDto.Local);
            reporte.AddParameter("paramatro_dom1_stand", tramiteDto.Stand);
            reporte.AddParameter("paramatro_contacto_email", !string.IsNullOrEmpty(tramiteDto.EmailEstablecimiento) ? tramiteDto.EmailEstablecimiento : " - ");
            reporte.AddParameter("paramatro_contacto_facebook", !string.IsNullOrEmpty(tramiteDto.Facebook) ? tramiteDto.Facebook : " - ");
            reporte.AddParameter("paramatro_contacto_WebPage", !string.IsNullOrEmpty(tramiteDto.WebPage) ? tramiteDto.WebPage : " - ");
            reporte.AddParameter("paramatro_contacto_celular", !string.IsNullOrEmpty(tramiteDto.Celular) ? "(" + tramiteDto.CodAreaCelular + ") " + tramiteDto.Celular : " - " );
            reporte.AddParameter("paramatro_contacto_telFijo", !string.IsNullOrEmpty(tramiteDto.TelFijo) ? "(" +tramiteDto.CodAreaTelFijo + ") " + tramiteDto.TelFijo : " - ");
            reporte.AddParameter("paramatro_dom2_departamento", null);
            reporte.AddParameter("paramatro_dom2_localidad", null);
            reporte.AddParameter("paramatro_dom2_barrio", null);
            reporte.AddParameter("paramatro_dom2_calle", null);
            reporte.AddParameter("paramatro_dom2_nroCalle", null);
            reporte.AddParameter("paramatro_dom2_dpto", null);
            reporte.AddParameter("paramatro_dom2_piso", null);
            reporte.AddParameter("paramatro_dom2_codPos", null);
            reporte.AddParameter("paramatro_dom2_oficina", tramiteDto.Stand);
            reporte.AddParameter("paramatro_dom2_local", tramiteDto.Local);
            reporte.AddParameter("paramatro_dom2_stand", tramiteDto.Oficina);
            reporte.AddParameter("paramatro_fecInicioAct", tramiteDto.FecIniTramite.ToShortDateString());
            reporte.AddParameter("paramatro_nroHabMunicipal", tramiteDto.NroHabMunicipal);
            reporte.AddParameter("paramatro_nroDGR", tramiteDto.NroDGR);
            reporte.AddParameter("paramatro_supVenta", tramiteDto.SupVentas != null ? tramiteDto.SupVentas.Value.ToString() : "-");
            reporte.AddParameter("paramatro_supDeposito", tramiteDto.SupDeposito != null ? tramiteDto.SupDeposito.Value.ToString() : "-");
            reporte.AddParameter("paramatro_supAdm", tramiteDto.SupAdministracion != null ? tramiteDto.SupAdministracion.Value.ToString() : "-");
            reporte.AddParameter("paramatro_cantPersTotal", tramiteDto.CantTotalpers);
            reporte.AddParameter("paramatro_cantPersRelacionDep", tramiteDto.CantPersRelDep);
            reporte.AddParameter("paramatro_poseeCobertura", tramiteDto.Cobertura);
            reporte.AddParameter("paramatro_realizoCapacitacion", tramiteDto.CapacUltAnio);
            reporte.AddParameter("paramatro_poseeSeguro", tramiteDto.Seguro);
            reporte.AddParameter("paramatro_origenProveedor", tramiteDto.NombreOrigenProveedor);
            reporte.AddParameter("parametro_empresa_cuit", tramiteDto.CUIT);
            reporte.AddParameter("parametro_empresa_razonSocial", tramiteDto.RazonSocial);
            reporte.AddParameter("parametro_empresa_AnioOperativo", DateTime.Now.Year.ToString());
            reporte.AddParameter("parametro_actividadPrimaria", tramiteDto.ActividadPrimaria);
            reporte.AddParameter("parametro_actividadSecundaria", tramiteDto.ActividadSecundaria);
            reporte.AddParameter("parametro_estado_tramite", tramiteDto.NombreEstadoActual);
            reporte.AddParameter("parametro_fecha_vencimiento", tramiteDto.FecVencimiento.Day + "/" + tramiteDto.FecVencimiento.Month + "/" + tramiteDto.FecVencimiento.Year);
            
            //cargo los datos del gestor y responsable
            reporte.AddParameter("parametro_gestor_nombre", tramiteDto.NombreYApellidoGestor);
            reporte.AddParameter("parametro_gestor_dni", tramiteDto.CuilGestor);
            reporte.AddParameter("parametro_gestor_telefono", tramiteDto.CelularGestor);  
            reporte.AddParameter("parametro_gestor_tipo", tramiteDto.NombreTipoGestor);
            reporte.AddParameter("parametro_responsable_nombre", tramiteDto.NombreYApellidoRepLegal);
            reporte.AddParameter("parametro_responsable_dni", tramiteDto.CuilRepLegal);
            reporte.AddParameter("parametro_responsable_telefono", tramiteDto.CelularRepLegal);
            reporte.AddParameter("parametro_responsable_cargo", tramiteDto.NombreCargoRepLegal);
            reporte.AddParameter("parametro_rango_alq", tramiteDto.RangoAlquiler);
            var inmueble = tramiteDto.Propietario == "S"
                ? "Propietario"
                : "Inquilino";
            reporte.AddParameter("parametro_propietario", inmueble);
            reporte.AddParameter("parametro_nombre_fantasia", tramiteDto.NombreFantasia);


            
            // Guardo datos del reporte en sessión
            Session["ReporteGeneral"] = reporte;

            //LEVANTA LA PAGINA RerporteGeneral
            //Response.Redirect("ReporteGeneral.aspx");
           
        }

        //protected void btnImprimirReportePrueba_OnClick(object sender, EventArgs e)
        //{
        //    NroTramiteAImprimir = 320;
        //    ImprimirReporteTramite();
        //}


        /// <summary>
        /// Nro de trámite que se acaba de generar.
        /// </summary>
        public long NroTramiteAImprimir
        {
            get
            {
                return Session["NroTramiteAImprimir"] == null ? 0 : (long) Session["NroTramiteAImprimir"];
            }
            set
            {
                Session["NroTramiteAImprimir"] = value;
            }
        }

        public AppComunicacion.ApiModels.Domicilio consultarDomicilioByIdVin(string idVinculacion)
        {
            var Domicilio = Bl.BlGetDomEmpresaByIdVin(idVinculacion);

            return Domicilio;
        }

        
         
        protected void chkNuevaSucursal_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkNuevaSucursal.Checked)
            {

                divSeccionInscripcionTramite.Visible = true;
                divSeccionReempadronamiento.Visible = false;

                lblTituloVentanaModalPrincipal.Text = "ALTA DE TRÁMITE";
                //modalInformacionTituloTramite.Visible = false;
                //MostrarOcultarModalVentanaMantSistema(true);
            }
            else
            {
                divSeccionInscripcionTramite.Visible = false;
                divSeccionReempadronamiento.Visible = true;
                lblTituloVentanaModalPrincipal.Text = "REEMPADRONAMIENTO - SIFCoS";
            }
             

        }
        
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] BuscarProducto(string prefixText, int count)
        {
            List<Producto> _productos = Bl_static.BlGetProductosbeta(prefixText.ToUpper()).ToList();

            string[] lista = new string[_productos.Count];
            var contador = 0;
            foreach (var row_producto in _productos)
            {
                lista[contador] = AutoCompleteExtender.CreateAutoCompleteItem(row_producto.NProducto, row_producto.IdProducto);
                contador++;
            }

            return lista;
        }

     
        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetLatitudLongitud(string pLatitud, string pLongitud)
        {
            //es necesario setear el control texbox de latitud y long. para tomar los valores del lado del servidor.
            //txtLatitud.Text = pLatitud;
            //txtLongitud.Text = pLongitud;

        }
        
        protected void btnIniciarTramite_OnClick(object sender, EventArgs e)
        {
            
             
        }
		
        protected void btnSalir_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalVentanaSalirDelTramite(true);
        }

        protected void btnAcepterYSalirTramite_OnClick(object sender, EventArgs e)
        {
            // borrar los datos hasta el momento.
           
            divVentanaInicioTramite.Visible = true;
            divVentanaPasosTramite.Visible = false;

            Response.Redirect("Inscripcion.aspx");
        }

        protected void btnCancelarYSeguirTramite_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalVentanaSalirDelTramite(false);
        }
		
		
        protected void btnBuscarRepresentante_Click(object sender, EventArgs e)
        {
            ObtenerUsuarioRepresentante();
            
        }

        protected void DdlTipoGestor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlTipoGestor.SelectedValue)
            {
                case "1"://TITULAR
                    divMatriculaContador.Visible = false;
                    divRepresentanteLegal.Visible = false;
                    break;
                case "2"://GESTOR
                    divMatriculaContador.Visible = false;
                    divRepresentanteLegal.Visible = true;
                    break;
                case "3"://CONTADOR
                    divMatriculaContador.Visible = true;
                    divRepresentanteLegal.Visible = true;
                    break;
            }
        }
		

        protected void btnIrAMisTramites_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MisTramites.aspx");
        }

        protected void btnRealizarOtroTramite_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Inscripcion.aspx");
        }

        protected void btnDescargarComprobante_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ReporteGeneral.aspx");
        }

		
        protected void btnSalir2_OnClick(object sender, EventArgs e)
        {
           LimpiarSessionesEnMemoria();
           LimpiarControlesRequeridos();
           Response.Redirect("Inscripcion.aspx");
        }

        protected void btnConfirmarBaja_OnClick(object sender, EventArgs e)
        {
           
            //CargarDatosObjInscripcion(NumeroPasoEnum.PrimerPaso);
            CargarDatosObjInscripcion(NumeroPasoEnum.CuartoPaso);

            //1 - ImprimirTRS();  
            // ImprimirTRS();

            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
            {
                //Si es un ALTA imprimo. si es Reempa.. no hace falta porque ya se utilizó las tasas al inicio.
                ImprimirTRSAlta();
            }

            //2 - GuardarInscripcionSifcos();
            GuardarInscripcionSifcos();

            //3 - mostrar modal de mensaje de Felicitaciones Finalización del trámite.

            //4 - ImprimirReporte. reportViewer
            ImprimirReporteTramite();


            //MostrarOcultarModalFelicitacionesFin(true);
            //Sólo en producción.
            //5 - 

            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
            {
                Session["NroTransaccionParaImprimir"] = NroTransaccionTasa_Inscripcion;
            }
            else
            {
                Session["NroTransaccionParaImprimir"] = "noImprimir";
            }


            divVentanaInicioTramite.Visible = true;
            divVentanaPasosTramite.Visible = false;
            //MostrarOcultarModalFinalizacionTramite(false); //no me lo toma porque está fuera del updatePanel del boton Cnfirmar.
            MostrarOcultarModalFelicitacionesFin(true);
            LimpiarSessionesEnMemoria();

            Response.Redirect("MisTramites.aspx?Exito=1");
        }



		#region CDD

		private int IdDocumentoCDD1
		{
			get
			{
				var a = Session["IdDocumentoCDD1"];
				if (a != null)
					return (int)a;
				return 0;
			}
			set { Session["IdDocumentoCDD1"] = value; }
		}
		private int IdDocumentoCDD2
		{
			get
			{
				var a = Session["IdDocumentoCDD2"];
				if (a != null)
					return (int)a;
				return 0;
			}
			set { Session["IdDocumentoCDD2"] = value; }
		}

		private CryptoDiffieHellman ObjDiffieHellman { get; set; }
		public CngKey Private_Key { get; set; }

		protected void BtnAdjuntarOtro1_Click(object sender, EventArgs e)
		{
			documento1 = null;
			DivAdjuntar.Visible = true;
			IdDocumentoCDD1 = new int();
		}
		protected void BtnAdjuntarOtro2_Click(object sender, EventArgs e)
		{
			documento2 = null;
			DivAdjuntar.Visible = true;
			IdDocumentoCDD2 = new int();
		}
		protected void BtnAdjuntar1_Click(object sender, EventArgs e)
		{

			if (documento1.HasFile)
			{
				var tamanoarchivo = documento1.FileContent.Length;
				if (tamanoarchivo > 4194304)
				{
					divMensajeErrorDocumentacion_1.Visible = true;
					lblMensajeErrorDocumentacion_1.Text = "El tamaño maximo permitido por archivo es de 4 MB";
					return;
				}
			}
			else
			{
				divMensajeErrorDocumentacion_1.Visible = true;
				lblMensajeErrorDocumentacion_1.Text = "No Hay un Archivo seleccionado.";
				return;
			}
			var _respLogin = new CDDResponseLogin();
			var _response = new CDDResponseInsercion();

			try
			{
				_respLogin = Get_Authorize_Web_Api_CDD_insert();

				if (_respLogin != null && _respLogin.Codigo_Resultado.Equals("SEO"))
				{
					if (Set_Parameters_Request_Post_Insert(_respLogin.Llave_BLOB_Login, 1))
					{
						_response = Save_Document_Web_Api_CDD();

						if (_response != null && _response.Codigo_Resultado.Equals("SEO"))
						{
							Show_Results(_response, 1);
							btnAdjuntarOtro1.Visible = true;
						}
						else
						{
							divMensajeErrorDocumentacion_1.Visible = true;
							lblMensajeErrorDocumentacion_1.Text = "Adjuntado erróneo. Codigo Error: " + _response.Codigo_Resultado +
												   " Descripcion: " + _response.Detalle_Resultado;
							btnAdjuntarOtro1.Visible = false;
						}
					}
				}
				else
				{
					divMensajeErrorDocumentacion_1.Visible = true;
					lblMensajeErrorDocumentacion_1.Text = "Autorización denegada desde la Web APi CDD. Codigo Error: " +
										   _respLogin.Codigo_Resultado +
										   " Descripcion: " + _respLogin.Detalle_Resultado;
					btnAdjuntarOtro1.Visible = false;
				}
			}
			catch (CDDException waEx)
			{
				divMensajeErrorDocumentacion_1.Visible = true;
				lblMensajeErrorDocumentacion_1.Text = "Codigo Error: " + waEx.ErrorCode +
									   " Descripcion: " + waEx.ErrorDescription;
				btnAdjuntarOtro2.Visible = false;
			}


		}
		protected void BtnAdjuntar2_Click(object sender, EventArgs e)
		{

			if (documento2.HasFile)
			{
				var tamanoarchivo = documento2.FileContent.Length;
				if (tamanoarchivo > 4194304)
				{
					divMensajeErrorDocumentacion_2.Visible = true;
					lblMensajeErrorDocumentacion_2.Text = "El tamaño maximo permitido por archivo es de 4 MB";
					return;
				}
			}
			else
			{
				divMensajeErrorDocumentacion_2.Visible = true;
				lblMensajeErrorDocumentacion_2.Text = "No Hay un Archivo seleccionado.";
				return;
			}
			var _respLogin = new CDDResponseLogin();
			var _response = new CDDResponseInsercion();

			try
			{
				_respLogin = Get_Authorize_Web_Api_CDD_insert();

				if (_respLogin != null && _respLogin.Codigo_Resultado.Equals("SEO"))
				{
					if (Set_Parameters_Request_Post_Insert(_respLogin.Llave_BLOB_Login, 2))
					{
						_response = Save_Document_Web_Api_CDD();

						if (_response != null && _response.Codigo_Resultado.Equals("SEO"))
						{
							Show_Results(_response, 2);
							btnAdjuntarOtro2.Visible = true;
						}
						else
						{
							divMensajeErrorDocumentacion_2.Visible = true;
							lblMensajeErrorDocumentacion_2.Text = "Adjuntado erróneo. Codigo Error: " + _response.Codigo_Resultado +
												   " Descripcion: " + _response.Detalle_Resultado;
							btnAdjuntarOtro2.Visible = false;
						}
					}
				}
				else
				{
					divMensajeErrorDocumentacion_2.Visible = true;
					lblMensajeErrorDocumentacion_2.Text = "Autorización denegada desde la Web APi CDD. Codigo Error: " +
										   _respLogin.Codigo_Resultado +
										   " Descripcion: " + _respLogin.Detalle_Resultado;
					btnAdjuntarOtro2.Visible = false;
				}
			}
			catch (CDDException waEx)
			{
				divMensajeErrorDocumentacion_2.Visible = true;
				lblMensajeErrorDocumentacion_2.Text = "Codigo Error: " + waEx.ErrorCode +
									   " Descripcion: " + waEx.ErrorDescription;
				btnAdjuntarOtro2.Visible = false;
			}


		}
		internal CDDResponseLogin Get_Authorize_Web_Api_CDD_insert()
		{
			var _cddRespLogin = new CDDResponseLogin();
			var _cddMapError = new CDDMapError();
			string rawjson = "";
			string result = "";
			string cadena = "";
			try
			{

				Initialize_Authorize_insert(out cadena);

				//_cddRespLogin =
				//    MapeadorWebApi.ConsumirWebApi<Autorizador, CDDResponseLogin>(MapeadorWebApi.Autorizar_Solicitud,
				//        ObjAutorizador);

				var httpWebRequest = (HttpWebRequest)WebRequest.Create(MapeadorWebApi.Autorizar_Solicitud);
				httpWebRequest.ContentType = "application/json; charset=utf-8";

				rawjson = JsonConvert.SerializeObject(ObjAutorizador);
				httpWebRequest.Method = "POST";

				var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());

				streamWriter.Write(rawjson);
				streamWriter.Flush();
				streamWriter.Close();

				var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				var streamReader = new StreamReader(httpResponse.GetResponseStream());
				result = streamReader.ReadToEnd();

				_cddRespLogin = JsonConvert.DeserializeObject<CDDResponseLogin>(result);
				_cddRespLogin.Detalle_Resultado = _cddRespLogin.Detalle_Resultado + "INTERNO:" + rawjson + "______" + result + "________" + cadena;




				if (_cddRespLogin != null)
				{
					if (_cddRespLogin.Codigo_Resultado.Equals("SEO"))
						Complete_Authorize(_cddRespLogin.Llave_BLOB_Login);
				}
			}
			catch (CDDException waEx)
			{
				_cddRespLogin.Codigo_Resultado = waEx.ErrorCode;
				_cddRespLogin.Detalle_Resultado = waEx.ErrorDescription;
			}
			catch (TimeoutException ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: TimeOut. " +
												  ex.StackTrace + "INTERNO:" + rawjson + "______" + result;
			}
			catch (WebException ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: WebException." +
												  ex.StackTrace + "INTERNO:" + rawjson + "______" + result;
			}
			catch (CryptoManagerException ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: Exception." +
												  ex.StackTrace + "INTERNO:" + cadena + rawjson + "______" + result;

			}
			catch (Exception ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: Exception." +
												  ex.StackTrace + "INTERNO:" + cadena + rawjson + "______" + result;
			}


			return _cddRespLogin;
		}
		private void Complete_Authorize(byte[] pPublicBlobKey)
		{
			try
			{
				// Creo la llave compartida
				ObjAutorizador.Shared_Key = ObjDiffieHellman.Share_Key_Generate(pPublicBlobKey);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void Initialize_Authorize_insert(out string cadena)
		{
			ObjDiffieHellman = new CryptoDiffieHellman();
			ObjAutorizador = new Autorizador();

			var _helper = new Helpers();
			var _cmd = new CryptoManagerData();

			String _mensaje = String.Empty;
			cadena = null;
			try
			{
				//TODO FACU : Se cambia porque no funcionaba el id_aplicaicon_origen. 

				//ObjAutorizador.Id_Aplicacion_Origen = Convert.ToInt32(MapeadorWebApi.Id_Aplicacion_Origen);

				ObjAutorizador.Id_Aplicacion_Origen =
					Convert.ToInt32(ConfigurationManager.AppSettings["CiDiIdAplicacion"].ToString());
				ObjAutorizador.Pwd_Aplicacion = _helper.Encriptar_Password(MapeadorWebApi.Pwd_Aplicacion_Origen);
				ObjAutorizador.Key_Aplicacion = MapeadorWebApi.Key_Aplicacion_Origen;
				ObjAutorizador.Operacion = "3"; // 3 = Insercion
				ObjAutorizador.Id_Usuario = MapeadorWebApi.User_Aplicacion_Origen;
				ObjAutorizador.Time_Stamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");


				cadena = cadena + "ENTRADAId_Aplicacion_Origen:" + ObjAutorizador.Id_Aplicacion_Origen + "ENTRADAPwd_Aplicacion:" +
						 ObjAutorizador.Pwd_Aplicacion + "ENTRADAKey_Aplicacion:" + ObjAutorizador.Key_Aplicacion + "ENTRADAOperacion:"
						 + ObjAutorizador.Operacion + "ENTRADAId_Usuario:" + ObjAutorizador.Id_Usuario + "ENTRADATime_Stamp:" + ObjAutorizador.Time_Stamp;
				cadena = cadena + 1 + "-";
				try
				{
					ObjAutorizador.Token = _cmd.Get_Token(ObjAutorizador.Time_Stamp, ObjAutorizador.Key_Aplicacion);
					cadena = cadena + ObjAutorizador.Token + "-";
					if (!string.IsNullOrEmpty(_mensaje)) throw new Exception(_mensaje);
					cadena = cadena + 3 + "-";
				}
				catch (Exception ex)
				{
					cadena = cadena + 4 + "-";
					throw ex;
				}

				// Creo la llave CNG
				Private_Key = ObjDiffieHellman.Create_Key_ECCDHP521();
				cadena = cadena + 5 + "-";
				// Creo la llave Blob pública
				ObjAutorizador.Public_Blob_Key = ObjDiffieHellman.Export_Key_Material();
				cadena = cadena + 6 + "-";
				// Llave compartida
				ObjAutorizador.Shared_Key = null;
			}
			catch (CryptoManagerException cryptoex)
			{
				cadena = cadena + 7 + "-" + cryptoex.ErrorDescription + cryptoex.ErrorCode;
				throw cryptoex;

			}

		}
		internal bool Set_Parameters_Request_Post_Insert(byte[] _p_Blob_Key, int opcion)
		{
			var objCryptoHash = new CryptoManagerV4._0.Clases.CryptoHash();
			String mensaje = String.Empty;
			bool _continuar = true;
			RequestPost = new CDDPost();
			var dc = new DocumentConverter();


			/*TODO FACU : Aquí se cambia el ID_APLICACIÓN_ORIGEN porque sinó no funcionaba.*/

			//RequestPost.Id_Aplicacion_Origen = ObjAutorizador.Id_Aplicacion_Origen; ANTES

			RequestPost.Id_Aplicacion_Origen = Convert.ToInt32(MapeadorWebApi.Id_Aplicacion_Origen);
			RequestPost.Pwd_Aplicacion = ObjAutorizador.Pwd_Aplicacion;
			RequestPost.IdUsuario = ObjAutorizador.Id_Usuario;
			RequestPost.Shared_Key = _p_Blob_Key;
			RequestPost.Id_Documento = 0;


			/* 
			 * Asignar el valor del Tipo de Documento en el Web.config según la asignacion de permisos.
			 * Solicitar los permisos a DesarrolloCiDi@cba.gov.ar
			 */
			if (ConfigurationManager.AppSettings["Id_Tipo_Documento"] != null)
				RequestPost.Id_Catalogo = Convert.ToInt32(MapeadorWebApi.Id_Tipo_Documento);
			else
				RequestPost.Id_Catalogo = 0;


			RequestPost.N_Documento = "Documentación_" + txtCuit.Text.Trim();  //documento.PostedFile.FileName;
			RequestPost.Extension = "pdf";

			//documento.PostedFile.FileName.Substring(documento.PostedFile.FileName.LastIndexOf(".") + 1).ToUpper();
			if (opcion == 1)
				RequestPost.Blob_Imagen = objCryptoHash.Cifrar_Datos(documento1.FileBytes, out mensaje);
			if (opcion == 2)
				RequestPost.Blob_Imagen = objCryptoHash.Cifrar_Datos(documento2.FileBytes, out mensaje);



			RequestPost.Vigencia = DateTime.Now.AddYears(1);
			RequestPost.N_Constatado = true;


			return _continuar;
		}
		internal CDDResponseInsercion Save_Document_Web_Api_CDD()
		{
			var _respuesta = new CDDResponseInsercion();
			var _cddMapError = new CDDMapError();

			try
			{
				_respuesta =
					MapeadorWebApi.ConsumirWebApi<CDDPost, CDDResponseInsercion>(MapeadorWebApi.Guardar_Documento,
						RequestPost);
			}
			catch (CDDException waEx)
			{
				_respuesta.Codigo_Resultado = waEx.ErrorCode;
				_respuesta.Detalle_Resultado = waEx.ErrorDescription;
			}
			catch (TimeoutException ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_respuesta.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_respuesta.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: TimeOut. " +
											   ex.StackTrace;
			}
			catch (WebException ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_respuesta.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_respuesta.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: WebException." +
											   ex.StackTrace;
			}
			catch (Exception ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_respuesta.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_respuesta.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: Exception." +
											   ex.StackTrace;
			}

			return _respuesta;
		}
		internal void Show_Results(CDDResponseInsercion _p_response, int opcion)
		{
			if (_p_response != null && _p_response.Codigo_Resultado.Equals("SEO"))
			{
				if (opcion == 1)
				{
					IdDocumentoCDD1 = _p_response.Id_Documento;
					divMensajeErrorDocumentacion_1.Visible = false;
					divMensajeExitoDocumentacion_1.Visible = true;
					lblMensajeExitoDocumentacion_1.Text = "Se digitalizó correctamente el documento con nro: " +
														  _p_response.Id_Documento;
				}
				if (opcion == 2)
				{
					IdDocumentoCDD2 = _p_response.Id_Documento;
					divMensajeErrorDocumentacion_2.Visible = false;
					divMensajeExitoDocumentacion_2.Visible = true;
					lblMensajeExitoDocumentacion_2.Text = "Se digitalizó correctamente el documento con nro: " +
														  _p_response.Id_Documento;
				}
				//DivAdjuntar.Visible = false;
			}
			else
			{

				if (opcion == 1)
				{

					divMensajeErrorDocumentacion_1.Visible = true;
					divMensajeExitoDocumentacion_1.Visible = false;
					lblMensajeErrorDocumentacion_1.Text = "Codigo Error: " + _p_response.Codigo_Resultado + " Descripcion: " +
														_p_response.Detalle_Resultado;
				}
				if (opcion == 2)
				{

					divMensajeErrorDocumentacion_2.Visible = true;
					divMensajeExitoDocumentacion_2.Visible = false;
					lblMensajeErrorDocumentacion_2.Text = "Codigo Error: " + _p_response.Codigo_Resultado + " Descripcion: " +
														_p_response.Detalle_Resultado;

				}

				IdDocumentoCDD1 = 0; IdDocumentoCDD2 = 0;

			}
		}

		#endregion
    }
}