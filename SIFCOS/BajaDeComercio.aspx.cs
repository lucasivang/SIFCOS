using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using CryptoManagerV4._0.Clases;
using CryptoManagerV4._0.Excepciones;
using CryptoManagerV4._0.General;
using Newtonsoft.Json;
using DA_SIFCOS.Entities.CDDAutorizador;
using DA_SIFCOS.Entities.CDDPost;
using BL_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.CDDResponse;
using DA_SIFCOS.Entities.Excepcion;
using DA_SIFCOS.Entities.Errores;
using DA_SIFCOS.Utils;


namespace SIFCOS
{
    public partial class BajaDeComercio : System.Web.UI.Page
    {
		private Autorizador ObjAutorizador { get; set; }
		private CDDPost RequestPost { get; set; }

        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public Principal master;
        protected void Page_Load(object sender, EventArgs e)
        {

            master = (Principal)Page.Master;
            RolUsuario = master.RolUsuario;
            var CargarTramiteBaja = Request.QueryString["Exito"];
            if (CargarTramiteBaja == "1")
            {
                CompletarBajaDesdeReempadronamiento();
                return;
            }
            
            if (!Page.IsPostBack)
            {
                 
                LimpiarControles();
            }
        }

        public string RolUsuario
        {
            get
            {
                return Session["RolUsuario"] == null ? "" : (string)Session["RolUsuario"];
            }
            set
            {
                Session["RolUsuario"] = value;
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

        protected void btnConsultarEstado_OnClick(object sender, EventArgs e)
        { 
            /*
             Consulto si ese SIFCoS está al día, de lo contrario muestro la grilla con las tasas o reempadronamiento que debe.
             */
            divAcciones.Visible = true;
            divMensajeError.Visible = false;
            divMensajeExito.Visible = false;
            btnIrAReempadronar.Visible = false;

            if (string.IsNullOrEmpty(txtFechaCeseMunicipal.Text))
            {
                lblMensajeError.Text = "Debe ingresar Fecha Cese Municipalidad, es un campo obligatorio.";
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
                return;
            }
            if (string.IsNullOrEmpty(txtNroSifcos.Text.Trim()))
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "Debe ingresar un numero SIFCoS, es un campo obligatorio.";
                txtNroSifcos.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCuit.Text.Trim()))
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "Debe ingresar un CUIT, es un campo obligatorio.";
                txtCuit.Focus();
                return;
            }
            lblCuit.Text = txtCuit.Text;
            lblFechaCese.Text = txtFechaCeseMunicipal.Text;
            lblNroSifcos.Text = txtNroSifcos.Text;
            
            DivDatosEntidad.Visible = true;
            DivDatosIniciales.Visible = false;
            var cuit = txtCuit.Text.Trim();

            var existeBaja = Bl.BlGetTramitesDeBaja(txtNroSifcos.Text, cuit);
            if (existeBaja.Rows.Count > 0)
            {
                btnBajaComercio.Visible = false;
                divMensajeExito.Visible = false;
                divMensajeError.Visible = false;
                lblResultadoEstadoComercio.Text = "USTED YA REALIZO EL TRAMITE DE BAJA. EL NRO DE TRAMITE DE BAJA ES: " + existeBaja.Rows[0]["NRO_TRAMITE"].ToString();
                return;
            }

            /*Obtengo todos los tramites de un CUIT. Tanto del sifcos viejo como del nuevo.  El campo "ORIGEN": "SIFCOS_NUEVO" Ó "SIFCOS_VIEJO"*/
            var tramites = Bl.BlGetEntidadTramite(txtCuit.Text);
            if (tramites.Count == 0)
            {
                lblMensajeError.Text = "No se encontraron registros para el CUIT proporcionado.";
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
                return;
            }
            
			//-lt si hay algun tramite con el sifcos nuevo o viejo.busco el ultimo y le saco el id entidad,con un cero se que no existe la entidad porque viene del viejo

			var ultimotramiteconesenrosifcos = tramites.FindLast(x => x.Nro_Sifcos == txtNroSifcos.Text.Trim() && x.Origen == "SIFCOS_NUEVO");
	        if (ultimotramiteconesenrosifcos != null)
	        {
				Session["id_entidad_ultimo"] = ultimotramiteconesenrosifcos.idEntidad;
	        }
	        else
	        {
				Session["id_entidad_ultimo"] = 0;
	        }
			//-

            var existeSifcos = tramites.Any(x => x.Nro_Sifcos == txtNroSifcos.Text.Trim());
            if (!existeSifcos)
            {
                lblMensajeError.Text = "No se encontraron registros para el Nro SIFCoS proporcionado.";
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
                return;
            }
              
            var fechaVto = Bl.BlGetFechaUltimoTramiteSifcosNuevo(txtNroSifcos.Text.Trim());
            int aniosDebe;
            /*Si fechaVto devuelve null, noy hay tramites en el nuevo sifcos.*/
            if (fechaVto == null)
            {
                //Primero averiguo si es un responsable de boca para controlar quien puede realizar este caso particular de tramite
                if (RolUsuario == "Gestor")
                {
                    lblMensajeExito.Text =
                        "USTED NO PRESENTA TRAMITES EN EL NUEVO SISTEMA. LA BAJA DEBE SER REALIZADA POR UN RESPONSABLE DE BOCA DE RECEPCION.";
                    //btnBajaComercio.Visible = true;
                    divMensajeExito.Visible = true;
                    divMensajeError.Visible = false;
                    btnIrAReempadronar.Visible = false;
                    btnCompletarDatos.Visible = false;
                    btnBajaComercio.Visible = false;
                    return;
                }
                //enconces no tiene tramites en el sifcos nuevo.
                //busco el vto en el sifcos viejo.
                DateTime? fecha_presentacion = Bl.BlGetFechaUltimoTramiteSifcosViejo(txtNroSifcos.Text.Trim()).Value;// me va a traer la fecha de presentacion.

                fechaVto = fecha_presentacion.Value.AddYears(1);

                if (DateTime.Parse(txtFechaCeseMunicipal.Text) <= fechaVto.Value.Date)
                {
                    lblMensajeExito.Text =
                        "USTED SE ENCUENTRA AL DÍA. ESTÁ EN CONDICIONES DE REALIZAR LA BAJA DEL COMERCIO. PRIMERO DEBE CARGAR LOS DATOS EN EL SISTEMA NUEVO. HAGA CLICK EN EL BOTÓN 'COMPLETAR DATOS'.";
                    //btnBajaComercio.Visible = true;
                    divMensajeExito.Visible = true;
                    divMensajeError.Visible = false;
					
                    btnIrAReempadronar.Visible = false;
                    btnCompletarDatos.Visible = true;
                    return;
                }

                //entonces debe Reempadronarse los años que debe. de la forma normal.
                TimeSpan ts = DateTime.Parse(txtFechaCeseMunicipal.Text)- fechaVto.Value;
                 aniosDebe = ts.Days / 365;
               
                if (aniosDebe == 0)
                {
                    if (ts.Days >= 0 && ts.Days < 365)
                        aniosDebe = 1;
                }
                else
                {
                    aniosDebe = aniosDebe + 1;
                }


            }
            else
            {
                //entonces SI tiene tramites cargados en el sifcos nuevo.
                var fecVto = Bl.BlGetFechaUltimoTramiteSifcosNuevo(txtNroSifcos.Text.Trim());

                fechaVto = fecVto.Value.AddYears(-1);
                TimeSpan ts = DateTime.Parse(txtFechaCeseMunicipal.Text) - fechaVto.Value;
                aniosDebe = ts.Days / 365;
                // NO DEBE REEMPADRONARSE, ESTÁ AL DÍA.
                if (aniosDebe <= 0)
                {

                    lblMensajeExito.Text ="USTED SE ENCUENTRA AL DÍA. ESTÁ EN CONDICIONES DE REALIZAR LA BAJA DEL COMERCIO. Haga click en 'DAR DE BAJA AL COMERCIO'." ;
                        //"Próximo vencimiento : " + fecVto.Value.Day + "/" +
                        //                         fecVto.Value.Month + "/" + fecVto.Value.Year;
	                divCDD.Visible = true;
                    btnBajaComercio.Visible = true;
                    btnCompletarDatos.Visible = false;
                    btnIrAReempadronar.Visible = false;
                    divMensajeExito.Visible = true;
                    divMensajeError.Visible = false;
                    return;
                }

            }
            
            var trsPagasSinUsar = Bl.BlGetTasasPagadasSinUsar(cuit);
            var cantTasasPagasSinUsar = trsPagasSinUsar.Count;


            lblResultadoEstadoComercio.Text = "El comercio correspondiente al SIFCoS N° " + txtNroSifcos.Text.Trim() +
                                              " adeuda según la fecha cese municipal un total de : " + aniosDebe +
                                              " Reempadronamiento/s. ";
                
            
                
            /*GUARDO UNA LISTA CON LOS NROS DE TRS A IMPRIMIR*/
            var contadorTrsAUtilizar = 0;
            var listaAux = new List<Trs>();
            foreach (var trs in trsPagasSinUsar)
            {
                if (contadorTrsAUtilizar < aniosDebe)
                {
                    listaAux.Add(trs);//guardo en memoria las trs a utilizar .
                    contadorTrsAUtilizar++;
                }
            }
            
            /*VALIDO LA CANTIDAD DE TASAS DIPONIBLES A FAVOR SI SON SUFICIENTES SEGÚN LA CANTIDAD DE REEMP QUE DEBE.*/

            if (aniosDebe <= cantTasasPagasSinUsar)
            {
                // ya que tiene suficientes tasas pagadas sin usar y estaría al día.
                lblResultadoEstadoComercio.Text = lblResultadoEstadoComercio.Text + " NO DEBE IMPRIMIR TRS ya que " +
                   " disponde de " + cantTasasPagasSinUsar + " TRS pagadas. ESTÁ EN CONDICIONES DE REEMPADRONARSE. Luego se puede realizar la Baja del Comercio. Haga click en 'Ir a Reempadronar'" ;
                btnCompletarDatos.Visible = false;
                btnIrAReempadronar.Visible = true;
                return;
            }
            
            int cantTrsImprimir = (aniosDebe - cantTasasPagasSinUsar);

            //Autor IB:
            //Para evitar mensaje de tasas a imprimir con número negativo
            if (cantTrsImprimir < 0)
            {
                lblResultadoEstadoComercio.Text = lblResultadoEstadoComercio.Text + " No debe imprimir TRS.";
            }
            //

            lblResultadoEstadoComercio.Text = lblResultadoEstadoComercio.Text + " Debe imprimir " + cantTrsImprimir + " TRS ya que " +
                " disponde de " + cantTasasPagasSinUsar + " TRS pagadas.";
            


            /*GENERO LAS TASAS */
            int IdTipoTramiteTrs = 4;

            string strIdConcepto = SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;
            String fecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoReempadronamiento;
            string urlTrs = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;
            string oFechaVenc;
            string oHashTrx;
            string oIdTransaccion;
            string oNroLiqOriginal;
            string strConcpeto = "";
            string strMonto = "";


            //var idConcepto = SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;
            //var importeConcepto =  250;
            //var conceptoTasa = new ConceptoTasa { id_concepto = idConcepto, fec_desde = DateTime.Parse(fecDesdeConcepto) };
            //string nroTransaccion = null;
            string resultado = "";
            

            


            var lstAux = new List<Trs>();
            if (cantTrsImprimir > 0)
            {
                for (int i = 0; i < cantTrsImprimir; i++)
                {
                    //genero
                    /*
                    resultado = Bl.GenerarTransaccionTRS(cuit, master.UsuarioCidiLogueado.Id_Sexo,
                                master.UsuarioCidiLogueado.NroDocumento, "ARG",
                                master.UsuarioCidiLogueado.Id_Numero, Int64.Parse(conceptoTasa.id_concepto), conceptoTasa.fec_desde,
                                "057", 1, importeConcepto, "", DateTime.Now.Year.ToString(), out nroTransaccion);
                    */
                    resultado = Bl.BlSolicitarTrs(IdTipoTramiteTrs, cuit, master.UsuarioCidiLogueado.CUIL,
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
                        divMensajeExito.Visible = false;
                        return;
                    }
                }
            }
            else
            {
                lblMensajeExito.Text = "USTED SE ENCUENTRA AL DÍA. ESTÁ EN CONDICIONES DE REALIZAR LA BAJA DEL COMERCIO.";
                //"Próximo vencimiento : " + fecVto.Value.Day + "/" +
                //                         fecVto.Value.Month + "/" + fecVto.Value.Year;
	            divCDD.Visible = true;
                btnBajaComercio.Visible = true;
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;
                return;
            }

            gvResultado.DataSource = lstAux;
            gvResultado.DataBind();
            
              
        }

        protected void btnBajaComercioo_OnClick(object sender, EventArgs e)
        {
            if (IdDocumentoCDD1 == 0)
            {
                lblMensajeError.Text = "Debe adjuntar la documentacion del tramite de baja del comercio.";
                documento1.Focus();
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
                return;

            }
            if (IdDocumentoCDD2 == 0)
            {
                lblMensajeError.Text = "Debe adjuntar foto de oblea o denuncia policial de extravío.";
                documento2.Focus();
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
                return;

            }
            DateTime fechaCese =DateTime.Parse(lblFechaCese.Text);

			string resultado = Bl.BlRegistrarBajaComercio(lblNroSifcos.Text.Trim(), master.UsuarioCidiLogueado.CUIL, fechaCese, IdDocumentoCDD1, IdDocumentoCDD2);
            if (resultado == "OK")
            {
                LimpiarControles();
	            divCDD.Visible = false;
                lblMensajeExito.Text = "SE REALIZÓ LA BAJA CON ÉXITO.";
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;
            }
            else
            {
                lblMensajeError.Text = "Ocurrió un problema al Registrar la Baja. Compruebe los datos ingresados.";
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;

            }
           
        }
		
        protected void btnLimpiar_OnClick(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void LimpiarControles()
        {
            txtCuit.Text = "";
            txtNroSifcos.Text = "";
            txtFechaCeseMunicipal.Text = "";
            lblResultadoEstadoComercio.Text = "";

            divAcciones.Visible = false;
            btnBajaComercio.Visible = false;
            divMensajeExito.Visible = false;
            divMensajeError.Visible = false;

            gvResultado.DataSource = null;
            gvResultado.DataBind();

            txtFechaCeseMunicipal.Focus();
            DivDatosEntidad.Visible = false;
            DivDatosIniciales.Visible = true;
        }

		protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Inscripcion.aspx");
        }

        protected void btnIrAReempadronar_OnClick(object sender, EventArgs e)
        {
            TipoTramite = "BAJA";
            Session["VENGO_DESDE_BAJA_CUIT"] = txtCuit.Text.Trim();
            Session["VENGO_DESDE_BAJA_NRO_SIFCOS"] = txtNroSifcos.Text.Trim();
            Session["VENGO_DESDE_BAJA_FECHA_CESE"] = txtFechaCeseMunicipal.Text.Trim();
            Response.Redirect("Inscripcion.aspx");
            
        }

        protected void btnCompletarDatos_OnClick(object sender, EventArgs e)
        {
            Session["VENGO_DESDE_BAJA"] = true;
            Session["VENGO_DESDE_BAJA_CUIT"] = txtCuit.Text.Trim();
            Session["VENGO_DESDE_BAJA_NRO_SIFCOS"] = txtNroSifcos.Text.Trim();
            Session["VENGO_DESDE_BAJA_FECHA_CESE"] = txtFechaCeseMunicipal.Text.Trim();
            Response.Redirect("CargaTramiteBaja.aspx");
        }

        protected void CompletarBajaDesdeReempadronamiento()
        {
            if (Session["VENGO_DESDE_REEMPA_FECHA_CESE"]!=null)
            {
                DivDatosIniciales.Visible = false;
                lblFechaCese.Text = (String)Session["VENGO_DESDE_REEMPA_FECHA_CESE"];
                lblNroSifcos.Text = (string)Session["VENGO_DESDE_REEMPA_NRO_SIFCOS"];
                lblCuit.Text = (string)Session["VENGO_DESDE_REEMPA_CUIT"];
                DivDatosEntidad.Visible = true;
                btnBajaComercio.Visible = true;
            }
            
            Session["VENGO_DESDE_REEMPA_FECHA_CESE"] = null;
            Session["VENGO_DESDE_REEMPA_NRO_SIFCOS"]= null;
            Session["VENGO_DESDE_REEMPA_CUIT"]= null;
            
            btnConsultarEstado.Enabled = false;
            divMensajeError.Visible = false;
            divMensajeExito.Visible = false;
            btnIrAReempadronar.Visible = false;
            var existeBaja = Bl.BlGetTramitesDeBaja(lblNroSifcos.Text, lblCuit.Text);
            if (existeBaja.Rows.Count > 0)
            {
                btnBajaComercio.Visible = false;
                divMensajeExito.Visible = false;
                divMensajeError.Visible = false;
                lblResultadoEstadoComercio.Text = "USTED YA REALIZO EL TRAMITE DE BAJA. EL NRO DE TRAMITE DE BAJA ES: " + existeBaja.Rows[0]["NRO_TRAMITE"].ToString();
                return;
            }

            /*Obtengo todos los tramites de un CUIT. Tanto del sifcos viejo como del nuevo.  El campo "ORIGEN": "SIFCOS_NUEVO" Ó "SIFCOS_VIEJO"*/
            var tramites = Bl.BlGetEntidadTramite(lblCuit.Text);
            if (tramites.Count == 0)
            {
                lblMensajeError.Text = "No se encontraron registros para el CUIT proporcionado.";
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
                return;
            }

            //-lt si hay algun tramite con el sifcos nuevo o viejo.busco el ultimo y le saco el id entidad,con un cero se que no existe la entidad porque viene del viejo

            var ultimotramiteconesenrosifcos = tramites.FindLast(x => x.Nro_Sifcos == lblNroSifcos.Text.Trim() && x.Origen == "SIFCOS_NUEVO");
            if (ultimotramiteconesenrosifcos != null)
            {
                Session["id_entidad_ultimo"] = ultimotramiteconesenrosifcos.idEntidad;
            }
            else
            {
                Session["id_entidad_ultimo"] = 0;
            }
            //-

            var existeSifcos = tramites.Any(x => x.Nro_Sifcos == lblNroSifcos.Text.Trim());
            if (!existeSifcos)
            {
                lblMensajeError.Text = "No se encontraron registros para el Nro SIFCoS proporcionado.";
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
                return;
            }

            var fechaVto = Bl.BlGetFechaUltimoTramiteSifcosNuevo(lblNroSifcos.Text.Trim());
            int aniosDebe;
            /*Si fechaVto devuelve null, noy hay tramites en el nuevo sifcos.*/
            if (fechaVto == null)
            {
                //Primero averiguo si es un responsable de boca para controlar quien puede realizar este caso particular de tramite
                if (RolUsuario == "Gestor")
                {
                    lblMensajeExito.Text =
                        "USTED NO PRESENTA TRAMITES EN EL NUEVO SISTEMA. LA BAJA DEBE SER REALIZADA POR UN RESPONSABLE DE BOCA DE RECEPCION.";
                    //btnBajaComercio.Visible = true;
                    divMensajeExito.Visible = true;
                    divMensajeError.Visible = false;
                    btnIrAReempadronar.Visible = false;
                    btnCompletarDatos.Visible = false;
                    btnBajaComercio.Visible = false;
                    return;
                }
                //enconces no tiene tramites en el sifcos nuevo.
                //busco el vto en el sifcos viejo.
                DateTime? fecha_presentacion = Bl.BlGetFechaUltimoTramiteSifcosViejo(lblNroSifcos.Text.Trim()).Value;// me va a traer la fecha de presentacion.

                fechaVto = fecha_presentacion.Value.AddYears(1);

                if (DateTime.Parse(lblFechaCese.Text) <= fechaVto.Value.Date)
                {
                    lblMensajeExito.Text =
                        "USTED SE ENCUENTRA AL DÍA. ESTÁ EN CONDICIONES DE REALIZAR LA BAJA DEL COMERCIO. PRIMERO DEBE CARGAR LOS DATOS EN EL SISTEMA NUEVO. HAGA CLICK EN EL BOTÓN 'COMPLETAR DATOS'.";
                    //btnBajaComercio.Visible = true;
                    divMensajeExito.Visible = true;
                    divMensajeError.Visible = false;

                    btnIrAReempadronar.Visible = false;
                    btnCompletarDatos.Visible = true;
                    return;
                }

                //entonces debe Reempadronarse los años que debe. de la forma normal.
                TimeSpan ts = DateTime.Parse(lblFechaCese.Text) - fechaVto.Value;
                aniosDebe = ts.Days / 365;

                if (aniosDebe == 0)
                {
                    if (ts.Days >= 0 && ts.Days < 365)
                        aniosDebe = 1;
                }
                else
                {
                    aniosDebe = aniosDebe + 1;
                }


            }
            else
            {
                //entonces SI tiene tramites cargados en el sifcos nuevo.
                var fecVto = Bl.BlGetFechaUltimoTramiteSifcosNuevo(lblNroSifcos.Text.Trim());

                fechaVto = fecVto.Value.AddYears(-1);
                TimeSpan ts = DateTime.Parse(lblFechaCese.Text) - fechaVto.Value;
                aniosDebe = ts.Days / 365;
                // NO DEBE REEMPADRONARSE, ESTÁ AL DÍA.
                if (aniosDebe <= 0)
                {

                    lblMensajeExito.Text = "USTED SE ENCUENTRA AL DÍA. ESTÁ EN CONDICIONES DE REALIZAR LA BAJA DEL COMERCIO. Haga click en 'DAR DE BAJA AL COMERCIO'.";
                    //"Próximo vencimiento : " + fecVto.Value.Day + "/" +
                    //                         fecVto.Value.Month + "/" + fecVto.Value.Year;
                    divCDD.Visible = true;
                    btnBajaComercio.Visible = true;
                    btnCompletarDatos.Visible = false;
                    btnIrAReempadronar.Visible = false;
                    divMensajeExito.Visible = true;
                    divMensajeError.Visible = false;
                    return;
                }

            }

            var trsPagasSinUsar = Bl.BlGetTasasPagadasSinUsar(lblCuit.Text);
            var cantTasasPagasSinUsar = trsPagasSinUsar.Count;


            lblResultadoEstadoComercio.Text = "El comercio correspondiente al SIFCoS N° " + lblNroSifcos.Text.Trim() +
                                              " adeuda según la fecha cese municipal un total de : " + aniosDebe +
                                              " Reempadronamiento/s. ";



            /*GUARDO UNA LISTA CON LOS NROS DE TRS A IMPRIMIR*/
            var contadorTrsAUtilizar = 0;
            var listaAux = new List<Trs>();
            foreach (var trs in trsPagasSinUsar)
            {
                if (contadorTrsAUtilizar < aniosDebe)
                {
                    listaAux.Add(trs);//guardo en memoria las trs a utilizar .
                    contadorTrsAUtilizar++;
                }
            }

            /*VALIDO LA CANTIDAD DE TASAS DIPONIBLES A FAVOR SI SON SUFICIENTES SEGÚN LA CANTIDAD DE REEMP QUE DEBE.*/

            if (aniosDebe <= cantTasasPagasSinUsar)
            {
                // ya que tiene suficientes tasas pagadas sin usar y estaría al día.
                lblResultadoEstadoComercio.Text = lblResultadoEstadoComercio.Text + " NO DEBE IMPRIMIR TRS ya que " +
                   " disponde de " + cantTasasPagasSinUsar + " TRS pagadas. ESTÁ EN CONDICIONES DE REEMPADRONARSE. Luego se puede realizar la Baja del Comercio. Haga click en 'Ir a Reempadronar'";
                btnCompletarDatos.Visible = false;
                btnIrAReempadronar.Visible = true;
                return;
            }

            int cantTrsImprimir = (aniosDebe - cantTasasPagasSinUsar);

            //Autor IB:
            //Para evitar mensaje de tasas a imprimir con número negativo
            if (cantTrsImprimir < 0)
            {
                lblResultadoEstadoComercio.Text = lblResultadoEstadoComercio.Text + " No debe imprimir TRS.";
            }
            //

            lblResultadoEstadoComercio.Text = lblResultadoEstadoComercio.Text + " Debe imprimir " + cantTrsImprimir + " TRS ya que " +
                " disponde de " + cantTasasPagasSinUsar + " TRS pagadas.";



            /*GENERO LAS TASAS */
            int IdTipoTramiteTrs = 4;

            string strIdConcepto = SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;
            String fecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoReempadronamiento;
            string urlTrs = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;
            string oFechaVenc;
            string oHashTrx;
            string oIdTransaccion;
            string oNroLiqOriginal;
            string strConcpeto = "";
            string strMonto = "";


            //var idConcepto = SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;
            //var importeConcepto =  250;
            //var conceptoTasa = new ConceptoTasa { id_concepto = idConcepto, fec_desde = DateTime.Parse(fecDesdeConcepto) };
            //string nroTransaccion = null;
            string resultado = "";





            var lstAux = new List<Trs>();
            if (cantTrsImprimir > 0)
            {
                for (int i = 0; i < cantTrsImprimir; i++)
                {
                    //genero
                    /*
                    resultado = Bl.GenerarTransaccionTRS(cuit, master.UsuarioCidiLogueado.Id_Sexo,
                                master.UsuarioCidiLogueado.NroDocumento, "ARG",
                                master.UsuarioCidiLogueado.Id_Numero, Int64.Parse(conceptoTasa.id_concepto), conceptoTasa.fec_desde,
                                "057", 1, importeConcepto, "", DateTime.Now.Year.ToString(), out nroTransaccion);
                    */
                    resultado = Bl.BlSolicitarTrs(IdTipoTramiteTrs, txtCuit.Text, master.UsuarioCidiLogueado.CUIL,
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
                        divMensajeExito.Visible = false;
                        return;
                    }
                }
            }
            else
            {
                lblMensajeExito.Text = "USTED SE ENCUENTRA AL DÍA. ESTÁ EN CONDICIONES DE REALIZAR LA BAJA DEL COMERCIO.";
                //"Próximo vencimiento : " + fecVto.Value.Day + "/" +
                //                         fecVto.Value.Month + "/" + fecVto.Value.Year;
                divCDD.Visible = true;
                btnBajaComercio.Visible = true;
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;
                return;
            }

            gvResultado.DataSource = lstAux;
            gvResultado.DataBind();
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
					lblMensajeErrorDocumentacion_1.Text = "El tamaño máximo permitido por archivo es de 4 MB";
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