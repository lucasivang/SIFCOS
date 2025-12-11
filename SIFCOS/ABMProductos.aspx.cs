using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using BL_SIFCOS;


namespace SIFCOS
{
    public partial class ABMProductos : System.Web.UI.Page
    {
        protected static ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divExitoProd.Visible = false;
                divErrorProd.Visible = false;
                lblTitulocantRegistros.Visible = false;
                lblTotalRegistrosGrilla.Visible = false;
                gvResultado.PagerSettings.Visible = false;
                rptBotonesPaginacion.DataSource = null;
                rptBotonesPaginacion.DataBind();
            }

        }
        
        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod]
        //public static string[] BuscarProducto(string prefixText, int count)
        //{
        //    List<Producto> _productos = Bl_static.BlGetProductosbeta(prefixText.ToUpper()).ToList();

        //    string[] lista = new string[_productos.Count];
        //    var contador = 0;
        //    foreach (var row_producto in _productos)
        //    {
        //        lista[contador] = AutoCompleteExtender.CreateAutoCompleteItem(row_producto.NProducto, row_producto.IdProducto);
        //        contador++;
        //    }

        //    return lista;
        //}
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

        protected void RefrescarGrilla()
        {
            var Consulta = Bl.BlGetProductos(txtNombreProducto.Text);
            if (Consulta.Count > 0)
            {
                gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double)(Consulta.Count / (double)gvResultado.PageSize)).ToString());
                gvResultado.DataSource = Consulta;
                gvResultado.DataBind();
                gvResultado.PagerSettings.Visible = false;
                lblTotalRegistrosGrilla.Text = Consulta.Count.ToString();
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
        }


        protected void btnConsultarProducto_OnClick(object sender, EventArgs e)
        {
                RefrescarGrilla();
                lblTitulocantRegistros.Visible = false;
                txtNombreProducto.Text = "";
                txtNombreProducto.Focus();
                
        }

        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "Modificar", "Eliminar" };
            if (!acciones.Contains(e.CommandName))
                return;

            // calculo el indice que corresponde según la paginación seleccionada de la grilla en la que estemos.
            gvResultado.SelectedIndex = calcularIndexPagina(Convert.ToInt32(e.CommandArgument));

            var indexPaginado = gvResultado.SelectedIndex;

            txtCodigoProd.Text = gvResultado.Rows[indexPaginado].Cells[0].Text;
            txtCodigoProd2.Text = gvResultado.Rows[indexPaginado].Cells[0].Text;
            txtNombreProd.Text = gvResultado.Rows[indexPaginado].Cells[1].Text;
            txtNombreProd2.Text = gvResultado.Rows[indexPaginado].Cells[1].Text;
            
            

            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            switch (e.CommandName)
            {
                case "Modificar":
                    MostrarOcultarModalModificarProducto(true);
                    
                    break;
                case "Eliminar":
                    MostrarOcultarModalEliminarProducto(true);
                    
                    break;

            }
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

        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
            //lblTitulocantRegistros.Visible = false;
            //gvResultado.DataSource = null;
            //gvResultado.DataBind();
            //lblTotalRegistrosGrilla.Text = "";
            //txtNombreProducto.Text = "";
            //txtNombreProducto.Focus();
            //titPaginas.Visible = false;
            //btnConsultarProducto.Enabled = true;
        }

        protected void btnAgregarProd_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalAgregarProd(true);
            divExitoProd.Visible = false;
            divErrorProd.Visible = false;
            btnAgregarProd.Visible = true;
            btnCancelarProd.Visible = true;
            btnCancelarProd.Text = "Cancelar";

        }
        

        protected void btnGuardarProd_OnClick(object sender, EventArgs e)
        {
            var guardarProd = Bl.BlRegistrarProducto(txtProducto.Text.ToUpper());
            if (guardarProd == "OK")
            {
                divExitoProd.Visible = true;
                divErrorProd.Visible = false;
                lblExitoProd.Text = "Se guardo con éxito el producto";
            }
            else
            {
                divExitoProd.Visible = false;
                divErrorProd.Visible = true;
                lblErrorProd.Text = "El Producto ya existe.";
            }
            
            MostrarOcultarModalAgregarProd(false);
            

        }

        protected void btnCancelarProd_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalAgregarProd(false);
            lblTitulocantRegistros.Visible = false;
            gvResultado.DataSource = null;
            gvResultado.DataBind();
            lblTotalRegistrosGrilla.Text = "";
            txtNombreProducto.Text = "";
            txtNombreProducto.Focus();
            titPaginas.Visible = false;
            btnConsultarProducto.Enabled = true;
            txtProducto.Text = "";
            btnCancelarProd.Text = "Cancelar";
            btnGuardarProd.Visible = true;
            btnCancelarProd.Visible = true;
        }
        private void MostrarOcultarModalAgregarProd(bool mostrar)
        {
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalAgregarProductos.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalAgregarProductos.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalAgregarProductos.Attributes.Add("class", String.Join(" ", modalAgregarProductos
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }
        private void MostrarOcultarModalModificarProducto(bool mostrar)
        {
            divErrorProd.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalModificarProducto.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalModificarProducto.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalModificarProducto.Attributes.Add("class", String.Join(" ", modalModificarProducto
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultarModalEliminarProducto(bool mostrar)
        {
            divErrorProd.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalEliminarProducto.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalEliminarProducto.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalEliminarProducto.Attributes.Add("class", String.Join(" ", modalEliminarProducto
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
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

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            var guardarcambios = Bl.BlModificarProducto(txtCodigoProd.Text, txtNombreProd.Text);
            if (guardarcambios == "OK")
            {
                divExitoProd.Visible = true;
                divErrorProd.Visible = false;
                lblExitoProd.Text = "Se guardo con éxito la modificación del producto";
                
            }
            else
            {
                divExitoProd.Visible = false;
                divErrorProd.Visible = true;
                lblErrorProd.Text = "Hubo un error en la actualizacion del producto. Intente nuevamente....";
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalModificarProducto(false);
        }

        protected void btnCancelarEliminar_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalEliminarProducto(false);
        }

        protected void btnAceptarEliminar_OnClick(object sender, EventArgs e)
        {
            
        }
    }
}