using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using BL_SIFCOS;
using DA_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;

namespace SIFCOS
{
    public partial class ABMUsuariosYOrganismos : System.Web.UI.Page
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
            //lstRolesNoAutorizados.Add("Administrador General");
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
                cargarComboOrdenConsulta();
                cargarComboOrganismos();
                divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] BuscarOrganismo(string prefixText, int count)
        {
            List<Organismo> _organismo = Bl_static.BlGetOrganismosList(prefixText).ToList();

            string[] lista = new string[_organismo.Count];
            var contador = 0;
            foreach (var row_organismo in _organismo)
            {
                lista[contador] = AutoCompleteExtender.CreateAutoCompleteItem(row_organismo.NOrganismo, row_organismo.IdOrganismo);
                contador++;
            }

            return lista;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] BuscarOrganismoInsertar(string prefixText, int count)
        {
            List<Organismo> _organismo = Bl_static.BlGetOrganismosList(prefixText).ToList();

            string[] lista = new string[_organismo.Count];
            var contador = 0;
            foreach (var row_organismo in _organismo)
            {
                lista[contador] = AutoCompleteExtender.CreateAutoCompleteItem(row_organismo.NOrganismo, row_organismo.IdOrganismo);
                contador++;
            }

            return lista;
        }

        protected void cargarComboOrdenConsulta()
        {
            ddlOrdenConsulta.Items.Clear();
            ddlOrdenConsulta.Items.Add(new ListItem("SELECCIONE UNA OPCION", "0"));
            ddlOrdenConsulta.Items.Add(new ListItem("CUIL", "1"));
            ddlOrdenConsulta.Items.Add(new ListItem("ORGANISMO", "2"));
            ddlOrdenConsulta.Items.Add(new ListItem("FECHA ULTIMO ACCESO", "3"));
            ddlOrdenConsulta.Items.Add(new ListItem("APELLIDO Y NOMBRE", "4"));
            
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

        public Usuario PersonaCIDI
        {
            get
            {
                return Session["PersonaCIDI"] == null ? new Usuario() : (Usuario)Session["PersonaCIDI"];

            }
            set
            {
                Session["PersonaCIDI"] = value;
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

        protected Usuario ObtenerPersonaCIDI(String cuil)
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


            PersonaCIDI = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Basicos_CUIL, entrada);

            if (PersonaCIDI.Respuesta.Resultado == Config.CiDi_OK)
            {
                usu = PersonaCIDI;
                return usu;

            }
            
            return PersonaCIDI;

        }
        private int calcularIndexPagina(int indexActual)
        {
            //PORQUE LA PAGINACIÓN ESTÁ SETEADA EN 10 REGISTROS POR PAGINA.

            var resto = indexActual % gvResultado.PageSize;
            var paginaActual = (indexActual - resto) / gvResultado.PageSize;
            return paginaActual;
        }

       

        protected void btnConfirmarEliminar_OnClick(object sender, EventArgs e)
        {
            String cuil = gvResultado.Rows[EntidadSeleccionada].Cells[0].Text;
            String resultado = Bl.BlEliminarOrganismo(cuil);
            MostrarOcultarmodalEliminarOrgUsuario(false);
            if (resultado == "OK")
            {
                lblMensajeExito.Text = "Se eliminó el Organismo asignado al usuario con éxito.";
                divMensajeExito.Visible = true;
                ddlOrganismos.Items.Clear();
                cargarComboOrganismos2();

            }
            else
            {
                lblMensajeError.Text = "Se produjo un error inesperado en la eliminación del Organismo del usuario. Vuelva a intentar mas tarde...";
                divMensajeError.Visible = true;

            }
        }

        

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            String resultado = Bl.BlActualizarOrgUsuario(txtDivCuil.Text, ddlOrganismos.SelectedIndex + 1);
            MostrarOcultarModalCambiarOrgUsuario(false);
            if (resultado == "OK")
            {
                lblMensajeExito.Text = "Se registró el cambio de Organismo al Usuario con éxito.";
                divMensajeExito.Visible = true;
                ddlOrganismos.Items.Clear();
                cargarComboOrganismos2();

            }
            else
            {
                lblMensajeError.Text = "Se produjo un error inesperado en el cambio de Organismo del Usuario. Vuelva a intentar mas tarde...";
                divMensajeError.Visible = true;

            }
            RefrescarGrilla();
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalCambiarOrgUsuario(false);
        }

       protected void btnNuevaRelacion_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalAgregarNuevaRelacion(true);
            cargarComboOrganismos();
        }
       
        protected void btnCancelarNuevaRelacion_OnClick(object sender, EventArgs e)
        {
            divPantallaConsulta.Visible = true;
            MostrarOcultarModalAgregarNuevaRelacion(false);

        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCuil.Text) && string.IsNullOrEmpty(txtBuscarOrganismo.Text))
            {
                lblMensajeError.Text = "Debe ingresar al menos un filtro de búsqueda.";
                divMensajeError.Visible = true;

            }
            else
            {

                HabilitarDesHabilitarCampos(false);
                /***/
                RefrescarGrilla();
                btnConsultar.Enabled = false;
                divMensajeError.Visible = false;
            }
        }

        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
            DtConsulta = new DataTable();
            gvResultado.DataSource = null;
            gvResultado.DataBind();
            txtCuil.Text = "";
            cargarComboOrganismos();
            HabilitarDesHabilitarCampos(true);
            divPantallaResultado.Visible = false;
            btnConsultar.Enabled = true;
            MostrarOcultarDiv(false);
            ddlOrganismos.Enabled = true;
            ddlOrganismosInsertar.Enabled = true;

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

        

        protected void gvResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }
        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "CambiarOrg", "EliminarOrg" };
            if (!acciones.Contains(e.CommandName))
                return;
            gvResultado.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            //var indexPaginado = calcularIndexPagina(gvResultado.SelectedIndex);// calculo el indice que corresponse según la paginación seleccionada de la grilla en la que estemos.


            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = gvResultado.SelectedIndex;

            switch (e.CommandName)
            {
                case "CambiarOrg":
                    cargarComboOrganismos2();
                    cargarInfoModificar();
                    MostrarOcultarModalCambiarOrgUsuario(true);
                    break;
                case "EliminarOrg":
                    cargarInfoEliminar();
                    MostrarOcultarmodalEliminarOrgUsuario(true);
                    break;



            }
        }

        protected void btnCancelarEliminar_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarmodalEliminarOrgUsuario(false);
        }
        

        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
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

        protected void cargarInfoModificar()
        {
            txtDivCuil.Text = gvResultado.Rows[EntidadSeleccionada].Cells[0].Text;
            txtDivApeYNom.Text = gvResultado.Rows[EntidadSeleccionada].Cells[1].Text;
            txtDivFechaUltAcceso.Text = gvResultado.Rows[EntidadSeleccionada].Cells[2].Text;
            txtDivOrganismo.Text = gvResultado.Rows[EntidadSeleccionada].Cells[3].Text;


        }

        protected void cargarInfoEliminar()
        {
            txtDiv2Cuil.Text = gvResultado.Rows[EntidadSeleccionada].Cells[0].Text;
            txtDiv2ApeYNom.Text = gvResultado.Rows[EntidadSeleccionada].Cells[1].Text;
            txtDiv2FechaUltAcceso.Text = gvResultado.Rows[EntidadSeleccionada].Cells[2].Text;
            txtDiv2Organismo.Text = gvResultado.Rows[EntidadSeleccionada].Cells[3].Text;


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

        protected void cargarComboOrganismos()
        {
            DataTable dt = new DataTable();
            dt = Bl.BlGetOrganismos();
            ddlOrganismosInsertar.DataSource = dt;
            ddlOrganismosInsertar.DataTextField = "n_organismo";
            ddlOrganismosInsertar.DataValueField = "id_organismo";
            ddlOrganismosInsertar.DataBind();

        }

        protected void cargarComboOrganismos2()
        {
            DataTable dt = new DataTable();
            dt = Bl.BlGetOrganismos();
            ddlOrganismos.DataSource = dt;
            ddlOrganismos.DataTextField = "n_organismo";
            ddlOrganismos.DataValueField = "id_organismo";
            ddlOrganismos.DataBind();

        }

        
        

        
        private void MostrarOcultarModalCambiarOrgUsuario(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalCambiarOrgUsuario.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalCambiarOrgUsuario.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalCambiarOrgUsuario.Attributes.Add("class", String.Join(" ", modalCambiarOrgUsuario
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultarmodalEliminarOrgUsuario(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalEliminarOrgUsuario.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalEliminarOrgUsuario.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalEliminarOrgUsuario.Attributes.Add("class", String.Join(" ", modalEliminarOrgUsuario
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultarModalAgregarNuevaRelacion(bool mostrar)
        {
            divMensajeError.Visible = false;
            limpiarModal();
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalAgregarNuevaRelacion.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalAgregarNuevaRelacion.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalAgregarNuevaRelacion.Attributes.Add("class", String.Join(" ", modalAgregarNuevaRelacion
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        private void RefrescarGrilla()
        {
            var orden_consulta = ddlOrdenConsulta.SelectedValue != "0" ? ddlOrdenConsulta.SelectedValue : "1";
            List<Organismos> ListaOrg = Bl.BlGetListaOrganismosUsuarios(txtBuscarOrganismo.Text, txtCuil.Text, orden_consulta);
            Session["ListaOrg"] = ListaOrg;

            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

            if (ListaOrg.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double)(ListaOrg.Count / (double)gvResultado.PageSize)).ToString());
                gvResultado.PagerSettings.Visible = false;
                gvResultado.DataSource = ListaOrg;
                gvResultado.DataBind();
                lblTitulocantRegistros.Visible = true;
                lblTotalRegistrosGrilla.Text = ListaOrg.Count.ToString();
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

        protected void btnAgregarNuevaRelacion_OnClick(object sender, EventArgs e)
        {
           //confirmar la nueva relacion insertar
            String resultado = Bl.BLAgregarUsuarioOrganismo(txtCuilBuscar.Text, Int64.Parse(ddlOrganismosInsertar.SelectedValue));
            MostrarOcultarModalAgregarNuevaRelacion(false);
            switch (resultado)
            {
                case "ORGANISMO ASIGNADO":
                                          lblMensajeExito.Text = "Se realizó la asignación con éxito.";
                                          divMensajeExito.Visible = true;
                                          ddlOrganismosInsertar.Items.Clear();
                break;
                case "RELACION ACTUALIZADA":
                                          lblMensajeExito.Text = "Se actualizó la relación del usuario existente con éxito.";
                                          divMensajeExito.Visible = true;
                                          ddlOrganismosInsertar.Items.Clear();
                break;
                case "USUARIO YA EXISTE":
                                          lblMensajeExito.Text = "el usuario ya tiene un organismo asignado.";
                                          divMensajeExito.Visible = true;
                                          ddlOrganismosInsertar.Items.Clear();
                break;
            }
            
        }

        protected void btnBuscarPersona_OnClick(object sender, EventArgs e)
        {
            ObtenerPersonaCIDI(txtCuilBuscar.Text);
            if (PersonaCIDI.Respuesta.Resultado == Config.CiDi_OK)
            {
                List<Persona> Persona = Bl.BlGetPersonasRcivil_CUIL2(txtCuilBuscar.Text).ToList();
                if (Persona.Count == 0)
                    return;
                Persona Per = Persona[0];
                txtNombrePersona.Text = Per.Nombre;
                txtApellidoPesona.Text = Per.Apellido;
                txtSexoPersona.Text = Per.Sexo;  
            }
            else
            {
                limpiarModal();
                MostrarOcultarModalAgregarNuevaRelacion(false);
                divMensajeError.Visible = true;
                lblMensajeError.Text = "El usuario no existe en CIDI";

            }
            

        }

        private void limpiarModal()
        {
            txtNombrePersona.Text = "";
            txtApellidoPesona.Text = "";
            txtSexoPersona.Text = "";
            txtCuilBuscar.Text = "";
            ddlOrganismosInsertar.Items.Clear();
            ddlOrganismos.Items.Clear();
        }

       
    }
}