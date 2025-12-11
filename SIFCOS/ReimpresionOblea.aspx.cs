using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL_SIFCOS;
using DA_SIFCOS.Entidades;

namespace SIFCOS
{
    public partial class ReimpresionOblea : System.Web.UI.Page
    {
        private bool banderaPrimeraCargaPagina = false;
        private string commandoBotonPaginaSeleccionado = "";
        private String pCuit = "";
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();
        public Principal master;

        protected void Page_Load(object sender, EventArgs e)
        {
            master = (Principal)Page.Master;
            var lstRolesNoAutorizados = new List<string>();

            lstRolesNoAutorizados.Add("Sin Asignar");

            if (lstRolesNoAutorizados.Contains(master.RolUsuario))
            {
                Response.Redirect("Inscripcion.aspx");
            }

            divMensajeErrorReimpresion.Visible = false;
            divMensajeExitoReimpresion.Visible = false;
            
            if (!Page.IsPostBack)
            {
                cargarComboDocumento();
                cargarComboTipoTramite();
                LimpiarControles();
                ObjetoInscripcion = new InscripcionSifcos();
                CargarDatosUsuarioLogueado();
                gvResultado.PagerSettings.Visible = false;
                
            }
        }
        private void CargarDatosUsuarioLogueado()
        {
            ObjetoInscripcion.CuilUsuarioCidi = master.UsuarioCidiLogueado.CUIL;
        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            divMensajeErrorReimpresion.Visible = false;
            divMensajeExitoReimpresion.Visible = false;
            ddlTipoDoc.Enabled = false;
            ddlTipoTramite.Enabled = false;
             
            
            pCuit = txtCuit.Text;
            if (ddlTipoTramite.SelectedValue == "1") // tipo de tramite sin emision de TRS
            {
                if (string.IsNullOrEmpty(txtNroSifcos.Text.Trim()))
                {
                    divMensajeErrorReimpresion.Visible = true;
                    lblMensajeError.Text = "Debe ingresar un numero SIFCoS, es un campo obligatorio.";
                    txtNroSifcos.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCuit.Text.Trim()))
                {
                    divMensajeErrorReimpresion.Visible = true;
                    lblMensajeError.Text = "Debe ingresar un CUIT, es un campo obligatorio.";
                    txtCuit.Focus();
                    return;
                }

                var existeBaja = Bl.BlGetTramitesDeBaja(txtNroSifcos.Text,txtCuit.Text.Trim());
                if (existeBaja.Rows.Count > 0)
                {
                    btnReimpresion.Visible = false;
                    divMensajeExitoReimpresion.Visible = false;
                    divMensajeErrorReimpresion.Visible = false;
                    lblResultadoEstadoComercio.Text = "USTED YA REALIZO UN TRAMITE DE BAJA. EL NRO DE TRAMITE DE BAJA ES: " + existeBaja.Rows[0]["NRO_TRAMITE"].ToString();
                    return;
                }
                 
                var ConsultaDirecciones = Bl.BlGetDireccionesSedes(pCuit, int.Parse(txtNroSifcos.Text));
                if (ConsultaDirecciones.Rows.Count > 0)
                {
                    Session["VENGO_DESDE_REIMPRESION_NRO_SIFCOS"] = Int64.Parse(txtNroSifcos.Text);
                     gvResultado.DataSource = ConsultaDirecciones;
                     gvResultado.DataBind();
                     lblTotalRegistrosGrilla.Text = ConsultaDirecciones.Rows.Count.ToString();
                     lblTitulocantRegistros.Visible = true;
                     lblTitulocantRegistros.Focus();
                     seleccionDom.Visible = true;
                     txtUltTramite.Text = gvResultado.Rows[0].Cells[0].Text;
                     txtDomicilio.Text = HttpUtility.HtmlDecode(gvResultado.Rows[0].Cells[2].Text);
                     gvResultado.Visible = false;
                     tituloTramEncontrada.Visible = true;
                     btnReimpresion.Visible = true;
                     var dtEmpresa = Bl.BlGetEmpresa(txtCuit.Text);
                     TituloRazonSocial.Visible = true;
                     lblRazonSocial.Text = dtEmpresa.Rows[0]["RAZON_SOCIAL"].ToString();
                     lblResultadoEstadoComercio.Text = "Para Realizar la solicitud debe hacer click en Solicitar Reimpresion de Oblea/Certificado.";
                 }
                else
                {
                     divMensajeErrorReimpresion.Visible = true;
                     lblMensajeError.Text = "No se encotró el comercio para el CUIT y nro de SIFCoS solicitado.";
                 }
            }
            if (ddlTipoTramite.SelectedValue == "0") // tipo de tramite con emision de TRS
            {
                var ConsultaDirecciones = Bl.BlGetDireccionesSedes(pCuit, 0);
                if (ConsultaDirecciones.Rows.Count > 0)
                {
                    gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double)(ConsultaDirecciones.Rows.Count / (double)gvResultado.PageSize)).ToString());
                    
                    lblTotalRegistrosGrilla.Visible = true;
                    lblTotalRegistrosGrilla.Text = ConsultaDirecciones.Rows.Count.ToString();
                    lblTitulocantRegistros.Visible = true;
                    lblTitulocantRegistros.Focus();
                    
                    rptBotonesPaginacion.Visible = true;
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
                    gvResultado.DataSource = ConsultaDirecciones;
                    gvResultado.DataBind();
                    gvResultado.Columns[0].Visible = false;
                    gvResultado.Visible = true;
                    seleccionDom.Visible = false;
                }
                else
                {
                    divMensajeErrorReimpresion.Visible = true;
                    lblMensajeError.Text = "No se encotró el comercio para el CUIT solicitado.";
                }
                
            }
        }

        protected void ddlTipoTramite_SelectedIndexChanged(object sender, EventArgs e)
        {
            div_nro_sifcos.Visible = ddlTipoTramite.SelectedValue == "1"; //tipo tramite sin impresion de trs
        }

        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "Seleccion" };
            if (!acciones.Contains(e.CommandName))
                return;
            
            gvResultado.SelectedIndex = calcularIndexPagina(Convert.ToInt32(e.CommandArgument));
            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = Int64.Parse(gvResultado.SelectedValue.ToString());

            switch (e.CommandName)
            {
                 case "Seleccion":
                    seleccionDom.Visible = true;
                    txtUltTramite.Text = gvResultado.Rows[gvResultado.SelectedIndex].Cells[1].Text;
                    txtDomicilio.Text = HttpUtility.HtmlDecode(gvResultado.Rows[gvResultado.SelectedIndex].Cells[2].Text);
                    
                    Session["VENGO_DESDE_REIMPRESION_NRO_SIFCOS"] = EntidadSeleccionada;
                    gvResultado.Visible = false;
                    tituloTramSeleccionado.Visible = true;
                    btnReimpresion.Visible = true;
                    TituloRazonSocial.Visible = true;
                    var dtEmpresa = Bl.BlGetEmpresa(txtCuit.Text.Trim());
                    lblRazonSocial.Text = dtEmpresa.Rows[0]["RAZON_SOCIAL"].ToString();
                    btnReimpresion.Visible = true;
                    verificarTipoTramite();
                    rptBotonesPaginacion.Visible = false;
                    break;
            }
        }

        private void verificarTipoTramite()
        {

            switch (ddlTipoDoc.SelectedValue)// 0:oblea 1:certificado 2:oblea y certificado
            {case "0":
                    btnReimpresion.Text = "Solicitud de Oblea";
                    break;
             case "1":
                    btnReimpresion.Text = "Solicitud de Certificado";
                    break;
             case "2":
                    btnReimpresion.Text = "Solicitud de Oblea Y Certificado";
                    break;

            }
            

            
        }

        private void RefrescarGrilla()
        {
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;
           
            pCuit = txtCuit.Text;
            if (string.IsNullOrEmpty(txtNroSifcos.Text))
                txtNroSifcos.Text = "0";
            var Sucursales = Bl.BlGetDireccionesSedes(pCuit, int.Parse(txtNroSifcos.Text));
            if (Sucursales.Rows.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double)(Sucursales.Rows.Count / (double)gvResultado.PageSize)).ToString());
                gvResultado.PagerSettings.Visible = false;
                gvResultado.DataSource = Sucursales;
                gvResultado.DataBind();
                lblTotalRegistrosGrilla.Visible = true;
                lblTitulocantRegistros.Visible = true;
                lblTotalRegistrosGrilla.Text = Sucursales.Rows.Count.ToString();
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
                    btn.BackColor = Color.Gainsboro; //pinto el boton.
                }
                if (btn.CommandArgument == commandoBotonPaginaSeleccionado)
                {
                    //por cada boton pregunto y encuentro el comando seleccionado q corresponde al boton selecionado.
                    btn.BackColor = Color.Gainsboro; //pinto el boton.
                    
                }
                //los demas botones se cargan con el color de fondo blanco por defecto.
            }
            lblTotalRegistrosGrilla.Focus();
        }

        private int calcularIndexPagina(int indexActual)
        {
            //PORQUE LA PAGINACIÓN ESTÁ SETEADA EN 10 REGISTROS POR PAGINA.
            if (indexActual < gvResultado.PageSize)
                return indexActual;
            var resto = indexActual % gvResultado.PageSize;

            return resto;
            //var paginaActual = (indexActual - resto) / gvResultado.PageSize;
            //return paginaActual;
        }


        protected void btnLimpiar_OnClick(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        
        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            LimpiarControles();
            Response.Redirect("Inscripcion.aspx");
        }

        protected void btnReimpresion_OnClick(object sender, EventArgs e)
        {
             
            Tipo_Tramite_Reimpresion = ddlTipoTramite.SelectedValue == "0" //EL TIPO 0 ES CON TRS PAGA
                ? "TRS_PAGA"
                : "SIN_PAGO_TRS";
            switch (ddlTipoDoc.SelectedValue)
            {
                case "0":
                    Tipo_Doc_Reimpresion = "REIMPRESIÓN DE OBLEA";
                    break;
                case "1":
                    Tipo_Doc_Reimpresion = "REIMPRESIÓN DE CERTIFICADO";
                    break;
                case "2":
                    Tipo_Doc_Reimpresion = "REIMPRESIÓN DE OBLEA Y CERTIFICADO";
                    break;
            }

            Session["VENGO_DESDE_REIMPRESION"] = true;
            Session["VENGO_DESDE_REIMPRESION_CUIT"] = txtCuit.Text.Trim();

            Session["VENGO_DESDE_REIMPRESION_TIPO_TRAMITE_REIMPRESION"] = Tipo_Tramite_Reimpresion;

            Session["VENGO_DESDE_REIMPRESION_NRO_ULTIMO_TRAMITE"] = txtUltTramite.Text.Trim();
            Response.Redirect("CargaTramiteReimpresion.aspx");
            
        }
        private void LimpiarControles()
        {
            Session["VENGO_DESDE_REIMPRESION"] = null;
            Session["VENGO_DESDE_REIMPRESION_CUIT"] = null;
            Session["VENGO_DESDE_REIMPRESION_NRO_SIFCOS"] = null;
            Session["VENGO_DESDE_REIMPRESION_NRO_ULTIMO_TRAMITE"] = null;
            Session["VENGO_DESDE_REIMPRESION_TIPO_TRAMITE_REIMPRESION"] = null;

            txtCuit.Text = "";
            txtNroSifcos.Text = "";
            lblResultadoEstadoComercio.Text = "";

            btnReimpresion.Visible = false;
            divMensajeExitoReimpresion.Visible = false;
            divMensajeErrorReimpresion.Visible = false;
            ddlTipoDoc.Enabled = true;
            ddlTipoTramite.Enabled = true;

            lblTotalRegistrosGrilla.Visible = false;
            lblTitulocantRegistros.Visible = false;
            TITULO.Visible = false;
            tituloTramSeleccionado.Visible = false;
            tituloTramEncontrada.Visible = false;
            TituloRazonSocial.Visible = false;
            txtUltTramite.Text = "";
            txtDomicilio.Text = "";
            seleccionDom.Visible = false;
            gvResultado.DataSource = null;
            gvResultado.DataBind();
            GVtrs.DataSource = null;
            GVtrs.DataBind();

            txtCuit.Focus();
            rptBotonesPaginacion.Visible = false;

        }

        protected void cargarComboTipoTramite()
        {
            ddlTipoTramite.Items.Clear();
            ddlTipoTramite.Items.Insert(0, new ListItem("CON Emision de Tasa Retributiva", "0"));
            ddlTipoTramite.Items.Insert(1, new ListItem("SIN Emision de Tasa Retributiva", "1"));
        }

        protected void cargarComboDocumento()
        {
            ddlTipoDoc.Items.Clear();
            ddlTipoDoc.Items.Insert(0, new ListItem("Oblea", "0"));
            ddlTipoDoc.Items.Insert(1, new ListItem("Certificado", "1"));
            ddlTipoDoc.Items.Insert(2, new ListItem("Oblea y Certificado", "2"));
            
        }

        public string NroTransaccionTasa_Reimpresion
        {
            get
            {
                return (string)Session["NroTransaccionTasa_Reimpresion"];
            }
            set
            {
                Session["NroTransaccionTasa_Reimpresion"] = value;
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
        private Int64 EntidadSeleccionada
        {
            get
            {
                return (Int64)Session["EntidadSeleccionada"];
            }
            set
            {
                Session["EntidadSeleccionada"] = value;
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


        protected void rptBotonesPaginacion_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
            gvResultado.PageIndex = nroPagina - 1;
            RefrescarGrilla();
        }

        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;
            RefrescarGrilla();
        }
    }
}