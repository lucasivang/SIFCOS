using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using BL_SIFCOS;
using DA_SIFCOS;
using DA_SIFCOS.Entidades;

namespace SIFCOS
{
    public partial class ABMSuperficie : System.Web.UI.Page
    {
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        protected DataTable DtConsulta=new DataTable();
        
        protected static ReglaDeNegocios Bl = new ReglaDeNegocios();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divMensajeError.Visible = false;
                divMensajeExito.Visible = false;
                divPantallaABM.Visible = false;
                divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;
            }
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

        private EstadoAbmcEnum EstadoVista
        {
            get
            {
                return (EstadoAbmcEnum)Session["EstadoVista"];
            }
            set
            {
                Session["EstadoVista"] = value;
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            divPantallaConsulta.Visible = true;
            divPantallaResultado.Visible = false;
            divPantallaABM.Visible = false;
           
        }

        //protected void btnGuardar_OnClick(object sender, EventArgs e)
        //{
        //    String pN_superficie = txtSuperficie.Text;
        //    String Resultado="ERROR";
        //    if (pN_superficie != "")
        //    {
        //        Resultado = Bl.BlRegistrarSuperficie(pN_superficie.ToUpper());
        //    }
        //    //DivModalGuardar.Visible = false;
        //    if (Resultado != "OK")
        //    {
        //      //  DivModalFracaso.Visible = true;

        //    }
        //    else
        //    {
        //      //  DivModalExito.Visible = true;
        //    }
        //}

        protected void btnNuevo_OnClick(object sender, EventArgs e)
        {
            CambiarEstado(EstadoAbmcEnum.REGISTRANDO);
            TraerIdSuperficie();
        }

        protected void TraerIdSuperficie()
        {
            String idSuperficie = Bl.BlGetIdSuperficie();
            txtIdSuperficie.Text = idSuperficie;
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
            var listaSup = (List<Superficie>) Session["ListaSuperficies"];
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

            if (listaSup.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount = int.Parse(Math.Ceiling((double) (listaSup.Count/(double) gvResultado.PageSize)).ToString());
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

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            List<Superficie> ListSup = Bl.BlGetSuperficies(txtFiltroProd.Text.ToUpper());
            Session["ListaSuperficies"] = ListSup;
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
            DataTable dtLimpiar=new DataTable();
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

       

        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "Editar", "Eliminar", "Ver" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvResultado.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            EntidadSeleccionada = 0;

            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "Ver":
                    VerEntidad(EstadoAbmcEnum.VIENDO);
                    break;

                case "Editar":
                    //EditarEntidad(EstadoAbmcEnum.EDITANDO);
                    break;

                case "Eliminar":
                    //EliminarEntidad(EstadoAbmcEnum.ELIMINANDO);
                    break;
            }
        }

       
        private void CargarCamposFormulario(Superficie entidad)
        {
            txtSuperficie.Text = entidad.NTipoSuperficie;
        }

        private void CambiarEstado(EstadoAbmcEnum estado)
        {
            switch (estado)
            {
                case EstadoAbmcEnum.CONSULTANDO:
                    divPantallaConsulta.Visible = true;
                    divPantallaResultado.Visible = true;
                    divPantallaABM.Visible = false;
                    EstadoVista = EstadoAbmcEnum.CONSULTANDO;
                    break;
                case EstadoAbmcEnum.REGISTRANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    divPantallaABM.Visible = true;
                    lblTituloPantallaABM.Text = "Registrar Nueva Superficie";
                    EstadoVista = EstadoAbmcEnum.REGISTRANDO;
                    HabilitarDeshabilitarCampos(true);
                    LimpiarCamporFormuario();
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.EDITANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    divPantallaABM.Visible = true;
                    lblTituloPantallaABM.Text = "Editar Superficie";
                    EstadoVista = EstadoAbmcEnum.EDITANDO;
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.VIENDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    divPantallaABM.Visible = true;
                    EstadoVista = EstadoAbmcEnum.VIENDO;
                    lblTituloPantallaABM.Text = "Datos de la Superficie";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.ELIMINANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    divPantallaABM.Visible = true;
                    EstadoVista = EstadoAbmcEnum.ELIMINANDO;
                    lblTituloPantallaABM.Text = "Eliminar Superficie";
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
            }
        }

        private void LimpiarCamporFormuario()
        {
            txtSuperficie.Text = string.Empty;
        }

        private void HabilitarDeshabilitarCampos(bool valor)
        {
            txtSuperficie.Enabled = valor;
        }
        private void VerEntidad(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
           // var entidad = Bl.BlGetSuperficies(EntidadSeleccionada.ToString());
           // CargarCamposFormulario(entidad);
            HabilitarDeshabilitarCampos(false);
        }
    }
}