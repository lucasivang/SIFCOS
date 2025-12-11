using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using BL_SIFCOS;
using DA_SIFCOS.Entidades;

namespace SIFCOS
{
    public partial class ProductosYActividades : System.Web.UI.Page
    {
        protected static ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                modalExito.Visible = false;
                modalError.Visible = false;
                divAct.Visible = false;
                divProd.Visible = false;
                cargarTipoConsulta();
                DtActividades = (DataTable)GVActividades.DataSource;
                Consulta = new DataTable();
            }

        }

        private void cargarTipoConsulta()
        {
            ddlTipoConsulta.Items.Clear();
            ddlTipoConsulta.Items.Insert(0, "SELECCIONE TIPO DE CONSULTA");
            ddlTipoConsulta.Items.Insert(1, "PRODUCTO");
            ddlTipoConsulta.Items.Insert(2, "ACTIVIDAD");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]

        public static string[] BuscarProducto(string prefixText, int count)
        {
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

        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;

            RefrescarGrilla();
        }

        protected DataTable Consulta
        {
            get { return Session["Consulta"] == null ? null : (DataTable)Session["Consulta"]; }
            set { Session["Consulta"] = value; }
        }


        protected void RefrescarGrilla()
        {
            if (Consulta.Rows.Count == 0)
            {
                if (txtNombreProducto.Text != "")
                {
                    Consulta = Bl.BlGetProductosPorAct(txtNombreProducto.Text);
                }
                else
                {
                    Consulta = Bl.BlGetProductosPorAct2(txtNombreActividad.Text);
                }
            }


            if (Consulta.Rows.Count > 0)
            {
                gvResultado.DataSource = Consulta;
                gvResultado.DataBind();
                lblTotalRegistrosGrilla.Text = Consulta.Rows.Count.ToString();
                lblTitulocantRegistros.Visible = true;
                titPaginas.Visible = true;

                var cantBotones =
                    int.Parse(Math.Ceiling((double)(Consulta.Rows.Count / (double)gvResultado.PageSize)).ToString());
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

        }

        protected void btnConsultarProducto_OnClick(object sender, EventArgs e)
        {
            RefrescarGrilla();
            txtNombreProducto.Text = "";
            txtNombreActividad.Text = "";
            divAct.Visible = false;
            divProd.Visible = false;
            ddlTipoConsulta.SelectedIndex = 0;

        }

        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {

            // gvResultado.DataSource = null;
            // gvResultado.DataBind();
            // lblTotalRegistrosGrilla.Text = "";

            //titPaginas.Visible = false;


            divAct.Visible = false;
            divProd.Visible = false;
            modalExito.Visible = false;
            modalError.Visible = false;
        }

        protected void btnAsociarProd_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalAgregarProdAct(true);
            modalExito.Visible = false;
            modalError.Visible = false;
            cargarProductos();
            cargarActividades();

        }

        protected void cargarActividades()
        {
            var Actividades = Bl.BlGetActividades();
            ddlActividades.DataSource = Actividades;
            ddlActividades.DataTextField = "n_actividad";
            ddlActividades.DataValueField = "id_actividad";
            ddlActividades.DataBind();
        }

        protected void cargarAct()
        {
            var Actividades = Bl.BlGetActividades();
            ddlAct.DataSource = Actividades;
            ddlAct.DataTextField = "n_actividad";
            ddlAct.DataValueField = "id_actividad";
            ddlAct.DataBind();
        }

        protected void cargarProductos()
        {
            var productos = Bl.BlGetProductosSinAsignar();
            ddlProductos.DataSource = productos;
            ddlProductos.DataTextField = "n_producto";
            ddlProductos.DataValueField = "id_producto";
            ddlProductos.DataBind();
        }

        protected void btnGuardarProdAct_OnClick(object sender, EventArgs e)
        {
            var listAct = new List<Actividad>();
            listAct.Add(new Actividad { Id_Actividad = ddlActividades.SelectedValue, N_Actividad = ddlActividades.SelectedItem.Text });

            //foreach (DataRow row in DtActividades.Rows)
            //{
            //    listAct.Add(new Actividad { Id_Actividad = row["id_actividad"].ToString(), N_Actividad = row["n_actividad"].ToString() });
            //}

            var guardarRelacion = Bl.BlModificarRelacionProdAct(ddlProductos.SelectedValue, listAct);
            if (guardarRelacion == "OK")
            {
                modalExito.Visible = true;
                lblExitoProdAct.Text = "Se guardo con éxito la relacion Producto / Actividad.";
                modalError.Visible = false;

            }
            else
            {
                modalExito.Visible = false;
                lblErrorProdAct.Text = "Hubo un error en el guardado, intente nuevamente....";
                modalError.Visible = true;
            }
            MostrarOcultarModalAgregarProdAct(false);
        }

        protected void btnGuardarRelProdAct_OnClick(object sender, EventArgs e)
        {
            var listAct = new List<Actividad>();
            foreach (DataRow row in DtActividades.Rows)
            {
                listAct.Add(new Actividad { Id_Actividad = row["id_actividad"].ToString(), N_Actividad = row["n_actividad"].ToString() });
            }
            if (GVActividades.Rows.Count > 0)
            {
                var vaciarRelaciones = Bl.BlEliminarRelProd(txtCodigoProd.Text);
                if (vaciarRelaciones == "OK")
                {
                    var guardarRelacion = Bl.BlModificarRelacionProdAct(txtCodigoProd.Text, listAct);
                    if (guardarRelacion == "OK")
                    {
                        modalExito.Visible = true;
                        lblExitoProdAct.Text = "Se guardo con éxito la relacion Producto / Actividad.";
                        modalError.Visible = false;

                    }
                    else
                    {
                        modalExito.Visible = false;
                        lblErrorProdAct.Text = "Hubo un error en el guardado, intente nuevamente....";
                        modalError.Visible = true;
                    }
                }

            }

            MostrarOcultarModalModificarRelacion(false);
        }

        protected void btnCancelarProdAct_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalModificarRelacion(false);
            MostrarOcultarModalAgregarProdAct(false);
            lblTitulocantRegistros.Visible = false;
            GVActividades.DataSource = null;
            GVActividades.DataBind();
            gvResultado.DataSource = null;
            gvResultado.DataBind();
            lblTotalRegistrosGrilla.Text = "";
            txtNombreProducto.Text = "";
            txtNombreProducto.Focus();
            titPaginas.Visible = false;
            btnConsultarProducto.Enabled = true;

        }

        protected void btnCancelarRelProdAct_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalModificarRelacion(false);
            GVActividades.DataSource = null;
            GVActividades.DataBind();

        }

        protected void MostrarOcultarModalAgregarProdAct(bool mostrar)
        {
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalAgregarProductosActividad.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalAgregarProductosActividad.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalAgregarProductosActividad.Attributes.Add("class", String.Join(" ", modalAgregarProductosActividad
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTipoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoConsulta.SelectedValue == "PRODUCTO")
            {
                divAct.Visible = false;
                divProd.Visible = true;
            }
            else
            {
                divProd.Visible = false;
                divAct.Visible = true;
            }
        }

        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "ModificarRelacion" };
            if (!acciones.Contains(e.CommandName))
                return;

            // calculo el indice que corresponde según la paginación seleccionada de la grilla en la que estemos.
            gvResultado.SelectedIndex = calcularIndexPagina(Convert.ToInt32(e.CommandArgument));

            txtCodigoProd.Text = gvResultado.SelectedValue.ToString();
            txtNombreProd.Text = gvResultado.Rows[gvResultado.SelectedIndex].Cells[1].Text;



            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "ModificarRelacion":
                    MostrarOcultarModalModificarRelacion(true);
                    cargarActividadesRelacionadas();
                    break;

            }
        }

        protected void gvActividades_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "QuitarRelacion" };
            if (!acciones.Contains(e.CommandName))
                return;

            // calculo el indice que corresponde según la paginación seleccionada de la grilla en la que estemos.
            GVActividades.SelectedIndex = calcularIndexPagina(Convert.ToInt32(e.CommandArgument));

            txtCodigoProd.Text = gvResultado.Rows[gvResultado.SelectedIndex].Cells[0].Text;

            txtNombreProd.Text = gvResultado.Rows[gvResultado.SelectedIndex].Cells[1].Text;



            if (GVActividades.SelectedValue != null)
                ActividadSeleccionada = GVActividades.SelectedValue.ToString();

            switch (e.CommandName)
            {
                case "QuitarRelacion":
                    QuitarRelacion(ActividadSeleccionada); //EntidadSeleccionada es el ID_ACTIVIDAD.
                    break;

            }
        }
        protected void btnAgregar_OnClick(object sender, EventArgs e)
        {
            if (DtActividades != null)
            {
                for (int i = 0; i < DtActividades.Rows.Count; i++)
                {
                    if (DtActividades.Rows.Count > 0)
                    {
                        if (DtActividades.Rows[i]["id_actividad"].ToString() == ddlAct.SelectedValue)
                        {
                            DtActividades.Rows[i].Delete();
                            break;
                        }
                    }

                }
                DtActividades.Rows.Add(ddlAct.SelectedValue, ddlAct.SelectedItem);
                GVActividades.DataSource = DtActividades;
                GVActividades.DataBind();
            }

            MostrarOcultarModalAgregarActividad(false);

        }

        protected void QuitarRelacion(string idActividad)
        {
            var dtAux = DtActividades.Clone();


            foreach (DataRow row in DtActividades.Rows)
            {

                if (row["id_actividad"].ToString() != idActividad)
                {
                    dtAux.ImportRow(row);
                }
            }

            GVActividades.DataSource = dtAux;
            GVActividades.DataBind();
            DtActividades = dtAux;


            lblTitAct.Text = GVActividades.Rows.Count != 0 ? " Actividades Relacionadas con el producto seleccionado:" : "No Hay actividades relacionadas.";

        }

        protected void cargarActividadesRelacionadas()
        {
            var prod = new List<int>();
            prod.Add(EntidadSeleccionada);
            var relaciones = Bl.BlGetActividadesProducto(prod);
            if (relaciones.Rows.Count > 0)
            {
                GVActividades.DataSource = relaciones;
                GVActividades.DataBind();
                DtActividades = relaciones;
            }
            lblTitAct.Text = GVActividades.Rows.Count != 0 ? " Actividades Relacionadas con el producto seleccionado:" : "No Hay actividades relacionadas.";
        }

        protected int calcularIndexPagina(int indexActual)
        {
            //PORQUE LA PAGINACIÓN ESTÁ SETEADA EN 10 REGISTROS POR PAGINA.
            if (indexActual < gvResultado.PageSize)
                return indexActual;
            var resto = indexActual % gvResultado.PageSize;

            return resto;
            //var paginaActual = (indexActual - resto) / gvResultado.PageSize;
            //return paginaActual;
        }

        protected void MostrarOcultarModalModificarRelacion(bool mostrar)
        {
            modalError.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalModificarRelacionProd.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalModificarRelacionProd.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalModificarRelacionProd.Attributes.Add("class", String.Join(" ", modalModificarRelacionProd
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        protected void MostrarOcultarModalAgregarActividad(bool mostrar)
        {
            modalError.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalAgregarActividad.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalAgregarActividad.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalAgregarActividad.Attributes.Add("class", String.Join(" ", modalAgregarActividad
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        protected int EntidadSeleccionada
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

        protected string ActividadSeleccionada
        {
            get
            {
                return (string)Session["ActividadSeleccionada"];
            }
            set
            {
                Session["ActividadSeleccionada"] = value;
            }
        }
        protected DataTable DtActividades
        {
            get { return Session["DtActividades"] == null ? null : (DataTable)Session["DtActividades"]; }
            set { Session["DtActividades"] = value; }
        }

        protected void btnAgregarActividad_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalAgregarActividad(true);
            cargarAct();
            txtCodProdSel.Text = txtCodigoProd.Text;
            txtDesProdSel.Text = txtNombreProd.Text;
        }



        protected void btnNoAgregar_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalAgregarActividad(false);
        }
    }
}