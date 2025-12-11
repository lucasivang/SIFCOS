using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Services;

using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using BL_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;

namespace SIFCOS
{
    public partial class consultaBDCIDI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static ReglaDeNegocios Bl = new ReglaDeNegocios();

        protected void btnLimpiarPantalla2_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnBuscarRCIVILNOMYAPE_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnConsultaSQL_OnClick(object sender, EventArgs e)
        {
            ResultadoRule result = new ResultadoRule();
            string SQL = "SELECT " + txtSELECT.Text + " FROM " + txtFROM.Text;
            string where= " WHERE " + txtWHERE.Text +" and rownum<50 ";
            if (!string.IsNullOrEmpty(txtWHERE.Text))
            {
                SQL = SQL + where;
            }
            
            var resultado = Bl.BlConsultaSQL(SQL,"2",out result);
            if (resultado.Rows.Count > 0)
            {
                GVResultadoSQL.DataSource = resultado;
                GVResultadoSQL.DataBind();
                lblCantRegistrosSQL.Text = resultado.Rows.Count.ToString();
                lblCantRegistrosSQL.Visible = true;
                lblRegistrosSQL.Visible = true;
            }
        }

        protected void btnLimpiarSQL_OnClick(object sender, EventArgs e)
        {
            GVResultadoSQL.DataSource = null;
            GVResultadoSQL.DataBind();
            txtSELECT.Text = "";
            txtFROM.Text = "";
            txtWHERE.Text = "";
            txtSELECT.Focus();
            lblCantRegistrosSQL.Visible = false;
            lblRegistrosSQL.Visible = false;
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

        
        
        protected Usuario ObtenerUsuarioCuil(String cuil)
        {
            Usuario usu;
            string urlapi = WebConfigurationManager.AppSettings["CiDiUrl"].ToString();
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["CiDi"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);
            entrada.Cuil = cuil;


            UsuarioCidiLogueado = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Basicos_CUIL, entrada);

            if (UsuarioCidiLogueado.Respuesta.Resultado == Config.CiDi_OK)
            {
                usu = UsuarioCidiLogueado;
                return usu;

            }
            return UsuarioCidiLogueado;

        }
        protected void btnImprimir_OnClick(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void Imprimir()
        {
            if (Session["ListaPersonas"] == null)
                return;

            ListPersonas = (List<Persona>)Session["ListaPersonas"];

            var reporte = new ReporteGeneral("SIFCOS.rptReporte.rdlc", ListPersonas, TipoArchivoEnum.Pdf);
            
            Session["ReporteGeneral"] = reporte;
            Response.Redirect("ReporteGeneral.aspx");
        }
        public List<Persona> ListPersonas
        {
            get
            {
                return Session["ListPersonas"] == null ? new List<Persona>() : (List<Persona>)Session["ListPersonas"];
            }
            set
            {
                Session["ListPersonas"] = value;
            }
        }
    }
}