using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using BL_SIFCOS;
using DA_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;


namespace SIFCOS
{
    public partial class ABMRoles : System.Web.UI.Page
    {

        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        protected DataTable DtConsulta = new DataTable();

        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();
        public String RolUsuario;
        public String UltimoAcceso;
        protected DataTable DtGrilla = new DataTable();
        public Principal master;
        protected void Page_Load(object sender, EventArgs e)
        {

            master = (Principal)Page.Master;
            var lstRolesNoAutorizados = new List<string>();
            //lstRolesNoAutorizados.Add("Secretaria de comercio");
            lstRolesNoAutorizados.Add("Boca de Recepcion");
            lstRolesNoAutorizados.Add("Gestor");//usuario comun;
            lstRolesNoAutorizados.Add("Sin Asignar");

            if (lstRolesNoAutorizados.Contains(master.RolUsuario))
            {
                Response.Redirect("Inscripcion.aspx");
            }


            if (!IsPostBack)
            {
                 
                MostrarOcultarDiv(false);
                cargarComboRolesConsulta();
                cargarComboOrdenConsulta();
                divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;
            }
        }

        private string EntidadSeleccionada
        {
            get
            {
                return (string)Session["EntidadSeleccionada"];
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
            MostrarOcultarModalCambiarRol(false);

        }
        protected void gvResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    var consultaTramite = (consultaTramite)e.Row.DataItem;

           
            //}


        }
        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;
            RefrescarGrilla();
        }
        private void RefrescarGrilla()
        {
            var orden_consulta = ddlOrdenConsulta.SelectedIndex != 0 ? ddlOrdenConsulta.SelectedIndex.ToString() : "1";
            var rol = ddlRolesConsulta.SelectedItem.Text == "SELECCIONE UN ROL"
                ? ""
                : ddlRolesConsulta.SelectedItem.Text;
            List<Roles> ListaRoles = Bl.BlGetListaRolesUsuarios(rol, txtCuil.Text, orden_consulta);
            Session["ListaRoles"] = ListaRoles;

            var listaRoles = (List<Roles>)Session["ListaRoles"];
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

            if (listaRoles.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double)(listaRoles.Count / (double)gvResultado.PageSize)).ToString());
                gvResultado.PagerSettings.Visible = false;
                gvResultado.DataSource = listaRoles;
                gvResultado.DataBind();
                lblTitulocantRegistros.Visible = true;
                lblTotalRegistrosGrilla.Text = listaRoles.Count.ToString();
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
            if (string.IsNullOrEmpty(txtCuil.Text) && ddlRolesConsulta.SelectedItem.Text == "SELECCIONE UN ROL")
            {
                lblMensajeError.Text = "Debe ingresar al menos un filtro de búsqueda.";
                divMensajeError.Visible = true;

            }
            else
            {
                
                HabilitarDesHabilitarCampos(false);
               /***/
                RefrescarGrilla();
                ddlRolesConsulta.Enabled = false;
                btnConsultar.Enabled = false;
                divMensajeError.Visible = false;
            }
            

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
        protected void btnMostrarGuardar_OnClick(object sender, EventArgs e)
        {
            //   DivModalGuardar.Visible = true;
        }
        protected void btnSalir_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("BocaRecepcion.aspx");
        }
        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("BocaRecepcion.aspx");
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
        protected Usuario ObtenerUsuarioHojaRuta(String cuil)
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
        private int calcularIndexPagina(int indexActual)
        {
            //PORQUE LA PAGINACIÓN ESTÁ SETEADA EN 10 REGISTROS POR PAGINA.

            var resto = indexActual % gvResultado.PageSize;
            var paginaActual = (indexActual - resto) / gvResultado.PageSize;
            return paginaActual;
        }
        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "CambiarRol", "EliminarRol" };
            if (!acciones.Contains(e.CommandName))
                return;
            gvResultado.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //var indexPaginado = calcularIndexPagina(gvResultado.SelectedIndex);// calculo el indice que corresponse según la paginación seleccionada de la grilla en la que estemos.


            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = gvResultado.SelectedValue.ToString();

            switch (e.CommandName)
            {
                case "CambiarRol":
                    cargarComboRoles();
                    cargarInfoModificar();
                    MostrarOcultarModalCambiarRol(true);
                    break;
                case "EliminarRol":
                    cargarInfoEliminar();
                    MostrarOcultarModalEliminarRol(true);
                    break;



            }
        }

        protected void cargarInfoModificar()
        {
          
            var listaRoles = (List<Roles>)Session["ListaRoles"];
            foreach (Roles rol in listaRoles)
            {
                if (rol.Cuil == EntidadSeleccionada.ToString())
                {
                    txtDivCuil.Text = rol.Cuil;
                    txtDivApeYNom.Text = rol.NomYApe;
                    txtDivFechaUltAcceso.Text = rol.FecUltAcceso;
                    txtDivRol.Text = rol.Rol;
                }
            } 
        }

        protected void cargarInfoEliminar()
        {
            var listaRoles = (List<Roles>)Session["ListaRoles"];
            foreach (Roles rol in listaRoles)
            {
                if (rol.Cuil == EntidadSeleccionada.ToString())
                {
                    txtDiv2Cuil.Text = rol.Cuil;
                    txtDiv2ApeYNom.Text = rol.NomYApe;
                    txtDiv2FechaUltAcceso.Text = rol.FecUltAcceso;
                    txtDiv2Rol.Text = rol.Rol;
                }
            }
          


        }

        protected void cargarComboOrdenConsulta()
        {
            ddlOrdenConsulta.Items.Clear();
            ddlOrdenConsulta.Items.Insert(0, "SELECCIONE UNA OPCION");
            ddlOrdenConsulta.Items.Insert(1, "CUIL");
            ddlOrdenConsulta.Items.Insert(2, "ROL");
            ddlOrdenConsulta.Items.Insert(3, "FECHA ULTIMO ACCESO");
            ddlOrdenConsulta.Items.Insert(4, "APELLIDO Y NOMBRE");
            

        }



        protected void cargarComboRoles()
        {
            DataTable dt = new DataTable();
            dt = Bl.BlGetRoles();
            ddlRoles.DataSource = dt;
            ddlRoles.DataTextField = "n_rol";
            ddlRoles.DataValueField = "id_rol";
            ddlRoles.DataBind();

        }

        protected void cargarComboRolesInsertar()
        {
            DataTable dt = new DataTable();
            dt = Bl.BlGetRoles();
            ddlRolesInsertar.DataSource = dt;
            ddlRolesInsertar.DataTextField = "n_rol";
            ddlRolesInsertar.DataValueField = "id_rol";
            ddlRolesInsertar.DataBind();

        }

        protected void cargarComboRolesConsulta()
        {
            DataTable dt = new DataTable();
            dt = Bl.BlGetRoles();
            ddlRolesConsulta.DataSource = dt;
            ddlRolesConsulta.DataTextField = "n_rol";
            ddlRolesConsulta.DataValueField = "id_rol";
            ddlRolesConsulta.DataBind();
            ddlRolesConsulta.Items.Insert(0, "SELECCIONE UN ROL");
        }
      
        private void CambiarEstado(EstadoAbmcEnum estado)
        {
            switch (estado)
            {
                case EstadoAbmcEnum.CONSULTANDO:
                    divPantallaConsulta.Visible = true;
                    divPantallaResultado.Visible = true;
                    EstadoVista = EstadoAbmcEnum.CONSULTANDO;
                    break;
                case EstadoAbmcEnum.REGISTRANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    EstadoVista = EstadoAbmcEnum.REGISTRANDO;
                    HabilitarDesHabilitarCampos(true);
                    LimpiarCamporFormuario();
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.EDITANDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    EstadoVista = EstadoAbmcEnum.EDITANDO;
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;

                case EstadoAbmcEnum.VIENDO:
                    divPantallaConsulta.Visible = false;
                    divPantallaResultado.Visible = false;
                    EstadoVista = EstadoAbmcEnum.VIENDO;
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = false;
                    break;
                case EstadoAbmcEnum.ELIMINANDO:
                    divPantallaConsulta.Visible = false;
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
            txtCuil.Enabled = valor;
            
            
        }
        private void MostrarOcultarDiv(bool valor)
        {
            divMensajeError.Visible = valor;
            divMensajeExito.Visible = valor;
            divMensaejeErrorModal.Visible = valor;

        }
       
      
       
        public DataTable DtProductosTramite
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
        public long NroTramite
        {
            get
            {
                var l = Session["NroTramite"] == null ? new long?() : (long)Session["NroTramite"];
                if (l != null)
                    return (long)l;
                return 0;
            }
            set
            {
                Session["NroTramite"] = value;
            }
        }
        private void MostrarOcultarModalCambiarRol(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalCambiarRolesUsuario.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalCambiarRolesUsuario.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalCambiarRolesUsuario.Attributes.Add("class", String.Join(" ", modalCambiarRolesUsuario
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultarModalEliminarRol(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalEliminarRolUsuario.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalEliminarRolUsuario.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalEliminarRolUsuario.Attributes.Add("class", String.Join(" ", modalEliminarRolUsuario
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultarModalAgregarUsuario(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalAgregarNuevoUsuario.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalAgregarNuevoUsuario.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalAgregarNuevoUsuario.Attributes.Add("class", String.Join(" ", modalAgregarNuevoUsuario
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }
        
        
        protected void btnCancelarEstado_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalCambiarRol(false);
        }
        
        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
            DtConsulta = new DataTable();
            gvResultado.DataSource = null;
            gvResultado.DataBind();
            txtCuil.Text = "";
            cargarComboRolesConsulta();
            HabilitarDesHabilitarCampos(true);
            divPantallaResultado.Visible = false;
            btnConsultar.Enabled = true;
            MostrarOcultarDiv(false);
            ddlRolesConsulta.Enabled = true;
        }
        
        protected void btnLimpiarConsulta_OnClick(object sender, EventArgs e)
        {
            DtConsulta = new DataTable();
            gvResultado.DataSource = null;
            gvResultado.DataBind();
            txtCuil.Text = "";
            cargarComboRolesConsulta();
            HabilitarDesHabilitarCampos(true);
            txtCuil.Focus();
            btnConsultar.Enabled = true;
            MostrarOcultarDiv(false);
            
            divPantallaConsulta.Visible = true;
            ddlRolesConsulta.Enabled = true;


        }
       
        protected void btnVolver_OnClick(object sender, EventArgs e)
        {
            
            divPantallaResultado.Visible = true;
            divPantallaConsulta.Visible = true;
        }


        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
           
            String resultado = Bl.BlActualizarRolUsuario(EntidadSeleccionada.ToString(), long.Parse( ddlRoles.SelectedValue));
            MostrarOcultarModalCambiarRol(false);
            if (resultado == "OK")
            {
                lblMensajeExito.Text = "Se registró el cambio de rol con éxito.";
                divMensajeExito.Visible = true;
                ddlRoles.Items.Clear();
                cargarComboRoles();
                
            }
            else
            {
                lblMensajeError.Text = "Se produjo un error inesperado en el cambio de rol del usuario. Vuelva a intentar mas tarde...";
                divMensajeError.Visible = true;

            }
            RefrescarGrilla();
        }
        

        protected void btnConfirmarEliminar_OnClick(object sender, EventArgs e)
        {
            String resultado = Bl.BlEliminarRolUsuario(EntidadSeleccionada.ToString());
            MostrarOcultarModalEliminarRol(false);
            if (resultado == "OK")
            {
                lblMensajeExito.Text = "Se eliminó el rol del usuario con éxito.";
                divMensajeExito.Visible = true;
                ddlRoles.Items.Clear();
                cargarComboRoles();

            }
            else
            {
                lblMensajeError.Text = "Se produjo un error inesperado en la eliminación del rol del usuario. Vuelva a intentar mas tarde...";
                divMensajeError.Visible = true;

            }
            RefrescarGrilla();
        }

        protected void btnCancelarEliminar_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalEliminarRol(false);
        }

        protected void btnNuevoRol_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalAgregarUsuario(true);
            cargarComboRolesInsertar();
        }

        protected void btnCancelarNuevoUsuario_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalAgregarUsuario(false);
        }

        protected void btnAgregarNuevoUsuario_OnClick(object sender, EventArgs e)
        {
            String resultado = Bl.BlAgregarUsuario(txtCuilBuscar.Text,long.Parse(ddlRolesInsertar.SelectedValue));
            MostrarOcultarModalAgregarUsuario(false);
            switch (resultado)
            {case "OK":
                        lblMensajeExito.Text = "Se registró el usuario con éxito.";
                        divMensajeExito.Visible = true;
                        ddlRolesInsertar.Items.Clear();
                        cargarComboRolesInsertar();
                        break;
            case "USUARIO YA TIENE ROL ASIGNADO":
                        lblMensajeExito.Text = "El usuario ya tiene Rol Asignado.";
                        divMensajeExito.Visible = true;
                        break;
            default:
                        lblMensajeError.Text = "Se produjo un error inesperado al insertar nuevo usuario. Vuelva a intentar mas tarde...";
                        divMensajeError.Visible = true;
                        break;
                    
            }
            
        }
    }
}