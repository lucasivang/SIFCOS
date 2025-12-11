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
using System.Web;
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
using DA_SIFCOS.Entities.CDDResponse;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Entities.Excepcion;
using DA_SIFCOS.Entities.Errores;
using DA_SIFCOS.Models;
using DA_SIFCOS.Utils;

namespace SIFCOS
{
	public partial class CargaTramiteReimpresion : System.Web.UI.Page
	{

		public ReglaDeNegocios Bl = new ReglaDeNegocios();
		public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();
		private Autorizador ObjAutorizador { get; set; }
		private CDDPost RequestPost { get; set; }

		DataColumn Producto = new DataColumn("PRODUCTO", System.Type.GetType("System.String"));
		DataColumn Rubro = new DataColumn("RUBRO", System.Type.GetType("System.String"));

		protected DataTable DtDeptos = new DataTable();
		protected DataTable DtProvincias = new DataTable();
		protected DataTable DtLocalidades = new DataTable();
		protected DataTable DtBarrios = new DataTable();


		protected DataTable DtGrilla = new DataTable();
		public Int64 pExiste;
		public Int64 pVencida;
		public Principal master;


		protected void Page_Load(object sender, EventArgs e)
		{



			/* Agregar a cada pagina web, para heredar el comportamiento del usuario CIDI.*/

			master = (Principal)Page.Master;
			var lstRolesNoAutorizados = new List<string>();
			lstRolesNoAutorizados.Add("Sin Asignar");

			if (lstRolesNoAutorizados.Contains(master.RolUsuario))
			{
				Response.Redirect("Inscripcion.aspx");
			}
			divMjeErrorRepLegal.Visible = false;
			divMjeExitoRepLegal.Visible = false;
			divMensajeErrorVentanaEncabezado.Visible = false;
			divMensajeError.Visible = false;
			divMensajeExito.Visible = false;
			DtGrilla.Columns.Add(Producto);
			DtGrilla.Columns.Add(Rubro);

			if (!Page.IsPostBack)
			{
				LimpiarSessionesEnMemoria();

				divVentanaPasosTramite.Visible = false;

				//btnCargarDomicilio1.Enabled = false;
				//btnCargarDomicilio2.Enabled = false;

				SessionLocalidades = null;
				SessionBarrios = null;

				divEncabezadoDatosEmpresa.Visible = false;
				ObjetoInscripcion = new InscripcionSifcos();
				//ObtenerUsuario();
				CargarDatosUsuarioLogueado();
				NumeroDePasoActual = NumeroPasoEnum.PrimerPaso;
				HabilitarBotonesFooter(NumeroDePasoActual);

				//CargarProvinciaLegal();
				//ddlProvinciaLegal.SelectedValue = "X";// selecciono por defecto provincia de CÓRDOBA.
				//CargarDeptos();
				//CargarDeptosLegal();
				//ddlDeptos.Focus();

				CargarBoca();

				lblTituloTramite.Text = "Trámite Reimpresión de Oblea / Certificado";

				//String TT_Reimpresion = Tipo_Tramite_Reimpresion;
				//String TD_Reimpresion = Tipo_Doc_Reimpresion;

				divMensaejeErrorModal.Visible = true;
				divMensaejeErrorModal.Visible = false;
				DtActividades = null;
				DtProductos = null;
				// RefrescarGrillaProductos();
				txtCuit.Text = (string)Session["VENGO_DESDE_REIMPRESION_CUIT"];

				IniciarTramite();
			}


		}

		private void CargarBoca()
		{
			ddlMyBoca.Items.Clear();
			DataTable dt = Bl.BlGetOrganismos();
			if (dt.Rows.Count != 0)
			{
			 foreach (DataRow dr in dt.Rows)
				{
					ddlMyBoca.Items.Add(new ListItem(dr["n_organismo"].ToString(), dr["id_organismo"].ToString()));

				}
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
			txtNroSifcos.Text = Session["VENGO_DESDE_REIMPRESION_NRO_SIFCOS"].ToString(); //campo que se carga en la pantalla ReimpresionOblea.aspx

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
			ObjetoInscripcion.TipoTramiteNum = TipoTramiteEnum.Reimpresion_Oblea;

			ObjetoInscripcion.CUIT = txtCuit.Text;

			//DEBO SELECCIONAR LA SEDE 
			CargarDomiciliosExistentes();

			//divBotonSeleccionarDomicilioExistente.Visible = DtDomicilios1.Rows.Count > 0;
			//btnCargarDomicilio1.Visible = DtDomicilios1.Rows.Count > 0;
			//divBotonSeleccionarDomicilioLegalExistente.Visible = DtDomicilios2.Rows.Count > 0;
			//btnCargarDomicilio2.Visible = DtDomicilios2.Rows.Count > 0;

			//MostrarOcultarModalTramiteSifcos(true);



			if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reimpresion_Oblea)
			{
				switch (Tipo_Doc_Reimpresion)
				{
					case "REIMPRESIÓN DE OBLEA":
						ObjetoInscripcion.TipoTramite = Tipo_Tramite_Reimpresion == "TRS_PAGA" ? 5 : 6;
						break;
					case "REIMPRESIÓN DE CERTIFICADO":
						ObjetoInscripcion.TipoTramite = Tipo_Tramite_Reimpresion == "TRS_PAGA" ? 7 : 8;
						break;
					case "REIMPRESIÓN DE OBLEA Y CERTIFICADO":
						ObjetoInscripcion.TipoTramite = Tipo_Tramite_Reimpresion == "TRS_PAGA" ? 9 : 10;
						break;
				}


				//lblTituloTramite.Text = "REEMPADRONAMIENTO - SIFCoS ";
				lblTituloVentanaModalPrincipal.Text = "REIMPRESION DE OBLEA / CERTIFICADO - SIFCoS ";
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

			//datos del gestor que se muestran en el paso 3

			txtNomApeConta.Text = master.UsuarioCidiLogueado.Nombre + " " + master.UsuarioCidiLogueado.Apellido;
			txtTelConta.Text = master.UsuarioCidiLogueado.CelFormateado;
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

		public string Tipo_Tramite_Reimpresion
		{
			get
			{
				return Session["VENGO_DESDE_REIMPRESION_TIPO_TRAMITE"] == null ? "" : (string)Session["VENGO_DESDE_REIMPRESION_TIPO_TRAMITE"];

			}
			set
			{
				Session["VENGO_DESDE_REIMPRESION_TIPO_TRAMITE"] = value;
			}
		}

		public string Tipo_Doc_Reimpresion
		{
			get
			{
				return Session["VENGO_DESDE_REIMPRESION_TIPO_DOC"] == null ? "" : (string)Session["VENGO_DESDE_REIMPRESION_TIPO_DOC"];

			}
			set
			{
				Session["VENGO_DESDE_REIMPRESION_TIPO_DOC"] = value;
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

		public DataTable SessionDepartamentos
		{
			get
			{
				return Session["DEPTO"] == null ? null : (DataTable)Session["DEPTO"];
			}
			set
			{
				Session["DEPTO"] = value;
			}
		}

		public DataTable SessionBarrios
		{
			get
			{
				return Session["BARRIO"] == null ? null : (DataTable)Session["BARRIO"];
			}
			set
			{
				Session["BARRIO"] = value;
			}
		}

		public DataTable SessionLocalidades
		{
			get
			{
				return Session["LOCALIDAD"] == null ? null : (DataTable)Session["LOCALIDAD"];
			}
			set
			{
				Session["LOCALIDAD"] = value;
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
					ProgressBar.Style.Value = "width: 25%";
					lblPasos.Text = "Paso 1 de 4";
					divPanel_1.Visible = true;
					divPanel_2.Visible = false;
					divPanel_3.Visible = false;
					divPanel_4.Visible = false;
					btnAtras.Visible = false;
					btnSiguiente.Visible = true;
					btnFinalizar.Visible = false;
					break;
				case NumeroPasoEnum.SegundoPaso:
					ProgressBar.Style.Value = "width: 50%";
					lblPasos.Text = "Paso 2 de 4";
					divPanel_1.Visible = false;
					divPanel_2.Visible = true;
					divPanel_3.Visible = false;
					divPanel_4.Visible = false;
					btnAtras.Visible = true;
					btnSiguiente.Visible = true;
					btnFinalizar.Visible = false;
					break;
				case NumeroPasoEnum.TercerPaso:
					ProgressBar.Style.Value = "width: 75%";
					lblPasos.Text = "Paso 3 de 4";
					divPanel_1.Visible = false;
					divPanel_2.Visible = false;
					divPanel_3.Visible = true;
					divPanel_4.Visible = false;
					btnAtras.Visible = true;
					btnSiguiente.Visible = true;
					btnFinalizar.Visible = false;
					break;
				case NumeroPasoEnum.CuartoPaso:
					ProgressBar.Style.Value = "width: 100%";
					lblPasos.Text = "Paso 4 de 4";
					divPanel_1.Visible = false;
					divPanel_2.Visible = false;
					divPanel_3.Visible = false;
					divPanel_4.Visible = true;
					btnAtras.Visible = true;
					btnSiguiente.Visible = false;
					btnFinalizar.Visible = true;
					break;

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

		protected void btnAtras_OnClick(object sender, EventArgs e)
		{
			CorregirPasoActual();
			switch (NumeroDePasoActual) //PASO ACTUAL
			{
				case NumeroPasoEnum.SegundoPaso:
					HabilitarBotonesFooter(NumeroPasoEnum.PrimerPaso);
					NumeroDePasoActual = NumeroPasoEnum.PrimerPaso;
					////CargarProvinciaLegal();
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

			txtNroDGR.Text = DtEmpresa.Rows[0]["NUMERO_INSCRIPCION"].ToString();
			//if (txtNroDGR.Text != "")
			//{
			//    txtNroDGR.Enabled = false;
			//}

			txtNroHabMun.Text = "";// no tiene porque viene de rentas.

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

			txtNroDGR.Text = DtEmpresa.Rows[0]["NRO_INGBRUTO"].ToString();
			//if (txtNroDGR.Text!="")
			//{
			//    txtNroDGR.Enabled = false;
			//}
			txtNroHabMun.Text = DtEmpresa.Rows[0]["NRO_HAB_MUNICIPAL"].ToString();

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

		private void CargarDomiciliosExistentes()
		{


			//lista de Sedes que ya están cargadas en SIFCoS.
			var lsSedesSifcos = new List<Sede>();

			//listas de todas las Sedes de una empresa. Están las que están cargadas en SIFCoS y las que NO.
			var lsSedesTodas = new List<Sede>();

			//Traigo todas las Sedes para un CUIT inscriptas en RENTAS
			var dbSedes = Bl.BlGetSedes(txtCuit.Text.Trim());


			//DtDomicilios1 = dbSedes;
			//gvDomicilio1.DataSource = DtDomicilios1;
			//gvDomicilio1.DataBind();


			//DtDomicilios2 = dbSedes;
			//gvDomicilio2.DataSource = DtDomicilios2;
			//gvDomicilio2.DataBind();


			//En el combo principal sólo muestro las sedes ya cargadas en sifcos para ese CUIT.
			foreach (DataRow row in dbSedes.Rows)
			{
				var sede = new Sede { IdSede = row["ID_SEDE"].ToString(), NombreSede = row["SEDES"].ToString(), IdVin_Sede = row["id_vin"].ToString() };

				sede.PerteneceSifcos =
					SessionTramitesDelCuit.Any(x => x.CUIT == txtCuit.Text.Trim() && x.id_sede_entidad == sede.IdSede);
				if (sede.PerteneceSifcos)
					if (!lsSedesSifcos.Contains(sede))
						lsSedesSifcos.Add(sede);

				if (!lsSedesTodas.Contains(sede))
					lsSedesTodas.Add(sede);

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
						CargarDatosObjInscripcion(NumeroPasoEnum.PrimerPaso);
						HabilitarBotonesFooter(NumeroPasoEnum.SegundoPaso);
						NumeroDePasoActual = NumeroPasoEnum.SegundoPaso;
						InicializarIframeDomEstab();
						iniciarModuloDirecciones();
						PintarTabBotonSiguiente(li_tab_1, li_tab_2);
						break;
					case NumeroPasoEnum.SegundoPaso:
						if (!ValidarDatosRequeridos(NumeroPasoEnum.SegundoPaso))
							break;
						CargarDatosObjInscripcion(NumeroPasoEnum.SegundoPaso);
						HabilitarBotonesFooter(NumeroPasoEnum.TercerPaso);
						NumeroDePasoActual = NumeroPasoEnum.TercerPaso;
						//CargarProductos();
						PintarTabBotonSiguiente(li_tab_2, li_tab_3);
						break;
					case NumeroPasoEnum.TercerPaso:
						if (!ValidarDatosRequeridos(NumeroPasoEnum.TercerPaso))
							break;
						CargarDatosObjInscripcion(NumeroPasoEnum.TercerPaso);
						HabilitarBotonesFooter(NumeroPasoEnum.CuartoPaso);
						NumeroDePasoActual = NumeroPasoEnum.CuartoPaso;
						PintarTabBotonSiguiente(li_tab_3, li_tab_4);
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

		}

		private void CargarCamposFormulario()
		{
			//PARA EL REEMPADRONAMIENTO SOLO SE MUESTRAN LOS DATOS.. HAY QUE CARGARLOS.
			habilitarCampoFormularioContacto(false);
			//habilitarCampoFormularioDomicilio1(false);
			//habilitarCampoFormularioDomicilio2(false);
			habilitarCampoFormularioInformacionGeneral(false);
		}
		private void habilitarCampoFormularioInformacionGeneral(bool mostrar)
		{
			txtFechaIniAct.Visible = mostrar;
			txtNroHabMun.Visible = mostrar;
			txtNroDGR.Visible = mostrar;
			txtM2Venta.Visible = mostrar;
			txtM2Admin.Visible = mostrar;
			txtM2Dep.Visible = mostrar;
			rbInquilino.Visible = mostrar;
			txtCantPersRelDep.Visible = mostrar;
			txtCantPersTotal.Visible = mostrar;
			rbSiCobertura.Visible = mostrar;
			rbSiCap.Visible = mostrar;
			rbSiSeg.Visible = mostrar;
			chkprov.Visible = mostrar;
			ChkNacional.Visible = mostrar;
			ChkInter.Visible = mostrar;
		}

		private void CargarDatosObjInscripcion(NumeroPasoEnum paso)
		{
			switch (paso)
			{

			  case NumeroPasoEnum.PrimerPaso:
			  
					if (!string.IsNullOrEmpty(IdDocumentoCDD1.ToString()))
					{
						ObjetoInscripcion.Id_Documento3_CDD = IdDocumentoCDD1;
						
					}
					if (!string.IsNullOrEmpty(IdDocumentoCDD2.ToString()))
					{
						ObjetoInscripcion.Id_Documento4_CDD = IdDocumentoCDD2;

					}
					ObjetoInscripcion.IdOrganismo = int.Parse(ddlMyBoca.SelectedValue);
                    break;
				case NumeroPasoEnum.SegundoPaso:
					//ObjetoInscripcion.Latitud = txtLatitud.Text;
					//ObjetoInscripcion.Longitud = txtLongitud.Text;
					if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
					{
						ObjetoInscripcion.FechaVencimiento = DateTime.Now.AddYears(1);//en el ALTA la fecha de vencimiento es el próximo año.
						ObjetoInscripcion.CantidadReempadranamientos = 0;
					}
					break;
				case NumeroPasoEnum.TercerPaso:
					ObjetoInscripcion.CapacUltAnio = rbSiCap.Checked ? "S" : "N";
					//ObjetoInscripcion.Local = txtLocal.Text;
					//ObjetoInscripcion.Oficina = txtOficina.Text;
					//ObjetoInscripcion.Stand = txtStand.Text;
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

					if (ObjetoInscripcion.Propietario == "N")
					{
						ObjetoInscripcion.RangoAlquiler = rb5.Checked ? "$5000" : "";
						if (string.IsNullOrEmpty(ObjetoInscripcion.RangoAlquiler))
							ObjetoInscripcion.RangoAlquiler = rb510.Checked ? "$5000 a $10000" : "";
						if (string.IsNullOrEmpty(ObjetoInscripcion.RangoAlquiler))
							ObjetoInscripcion.RangoAlquiler = rb1015.Checked ? "$10000 a $15000" : "";
						if (string.IsNullOrEmpty(ObjetoInscripcion.RangoAlquiler))
							ObjetoInscripcion.RangoAlquiler = rb1520.Checked ? "más de $15000" : "";

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
				case NumeroPasoEnum.CuartoPaso:
					if (ObjetoInscripcion == null)
						Response.Redirect("MisTramites.aspx?Exito=1");
					ObjetoInscripcion.CuilGestor = master.UsuarioCidiLogueado.CUIL;
					ObjetoInscripcion.EmailGestor = txtEmailConta.Text;
					ObjetoInscripcion.TelGestor = txtTelConta.Text;
					ObjetoInscripcion.IdEstado = 1;//TRÁMITE estado: CARGADO REQUIERE DE REVISION

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


			//contacto
			controles.Add(txtTelFijoCodArea);
			controles.Add(txtTelFijo);
			controles.Add(txtCelularCodArea);
			controles.Add(txtCelular);
			controles.Add(txtEmail_Establecimiento);

			//domicilio 2
			//controles.Add(ddlDeptoLegal);
			//controles.Add(ddlLocalidadLegal);
			//controles.Add(ddlBarriosLegal);
			//controles.Add(txtBarrioLegal);
			//controles.Add(txtCodPosLegal);
			//controles.Add(txtNroCalleLegal);
			//controles.Add(txtCalleLegal);

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
						AgregarCssClass("campoRequerido", txtCuit);
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
				case NumeroPasoEnum.PrimerPaso: //boca y cdd
					int marca = 0;

					//--LT: VALIDO BOCA DE RECEPCION EN ALTA PURA Y REEMPADRONAMIENTOS
					if (string.IsNullOrEmpty(ddlMyBoca.SelectedValue) || ddlMyBoca.SelectedValue == "0")
					{
						lblMensajeError.Text = "Debe seleccionar boca de recepción del trámite";
						AgregarCssClass("campoRequerido", ddlMyBoca);
						divMensajeError.Visible = true;
						return false;
					}

					if (DivAdjuntar.Visible)
					{
						if (IdDocumentoCDD1 != null)
						{
							if (IdDocumentoCDD1 == 0)
							{
								marca = marca + 1;
							}
						}

						if (IdDocumentoCDD2 != null)
						{
							if (IdDocumentoCDD2 == 0)
							{
								marca = marca + 1;
							}
						}


						if (marca != 0)
						{
							divMensajeError.Visible = true;
							lblMensajeError.Text = "Es obligatorio adjuntar alguno de los dos documentos indicados.";
							return false;
						}
						return true;
					}
					else
					{
						return true;
					}
					break;
				case NumeroPasoEnum.SegundoPaso: //ESTABILECIMIENTO

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

					//if (txtCodPos.Text.Length != 4)
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
					if (string.IsNullOrEmpty(txtEmail_Establecimiento.Text))
					{
						lblMensajeError.Text = "Debe ingresar el Email con que trabaja el establecimiento.";
						AgregarCssClass("campoRequerido", txtEmail_Establecimiento);
						divMensajeError.Visible = true;
						return false;
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


					// 3) DOMICILIOS 2

					//if (ddlDeptoLegal.SelectedValue == "0")
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


					//if (string.IsNullOrEmpty(txtCodPosLegal.Text))
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

					//if (string.IsNullOrEmpty(txtNroCalleLegal.Text))
					//{
					//    lblMensajeError.Text = "La Altura de la Calle es un campo obligatorio (*). Si ya tiene seleccionado un domicilio por defecto, puede agregar habilitar los campos haciendo click en el botón 'Habilitar para Cargar Nuevo Domicilio'.";
					//    AgregarCssClass("campoRequerido", txtNroCalleLegal);
					//    divMensajeError.Visible = true;
					//    return false;
					//}


					return true;

				case NumeroPasoEnum.TercerPaso: //INFORMACIÓN ADICIONAL
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


				case NumeroPasoEnum.CuartoPaso:
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

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Método que guarda el domicilio del establecimiento y legal en BD del esquema t_comunes.
		/// </summary>
		/// <returns> Retorna TRUE si se guardó con éxito el domicilio y se asignó en sessión IdVinDomicilio1 y IdVinDomicilio2. Retorna FALSE en caso contrario.</returns>
		private bool GuardarDomicilios()
		{
			if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reimpresion_Oblea)
				idSedeSeleccionada = "00";
			else
			{
				idSedeSeleccionada = "00";//para INSCRIPCIÓN/ALTA de tramite la sede por defecto es la 00 porque carga un domicilio nuevo.
			}

			if (!IdVinDomicilio1.HasValue || !IdVinDomicilio2.HasValue)
			{
				//domicilio1
				//cargo domicilio nuevo
				//--int? idVin = null;
				var resultado = "";

				//if (!IdVinDomicilio1.HasValue)
				//{
				//    var barrio = !chkBarrioNoExiste.Checked
				//        ? ddlBarrios.SelectedItem.Text
				//        : txtBarrio.Text;
				//    var id_barrio = !chkBarrioLegalNoExiste.Checked
				//        ? ddlBarrios.SelectedValue
				//        : null;
				//    resultado = Bl.CargarDomicilio(txtCuit.Text + idSedeSeleccionada, "X", ddlDeptos.SelectedValue,
				//        ddlLocalidad.SelectedItem.Text, "", "calle", "", txtCalle.Text, id_barrio, barrio, "",
				//        txtNroCalle.Text, txtPiso.Text, txtNroDepto.Text, "", ddlLocalidad.SelectedValue, txtCodPos.Text, "", "", out idVin);
				//    if (resultado == "OK")
				//    {
				//        //se cargó el Domicilio1 con éxito
				//        IdVinDomicilio1 = idVin;
				//    }
				//    else
				//    {
				//        lblMensajeError.Text = "Ocurrió un problema al cargar los datos de la Dirección en la Base de Datos.";
				//        divMensajeError.Visible = true;
				//        return false;
				//    }
				//}

				//if (!IdVinDomicilio2.HasValue)
				//{
				//    //domicilio 2
				//    //cargo domicilio legal nuevo
				//    int? idVin2 = null;
				//    var barrioLegal = !chkBarrioLegalNoExiste.Checked
				//        ? ddlBarriosLegal.SelectedItem.Text
				//        : txtBarrioLegal.Text;
				//    var id_barrio_legal = !chkBarrioLegalNoExiste.Checked
				//        ? ddlBarriosLegal.SelectedValue
				//        : null;
				//    resultado = Bl.CargarDomicilio(txtCuit.Text + idSedeSeleccionada, ddlProvinciaLegal.SelectedValue,
				//        ddlDeptoLegal.SelectedValue, ddlLocalidadLegal.SelectedItem.Text, "", "calle", "", txtCalleLegal.Text,
				//        id_barrio_legal, barrioLegal, "", txtNroCalleLegal.Text, txtPisoLegal.Text, txtNroDeptoLegal.Text, "",
				//        ddlLocalidadLegal.SelectedValue, txtCodPosLegal.Text, "", "", out idVin2);
				//    if (resultado == "OK")
				//    {
				//        //se cargó el Domicilio2 con éxito
				//        IdVinDomicilio2 = idVin2;
				//    }
				//    else
				//    {
				//        lblMensajeError.Text = "ERROR AL CARGAR DEL DOMICLIO LEGAL. " + resultado;
				//        divMensajeError.Visible = true;
				//        return false;
				//    }
				//}
			}
			return true;
		}

		private void PreCargarCamposParaReempadronamiento()
		{
			if (SessionNroSifcos != 0)
			{
				DataTable tramite = Bl.BlGetUltimoTramiteSifcosNuevo(SessionNroSifcos.ToString().Trim());

				if (tramite == null)
				{
					return;
				}

				var _Id_tramite = int.Parse(tramite.Rows[0]["NRO_TRAMITE_SIFCOS"].ToString());

				#region DOMICILIO DEL ESTABLECIMIENTO

				//var domicilio1 = consultarDomicilioByIdVin(tramite.Rows[0]["ID_VIN_DOM_LOCAL"].ToString());

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
				//txtOficina.Text = tramite.Rows[0]["OFICINA"].ToString();
				//txtStand.Text = tramite.Rows[0]["STAND"].ToString();
				//txtLocal.Text = tramite.Rows[0]["LOCAL"].ToString();
				//chkBarrioNoExiste.Enabled = !string.IsNullOrEmpty(domicilio1.Barrio);

				#endregion // DOMICILIO DEL ESTABLECIMIENTO

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
								txtEmail_Establecimiento.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString())
									? row["NRO_MAIL"].ToString()
									: " - ";
								break;
							case "12": //PAGINA WEB
								txtWebPage.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString())
									? row["NRO_MAIL"].ToString()
									: " - ";
								break;
							case "17": //FACEBOOK
								txtRedSocial.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString())
									? row["NRO_MAIL"].ToString()
									: " - ";
								break;
						}
					}
				}

				#endregion //CONTACTO

				#region DOMICILIO LEGAL

				////var domicilio2 = consultarDomicilioByIdVin(tramite.Rows[0]["ID_VIN_DOM_LEGAL"].ToString());

				////ddlProvinciaLegal.SelectedValue = domicilio2.IdProvincia;
				////CargarDeptosLegal();
				////ddlDeptoLegal.SelectedValue = domicilio2.IdDepartamento;
				////CargarLocalidadesLegal();
				////ddlLocalidadLegal.SelectedValue = domicilio2.IdLocalidad;
				////CargarBarriosLegal();
				////ddlBarriosLegal.SelectedValue = string.IsNullOrEmpty(domicilio2.IdBarrio) ? "0" : domicilio2.IdBarrio;
				////txtBarrioLegal.Text = domicilio2.Barrio;
				////chkBarrioLegalNoExiste.Checked = !string.IsNullOrEmpty(domicilio2.Barrio);
				////txtCalleLegal.Text = domicilio2.Calle;
				////txtNroCalleLegal.Text = domicilio2.Altura;
				////txtCodPosLegal.Text = domicilio2.CodigoPostal;

				#endregion //DOMICILIO LEGAL

				#region PRODUCTO - ACTIVIDAD

				DtProductos = Bl.BlGetProductosTramite(_Id_tramite.ToString());
				RefrescarGrillaProductos();
				chkConfirmarListaDeProducto.Checked = true; //no entra al EVENTO DEL CHECKBOX

				ddlRubroPrimario.Enabled = true;
				ddlRubroSecundario.Enabled = true;
				CargarRubrosSegunProductos();

				ddlRubroPrimario.SelectedValue = tramite.Rows[0]["ID_ACTIVIDAD_PPAL"].ToString();
				ddlRubroSecundario.SelectedValue = tramite.Rows[0]["ID_ACTIVIDAD_SRIA"].ToString();

				#endregion // PRODUCTO - ACTIVIDAD

				#region INFO GENERAL

				txtFechaIniAct.Text = tramite.Rows[0]["FEC_INI_TRAMITE"].ToString();

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
						case "2": //SUP. VENTAS
							txtM2Venta.Text = row["superficie"].ToString();
							break;
						case "3": //SUP. DEPOSITO
							txtM2Dep.Text = row["superficie"].ToString();
							break;
					}
				}
				txtCantPersRelDep.Text = tramite.Rows[0]["CANT_PERS_REL_DEPENDENCIA"].ToString();
				txtCantPersTotal.Text = tramite.Rows[0]["CANT_PERS_TOTAL"].ToString();
				rbPropietario.Checked = tramite.Rows[0]["PROPIETARIO"].ToString() == "S";
				rbInquilino.Checked = tramite.Rows[0]["PROPIETARIO"].ToString() == "N";
				rb1015.Checked = tramite.Rows[0]["RANGO_ALQ"].ToString() == "$10000 a $15000";
				rb1520.Checked = tramite.Rows[0]["RANGO_ALQ"].ToString() == "mas de $15000";
				rb5.Checked = tramite.Rows[0]["RANGO_ALQ"].ToString() == "$5000";
				rb510.Checked = tramite.Rows[0]["RANGO_ALQ"].ToString() == "$5000 a $10000";
				rbSiCap.Checked = tramite.Rows[0]["CAPACITACION_ULT_ANIO"].ToString() == "S";
				rbNoCap.Checked = tramite.Rows[0]["CAPACITACION_ULT_ANIO"].ToString() == "N";
				rbSiCobertura.Checked = tramite.Rows[0]["COBERTURA_MEDICA"].ToString() == "S";
				rbNoCobertura.Checked = tramite.Rows[0]["COBERTURA_MEDICA"].ToString() == "N";
				rbSiSeg.Checked = tramite.Rows[0]["SEGURO_LOCAL"].ToString() == "S";
				rbNoSeg.Checked = tramite.Rows[0]["SEGURO_LOCAL"].ToString() == "N";

				switch (tramite.Rows[0]["ID_ORIGEN_PROVEEDOR"].ToString())
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

				#region GESTOR DEL TRAMITE

				//var dtGestor = Bl.BlGetGestor(int.Parse(dt.Rows[0]["ID_GESTOR_ENTIDAD"].ToString()));
				//dtGestor.Rows[0]["CUIL"].ToString();

				#endregion GESTOR DEL TRAMITE
			}
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
				return (List<consultaTramite>)Session["SessionTramitesDelCuit"];
			}
			set
			{
				Session["SessionTramitesDelCuit"] = value;
			}
		}

		public Int64 SessionNroSifcos
		{
			get
			{
				return (Int64)Session["VENGO_DESDE_REIMPRESION_NRO_SIFCOS"];
			}
			set
			{
				Session["VENGO_DESDE_REIMPRESION_NRO_SIFCOS"] = value;
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

		private void habilitarCampoFormularioContacto(bool valor)
		{
			txtCelularCodArea.Enabled = valor;
			txtCelular.Enabled = valor;
			txtTelFijoCodArea.Enabled = valor;
			txtTelFijo.Enabled = valor;
			txtRedSocial.Enabled = valor;
			txtEmail_Establecimiento.Enabled = valor;
			txtWebPage.Enabled = valor;

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
				html.AppendLine(" . Ud está finalizando con el trámite de BAJA DE COMERCIO. Confirme que la información ingresada es correcta. ");
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
			if (!ValidarDatosRequeridos(NumeroPasoEnum.TercerPaso))
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

		private int EntidadSeleccionada
		{
			get
			{
				return (int)Session["EntidadSeleccionada"];
			}
			set
			{
				Session["EntidadSeleccionada"] = value;
			}
		}

		private void CargarInformacionModalFinalizacionTramite()
		{
			CargarHtml_EncabezadoModalFinalizacion();

			lblTituloEmpresaModalConfirmacion.Text = "EMPRESA : " + txtRazonSocial.Text + " | CUIT : " + txtCuit.Text;

			gvProductosActividades_ModalFinalizacion.DataSource = DtProductos;
			gvProductosActividades_ModalFinalizacion.DataBind();

			lblActividadPrimaria.Text = ddlRubroPrimario.SelectedItem.Text;
			lblActividadSecundaria.Text = ddlRubroSecundario.SelectedItem.Text;

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
			//            lblBarrio.Text = ddlBarrios.SelectedItem.Text;
			//}

			//lblCalle.Text = txtCalle.Text;
			//lblNroCalle.Text = txtNroCalle.Text;
			//lblCodPos.Text = txtCodPos.Text;
			//lblPiso.Text = txtPiso.Text;
			//lblNroDepto.Text = txtNroDepto.Text;
			//lblOficina.Text = txtOficina.Text;
			//lblStand.Text = txtStand.Text;
			//lblLocal.Text = txtLocal.Text;
			lblEmailC.Text = txtEmail_Establecimiento.Text;
			lblCelular.Text = !string.IsNullOrEmpty(txtCelular.Text) ? "(" + txtCelularCodArea.Text + ")" + txtCelular.Text : " - ";
			lblTelFijo.Text = !string.IsNullOrEmpty(txtTelFijo.Text) ? "(" + txtTelFijoCodArea.Text + ")" + txtTelFijo.Text : " - ";
			lblWebPag.Text = !string.IsNullOrEmpty(txtWebPage.Text) ? txtWebPage.Text : " - ";
			lblFacebook.Text = !string.IsNullOrEmpty(txtRedSocial.Text) ? txtRedSocial.Text : " - ";
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


			lblFecInicioActividad.Text = txtFechaIniAct.Text;
			lblNroHabilitacionMunicipal.Text = txtNroHabMun.Text;
			lblNroDGR.Text = txtNroDGR.Text;
			lblSuperficieVenta.Text = txtM2Venta.Text;
			lblSuperficieAdministracion.Text = txtM2Admin.Text;
			lblSuperficioDeposito.Text = txtM2Dep.Text;
			lblInmueble_PropietarioInquil.Text = rbInquilino.Checked ? "Inquilino" : "Propietario";

			if (rbInquilino.Checked)
			{
				lblInmueble_RangoAlquiler.Text = rb5.Checked ? "$50000" : "";
				if (string.IsNullOrEmpty(lblInmueble_RangoAlquiler.Text))
					lblInmueble_RangoAlquiler.Text = rb510.Checked ? "$50000 a $100000" : "";
				if (string.IsNullOrEmpty(lblInmueble_RangoAlquiler.Text))
					lblInmueble_RangoAlquiler.Text = rb1015.Checked ? "$100000 a $150000" : "";
				if (string.IsNullOrEmpty(lblInmueble_RangoAlquiler.Text))
					lblInmueble_RangoAlquiler.Text = rb1520.Checked ? "más de $150000" : "";
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

		private void GuardarInscripcionSifcos()
		{
			if (!GuardarDomicilios())
				return;

			ObjetoInscripcion.IdSede = idSedeSeleccionada;

			//ObjetoInscripcion.IdVinDomLegal = IdVinDomicilio2;
			//ObjetoInscripcion.IdVinDomLocal = IdVinDomicilio1;

			//--
			if (DomComercio.IdVin != 0)
			{
				ObjetoInscripcion.IdVinDomLocal = DomComercio.IdVin;
			}
			if (DomLegal.IdVin != 0)
			{
				ObjetoInscripcion.IdVinDomLegal = DomLegal.IdVin;
			}

			if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reimpresion_Oblea)
			{

				//lt: el identidad puede tenerlo o no segun este en sifcos nuevo
				var identidad = ObjetoInscripcion.IdEntidad;
				if (identidad != "0") // siempre deberia ser asi
				{
					ObjetoInscripcion.DomComercio.ID_ENTIDAD = int.Parse(identidad);
				}
			}


			//--

			ObjetoInscripcion.ListaTrs = ListaTrs;
			//solo para REIMPRESIÓN se asigna el nro sifcos. Ya que es un tramite de Alta pero con datos tomados de otro tramite. Y el nro sifcos existe.
			ObjetoInscripcion.NroSifcos = Session["VENGO_DESDE_REIMPRESION_NRO_SIFCOS"].ToString();


			string nuevoNroTramite = "";
			var resultado = Bl.BlRegistrarInscripcion(ObjetoInscripcion, out nuevoNroTramite);
			EntidadSeleccionada = int.Parse(nuevoNroTramite);

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
					Response.Redirect("Inscripcion.aspx");
					break;
				default:
					MostrarOcultarModalFinalizacionTramite(false);
					lblMensajeError.Text =
						"OCURRIÓ UN ERROR AL GUARDAR EL TRÁMITE. COMUNÍQUESE CON SIFCoS PARA DARLE SOPORTE. MUCHAS GRACIAS.";
					divMensajeError.Visible = true;
					break;


			}
		}

		//public void CargarProvinciaLegal()
		//{
		//    ddlBarriosLegal.Items.Clear();
		//    ddlLocalidadLegal.Items.Clear();
		//    ddlDeptoLegal.Items.Clear();
		//    DtProvincias = Bl.BlGetProvincias();
		//    if (DtProvincias.Rows.Count != 0)
		//    {

		//        ddlProvinciaLegal.Items.Clear();
		//        foreach (DataRow dr in DtProvincias.Rows)
		//        {

		//            ddlProvinciaLegal.Items.Add(new ListItem(dr["n_provincia"].ToString(), dr["id_provincia"].ToString()));
		//        }

		//        ddlProvinciaLegal.Enabled = true;
		//        if (ddlProvinciaLegal.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
		//            ddlProvinciaLegal.SelectedValue = "0";
		//    }

		//}

		//public void CargarDeptos()
		//{
		//    ddlBarrios.Items.Clear();
		//    ddlLocalidad.Items.Clear();

		//    DtDeptos = Bl.BlGetDeptartamentos("X"); //seteo por defecto la provincia de CÓRDOBA.
		//    if (DtDeptos.Rows.Count != 0)
		//    {
		//        ddlDeptos.Items.Clear();
		//        foreach (DataRow dr in DtDeptos.Rows)
		//        {
		//            ddlDeptos.Items.Add(new ListItem(dr["n_departamento"].ToString(), dr["id_departamento"].ToString()));
		//        }
		//        ddlDeptos.Enabled = true;
		//        if (ddlDeptos.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
		//            ddlDeptos.SelectedValue = "0";
		//        ddlLocalidad.Focus();

		//    }
		//    else
		//    {
		//        ddlDeptos.Items.Clear();
		//    }

		//}

		//public void CargarDeptosLegal()
		//{
		//    ddlBarriosLegal.Items.Clear();
		//    ddlLocalidadLegal.Items.Clear();
		//    ddlDeptoLegal.Items.Clear();

		//    DtDeptos = Bl.BlGetDeptartamentos(ddlProvinciaLegal.SelectedValue);
		//    if (DtDeptos.Rows.Count != 0)
		//    {
		//        ddlDeptoLegal.Items.Clear();
		//        foreach (DataRow dr in DtDeptos.Rows)
		//        {
		//            ddlDeptoLegal.Items.Add(new ListItem(dr["n_departamento"].ToString(), dr["id_departamento"].ToString()));

		//        }
		//        ddlDeptoLegal.Enabled = true;
		//        if (ddlDeptoLegal.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
		//        {
		//            ddlDeptoLegal.SelectedValue = "0";
		//        }

		//        ddlLocalidadLegal.Focus();
		//    }
		//    else
		//    {

		//        ddlDeptoLegal.Items.Clear();
		//        ddlDeptoLegal.Items.Add(new ListItem("SIN ASIGNAR", "0"));

		//    }

		//}

		//protected void ddlDeptos_SelectedIndexChanged(object sender, EventArgs e)
		//{

		//    CargarLocalidades();
		//}

		//private void CargarLocalidades()
		//{
		//    ddlBarrios.Items.Clear();
		//    ddlLocalidad.Items.Clear();


		//    var idDepartamento = ddlDeptos.SelectedValue;
		//    DtLocalidades = Bl.BlGetLocalidades(idDepartamento);

		//    if (DtLocalidades.Rows.Count != 0)
		//    {
		//        foreach (DataRow dr in DtLocalidades.Rows)
		//        {
		//            ddlLocalidad.Items.Add(new ListItem(dr["n_localidad"].ToString(), dr["id_localidad"].ToString()));
		//        }

		//        SessionLocalidades = DtLocalidades;
		//        if (ddlLocalidad.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
		//            ddlLocalidad.SelectedValue = "0";
		//        ddlLocalidad.Enabled = true;
		//    }
		//    else
		//    {
		//        ddlBarrios.Enabled = false;
		//        ddlLocalidad.Enabled = false;
		//    }
		//}

		//protected void ddlLocalidad_SelectedIndexChanged(object sender, EventArgs e)
		//{

		//    CargarBarrios();

		//}

		//private void CargarBarrios()
		//{
		//    ddlBarrios.Items.Clear();
		//    DtBarrios = Bl.BlGetBarrios(ddlLocalidad.SelectedValue);

		//    if (DtBarrios.Rows.Count != 0)
		//    {
		//        ddlBarrios.Items.Clear();
		//        foreach (DataRow dr in DtBarrios.Rows)
		//        {
		//            ddlBarrios.Items.Add(new ListItem(dr["n_Barrio"].ToString(), dr["id_Barrio"].ToString()));
		//        }
		//        SessionBarrios = DtBarrios;
		//        if (ddlBarrios.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
		//            ddlBarrios.SelectedValue = "0";
		//        ddlBarrios.Enabled = true;
		//    }
		//    else
		//    {
		//        ddlBarrios.Items.Clear();
		//        ddlBarrios.Enabled = false;
		//    }
		//}

		//protected void ddlDeptoLegal_SelectedIndexChanged(object sender, EventArgs e)
		//{

		//    CargarLocalidadesLegal();
		//}

		//private void CargarLocalidadesLegal()
		//{
		//    ddlBarriosLegal.Items.Clear();
		//    ddlLocalidadLegal.Items.Clear();

		//    DtLocalidades = Bl.BlGetLocalidades(ddlDeptoLegal.SelectedValue);

		//    if (DtLocalidades.Rows.Count != 0)
		//    {
		//        foreach (DataRow dr in DtLocalidades.Rows)
		//        {
		//            ddlLocalidadLegal.Items.Add(new ListItem(dr["n_localidad"].ToString(), dr["id_localidad"].ToString()));
		//        }


		//        Session["LOCALIDAD_LEGAL"] = DtLocalidades;
		//        if (ddlLocalidadLegal.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
		//            ddlLocalidadLegal.SelectedValue = "0";
		//        ddlLocalidadLegal.Enabled = true;
		//    }
		//    else
		//    {
		//        ddlBarriosLegal.Enabled = false;
		//        ddlLocalidadLegal.Enabled = false;
		//    }
		//}

		//protected void ddlLocalidadLegal_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    txtBarrioLegal.Text = txtBarrio.Text;
		//    txtCalleLegal.Text = txtCalle.Text;
		//    txtNroCalleLegal.Text = txtNroCalle.Text;
		//    txtPisoLegal.Text = txtPiso.Text;
		//    txtNroDeptoLegal.Text = txtNroDepto.Text;
		//    txtCodPosLegal.Text = txtCodPos.Text;
		//    CargarBarriosLegal();
		//}

		//private void CargarBarriosLegal()
		//{
		//    ddlBarriosLegal.Items.Clear();

		//    DtBarrios = Bl.BlGetBarrios(ddlLocalidadLegal.SelectedValue);

		//    if (DtBarrios.Rows.Count != 0)
		//    {
		//        ddlBarriosLegal.Items.Clear();
		//        foreach (DataRow dr in DtBarrios.Rows)
		//        {
		//            ddlBarriosLegal.Items.Add(new ListItem(dr["n_Barrio"].ToString(), dr["id_Barrio"].ToString()));
		//        }
		//        Session["BARRIO_LEGAL"] = DtBarrios;
		//        if (ddlBarriosLegal.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
		//            ddlBarriosLegal.SelectedValue = "0";
		//        ddlBarriosLegal.Enabled = true;
		//    }
		//    else
		//    {
		//        ddlBarriosLegal.Items.Clear();
		//        ddlBarriosLegal.Enabled = false;
		//    }
		//}

		//protected void ddlProvinciaLegal_SelectedIndexChanged(object sender, EventArgs e)
		//{

		//    CargarDeptosLegal();

		//}

		protected void btnAgregarProd_Click(object sender, EventArgs e)
		{

			if (string.IsNullOrEmpty(ace1Value.Value) || ace1Value.Value == "0")
				return;


			if (!ValidarExistenciaProductoEnGrilla())
				return;// sino pasó la validación se interrumpe el agregado del producto

			var dr = DtProductos.NewRow();
			dr["IdProducto"] = ace1Value.Value;
			dr["NProducto"] = txtBuscarProducto.Text;

			DtProductos.Rows.Add(dr);
			RefrescarGrillaProductos();
			txtBuscarProducto.Text = string.Empty;
			txtBuscarProducto.Focus();
		}

		private bool ValidarExistenciaProductoEnGrilla()
		{

			//el hidden de id_producto  tiene ya seleccionado el producto a agregar.
			foreach (DataRow row in DtProductos.Rows)
			{
				if (row["IdProducto"].ToString() == ace1Value.Value)
				{
					lblMensajeError.Text = "El Producto que desea cargar a la lista, ya existe en la misma.";
					divMensajeError.Visible = true;
					return false;
				}
			}
			return true;
		}

		protected void btnImprimirTRS_Click(object sender, EventArgs e)
		{
			ImprimirTRSAlta();

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
				*/
				string urlTrs = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;
				int IdTipoTramiteTrs = 1;
				if (ObjetoInscripcion.TipoTramite == 4 || ObjetoInscripcion.TipoTramite == 2)
					IdTipoTramiteTrs = 4;




				//imprimo la cantidad de Tasas que corresponden según la grilla.
				//string resultado = "";
				var lstAux = new List<Trs>();

				//ObjetoInscripcion.TipoTramiteNum = TipoTramiteEnum.Instripcion_Sifcos;
				//var fecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoAlta;
				//Int64 importeConcepto = 500;

				//var conceptoTasa = new ConceptoTasa { id_concepto = idConcepto, fec_desde = DateTime.Parse(fecDesdeConcepto) };
				//string nroTransaccion = null;

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
				//lstAux.Add(new Trs { NroTransaccion = oIdTransaccion });
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


			}
			catch (Exception e)
			{
				lblMensajeError.Text = "Ocurrió un Error al Imprimir la TRS. Por favor intentelo más tarde.";
				divMensajeError.Visible = false;
			}



		}


		protected void gvDomicilio1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}

		protected void gvDomicilio2_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}


		//protected void gvDomicilio1BtnSeleccionar_click(object sender, EventArgs e)
		//{

		//    if (DtDomicilios1 == null)
		//    {
		//        lblMensajeError.Text = "SE HA SUSPENDIDO EL TRÁMITE. Se superó el tiempo de innactividad.";
		//        divMensajeError.Visible = true;
		//        return;
		//    }
		//    var row = (GridViewRow)((LinkButton)sender).NamingContainer;
		//    //Recupero los datos necesarios para obtener el domicilio a partir del  DataKeys.

		//    IdVinDomicilio1 = int.Parse(gvDomicilio1.DataKeys[row.RowIndex]["id_vin"].ToString());

		//    foreach (DataRow dataRow in DtDomicilios1.Rows)
		//    {
		//        if (dataRow["id_vin"].ToString() == IdVinDomicilio1.Value.ToString())
		//        {
		//            ddlDeptos.SelectedValue = dataRow["id_departamento"].ToString();
		//            CargarLocalidades();
		//            ddlLocalidad.SelectedValue = string.IsNullOrEmpty(dataRow["id_localidad"].ToString()) ? "0" : dataRow["id_localidad"].ToString();

		//            CargarBarrios();
		//            ddlBarrios.SelectedValue = string.IsNullOrEmpty(dataRow["id_barrio"].ToString()) ? "0" : dataRow["id_barrio"].ToString();
		//            txtCalle.Text = dataRow["n_calle"].ToString();
		//            txtNroCalle.Text = dataRow["altura"].ToString();
		//            txtPiso.Text = dataRow["piso"].ToString();
		//            txtNroDepto.Text = dataRow["depto"].ToString();
		//            txtCodPos.Text = dataRow["cpa"].ToString();
		//            //local, oficina, stand

		//            habilitarCampoFormularioDomicilio1(false);
		//            return;
		//        }

		//    }


		//}

		//protected void gvDomicilio2BtnSeleccionar_click(object sender, EventArgs e)
		//{
		//    if (DtDomicilios2 == null)
		//    {
		//        lblMensajeError.Text = "SE HA SUSPENDIDO EL TRÁMITE. Se superó el tiempo de innactividad.";
		//        divMensajeError.Visible = true;
		//        return;
		//    }

		//    var row = (GridViewRow)((LinkButton)sender).NamingContainer;
		//    //Recupero los datos necesarios para obtener el domicilio a partir del  DataKeys.

		//    IdVinDomicilio2 = int.Parse(gvDomicilio2.DataKeys[row.RowIndex]["id_vin"].ToString());

		//    foreach (DataRow dataRow in DtDomicilios1.Rows)
		//    {
		//        if (dataRow["id_vin"].ToString() == IdVinDomicilio2.Value.ToString())
		//        {
		//            ddlDeptoLegal.SelectedValue = dataRow["id_departamento"].ToString();
		//            CargarLocalidadesLegal();
		//            ddlLocalidadLegal.SelectedValue = string.IsNullOrEmpty(dataRow["id_localidad"].ToString()) ? "0" : dataRow["id_localidad"].ToString();
		//            CargarBarriosLegal();
		//            ddlBarriosLegal.SelectedValue = string.IsNullOrEmpty(dataRow["id_barrio"].ToString()) ? "0" : dataRow["id_barrio"].ToString();
		//            txtCalleLegal.Text = dataRow["n_calle"].ToString();
		//            txtNroCalleLegal.Text = dataRow["altura"].ToString();
		//            txtPisoLegal.Text = dataRow["piso"].ToString();
		//            txtNroDeptoLegal.Text = dataRow["depto"].ToString();
		//            txtCodPosLegal.Text = dataRow["cpa"].ToString();
		//            //local, oficina, stand

		//            habilitarCampoFormularioDomicilio2(false);
		//            MostrarOcultarModalDomicilio2(false);
		//            return;
		//        }
		//    }


		//}



		protected void btnAceptarTramiteARealizar_OnClick(object sender, EventArgs e)
		{
			// AceptarTramiteARealizar();
		}

		private void AceptarTramiteARealizar()
		{
			ObjetoInscripcion.DomComercio = new ComercioDto();
			ObjetoInscripcion.Latitud = "0";
			ObjetoInscripcion.Longitud = "0";

			if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Reimpresion_Oblea)
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
					anios_debe = ts.Days / 365;
					// NO DEBE REEMPADRONARSE, ESTÁ AL DÍA. AL SER REIMPRESION SE HACE EL TRAMITE SI O SI
					//if (anios_debe == 0)
					//{
					//    lblFechaUltimoTramite.Text = "Fecha del próximo vencimiento : " + fecVto.Value.Day + "/" +
					//                                 fecVto.Value.Month + "/" + fecVto.Value.Year;
					//    lblCantTrsPagas.Text =
					//        "USTED SE ENCUENTRA AL DÍA. DEBE REALIZAR EL REEMPADRONAMIENTO POSTERIOR A LA FECHA DE VENCIMIENTO.";
					//    divBotonImpirmirTRS.Visible = true;
					//    divSeccionDebeTrs.Visible = false;
					//    return;
					//}

					ObjetoInscripcion.FechaVencimiento = fecVto.Value.AddYears(anios_debe);
					//fecUltVto = ObjetoInscripcion.FechaVencimiento.AddYears(-1);
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
					anios_debe = ts.Days / 365;
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

				divVentanaPasosTramite.Visible = false;
				divVentanaInicioTramite.Visible = true;
				txtCuit.Focus();
			}
		}

		protected void gvProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

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

		//protected void btnCargarDomicilio1_OnClick(object sender, EventArgs e)
		//{

		//    habilitarCampoFormularioDomicilio1(true);
		//    IdVinDomicilio1 = null;

		//}

		//protected void btnCargarDomicilio2_OnClick(object sender, EventArgs e)
		//{

		//    habilitarCampoFormularioDomicilio2(true);
		//    IdVinDomicilio2 = null;
		//    //MostrarOcultarModalDomicilio2(true);
		//}

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

			if (NumeroDePasoActual != NumeroPasoEnum.CuartoPaso)
			{
				return;
			}
			CargarDatosObjInscripcion(NumeroPasoEnum.CuartoPaso);

			var tipo_tramite_reimpresion = (String)Session["VENGO_DESDE_REIMPRESION_TIPO_TRAMITE_REIMPRESION"];

			//1 - ImprimirTRS();  
			if (tipo_tramite_reimpresion == "TRS_PAGA")
			{
				ImprimirTRSAlta();
			}

			//2 - GuardarInscripcionSifcos();
			GuardarInscripcionSifcos();
			//3 - mostrar modal de mensaje de Felicitaciones Finalización del trámite.

			//4 - ImprimirReporte. reportViewer
			ImprimirReporteTramite();


			//MostrarOcultarModalFelicitacionesFin(true);


			//if (ObjetoInscripcion.TipoTramiteNum == TipoTramiteEnum.Instripcion_Sifcos)
			//{
			//    Session["NroTransaccionParaImprimir"] = NroTransaccionTasa_Inscripcion;
			//}
			//else
			//{
			//    Session["NroTransaccionParaImprimir"] = "noImprimir";
			//}


			divVentanaInicioTramite.Visible = true;
			divVentanaPasosTramite.Visible = false;
			//MostrarOcultarModalFinalizacionTramite(false); //no me lo toma porque está fuera del updatePanel del boton Cnfirmar.
			MostrarOcultarModalFelicitacionesFin(true);
			LimpiarSessionesEnMemoria();
			Session["VENGO_DESDE_REIMPRESION"] = null;
			Session["VENGO_DESDE_REIMPRESION_CUIT"] = null;
			Session["VENGO_DESDE_REIMPRESION_NRO_SIFCOS"] = null;
			Session["VENGO_DESDE_REIMPRESION_NRO_ULTIMO_TRAMITE"] = null;



			Response.Redirect("MisTramites.aspx?Exito=1");
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

		private void LimpiarSessionesEnMemoria()
		{
			IdVinDomicilio1 = null;
			IdVinDomicilio2 = null;
			NroTramiteAImprimir = 0;
			Session["NroTransaccionTasa_Inscripcion"] = null;
			//Session["ReporteGeneral"] = null;
			ObjetoInscripcion = null;
			DtDomicilios1 = null;
			DtDomicilios2 = null;
			DtActividades = null;
			DtBarrios = null;
			DtComunicaciones = null;
			DtEmpresa = null;
			DtGrilla = null;
			SessionBarrios = null;
			SessionDepartamentos = null;
			SessionLocalidades = null;
			SessionTramitesDelCuit = null;
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

		private void ImprimirReporteTramite()
		{

			var lista = Bl.GetInscripcionSifcosDto(NroTramiteAImprimir).ToList();
			if (lista.Count == 0)
				return;
			InscripcionSifcosDto tramiteDto = lista[0];


			tramiteDto.ActividadPrimaria = lblActividadPrimaria.Text;
			tramiteDto.ActividadSecundaria = lblActividadSecundaria.Text;
			tramiteDto.NroHabMunicipal = txtNroHabMun.Text;



			var lis = new List<Producto>();
			DataTable dtProductosTramite = Bl.BlGetProductosTramite(NroTramiteAImprimir.ToString());
			foreach (DataRow row in dtProductosTramite.Rows)
			{
				lis.Add(new Producto { IdProducto = row["idproducto"].ToString(), NProducto = row["nproducto"].ToString() });
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

			var nombreReporteRdlc = "";
			if (Tipo_Tramite_Reimpresion == "TRS_PAGA")
				nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresion.rdlc";
			if (Tipo_Tramite_Reimpresion == "SIN_PAGO_TRS")
				nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresionSinTRS.rdlc";


			var reporte = new ReporteGeneral(nombreReporteRdlc, lis, TipoArchivoEnum.Pdf);

			reporte.AddParameter("parametro_Titulo_reporte", "Comprobante de Trámite - " + Tipo_Doc_Reimpresion);
			reporte.AddParameter("nroTramiteSifcos", tramiteDto.NroTramiteSifcos);
			reporte.AddParameter("paramatro_dom1_departamento", domicilio1.Departamento.Nombre);
			reporte.AddParameter("paramatro_dom1_localidad", domicilio1.Localidad.Nombre);
			reporte.AddParameter("paramatro_dom1_barrio", domicilio1.Barrio.Nombre);
			reporte.AddParameter("paramatro_dom1_calle", domicilio1.Calle.Nombre);
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
			reporte.AddParameter("paramatro_dom2_departamento", domicilio2.Departamento.Nombre);
			reporte.AddParameter("paramatro_dom2_localidad", domicilio2.Localidad.Nombre);
			reporte.AddParameter("paramatro_dom2_barrio", domicilio2.Barrio.Nombre);
			reporte.AddParameter("paramatro_dom2_calle", domicilio2.Calle.Nombre);
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
			reporte.AddParameter("parametro_fecha_vencimiento", "Sin Vencimiento");

			//cargo los datos del gestor y responsable
			reporte.AddParameter("parametro_gestor_nombre", tramiteDto.NombreYApellidoGestor);
			reporte.AddParameter("parametro_gestor_dni", tramiteDto.DniGestor);
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

		//[System.Web.Script.Services.ScriptMethod()]
		//[System.Web.Services.WebMethod]
		//public static string[] BuscarCalles(string prefixText, int count)
		//{
		//    List<Calles> _calles = Bl_static.BlGetCalles(prefixText.ToUpper()).ToList();

		//    string[] lista = new string[_calles.Count];
		//    var contador = 0;
		//    foreach (var row_calles in _calles)
		//    {
		//        lista[contador] = AutoCompleteExtender.CreateAutoCompleteItem(row_calles.NCalle, row_calles.IdCalle);
		//        contador++;
		//    }

		//    return lista;
		//}

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

		//protected void chkBarrioNoExiste_OnCheckedChanged(object sender, EventArgs e)
		//{
		//    if (chkBarrioNoExiste.Checked)
		//    {
		//        if (ddlBarrios.SelectedItem != null)
		//        {
		//            ddlBarrios.SelectedValue = "0";
		//        }

		//        ddlBarrios.Enabled = false;
		//        txtBarrio.Enabled = true;
		//        txtBarrio.Focus();
		//    }
		//    else
		//    {
		//        ddlBarrios.Enabled = true;
		//        txtBarrio.Text = string.Empty;
		//        txtBarrio.Enabled = false;
		//    }
		//}

		//protected void chkBarrioLegalNoExiste_OnCheckedChanged(object sender, EventArgs e)
		//{
		//    if (chkBarrioLegalNoExiste.Checked)
		//    {
		//        if (ddlBarriosLegal.SelectedItem != null)
		//        {
		//            ddlBarriosLegal.SelectedValue = "0";
		//        }
		//        ddlBarriosLegal.Enabled = false;
		//        txtBarrioLegal.Enabled = true;
		//        txtBarrioLegal.Focus();
		//    }
		//    else
		//    {
		//        ddlBarriosLegal.Enabled = true;
		//        txtBarrioLegal.Text = string.Empty;
		//        txtBarrioLegal.Enabled = false;
		//    }
		//}

		protected void btnBuscarRepresentante_Click(object sender, EventArgs e)
		{
			ObtenerUsuarioRepresentante();
			//StringBuilder javaScript = new StringBuilder();

			//javaScript.Append(" var val1 = $('#ConenedorPrincipal_ddlTipoGestor option:selected').val(); ");
			//javaScript.Append("  if (val1 == \"3\") { "); 
			//javaScript.Append(" $(\"#divMatriculaContador\").show(); ");
			//javaScript.Append(" } else {  ");
			//javaScript.Append(" $(\"#divMatriculaContador\").hide(); ");
			//javaScript.Append(" } ");
			//javaScript.Append(" if (val1 == \"1\") { ");
			//javaScript.Append(" $(\"#divRepresentanteLegal\").hide(); ");
			//javaScript.Append(" } else { ");
			//javaScript.Append(" $(\"#divRepresentanteLegal\").show(); ");
			//javaScript.Append(" } ");
			//ScriptManager.RegisterClientScriptBlock(upRepresentanteLegal, typeof(UpdatePanel), upRepresentanteLegal.ClientID, "ejecutarTipoGestior()", true); 
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

		protected void txtBuscarProducto_TextChanged(object sender, EventArgs e)
		{

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

		protected void btnEnviar_Onclick(object sender, EventArgs e)
		{
			if (NumeroDePasoActual != NumeroPasoEnum.CuartoPaso)
			{
				return;
			}
			CargarDatosObjInscripcion(NumeroPasoEnum.CuartoPaso);

			//1 - IMPRIMIR TASA
			ImprimirTRSAlta();

			//2 - GuardarInscripcionSifcos();
			GuardarInscripcionSifcos();

			//3 - ImprimirReporte. reportViewer
			ImprimirReporteTramite();


			//MostrarOcultarModalFelicitacionesFin(true);



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

		protected void btnSalir2_OnClick(object sender, EventArgs e)
		{
			LimpiarSessionesEnMemoria();
			LimpiarControlesRequeridos();
			Response.Redirect("Inscripcion.aspx");
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
					UrlDomComercio = Helper.getURLDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);

					DomComercio = Helper.getDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);
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


					idVinComercio = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);
					if (idVinComercio == 0)
					{
						sessionBase = new HttpSessionStateWrapper(Page.Session);
						Requestwrapper = new HttpRequestWrapper(Request);
						UrlDomComercio = Helper.AltaDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);
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


				idVinComercio = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);


				if (idVinComercio == 0)
				{
					sessionBase = new HttpSessionStateWrapper(Page.Session);
					Requestwrapper = new HttpRequestWrapper(Request);
					UrlDomComercio = Helper.AltaDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);
				}
				else
				{
					UrlDomComercio = Helper.getURLDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);
				}

				DomComercio = Helper.getDomicilio(sessionBase, Requestwrapper, "SIF" + ObjetoInscripcion.CUIT);
				if (DomComercio != null)
				{
					if (DomComercio.IdVin != 0)
					{
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
			if (opcion == 1)
			{
				if (ConfigurationManager.AppSettings["Id_Tipo_DocumentoDenuncia"] != null)
					RequestPost.Id_Catalogo = Convert.ToInt32(MapeadorWebApi.Id_Tipo_Documento);
				else
					RequestPost.Id_Catalogo = 0;


				RequestPost.N_Documento = "DocumentaciónDenuncia_" + ObjetoInscripcion.CUIT; //documento.PostedFile.FileName;
				RequestPost.Extension = "pdf";
			}


			if (opcion == 2)
			{
				if (ConfigurationManager.AppSettings["Id_Tipo_DocumentoFotoOblea"] != null)
					RequestPost.Id_Catalogo = Convert.ToInt32(MapeadorWebApi.Id_Tipo_Documento);
				else
					RequestPost.Id_Catalogo = 0;


				RequestPost.N_Documento = "DocumentaciónFotoOblea_" + ObjetoInscripcion.CUIT; //documento.PostedFile.FileName;
				RequestPost.Extension = "pdf";
			}
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