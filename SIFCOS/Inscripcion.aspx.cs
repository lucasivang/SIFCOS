using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using AjaxControlToolkit;
using CryptoManagerV4._0.Clases;
using CryptoManagerV4._0.Excepciones;
using CryptoManagerV4._0.General;
using DA_SIFCOS;
using Newtonsoft.Json;
using ThoughtWorks.QRCode.Codec;
using System.Collections;
using DA_SIFCOS.Entities.CDDAutorizador;
using DA_SIFCOS.Entities.CDDPost;
using DA_SIFCOS.Entities.CDDResponse;
using DA_SIFCOS.Entities.Errores;
using DA_SIFCOS.Entities.Excepcion;
using DA_SIFCOS.Utils;
using BL_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;

namespace SIFCOS
{
    public partial class Inscripcion : System.Web.UI.Page
    {
        private string commandoBotonPaginaSeleccionadoDir = "";
        private bool banderaPrimeraCargaPaginaDir = false;
        private Autorizador ObjAutorizador { get; set; }
        private CDDPost RequestPost { get; set; }

        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();

        DataColumn Producto = new DataColumn("PRODUCTO", System.Type.GetType("System.String"));
        DataColumn Rubro = new DataColumn("RUBRO", System.Type.GetType("System.String"));

        List<Roles> Autorizados = new List<Roles>();
        protected DataTable DtGrilla = new DataTable();
        public Int64 pExiste;
        public Int64 pVencida;
        public Principal master;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CuitAutorizados"] != null)
            {
                Autorizados = (List<Roles>)Session["CuitAutorizados"];
            }

            var test = (string)Session["test"];

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
            DtGrilla.Columns.Add(Producto);
            DtGrilla.Columns.Add(Rubro);
            if (Request.Cookies["CiDi"] != null)
            {

                if (!Page.IsPostBack)
                {
                    LimpiarSessionesEnMemoria();

                    divVentanaPasosTramite.Visible = false;
                    divEncabezadoDatosEmpresa.Visible = false;

                    CargarDatosUsuarioLogueado();
                    NumeroDePasoActual = NumeroPasoEnum.PrimerPaso;
                    HabilitarBotonesFooter(NumeroDePasoActual);

                    CargarProductos();

                    lblTituloTramite.Text = "Alta Trámite SIFCoS";
                    divBotonImpirmirTRS.Visible = false;
                    divMensaejeErrorModal.Visible = true;
                    divMensaejeErrorModal.Visible = false;
                    DtActividades = null;
                    DtProductos = null;
                    RefrescarGrillaProductos();
                    if (Autorizados.Count > 0)
                    {
                        DivSeleccionCuit.Visible = true;
                        DivIngresoCuit.Visible = false;
                        ddlSeleccionEntidad.Items.Clear();
                        ddlSeleccionEntidad.Items.Add(new ListItem("SELECCIONE CUIT AUTORIZADO", "0"));
                        ddlSeleccionEntidad.Items.Add(new ListItem(master.UsuarioCidiLogueado.CUIL, "1"));
                        ddlSeleccionEntidad.SelectedValue = "0";
                        foreach (var Rol in Autorizados)
                        {
                            {
                                ddlSeleccionEntidad.Items.Add(new ListItem(Rol.Permiso, Rol.Rol.ToString()));
                            }
                        }
                    }
                    else
                    {
                        if ((String)Session["RdoCuilCuit"] == "")
                        {
                            txtCuit.Text = master.UsuarioCidiLogueado.CUIL;
                        }
                        else
                        {
                            txtCuit.Text = (String)Session["RdoCuilCuit"];
                        }
                        DivSeleccionCuit.Visible = false;
                        DivIngresoCuit.Visible = true;
                    }

                    if (test == "1")
                    {
                        txtCuit.Enabled = true;
                    }
                    else
                    {
                        txtCuit.Enabled = false;
                    }
                    if (TipoTramite == "BAJA")
                    {
                        //Significa que vengo desde la pantalla Baja de Comercio y debo realizar un Reempadronamiento para Baja.
                        //INICIA EL TRAMITE AUTOMATICAMENTE
                        IniciarTramiteParaBajaComercio();
                        //CAMBIO TITULOS GENERALES
                        lblTituloTramite.Text = "REEMPADRONAMIENTO PARA BAJA DE COMERCIO - SIFCoS ";
                        lblTituloVentanaModalPrincipal.Text = "REEMPADRONAMIENTO PARA BAJA DE COMERCIO - SIFCoS ";
                    }
                }

            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["CiDiUrl"] + "?url=" + ConfigurationManager.AppSettings["Url_Retorno"] + "&app=" + ConfigurationManager.AppSettings["CiDiIdAplicacion"]);
            }
        }

        protected void CargarDirecciones(String Cuit)
        {
            var ConsultaDirecciones = Bl.BlGetDireccionesSedes(Cuit, 0);
            if (ConsultaDirecciones.Rows.Count > 0)
            {
                gvDirecciones.PagerSettings.Visible = false;
                gvDirecciones.DataSource = ConsultaDirecciones;
                gvDirecciones.DataBind();
                var cantBotones = gvDirecciones.PagerSettings.PageButtonCount;
                var listaNumeros = new ArrayList();

                for (int i = 0; i < cantBotones; i++)
                {
                    var datos = new
                    {
                        nroPagina = i + 1
                    };
                    listaNumeros.Add(datos);
                }
                rptBotonesPaginacionDirecciones.DataSource = listaNumeros;
                rptBotonesPaginacionDirecciones.DataBind();
                lblTotalRegistrosDir.Text = ConsultaDirecciones.Rows.Count.ToString();
                lblTitulocantRegistrosDir.Visible = true;
            }
            else
            {
                lblMensajeError.Text = "No se encontraron direcciones cargadas para ese CUIT.";
            }

        }



        //-aca empezamos!
        protected void btnIniciarTramite_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCuit.Text))
            {
                PCuit = txtCuit.Text;
            }
            else
            {
                PCuit = ddlSeleccionEntidad.SelectedItem.Text;

            }
            ContadorClicksEnBotonConfirmar = 0;

            if (ObjetoInscripcion == null)
                Response.Redirect("Inscripcion.aspx");
            if (!ValidarDatosRequeridos(NumeroPasoEnum.PasoIniciarTramite))
                return;


            txtNroSifcos.Text = "";
            Session["CuitPermiso"] = null;
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

            ObjetoInscripcion.CUIT = PCuit;

            ObjetoInscripcion.TipoTramiteNum = ObtenerTramiteSegunCuit();


            switch (ObjetoInscripcion.TipoTramiteNum)
            {
                case TipoTramiteEnum.Reempadronamiento:
                    ObjetoInscripcion.TipoTramite = 4;
                    lblTituloTramite.Text = "REEMPADRONAMIENTO - SIFCoS ";
                    lblTituloVentanaModalPrincipal.Text = "REEMPADRONAMIENTO - SIFCoS ";
                    divSeccionReempadronamiento.Visible = true;
                    divSeccionInscripcionTramite.Visible = false;
                    divBotonImpirmirTRS.Visible = false;
                    divCheckNuevaSucursal.Visible = true;
                    break;
                case TipoTramiteEnum.Instripcion_Sifcos:
                    ObjetoInscripcion.TipoTramite = 1;
                    lblTituloTramite.Text = "INSCRIPCIÓN A SIFCoS";
                    lblTituloVentanaModalPrincipal.Text = "INSCRIPCIÓN A SIFCoS";
                    divSeccionReempadronamiento.Visible = false;
                    //Si el cuit se trae de RENTAS no se muestra la opción de seleccionar sucursal ya que no va a tener sede.
                    divSeccionInscripcionTramite.Visible = !EsCuitDeRentas;
                    divBotonImpirmirTRS.Visible = false;
                    divCheckNuevaSucursal.Visible = false;
                    ImprimirTRSAlta();
                    break;

            }

            //DEBO SELECCIONAR LA SEDE 
            CargarDomiciliosExistentes();

            MostrarOcultarModalTramiteSifcos(true);

            //consulta si existe tramite para el cuit ingresado.
            //Bl.BlExisteEnSifcos(ObjetoInscripcion.CUIT, out pExiste);
            //Lo correcto sería pExiste = 1 --> NO EXISTE EN SIFCOS.
            //                  pExiste = 2 --> SI EXISTE EN SIFCOS.






            divVentanaPasosTramite.Visible = true;

        }

        // Una vez validado el tipo de empresa y su existencia validamos si es inscripcion, reempadronamiento o migracion del viejo sifcos
        protected void btnAceptarTramiteARealizar_OnClick(object sender, EventArgs e)
        {

            if (chkNuevaSucursal.Checked)
            {
                ObjetoInscripcion.TipoTramiteNum = TipoTramiteEnum.Instripcion_Sifcos;
                ObjetoInscripcion.TipoTramite = 1; //ALTA = 1 ,REEMPADRONAMIENTO = 4
                lblTituloTramite.Text = "INSCRIPCIÓN A SIFCoS ";
                lblTituloVentanaModalPrincipal.Text = "INSCRIPCIÓN A SIFCoS ";
                divSeccionInscripcionTramite.Visible = true;
                divSeccionReempadronamiento.Visible = false;
                DivAdjuntar.Visible = true;
                divBotonImpirmirTRS.Visible = false;
                ImprimirTRSAlta();
                MostrarOcultarModalTramiteSifcos(false);
                return;
            }

            ObjetoInscripcion.TipoTramiteNum = ObtenerTramiteSegunCuit();
            //--- REEMPADRONAMIENTO
            if (ObjetoInscripcion.TipoTramiteNum != TipoTramiteEnum.Instripcion_Sifcos)
            {
                if (string.IsNullOrEmpty(PCuit) || txtNroSifcos.Text.Trim() == "0")
                {
                    divMensaejeErrorModal.Visible = true;
                    lblMensajeErrorModal.Text = "Debe ingresar un Nro de Sifcos válido.";
                    return;
                }
                ObjetoInscripcion.NroSifcos = txtNroSifcos.Text.Trim();
            }



            ObjetoInscripcion.DomComercio = new ComercioDto();
            ObjetoInscripcion.Latitud = "0";
            ObjetoInscripcion.Longitud = "0";
            ObjetoInscripcion.NombreComercio = txtModalNombreFantasia.Text;
            txtNomFantasia.Text = txtModalNombreFantasia.Text;
            lblNombreComercio.Text = txtModalNombreFantasia.Text;
            //var Entidad = 0;
            //string RegistrarEntidad;

            #region REEMPADRONAMIENTO



            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento || ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento_ViejoSifcos)
            {
                DivAdjuntar.Visible = false;
                //Si es reempadronamiento y esta cargando una nueva sucursal el trámite pasa a ser INSCRIPCIÓN A SIFCoS.    

                //validar si existe el NroSifcos ingresado
                var bandExisteNroSifcosIngresado = false;
                var bandTieneTramitesEnSifcosNuevo = false;

                ObjetoInscripcion.IdEntidad = "0";

                foreach (var tramite in SessionTramitesDelCuit)
                {
                    if (tramite.Nro_Sifcos == txtNroSifcos.Text.Trim())
                    {
                        bandExisteNroSifcosIngresado = true;
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


                if (!bandExisteNroSifcosIngresado)
                {
                    //si no existe nro sifcos
                    divMensaejeErrorModal.Visible = true;
                    lblMensajeErrorModal.Text = "Debe ingresar un Nro de Sifcos válido.";
                    return;
                }
                if (string.IsNullOrEmpty(PCuit))
                {
                    divMensajeError.Visible = true;
                    lblMensajeError.Text = "Debe ingresar un CUIT, es un campo obligatorio.";
                    txtCuit.Focus();
                    return;
                }

                //NRO SIFCoS SI EXISTE, Y ESTÁ POR REALIZAR EL REEMPADRONAMIENTO.

                var existeBaja = Bl.BlGetTramitesDeBaja(txtNroSifcos.Text.Trim(), PCuit);
                if (existeBaja.Rows.Count > 0)
                {
                    divBotonImpirmirTRS.Visible = true;
                    divSeccionDebeTrs.Visible = false;
                    lblCantTrsPagas.Text = "USTED YA REALIZO UN TRAMITE DE BAJA Y NO PUEDE REEMPADRONARSE. EL NRO DE TRAMITE DE BAJA ES: " + existeBaja.Rows[0]["NRO_TRAMITE"].ToString();
                    return;
                }
                //asigno FECHA DE VENCIMIENTO al tramite.
                int anios_debe = 0;
                DateTime? fecVto;
                DateTime? fecAux = null; // es la fecha de presentación que se utiliza en el sifcos viejo.Representa que el PROX. VTO = FECHA_PRESENTACIÓN + 1 AÑO.
                DateTime fecUltVto;
                if (bandTieneTramitesEnSifcosNuevo)
                {
                    /*
                     *  1 -- ES UN TRAMITE DE REEMPADRONAMIENTO CON TRAMITES YA REALIZADOS EN EL SIFCOS NUEVO
                     *  Si la fecha de VENCIMIENTO del ultimo tramite es el 18/10/2014. Fecha de alta fue : 15/11/2014
                     *  
                     *  Debe 2 reempadronamiento
                     *    El 1ro vence el 18/10/2015 
                     *    El 2do vence el 18/10/2016 
                     *  
                     *  hoy es : 29/12/2016
                     *  Proximo vencimiento 18/10/2017
                     *  El objetoInscripcion debe tener en el  tramite como fecha de vencimiento el 18/10/2017.
                     */

                    fecVto = Bl.BlGetFechaUltimoTramiteSifcosNuevo(txtNroSifcos.Text.Trim());
                    fecAux = fecVto.Value.AddYears(-1);
                    TimeSpan ts = DateTime.Now - fecAux.Value;
                    anios_debe = ts.Days / 365;
                    // NO DEBE REEMPADRONARSE, ESTÁ AL DÍA.
                    if (anios_debe == 0)
                    {
                        lblFechaUltimoTramite.Text = "Fecha del próximo vencimiento : " + fecVto.Value.Day + "/" +
                                                     fecVto.Value.Month + "/" + fecVto.Value.Year;
                        lblCantTrsPagas.Text = "USTED SE ENCUENTRA AL DÍA. DEBE REALIZAR EL REEMPADRONAMIENTO POSTERIOR A LA FECHA DE VENCIMIENTO.";
                        divBotonImpirmirTRS.Visible = true;
                        divSeccionDebeTrs.Visible = false;
                        return;
                    }

                    ObjetoInscripcion.FechaVencimiento = fecVto.Value.AddYears(anios_debe);
                    fecUltVto = fecVto.Value;//ObjetoInscripcion.FechaVencimiento.AddYears(-1);

                    PreCargarCamposParaReempadronamiento();
                }
                else
                {
                    /*  2 -- ES UN TRAMITE DE REEMPADRONAMIENTO POR PRIMERA VEZ. ES DECIR VIENE LOS DATOS DEL SIFCOS VIEJO.
                     **************************************
                     *  Si la fecha de presentacion de un reempa. es 18/10/2013. (fecVto)
                     *   Debe 2 reempadronamientos ya que ese registrio indica que vencería el 18/10/2014 
                     *    El 1ro vence el 18/10/2015 
                     *    El 2do vence el 18/10/2016 
                     *   
                     *  hoy es : 29/12/2016
                     *  Proximo vencimiento 18/10/2017
                     **************************************
                     *  Si la fecha de presentacion de un reempa. es 20/12/2015. (fecVto)
                     *   Debe 1 reempadronamientos ya que ese registrio indica que vencería el 20/12/2016 
                     *    El 1ro vence el 18/10/2016 
                          *  hoy es : 25/01/2017
                     *    Proximo vencimiento 20/12/2017 --> campo de T_SIF_TRAMITE_SIFCOS del nvo sifcos.
                     *    objetoInscripcion debe tener en el  tramite como fecha de vencimiento el 18/10/2017.
                     *  */

                    fecVto = Bl.BlGetFechaUltimoTramiteSifcosViejo(txtNroSifcos.Text.Trim()).Value;// me va a traer la fecha de presentacion.
                    fecAux = fecVto.Value.AddYears(1);
                    TimeSpan ts = DateTime.Now - fecAux.Value;
                    anios_debe = ts.Days / 365;
                    ObjetoInscripcion.FechaVencimiento = ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Baja_de_Comercio ? fecAux.Value.AddYears(anios_debe) : fecAux.Value.AddYears(anios_debe + 1);
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


                divBotonImpirmirTRS.Visible = true;
                divSeccionInscripcionTramite.Visible = false;
                divCheckNuevaSucursal.Visible = false;


                //ObjetoInscripcion.FechaVencimiento --> contiene la fecha del proximo vencimiento. 

                lblFechaUltimoTramite.Text = "Fecha Vencimiento : " + ObjetoInscripcion.FechaVencimiento.Day + "/" + ObjetoInscripcion.FechaVencimiento.Month + "/" + (fecUltVto.Year);

                if (fecUltVto < DateTime.Now)
                {
                    //entonces está vencido y debe reempadronarse.

                    var cantTasasPagasSinUsar = Bl.BlGetCantTasasPagadas(PCuit);
                    if (cantTasasPagasSinUsar == 0)
                        lblCantTrsPagas.Text = "Usted NO dispone de tasas pagadas.";
                    else
                    {
                        lblCantTrsPagas.Text = "Usted disponde de " + cantTasasPagasSinUsar + " TRS.";
                    }

                    if (cantTasasPagasSinUsar > anios_debe)
                    {
                        lblCantTrsNoPagas.Text = "A la fecha adeuda " + anios_debe +
                                                 " Reempadronamiento/s. Debe imprimir " + anios_debe + " TRS. ";
                    }
                    else
                    {
                        CantidadTRS_A_Imprimir = anios_debe - cantTasasPagasSinUsar;
                        lblCantTrsNoPagas.Text = "A la fecha adeuda " + anios_debe +
                                                 " Reempadronamiento/s. Debe imprimir " + CantidadTRS_A_Imprimir + " TRS. ";
                    }



                    if (cantTasasPagasSinUsar < anios_debe)
                    {

                        divSeccionDebeTrs.Visible = true;

                    }
                    else
                    {
                        divSeccionDebeTrs.Visible = false;

                        //entonces la tasa SI está paga, oculto la modal y sigo con el rempadronamiento.

                        //Asigno las TRS pagas a la listaTRs para luego guardarlas al final del reempadronamiento.

                        var trsPagasSinUsar = Bl.BlGetTasasPagadasSinUsar(PCuit);
                        var contadorTrsAUtilizar = 0;
                        var listaAux = new List<Trs>();
                        foreach (var trs in trsPagasSinUsar)
                        {
                            if (contadorTrsAUtilizar < anios_debe)
                            {
                                listaAux.Add(trs);//guardo en memoria las trs a utilizar cdo finalice el tramite de reempadronamiento.
                                contadorTrsAUtilizar++;
                            }
                        }

                        ListaTrs = listaAux;
                        NumeroDePasoActual = NumeroPasoEnum.TercerPaso;
                        CorregirPasoActual();
                        HabilitarBotonesFooter(NumeroPasoEnum.TercerPaso);
                        NumeroDePasoActual = NumeroPasoEnum.TercerPaso;
                        PintarTabBotonSiguiente(li_tab_1, li_tab_2);
                        PintarTabBotonSiguiente(li_tab_2, li_tab_3);
                        iniciarModuloDirecciones();
                        btnAtras.Visible = false;
                        MostrarOcultarModalTramiteSifcos(false);
                    }
                }
                else
                {
                    if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Baja_de_Comercio)
                    {
                        if (anios_debe == 1)
                        {
                            lblFechaUltimoTramite.Text = "Fecha del próximo vencimiento : " + fecVto.Value.Day + "/" +
                                                         fecVto.Value.Month + "/" + fecVto.Value.Year;
                            lblCantTrsPagas.Text = "USTED SE ENCUENTRA AL DÍA. DEBE REALIZAR EL REEMPADRONAMIENTO POSTERIOR A LA FECHA DE VENCIMIENTO.";
                            divBotonImpirmirTRS.Visible = true;
                            divSeccionDebeTrs.Visible = false;
                            return;
                        }
                    }
                    // NO DEBE REEMPADRONARSE, ESTÁ AL DÍA.
                    lblCantTrsPagas.Text = "USTED SE ENCUENTRA AL DÍA. DEBE REALIZAR EL REEMPADRONAMIENTO POSTERIOR A LA FECHA DE VENCIMIENTO.";
                    divSeccionDebeTrs.Visible = false;
                }

            }

            #endregion

            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
            {

                MostrarOcultarModalTramiteSifcos(false);
            }
        }

        //private void CargarBoca()
        //{
        //	ddlMyBoca.Items.Clear();
        //	DataTable dt = Bl.BlGetOrganismos();
        //	if (dt.Rows.Count != 0)
        //	{
        //		foreach (DataRow dr in dt.Rows)
        //		{
        //			ddlMyBoca.Items.Add(new ListItem(dr["n_organismo"].ToString(), dr["id_organismo"].ToString()));

        //		}
        //	}
        //          ddlMyBoca.Items.Add(new ListItem("SELECCIONE BOCA DE RECEPCION", "0"));
        //          ddlMyBoca.SelectedValue = "0";

        //}

        private void CargarDatosUsuarioLogueado()
        {
            //txtNombreCidi.Text = master.UsuarioCidiLogueado.Nombre;
            //txtApellidoCidi.Text = master.UsuarioCidiLogueado.Apellido;
            lblApeyNomUsuarioLogueado.Text = master.UsuarioCidiLogueado.Apellido + ", " + master.UsuarioCidiLogueado.Nombre;
            if ((string)Session["TieneRepresentados"] == "S")
            {
                var aa = (string)Session["RdoCuilCuit"];
                var bb = int.Parse(aa.Substring(0, 2));
                if (bb >= 29)
                    lblTitularTramite.Text = "CUIT de la persona jurídica a realizar el trámite:";
                else
                    lblTitularTramite.Text = "CUIL de La persona física a realizar el trámite:";
            }
            //else
            //{

            //    if (Session["CuitPermiso"] != null)
            //    {
            //        lblTitularTramite.Text = "CUIT de la empresa a realizar el trámite:";
            //        txtCuit.Text = (string)Session["CuitPermiso"];
            //    }
            //    else
            //    {
            //        lblTitularTramite.Text = "CUIL de La persona física a realizar el trámite:";
            //        txtCuit.Text = master.UsuarioCidiLogueado.CUIL;
            //    }

            //}

            txtCuilCidi.Text = master.UsuarioCidiLogueado.CuilFormateado;


            //datos del gestor que se muestran en el paso 4

            txtNomApeConta.Text = master.UsuarioCidiLogueado.Nombre + " " + master.UsuarioCidiLogueado.Apellido;
            txtTelConta.Text = master.UsuarioCidiLogueado.CelFormateado;
            ObjetoInscripcion.TelGestor = master.UsuarioCidiLogueado.CelFormateado;
            txtDniConta.Text = master.UsuarioCidiLogueado.NroDocumento;
            txtEmailConta.Text = master.UsuarioCidiLogueado.Email;
            txtSexoConta.Text = master.UsuarioCidiLogueado.Id_Sexo == "01" ? "MASCULINO" : "FEMENINO";

            ObjetoInscripcion.CuilUsuarioCidi = master.UsuarioCidiLogueado.CUIL;
        }

        #region Propiedades

        public int CantidadTRS_A_Imprimir
        {
            get
            {
                return (int)Session["CantidadTRS_A_Imprimir"];

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

        public string PCuit
        {
            get
            {
                return Session["PCuit"] == null ? "" : (string)Session["PCuit"];

            }
            set
            {
                Session["PCuit"] = value;
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
                return Session["ObjetoInscripcion"] == null ? ObjetoInscripcion = new InscripcionSifcos() : (InscripcionSifcos)Session["ObjetoInscripcion"];
            }
            set
            {
                Session["ObjetoInscripcion"] = value;
            }
        }

        public int? IdVinDomicilio1
        {
            get
            {
                return Session["IdVinDomicilio1"] == null ? new int?() : (int)Session["IdVinDomicilio1"];
            }
            set
            {
                Session["IdVinDomicilio1"] = value;
            }
        }
        public int? IdVinDomicilio2
        {
            get
            {
                return Session["IdVinDomicilio2"] == null ? new int?() : (int)Session["IdVinDomicilio2"];
            }
            set
            {
                Session["IdVinDomicilio2"] = value;
            }
        }
        public DataTable DtProductos
        {
            get
            {

                if (Session["PRODUCTOS"] == null)
                {
                    var dt = new DataTable();
                    dt.Columns.Add(new DataColumn("IdProducto"));
                    dt.Columns.Add(new DataColumn("NProducto"));
                    Session["PRODUCTOS"] = dt;
                    return dt;
                }
                return (DataTable)Session["PRODUCTOS"];
            }
            set
            {
                Session["PRODUCTOS"] = value;
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
        public DataTable DtActividades
        {
            get
            {
                return Session["RUBROS"] == null ? new DataTable() : (DataTable)Session["RUBROS"];
            }
            set
            {
                Session["RUBROS"] = value;
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
        private int conteoClicks = 0;
        /// <summary>
        /// Nro de trámite que se acaba de generar.
        /// </summary>
        public long NroTramiteAImprimir
        {
            get
            {
                return Session["NroTramiteAImprimir"] == null ? 0 : (long)Session["NroTramiteAImprimir"];
            }
            set
            {
                Session["NroTramiteAImprimir"] = value;
            }
        }
        public int ContadorClicksEnBotonConfirmar
        {
            get
            {
                return Session["ContadorClicksEnBotonConfirmar"] == null
                    ? 0
                    : (int)Session["ContadorClicksEnBotonConfirmar"];
            }
            set
            {
                Session["ContadorClicksEnBotonConfirmar"] = value;
            }
        }
        /// <summary>
        /// Contiene una lista de trámites del cuit (empresa) que ingresó. En la misma aparecen todos las sucursales de esa empresa, cada una con su Nro Sifcos.
        /// </summary>
        public List<consultaTramite> SessionTramitesDelCuit
        {
            get
            {
                return (List<consultaTramite>)Session["SessionTramitesDelCuit"];
            }
            set
            {
                Session["SessionTramitesDelCuit"] = value;
            }
        }
        public NumeroPasoEnum NumeroDePasoActual
        {
            get
            {
                return Session["NumeroPaso"] == null ? NumeroPasoEnum.PrimerPaso : (NumeroPasoEnum)Session["NumeroPaso"];
            }
            set
            {
                Session["NumeroPaso"] = value;
            }
        }
        public string NroTransaccionTasa_Inscripcion
        {
            get
            {
                return (string)Session["NroTransaccionTasa_Inscripcion"];
            }
            set
            {
                Session["NroTransaccionTasa_Inscripcion"] = value;
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

        #endregion

        #region estetico

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
        private void QuitarCssClass(string nombreClass, WebControl control)
        {
            List<string> classes = new List<string>();
            if (!string.IsNullOrWhiteSpace(control.CssClass))
            {
                classes = control.CssClass.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            classes.Remove(nombreClass);
            control.CssClass = string.Join(" ", classes.ToArray());
        }
        private void LimpiarControlesRequeridos()
        {
            var controles = new List<Control>();


            //Listado de controles que nos Requeridos.
            controles.Add(txtCuit);


            //contacto
            controles.Add(txtTelFijoCodArea);
            controles.Add(txtTelFijo);
            controles.Add(txtCelularCodArea);
            controles.Add(txtCelular);
            controles.Add(txtEmail_Establecimiento);


            //información adicional

            controles.Add(txtM2Venta);
            controles.Add(txtM2Dep);
            controles.Add(txtM2Admin);
            controles.Add(txtCantPersRelDep);
            controles.Add(txtCantPersTotal);
            controles.Add(txtNroHabMun);

            controles.Add(lblInmueble);
            //controles.Add(rb1015);
            //controles.Add(rb1520);
            //controles.Add(rb5);
            //controles.Add(rb510);
            //controles.Add(rbInquilino);
            //controles.Add(rbNoCap);
            //controles.Add(rbNoCobertura);
            //controles.Add(rbNoSeg);
            //controles.Add(rbPropietario);
            //controles.Add(rbSiCap);
            //controles.Add(rbSiCobertura);
            //controles.Add(rbSiSeg);

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
        public void HabilitarBotonesFooter(NumeroPasoEnum numeroPasoEnum)
        {
            switch (numeroPasoEnum)
            {
                case NumeroPasoEnum.PrimerPaso:
                    ProgressBar.Style.Value = "width: 20%";
                    lblPasos.Text = "Paso 1 de 5";
                    divPanel_1.Visible = true;
                    divPanel_2.Visible = false;
                    divPanel_3.Visible = false;
                    divPanel_4.Visible = false;
                    divPanel_5.Visible = false;
                    btnAtras.Visible = false;
                    btnSiguiente.Visible = true;
                    btnFinalizar.Visible = false;
                    break;
                case NumeroPasoEnum.SegundoPaso:
                    ProgressBar.Style.Value = "width: 40%";
                    lblPasos.Text = "Paso 2 de 5";
                    divPanel_1.Visible = false;
                    divPanel_2.Visible = true;
                    divPanel_3.Visible = false;
                    divPanel_4.Visible = false;
                    divPanel_5.Visible = false;
                    btnAtras.Visible = true;
                    btnSiguiente.Visible = true;
                    btnFinalizar.Visible = false;
                    break;
                case NumeroPasoEnum.TercerPaso:
                    ProgressBar.Style.Value = "width: 60%";
                    lblPasos.Text = "Paso 3 de 5";
                    divPanel_1.Visible = false;
                    divPanel_2.Visible = false;
                    divPanel_3.Visible = true;
                    divPanel_4.Visible = false;
                    divPanel_5.Visible = false;
                    btnAtras.Visible = true;
                    btnSiguiente.Visible = true;
                    btnFinalizar.Visible = false;
                    break;
                case NumeroPasoEnum.CuartoPaso:
                    ProgressBar.Style.Value = "width: 80%";
                    lblPasos.Text = "Paso 4 de 5";
                    divPanel_1.Visible = false;
                    divPanel_2.Visible = false;
                    divPanel_3.Visible = false;
                    divPanel_4.Visible = true;
                    divPanel_5.Visible = false;
                    btnAtras.Visible = true;
                    btnSiguiente.Visible = true;
                    btnFinalizar.Visible = false;
                    break;
                case NumeroPasoEnum.QuintoPaso:
                    ProgressBar.Style.Value = "width: 100%";
                    lblPasos.Text = "Paso 5 de 5";
                    divPanel_1.Visible = false;
                    divPanel_2.Visible = false;
                    divPanel_3.Visible = false;
                    divPanel_4.Visible = false;
                    divPanel_5.Visible = true;
                    btnAtras.Visible = true;
                    btnSiguiente.Visible = false;
                    btnFinalizar.Visible = true;
                    break;
            }
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

        private void MostrarOcultarModalVerSucursales(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarModalTramite";
                string[] listaStrings = modalSucursales.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalSucursales.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalSucursales.Attributes.Add("class", String.Join(" ", modalSucursales
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarModalTramite" })
                          .ToArray()
                  ));
            }
        }




        #endregion

        #region navegacion

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
                        HabilitarBotonesFooter(NumeroPasoEnum.SegundoPaso);
                        NumeroDePasoActual = NumeroPasoEnum.SegundoPaso;
                        PintarTabBotonSiguiente(li_tab_1, li_tab_2);
                        break;
                    case NumeroPasoEnum.SegundoPaso:
                        if (!ValidarDatosRequeridos(NumeroPasoEnum.SegundoPaso))
                            break;
                        CargarDatosObjInscripcion(NumeroPasoEnum.SegundoPaso);
                        HabilitarBotonesFooter(NumeroPasoEnum.TercerPaso);
                        NumeroDePasoActual = NumeroPasoEnum.TercerPaso;
                        PintarTabBotonSiguiente(li_tab_2, li_tab_3);
                        iniciarModuloDirecciones();
                        break;
                    case NumeroPasoEnum.TercerPaso:
                        if (!ValidarDatosRequeridos(NumeroPasoEnum.TercerPaso))
                            break;
                        CargarDatosObjInscripcion(NumeroPasoEnum.TercerPaso);
                        HabilitarBotonesFooter(NumeroPasoEnum.CuartoPaso);
                        NumeroDePasoActual = NumeroPasoEnum.CuartoPaso;
                        PintarTabBotonSiguiente(li_tab_3, li_tab_4);
                        break;
                    case NumeroPasoEnum.CuartoPaso:
                        if (!ValidarDatosRequeridos(NumeroPasoEnum.CuartoPaso))
                            break;
                        CargarDatosObjInscripcion(NumeroPasoEnum.CuartoPaso);
                        HabilitarBotonesFooter(NumeroPasoEnum.QuintoPaso);
                        NumeroDePasoActual = NumeroPasoEnum.QuintoPaso;
                        PintarTabBotonSiguiente(li_tab_4, li_tab_5);
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
                lblMensajeErrorModal.Text = "ERROR BOTON SIGUIENTE:   txtFechaIniAct.Text :" + txtFechaIniAct.Text;
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
                    //CargarProvinciaLegal();
                    PintarTabBotonAnterior(li_tab_2, li_tab_1);

                    //limpiar las propiedades en sessión y controles.
                    idSedeSeleccionada = null;
                    chkNuevaSucursal.Checked = false;
                    break;
                case NumeroPasoEnum.TercerPaso:
                    HabilitarBotonesFooter(NumeroPasoEnum.SegundoPaso);
                    NumeroDePasoActual = NumeroPasoEnum.SegundoPaso;
                    PintarTabBotonAnterior(li_tab_3, li_tab_2);
                    

                    break;
                case NumeroPasoEnum.CuartoPaso:
                    HabilitarBotonesFooter(NumeroPasoEnum.TercerPaso);
                    NumeroDePasoActual = NumeroPasoEnum.TercerPaso;
                    PintarTabBotonAnterior(li_tab_4, li_tab_3);
                    if (ObjetoInscripcion.TipoTramite == 4)
                    {
                        btnAtras.Visible = false;
                    }
                    break;
                case NumeroPasoEnum.QuintoPaso:
                    HabilitarBotonesFooter(NumeroPasoEnum.CuartoPaso);
                    NumeroDePasoActual = NumeroPasoEnum.CuartoPaso;
                    PintarTabBotonAnterior(li_tab_5, li_tab_4);
                    break;
            }
        }
        private void CorregirPasoActual()
        {
            if (divPanel_1.Visible)
                NumeroDePasoActual = NumeroPasoEnum.PrimerPaso;
            if (divPanel_2.Visible)
                NumeroDePasoActual = NumeroPasoEnum.SegundoPaso;
            if (divPanel_3.Visible)
                NumeroDePasoActual = NumeroPasoEnum.TercerPaso;
            if (divPanel_4.Visible)
                NumeroDePasoActual = NumeroPasoEnum.CuartoPaso;
            if (divPanel_5.Visible)
                NumeroDePasoActual = NumeroPasoEnum.QuintoPaso;

        }
        protected void btnVolverTramiteARealizar_OnClick(object sender, EventArgs e)
        {
            if (NumeroDePasoActual == NumeroPasoEnum.PrimerPaso)
            {
                divEncabezadoDatosEmpresa.Visible = false;
                HabilitarBotonesFooter(NumeroPasoEnum.PrimerPaso);
                NumeroDePasoActual = NumeroPasoEnum.PrimerPaso;
                PintarTabBotonAnterior(li_tab_2, li_tab_1);
                MostrarOcultarModalTramiteSifcos(false);
                chkNuevaSucursal.Checked = false;
                divBotonImpirmirTRS.Visible = false;
                divVentanaPasosTramite.Visible = false;
                divVentanaInicioTramite.Visible = true;
                Response.Redirect("Solicitud.aspx");
            }
        }
        protected void btnVolverFinalizacionTramite_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalFinalizacionTramite(false);
        }
        protected void btnFinalizar_OnClick(object sender, EventArgs e)
        {
            if (!ValidarDatosRequeridos(NumeroPasoEnum.QuintoPaso))
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
        protected void btnCancelarYSeguirTramite_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalVentanaSalirDelTramite(false);
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
            LimpiarSessionesEnMemoria();
            MostrarOcultarModalVentanaSalirDelTramite(false);
        }
        protected void btnIrAMisTramites_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MisTramites.aspx");
        }
        protected void btnRealizarOtroTramite_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Inscripcion.aspx");
        }
        protected void btnSalir2_OnClick(object sender, EventArgs e)
        {
            LimpiarSessionesEnMemoria();
            LimpiarControlesRequeridos();
            Session["VENGO_DESDE_BAJA"] = null;
            Session["VENGO_DESDE_BAJA_CUIT"] = null;
            Session["VENGO_DESDE_BAJA_NRO_SIFCOS"] = null;
            Session["VENGO_DESDE_BAJA_FECHA_CESE"] = null;

            Response.Redirect("Inscripcion.aspx");
        }

        #endregion

        #region validacion_empresa

        private bool buscarEmpresaEnRENTAS()
        {
            DtEmpresa = Bl.BlGetEmpresaEnRentas(PCuit);
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

            txtNroDGR.Text = DtEmpresa.Rows[0]["NUMERO_INSCRIPCION"].ToString();
            txtNroHabMun.Text = "";// no tiene porque viene de rentas.

            //txtNroDGR.Enabled = false;


            EsCuitDeRentas = true;

            return true;

        }
        private bool buscarEmpresaEnBDComunes()
        {

            //valida que tenga nro de DGR y  que exista en BD T_Comunes
            DtEmpresa = Bl.BlGetEmpresa(PCuit);
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

            txtNroDGR.Text = DtEmpresa.Rows[0]["NRO_INGBRUTO"].ToString();
            txtNroHabMun.Text = DtEmpresa.Rows[0]["NRO_HAB_MUNICIPAL"].ToString();
            //txtNroDGR.Enabled = false;
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



        #endregion

        #region representante

        protected void btnBuscarRepresentante_Click(object sender, EventArgs e)
        {
            ObtenerUsuarioRepresentante();
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

        #endregion

        #region tasa
        protected void btnImprimirTRS_Click(object sender, EventArgs e)
        {
            ImprimirTRS();

        }
        private void ImprimirTRS()
        {
            try
            {
                // CONCEPTO
                //ID : 92010000  | NOMBRE : Art. 76 - Inc. 1 - Tasa retributiva por derecho de inscripcion en SIFCoS (Inscripcion)
                //ID : 92020000  | NOMBRE : Art. 76 - Inc. 2 - Por renovacion anual del derecho de inscripcion en el SIFCoS  (Reempadronamiento)

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

                /*
				var idConcepto = ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos
					? SingletonParametroGeneral.GetInstance().IdConceptoTasaAlta
					: SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;

                 

				//imprimo la cantidad de Tasas que corresponden según la grilla.
				string resultado=""; 
				var lstAux = new List<Trs>();

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
                int IdTipoTramiteTrs = 1;
                if (ObjetoInscripcion.TipoTramite == 4 || ObjetoInscripcion.TipoTramite == 2)
                    IdTipoTramiteTrs = 4;
                string urlTrs = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;

                var dtExisteIPJ = Bl.BlGetEmpresa(ObjetoInscripcion.CUIT);
                if (dtExisteIPJ.Rows.Count == 0)
                {
                    /*Si el CUIT no pertenece a la tabla T_PERS_JURIDICA, el generar Transacción de la TRS daría error. Por eso se da de alta en la tabla al cuit.*/
                    var res = Bl.RegistrarEntidadPerJur(ObjetoInscripcion.CUIT, txtModalRazonSocial.Text, "");

                }
                /*
				 resultado = Bl.GenerarTransaccionTRS(ObjetoInscripcion.CUIT, master.UsuarioCidiLogueado.Id_Sexo,
					master.UsuarioCidiLogueado.NroDocumento, "ARG",
					master.UsuarioCidiLogueado.Id_Numero, Int64.Parse(conceptoTasa.id_concepto), conceptoTasa.fec_desde,
					"057", 1, importeConcepto, "", DateTime.Now.Year.ToString(), out nroTransaccion);
                        
				//Agrego una trs a la lista auxiliar.
				lstAux.Add(new Trs{NroTransaccion = nroTransaccion});
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
        #endregion

        #region producto
        protected void btnAgregarProd_Click(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(ace1Value.Value) || ace1Value.Value == "0")
            //	return;
            if (string.IsNullOrEmpty(ddlProductos.SelectedItem.Text))
                return;


            if (!ValidarExistenciaProductoEnGrilla())
                return;// sino pasó la validación se interrumpe el agregado del producto

            var dr = DtProductos.NewRow();
            dr["IdProducto"] = ddlProductos.SelectedItem.Value;//ace1Value.Value;
            dr["NProducto"] = ddlProductos.SelectedItem.Text;//txtBuscarProducto.Text;

            DtProductos.Rows.Add(dr);
            RefrescarGrillaProductos();
            //txtBuscarProducto.Text = string.Empty;
            //txtBuscarProducto.Focus();
        }
        private bool ValidarExistenciaProductoEnGrilla()
        {

            //el hidden de id_producto  tiene ya seleccionado el producto a agregar.
            foreach (DataRow row in DtProductos.Rows)
            {
                if (row["IdProducto"].ToString() == ddlProductos.SelectedItem.Value)//ace1Value.Value
                {
                    lblMensajeError.Text = "El Producto que desea cargar a la lista, ya existe en la misma.";
                    divMensajeError.Visible = true;
                    return false;
                }
            }
            return true;
        }
        public void RefrescarGrillaProductos()
        {
            gvProducto.DataSource = DtProductos;
            gvProducto.DataBind();

            if (chkConfirmarListaDeProducto.Checked)
                CargarRubrosSegunProductos();
        }
        private void CargarRubrosSegunProductos()
        {
            var lst = new List<int>();
            foreach (DataRow dr in DtProductos.Rows)
            {
                lst.Add(int.Parse(dr["IdProducto"].ToString()));
            }


            DtActividades = Bl.BlGetActividadesProducto(lst);
            DataRow _ravi = DtActividades.NewRow();
            _ravi["ID_ACTIVIDAD"] = "0";
            _ravi["N_ACTIVIDAD"] = "SIN ASIGNAR";
            DtActividades.Rows.Add(_ravi);

            ddlRubroPrimario.DataSource = DtActividades;
            ddlRubroPrimario.DataValueField = "ID_ACTIVIDAD";
            ddlRubroPrimario.DataTextField = "N_ACTIVIDAD";
            ddlRubroPrimario.DataBind();
            ddlRubroPrimario.SelectedValue = "0";

            ddlRubroSecundario.DataSource = DtActividades;
            ddlRubroSecundario.DataValueField = "ID_ACTIVIDAD";
            ddlRubroSecundario.DataTextField = "N_ACTIVIDAD";
            ddlRubroSecundario.DataBind();
            ddlRubroSecundario.SelectedValue = "0";
        }
        protected void gvProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvDirecciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void rptBotonesPaginacionDirecciones_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
            gvDirecciones.PageIndex = nroPagina - 1;
            CargarDirecciones(PCuit);
        }

        protected void btnNroPaginaDirecciones_OnClick(object sender, EventArgs e)
        {
            banderaPrimeraCargaPaginaDir = true;

            var btn = (LinkButton)sender;
            //guardo el comando del boton de pagina seleccinoado
            commandoBotonPaginaSeleccionadoDir = btn.CommandArgument;
        }

        protected void rptBotonesPaginacionDirecciones_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                var btn = (LinkButton)e.Item.FindControl("btnNroPaginaDirecciones");
                if (btn.CommandArgument == "1" && banderaPrimeraCargaPaginaDir == false)
                {
                    btn.BackColor = Color.Gainsboro; //pinto el boton.
                }
                if (btn.CommandArgument == commandoBotonPaginaSeleccionadoDir)
                {
                    //por cada boton pregunto y encuentro el comando seleccionado q corresponde al boton selecionado.
                    btn.BackColor = Color.Gainsboro; //pinto el boton.
                }
                //los demas botones se cargan con el color de fondo blanco por defecto.
            }
        }


        protected void chkConfirmarListaDeProducto_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkConfirmarListaDeProducto.Checked)
            {
                if (DtProductos.Rows.Count == 0)
                {
                    lblMensajeError.Text =
                        "Debe cargar al menos un producto para poder confirmar la lista de productos.";
                    divMensajeError.Visible = true;
                    return;
                }
                ddlRubroPrimario.Enabled = true;
                ddlRubroSecundario.Enabled = true;
                CargarRubrosSegunProductos();
            }
            else
            {
                ddlRubroPrimario.Enabled = false;
                ddlRubroPrimario.Items.Clear();
                ddlRubroSecundario.Enabled = false;
                ddlRubroSecundario.Items.Clear();
                DtActividades = null;

            }
        }
        protected void gvProducto_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "QuitarProducto" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvProducto.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            var idProductoSeleccionado = "0";

            if (gvProducto.SelectedValue != null)
                idProductoSeleccionado = gvProducto.SelectedValue.ToString();

            switch (e.CommandName)
            {

                case "QuitarProducto":

                    foreach (DataRow row in DtProductos.Rows)
                    {
                        if (row["IdProducto"].ToString() == idProductoSeleccionado)
                        {
                            DtProductos.Rows.Remove(row);
                            break;
                        }
                    }
                    RefrescarGrillaProductos();
                    break;
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

        private void CargarProductos()
        {
            ddlProductos.DataSource = Bl.BlGetProductos("");
            ddlProductos.DataValueField = "IdProducto";
            ddlProductos.DataTextField = "NProducto";
            ddlProductos.DataBind();
        }


        #endregion

        private void CargarDomiciliosExistentes()
        {

            //lista de Sedes que ya están cargadas en SIFCoS.
            var lsSedesSifcos = new List<Sede>();

            //listas de todas las Sedes de una empresa. Están las que están cargadas en SIFCoS y las que NO.
            var lsSedesTodas = new List<Sede>();

            //Traigo todas las Sedes para un CUIT inscriptas en RENTAS
            var dbSedes = Bl.BlGetSedes(PCuit);


            DtDomicilios1 = dbSedes;
            DtDomicilios2 = dbSedes;


            //En el combo principal sólo muestro las sedes ya cargadas en sifcos para ese CUIT.
            foreach (DataRow row in dbSedes.Rows)
            {
                var sede = new Sede { IdSede = row["ID_SEDE"].ToString(), NombreSede = row["SEDES"].ToString(), IdVin_Sede = row["id_vin"].ToString() };

                sede.PerteneceSifcos =
                    SessionTramitesDelCuit.Any(x => x.CUIT == PCuit && x.id_sede_entidad == sede.IdSede);
                if (sede.PerteneceSifcos)
                    if (!lsSedesSifcos.Contains(sede))
                        lsSedesSifcos.Add(sede);

                if (!lsSedesTodas.Contains(sede))
                    lsSedesTodas.Add(sede);

            }

        }
        private void CargarDatosObjInscripcion(NumeroPasoEnum paso)
        {
            switch (paso)
            {
                case NumeroPasoEnum.TercerPaso:
                    //ObjetoInscripcion.Latitud = txtLatitud.Text;
                    //ObjetoInscripcion.Longitud = txtLongitud.Text;
                    if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
                    {
                        ObjetoInscripcion.FechaVencimiento = DateTime.Now.AddYears(1);//en el ALTA la fecha de vencimiento es el próximo año.
                        ObjetoInscripcion.CantidadReempadranamientos = 0;
                    }


                    break;
                case NumeroPasoEnum.CuartoPaso:
                    ObjetoInscripcion.CapacUltAnio = rbSiCap.Checked ? "S" : "N";
                    ObjetoInscripcion.Cobertura = rbSiCobertura.Checked ? "S" : "N";
                    ObjetoInscripcion.Seguro = rbSiSeg.Checked ? "S" : "N";
                    ObjetoInscripcion.CantPersRelDep = txtCantPersRelDep.Text;
                    ObjetoInscripcion.CantTotalpers = txtCantPersTotal.Text;

                    long supVenta = 0;
                    long.TryParse(txtM2Venta.Text, out supVenta);
                    ObjetoInscripcion.SupVentas = supVenta;

                    long supDeposito = 0;
                    long.TryParse(txtM2Dep.Text, out supDeposito);
                    ObjetoInscripcion.SupDeposito = supDeposito;

                    long supAdm = 0;
                    long.TryParse(txtM2Admin.Text, out supAdm);
                    ObjetoInscripcion.SupAdministracion = supAdm;

                    ObjetoInscripcion.Propietario = rbPropietario.Checked ? "S" : "N";
                    if (ObjetoInscripcion.Propietario == "S")
                    {
                        ObjetoInscripcion.RangoAlquiler = " - ";
                    }
                    else
                    {
                        ObjetoInscripcion.RangoAlquiler = rb5.Checked ? "MENOS DE $ 500.000" : "";
                        if (string.IsNullOrEmpty(ObjetoInscripcion.RangoAlquiler))
                            ObjetoInscripcion.RangoAlquiler = rb510.Checked ? "$ 500.000 a $ 1.000.000" : "";
                        if (string.IsNullOrEmpty(ObjetoInscripcion.RangoAlquiler))
                            ObjetoInscripcion.RangoAlquiler = rb1015.Checked ? "$ 1.000.000 a $ 3.000.000" : "";
                        if (string.IsNullOrEmpty(ObjetoInscripcion.RangoAlquiler))
                            ObjetoInscripcion.RangoAlquiler = rb1520.Checked ? "MAS DE $ 3.000.000" : "";

                    }

                    //LA FECHA DE INICIO DEL TRAMITE NO ES IGUAL A LA FECHA DE INICIO DE ACTIVIDAD


                    string[] formats = { "dd/MM/yyyy" };
                    var dateTime = DateTime.ParseExact(txtFechaIniAct.Text, formats, new CultureInfo("en-US"), DateTimeStyles.None);

                    ObjetoInscripcion.FecIniTramite = dateTime;


                    //string.IsNullOrEmpty(txtFechaIniAct.Text) ? DateTime.Now :  DateTime.Parse(txtFechaIniAct.Text);
                    ObjetoInscripcion.NroHabilitacion = txtNroHabMun.Text;

                    ObjetoInscripcion.idActividadPri = ddlRubroPrimario.SelectedValue;
                    ObjetoInscripcion.idActividadSec = ddlRubroSecundario.SelectedValue;

                    var listProd = new List<Producto>();
                    foreach (DataRow row in DtProductos.Rows)
                    {
                        listProd.Add(new Producto { IdProducto = row["IdProducto"].ToString(), NProducto = row["NProducto"].ToString() });
                    }
                    ObjetoInscripcion.Productos = listProd;

                    CargarOrigenProveedor();
                    break;
                case NumeroPasoEnum.QuintoPaso:
                    if (ObjetoInscripcion == null)
                    {
                        Response.Redirect("MisTramites.aspx?Exito=1");
                    }
                    ObjetoInscripcion.CuilGestor = master.UsuarioCidiLogueado.CUIL;
                    ObjetoInscripcion.NroMatricula = txtNroMatricula.Text;
                    ObjetoInscripcion.EmailGestor = txtEmailConta.Text;
                    ObjetoInscripcion.TelGestor = txtTelConta.Text;
                    if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
                        ObjetoInscripcion.IdEstado = 1;//TRÁMITE estado: CARGADO
                    else
                    {
                        ObjetoInscripcion.IdEstado = 6;//TRÁMITE estado: AUTORIZADO POR MINISTERIO
                    }
                    ObjetoInscripcion.IdCargo = string.IsNullOrEmpty(ddlCargoOcupa.SelectedValue) ? 1 : long.Parse(ddlCargoOcupa.SelectedValue);
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

                    if (!string.IsNullOrEmpty(IdDocumentoCDD1.ToString()))
                    {
                        ObjetoInscripcion.Id_Documento1_CDD = IdDocumentoCDD1;

                    }
                    if (!string.IsNullOrEmpty(IdDocumentoCDD2.ToString()))
                    {
                        ObjetoInscripcion.Id_Documento2_CDD = IdDocumentoCDD2;

                    }

                    break;
            }

        }
        private void CargarOrigenProveedor()
        {
            int idOrigenProveedor = 0;
            if (chkprov.Checked && !ChkNacional.Checked && !ChkInter.Checked)
                idOrigenProveedor = 1;
            if (!chkprov.Checked && ChkNacional.Checked && !ChkInter.Checked)
                idOrigenProveedor = 2;
            if (!chkprov.Checked && !ChkNacional.Checked && ChkInter.Checked)
                idOrigenProveedor = 3;
            if (chkprov.Checked && ChkNacional.Checked && !ChkInter.Checked)
                idOrigenProveedor = 12;
            if (chkprov.Checked && ChkNacional.Checked && ChkInter.Checked)
                idOrigenProveedor = 123;
            if (!chkprov.Checked && ChkNacional.Checked && ChkInter.Checked)
                idOrigenProveedor = 23;
            if (chkprov.Checked && !ChkNacional.Checked && ChkInter.Checked)
                idOrigenProveedor = 13;
            if (idOrigenProveedor != 0)
                ObjetoInscripcion.IdOrigenProveedor = idOrigenProveedor;
        }
        private bool ValidarDatosRequeridos(NumeroPasoEnum paso)
        {
            var validado = false;
            LimpiarControlesRequeridos();
            switch (paso)
            {
                case NumeroPasoEnum.PasoIniciarTramite:

                    if (string.IsNullOrEmpty(txtCuit.Text) && Autorizados.Count == 0)
                    {
                        //mensaje de error.
                        lblMensajeErrorVentanaEncabezado.Text = "Debe ingresar el número de cuit sin guiones para buscar la empresa.";
                        AgregarCssClass("campoRequerido", txtCuit);
                        divMensajeErrorVentanaEncabezado.Visible = true;
                        return false;
                    }
                    if (Autorizados.Count > 0 && ddlSeleccionEntidad.SelectedValue == "0")
                    {
                        //mensaje de error.
                        lblMensajeErrorVentanaEncabezado.Text = " Debe seleccionar un CUIT de la lista de autorizados.";
                        AgregarCssClass("campoRequerido", ddlSeleccionEntidad);
                        divMensajeErrorVentanaEncabezado.Visible = true;
                        return false;
                    }
                    if (txtCuit.Text.Trim().Length != 11 && Autorizados.Count == 0)
                    {
                        //mensaje de error.
                        lblMensajeErrorVentanaEncabezado.Text = "El formato de CUIT no es correcto.";
                        AgregarCssClass("campoRequerido", txtCuit);
                        divMensajeErrorVentanaEncabezado.Visible = true;
                        return false;
                    }
                    validado = true;
                    break;
                case NumeroPasoEnum.PrimerPaso:
                    if (ObjetoInscripcion.TipoTramite == 1 && (int)Session["ImprimirTasa"] == 0)
                    {
                        divMensajeError.Visible = true;
                        lblMensajeError.Text = "es obligatorio imprimir la tasa para continuar el trámite.";
                        return false;
                    }
                    validado = true;
                    break;
                case NumeroPasoEnum.SegundoPaso: //BOCA Y CDD

                    if (DivAdjuntar.Visible)
                    {
                        if (IdDocumentoCDD1 != null)
                        {
                            if (IdDocumentoCDD1 == 0)
                            {
                                divMensajeError.Visible = true;
                                lblMensajeError.Text = "es obligatorio adjuntar la habilitación municipal.";
                                return false;
                            }
                        }

                        if (IdDocumentoCDD2 != null)
                        {
                            if (IdDocumentoCDD2 == 0)
                            {
                                divMensajeError.Visible = true;
                                lblMensajeError.Text = "es obligatorio adjuntar la constancia de afip.";
                                return false;
                            }
                        }

                        return true;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case NumeroPasoEnum.TercerPaso: //ESTABLECIMIENTO

                    // 1) DOMICILIOS LEGAL
                    HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
                    HttpRequestWrapper Requestwrapper = new HttpRequestWrapper(Request);

                    DomLegal = Helper.getDomicilio(sessionBase, Requestwrapper, "SIFLEG" + ObjetoInscripcion.CUIT);
                    if (DomLegal != null)
                    {
                        if (DomLegal.IdVin == 0)
                        {
                            lblMensajeError.Text = "El domicilio Legal del comercio no fue actualizado por favor registre el domiclio haciendo click en 'GUARDAR' en la seccion II - DOMICILIO LEGAL.";
                            divMensajeError.Visible = true;
                            return false;
                        }

                    }
                    else
                    {
                        lblMensajeError.Text = "El domicilio Legal del comercio no fue actualizado por favor registre el domiclio haciendo click en 'GUARDAR' en la seccion II - DOMICILIO LEGAL.";
                        divMensajeError.Visible = true;
                        return false;

                    }

                    //// 2) DOMICILIOS ESTABLECIMIENTO
                    DomComercio = Helper.getDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + ObjetoInscripcion.NroSifcos);
                    if (DomComercio != null)
                    {
                        if (DomComercio.IdVin == 0)
                        {
                            lblMensajeError.Text = "El domicilio real del comercio no fue actualizado por favor registre el domiclio haciendo click en 'GUARDAR' en la seccion I - DOMICILIO COMERCIAL.";
                            divMensajeError.Visible = true;
                            return false;
                        }
                    }
                    else
                    {
                        lblMensajeError.Text = "El domicilio real del comercio no fue actualizado por favor registre el domiclio haciendo click en 'GUARDAR' en la seccion I - DOMICILIO COMERCIAL.";
                        divMensajeError.Visible = true;
                        return false;
                    }


                    // 2) CONTACTO
                    if (string.IsNullOrEmpty(txtEmail_Establecimiento.Text))
                    {
                        lblMensajeError.Text = "Debe ingresar el Email con que trabaja el establecimiento.";
                        AgregarCssClass("campoRequerido", txtEmail_Establecimiento);
                        divMensajeError.Visible = true;
                        return false;
                    }

                    // Autor: (IB) Verificamos formato del mail
                    if (!IsMailValid(txtEmail_Establecimiento.Text))
                    {
                        lblMensajeError.Text = "El formato del mail no es correcto.";
                        AgregarCssClass("campoRequerido", txtEmail_Establecimiento);
                        divMensajeError.Visible = true;
                        return false;
                    }

                    // Autor: (IB)Verificamos si ese email está libre 
                    string VerificarMail = WebConfigurationManager.AppSettings["VerificarMail"];
                    if (VerificarMail == "1")
                    {
                        string mensaje = "";
                        bool retorno;
                        retorno = Bl.BlEmailLibre(txtCuitLeido.Text, txtEmail_Establecimiento.Text, out mensaje);
                        if (!retorno)
                        {
                            lblMensajeError.Text = mensaje;
                            AgregarCssClass("campoRequerido", txtEmail_Establecimiento);
                            divMensajeError.Visible = true;
                            return false;
                        }
                    }


                    if ((string.IsNullOrEmpty(txtTelFijoCodArea.Text) || string.IsNullOrEmpty(txtTelFijo.Text)) &&
                        (string.IsNullOrEmpty(txtCelularCodArea.Text) || string.IsNullOrEmpty(txtCelular.Text)))
                    {
                        lblMensajeError.Text = "Debe ingresar al menos Telefono Fijo ó Celular.";
                        if (string.IsNullOrEmpty(txtTelFijoCodArea.Text) || string.IsNullOrEmpty(txtTelFijo.Text))
                        {
                            AgregarCssClass("campoRequerido", txtTelFijoCodArea);
                            AgregarCssClass("campoRequerido", txtTelFijo);
                        }
                        if (string.IsNullOrEmpty(txtCelularCodArea.Text) || string.IsNullOrEmpty(txtCelular.Text))
                        {
                            AgregarCssClass("campoRequerido", txtCelular);
                            AgregarCssClass("campoRequerido", txtCelularCodArea);
                        }

                        divMensajeError.Visible = true;
                        return false;
                    }



                    return true;

                case NumeroPasoEnum.CuartoPaso: //INFORMACIÓN ADICIONAL
                    //Rubros y Productos
                    if (string.IsNullOrEmpty(ddlRubroPrimario.SelectedValue) ||
                        ddlRubroPrimario.SelectedValue == "0")
                    //||ddlRubroSecundario.SelectedValue == "0") LA ACTIVIDAD SECUNDARIA PUEDE SER SIN ASIGNAR.
                    {
                        lblMensajeError.Text = "Debe seleccionar Actividad Primaria.";
                        AgregarCssClass("campoRequerido", ddlRubroPrimario);
                        divMensajeError.Visible = true;
                        return false;
                    }
                    validado = true;

                    //INFORMACIÓN GENERAL

                    if (string.IsNullOrEmpty(txtFechaIniAct.Text))
                    {
                        lblMensajeError.Text = "Debe ingresar la Fecha de Inicio de Actividad.";
                        AgregarCssClass("campoRequerido", txtFechaIniAct);
                        divMensajeError.Visible = true;
                        return false;
                    }

                    if (string.IsNullOrEmpty(txtNroHabMun.Text))
                    {
                        lblMensajeError.Text = "Debe ingresar el Nro de Habilitación Municipal.";
                        AgregarCssClass("campoRequerido", txtNroHabMun);
                        divMensajeError.Visible = true;
                        return false;
                    }
                    if (string.IsNullOrEmpty(txtM2Venta.Text))
                    {
                        lblMensajeError.Text = "Debe ingresar el valor la Superficie de venta.";
                        AgregarCssClass("campoRequerido", txtM2Venta);
                        divMensajeError.Visible = true;
                        return false;
                    }
                    else
                    {
                        if (double.Parse(txtM2Venta.Text) < 1)
                        {
                            lblMensajeError.Text = "La Superficie de venta debe ser mayor o igual a 1.";
                            AgregarCssClass("campoRequerido", txtM2Venta);
                            divMensajeError.Visible = true;
                            return false;
                        }
                    }

                    if (string.IsNullOrEmpty(txtCantPersTotal.Text))
                    {
                        lblMensajeError.Text = "La Cantidad de Personal Total es un campo obligatorio.";
                        AgregarCssClass("campoRequerido", txtCantPersTotal);
                        divMensajeError.Visible = true;
                        return false;
                    }
                    else
                    {
                        if (double.Parse(txtCantPersTotal.Text) < 1)
                        {
                            lblMensajeError.Text = "La Cantidad de Personal Total debe ser un valor superior ó igual a 1.";
                            AgregarCssClass("campoRequerido", txtCantPersTotal);
                            divMensajeError.Visible = true;
                            return false;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtCantPersRelDep.Text))
                    {
                        if (double.Parse(txtCantPersRelDep.Text) > double.Parse(txtCantPersTotal.Text))
                        {
                            lblMensajeError.Text = "La Cantidad de Personal en Relación de Dependencia no puede superar a la Cantidad de Personal Total.";
                            AgregarCssClass("campoRequerido", txtCantPersRelDep);
                            divMensajeError.Visible = true;
                            return false;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtM2Admin.Text))
                    {
                        if (long.Parse(txtM2Admin.Text) < 1)
                        {
                            lblMensajeError.Text = "Los valores de las Superficies Administración debe ser 1 o mayor.";
                            divMensajeError.Visible = true;
                            return false;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtM2Venta.Text))
                    {
                        if (long.Parse(txtM2Venta.Text) < 1)
                        {
                            lblMensajeError.Text = "Los valores de las Superficies de Venta debe ser 1 o mayor.";
                            divMensajeError.Visible = true;
                            return false;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtM2Dep.Text))
                    {
                        if (long.Parse(txtM2Dep.Text) < 1)
                        {
                            lblMensajeError.Text = "Los valores de las Superficies de Deposito debe ser 1 o mayor.";
                            divMensajeError.Visible = true;
                            return false;
                        }
                    }


                    if (!rbInquilino.Checked && !rbPropietario.Checked)
                    {
                        lblMensajeError.Text = "Debe indicar si es Propietario o Inquilino.";
                        divMensajeError.Visible = true;

                        return false;
                    }

                    if (rbInquilino.Checked)
                    {
                        if (!rb1015.Checked && !rb1520.Checked && !rb5.Checked && !rb510.Checked)
                        {
                            lblMensajeError.Text = "Si es Inquilino, debe seleccionar un monto de alquiler.";
                            divMensajeError.Visible = true;
                            return false;
                        }
                    }
                    //CANTIDAD DE PERSONAL CON RELACIÓN DE DEPENDENCIA
                    //if (string.IsNullOrEmpty(txtCantPersRelDep.Text) || string.IsNullOrEmpty(txtCantPersTotal.Text))
                    //{
                    //    lblMensajeError.Text = "Debe completar la Cantidad de Personal.";
                    //    divMensajeError.Visible = true;
                    //    return false;
                    //}

                    //INFORMACIÓN ADICIONAL
                    if (!rbSiCap.Checked && !rbNoCap.Checked)
                    {
                        lblMensajeError.Text = "Debe indicar si realizó Capacitación o no.";
                        divMensajeError.Visible = true;
                        return false;
                    }
                    if (!rbSiCobertura.Checked && !rbNoCobertura.Checked)
                    {
                        lblMensajeError.Text = "Debe indicar si posee Cobertura o no.";
                        divMensajeError.Visible = true;
                        return false;
                    }

                    if (!rbSiSeg.Checked && !rbNoSeg.Checked)
                    {
                        lblMensajeError.Text = "Debe indicar si posee Seguro o no.";
                        divMensajeError.Visible = true;
                        return false;
                    }
                    if (!chkprov.Checked && !ChkInter.Checked && !ChkNacional.Checked)
                    {
                        lblMensajeError.Text = "Debe indicar al menos una opción de 'Origen proveedor'.";
                        divMensajeError.Visible = true;
                        return false;
                    }



                    break;


                case NumeroPasoEnum.QuintoPaso:
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
                            if (txtEmailConta.Text.Trim() == txtEmail_Establecimiento.Text.Trim())
                            {
                                lblMensajeError.Text = "EL EMAIL DE USTED (" + txtEmailConta.Text + ") NO PUEDE SER UTILIZADO COMO 'EMAIL DEL ESTABLECIMIENTO' (INGRESADO EN EL PASO 2), YA QUE ES UN GESTOR DEL TRÁMITE.";
                                divMensajeError.Visible = true;
                                return false;
                            }
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

                            if (txtEmailConta.Text.Trim() == txtEmail_Establecimiento.Text.Trim())
                            {
                                lblMensajeError.Text = "EL EMAIL DE USTED (" + txtEmailConta.Text + ") NO PUEDE SER UTILIZADO COMO 'EMAIL DEL ESTABLECIMIENTO' (INGRESADO EN EL PASO 2), YA QUE ESTÁ REALIZANDO EL TRÁMITE EN CARACTER DE CONTADOR.";
                                divMensajeError.Visible = true;
                                return false;
                            }
                            break;
                    }

                    validado = true;
                    break;
            }
            return validado;
        }
        private bool GuardarContacto()
        {

            var entidad = NroTramiteAImprimir;
            var resultado = "";

            resultado =
                Bl.BlRegistrarContacto(new Comunicacion()
                {
                    IdEntidad = entidad.ToString(),
                    CodAreaCel = txtCelularCodArea.Text,
                    CodAreaTelFijo = txtTelFijoCodArea.Text,
                    EMail = txtEmail_Establecimiento.Text,
                    Facebook = txtRedSocial.Text,
                    NroCel = txtCelular.Text,
                    NroTelfijo = txtTelFijo.Text,
                    PagWeb = txtWebPage.Text
                });


            //cargo Contacto nuevo

            if (resultado == "OK")
            {
                //se cargó el contacto con exito.
                return true;
            }
            lblMensajeError.Text = "ERROR AL CARGAR CONTACTO. " + resultado;
            divMensajeError.Visible = true;
            return false;
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
            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento)
            {
                html.Append("");
                html.AppendLine(" <p> ");

                html.AppendLine(master.UsuarioCidiLogueado.Apellido + ", " + master.UsuarioCidiLogueado.Nombre);
                html.AppendLine(" . Ud está finalizando con el trámite de REEMPADRONAMIENTO. Confirme que la información ingresada es correcta. ");
                html.AppendLine(" Luego de Confirmar se emitirá un Comprobante de trámite realizado. ");
                html.AppendLine(" </p> ");
                divHtml_EncabezadoModalFinalizacion.InnerHtml = html.ToString();
            }
            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento_ViejoSifcos)
            {
                html.Append("");
                html.AppendLine(" <p> ");

                html.AppendLine(master.UsuarioCidiLogueado.Apellido + ", " + master.UsuarioCidiLogueado.Nombre);
                html.AppendLine(" . Ud está finalizando con el trámite de REEMPADRONAMIENTO. Confirme que la información ingresada es correcta. ");
                html.AppendLine(" Luego de Confirmar se emitirá un Comprobante de trámite realizado. ");
                html.AppendLine(" </p> ");
                divHtml_EncabezadoModalFinalizacion.InnerHtml = html.ToString();
            }
            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
            {
                html.Append("");
                html.AppendLine(" <p> ");
                html.AppendLine(master.UsuarioCidiLogueado.Apellido + ", " + master.UsuarioCidiLogueado.Nombre);
                html.AppendLine(" . Ud está finalizando con el trámite de INSCRIPCIÓN A SIFCoS. Confirme que la información ingresada es correcta. ");
                html.AppendLine(" Luego de Confirmar se emitirá lo siguitente: ");
                html.AppendLine(" </p> ");
                html.AppendLine("<ul> ");
                html.AppendLine("<li> Comprobante del trámite realizado</li> ");
                html.AppendLine("<li> Tasa Retributiva de Servicio: Ud. deberá pagarla para presentar en la Boca de Recepción para poder dar de alta a la entidad.</li> ");
                html.AppendLine("</ul> ");
                html.AppendLine("</ul> ");
                divHtml_EncabezadoModalFinalizacion.InnerHtml = html.ToString();
            }

        }

        protected void btnConfirmarImprimirTramite_OnClick(object sender, EventArgs e)
        {
            conteoClicks = ContadorClicksEnBotonConfirmar;
            // actualizacion junio 2025
            // Para registrar el tramite si es de la ciudad de Cordoba se registra para el organismo Camara de comercio sino se registra para FEDECOM
            ObjetoInscripcion.DomComercio = new ComercioDto();
            if (DomComercio.IdVin != 0)
            {
                ObjetoInscripcion.IdVinDomLocal = DomComercio.IdVin;
                if (DomComercio.Localidad.IdLocalidad == 1)
                {
                    ObjetoInscripcion.IdOrganismo = 3;
                }
                else
                {
                    ObjetoInscripcion.IdOrganismo = 2;
                }
            }
            if (DomLegal.IdVin != 0)
            {
                ObjetoInscripcion.IdVinDomLegal = DomLegal.IdVin;
            }

            if (ContadorClicksEnBotonConfirmar > 0)
            {
                //TRUE: existe el tramite en BD guadado , FALSE : no existe guardado en BD.
                bool existeTramite = Bl.BlExisteTramite(ObjetoInscripcion);
                if (existeTramite)
                    return;
            }

            if (ContadorClicksEnBotonConfirmar == 0)
            {
                conteoClicks++;
                ContadorClicksEnBotonConfirmar = conteoClicks;
            }

            if (NumeroDePasoActual != NumeroPasoEnum.QuintoPaso)
                return;

            CargarDatosObjInscripcion(NumeroPasoEnum.QuintoPaso);

            string RegistrarEntidad = "";
            var Entidad = 0;
            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
            {
                //Si es un ALTA imprimo. si es Reempa.. no hace falta porque ya se utilizó las tasas al inicio.
               // ImprimirTRSAlta();
                ObjetoInscripcion.NroSifcos = "0"; //DEJAR EL CERO!! NO CAMBIAR!! SINO SE ROMPE LA INSCRIPCIÓN. limpio el nro sifcos. por si estuvo en una sesión anterior realizando reempadronamiento.
                RegistrarEntidad = Bl.BlRegistrarEntidad(ObjetoInscripcion, out Entidad);
            }
            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento || ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento_ViejoSifcos)
            {
                //lt: al nro sifcos si o si lo tiene que tener pq es un reempadronamiento y llego hasta aca
                var nrosifcos = ObjetoInscripcion.NroSifcos;
                //lt: el identidad puede tenerlo o no segun este en sifcos nuevo

                if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento_ViejoSifcos)
                {
                    RegistrarEntidad = Bl.BlRegistrarEntidadMigracion(ObjetoInscripcion, out Entidad);
                }
                else
                {
                    if (ObjetoInscripcion.IdEntidad == "0")
                    {
                        RegistrarEntidad = Bl.BlRegistrarEntidadMigracion(ObjetoInscripcion, out Entidad);
                    }
                    else
                    {
                        ObjetoInscripcion.DomComercio.ID_ENTIDAD = int.Parse(ObjetoInscripcion.IdEntidad);
                    }
                }
            }
            if (RegistrarEntidad == "OK")
            {
                ObjetoInscripcion.DomComercio.ID_ENTIDAD = Entidad;
            }


            //2 - GuardarInscripcionSifcos();

            GuardarInscripcionSifcos();

            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento || ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reempadronamiento_ViejoSifcos)
            {
                //Solo se actualiza el ID_ORGANISMO_ALTA si el tramite es de reempadronamiento.
                var res = Bl.BlActualizarOrganismoAltaPorNroSifcos(int.Parse(ObjetoInscripcion.NroTramite),
                    int.Parse(ObjetoInscripcion.NroSifcos));
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
            var TipoTramiteNum = ObjetoInscripcion.TipoTramiteNum;
            LimpiarSessionesEnMemoria();
            if (TipoTramite == "BAJA")
            {
                TipoTramite = "";
                Response.Redirect("BajaDeComercio.aspx?Exito=1");

            }
            Response.Redirect("MisTramites.aspx?Exito=1");
        }
        private void GuardarInscripcionSifcos()
        {

            ObjetoInscripcion.IdSede = idSedeSeleccionada;
            ObjetoInscripcion.ListaTrs = ListaTrs;

            string nuevoNroTramite = "";
            var resultado = Bl.BlRegistrarInscripcion(ObjetoInscripcion, out nuevoNroTramite);

            ObjetoInscripcion.NroTramite = nuevoNroTramite;
            switch (resultado)
            {
                case "OK":
                    NroTramiteAImprimir = Convert.ToInt64(nuevoNroTramite);
                    if (!GuardarContacto())
                    {
                        lblMensajeExito.Text = "EL TRAMITE SE GUARDÓ CON EXITO. EL CONTACTO ASOCIADO NO SE PUDO GUARDAR";
                        divMensajeExito.Visible = true;
                        break;
                    }

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
                    MostrarOcultarModalFinalizacionTramite(false);
                    lblMensajeError.Text =
                        "EL TRÁMITE YA EXISTE.";
                    divMensajeError.Visible = true;
                    break;
                default:
                    MostrarOcultarModalFinalizacionTramite(false);
                    lblMensajeError.Text =
                        "OCURRIÓ UN ERROR AL GUARDAR EL TRÁMITE. COMUNÍQUESE CON SIFCoS PARA DARLE SOPORTE. MUCHAS GRACIAS.";
                    divMensajeError.Visible = true;
                    break;


            }
        }
        private TipoTramiteEnum ObtenerTramiteSegunCuit()
        {

            if (Session["VENGO_DESDE_BAJA_NRO_SIFCOS"] != null)
            {
                ObjetoInscripcion.NroSifcos = (String)Session["VENGO_DESDE_BAJA_NRO_SIFCOS"];

            }

            // se verifica si viene del viejo sifcos 
            var viejoSifcos = Bl.BlGetTramitesSifcosViejo(null, txtNroSifcos.Text.Trim(), null);
            SessionTramitesDelCuit = Bl.BlGetEntidadTramite(PCuit);
            if (viejoSifcos.Rows.Count > 0)
            {
                lblTituloTramite.Text = "REEMPADRONAMIENTO MIGRADO - SIFCoS ";
                lblTituloVentanaModalPrincipal.Text = "REEMPADRONAMIENTO MIGRADO - SIFCoS ";
                return TipoTramiteEnum.Reempadronamiento_ViejoSifcos;
            }


            if (SessionTramitesDelCuit.Count == 0)
            {
                //modalInformacionTituloTramite.Visible = false;
                //MostrarOcultarModalVentanaMantSistema(true);
                return TipoTramiteEnum.Instripcion_Sifcos;
            }

            return TipoTramiteEnum.Reempadronamiento;


        }
        private void PreCargarCamposParaReempadronamiento()
        {
            DataTable dt = Bl.BlGetUltimoTramiteSifcosNuevo(ObjetoInscripcion.NroSifcos);


            var _Id_tramite = int.Parse(dt.Rows[0]["NRO_TRAMITE_SIFCOS"].ToString());


            #region CONTACTO
            DataTable dtContacto = Bl.BlGetComEmpresa(_Id_tramite.ToString());

            if (dtContacto.Rows.Count > 0)
            {
                foreach (DataRow row in dtContacto.Rows)
                {
                    switch (row["ID_TIPO_COMUNICACION"].ToString())
                    {
                        case "01": //TELEFONO FIJO
                            txtTelFijoCodArea.Text = row["COD_AREA"].ToString();
                            txtTelFijo.Text = row["NRO_MAIL"].ToString();
                            break;
                        case "07": //CELULAR
                            txtCelularCodArea.Text = row["COD_AREA"].ToString();
                            txtCelular.Text = row["NRO_MAIL"].ToString();
                            break;
                        case "11": //EMAIL
                            txtEmail_Establecimiento.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;
                        case "12": //PAGINA WEB
                            txtWebPage.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;
                        case "17": //FACEBOOK
                            txtRedSocial.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;
                    }
                }
            }

            #endregion //CONTACTO


            //#endregion //DOMICILIO LEGAL


            #region PRODUCTO - ACTIVIDAD

            DtProductos = Bl.BlGetProductosTramite(_Id_tramite.ToString());
            RefrescarGrillaProductos();
            chkConfirmarListaDeProducto.Checked = true; //no entra al EVENTO DEL CHECKBOX

            ddlRubroPrimario.Enabled = true;
            ddlRubroSecundario.Enabled = true;
            CargarRubrosSegunProductos();

            //if (DtProductos.Rows.Count > 0)
            //{
            //    ddlRubroPrimario.SelectedValue = dt.Rows[0]["ID_ACTIVIDAD_PPAL"].ToString();
            //    ddlRubroSecundario.SelectedValue = dt.Rows[0]["ID_ACTIVIDAD_SRIA"].ToString();
            //}


            #endregion // PRODUCTO - ACTIVIDAD

            #region INFO GENERAL

            ObjetoInscripcion.IdEntidad = dt.Rows[0]["ID_ENTIDAD"].ToString();
            txtFechaIniAct.Text = dt.Rows[0]["FEC_INI_TRAMITE"].ToString().Substring(0,10);

            //txtNroHabMun.Text = ""; // viene precargado antes.
            //txtNroDGR.Text = "";// viene precargado antes.
            var dtSuperficie = Bl.BlGetSuperficeByNroTramite(_Id_tramite);
            foreach (DataRow row in dtSuperficie.Rows)
            {
                switch (row["id_tipo_superficie"].ToString())
                {
                    case "1": //SUP. ADMINISTRACION 
                        txtM2Admin.Text = row["superficie"].ToString();
                        break;
                    case "2"://SUP. VENTAS
                        txtM2Venta.Text = row["superficie"].ToString();
                        break;
                    case "3"://SUP. DEPOSITO
                        txtM2Dep.Text = row["superficie"].ToString();
                        break;
                }
            }
            txtCantPersRelDep.Text = dt.Rows[0]["CANT_PERS_REL_DEPENDENCIA"].ToString();
            txtCantPersTotal.Text = dt.Rows[0]["CANT_PERS_TOTAL"].ToString();
            rbPropietario.Checked = dt.Rows[0]["PROPIETARIO"].ToString() == "S";
            rbInquilino.Checked = dt.Rows[0]["PROPIETARIO"].ToString() == "N";
            rb5.Checked = dt.Rows[0]["RANGO_ALQ"].ToString() == "MENOS DE $500000";
            rb510.Checked = dt.Rows[0]["RANGO_ALQ"].ToString() == "$500000 a $1000000";
            rb1015.Checked = dt.Rows[0]["RANGO_ALQ"].ToString() == "$1000000 a $3000000";
            rb1520.Checked = dt.Rows[0]["RANGO_ALQ"].ToString() == "MAS DE $3000000";
            rbSiCap.Checked = dt.Rows[0]["CAPACITACION_ULT_ANIO"].ToString() == "S";
            rbNoCap.Checked = dt.Rows[0]["CAPACITACION_ULT_ANIO"].ToString() == "N";
            rbSiCobertura.Checked = dt.Rows[0]["COBERTURA_MEDICA"].ToString() == "S";
            rbNoCobertura.Checked = dt.Rows[0]["COBERTURA_MEDICA"].ToString() == "N";
            rbSiSeg.Checked = dt.Rows[0]["SEGURO_LOCAL"].ToString() == "S";
            rbNoSeg.Checked = dt.Rows[0]["SEGURO_LOCAL"].ToString() == "N";

            switch (dt.Rows[0]["ID_ORIGEN_PROVEEDOR"].ToString())
            {
                case "1":
                    chkprov.Checked = true;
                    ChkNacional.Checked = false;
                    ChkInter.Checked = false;
                    break;
                case "2":
                    chkprov.Checked = false;
                    ChkNacional.Checked = true;
                    ChkInter.Checked = false;
                    break;
                case "3":
                    chkprov.Checked = false;
                    ChkNacional.Checked = false;
                    ChkInter.Checked = true;
                    break;
                case "12":
                    chkprov.Checked = true;
                    ChkNacional.Checked = true;
                    ChkInter.Checked = false;
                    break;
                case "123":
                    chkprov.Checked = true;
                    ChkNacional.Checked = true;
                    ChkInter.Checked = true;
                    break;
                case "23":
                    chkprov.Checked = false;
                    ChkNacional.Checked = true;
                    ChkInter.Checked = true;
                    break;
                case "13":
                    chkprov.Checked = true;
                    ChkNacional.Checked = false;
                    ChkInter.Checked = true;
                    break;
            }



            #endregion  //INFO GRAL

            //GESTOR DEL TRAMITE
            //var dtGestor = Bl.BlGetGestor(int.Parse(dt.Rows[0]["ID_GESTOR_ENTIDAD"].ToString()));
            //dtGestor.Rows[0]["CUIL"].ToString();

            //REPRESENTANTE LEGAL
        }

        /// <summary>
        /// Método que tiene el comportamiento similar al boton IniciarTramite. La diferencia es que se tiene en Session al NroSifcos y Cuit Seleccionado
        /// </summary>
        private void IniciarTramiteParaBajaComercio()
        {

            ContadorClicksEnBotonConfirmar = 0;
            PCuit = (string)Session["VENGO_DESDE_BAJA_CUIT"];
            txtNroSifcos.Text = (string)Session["VENGO_DESDE_BAJA_NRO_SIFCOS"];


            if (ObjetoInscripcion == null)
                Response.Redirect("Inscripcion.aspx");
            if (!ValidarDatosRequeridos(NumeroPasoEnum.PasoIniciarTramite))
                return;


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

            ObjetoInscripcion.TipoTramiteNum = ObtenerTramiteSegunCuit();
            ObjetoInscripcion.CUIT = PCuit;

            //DEBO SELECCIONAR LA SEDE 
            CargarDomiciliosExistentes();

            MostrarOcultarModalTramiteSifcos(true);
            DivAdjuntar.Visible = false;

            //consulta si existe tramite para el cuit ingresado.
            //Bl.BlExisteEnSifcos(ObjetoInscripcion.CUIT, out pExiste);
            //Lo correcto sería pExiste = 1 --> NO EXISTE EN SIFCOS. 
            //                  pExiste = 2 --> SI EXISTE EN SIFCOS.

            switch (ObjetoInscripcion.TipoTramiteNum)
            {
                case TipoTramiteEnum.Reempadronamiento:
                    ObjetoInscripcion.TipoTramite = 4;
                    lblTituloTramite.Text = "REEMPADRONAMIENTO - SIFCoS ";
                    lblTituloVentanaModalPrincipal.Text = "REEMPADRONAMIENTO - SIFCoS ";
                    divSeccionReempadronamiento.Visible = true;
                    divSeccionInscripcionTramite.Visible = false;
                    divBotonImpirmirTRS.Visible = false;
                    divCheckNuevaSucursal.Visible = true;
                    break;
                case TipoTramiteEnum.Reempadronamiento_ViejoSifcos:
                    ObjetoInscripcion.TipoTramite = 4;
                    lblTituloTramite.Text = "REEMPADRONAMIENTO PARA REALIZAR BAJA - SIFCoS ";
                    lblTituloVentanaModalPrincipal.Text = "REEMPADRONAMIENTO PARA BAJA - SIFCoS ";
                    divSeccionReempadronamiento.Visible = true;
                    divSeccionInscripcionTramite.Visible = false;
                    divBotonImpirmirTRS.Visible = false;
                    divCheckNuevaSucursal.Visible = true;
                    break;

            }



            divVentanaPasosTramite.Visible = true;

            AceptarTramiteARealizar_ParaBajaComercio();
        }
        /// <summary>
        /// Similar al Boton btnAceptarTramiteARealizar pero se ejecuta cuando se viene desde la pantalla Baja de Comercio.
        /// </summary>
        private void AceptarTramiteARealizar_ParaBajaComercio()

        {
            var bandTieneTramitesEnSifcosNuevo = false;
            SessionTramitesDelCuit = Bl.BlGetEntidadTramite(PCuit);
            if (SessionTramitesDelCuit.Count != 0)
            {
                foreach (var tramites in SessionTramitesDelCuit)
                {
                    if (tramites.Origen == "SIFCOS_NUEVO" && tramites.Nro_Sifcos == ObjetoInscripcion.NroSifcos)
                    {
                        bandTieneTramitesEnSifcosNuevo = true;
                        break;
                    }
                }

            }

            if (ObjetoInscripcion.TipoTramiteNum != TipoTramiteEnum.Instripcion_Sifcos)
            {

                //validar si existe el NroSifcos ingresado
                var bandExisteNroSifcosIngresado = false;


                ObjetoInscripcion.NroSifcos = (string)Session["VENGO_DESDE_BAJA_NRO_SIFCOS"];




                //NRO SIFCoS SI EXISTE, Y ESTÁ POR REALIZAR EL REEMPADRONAMIENTO.


                //asigno FECHA DE VENCIMIENTO al tramite.
                int anios_debe = 0;
                DateTime? fecVto;
                DateTime? fecAux = null; // es la fecha de presentación que se utiliza en el sifcos viejo.Representa que el PROX. VTO = FECHA_PRESENTACIÓN + 1 AÑO.
                DateTime fecUltVto;

                var fechaCeseBajaMunicipal = DateTime.Parse((string)Session["VENGO_DESDE_BAJA_FECHA_CESE"]);


                if (bandTieneTramitesEnSifcosNuevo)
                {

                    fecVto = Bl.BlGetFechaUltimoTramiteSifcosNuevo(ObjetoInscripcion.NroSifcos);
                    fecAux = fecVto.Value.AddYears(-1);



                    TimeSpan ts = fechaCeseBajaMunicipal - fecAux.Value;
                    anios_debe = ts.Days / 365;
                    // NO DEBE REEMPADRONARSE, ESTÁ AL DÍA.
                    if (anios_debe == 0)
                    {
                        lblFechaUltimoTramite.Text = "Fecha del próximo vencimiento : " + fecVto.Value.Day + "/" +
                                                     fecVto.Value.Month + "/" + fecVto.Value.Year;
                        lblCantTrsPagas.Text = "USTED SE ENCUENTRA AL DÍA. DEBE REALIZAR EL REEMPADRONAMIENTO POSTERIOR A LA FECHA DE VENCIMIENTO.";
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
                    /*No tiene tramites en el sifcos nuevo. Ultimo tramite es del sifcos viejo*/

                    fecVto = Bl.BlGetFechaUltimoTramiteSifcosViejo(txtNroSifcos.Text.Trim()).Value;// me va a traer la fecha de presentacion.
                    fecAux = fecVto.Value.AddYears(1);


                    TimeSpan ts = fechaCeseBajaMunicipal - fecAux.Value;
                    anios_debe = ts.Days / 365;
                    ObjetoInscripcion.FechaVencimiento = ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Baja_de_Comercio ? fecAux.Value.AddYears(anios_debe) : fecAux.Value.AddYears(anios_debe + 1);
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

                divBotonImpirmirTRS.Visible = true;
                divSeccionInscripcionTramite.Visible = false;
                divCheckNuevaSucursal.Visible = false;


                //ObjetoInscripcion.FechaVencimiento --> contiene la fecha del proximo vencimiento. 

                lblFechaUltimoTramite.Text = "Fecha Vencimiento : " + ObjetoInscripcion.FechaVencimiento.Day + "/" + ObjetoInscripcion.FechaVencimiento.Month + "/" + (fecUltVto.Year);

                if (fecUltVto < fechaCeseBajaMunicipal)
                {
                    //entonces está vencido y debe reempadronarse.

                    var cantTasasPagasSinUsar = Bl.BlGetCantTasasPagadas(PCuit).ToString();
                    if (cantTasasPagasSinUsar == "0")
                        lblCantTrsPagas.Text = "Usted NO dispone de tasas pagadas.";
                    else
                    {
                        lblCantTrsPagas.Text = "Usted disponde de " + cantTasasPagasSinUsar + " TRS.";
                    }

                    if (int.Parse(cantTasasPagasSinUsar) > anios_debe)
                    {
                        lblCantTrsNoPagas.Text = "A la fecha adeuda " + anios_debe +
                                                 " Reempadronamiento/s. Debe imprimir " + anios_debe + " TRS. ";
                    }
                    else
                    {
                        CantidadTRS_A_Imprimir = anios_debe - int.Parse(cantTasasPagasSinUsar);
                        lblCantTrsNoPagas.Text = "A la fecha adeuda " + anios_debe +
                                                 " Reempadronamiento/s. Debe imprimir " + CantidadTRS_A_Imprimir + " TRS. ";
                    }





                    if (int.Parse(cantTasasPagasSinUsar) < anios_debe)
                    {

                        divSeccionDebeTrs.Visible = true;

                    }
                    else
                    {
                        divSeccionDebeTrs.Visible = false;

                        //entonces la tasa SI está paga, oculto la modal y sigo con el rempadronamiento.

                        //Asigno las TRS pagas a la listaTRs para luego guardarlas al final del reempadronamiento.

                        var trsPagasSinUsar = Bl.BlGetTasasPagadasSinUsar(PCuit);
                        var contadorTrsAUtilizar = 0;
                        var listaAux = new List<Trs>();
                        foreach (var trs in trsPagasSinUsar)
                        {
                            if (contadorTrsAUtilizar < anios_debe)
                            {
                                listaAux.Add(trs);//guardo en memoria las trs a utilizar cdo finalice el tramite de reempadronamiento.
                                contadorTrsAUtilizar++;
                            }
                        }

                        ListaTrs = listaAux;

                        MostrarOcultarModalTramiteSifcos(false);
                    }
                }

            }
            Session["VENGO_DESDE_REEMPA_CUIT"] = (string)Session["VENGO_DESDE_BAJA_CUIT"];
            Session["VENGO_DESDE_REEMPA_NRO_SIFCOS"] = (string)Session["VENGO_DESDE_BAJA_NRO_SIFCOS"];
            Session["VENGO_DESDE_REEMPA_FECHA_CESE"] = (string)Session["VENGO_DESDE_BAJA_FECHA_CESE"];
            Session["VENGO_DESDE_BAJA_CUIT"] = null;
            Session["VENGO_DESDE_BAJA_NRO_SIFCOS"] = null;
            Session["VENGO_DESDE_BAJA_FECHA_CESE"] = null;

        }


        //-aca vamos terminando!
        protected void btnEnviar_Onclick(object sender, EventArgs e)
        {
            if (NumeroDePasoActual != NumeroPasoEnum.CuartoPaso)
            {
                return;
            }
            CargarDatosObjInscripcion(NumeroPasoEnum.CuartoPaso);

            //1 - ImprimirTRS();  
            // ImprimirTRS();

            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
            {
                //Si es un ALTA imprimo. si es Reempa.. no hace falta porque ya se utilizó las tasas al inicio.
                //ImprimirTRSAlta();
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
        private void CargarInformacionModalFinalizacionTramite()
        {
            CargarHtml_EncabezadoModalFinalizacion();

            lblTituloEmpresaModalConfirmacion.Text = "EMPRESA : " + txtRazonSocial.Text + " | CUIT : " + txtCuit.Text;

            gvProductosActividades_ModalFinalizacion.DataSource = DtProductos;
            gvProductosActividades_ModalFinalizacion.DataBind();

            lblActividadPrimaria.Text = ddlRubroPrimario.SelectedItem.Text;
            lblActividadSecundaria.Text = ddlRubroSecundario.SelectedItem.Text;


            if (DomComercio != null)
            {
                if (DomComercio.IdVin != 0)
                {
                    lblDepartamento.Text = DomComercio.Departamento != null ? DomComercio.Departamento.Nombre : "";
                    lblLocalidad.Text = DomComercio.Localidad != null ? DomComercio.Localidad.Nombre : "";
                    lblBarrio.Text = DomComercio.Barrio != null ? DomComercio.Barrio.Nombre : "SIN ASIGNAR";
                    lblCalle.Text = DomComercio.Calle != null ? DomComercio.Calle.Nombre : "";
                    lblNroCalle.Text = DomComercio.Altura;
                    lblCodPos.Text = DomComercio.CodigoPostal;
                    lblTorre.Text = DomComercio.Torre;
                    lblPiso.Text = DomComercio.Piso;
                    lblNroDepto.Text = DomComercio.Dpto;
                }

            }

            if (DomLegal != null)
            {
                if (DomLegal.IdVin != 0)
                {
                    lblDepartamentoLegal.Text = DomLegal.Departamento != null ? DomLegal.Departamento.Nombre : "";
                    lblLocalidadLegal.Text = DomLegal.Localidad != null ? DomLegal.Localidad.Nombre : "";
                    lblBarrioLegal.Text = DomLegal.Barrio != null ? DomLegal.Barrio.Nombre : "SIN ASIGNAR";
                    lblCalleLegal.Text = DomLegal.Calle != null ? DomLegal.Calle.Nombre : "";
                    lblNroCalleLegal.Text = DomLegal.Altura;
                    lblCodPosLegal.Text = DomLegal.CodigoPostal;
                    lblTorreLegal.Text = DomLegal.Torre;
                    lblPisoLegal.Text = DomLegal.Piso;
                    lblNroDptoLegal.Text = DomLegal.Dpto;
                }

            }

            lblNombreComercio.Text = txtNomFantasia.Text;

            lblEmailC.Text = txtEmail_Establecimiento.Text;
            lblCelular.Text = !string.IsNullOrEmpty(txtCelular.Text) ? "(" + txtCelularCodArea.Text + ")" + txtCelular.Text : " - ";
            lblTelFijo.Text = !string.IsNullOrEmpty(txtTelFijo.Text) ? "(" + txtTelFijoCodArea.Text + ")" + txtTelFijo.Text : " - ";
            lblWebPag.Text = !string.IsNullOrEmpty(txtWebPage.Text) ? txtWebPage.Text : " - ";
            lblFacebook.Text = !string.IsNullOrEmpty(txtRedSocial.Text) ? txtRedSocial.Text : " - ";



            lblFecInicioActividad.Text = txtFechaIniAct.Text;
            lblNroHabilitacionMunicipal.Text = txtNroHabMun.Text;
            lblNroDGR.Text = txtNroDGR.Text;
            lblSuperficieVenta.Text = txtM2Venta.Text;
            lblSuperficieAdministracion.Text = txtM2Admin.Text;
            lblSuperficioDeposito.Text = txtM2Dep.Text;
            lblInmueble_PropietarioInquil.Text = rbInquilino.Checked ? "Inquilino" : "Propietario";

            if (rbInquilino.Checked)
            {
                lblInmueble_RangoAlquiler.Text = rb5.Checked ? "menos de $100.000" : "";
                if (string.IsNullOrEmpty(lblInmueble_RangoAlquiler.Text))
                    lblInmueble_RangoAlquiler.Text = rb510.Checked ? "$100.000 a $300.000" : "";
                if (string.IsNullOrEmpty(lblInmueble_RangoAlquiler.Text))
                    lblInmueble_RangoAlquiler.Text = rb1015.Checked ? "$300.000 a $500.000" : "";
                if (string.IsNullOrEmpty(lblInmueble_RangoAlquiler.Text))
                    lblInmueble_RangoAlquiler.Text = rb1520.Checked ? "mas de $500.000" : "";
            }
            else
            {
                lblInmueble_RangoAlquiler.Text = "No Posee";
            }

            lblCantPersRel.Text = txtCantPersRelDep.Text;
            lblCantPersTotal.Text = txtCantPersTotal.Text;
            lblPoseeCobert_SiNo.Text = rbSiCobertura.Checked ? "Si posee cobertura médica" : "No posee cobertura médica";
            lblCapacitacionUltAnio.Text = rbSiCap.Checked ? "Si se capacitó." : "No se capacitó.";
            lblPoseeSeguro.Text = rbSiSeg.Checked ? "Si posee seguro social" : "No posee seguro social";
            var seg = chkprov.Checked ? "Provincial" : "";
            seg = ChkNacional.Checked ? seg + " - " + "Nacional" : seg + "";
            seg = ChkInter.Checked ? seg + " - " + "Internacional" : seg + "";
            lblOrigenProveedor.Text = seg;

        }
        private void LimpiarSessionesEnMemoria()
        {
            IdVinDomicilio1 = null;
            IdVinDomicilio2 = null;
            NroTramiteAImprimir = 0;
            Session["NroTransaccionTasa_Inscripcion"] = null;
            //Session["CuitAutorizados"] = null;

            //Session["ReporteGeneral"] = null;
            ObjetoInscripcion = null;
            DtDomicilios1 = null;
            DtDomicilios2 = null;
            DtActividades = null;
            // DtBarrios = null;
            DtComunicaciones = null;
            DtEmpresa = null;
            DtGrilla = null;
            //SessionBarrios = null;
            //SessionDepartamentos = null;
            //SessionLocalidades = null;
            SessionTramitesDelCuit = null;
            ObjetoInscripcion = null;
            Session["ImprimirTasa"] = 0;


        }
        private void ImprimirReporteTramite()
        {
            //var tramiteDto = new InscripcionSifcosDto();
            var lista = Bl.GetInscripcionSifcosDto(NroTramiteAImprimir).ToList();
            if (lista.Count == 0)
                return;
            InscripcionSifcosDto tramiteDto = lista[0];


            tramiteDto.ActividadPrimaria = lblActividadPrimaria.Text;
            tramiteDto.ActividadSecundaria = lblActividadSecundaria.Text;
            tramiteDto.NroHabMunicipal = txtNroHabMun.Text;


            //Se crea un arreglo de tipo byte que contendra el texto codificado en QR.
            String hash = HttpUtility.UrlEncode(Encrypt(tramiteDto.NroTramiteSifcos));
            Bitmap Arreglo_Imagen = GeneraCodigoQR("https://sifcos.cba.gov.ar/ComprobanteOnline.aspx?key=" + hash);

            var lis = new List<Producto>();
            DataTable dtProductosTramite = Bl.BlGetProductosTramite(NroTramiteAImprimir.ToString());
            foreach (DataRow row in dtProductosTramite.Rows)
            {
                lis.Add(new Producto { IdProducto = row["idproducto"].ToString(), NProducto = row["nproducto"].ToString(), IdRubro = "", UrlCodigoQr = "", ImgBmp = imageToByteArray(Arreglo_Imagen) });
            }

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
                            tramiteDto.WebPage = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;
                        case "17": //FACEBOOK
                            tramiteDto.Facebook = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;

                    }
                }

            }



            tramiteDto.CelularGestor = UsuarioCidiGestor != null ? UsuarioCidiGestor.CelFormateado : " - ";
            tramiteDto.CelularRepLegal = UsuarioCidiRep != null ? UsuarioCidiRep.CelFormateado : " - ";

            var domicilio1 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLocal.Value.ToString());
            var domicilio2 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLegal.Value.ToString());

            var dtSuperficie = Bl.BlGetSuperficeByNroTramite(Convert.ToInt64(tramiteDto.NroTramiteSifcos));

            foreach (DataRow row in dtSuperficie.Rows)
            {
                switch (row["id_tipo_superficie"].ToString())
                {
                    case "1": //SUP. ADMINISTRACION 
                        tramiteDto.SupAdministracion = string.IsNullOrEmpty(row["superficie"].ToString()) ? 0 : long.Parse(row["superficie"].ToString());
                        break;
                    case "2"://SUP. VENTAS
                        tramiteDto.SupVentas = string.IsNullOrEmpty(row["superficie"].ToString()) ? 0 : long.Parse(row["superficie"].ToString());
                        break;
                    case "3"://SUP. DEPOSITO
                        tramiteDto.SupDeposito = string.IsNullOrEmpty(row["superficie"].ToString()) ? 0 : long.Parse(row["superficie"].ToString());
                        break;
                }
            }
            // Creo el nuevo reporte con NOMBRE DEL REPORTE

            var nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReempadronamiento.rdlc";
            if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
                nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSAlta.rdlc";

            var reporte = new ReporteGeneral(nombreReporteRdlc, lis, TipoArchivoEnum.Pdf);





            //var listCodigo = new List<CodigoQr>();
            //listCodigo.Add(new CodigoQr { UrlCodigoQr = "", ImgBmp = imageToByteArray(Arreglo_Imagen) });
            //reporte.AddDataSource("dataset", listCodigo);

            var barrio1 = " - ";
            if (domicilio1.Barrio != null)
            {
                barrio1 = domicilio1.Barrio.Nombre;

            }
            var departamento1 = " - ";
            if (domicilio1.Departamento != null)
            {
                departamento1 = domicilio1.Departamento.Nombre;

            }
            var localidad1 = " - ";
            if (domicilio1.Localidad != null)
            {
                localidad1 = domicilio1.Localidad.Nombre;

            }
            var calle1 = " - ";
            if (domicilio1.Calle != null)
            {
                calle1 = domicilio1.Calle.Nombre;

            }
            var barrio2 = " - ";
            if (domicilio2.Barrio != null)
            {
                barrio2 = domicilio2.Barrio.Nombre;

            }
            var departamento2 = " - ";
            if (domicilio2.Departamento != null)
            {
                departamento2 = domicilio2.Departamento.Nombre;

            }
            var localidad2 = " - ";
            if (domicilio2.Localidad != null)
            {
                localidad2 = domicilio2.Localidad.Nombre;

            }
            var calle2 = " - ";
            if (domicilio2.Calle != null)
            {
                calle2 = domicilio2.Calle.Nombre;

            }
            reporte.AddParameter("parametro_Titulo_reporte", "Comprobante de Trámite - " + tramiteDto.NombreTipoTramite.ToUpper());
            reporte.AddParameter("nroTramiteSifcos", tramiteDto.NroTramiteSifcos);
            reporte.AddParameter("paramatro_dom1_departamento", departamento1);
            reporte.AddParameter("paramatro_dom1_localidad", localidad1);
            reporte.AddParameter("paramatro_dom1_barrio", barrio1);
            reporte.AddParameter("paramatro_dom1_calle", calle1);
            reporte.AddParameter("paramatro_dom1_nroCalle", domicilio1.Altura);
            reporte.AddParameter("paramatro_dom1_dpto", domicilio1.Dpto);
            reporte.AddParameter("paramatro_dom1_piso", domicilio1.Piso);
            reporte.AddParameter("paramatro_dom1_codPos", domicilio1.CodigoPostal);
            reporte.AddParameter("paramatro_dom1_oficina", tramiteDto.Oficina);
            reporte.AddParameter("paramatro_dom1_local", tramiteDto.Local);
            reporte.AddParameter("paramatro_dom1_stand", tramiteDto.Stand);
            reporte.AddParameter("paramatro_contacto_email", !string.IsNullOrEmpty(tramiteDto.EmailEstablecimiento) ? tramiteDto.EmailEstablecimiento : " - ");
            reporte.AddParameter("paramatro_contacto_facebook", !string.IsNullOrEmpty(tramiteDto.Facebook) ? tramiteDto.Facebook : " - ");
            reporte.AddParameter("paramatro_contacto_WebPage", !string.IsNullOrEmpty(tramiteDto.WebPage) ? tramiteDto.WebPage : " - ");
            reporte.AddParameter("paramatro_contacto_celular", !string.IsNullOrEmpty(tramiteDto.Celular) ? "(" + tramiteDto.CodAreaCelular + ") " + tramiteDto.Celular : " - ");
            reporte.AddParameter("paramatro_contacto_telFijo", !string.IsNullOrEmpty(tramiteDto.TelFijo) ? "(" + tramiteDto.CodAreaTelFijo + ") " + tramiteDto.TelFijo : " - ");
            reporte.AddParameter("paramatro_dom2_departamento", departamento2);
            reporte.AddParameter("paramatro_dom2_localidad", localidad2);
            reporte.AddParameter("paramatro_dom2_barrio", barrio2);
            reporte.AddParameter("paramatro_dom2_calle", calle2);
            reporte.AddParameter("paramatro_dom2_nroCalle", domicilio2.Altura);
            reporte.AddParameter("paramatro_dom2_dpto", domicilio2.Dpto);
            reporte.AddParameter("paramatro_dom2_piso", domicilio2.Piso);
            reporte.AddParameter("paramatro_dom2_codPos", domicilio2.CodigoPostal);
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
            reporte.AddParameter("parametro_responsable_dni", tramiteDto.DniRepLegal);
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

        protected void btnVerSucursales_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCuit.Text))
            {
                CargarDirecciones(txtCuit.Text);
            }
            else
            {
                CargarDirecciones(ddlSeleccionEntidad.SelectedItem.Text);
            }

            MostrarOcultarModalVerSucursales(true);

        }

        protected void btnCerrarSucursales_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalVerSucursales(false);
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

        protected void ddlSeleccionEntidad_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlSeleccionEntidad.SelectedValue)
            {
                case "1"://CUIT EMPRESA
                    txtCuit.Text = (string)Session["CuitPermiso"];
                    divRepresentanteLegal.Visible = false;
                    break;
                case "2"://CUIT PERSONA FISICA
                    txtCuit.Text = master.UsuarioCidiLogueado.CUIL;
                    break;

            }
        }
        protected void btnDescargarComprobante_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ReporteGeneral.aspx");
        }
        public bool IsMailValid(string emailaddress)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        public Bitmap GeneraCodigoQR(string TextoCodificar)
        {
            //Instancia del objeto encargado de codificar el codigo QR
            QRCodeEncoder CodigoQR = new QRCodeEncoder();

            //configuracion de las propiedades del objeto.
            CodigoQR.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            CodigoQR.QRCodeScale = 4;
            CodigoQR.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
            CodigoQR.QRCodeVersion = 0;
            CodigoQR.QRCodeBackgroundColor = System.Drawing.Color.White;
            CodigoQR.QRCodeForegroundColor = System.Drawing.Color.Black;



            //Se retorna el Codigo QR codificado en formato de arreglo de bytes.
            return CodigoQR.Encode(TextoCodificar, System.Text.Encoding.UTF8);
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            //Se crea un buffer de lectura
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //Se guarda la imagen que es enviada como parametro a traves de la funcion Save del componente de imagen en el buffer de lectura
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            //Se retorna la imagen en formato de arreglo de bytes
            return ms.ToArray();
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }



        //protected void btnImprimirReportePrueba_OnClick(object sender, EventArgs e)
        //{
        //    NroTramiteAImprimir = 320;
        //    ImprimirReporteTramite();
        //}

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


            RequestPost.N_Documento = "Documentación_" + ObjetoInscripcion.CUIT; //documento.PostedFile.FileName;
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

        #region api nueva de domicilio

        public string UrlDomLegal
        {
            get { return (string)Session["UrlDomLegal"]; }
            set { Session["UrlDomLegal"] = value; }
        }

        public string UrlDomComercio
        {
            get { return (string)Session["UrlDomComercio"]; }
            set { Session["UrlDomComercio"] = value; }
        }

        public int idVinLegal
        {
            get { return (int)Session["IdVinLegal"]; }
            set { Session["IdVinLegal"] = value; }
        }
        public int idVinComercio
        {
            get { return (int)Session["idVinComercio"]; }
            set { Session["idVinComercio"] = value; }
        }

        public AppComunicacion.ApiModels.Domicilio DomComercio
        {
            get { return (AppComunicacion.ApiModels.Domicilio)Session["DomComercio"]; }
            set { Session["DomComercio"] = value; }
        }
        public AppComunicacion.ApiModels.Domicilio DomLegal
        {
            get { return (AppComunicacion.ApiModels.Domicilio)Session["DomLegal"]; }
            set { Session["DomLegal"] = value; }
        }

        protected void btnModificarDomComercio_OnClick(object sender, EventArgs e)
        {
            AltaDomComercio();
        }

        protected void btnModificarDomLegal_OnClick(object sender, EventArgs e)
        {
            AltaDomLegal();
        }

        protected void AltaDomLegal()
        {
            HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
            var Requestwrapper = new HttpRequestWrapper(Request);
            UrlDomLegal = Helper.AltaDomiciliolegal(sessionBase, Requestwrapper, "SIFLEG" + ObjetoInscripcion.CUIT);

        }
        protected void AltaDomComercio()
        {
            HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
            var Requestwrapper = new HttpRequestWrapper(Request);
            UrlDomComercio = Helper.AltaDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);

        }

        protected void InicializarIframeDomEstab()
        {
            try
            {
                //iniciar el modulo de direccion legal
                HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
                var Requestwrapper = new HttpRequestWrapper(Request);

                //iniciar el modulo de direccion del establecimiento
                //sessionBase = new HttpSessionStateWrapper(Page.Session);
                //Requestwrapper = new HttpRequestWrapper(Request);

                if (ObjetoInscripcion.DomComercio.ID_ENTIDAD != 0)
                {
                    UrlDomComercio = Helper.getURLDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + ObjetoInscripcion.NroSifcos);

                    DomComercio = Helper.getDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + ObjetoInscripcion.NroSifcos);
                    if (DomComercio != null)
                    {
                        if (DomComercio.IdVin != 0)
                        {
                            ObjetoInscripcion.DomComercio.idVin = DomComercio.IdVin;
                            ObjetoInscripcion.DomComercio.LATITUD = DomComercio.Latitud;
                            ObjetoInscripcion.DomComercio.LONGITUD = DomComercio.Longitud;
                            ObjetoInscripcion.DomComercio.BARRIO = DomComercio.Barrio.Nombre;
                            ObjetoInscripcion.DomComercio.DEPARTAMENTO = DomComercio.Departamento.Nombre;
                            ObjetoInscripcion.DomComercio.CALLE = DomComercio.Calle.Nombre;
                            ObjetoInscripcion.DomComercio.ALTURA = DomComercio.Altura;
                            ObjetoInscripcion.DomComercio.CP = DomComercio.CodigoPostal;
                            ObjetoInscripcion.DomComercio.DEPTO = DomComercio.Dpto;
                            ObjetoInscripcion.DomComercio.LOCALIDAD = DomComercio.Localidad.Nombre;
                            ObjetoInscripcion.DomComercio.TORRE = DomComercio.Torre;
                            ObjetoInscripcion.DomComercio.PISO = DomComercio.Piso;
                        }
                    }


                    idVinComercio = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + ObjetoInscripcion.NroSifcos);
                    if (idVinComercio == 0)
                    {
                        sessionBase = new HttpSessionStateWrapper(Page.Session);
                        Requestwrapper = new HttpRequestWrapper(Request);
                        UrlDomComercio = Helper.AltaDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + ObjetoInscripcion.NroSifcos);
                    }

                }

            }
            catch (Exception e)
            {
                divMensajeError.Visible = true;
                var dom = DomLegal == null ? "DomLegal es nulo." : "DomLegal no es nulo : id_vin --> " + DomLegal.IdVin;
                var dom2 = DomComercio == null ? "DomEstab es nulo." : "DomEstab no es nulo : id_vin --> " + DomComercio.IdVin;
                lblMensajeError.Text = "Error en el método iniciarModuloDirecciones(). " + dom + " . " + dom2 + " . Detalle de la Excepción: " + e.Message;

            }

        }
        private void iniciarModuloDirecciones()
        {
            try
            {
                //iniciar el modulo de direccion legal
                HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
                var Requestwrapper = new HttpRequestWrapper(Request);


                //if (UrlDomComercio == null && UrlDomLegal == null)
                //{
                //    UrlDomComercio = Models.Helper.getURLDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);
                //    UrlDomLegal = Models.Helper.getURLDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + "DOMLEGAL");
                //    return;
                //}
                //- sino salio antes es pq es una entidad valida


                idVinLegal = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIFLEG" + ObjetoInscripcion.CUIT);
                if (idVinLegal == 0)
                {
                    sessionBase = new HttpSessionStateWrapper(Page.Session);
                    Requestwrapper = new HttpRequestWrapper(Request);
                    UrlDomLegal = Helper.AltaDomiciliolegal(sessionBase, Requestwrapper, "SIFLEG" + ObjetoInscripcion.CUIT);
                }
                else
                {
                    UrlDomLegal = Helper.getURLDomicilio(sessionBase, Requestwrapper, "SIFLEG" + ObjetoInscripcion.CUIT);
                }



                DomLegal = Helper.getDomicilio(sessionBase, Requestwrapper, "SIFLEG" + ObjetoInscripcion.CUIT);
                if (DomLegal != null)
                {
                    if (DomLegal.IdVin != 0)
                    {
                        ObjetoInscripcion.IdVinDomLegal = DomLegal.IdVin;
                        ObjetoInscripcion.Latitud = DomLegal.Latitud;
                        ObjetoInscripcion.Longitud = DomLegal.Longitud;
                    }
                }


                //iniciar el modulo de direccion del establecimiento
                sessionBase = new HttpSessionStateWrapper(Page.Session);
                Requestwrapper = new HttpRequestWrapper(Request);


                idVinComercio = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + ObjetoInscripcion.NroSifcos);


                if (idVinComercio == 0)
                {
                    sessionBase = new HttpSessionStateWrapper(Page.Session);
                    Requestwrapper = new HttpRequestWrapper(Request);
                    UrlDomComercio = Helper.AltaDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + ObjetoInscripcion.NroSifcos);
                }
                else
                {
                    UrlDomComercio = Helper.getURLDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + ObjetoInscripcion.NroSifcos);
                }

                DomComercio = Helper.getDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT + ObjetoInscripcion.NroSifcos);
                if (DomComercio != null)
                {
                    if (DomComercio.IdVin != 0)
                    {
                        ObjetoInscripcion.DomComercio = new ComercioDto();
                        ObjetoInscripcion.DomComercio.idVin = DomComercio.IdVin;
                        ObjetoInscripcion.DomComercio.LATITUD = DomComercio.Latitud;
                        ObjetoInscripcion.DomComercio.LONGITUD = DomComercio.Longitud;
                    }
                }



            }
            catch (Exception e)
            {
                divMensajeError.Visible = true;
                ////var dom = DomLegal == null ? "DomLegal es nulo." : "DomLegal no es nulo : id_vin --> " + DomLegal.IdVin;
                ////var dom2 = DomEstab == null ? "DomEstab es nulo." : "DomEstab no es nulo : id_vin --> " + DomEstab.IdVin;
                lblMensajeError.Text = "Error en la comunicacion con BASE DE DATOS. Intente Mas tarde...";
                //btnComenzarTramite.Enabled = false;

            }

        }
        #endregion


       
    }
}