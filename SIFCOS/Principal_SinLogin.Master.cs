using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL_SIFCOS;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;


namespace SIFCOS
{
    public partial class Principal_SinLogin : System.Web.UI.MasterPage
    {
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        
        public String UltimoAcceso;
        public String RolUsuario { get; set; }

        /*Nota (Facundo Álvarez) : se inicializa el evento Init porque surge la necesidad de ejecutar primero el evento Init de la master y luego cualquier load de las Paginas que utilizan la master.*/
        protected void Page_Init(object sender, EventArgs e)
        {
            lblVersion.Text = VersionProducto;
            //CargarDatosInicialesGenerales();
        }

        public string VersionProducto
        {
            get
            {
                return ConfigurationManager.AppSettings["VersionProducto"].ToString();
            }
        }

        //private void CargarDatosInicialesGenerales()
        //{
        //    DataTable dt = Bl.BlGetParametrosGral();

        //    var gralEsquema = "";
        //    var gralIdConceptoAlta = "";
        //    var gralIdConceptoReempadronamiento = "";
        //    var gralFecDesdeConceptoAlta = "";
        //    var gralFecDesdeConceptoReempadronamiento = "";
            
        //    var gralUrlGeneracionTrs = "";
        //    var gralMontoTasaAlta = "";
        //    var gralMontoTasaReempadronamiento = "";


        //    foreach (DataRow row in dt.Rows)
        //    {
        //        switch (row["ID_PARAMETRO_GRAL"].ToString())
        //        {
        //            case "1"://P:PRODCCIÓN, D:DESARROLLO 
        //                gralEsquema = row["VALOR"].ToString();
        //                break;
        //            case "8":
        //                gralUrlGeneracionTrs = row["VALOR"].ToString();
        //                break;

        //        }
        //    }

        //    //LLAMADO A CONCEPTOS VIGENTES
        //    List<ConceptoTasa> lconceptos = Bl.BlGetConceptosAFecha(DateTime.Now);
        //    if (lconceptos.Count>0)
        //    {
                
        //        if (lconceptos.Where(x => x.IdTipoConcepto == 1).ToList().Count > 0)
        //        {
        //            gralFecDesdeConceptoAlta = lconceptos.Where(x => x.IdTipoConcepto == 1).FirstOrDefault().fec_desde.ToString("dd/MM/yyyy");
        //            gralIdConceptoAlta = lconceptos.Where(x => x.IdTipoConcepto == 1).FirstOrDefault().id_concepto;
        //            gralMontoTasaAlta = lconceptos.Where(x => x.IdTipoConcepto == 1).FirstOrDefault().precio_base;
        //        }
        //        //Preguntar si tiene valor para 1
        //        if (lconceptos.Where(x => x.IdTipoConcepto == 4).ToList().Count > 0)
        //        {
        //            gralFecDesdeConceptoReempadronamiento = lconceptos.Where(x => x.IdTipoConcepto == 4).FirstOrDefault().fec_desde.ToString("dd/MM/yyyy");
        //            gralIdConceptoReempadronamiento = lconceptos.Where(x => x.IdTipoConcepto == 4).FirstOrDefault().id_concepto;
        //            gralMontoTasaReempadronamiento = lconceptos.Where(x => x.IdTipoConcepto == 4).FirstOrDefault().precio_base;
        //        }
        //    }

        //    /* Creo el objeto Singleton por unica vez y luego se utiliza en todos lados.*/
        //    SingletonParametroGeneral.GetInstance(gralEsquema, gralIdConceptoAlta, gralIdConceptoReempadronamiento,
        //        gralFecDesdeConceptoAlta, gralFecDesdeConceptoReempadronamiento, gralUrlGeneracionTrs,gralMontoTasaAlta,gralMontoTasaReempadronamiento         );
        
        //}
        

        protected void Page_Load(object sender, EventArgs e)
        {
             
        }
        public  Usuario UsuarioCidiLogueado
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

        public Usuario ObtenerUsuarioRepresentante(string cuil)
        {
            
            string urlapi = WebConfigurationManager.AppSettings["CiDiUrl"].ToString();
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["CiDi"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);
            entrada.Cuil = cuil;
            UsuarioCidiRep = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Basicos_CUIL, entrada);
            return UsuarioCidiRep;

        }

        protected void ObtenerUsuario()
        {
            string urlapi = WebConfigurationManager.AppSettings["CiDiUrl"].ToString();
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["CiDi"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);

            

            UsuarioCidiLogueado = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Aplicacion, entrada);

            if (UsuarioCidiLogueado.Respuesta.Resultado == Config.CiDi_OK)
            {
                var getRolUsuario = Bl.BlGetRolUsuario(UsuarioCidiLogueado.CUIL);
	            //var getRolUsuario = "1";
                switch (getRolUsuario)
                {case "0":
                        //Bl.BlActualizarRolUsuario(UsuarioCidiLogueado.CUIL, 0);
                        Bl.BlAgregarUsuario(UsuarioCidiLogueado.CUIL, 4);
                        break;
                 case "1":
                        Bl.BlActualizarRolUsuario(UsuarioCidiLogueado.CUIL, 1);
                        break;
                 case "2":
                        Bl.BlActualizarRolUsuario(UsuarioCidiLogueado.CUIL, 2);
                        break;
                 case "3":
                        Bl.BlActualizarRolUsuario(UsuarioCidiLogueado.CUIL, 3);
                        break;
                 case "4":
                        Bl.BlActualizarRolUsuario(UsuarioCidiLogueado.CUIL, 4);
                        break;
                 case "5":
                        Bl.BlActualizarRolUsuario(UsuarioCidiLogueado.CUIL, 5);
                        break;
                 default:
                    Response.Redirect("SifcosWeb3.aspx");
                    break;
                 }

                if (UsuarioCidiLogueado.Id_Estado != null)
                {
                    int nivelCidi = (int)UsuarioCidiLogueado.Id_Estado;//nivel de cidi '0' no verificado nivel 1 si confirmacion de mail,id=1 nivel 1, id=2 nivel 2 confirmado
                    String estadoCidi = "";
                    switch (nivelCidi)
                    {
                        case 0:
                            estadoCidi = "no confirmado";
                            break;
                        case 1:
                            estadoCidi = "confirmado";
                            break;
                        case 2:
                            estadoCidi = "no verificado";
                            break;
                        case 3:
                            estadoCidi = "verificado";
                            break;
                    }

                    Label varUsuarioCIDI = lblUsuarioCIDI;
                    varUsuarioCIDI.Text = UsuarioCidiLogueado.NombreFormateado + ", Nivel " + nivelCidi + " " + estadoCidi;
                    
                    var rolUsuario = "";
                    Bl.BlActualizarAccesoUsuario(UsuarioCidiLogueado.CUIL, out rolUsuario, out UltimoAcceso);
                   
				   //lt
					//var rolUsuario = "1";

                    //si el USUARIO NO TIENE ASIGNADO UN ROL.
                    if(string.IsNullOrEmpty(rolUsuario))
                        Response.Redirect("SifcosWeb3.aspx");
                    
                    RolUsuario = rolUsuario;

                    //if (UltimoAcceso != "ERROR")
                    //{
                    //    lblUltimoAcceso.Text = UltimoAcceso;
                    //    lblRolUsuario.Text = RolUsuario;
                    //}
                        
                    //switch (RolUsuario)
                    //{
                    //    case "Boca de Recepcion":
                    //        liBocaMinisterio.Attributes.Add("Class", "cerrar");
                    //        liConfiguracion.Attributes.Add("Class", "cerrar");
                    //        //liPanelControl.Attributes.Add("Class","cerrar");
                    //        liLiquidaciones.Attributes.Add("Class", "cerrar");
                    //        liLiberarTasas.Attributes.Add("Class", "cerrar");
                    //        liSistemas.Attributes.Add("Class", "cerrar");
                    //        liRelevamiento.Attributes.Add("Class", "cerrar");
                    //        liConsultaInterna.Attributes.Add("Class", "cerrar");
                    //        break;
                    //    case "Boca de Ministerio":
                    //        liBocaRecepcion.Attributes.Add("Class", "cerrar");
                    //        liConfiguracion.Attributes.Add("Class", "cerrar");
                    //        liMisTramites.Attributes.Add("Class", "cerrar");
                    //        //liPanelControl.Attributes.Add("Class", "cerrar");
                    //        liLiberarTasas.Attributes.Add("Class", "cerrar");
                    //        liSistemas.Attributes.Add("Class", "cerrar");
                    //        liRelevamiento.Attributes.Add("Class", "cerrar");
                    //        liConsultaInterna.Attributes.Add("Class", "cerrar");
                    //        break;
                    //    case "Secretaria de comercio":
                    //        liBocaRecepcion.Attributes.Add("Class", "cerrar");
                    //        liMisTramites.Attributes.Add("Class", "cerrar");
                    //        liSistemas.Attributes.Add("Class", "cerrar");
                    //        liConsultaInterna.Attributes.Add("Class", "cerrar");
                    //        break;
                    //    case "Gestor":
                    //        liBocaRecepcion.Attributes.Add("Class", "cerrar");
                    //        liBocaMinisterio.Attributes.Add("Class", "cerrar");
                    //        liReportes.Attributes.Add("Class", "cerrar");
                    //        liConfiguracion.Attributes.Add("Class", "cerrar");
                    //        liPanelControl.Attributes.Add("Class","cerrar");
                    //        liLiberarTasas.Attributes.Add("Class", "cerrar");
                    //        liSistemas.Attributes.Add("Class", "cerrar");
                    //        liLiquidaciones.Attributes.Add("Class", "cerrar");
                    //        liRelevamiento.Attributes.Add("Class", "cerrar");
                    //        liConsultaInterna.Attributes.Add("Class", "cerrar");
                    //        break;
                    //    case "Administrador General":
                            
                    //        break;
                    //}
                }
                


            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["CiDiUrl"] + "?url=" + ConfigurationManager.AppSettings["Url_Retorno"] + "&app=" + ConfigurationManager.AppSettings["CiDiIdAplicacion"]);
            }
        }

        public void SetTituloPaginaContenido(string valor)
        {
            lblTituloPaginaContenido.Text = valor;
        }

        public string TituloPaginaContenido
        {
            get
            {
                return lblTituloPaginaContenido.Text;
            }
            set
            {
                lblTituloPaginaContenido.Text = value;
            }
        }
    }
}