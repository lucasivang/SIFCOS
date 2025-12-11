using BL_SIFCOS;
using DA_SIFCOS;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using DA_SIFCOS.Entidades;


namespace SIFCOS
{
    public partial class ABMGestores : System.Web.UI.Page
    {
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        protected DataTable DtConsulta = new DataTable();

        protected static ReglaDeNegocios Bl = new ReglaDeNegocios();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divPantallaAlta.Visible = false;
                divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            divPantallaConsulta.Visible = true;
            divPantallaResultado.Visible = false;
            divPantallaAlta.Visible = false;

        }

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
         
        }

        protected void btnNuevo_OnClick(object sender, EventArgs e)
        {
            divPantallaConsulta.Visible = false;
            divPantallaResultado.Visible = false;
            divPantallaAlta.Visible = true;
        }

        protected void gvResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            var listaSup = (List<Superficie>)Session["ListaSuperficies"];
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

            if (listaSup.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount = int.Parse(Math.Ceiling((double)(listaSup.Count / (double)gvResultado.PageSize)).ToString());
                gvResultado.PagerSettings.Visible = false;
                gvResultado.DataSource = listaSup;
                gvResultado.DataBind();
                lblTitulocantRegistros.Visible = true;
                lblTotalRegistrosGrilla.Text = listaSup.Count.ToString();
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
            }
            divPantallaResultado.Visible = true;
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

        protected void ObtenerUsuario()
        {
            string urlapi = WebConfigurationManager.AppSettings["CiDiUrl"].ToString();
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["CiDi"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);

            UsuarioCidiLogueado = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Basicos_Domicilio_CUIL, entrada);

            if (UsuarioCidiLogueado.Respuesta.Resultado == Config.CiDi_OK)
            {
                String Resp = UsuarioCidiLogueado.CUIL;
                Session["CidiUser"] = Resp;
                

            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["CiDiUrl"] + "?url=" + ConfigurationManager.AppSettings["Url_Retorno"] + "&app=" + ConfigurationManager.AppSettings["CiDiIdAplicacion"]);
            }
        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            //List<Roles> ListRoles = Bl.BlGetListaRolesUsuarios(txtFiltroProd.Text.ToUpper());
            //Session["ListaRoles"] = ListRoles;
            //RefrescarGrilla();
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

            var btn = (LinkButton)sender;
            //guardo el comando del boton de pagina seleccinoado
            commandoBotonPaginaSeleccionado = btn.CommandArgument;
        }

        protected void rptBotonesPaginacion_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                var btn = (LinkButton)e.Item.FindControl("btnNroPagina");
                if (btn.CommandArgument == "1" && banderaPrimeraCargaPagina == false)
                {
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                if (btn.CommandArgument == commandoBotonPaginaSeleccionado)
                {
                    //por cada boton pregunto y encuentro el comando seleccionado q corresponde al boton selecionado.
                    btn.BackColor = Color.Gainsboro;//pinto el boton.
                }
                //los demas botones se cargan con el color de fondo blanco por defecto.
            }
        }

        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
            DataTable dtLimpiar = new DataTable();
            txtFiltroProd.Text = "";
            gvResultado.DataSource = dtLimpiar;
            gvResultado.DataBind();
            txtFiltroProd.Focus();
            divPantallaResultado.Visible = false;
        }

        protected void btnMostrarGuardar_OnClick(object sender, EventArgs e)
        {
            //   DivModalGuardar.Visible = true;
        }

        protected void btnSalir_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ABMSuperficie.aspx");
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ABMSuperficie.aspx");
        }
    }

    
}