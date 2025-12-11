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
using System.Web.UI.WebControls;
using BL_SIFCOS;
using CryptoManagerV4._0.Clases;
using CryptoManagerV4._0.General;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace SIFCOS
{
    public partial class ConsultaInterna : System.Web.UI.Page
    {
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        protected DataTable DtConsulta = new DataTable();

        protected static ReglaDeNegocios Bl = new ReglaDeNegocios();
        public Principal master;
		

        protected void Page_Load(object sender, EventArgs e)
        {
           
       
           
			//master = (Principal)Page.Master;
			//var lstRolesNoAutorizados = new List<string>();
			//lstRolesNoAutorizados.Add("Administrador General");
			////lstRolesNoAutorizados.Add("Boca de Recepcion");
			////lstRolesNoAutorizados.Add("Boca de Ministerio");
			////lstRolesNoAutorizados.Add("Secretaria de comercio");
			////lstRolesNoAutorizados.Add("Gestor");//usuario comun;
			//lstRolesNoAutorizados.Add("Sin Asignar");

			//if (!lstRolesNoAutorizados.Contains(master.RolUsuario))
			//{
			//    Response.Redirect("Inscripcion.aspx");
			//}

			tramiteDto = new InscripcionSifcosDto();

            if (!IsPostBack)
            {
                divMensajeError.Visible = false;
                divMensajeExito.Visible = false;
                divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;
               
               
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
		
        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            var listaTramites = (DataTable) Session["ListaTramites"];
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;
       
            if (listaTramites.Rows.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double) (listaTramites.Rows.Count/(double) gvResultado.PageSize)).ToString());
                gvResultado.PagerSettings.Visible = false;
                gvResultado.DataSource = listaTramites;
                gvResultado.DataBind();
                lblTitulocantRegistros.Visible = true;
                lblTotalRegistrosGrilla.Text = listaTramites.Rows.Count.ToString();
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

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
         
            if (UsuarioCidiLogueado.CUIL != "")
            {
                DataTable ListaTramites = Bl.BlGetTramitesSifcosViejo(txtFiltroCuit.Text, txtFiltroNroSifcos.Text, txtFiltroNroTramite.Text);
                Session["ListaTramites"] = ListaTramites;
            }
            
            RefrescarGrilla();
            
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
            txtFiltroCuit.Text = "";
            txtFiltroCuit.Focus();
            divPantallaResultado.Visible = false;
        
        }
		
        protected void btnSalir_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MisTramites.aspx");
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MisTramites.aspx");
        }
		
        private int calcularIndexPagina(int indexActual)
        {
            //PORQUE LA PAGINACIÓN ESTÁ SETEADA EN 10 REGISTROS POR PAGINA.

            var resto = indexActual % gvResultado.PageSize;
            var paginaActual = (indexActual - resto) / gvResultado.PageSize;
            return paginaActual;
        }
		
       
        protected void btnLimpiarConsulta_OnClick(object sender, EventArgs e)
        {
            divPantallaResultado.Visible = false;
            txtFiltroCuit.Text = "";
          
            btnConsultar.Enabled = true;
            divPantallaConsulta.Visible = true;
        }

        protected void btnDescargarComprobante_OnClick(object sender, EventArgs e)
        {
            if (Session["ReporteGeneral"] != null)
                Response.Redirect("ReporteGeneral.aspx");
        }
        
      
    }
}