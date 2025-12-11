using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL_SIFCOS;
using DA_SIFCOS;
using System.IO; 
using ListItem = System.Web.UI.WebControls.ListItem;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Ciudadano_Digital;


namespace SIFCOS
{
   public partial class ReporteComercios : System.Web.UI.Page
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
                CargarDeptos();
             
                divPantallaInfoBoca.Visible = true;
                divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;  
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
            var banderaUtilizaFiltro = false;

            if (!string.IsNullOrEmpty(txtFechaDesde.Text) && !string.IsNullOrEmpty(txtFechaHasta.Text))
            {
                if (DateTime.ParseExact(txtFechaDesde.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(txtFechaHasta.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    lblMensajeError.Text = "La fecha de inicio no puede superar a la fecha fin.";
                    divMensajeError.Visible = true;
                    return;
                }
            }

            if (ResultadoConsulta.Count == 0)
            {
                //la primera consulta garga en memoria todos los comercios (alrededor de 82 mil registros. Tarda 10 minutos la consulta.
                ResultadoConsulta = Bl.BlReporteComercios(txtFechaDesde.Text,txtFechaHasta.Text,ddlDeptos.SelectedValue,ddlLocalidad.SelectedValue);
            }
            
           
            if (!string.IsNullOrEmpty(ddlDeptos.SelectedValue) && ddlDeptos.SelectedValue != "0")
            {
                banderaUtilizaFiltro = true;
                GVResultadoReporte.Columns[3].Visible =false; //columna departamento no se muestra
                //Si selecciona departamento y localidad, consulta solo por localidad
                if (!string.IsNullOrEmpty(ddlLocalidad.SelectedValue) && ddlLocalidad.SelectedValue != "0")
                {
                    DataSourceGrilla = ResultadoConsulta.Where(x => x.ID_LOCALIDAD == ddlLocalidad.SelectedValue).ToList();
                }
                else
                {
                    //si solo selecciona departamento, consulta solo por departamento.
                    DataSourceGrilla = ResultadoConsulta.Where(x => x.ID_DEPARTAMENTO == ddlDeptos.SelectedValue).ToList();
                } 
            }

            if (!string.IsNullOrEmpty(ddlLocalidad.SelectedValue) && ddlLocalidad.SelectedValue != "0")
            {
                GVResultadoReporte.Columns[4].Visible = false; //columna localidad no se muestra
            }


            if (!string.IsNullOrEmpty(txtFechaDesde.Text))
            {
                banderaUtilizaFiltro = true;

                DateTime fechaDesde = DateTime.ParseExact(txtFechaDesde.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (DataSourceGrilla.Count == 0)
                {
                    try
                    {
                        DataSourceGrilla = ResultadoConsulta.Where(x => DateTime.ParseExact(x.FEC_VTO.Substring(0, 10), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) >= fechaDesde).ToList();
                    }
                    catch (System.FormatException ex)
                    {
                        DataSourceGrilla = ResultadoConsulta.Where(x => DateTime.ParseExact(x.FEC_VTO.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture) >= fechaDesde).ToList();
                    }
                }
                else
                {
                    var listaAux = new List<ComercioDto>();
                    try
                    {
                        listaAux = DataSourceGrilla.Where(x => DateTime.ParseExact(x.FEC_VTO.Substring(0, 10), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) >= fechaDesde).ToList();
                    }
                    catch (System.FormatException ex)
                    {
                        listaAux = DataSourceGrilla.Where(x => DateTime.ParseExact(x.FEC_VTO.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture) >= fechaDesde).ToList();
                    }

                    DataSourceGrilla = listaAux;
                } 
            }

            if (!string.IsNullOrEmpty(txtFechaHasta.Text))
            {
                banderaUtilizaFiltro = true;

                DateTime fechaHasta = DateTime.ParseExact(txtFechaHasta.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (DataSourceGrilla.Count == 0)
                {
                    try
                    {
                        DataSourceGrilla = ResultadoConsulta.Where(x => DateTime.ParseExact(x.FEC_VTO.Substring(0, 10), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) <= fechaHasta).ToList();    
                    }
                    catch (System.FormatException ex)
                    {
                        DataSourceGrilla = ResultadoConsulta.Where(x => DateTime.ParseExact(x.FEC_VTO.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture) <= fechaHasta).ToList();
                    }
                    
                }
                else
                {
                    var listaAux = new List<ComercioDto>();
                    try
                    {
                        listaAux = DataSourceGrilla.Where(x => DateTime.ParseExact(x.FEC_VTO.Substring(0, 10), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) <= fechaHasta).ToList();
                    }
                    catch (System.FormatException ex)
                    {
                        listaAux = DataSourceGrilla.Where(x => DateTime.ParseExact(x.FEC_VTO.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture) <= fechaHasta).ToList();
                    }
                    
                    DataSourceGrilla = listaAux;
                } 
            }

            if (DataSourceGrilla.Count == 0 || (DataSourceGrilla.Count > 0 && !banderaUtilizaFiltro))
            {
                /*Si no está cargada la grilla, la carga con la consulta origen que se alimenta*/
                /*Si está cargada la grilla, y limpio y no utiliza filtro. También debe traer la consulta origen que se alimenta*/
                DataSourceGrilla = ResultadoConsulta;
            }
             

            if (ddlOrdenPor.SelectedValue != "0")
            {
                var listaOrdenada = new List<ComercioDto>(); 
                switch (ddlOrdenPor.SelectedValue)
                {
                    case "LOCALIDAD":
                        listaOrdenada = DataSourceGrilla.OrderBy(x => x.LOCALIDAD).ToList();
                        break;
                    case "DEPARTAMENTO":
                        listaOrdenada = DataSourceGrilla.OrderBy(x => x.DEPARTAMENTO).ToList();
                        break;
                    case "NRO_SIFCOS":
                        listaOrdenada = DataSourceGrilla.OrderBy(x => x.NRO_SIFCOS).ToList();
                        break;
                    case "DEBE":
                        listaOrdenada = DataSourceGrilla.OrderBy(x => x.DEBE).ToList();
                        break;
                    case "FECHA_VTO":
                        listaOrdenada = DataSourceGrilla.OrderBy(x => x.FEC_VTO).ToList();
                        break;
                }
                DataSourceGrilla = listaOrdenada;
            }
            
            RefrescarGrilla();
             divPantallaResultado.Visible = true;
            
        }

       public List<ComercioDto> ResultadoConsulta
       {
           get
           {
               return Session["ResultadoConsulta"] == null ? new List<ComercioDto>() : (List<ComercioDto>)Session["ResultadoConsulta"] ;
           }
           set
           {
               Session["ResultadoConsulta"] = value;
           }
       }

       public List<ComercioDto> DataSourceGrilla
       {
           get
           {
               return Session["DataSourceGrilla"] == null ? new List<ComercioDto>() : (List<ComercioDto>)Session["DataSourceGrilla"];
           }
           set
           {
               Session["DataSourceGrilla"] = value;
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
           txtFechaDesde.Text = "";
           txtFechaHasta.Text = "";
           rptBotonesPaginacion.DataSource = null;
           rptBotonesPaginacion.DataBind();
           divPantallaResultado.Visible = false;
            
           //divPantallaResultado.Visible = false;
           btnConsultar.Enabled = true;

        }
        
         

        protected void btnPDF_OnClick(object sender, EventArgs e)
        {
            if (ResultadoConsulta == null )
                return; 
            
            var reporte = new ReporteGeneral("SIFCOS.rptReporte.rdlc", ResultadoConsulta, TipoArchivoEnum.Pdf);
            reporte.AddParameter("Parametro_Fecha_Desde", !string.IsNullOrEmpty(txtFechaDesde.Text) ? txtFechaDesde.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Fecha_Hasta", !string.IsNullOrEmpty(txtFechaHasta.Text) ? txtFechaHasta.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_Boca_Recepcion", !string.IsNullOrEmpty(txtBocaRecepcion.Text) ? txtBocaRecepcion.Text : " NO APLICA ");


            Session["ReporteGeneral"] = reporte;
            Response.Redirect("ReporteGeneral.aspx");

        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            if(ResultadoConsulta == null)
                return;

            var reporte = new ReporteGeneral("SIFCOS.rptReporteComercios.rdlc", DataSourceGrilla, TipoArchivoEnum.Excel);
            reporte.AddParameter("Parametro_fec_vto_desde", !string.IsNullOrEmpty(txtFechaDesde.Text) ? txtFechaDesde.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_fec_vto_hasta", !string.IsNullOrEmpty(txtFechaHasta.Text) ? txtFechaHasta.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_depto", ddlDeptos.SelectedItem.Text=="SIN ASIGNAR" ? ddlDeptos.SelectedItem.Text : " NO APLICA ");
            reporte.AddParameter("Parametro_localidad", ddlLocalidad.SelectedItem.Text=="SIN ASIGNAR" ? ddlLocalidad.SelectedItem.Text : " NO APLICA ");

            Session["ReporteGeneral"] = reporte;
            Response.Redirect("ReporteGeneral.aspx");


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
           if (DataSourceGrilla.Count > 0)
           {
               btnVolverReporte.Visible = true;
               divGVResultado.Visible = true;
               GVResultadoReporte.PagerSettings.PageButtonCount =
               int.Parse(Math.Ceiling((double)(DataSourceGrilla.Count / (double)GVResultadoReporte.PageSize)).ToString());
               GVResultadoReporte.PagerSettings.Visible = false;
               GVResultadoReporte.DataSource = DataSourceGrilla;
               GVResultadoReporte.DataBind();
               GVResultadoReporte.Visible = true;
               lblTotalRegistrosGrilla.Text = DataSourceGrilla.Count.ToString();
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
               divGVResultado.Visible = false;
                
               
           }
           
           //divPantallaResultado.Visible = true;
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
               ddlLocalidad.Items.Insert(0, new ListItem("SIN ASIGNAR", "0"));

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
            
           lblTitulocantRegistros.Visible = true;
           lblTotalRegistrosGrilla.Visible = true;
           rptBotonesPaginacion.Visible = true;
           btnVolverReporte.Visible = false;
       }

        //protected void btnTipoTramite_Click(object sender, EventArgs e)
        //{
        //    divMensajeExito.Visible = false;
        //    divMensajeError.Visible = false;
        //    //divSeccionTipoTramite.Visible = true;
        //    //divSeccionFechaAlta.Visible = false;
        //    //divSeccionComercio.Visible = false;
        //    //divSeccionRangoTramite.Visible = false;
        //    //divSeccionUbicacion.Visible = false;
        //    //divSeccionActividad.Visible = false;
        //    //divGuardarCambios.Visible = true;
        //    Seleccion = "TipoTramite";
        //    IniciarReporte();
        //}

        //protected void btnFecAlta_Click(object sender, EventArgs e)
        //{
        //    divMensajeExito.Visible = false;
        //    divMensajeError.Visible = false;
        //    //divSeccionTipoTramite.Visible = false;
        //    //divSeccionFechaAlta.Visible = true;
        //    //divSeccionComercio.Visible = false;
        //    //divSeccionRangoTramite.Visible = false;
        //    //divSeccionUbicacion.Visible = false;
        //    //divSeccionActividad.Visible = false;
        //    //divGuardarCambios.Visible = true;
        //    Seleccion = "FecAlta";
        //    IniciarReporte();
        //}

        //protected void btnComercio_Click(object sender, EventArgs e)
        //{
        //    divMensajeExito.Visible = false;
        //    divMensajeError.Visible = false;
        //    //divSeccionTipoTramite.Visible = false;
        //    //divSeccionFechaAlta.Visible = false;
        //    //divSeccionComercio.Visible = true;
        //    //divSeccionRangoTramite.Visible = false;
        //    //divSeccionUbicacion.Visible = false;
        //    //divSeccionActividad.Visible = false;
        //    //divGuardarCambios.Visible = true;
        //    Seleccion = "Comercio";
        //    IniciarReporte();
        //}

        //protected void btnRangoTramite_Click(object sender, EventArgs e)
        //{
        //    divMensajeExito.Visible = false;
        //    divMensajeError.Visible = false;
        //    //divSeccionTipoTramite.Visible = false;
        //    //divSeccionFechaAlta.Visible = false;
        //    //divSeccionComercio.Visible = false;
        //    //divSeccionRangoTramite.Visible = true;
        //    //divSeccionUbicacion.Visible = false;
        //    //divSeccionActividad.Visible = false;
        //    //divGuardarCambios.Visible = true;
        //    Seleccion = "RangoTramite";
        //    IniciarReporte();
        //}

        //protected void btnUbicacion_Click(object sender, EventArgs e)
        //{
        //    divMensajeExito.Visible = false;
        //    divMensajeError.Visible = false;
        //    //divSeccionTipoTramite.Visible = false;
        //    //divSeccionFechaAlta.Visible = false;
        //    //divSeccionComercio.Visible = false;
        //    //divSeccionRangoTramite.Visible = false;
        //    //divSeccionUbicacion.Visible = true;
        //    //divSeccionActividad.Visible = false;
        //    //divGuardarCambios.Visible = true;
        //    Seleccion = "Ubicacion";
        //    IniciarReporte();
        //}

        //protected void btnActividad_Click(object sender, EventArgs e)
        //{
        //    divMensajeExito.Visible = false;
        //    divMensajeError.Visible = false;
        //    //divSeccionTipoTramite.Visible = false;
        //    //divSeccionFechaAlta.Visible = false;
        //    //divSeccionComercio.Visible = false;
        //    //divSeccionRangoTramite.Visible = false;
        //    //divSeccionUbicacion.Visible = false;
        //    //divSeccionActividad.Visible = true;
        //    //divGuardarCambios.Visible = true;
        //    Seleccion = "Actividad";
        //    IniciarReporte();
        //}
        //private void IniciarReporte()
        //{
        //    divMensajeError.Visible = false;
        //    divMensajeExito.Visible = false;
        //    var tramiteDto = new InscripcionSifcosDto();

        //    switch (Seleccion)
        //    {
        //        case "TipoTramite":
        //            //REPORTE DE TRAMITES POR TIPO DE TRAMITE
        //            divSeccionTipoTramite.Visible = true;

        //            break;
        //        case "FecAlta":
        //            //REPORTE DE TRAMITES POR FECHA DE ALTA
        //            divSeccionFechaAlta.Visible = true;

        //            break;
        //        case "Comercio":
        //            // REPORTE DE COMERCIOS FILTRADOS POR DNI Y SEXO, RAZON SOCIAL O CUIT
        //            divSeccionComercio.Visible = true;


        //            break;
        //        case "RangoTramite":
        //            //REPORTE POR RANGO DE NRO DE TRAMITE O NRO SIFCOS
        //            divSeccionRangoTramite.Visible = true;

        //            break;
        //        case "Ubicacion":
        //            //REPORTE POR UBICACION GEOGRAFICA DEL TRAMITE
        //            divSeccionUbicacion.Visible = true;

        //            break;
        //        case "Actividad":
        //            //REPORTE POR TIPO DE ACTIVIDAD
        //            divSeccionActividad.Visible = true;
        //            break;

        //    }

        //}

       protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
       {
           var acciones = new List<string> { "Contacto" };
           if (!acciones.Contains(e.CommandName))
               return;

           // calculo el indice que corresponde según la paginación seleccionada de la grilla en la que estemos.
           GVResultadoReporte.SelectedIndex = calcularIndexPagina(Convert.ToInt32(e.CommandArgument));

           if (GVResultadoReporte.SelectedValue != null)
               EntidadSeleccionada = int.Parse(GVResultadoReporte.SelectedValue.ToString());

           switch (e.CommandName)
           {
               case "Contacto":
                   btnVolverReporte.Visible = true;
                   Contacto();

                   break;
           }
       }

       private void Contacto()
       {
           
           ResultadoConsultaDetalle = Bl.BLConsultarContacto(EntidadSeleccionada.ToString());
           GvContacto.DataSource = ResultadoConsultaDetalle;
           GvContacto.DataBind();
           GvContacto.Visible = true;
           divGVResultado.Visible = false;
           divContacto.Visible = true;
           lblTitulocantRegistros.Visible = false;
           lblTotalRegistrosGrilla.Visible = false;
           rptBotonesPaginacion.Visible = false;
           btnExcel.Focus();
       }

       protected void btnVolverReporte_OnClick(object sender, EventArgs e)
       {
           divGVResultado.Visible = true;
           divContacto.Visible = false;
           btnVolverReporte.Visible = false;
           lblTitulocantRegistros.Visible = true;
           lblTotalRegistrosGrilla.Visible = true;
           rptBotonesPaginacion.Visible = true;
       }
    }
}