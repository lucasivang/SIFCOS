using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using BL_SIFCOS;
using DA_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;

namespace SIFCOS
{
    public partial class ReporteTotalizados : System.Web.UI.Page
    {
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        protected DataTable DtConsulta = new DataTable();

        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public Principal master;
        protected void Page_Load(object sender, EventArgs e)
        {
            master = (Principal)Page.Master;
            var lstRolesNoAutorizados = new List<string>();
            lstRolesNoAutorizados.Add("Boca de Recepcion");
            lstRolesNoAutorizados.Add("Gestor");//usuario comun;
            lstRolesNoAutorizados.Add("Sin Asignar");

            divMensajeError.Visible = false;
            divMensajeExito.Visible = false;
            if (lstRolesNoAutorizados.Contains(master.RolUsuario))
            {
                Response.Redirect("Inscripcion.aspx");
            }
            tramiteDto = new InscripcionSifcosDto();
            if (!IsPostBack)
            {
                CargarInfoBoca();
                divPantallaInfoBoca.Visible = true;
                divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;
                cargarComboTipoConsulta();
            }

        }
        private void CargarInfoBoca()
        {
            DataTable dtInfoBoca = Bl.BlGetInfoBoca(UsuarioCidiLogueado.CUIL);
            if (dtInfoBoca.Rows.Count == 0)
            {
                Session["PaginaMensaje_TipoMensajeMostrar"] = TipoMensajeMostrar.Mensaje_de_Error;
                Session["PaginaMensaje_Mensaje"] = "El usuario logueado " + UsuarioCidiLogueado.NombreFormateado + " no se encuentra vinculado a una Boca de Recepción";
                Session["PaginaMensaje_UrlDondeVolver"] = "Inscripcion.aspx";
                Response.Redirect("PaginaMensaje.aspx");
            }

            txtBocaRecepcion.Text = dtInfoBoca.Rows[0]["boca_recepcion"].ToString();
            txtLocalidadBoca.Text = dtInfoBoca.Rows[0]["localidad"].ToString();
            txtDependencia.Text = dtInfoBoca.Rows[0]["dependencia"].ToString();
        }
        public InscripcionSifcosDto tramiteDto
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


        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFechaDesde.Text) && !string.IsNullOrEmpty(txtFechaHasta.Text))
            {
                if (DateTime.ParseExact(txtFechaDesde.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(txtFechaHasta.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    lblMensajeError.Text = "La fecha de inicio no puede superar a la fecha fin.";
                    divMensajeError.Visible = true;
                    return;
                }
            }
            
           
            if (ddlTipoConsulta.SelectedIndex == 0)
            {
                lblMensajeError.Text = "Debe seleccionar un tipo de consulta.";
                divMensajeError.Visible = true;
                return;
            }
             
            DataTable resultadoConsulta = null;
            

            switch (ddlTipoConsulta.SelectedIndex)
            {
                case 1://POR ESTADO
                    resultadoConsulta = Bl.BlGetTramitesPorEstado(txtFechaDesde.Text, txtFechaHasta.Text);
                    break;
                case 2://POR DEPARTAMENTO / LOCALIDAD
                     resultadoConsulta = Bl.BlGetTramitesPorlocalidad(txtFechaDesde.Text, txtFechaHasta.Text);
                    break;
                case 3://POR TIPO DE TRAMITE
                    resultadoConsulta = Bl.BlGetTramitesPorTipoTramite(txtFechaDesde.Text, txtFechaHasta.Text);
                    break;
                case 4://POR ACTIVIDAD
                    resultadoConsulta = Bl.BlGetTramitesPorActividad(txtFechaDesde.Text, txtFechaHasta.Text);
                    break;
            }
            Int64 total=0; 
            foreach (DataRow row in resultadoConsulta.Rows)
            {
                if(resultadoConsulta.Rows.Count>0)
                {
                    total += Int64.Parse(row["CANTIDAD"].ToString());
                }
            }
            lblTotalRegistrosGrilla.Text = total.ToString();

            gvResultado.DataSource = resultadoConsulta;
            gvResultado.DataBind();
            divPantallaResultado.Visible = true;
        }

        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
            DtConsulta = new DataTable();
            gvResultado.DataSource = null;
            gvResultado.DataBind();
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            divPantallaResultado.Visible = false;
            btnConsultar.Enabled = true;
            
        }
        protected void cargarComboTipoConsulta()
        {
            ddlTipoConsulta.Items.Clear();
            ddlTipoConsulta.Items.Insert(0, "SELECCIONE UNA OPCION");
            ddlTipoConsulta.Items.Insert(1, "POR ESTADO");
            ddlTipoConsulta.Items.Insert(2, "POR DEPARTAMENTO / LOCALIDAD");
            ddlTipoConsulta.Items.Insert(3, "POR TIPO DE TRAMITE");
            ddlTipoConsulta.Items.Insert(4, "POR ACTIVIDAD");
            

        }


        protected void btnPDF_OnClick(object sender, EventArgs e)
        {
            
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            
        }
    }
}