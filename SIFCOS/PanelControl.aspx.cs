using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using BL_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;

namespace SIFCOS
{
    public partial class PanelControl : System.Web.UI.Page
    {
        public static ReglaDeNegocios Bl = new ReglaDeNegocios();
        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarOcultarModalEnConstruccion(true);
            if (!base.IsPostBack)
            {
                Session["IdOrganismoForzado"] = "";
                divOrganismo.Visible = false;
                string text = ObtenerOrganismoUsuarioLogueado();
                if (text == "1")
                {
                    cargarComboBocaRecepcion();
                }
            }
        }
        private void MostrarOcultarModalEnConstruccion(bool mostrar)
        {
            if (mostrar)
            {
                var classname = "mostrarInfoDeudaTramite";
                string[] listaStrings = ModalPaginaConstruccion.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                );
                ModalPaginaConstruccion.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                ModalPaginaConstruccion.Attributes.Add("class", String.Join(" ", ModalPaginaConstruccion
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "mostrarInfoDeudaTramite" })
                    .ToArray()
                ));
            }
        }

        public static string ObtenerOrganismoUsuarioLogueado()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Session["IdOrganismoForzado"].ToString()))
            {
                return HttpContext.Current.Session["IdOrganismoForzado"].ToString();
            }
            if (HttpContext.Current.Session["IdOrganismoUsuarioLogueado"] == null)
            {
                Usuario usuario = (Usuario)HttpContext.Current.Session["UsuarioCiDiLogueado"];
                DataTable dataTable = Bl.BlGetInfoBoca(usuario.CUIL);
                if (dataTable.Rows.Count > 0)
                {
                    if (int.TryParse(dataTable.Rows[0]["id_organismo"].ToString(), out var result))
                    {
                        HttpContext.Current.Session["IdOrganismoUsuarioLogueado"] = result.ToString();
                        return result.ToString();
                    }
                    return "0";
                }
                HttpContext.Current.Session["IdOrganismoUsuarioLogueado"] = "2";
                return "2";
            }
            return HttpContext.Current.Session["IdOrganismoUsuarioLogueado"].ToString();
        }

        public void cargarComboBocaRecepcion()
        {
            divOrganismo.Visible = true;
            DataTable dataTable = new DataTable();
            dataTable = Bl.BlGetOrganismos();
            ddlBocaRecepcion.DataSource = dataTable;
            ddlBocaRecepcion.DataTextField = "n_organismo";
            ddlBocaRecepcion.DataValueField = "id_organismo";
            ddlBocaRecepcion.DataBind();
            ddlBocaRecepcion.Items.Insert(0, new ListItem("SELECCIONE BOCA DE RECEPCION", "0"));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object ObtenerCantAltasHist()
        {
            new List<Exception>();
            try
            {
                int altasHist = Bl.BlObtenerCantAltasHist((int?)Convert.ToInt32(ObtenerOrganismoUsuarioLogueado()));
                return new
                {
                    Result = "OK",
                    AltasHist = altasHist
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Result = "ERROR",
                    Message = ex.Message
                };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object ObtenerCantBajasHist()
        {
            new List<Exception>();
            try
            {
                int bajasHist = Bl.BlObtenerCantBajasHist((int?)Convert.ToInt32(ObtenerOrganismoUsuarioLogueado()));
                return new
                {
                    Result = "OK",
                    BajasHist = bajasHist
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Result = "ERROR",
                    Message = ex.Message
                };
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object ObtenerCantComerciosAct()
        {
            new List<Exception>();
            try
            {
                int comerciosAct = Bl.BlObtenerCantComerciosAct((int?)Convert.ToInt32(ObtenerOrganismoUsuarioLogueado()));
                return new
                {
                    Result = "OK",
                    ComerciosAct = comerciosAct
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Result = "ERROR",
                    Message = ex.Message
                };
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object ObtenerCantReempaPen()
        {
            new List<Exception>();
            try
            {
                int reempaPen = Bl.BlObtenerCantReempaPendientes((int?)Convert.ToInt32(ObtenerOrganismoUsuarioLogueado()));
                return new
                {
                    Result = "OK",
                    ReempaPen = reempaPen
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Result = "ERROR",
                    Message = ex.Message
                };
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object ObtenerReempaPen(int jtStartIndex, int jtPageSize, string pTotal)
        {
            try
            {
                new List<Exception>();
                int totalRecordCount = Convert.ToInt32(pTotal);
                new PanelControl();
                List<tramitePanel> source = Bl.BlObtenerReempaPendientes((int?)Convert.ToInt32(ObtenerOrganismoUsuarioLogueado()), jtStartIndex, jtStartIndex + jtPageSize, "fec_vencimiento ASC");
                var records = from o in source.AsEnumerable()
                              select new { o.CUIT, o.Nro_Sifcos, o.Razon_Social, o.fec_vencimiento, o.Nro_tramite };
                return new
                {
                    Result = "OK",
                    Records = records,
                    TotalRecordCount = totalRecordCount
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Result = "ERROR",
                    Message = ex.Message
                };
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object ObtenerAltasHist(int jtStartIndex, int jtPageSize, string pTotal)
        {
            try
            {
                new List<Exception>();
                int totalRecordCount = Convert.ToInt32(pTotal);
                new PanelControl();
                List<tramitePanel> source = Bl.BlObtenerAltasHist((int?)Convert.ToInt32(ObtenerOrganismoUsuarioLogueado()), jtStartIndex, jtStartIndex + jtPageSize, "fec_alta ASC");
                var records = from o in source.AsEnumerable()
                              select new { o.CUIT, o.Nro_Sifcos, o.Razon_Social, o.fec_alta, o.Nro_tramite };
                return new
                {
                    Result = "OK",
                    Records = records,
                    TotalRecordCount = totalRecordCount
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Result = "ERROR",
                    Message = ex.Message
                };
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object ObtenerBajasHist(int jtStartIndex, int jtPageSize, string pTotal)
        {
            try
            {
                new List<Exception>();
                int totalRecordCount = Convert.ToInt32(pTotal);
                new PanelControl();
                List<tramitePanel> source = Bl.BlObtenerBajasHist((int?)Convert.ToInt32(ObtenerOrganismoUsuarioLogueado()), jtStartIndex, jtStartIndex + jtPageSize, "fec_alta ASC");
                var records = from o in source.AsEnumerable()
                              select new { o.CUIT, o.Nro_Sifcos, o.Razon_Social, o.fec_alta, o.Nro_tramite };
                return new
                {
                    Result = "OK",
                    Records = records,
                    TotalRecordCount = totalRecordCount
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Result = "ERROR",
                    Message = ex.Message
                };
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object ObtenerComerciosAct(int jtStartIndex, int jtPageSize, string pTotal)
        {
            try
            {
                new List<Exception>();
                int totalRecordCount = Convert.ToInt32(pTotal);
                new PanelControl();
                List<tramitePanel> source = Bl.BlObtenerComerciosAct((int?)Convert.ToInt32(ObtenerOrganismoUsuarioLogueado()), jtStartIndex, jtStartIndex + jtPageSize, "fec_vencimiento ASC");
                var records = from o in source.AsEnumerable()
                              select new { o.CUIT, o.Nro_Sifcos, o.Razon_Social, o.fec_vencimiento, o.Nro_tramite };
                return new
                {
                    Result = "OK",
                    Records = records,
                    TotalRecordCount = totalRecordCount
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Result = "ERROR",
                    Message = ex.Message
                };
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
        }

        protected void ddlBocaRecepcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = ddlBocaRecepcion.SelectedItem.Value;
            if (text == "0")
            {
                text = "";
            }
            Session["IdOrganismoForzado"] = text;
        }

        protected void btnPagConstruccion_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }

 
}