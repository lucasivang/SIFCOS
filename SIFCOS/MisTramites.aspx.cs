using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using BL_SIFCOS;
using BotonUnClick;
using CryptoManagerV4._0.Clases;
using CryptoManagerV4._0.General;
using DA_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.CDDAutorizador;
using DA_SIFCOS.Entities.CDDPost;
using DA_SIFCOS.Entities.CDDResponse;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Entities.Errores;
using DA_SIFCOS.Entities.Excepcion;
using DA_SIFCOS.Utils;

using iTextSharp.text;
using iTextSharp.text.pdf;


namespace SIFCOS
{
    public partial class MisTramites : System.Web.UI.Page
    {
        private const String Key_Cif_Decif = "Warrior2025";
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        protected DataTable DtConsulta = new DataTable();

        protected static ReglaDeNegocios Bl = new ReglaDeNegocios();
        public Principal master;
		private CryptoDiffieHellman ObjDiffieHellman { get; set; }
		private Autorizador ObjAutorizador { get; set; }
		private CDDPost RequestPost { get; set; }
		public CngKey Private_Key { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            var mostrarMensajeExitoFinTramite = Request.QueryString["Exito"];
            if (mostrarMensajeExitoFinTramite == "1")
            {
                divMensajeFinalizacionTramiteSifcosExito.Visible = true;
                mostrarMensajeExitoFinTramite = "0";
                if (Session["NroTransaccionParaImprimir"] != null)
                    divBotonDescargarTRS.Visible = Session["NroTransaccionParaImprimir"].ToString() != "noImprimir";

                var tipo_reimpresion = (string)Session["VENGO_DESDE_REIMPRESION_TIPO_TRAMITE_REIMPRESION"];
                divBotonDescargarTRS.Visible = tipo_reimpresion == "TRS_PAGA";

            }
            else
            {
                divMensajeFinalizacionTramiteSifcosExito.Visible = false;
            }
            master = (Principal)Page.Master;
            var lstRolesNoAutorizados = new List<string>();
            //lstRolesNoAutorizados.Add("Administrador General");
            //lstRolesNoAutorizados.Add("Boca de Recepcion");
            lstRolesNoAutorizados.Add("Boca de Ministerio");
            lstRolesNoAutorizados.Add("Secretaria de comercio");
            //lstRolesNoAutorizados.Add("Gestor");//usuario comun;
            //lstRolesNoAutorizados.Add("Sin Asignar");

            if (lstRolesNoAutorizados.Contains(master.RolUsuario))
            {
                Response.Redirect("Inscripcion.aspx");
            }

			tramiteDto = new InscripcionSifcosDto();

            if (!IsPostBack)
            {
                divMensajeError.Visible = false;
                divMensajeExito.Visible = false;
                //divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;
                lblInfoGeneral.Text = "";
                divInformacion.Visible = false;
				Consultar();
            }
        }

        private int EntidadSeleccionada
        {
            get
            {
                return (int) Session["EntidadSeleccionada"];
            }
            set
            {
                Session["EntidadSeleccionada"] = value;
            }
        }

        public Usuario UsuarioCidiLogueado
        {
            get
            {
                return Session["UsuarioCiDiLogueado"] == null ? new Usuario() : (Usuario)Session["UsuarioCiDiLogueado"];

            }
            set
            {
                Session["UsuarioCiDiLogueado"] = value;
            }
        }

        private EstadoAbmcEnum EstadoVista
        {
            get
            {
                return (EstadoAbmcEnum) Session["EstadoVista"];
            }
            set
            {
                Session["EstadoVista"] = value;
            }
        }

		private int IdDocumentoCDD
		{
			get { return (int)Session["IdDocumentoCDD"]; }
			set { Session["IdDocumentoCDD"] = value; }
		}

		protected InscripcionSifcosDto tramiteDto
		{
			get
			{
				return Session["tramiteDto"] == null ? null : (InscripcionSifcosDto)Session["tramiteDto"];
			}
			set
			{
				Session["tramiteDto"] = value;
			}
		}

		private void MostrarOcultarAdjuntarDocumentacionHab(bool mostrar)
		{

			if (mostrar)
			{
				var classname = "mostrarModal";
				string[] listaStrings = divModalAdjuntarDocumentacionHab.Attributes["class"].Split(' ');
				var listaStr = String.Join(" ", listaStrings
					.Except(new string[] { "", classname })
					.Concat(new string[] { classname })
					.ToArray()
				);
				divModalAdjuntarDocumentacionHab.Attributes.Add("class", listaStr);
			}
			else
			{
				//oculta la Modal 
				divModalAdjuntarDocumentacionHab.Attributes.Add("class", String.Join(" ", divModalAdjuntarDocumentacionHab
					.Attributes["class"]
					.Split(' ')
					.Except(new string[] { "", "mostrarModal" })
					.ToArray()
				));
			}
		}

		private void MostrarOcultarAdjuntarDocumentacionAFIP(bool mostrar)
		{

			if (mostrar)
			{
				var classname = "mostrarModal";
				string[] listaStrings = divModalAdjuntarDocumentacionAFIP.Attributes["class"].Split(' ');
				var listaStr = String.Join(" ", listaStrings
					.Except(new string[] { "", classname })
					.Concat(new string[] { classname })
					.ToArray()
				);
				divModalAdjuntarDocumentacionAFIP.Attributes.Add("class", listaStr);
			}
			else
			{
				//oculta la Modal 
				divModalAdjuntarDocumentacionAFIP.Attributes.Add("class", String.Join(" ", divModalAdjuntarDocumentacionAFIP
					.Attributes["class"]
					.Split(' ')
					.Except(new string[] { "", "mostrarModal" })
					.ToArray()
				));
			}
		}

		protected void btnCerrarVerDocHab_OnClick(object sender, EventArgs e)
		{
			MostrarOcultarAdjuntarDocumentacionHab(false);
		}
		protected void btnCerrarVerDocAFIP_OnClick(object sender, EventArgs e)
		{
			MostrarOcultarAdjuntarDocumentacionAFIP(false);
		}

		protected void btnAdjuntarDoc_Click(object sender, EventArgs e)
		{
			MostrarOcultarAdjuntarDocumentacionHab(true);
			divMensajeErrorDocumentacionHab.Visible = false;
			divMensajeExitoDocumentacionHab.Visible = false;
		}

		protected void BtnAdjuntarOtro1_Click(object sender, EventArgs e)
		{
			documento1 = null;
			IdDocumentoCDD1 = new int();
		}

		protected void BtnAdjuntarOtro2_Click(object sender, EventArgs e)
		{
			documento2 = null;
			IdDocumentoCDD2 = new int();
		}

		protected void BtnAdjuntar1_Click(object sender, EventArgs e)
		{

			if (documento1.HasFile)
			{
				var tamanoarchivo = documento1.FileContent.Length;
				if (tamanoarchivo > 4194304)
				{
					divMensajeErrorDocumentacionAFIP.Visible = true;
					lblMensajeErrorDocumentacionAFIP.Text = "El tamaño maximo permitido por archivo es de 4 MB";
					return;
				}
			}
			else
			{
				divMensajeErrorDocumentacionAFIP.Visible = true;
				lblMensajeErrorDocumentacionAFIP.Text = "No Hay un Archivo seleccionado.";
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
						}
						else
						{
							divMensajeErrorDocumentacionHab.Visible = true;
							lblMensajeErrorDocumentacionHab.Text = "Adjuntado erróneo. Codigo Error: " + _response.Codigo_Resultado +
												   " Descripcion: " + _response.Detalle_Resultado;
						}
					}
				}
				else
				{
					divMensajeErrorDocumentacionHab.Visible = true;
					lblMensajeErrorDocumentacionHab.Text = "Autorización denegada desde la Web APi CDD. Codigo Error: " +
										   _respLogin.Codigo_Resultado +
										   " Descripcion: " + _respLogin.Detalle_Resultado;
				}
			}
			catch (CDDException waEx)
			{
				divMensajeErrorDocumentacionHab.Visible = true;
				lblMensajeErrorDocumentacionHab.Text = "Codigo Error: " + waEx.ErrorCode +
									   " Descripcion: " + waEx.ErrorDescription;
			}


		}
		protected void BtnAdjuntar2_Click(object sender, EventArgs e)
		{

			if (documento2.HasFile)
			{
				var tamanoarchivo = documento2.FileContent.Length;
				if (tamanoarchivo > 4194304)
				{
					divMensajeErrorDocumentacionAFIP.Visible = true;
					lblMensajeErrorDocumentacionAFIP.Text = "El tamaño maximo permitido por archivo es de 4 MB";
					return;
				}
			}
			else
			{
				divMensajeErrorDocumentacionAFIP.Visible = true;
				lblMensajeErrorDocumentacionAFIP.Text = "No Hay un Archivo seleccionado.";
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
						}
						else
						{
							divMensajeErrorDocumentacionAFIP.Visible = true;
							lblMensajeErrorDocumentacionAFIP.Text = "Adjuntado erróneo. Codigo Error: " + _response.Codigo_Resultado +
												   " Descripcion: " + _response.Detalle_Resultado;
						}
					}
				}
				else
				{
					divMensajeErrorDocumentacionAFIP.Visible = true;
					lblMensajeErrorDocumentacionAFIP.Text = "Autorización denegada desde la Web APi CDD. Codigo Error: " +
										   _respLogin.Codigo_Resultado +
										   " Descripcion: " + _respLogin.Detalle_Resultado;
				}
			}
			catch (CDDException waEx)
			{
				divMensajeErrorDocumentacionAFIP.Visible = true;
				lblMensajeErrorDocumentacionAFIP.Text = "Codigo Error: " + waEx.ErrorCode +
									   " Descripcion: " + waEx.ErrorDescription;
			}


		}
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

		internal CDDResponseLogin Get_Authorize_Web_Api_CDD_insert()
		{
			var _cddRespLogin = new CDDResponseLogin();
			var _cddMapError = new CDDMapError();

			try
			{
				Initialize_Authorize_insert();

				_cddRespLogin =
					MapeadorWebApi.ConsumirWebApi<Autorizador, CDDResponseLogin>(MapeadorWebApi.Autorizar_Solicitud,
						ObjAutorizador);

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
												  ex.StackTrace;
			}
			catch (WebException ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: WebException." +
												  ex.StackTrace;
			}
			catch (Exception ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: Exception." +
												  ex.StackTrace;
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

		private void Initialize_Authorize_insert()
		{
			ObjDiffieHellman = new CryptoDiffieHellman();
			ObjAutorizador = new Autorizador();

			var _helper = new Helpers();
			var _cmd = new CryptoManagerData();

			String _mensaje = String.Empty;

			try
			{
				
				ObjAutorizador.Id_Aplicacion_Origen =
					Convert.ToInt32(ConfigurationManager.AppSettings["CiDiIdAplicacion"].ToString());
				ObjAutorizador.Pwd_Aplicacion = _helper.Encriptar_Password(MapeadorWebApi.Pwd_Aplicacion_Origen);
				ObjAutorizador.Key_Aplicacion = MapeadorWebApi.Key_Aplicacion_Origen;
				ObjAutorizador.Operacion = "3"; // 3 = Insercion
				ObjAutorizador.Id_Usuario = MapeadorWebApi.User_Aplicacion_Origen;
				ObjAutorizador.Time_Stamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

				//EscribirLogErrores(ObjAutorizador);
				try
				{
					ObjAutorizador.Token = _cmd.Get_Token(ObjAutorizador.Time_Stamp, ObjAutorizador.Key_Aplicacion);

					if (!string.IsNullOrEmpty(_mensaje)) throw new Exception(_mensaje);
				}
				catch (Exception ex)
				{
					throw ex;
				}

				// Creo la llave CNG
				Private_Key = ObjDiffieHellman.Create_Key_ECCDHP521();

				// Creo la llave Blob pública
				ObjAutorizador.Public_Blob_Key = ObjDiffieHellman.Export_Key_Material();

				// Llave compartida
				ObjAutorizador.Shared_Key = null;
			}
			catch (Exception ex)
			{
				throw ex;
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


			RequestPost.N_Documento = "Documentación_" + tramiteDto.CUIT; //documento.PostedFile.FileName;
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
					IdDocumentoCDD1 = _p_response.Id_Documento;
				if (opcion == 2)
					IdDocumentoCDD2 = _p_response.Id_Documento;

				divMensajeErrorDocumentacionHab.Visible = false;
				divMensajeErrorDocumentacionAFIP.Visible = false;
				divMensajeExitoDocumentacionAFIP.Visible = true;
				divMensajeExitoDocumentacionHab.Visible = true;
				lblMensajeExitoDocumentacionHab.Text = "Se digitalizó correctamente el documento con nro: " + _p_response.Id_Documento;
				lblMensajeExitoDocumentacionAFIP.Text = "Se digitalizó correctamente el documento con nro: " + _p_response.Id_Documento;
				//DivAdjuntar.Visible = false;

			}
			else
			{
				divMensajeErrorDocumentacionHab.Visible = true;
				divMensajeErrorDocumentacionAFIP.Visible = true;
				divMensajeExitoDocumentacionHab.Visible = false;
				divMensajeExitoDocumentacionAFIP.Visible = false;
				lblMensajeErrorDocumentacionHab.Text = "Codigo Error: " + _p_response.Codigo_Resultado + " Descripcion: " +
									   _p_response.Detalle_Resultado;
				lblMensajeErrorDocumentacionAFIP.Text = "Codigo Error: " + _p_response.Codigo_Resultado + " Descripcion: " +
									   _p_response.Detalle_Resultado;

				IdDocumentoCDD1 = 0; IdDocumentoCDD2 = 0;



			}
		}
        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            //divPantallaConsulta.Visible = true;
            divPantallaResultado.Visible = false;

        }

        protected void gvResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var consultaTramite = (consultaTramite)e.Row.DataItem;

               
                if (consultaTramite != null)
                {
                    /* Si el tramite es reempadronamiento o baja no se muestra el boton de imprimir TRS */
                    if (consultaTramite.Tipo_Tramite == "REEMPADRONAMIENTO" || consultaTramite.Tipo_Tramite=="BAJA")
                    {
                        var btn = (Button)e.Row.FindControl("BtnImprimirTasa");
						btn.Visible = false;

				    }
					var btnVerDoc1 = (Button)e.Row.FindControl("btnVerDoc1");
					var btnVerDoc2 = (Button)e.Row.FindControl("btnVerDoc2");
					IdDocumentoCDD = Bl.blGetIdDocumentoCDD(int.Parse(consultaTramite.Nro_tramite),1);
					if (IdDocumentoCDD != 0)
					{
						btnVerDoc1.CssClass = "botonDocumentacion";
						btnVerDoc1.ToolTip = "Descargar Documentacion Adjuntada";
					}
					else
					{
						btnVerDoc1.CssClass = "botonNoArchivo";
						btnVerDoc1.ToolTip = "Su Documento no fue adjuntado o esta dañado, intente adjuntarlo nuevamente";
					}

					IdDocumentoCDD = Bl.blGetIdDocumentoCDD(int.Parse(consultaTramite.Nro_tramite), 2);
					if (IdDocumentoCDD != 0)
					{
						btnVerDoc2.CssClass = "botonDocumentacion";
						btnVerDoc2.ToolTip = "Descargar Documentacion Adjuntada";
					}
					else
					{
						btnVerDoc2.CssClass = "botonNoArchivo";
						btnVerDoc2.ToolTip = "Su Documento no fue adjuntado o esta dañado, intente adjuntarlo nuevamente";
					}
					IdDocumentoCDD = 0;
                }
            }
        }

        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            var listaTramites = (List<consultaTramite>) Session["ListaTramites"];
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;
            divInformacion.Visible = true;
            lblInfoGeneral.Text =
                "Sr. Contribuyente: Informamos que los trámites de alta que se iniciaron y tengan una antigüedad mayor a la fecha de " +
                " vencimiento de su tasa retributiva y NO HAYA SIDO ABONADA PARA SER VERIFICADO, " +
                "se cambiará el estado a TRAMITE VENCIDO y se deberá realizar nuevamente. " +
                "Muchas Gracias por su atención.";
            if (listaTramites.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double) (listaTramites.Count/(double) gvResultado.PageSize)).ToString());
                gvResultado.PagerSettings.Visible = false;
                gvResultado.DataSource = listaTramites;
                gvResultado.DataBind();
                lblTitulocantRegistros.Visible = true;
                lblTotalRegistrosGrilla.Text = listaTramites.Count.ToString();
                var cantBotones = gvResultado.PagerSettings.PageButtonCount;
                var listaNumeros = new ArrayList();

                for (int i = 0; i < cantBotones; i++)
                {
                    var datos = new
                    {
                        nroPagina = i + 1
                    };
                    listaNumeros.Add(datos);
                }
                rptBotonesPaginacion.DataSource = listaNumeros;
                rptBotonesPaginacion.DataBind();

            }
            else
            {
                lblTotalRegistrosGrilla.Text = "No se encontraron registros que coincidan con el filtro de búsqueda";
                lblTitulocantRegistros.Visible = false;
                lblTotalRegistrosGrilla.Visible = false;
                
            }
            divPantallaResultado.Visible = true;
        }

        protected void Consultar()

        {
			
			String CUIT = UsuarioCidiLogueado.CUIL;
			
			if (UsuarioCidiLogueado.TieneRepresentados == "S")
			{
				CUIT = UsuarioCidiLogueado.Representado.RdoCuilCuit;
			}
            DateTime? fecVto = new DateTime();
            String Notificado = string.Empty;
            if (UsuarioCidiLogueado.CUIL != "")
            {
                var ListaTramites = Bl.BlGetTramites(CUIT, 0,UsuarioCidiLogueado.CUIL);
                foreach (var item in ListaTramites)
                {
                    
                    if (!string.IsNullOrEmpty(item.Nro_Sifcos) && item.Nro_Sifcos!="0")
                    {
                        fecVto = Bl.BlGetFechaUltimoTramiteSifcosNuevo(item.Nro_Sifcos);
                        if (string.IsNullOrEmpty(fecVto.ToString()))
                        {
                            fecVto = Bl.BlGetFechaUltimoTramiteSifcosViejo(item.Nro_Sifcos);
                        }
                        Notificado = Bl.BlgetNotificado(item.Nro_Sifcos);
                    }

					//if (string.IsNullOrEmpty(Notificado) && item.Nro_Sifcos!="0")
					//{
     //                   EnviarNotificacionCIDI(item.CUIT);
     //               }
                    if (item.Nro_Sifcos.Substring(0, 1) == "0")
                    {
                        item.Nro_Sifcos = "NO ASIGNADO";

                    }



                }
                
                Session["ListaTramites"] = ListaTramites;
            }
            
            RefrescarGrilla();
            
        }
        protected void EnviarNotificacionCIDI(String CUIT)
        {
            var destinatario = Bl.BlGetDestinatario(CUIT);

            if (string.IsNullOrEmpty(destinatario))
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "El Responsable Legal del tramite no fue cargado.";
                return;
            }

            var Hash = Cifrar(destinatario);
			Session["MisTramites"] = "OK";

            Response.Redirect("Enviar.aspx?destinatario=" + Hash);
        }
		public string Cifrar(string pTextoACifrar)
		{
			string key = Key_Cif_Decif;
			byte[] clearBytes = Encoding.Unicode.GetBytes(pTextoACifrar);

			using (Aes encriptadorAES = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				encriptadorAES.Key = pdb.GetBytes(32);
				encriptadorAES.IV = pdb.GetBytes(16);

				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encriptadorAES.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}

					pTextoACifrar = Convert.ToBase64String(ms.ToArray());
				}
			}

			return pTextoACifrar;
		}


            protected void rptBotonesPaginacion_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
            gvResultado.PageIndex = nroPagina - 1;
            RefrescarGrilla();
        }

        protected void btnNroPagina_OnClick(object sender, EventArgs e)
        {
            banderaPrimeraCargaPagina = true;

            var btn = (LinkButton) sender;
            //guardo el comando del boton de pagina seleccinoado
            commandoBotonPaginaSeleccionado = btn.CommandArgument;
        }

        protected void rptBotonesPaginacion_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                var btn = (LinkButton) e.Item.FindControl("btnNroPagina");
                if (btn.CommandArgument == "1" && banderaPrimeraCargaPagina == false)
                {
                    btn.BackColor = Color.Gainsboro; //pinto el boton.
                }
                if (btn.CommandArgument == commandoBotonPaginaSeleccionado)
                {
                    //por cada boton pregunto y encuentro el comando seleccionado q corresponde al boton selecionado.
                    btn.BackColor = Color.Gainsboro; //pinto el boton.
                }
                //los demas botones se cargan con el color de fondo blanco por defecto.
            }
        }

        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
            DtConsulta = new DataTable();
            gvResultado.DataSource = DtConsulta;
            gvResultado.DataBind();
            //txtFiltroCuit.Text = "";
            //txtFiltroCuit.Focus();
            divPantallaResultado.Visible = false;
            lblInfoGeneral.Text = "";
            divInformacion.Visible = false;
        }

        protected void btnMostrarGuardar_OnClick(object sender, EventArgs e)
        {
            //   DivModalGuardar.Visible = true;
        }

        protected void btnSalir_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MisTramites.aspx");
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MisTramites.aspx");
        }

        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
			var acciones = new List<string> { "ImprimirTramite", "ImprimirTasa", "VerDocumentacion1", "VerDocumentacion2", "AdjuntarHab", "AdjuntarAFIP" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvResultado.SelectedIndex = calcularIndexPagina(Convert.ToInt32(e.CommandArgument));
            var indexPaginado = gvResultado.SelectedIndex;// calculo el indice que corresponse según la paginación seleccionada de la grilla en la que estemos.
            
            EntidadSeleccionada = 0;

            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "ImprimirTramite":
                    NroTramiteAImprimir = EntidadSeleccionada;
                     ImprimirReporteTramite();
                    break;
                case "ImprimirTasa":
                    NroTramiteAImprimir = EntidadSeleccionada;
                    ImprimirTasa();
                    break;
			    case "VerDocumentacion1":
                    tramiteDto.NroTramiteSifcos = EntidadSeleccionada.ToString();
                    ImprimirDoc(1);
                    break;
				case "VerDocumentacion2":
					tramiteDto.NroTramiteSifcos = EntidadSeleccionada.ToString();
					ImprimirDoc(2);
					break;
				case "AdjuntarHab":
					MostrarOcultarAdjuntarDocumentacionHab(true);
					break;
				case "AdjuntarAFIP":
					MostrarOcultarAdjuntarDocumentacionAFIP(true);
					break;
            }
        }


		private void ImprimirDoc(int opcion)
		{
			
			IdDocumentoCDD = Bl.blGetIdDocumentoCDD(EntidadSeleccionada, opcion);

			if (IdDocumentoCDD > 0)
			{
				var _respLogin = new CDDResponseLogin();
				var _response = new CDDResponseConsulta();

				try
				{
					_respLogin = Get_Authorize_Web_Api_CDD();

					if (_respLogin != null && _respLogin.Codigo_Resultado.Equals("SEO"))
					{
						if (Set_Parameters_Request_Post(_respLogin.Llave_BLOB_Login))
						{
							_response = Get_Document_Web_Api_CDD();

							if (_response != null && _response.Codigo_Resultado.Equals("SEO"))
							{
								Download_Document(_response);
							}
							else
							{
								Response.Write("<script>alert('Codigo Error: " + _response.Codigo_Resultado +
											   " Descripcion: " + _response.Detalle_Resultado + "');</script>");
							}
						}
					}
					else
					{
						Response.Write("<script>alert('Codigo Error: " + _respLogin.Codigo_Resultado + " Descripcion: " +
									   _respLogin.Detalle_Resultado + "');</script>");
					}
				}
				catch (CDDException waEx)
				{
					Response.Write("<script>alert('Codigo Error: " + waEx.ErrorCode + " Descripcion: " +
								   waEx.ErrorDescription + "');</script>");
				}
				catch (Exception ex)
				{
					Response.Write("<script>alert('Codigo Error: " + ex.InnerException + "');</script>");
				}

			}
			else
			{
				divMensajeError.Visible = true;
				lblMensajeError.Text = "No se Encontró documentacion adjunta para el trámite seleccionado";
			}

		}

		internal bool Set_Parameters_Request_Post(byte[] _p_Blob_Key)
		{
			bool _continuar = true;
			RequestPost = new CDDPost();

			RequestPost.Id_Aplicacion_Origen = ObjAutorizador.Id_Aplicacion_Origen;
			RequestPost.Pwd_Aplicacion = ObjAutorizador.Pwd_Aplicacion;
			RequestPost.IdUsuario = ObjAutorizador.Id_Usuario;
			RequestPost.Shared_Key = _p_Blob_Key;
			RequestPost.Id_Documento = IdDocumentoCDD;
			RequestPost.N_Documento = "Documentacion_" + tramiteDto.CUIT;

			/* 
			 * Asignar el valor del Tipo de Documento en el Web.config según la asignacion de permisos.
			 * Solicitar los permisos a DesarrolloCiDi@cba.gov.ar
			 */
			if (ConfigurationManager.AppSettings["Id_Tipo_Documento"] != null)
				RequestPost.Id_Catalogo = Convert.ToInt32(MapeadorWebApi.Id_Tipo_Documento);
			else
				RequestPost.Id_Catalogo = 0;

			return _continuar;
		}

		internal void Download_Document(CDDResponseConsulta _p_response)
		{
			try
			{
				divMensajeError.Visible = false;
				String pathImg = String.Empty;

				// Desencriptado de la Documentación y Preview
				var objCryptoHash = new CryptoManagerV4._0.Clases.CryptoHash();
				String mensaje = String.Empty;

				if (_p_response.Documentacion.Imagen_Documentacion != null)
				{
					pathImg = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
						"documentacion." + _p_response.Documentacion.Extension.ToLower());

					divMensajeError.Visible = false;
					lblMensajeError.Text = String.Empty;
					lblMensajeError.Visible = false;

					if (_p_response.Documentacion.Extension.ToUpper().Equals("PDF"))
					{
						Response.ContentType = "application/pdf";
						Response.AddHeader("content-disposition", "attachment; filename= Documentacion_" + tramiteDto.CUIT + ".pdf");
						Response.BinaryWrite(_p_response.Documentacion.Imagen_Documentacion);
						Response.Flush();

					}
					else if (_p_response.Documentacion.Extension.ToUpper().Equals("DOC") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("DOCX") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("XLS") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("XLSX") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("RAR") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("ZIP"))
					{
						_p_response.Documentacion.Imagen_Documentacion =
							objCryptoHash.Descifrar_Datos(_p_response.Documentacion.Imagen_Documentacion, out mensaje);

						File.WriteAllBytes(pathImg, _p_response.Documentacion.Imagen_Documentacion);
						divMensajeExito.Visible = true;
						lblMensajeExito.Text = "El documento se descargó con éxito en " + pathImg;

					}
					else if (_p_response.Documentacion.Extension.ToUpper().Equals("PNG") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("JPG") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("JPEG") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("BMP") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("GIF") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("TIFF") ||
							 _p_response.Documentacion.Extension.ToUpper().Equals("TIF"))
					{
						_p_response.Documentacion.Imagen_Documentacion =
							objCryptoHash.Descifrar_Datos(_p_response.Documentacion.Imagen_Documentacion, out mensaje);

						iTextSharp.text.Image image =
							iTextSharp.text.Image.GetInstance(_p_response.Documentacion.Imagen_Documentacion);

						using (FileStream fs = new FileStream(pathImg, FileMode.Create, FileAccess.Write, FileShare.None))
						{
							using (iTextSharp.text.Document doc = new Document(image))
							{
								doc.SetPageSize(new iTextSharp.text.Rectangle(0, 0, image.Width, image.Height, 0));
								doc.NewPage();

								using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
								{
									doc.Open();
									image.SetAbsolutePosition(0, 0);
									writer.DirectContent.AddImage(image);
									doc.Close();
								}
							}
						}

						var arrDocImg = File.ReadAllBytes(pathImg);

						Response.ContentType = "application/pdf";
						Response.AddHeader("content-disposition", "attachment;filename=" + RequestPost.N_Documento + ".pdf");
						Response.BinaryWrite(arrDocImg);
						Response.Flush();
					}
				}
				else
				{
					Response.Write("<script>alert('Imagen nula!');</script>");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

			}

		}

		internal CDDResponseConsulta Get_Document_Web_Api_CDD()
		{
			var respuesta = new CDDResponseConsulta();
			var cddMapError = new CDDMapError();

			try
			{
				respuesta = MapeadorWebApi.ConsumirWebApi<CDDPost, CDDResponseConsulta>(
					MapeadorWebApi.Obtener_Documento, RequestPost);
			}
			catch (CDDException waEx)
			{
				respuesta.Codigo_Resultado = waEx.ErrorCode;
				respuesta.Detalle_Resultado = waEx.ErrorDescription;
			}
			catch (TimeoutException ex)
			{
				cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				respuesta.Codigo_Resultado = cddMapError.Codigo_WA_Error;
				respuesta.Detalle_Resultado = cddMapError.Descripcion_WA_Error + " | Descripcion: TimeOut. " +
											  ex.StackTrace;
			}
			catch (WebException ex)
			{
				cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				respuesta.Codigo_Resultado = cddMapError.Codigo_WA_Error;
				respuesta.Detalle_Resultado = cddMapError.Descripcion_WA_Error + " | Descripcion: WebException." +
											  ex.StackTrace;
			}
			catch (Exception ex)
			{
				cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				respuesta.Codigo_Resultado = cddMapError.Codigo_WA_Error;
				respuesta.Detalle_Resultado = cddMapError.Descripcion_WA_Error + " | Descripcion: Exception." +
											  ex.StackTrace;
			}

			return respuesta;
		}

		internal CDDResponseLogin Get_Authorize_Web_Api_CDD()
		{
			var _cddRespLogin = new CDDResponseLogin();
			var _cddMapError = new CDDMapError();

			try
			{
				Initialize_Authorize();

				_cddRespLogin =
					MapeadorWebApi.ConsumirWebApi<Autorizador, CDDResponseLogin>(MapeadorWebApi.Autorizar_Solicitud,
						ObjAutorizador);

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
												  ex.StackTrace;
			}
			catch (WebException ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: WebException." +
												  ex.StackTrace;
			}
			catch (Exception ex)
			{
				_cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

				_cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
				_cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: Exception." +
												  ex.StackTrace;
			}

			return _cddRespLogin;
		}

		protected void btnActualizarAdjunto1Modal_OnClick(object sender, EventArgs e)
		{
			if (IdDocumentoCDD1 != null)
			{
				var lista = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
				if (lista.Count == 0)
					return;
				InscripcionSifcosDto objtramiteDto = lista[0];
				objtramiteDto.USR_MODIF = master.UsuarioCidiLogueado.CUIL;
				objtramiteDto.Id_Documento1_CDD = IdDocumentoCDD1;
				Bl.blActualizar_ID_DOCUMENTO_CDD_1(objtramiteDto);

			}
		}

	    protected void btnActualizarAdjunto2Modal_OnClick(object sender, EventArgs e)
	    {
			if (IdDocumentoCDD2 != null)
			{
				var lista = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
				if (lista.Count == 0)
					return;
				InscripcionSifcosDto objtramiteDto = lista[0];
				objtramiteDto.USR_MODIF = master.UsuarioCidiLogueado.CUIL;
				objtramiteDto.Id_Documento2_CDD = IdDocumentoCDD2;
				Bl.blActualizar_ID_DOCUMENTO_CDD_2(objtramiteDto);

			}

	    }

	    private void Initialize_Authorize()
		{
			ObjDiffieHellman = new CryptoDiffieHellman();
			ObjAutorizador = new Autorizador();

			var _helper = new Helper();
			var _cmd = new CryptoManagerData();

			String _mensaje = String.Empty;

			try
			{
				ObjAutorizador.Id_Aplicacion_Origen = Convert.ToInt32(MapeadorWebApi.Id_Aplicacion_Origen);
				ObjAutorizador.Pwd_Aplicacion = MapeadorWebApi.Pwd_Aplicacion_Origen;
				ObjAutorizador.Key_Aplicacion = MapeadorWebApi.Key_Aplicacion_Origen;
				ObjAutorizador.Operacion = "1"; // 1 = Consulta
				ObjAutorizador.Id_Usuario = MapeadorWebApi.User_Aplicacion_Origen;
				ObjAutorizador.Time_Stamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

				try
				{
					ObjAutorizador.Token = _cmd.Get_Token(ObjAutorizador.Time_Stamp, ObjAutorizador.Key_Aplicacion);

					if (!string.IsNullOrEmpty(_mensaje)) throw new Exception(_mensaje);
				}
				catch (Exception ex)
				{
					throw ex;
				}

				// Creo la llave CNG
				Private_Key = ObjDiffieHellman.Create_Key_ECCDHP521();

				// Creo la llave Blob pública
				ObjAutorizador.Public_Blob_Key = ObjDiffieHellman.Export_Key_Material();

				// Llave compartida
				ObjAutorizador.Shared_Key = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
        private void ImprimirTasa()
        {
            try
            {
                // CONCEPTO
                //ID : 76010000  | NOMBRE : Art. 76 - Inc. 1 - Tasa retributiva por derecho de inscripcion en SIFCoS (Inscripcion)
                //ID : 76020000  | NOMBRE : Art. 76 - Inc. 2 - Por renovacion anual del derecho de inscripcion en el SIFCoS  (Reempadronamiento)
               
                var _tramite = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList()[0];
                int IdTipoTramiteTrs = 4;
                //String idConcepto = _tramite.IdTipoTramite;
                string strIdConcepto =  SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;
                String fecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoReempadronamiento;
                string urlTrs = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;
                string strConcpeto = "";
                string strMonto = "";

                switch (_tramite.IdTipoTramite)
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


                var resultado = Bl.BlSolicitarTrs(IdTipoTramiteTrs, _tramite.CUIT, master.UsuarioCidiLogueado.CUIL,
                    out oFechaVenc, out oHashTrx, out oIdTransaccion, out oNroLiqOriginal, out strIdConcepto, out fecDesdeConcepto, out strMonto, out strConcpeto);


               // var conceptoTasa = new ConceptoTasa { id_concepto = idConcepto, fec_desde = DateTime.Parse(fecDesdeConcepto) };
                
                /*
                var resultado = Bl.GenerarTransaccionTRS(_tramite.CUIT, master.UsuarioCidiLogueado.Id_Sexo,
                    master.UsuarioCidiLogueado.NroDocumento, "ARG",
                    master.UsuarioCidiLogueado.Id_Numero, Int64.Parse(conceptoTasa.id_concepto), conceptoTasa.fec_desde,
                    "057", 1, importeConcepto, "", DateTime.Now.Year.ToString(), out nroTransaccion);
                */

                if (resultado != "OK")
                {
                    lblMensajeError.Text = resultado;
                    divMensajeError.Visible = true;
                    return;
                }

                if (oIdTransaccion == null)
                {
                    lblMensajeError.Text = "DatoS : CUIT: " + _tramite.CUIT + " - id_sexo:" + master.UsuarioCidiLogueado.Id_Sexo + " - dni_cidi:" +
                    master.UsuarioCidiLogueado.NroDocumento + " - pais:" + "ARG" + " - ID_NRO cidi:" +
                    master.UsuarioCidiLogueado.Id_Numero + " - ID_CONCEPTO :" + strIdConcepto + " - FECHA DESDE: " + fecDesdeConcepto + "  " +
                    "057, 1, 150, , 2017";
                    divMensajeError.Visible = true;
                    return;
                }
                TipoTramiteEnum _tipo = _tramite.IdTipoTramite == "1" ? TipoTramiteEnum.Instripcion_Sifcos : TipoTramiteEnum.Reempadronamiento;
                //Bl.BlActualizarTasas(Int64.Parse(_tramite.NroTramiteSifcos), nroTransaccion, _tipo);//se asigna la nueva transaccion al tramite
                if (_tramite.IdTipoTramite == "1" || _tramite.IdTipoTramite == "2" || _tramite.IdTipoTramite == "4" || _tramite.IdTipoTramite == "5" || _tramite.IdTipoTramite == "7" || _tramite.IdTipoTramite == "9")
                {
                    Response.Redirect(urlTrs + oHashTrx + "/" + oIdTransaccion);
                }
                lblMensajeExito.Text = "IMPRESIÓN DE LA TASA CON ÉXITO";
                divMensajeExito.Visible = true;


            }
            catch (Exception e)
            {
                lblMensajeError.Text = "Ocurrió un Error al Imprimir la Tasa. Por favor intentelo más tarde: " + e.Message;
                divMensajeError.Visible = false;
            }
        }

        private void VerEntidad(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
            
        }
       
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

        private void CambiarEstado(EstadoAbmcEnum estado)
        {
            switch (estado)
            {
                case EstadoAbmcEnum.CONSULTANDO:
                    //divPantallaConsulta.Visible = true;
                    divPantallaResultado.Visible = true;
                    EstadoVista = EstadoAbmcEnum.CONSULTANDO;
                    break;
                case EstadoAbmcEnum.REGISTRANDO:
                    //divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    EstadoVista = EstadoAbmcEnum.REGISTRANDO;
                    HabilitarDesHabilitarCampos(true);
                    LimpiarCamporFormuario();
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.EDITANDO:
                    //divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    EstadoVista = EstadoAbmcEnum.EDITANDO;
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.VIENDO:
                    //divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    EstadoVista = EstadoAbmcEnum.VIENDO;
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    ImprimirReporteTramite();
                    break;
                case EstadoAbmcEnum.ELIMINANDO:
                    //divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    EstadoVista = EstadoAbmcEnum.ELIMINANDO;
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
            }
        }

        private void LimpiarCamporFormuario()
        {
        }

        private void HabilitarDesHabilitarCampos(bool valor)
        {
        }

        protected void btnImprimir_OnClick(object sender, EventArgs e)
        {
            ImprimirReporteTramite();
        }

        private int calcularIndexPagina(int indexActual)
        {
            //PORQUE LA PAGINACIÓN ESTÁ SETEADA EN 10 REGISTROS POR PAGINA.
            if (indexActual < gvResultado.PageSize)
                return indexActual;
            var resto = indexActual % gvResultado.PageSize;

            return resto;
            
        }

        private void ImprimirReporteTramite()
        {
            var lista = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
            if (lista.Count == 0)
                return;
            InscripcionSifcosDto tramiteDto = lista[0];


            var lis = new List<Producto>();
            DataTable dtProductosTramite = Bl.BlGetProductosTramite(tramiteDto.NroTramiteSifcos);
            foreach (DataRow row in dtProductosTramite.Rows)
            {
                lis.Add(new Producto { IdProducto = row["idproducto"].ToString(), NProducto = row["nproducto"].ToString() });
            }

            DataTable dtContacto = Bl.BlGetComEmpresa(tramiteDto.NroTramiteSifcos);

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



            var domicilio1 = new AppComunicacion.ApiModels.Domicilio();
            if (tramiteDto.IdVinDomLocal.HasValue)
            {
                domicilio1 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLocal.Value.ToString());
            }

            var domicilio2 = new AppComunicacion.ApiModels.Domicilio();
            if (tramiteDto.IdVinDomLegal.HasValue)
            {
                domicilio2 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLegal.Value.ToString());
            }

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
            var nombreReporteRdlc = "";

            var tituloReporte = tramiteDto.NombreTipoTramite.ToUpper();

            switch (tramiteDto.IdTipoTramite)
            {
                case "1":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSAlta.rdlc";
                    break;
                case "2":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSBaja.rdlc";
                    break;
                case "4":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReempadronamiento.rdlc";
                    break;
                case "5":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresion.rdlc";
                    tituloReporte = "REIMPRESION DE OBLEA";
                    break;
                case "6":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresionSinTRS.rdlc";
                    tituloReporte = "REIMPRESION DE OBLEA";
                    break;
                case "7":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresion.rdlc";
                    tituloReporte = "REIMPRESION DE CERTIFICADO";
                    break;
                case "8":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresionSinTRS.rdlc";
                    tituloReporte = "REIMPRESION DE CERTIFICADO";
                    break;
                case "9":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresion.rdlc";
                    tituloReporte = "REIMPRESION DE OBLEA Y CERTIFICADO";
                    break;
                case "10":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresionSinTRS.rdlc";
                    tituloReporte = "REIMPRESION DE OBLEA Y CERTIFICADO";
                    break;
                default:
                    tituloReporte = tramiteDto.NombreTipoTramite.ToUpper();
                    break;
            }

            var reporte = new ReporteGeneral(nombreReporteRdlc, lis, TipoArchivoEnum.Pdf);
            reporte.AddParameter("parametro_Titulo_reporte", "Comprobante de Trámite - " + tituloReporte);
            if (nombreReporteRdlc == "SIFCOS.rptTramiteSIFCoSBaja.rdlc")
            {
                reporte.AddParameter("parametro_fecha_baja", tramiteDto.FecVencimiento.ToString());
            }
            else
            {
                reporte.AddParameter("parametro_fecha_vencimiento", tramiteDto.FecVencimiento.Day + "/" + tramiteDto.FecVencimiento.Month + "/" + tramiteDto.FecVencimiento.Year);
            }
           
            
            reporte.AddParameter("nroTramiteSifcos", tramiteDto.NroTramiteSifcos);
            reporte.AddParameter("paramatro_dom1_departamento", domicilio1.Departamento != null ? domicilio1.Departamento.Nombre : " - ");
            reporte.AddParameter("paramatro_dom1_localidad", domicilio1.Localidad != null ? domicilio1.Localidad.Nombre : " - ");
            reporte.AddParameter("paramatro_dom1_barrio", domicilio1.Barrio != null ? domicilio1.Barrio.Nombre : " - ");
            reporte.AddParameter("paramatro_dom1_calle", domicilio1.Calle != null ? domicilio1.Calle.Nombre : " - ");
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
            reporte.AddParameter("paramatro_dom2_departamento", domicilio2.Departamento != null ? domicilio2.Departamento.Nombre : " - ");
            reporte.AddParameter("paramatro_dom2_localidad", domicilio2.Localidad != null ? domicilio2.Localidad.Nombre : " - ");
            reporte.AddParameter("paramatro_dom2_barrio", domicilio2.Barrio != null ? domicilio2.Barrio.Nombre : " - ");
            reporte.AddParameter("paramatro_dom2_calle", domicilio2.Calle != null ? domicilio2.Calle.Nombre : " - ");
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
            
            //cargo los datos del gestor y responsable
            reporte.AddParameter("parametro_gestor_nombre", tramiteDto.NombreYApellidoGestor);
            reporte.AddParameter("parametro_gestor_dni", tramiteDto.CuilGestor);


            reporte.AddParameter("parametro_gestor_telefono","sin asignar" );//UsuarioCidiRep.CelFormateado != "" ? UsuarioRespGestor.CelFormateado : " - ");
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
            Response.Redirect("ReporteGeneral.aspx");
        }

        public AppComunicacion.ApiModels.Domicilio consultarDomicilioByIdVin(string idVinculacion)
        {
            var Domicilio = Bl.BlGetDomEmpresaByIdVin(idVinculacion);

            return Domicilio;
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

        public DataTable DtRubros
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

        protected void btnNuevaInscripcion_OnClick(object sender, EventArgs e)
        {
			Response.Redirect("inscripcion.aspx");
        }

        protected void btnDescargarComprobante_OnClick(object sender, EventArgs e)
        {
            if (Session["ReporteGeneral"] != null)
                Response.Redirect("ReporteGeneral.aspx");
        }
        
        protected void btnDescargarTasa_OnClick(object sender, EventArgs e)
        {
             if (Session["NroTransaccionParaImprimir"] != null)
                if(Session["NroTransaccionParaImprimir"].ToString() != "noImprimir") //significa q es un alta y debe redireccionar.
                    Response.Redirect("https://tasas.cba.gov.ar/GenerarCedulon.aspx?id=" + Session["NroTransaccionParaImprimir"].ToString());
            if (Session["VENGO_DESDE_CARGA_REIMPRESION_TRS"] != null)
            {
                Response.Redirect("https://tasas.cba.gov.ar/GenerarCedulon.aspx?id=" + Session["VENGO_DESDE_CARGA_REIMPRESION_TRS"].ToString());
            }
            Session["VENGO_DESDE_CARGA_REIMPRESION_TRS"] = null;
            Session["NroTransaccionParaImprimir"] = null;
        }
    }
}