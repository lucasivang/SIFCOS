using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using BL_SIFCOS;
using DA_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using ListItem = System.Web.UI.WebControls.ListItem;


namespace SIFCOS
{
   public partial class ReporteSIFCoS : System.Web.UI.Page
    {
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        protected DataTable DtConsulta = new DataTable();

        protected DataTable DtDeptos = new DataTable();
        protected DataTable DtLocalidades = new DataTable();

        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public Principal master;
        protected void Page_Load(object sender, EventArgs e)
        {
            master = (Principal)Page.Master;
            var lstRolesNoAutorizados = new List<string>();
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
             
                cargarComboBocaRecepcionPadre();
                cargarComboTipoTramite();
                
                CargarInfoBoca();
                CargarCombosBoca();
                CargarDeptos();
                cargarComboEstadosTramite();
                divPantallaInfoBoca.Visible = true;
                divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;
                btnSalirDetalleSeleccionado.Visible = false;
               
            }

        }

       private void CargarCombosBoca()
       {
           if (master.RolUsuario == "Boca de Recepcion") // boca de recepcion
           {
               if (IdOrganismoUsuarioLogueado == "1" || IdOrganismoUsuarioLogueado == "2" ||
                   IdOrganismoUsuarioLogueado == "3")
               {
                   cargarComboBocaRecepcionPadre();
                   ddlBocaRecepcionPadre.SelectedValue = IdOrganismoUsuarioLogueado;
                   cargarComboBocaRecepcion(IdOrganismoUsuarioLogueado);
                   divOrganismoPadre.Visible = false;
                   divOrganismo.Visible = true;

               }
               else
               {
                   cargarComboBocaRecepcionOne(IdOrganismoUsuarioLogueado);
                   ddlBocaRecepcion.SelectedValue = IdOrganismoUsuarioLogueado;
                   divOrganismoPadre.Visible = false;
                   divOrganismo.Visible = false;
               }
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
            IdOrganismoUsuarioLogueado = dtInfoBoca.Rows[0]["id_organismo"].ToString();
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
            var IdBocaRecepcion="";
            if (master.RolUsuario == "Boca de Recepcion" && IdOrganismoUsuarioLogueado != "3" && IdOrganismoUsuarioLogueado != "2" && IdOrganismoUsuarioLogueado != "1") // boca de recepcion
            {
                IdBocaRecepcion = IdOrganismoUsuarioLogueado;
            }
            else
            {
                IdBocaRecepcion = ddlBocaRecepcion.SelectedValue;
            }

            ResultadoConsulta = Bl.BlReporteGerencial(ddlTipoTramite.SelectedValue,
                ddlEstadoTramite.SelectedValue,txtFechaDesde.Text,txtFechaHasta.Text,
                txtCuit.Text.Trim(),txtRazonSocial.Text,txtNroTramiteDesde.Text.Trim(),txtNroTramiteHasta.Text.Trim(),
                txtNroSifcosDesde.Text.Trim(),txtNroSifcosHasta.Text.Trim(),IdBocaRecepcion,
                ddlBocaRecepcionPadre.SelectedValue,ddlDeptos.SelectedValue,ddlLocalidad.SelectedValue);
            
            RefrescarGrilla();
             divPantallaResultado.Visible = true;
            
        }

       public DataTable ResultadoConsulta
       {
           get
           {
               return (DataTable) Session["ResultadoConsulta"];
           }
           set
           {
               Session["ResultadoConsulta"] = value;
           }
       }

       public DataTable ResultadoConsultaDetalle
       {
           get
           {
               return (DataTable) Session["ResultadoConsultaDetalle"];
           }
           set
           {
               Session["ResultadoConsultaDetalle"] = value;
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

       protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
           DtConsulta = new DataTable();
           GVResultadoReporte.DataSource = null;
           GVResultadoReporte.DataBind();
           rptBotonesPaginacion.DataSource = null;
           rptBotonesPaginacion.DataBind();
           txtFechaDesde.Text = "";
           txtFechaHasta.Text = "";
           txtNroTramiteDesde.Text = "";
           txtNroTramiteHasta.Text = "";
           txtNroSifcosDesde.Text = "";
           txtNroSifcosHasta.Text = "";
           txtRazonSocial.Text = "";
           txtCuit.Text = "";
           CargarCombosBoca();
           divPantallaResultado.Visible = false;
           btnConsultar.Enabled = true;

        }
        
        public string Seleccion
        {
            get
            {
                return Session["Seleccion"] == null ? "" : (string)Session["Seleccion"];

            }
            set
            {
                Session["Seleccion"] = value;
            }
        }
 

        protected void btnPDF_OnClick(object sender, EventArgs e)
        {
            if (ResultadoConsulta == null)
                return;
            List<ReporteDto> lis = Bl.ConvertirAReporteDto(ResultadoConsulta);

            var reporte = new ReporteGeneral("SIFCOS.rptReporte.rdlc", lis, TipoArchivoEnum.Pdf);
            reporte.AddParameter("Parametro_Tipo_Tramite", !string.IsNullOrEmpty(ddlTipoTramite.SelectedItem.Text) ? ddlTipoTramite.SelectedItem.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Fecha_Desde", !string.IsNullOrEmpty(txtFechaDesde.Text) ? txtFechaDesde.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Fecha_Hasta", !string.IsNullOrEmpty(txtFechaHasta.Text) ? txtFechaHasta.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Nro_Tramite_Desde", !string.IsNullOrEmpty(txtNroTramiteDesde.Text) ? txtNroTramiteDesde.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Nro_Tramite_Hasta", !string.IsNullOrEmpty(txtNroTramiteHasta.Text) ? txtNroTramiteHasta.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Nro_Sifcos_Desde", !string.IsNullOrEmpty(txtNroSifcosDesde.Text) ? txtNroSifcosDesde.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Nro_Sifcos_Hasta", !string.IsNullOrEmpty(txtNroSifcosHasta.Text) ? txtNroSifcosHasta.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Boca_Recepcion", !string.IsNullOrEmpty(txtBocaRecepcion.Text) ? txtBocaRecepcion.Text : " NO APLICA ");


            Session["ReporteGeneral"] = reporte;
            Response.Redirect("ReporteGeneral.aspx");

        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            if(ResultadoConsulta == null)
                return;
            List<ReporteDto> lis = Bl.ConvertirAReporteDto(ResultadoConsulta);

            var reporte = new ReporteGeneral("SIFCOS.rptReporte.rdlc", lis, TipoArchivoEnum.Excel);
            reporte.AddParameter("Parametro_Tipo_Tramite", ddlTipoTramite.SelectedValue != "0" && !string.IsNullOrEmpty(ddlTipoTramite.SelectedValue) ? ddlTipoTramite.SelectedItem.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Fecha_Desde", !string.IsNullOrEmpty(txtFechaDesde.Text) ? txtFechaDesde.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Fecha_Hasta", !string.IsNullOrEmpty(txtFechaHasta.Text) ? txtFechaHasta.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Nro_Tramite_Desde", !string.IsNullOrEmpty(txtNroTramiteDesde.Text) ? txtNroTramiteDesde.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Nro_Tramite_Hasta", !string.IsNullOrEmpty(txtNroTramiteHasta.Text) ? txtNroTramiteHasta.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Nro_Sifcos_Desde", !string.IsNullOrEmpty(txtNroSifcosDesde.Text) ? txtNroSifcosDesde.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Nro_Sifcos_Hasta", !string.IsNullOrEmpty(txtNroSifcosHasta.Text) ? txtNroSifcosHasta.Text : " NO APLICA ");
            var bocaRecepcion = "";
            if (ddlBocaRecepcion.SelectedValue != "")
                bocaRecepcion = ddlBocaRecepcion.SelectedItem.Text;   //muestro la Boca hija.
            else
            {
                if (ddlBocaRecepcionPadre.SelectedValue != "")
                {
                    bocaRecepcion = ddlBocaRecepcionPadre.SelectedItem.Text;
                }
            }
            reporte.AddParameter("Parametro_Boca_Recepcion", !string.IsNullOrEmpty(bocaRecepcion) ? bocaRecepcion : " NO APLICA ");

            reporte.AddParameter("Parametro_Estado", ddlEstadoTramite.SelectedValue != "0" && !string.IsNullOrEmpty(ddlEstadoTramite.SelectedValue) ? ddlEstadoTramite.SelectedItem.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Localidad", ddlLocalidad.SelectedValue != "0" && !string.IsNullOrEmpty(ddlLocalidad.SelectedValue) ? ddlLocalidad.SelectedItem.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Departamento", ddlDeptos.SelectedValue != "0" && !string.IsNullOrEmpty(ddlDeptos.SelectedValue) ? ddlDeptos.SelectedItem.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Cuit", !string.IsNullOrEmpty(txtCuit.Text) ? txtCuit.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_RazonSocial", !string.IsNullOrEmpty(txtRazonSocial.Text) ? txtRazonSocial.Text : " NO APLICA ");

            Session["ReporteGeneral"] = reporte;
            Response.Redirect("ReporteGeneral.aspx");


        }

       protected void DetalleTramite()
       {

           ResultadoConsultaDetalle = Bl.BlReporteGerencial(null, null, null, null,null, null, EntidadSeleccionada.ToString(), EntidadSeleccionada.ToString(), null, null, null, null,null, null);
           GVDetalleTramite.DataSource = ResultadoConsultaDetalle;
           GVDetalleTramite.DataBind();
           GVDetalleTramite.Visible = true;
           divGVResultado.Visible = false;
           divGVdetalle.Visible = true;
           lblTitulocantRegistros.Visible = false;
           lblTotalRegistrosGrilla.Visible = false;
           rptBotonesPaginacion.Visible = false;

       }







       protected void cargarComboTipoTramite()
       {
           ddlTipoTramite.Items.Clear();
           ddlTipoTramite.Items.Insert(0, new ListItem("SELECCIONE TIPO DE TRAMITE","0"));
           ddlTipoTramite.Items.Insert(1, new ListItem("ALTA","1"));
           ddlTipoTramite.Items.Insert(2, new ListItem("BAJA","2"));
           ddlTipoTramite.Items.Insert(3, new ListItem("MODIFICACION","3"));
           ddlTipoTramite.Items.Insert(4, new ListItem("REEMPADRONAMIENTO","4"));
           ddlTipoTramite.Items.Insert(5, new ListItem("REIMPRESION DE OBLEA CON TRS PAGA","5"));
           ddlTipoTramite.Items.Insert(6,new ListItem("REIMPRESION DE OBLEA SIN TRS PAGA","6"));
           ddlTipoTramite.Items.Insert(7, new ListItem("REIMPRESION DE CERTIFICADO CON TRS PAGA","7"));
           ddlTipoTramite.Items.Insert(8, new ListItem("REIMPRESION DE CERTIFICADO SIN TRS PAGA","8"));
           ddlTipoTramite.Items.Insert(9, new ListItem("REIMPRESION DE OBLEA Y CERT CON TRS PAGA","9"));
           ddlTipoTramite.Items.Insert(10, new ListItem("REIMPRESION DE OBLEA Y CERT SIN TRS PAGA", "10"));
       }

       protected void cargarComboBocaRecepcionPadre()
       {
           ddlBocaRecepcionPadre.Items.Clear();
           ddlBocaRecepcionPadre.Items.Insert(0,new ListItem("SELECCIONE ORGANISMO","0")); 
           ddlBocaRecepcionPadre.Items.Insert(1, new ListItem("MINISTERIO DE INDUSTRIA, COMERCIO Y MINERIA","1"));
           ddlBocaRecepcionPadre.Items.Insert(2, new ListItem("FEDERACION COMERCIAL DE LA PROVINCIA DE CORDOBA","2"));
           ddlBocaRecepcionPadre.Items.Insert(3, new ListItem("CAMARA DE COMERCIO DE CORDOBA", "3"));
       }

       protected void cargarComboEstadosTramite()
       {
           ddlEstadoTramite.Items.Clear();
           ddlEstadoTramite.Items.Insert(0, new ListItem("SELECCIONE UNA OPCION","0"));
           ddlEstadoTramite.Items.Insert(1, new ListItem("CARGADO","1"));
           ddlEstadoTramite.Items.Insert(2, new ListItem("VERIFICADO BOCA","2"));
           ddlEstadoTramite.Items.Insert(3, new ListItem("RECHAZADO BOCA","3"));
           ddlEstadoTramite.Items.Insert(4, new ListItem("VERIFICADO MINISTERIO","4"));
           ddlEstadoTramite.Items.Insert(5, new ListItem("RECHAZADO MINISTERIO","5"));
           ddlEstadoTramite.Items.Insert(6, new ListItem("AUTORIZADO MINISTERIO","6"));
           ddlEstadoTramite.Items.Insert(7, new ListItem("RECHAZADO SIN TASA PAGA","7"));
           ddlEstadoTramite.Items.Insert(8, new ListItem("ADEUDA","8"));
           ddlEstadoTramite.Items.Insert(9, new ListItem("MODIFICADO","9"));
           ddlEstadoTramite.Items.Insert(10, new ListItem("DADO DE BAJA","10"));
           ddlEstadoTramite.Items.Insert(11, new ListItem("RECHAZADO CON DEV DE TASA","11"));
           ddlEstadoTramite.Items.Insert(12, new ListItem("REIMPRESION VERIFICADA","12"));
           ddlEstadoTramite.Items.Insert(13, new ListItem("REIMPRESION AUTORIZADA","13"));
       }

       protected void ddlBocaRecepcionPadre_OnSelectedIndexChanged(object sender, EventArgs e)
       {
           cargarComboBocaRecepcion(ddlBocaRecepcionPadre.SelectedValue);

       }
       protected void cargarComboBocaRecepcion(String idOrganismo)
       {
           DataTable dt = new DataTable();
           dt = Bl.BlGetOrganismosByIdOrganismos(idOrganismo);
           ddlBocaRecepcion.DataSource = dt;
           ddlBocaRecepcion.DataTextField = "n_organismo";
           ddlBocaRecepcion.DataValueField = "id_organismo";
           ddlBocaRecepcion.DataBind();
           ddlBocaRecepcion.Items.Insert(0, new ListItem("SELECCIONE BOCA DE RECEPCION","0")); 
           
       }

       protected void cargarComboBocaRecepcionOne(String idOrganismo)
       {
           DataTable dt = new DataTable();
           dt = Bl.BlGetOrganismosOne(idOrganismo);
           ddlBocaRecepcion.DataSource = dt;
           ddlBocaRecepcion.DataTextField = "n_organismo";
           ddlBocaRecepcion.DataValueField = "id_organismo";
           ddlBocaRecepcion.DataBind();
           ddlBocaRecepcion.Items.Insert(0, new ListItem("SELECCIONE BOCA DE RECEPCION", "0"));

       }



       protected void gvResultadoReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
       {
           GVResultadoReporte.PageIndex = e.NewPageIndex;
           RefrescarGrilla();
       }

       protected void gvResultadoReporte_RowDataBound(object sender, GridViewRowEventArgs e)
       {
          
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
       private int calcularIndexPagina(int indexActual)
       {
           //PORQUE LA PAGINACIÓN ESTÁ SETEADA EN 10 REGISTROS POR PAGINA.
           if (indexActual < GVResultadoReporte.PageSize)
               return indexActual;
           var resto = indexActual % GVResultadoReporte.PageSize;

           return resto;
           //var paginaActual = (indexActual - resto) / gvResultado.PageSize;
           //return paginaActual;
       }
       private void RefrescarGrilla()
       { 
           GVResultadoReporte.PagerSettings.Mode = PagerButtons.Numeric;
           if (ResultadoConsulta.Rows.Count > 0)
           {
               divGVdetalle.Visible = false;
               divGVResultado.Visible = true;
               GVResultadoReporte.PagerSettings.PageButtonCount =
               int.Parse(Math.Ceiling((double)(ResultadoConsulta.Rows.Count / (double)GVResultadoReporte.PageSize)).ToString());
               GVResultadoReporte.PagerSettings.Visible = false;
               GVResultadoReporte.DataSource = ResultadoConsulta;
               GVResultadoReporte.DataBind();
               GVResultadoReporte.Visible = true;
               lblTotalRegistrosGrilla.Text = ResultadoConsulta.Rows.Count.ToString();
               lblTitulocantRegistros.Visible = true;
               var cantBotones = GVResultadoReporte.PagerSettings.PageButtonCount;
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
               GVResultadoReporte.DataSource = null;
               GVResultadoReporte.DataBind();
               rptBotonesPaginacion.DataSource = null;
               rptBotonesPaginacion.DataBind();
               divGVResultado.Visible = false;
               divGVdetalle.Visible = false;
               
           }
           
           divPantallaResultado.Visible = true;
       }

       protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
       {
           var acciones = new List<string> { "Detalle" };
           if (!acciones.Contains(e.CommandName))
               return;

           // calculo el indice que corresponde según la paginación seleccionada de la grilla en la que estemos.
           GVResultadoReporte.SelectedIndex = calcularIndexPagina(Convert.ToInt32(e.CommandArgument));

           if (GVResultadoReporte.SelectedValue != null)
               EntidadSeleccionada = int.Parse(GVResultadoReporte.SelectedValue.ToString());

           switch (e.CommandName)
           {
               case "Detalle":
                   btnSalirDetalleSeleccionado.Visible = true;
                   DetalleTramite();
                   
                   break;
           }
       }

       protected void rptBotonesPaginacion_OnItemCommand(object source, RepeaterCommandEventArgs e)
       {
           int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
           GVResultadoReporte.PageIndex = nroPagina - 1;
           RefrescarGrilla();
       }

       protected void ddlDeptos_SelectedIndexChanged(object sender, EventArgs e)
       {
           CargarLocalidades();
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
               ddlDeptos.SelectedValue = "0";
               ddlLocalidad.Focus();

           }
           else
           {
               ddlDeptos.Items.Clear();
               ddlDeptos.Items.Insert(0, "NO HAY DEPARTAMENTOS CARGADOS");
           }

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
               ddlLocalidad.Enabled = true;
           }
           else
           {
               ddlLocalidad.Items.Insert(0, "NO HAY LOCALIDADES CARGADAS");
               ddlLocalidad.Enabled = false;
           }
           ddlLocalidad.SelectedValue = "0";
       }
        
       protected void btnSalirDetalleSeleccionado_OnClick(object sender, EventArgs e)
       {
           divGVResultado.Visible = true;
           divGVdetalle.Visible = false;
           btnSalirDetalleSeleccionado.Visible = false;
           lblTitulocantRegistros.Visible = true;
           lblTotalRegistrosGrilla.Visible = true;
           rptBotonesPaginacion.Visible = true;
       }

        

       
    }
}