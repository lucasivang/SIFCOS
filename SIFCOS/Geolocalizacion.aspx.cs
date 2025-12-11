using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using BL_SIFCOS;
using Subgurim.Controles;
using Subgurim.Controles.GoogleChartIconMaker;
using Newtonsoft.Json;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;

namespace SIFCOS
{
    public partial class Geolocalizacion : System.Web.UI.Page
    {
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();
        protected DataTable DtDeptos = new DataTable();
        protected DataTable DtLocalidades = new DataTable();
       // public Principal master;

        protected void Page_Load(object sender, EventArgs e)
        {
           // master = (Principal)Page.Master;
            //var lstRolesNoAutorizados = new List<string>();
            //lstRolesNoAutorizados.Add("Gestor");//usuario comun;
            //lstRolesNoAutorizados.Add("Sin Asignar");

            //if (lstRolesNoAutorizados.Contains(master.RolUsuario))
            //{
            //    Response.Redirect("Inscripcion.aspx");
            //}
            if (!Page.IsPostBack)
            {

                divMensajeError.Visible = false;
                divMensajeExito.Visible = false;
                LimpiarSessionesEnMemoria();
                //CargarInfoBoca();
               // divPantallaInfoBoca.Visible = true;
                CargarDeptos();
                CargarLocalidades();
                divResultado.Visible = false;



            }

        }




        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] BuscarProducto(string prefixText, int count)
        {
            //lblProdSel.Text = "Ningún producto seleccionado";
            List<Producto> _productos = Bl_static.BlGetProductosbeta(prefixText.ToUpper()).ToList();

            string[] lista = new string[_productos.Count];
            var contador = 0;
            foreach (var row_producto in _productos)
            {
                lista[contador] = AutoCompleteExtender.CreateAutoCompleteItem(row_producto.NProducto, row_producto.IdProducto);
                contador++;
            }

            return lista;
        }

        /// <summary>
        /// Autor: (IB)
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] BuscarRubro(string prefixText, int count)
        {
            List<Rubro> _Rubros = Bl_static.BlGetRubrosComercio(prefixText.ToUpper()).ToList();

            string[] lista = new string[_Rubros.Count];
            var contador = 0;
            foreach (var row_Rubro in _Rubros)
            {
                lista[contador] = AutoCompleteExtender.CreateAutoCompleteItem(row_Rubro.NRubro, row_Rubro.IdRubro);
                contador++;
            }

            return lista;
        }

        protected void gvProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }





        /// <summary>
        /// Modificado por (IB) 03/2018
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Consultar_OnClick(object sender, EventArgs e)
        {

            divMensajeError.Visible = false;
            //var banderaUtilizaFiltro = false;
            /*FILTROS*/
            //var idProducto = string.IsNullOrEmpty(txtBuscarProducto.Text) ? "" : ace1Value.Value;
            string strIdLocalidad = ddlLocalidad.SelectedValue;
            string strIdDepartamento = ddlDeptos.SelectedValue;
            int idLocalidad = 0;
            int idDepartamento = 0;



            //Parámetro IdOrganismo
            //int idOrganismo = 0;
            //int idOrg;
            //bool idOrgOK;
            //idOrgOK = int.TryParse(IdOrganismoUsuarioLogueado, out idOrg);
            //if (idOrgOK)
            //    idOrganismo = idOrg;
            //else
            //    throw new Exception("Organismo Inválido.");


            //Parámetro IdProducto
            string strIdproducto = ace1Value.Value;
            int? idProducto = null;
            if (!string.IsNullOrEmpty(strIdproducto))
            {
                int IdProd;
                bool IdProdOK = false;
                IdProdOK = int.TryParse(strIdproducto, out IdProd);
                if (IdProdOK)
                    idProducto = IdProd;
                else
                    throw new Exception("Producto Inválido.");
            }

            //Parámetro IdRubro
            int? idRubro = null;


            if (!string.IsNullOrEmpty(ddlDeptos.SelectedValue) && ddlDeptos.SelectedValue != "0")
            {
                //Si selecciona departamento y localidad, consulta solo por localidad
                if (!string.IsNullOrEmpty(ddlLocalidad.SelectedValue) && ddlLocalidad.SelectedValue != "0")
                {
                    int idLoc;
                    bool idLocOK = false;
                    idLocOK = int.TryParse(strIdLocalidad, out idLoc);
                    if (idLocOK)
                        idLocalidad = idLoc;
                    else
                        throw new Exception("Localidad Inválida.");

                }
                else
                {
                    //si solo selecciona departamento, consulta solo por departamento.
                    int idDep;
                    bool idDepOK = false;
                    idDepOK = int.TryParse(strIdDepartamento, out idDep);
                    if (idDepOK)
                        idDepartamento = idDep;
                    else
                        throw new Exception("Departamento Inválido.");
                }
            }

            comercios = Bl.GetComerciosDtos(1, idDepartamento, idLocalidad, idProducto, idRubro);
            lblTotal.Text = "Total registros: " + comercios.Count.ToString();



            foreach (var i in comercios)
            {
                lblIdsGeolocalizacion.Text += i.ID_ENTIDAD + ",";
            }
            RefrescarGrilla();

        }

        protected void ddlDeptoLegal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidadesLegal();
        }
        private void CargarLocalidadesLegal()
        {
            ddlLocalidad.Items.Clear();

            DtLocalidades = Bl.BlGetLocalidades(ddlDeptos.SelectedValue);

            if (DtLocalidades.Rows.Count != 0)
            {
                foreach (DataRow dr in DtLocalidades.Rows)
                {
                    ddlLocalidad.Items.Add(new ListItem(dr["n_localidad"].ToString(), dr["id_localidad"].ToString()));
                }
                Session["LOCALIDAD"] = DtLocalidades;
                if (ddlLocalidad.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
                    ddlLocalidad.SelectedValue = "0";
                ddlLocalidad.Enabled = true;
            }
            else
            {
                ddlLocalidad.Items.Contains(new ListItem("SIN ASIGNAR", "0"));
                ddlLocalidad.Enabled = false;
            }
        }
        private void LimpiarSessionesEnMemoria()
        {
            DtProductos = null;
            SessionDepartamentos = null;
            SessionLocalidades = null;
            comercios = null;


        }
        public void CargarDeptos()
        {
            ddlLocalidad.Items.Clear();

            DtDeptos = Bl.BlGetDeptartamentos("X"); //seteo por defecto la provincia de CÓRDOBA.
            if (DtDeptos.Rows.Count != 0)
            {
                ddlDeptos.Items.Clear();
                foreach (DataRow dr in DtDeptos.Rows)
                {
                    ddlDeptos.Items.Add(new ListItem(dr["n_departamento"].ToString(), dr["id_departamento"].ToString()));
                }
                ddlDeptos.Enabled = true;
                if (ddlDeptos.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
                    ddlDeptos.SelectedValue = "0";
                ddlLocalidad.Focus();



            }
            else
            {
                ddlDeptos.Items.Clear();
                ddlDeptos.Items.Contains(new ListItem("SIN ASIGNAR", "0"));
            }

        }

        public DataTable SessionDepartamentos
        {
            get
            {
                return Session["DEPTO"] == null ? null : (DataTable)Session["DEPTO"];
            }
            set
            {
                Session["DEPTO"] = value;
            }
        }



        public DataTable SessionLocalidades
        {
            get
            {
                return Session["LOCALIDAD"] == null ? null : (DataTable)Session["LOCALIDAD"];
            }
            set
            {
                Session["LOCALIDAD"] = value;
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

        public List<ComercioDto> comercios
        {
            get
            {
                return Session["comercios"] == null ? null : (List<ComercioDto>)Session["comercios"];
            }
            set
            {
                Session["comercios"] = value;
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
        public string IdOrganismoUsuarioLogueado
        {
            get
            {
                return Session["IdOrganismoUsuarioLogueado"] == null ? "" : (string)Session["IdOrganismoUsuarioLogueado"];
            }
            set
            {
                Session["IdOrganismoUsuarioLogueado"] = value;
            }
        }
        public DataTable DtProductos
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

        protected void ddlDeptos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidades();
            txtBuscarProducto.Text = "";
            ace1Value.Value = "";
            lblProdSel.Text = "";
        }

        private void CargarLocalidades()
        {
            ddlLocalidad.Items.Clear();

            var idDepartamento = ddlDeptos.SelectedValue;
            DtLocalidades = Bl.BlGetLocalidades(idDepartamento);

            if (DtLocalidades.Rows.Count != 0)
            {
                foreach (DataRow dr in DtLocalidades.Rows)
                {
                    ddlLocalidad.Items.Add(new ListItem(dr["n_localidad"].ToString(), dr["id_localidad"].ToString()));
                }
                SessionLocalidades = DtLocalidades;
                if (ddlLocalidad.Items.Contains(new ListItem("SIN ASIGNAR", "0")))
                    ddlLocalidad.SelectedValue = "0";
                ddlLocalidad.Enabled = true;
            }
            else
            {
                ddlLocalidad.Items.Contains(new ListItem("SIN ASIGNAR", "0"));
                ddlLocalidad.Enabled = false;
            }
        }

        //private void CargarInfoBoca()
        //{
        //    DataTable dtInfoBoca = Bl.BlGetInfoBoca(UsuarioCidiLogueado.CUIL);
        //    if (dtInfoBoca.Rows.Count == 0)
        //    {
        //        IdOrganismoUsuarioLogueado = null;
        //        txtBocaRecepcion.Text = " - ";
        //        txtLocalidadBoca.Text = " - ";
        //        txtDependencia.Text = " - ";
        //        return;
        //    }
        //    IdOrganismoUsuarioLogueado = dtInfoBoca.Rows[0]["id_organismo"].ToString();
        //    txtBocaRecepcion.Text = dtInfoBoca.Rows[0]["boca_recepcion"].ToString();
        //    txtLocalidadBoca.Text = dtInfoBoca.Rows[0]["localidad"].ToString();
        //    txtDependencia.Text = dtInfoBoca.Rows[0]["dependencia"].ToString();
        //}







        private void AgregarCssClass(string nombreClass, WebControl control)
        {
            List<string> classes;
            if (!string.IsNullOrWhiteSpace(control.CssClass))
            {
                classes = control.CssClass.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (!classes.Contains(nombreClass))
                    classes.Add(nombreClass);
            }
            else
            {
                classes = new List<string> { nombreClass };
            }
            control.CssClass = string.Join(" ", classes.ToArray());
        }



        private void QuitarCssClass(string nombreClass, WebControl control)
        {
            List<string> classes = new List<string>();
            if (!string.IsNullOrWhiteSpace(control.CssClass))
            {
                classes = control.CssClass.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            classes.Remove(nombreClass);
            control.CssClass = string.Join(" ", classes.ToArray());
        }



        protected void Limpiar_Busqueda_OnClick(object sender, EventArgs e)
        {

        }

        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            divResultado.Visible = true;
            var pCuit = "";

           
            completarLabelConIdsGeolocalizacion();
           
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

            if (comercios.Count > 0 )
            {
                botonGEO.Visible = true;
                var cantBntPage = int.Parse(Math.Ceiling((double)(comercios.Count / (double)gvResultado.PageSize)).ToString());
                if (cantBntPage > 20)
                    cantBntPage = 20;

                gvResultado.PagerSettings.PageButtonCount = cantBntPage;
                gvResultado.PagerSettings.Visible = false;

                gvResultado.DataSource = comercios;
                gvResultado.DataBind();

                lblTituloTotal.Visible = true;
                lblTotal.Text = comercios.Count.ToString();
                var cantBotones = gvResultado.PagerSettings.PageButtonCount;
                var listaNumeros = new ArrayList();

                var nroPagSelec = 1;
                if (!string.IsNullOrEmpty(commandoBotonPaginaSeleccionado))
                    nroPagSelec = int.Parse(commandoBotonPaginaSeleccionado);


                for (int i = 0; i < cantBotones; i++)
                {

                    if (nroPagSelec <= 10)
                    {
                        var datos = new
                        {
                            nroPagina = 1 + i
                        };
                        listaNumeros.Add(datos);
                    }
                    else
                    {
                        var datos = new
                        {
                            nroPagina = (nroPagSelec - 10) + i
                        };
                        listaNumeros.Add(datos);
                    }

                }
                rptBotonesPaginacion.DataSource = listaNumeros;
                rptBotonesPaginacion.DataBind();

            }

            else
            {
                lblTituloTotal.Visible = false;
                botonGEO.Visible = false;
                lblTotal.Text = "No se encontraron registros que coincidan con el filtro de búsqueda";


            }

        }

        private void completarLabelConIdsGeolocalizacion()
        {
            List<string> listAux = new List<string>();

            foreach (ComercioDto Comercio in comercios)
            {
                listAux.Add(Comercio.ID_ENTIDAD.ToString());
            }

            lblIdsGeolocalizacion.Text = string.Join(",", listAux.ToArray());

        }

        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "Actividad", "PromIndustrial" };
            if (!acciones.Contains(e.CommandName))
                return;

            gvResultado.SelectedIndex = calcularIndexPagina(Convert.ToInt32(e.CommandArgument));

            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            //switch (e.CommandName)
            //{
                //case "Actividad":
                   
                //    divMensajeError.Visible = false;
                //    break;
                //case "PromIndustrial":
                    
                //    break;

            //}
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

        protected void rptBotonesPaginacion_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
            gvResultado.PageIndex = nroPagina - 1;
            RefrescarGrilla();
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
           Response.Redirect("Geolocalizacion.aspx");
        }
    }
}