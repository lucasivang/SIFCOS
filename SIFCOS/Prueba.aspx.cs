using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Services;

using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using AjaxControlToolkit;
using BL_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;


namespace SIFCOS
{
    public class ObjMostrar
    {
        public string IdDptoSeleccionado { get; set; }
    }

    public partial class Prueba : System.Web.UI.Page
    {
        public Principal master;
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        private static DropDownList ddlDepartamento;
        private static TextBox _txtMostrar;

        protected void Page_Load(object sender, EventArgs e)
        {
            master = (Principal) Page.Master;
            var lstRolesNoAutorizados = new List<string>();
            lstRolesNoAutorizados.Add("Secretaria de comercio");
            lstRolesNoAutorizados.Add("Boca de Recepcion");
            lstRolesNoAutorizados.Add("Gestor"); //usuario comun;
            lstRolesNoAutorizados.Add("Sin Asignar");

            if (lstRolesNoAutorizados.Contains(master.RolUsuario))
            {
                Response.Redirect("Inscripcion.aspx");
            }

            if (!IsPostBack)
            {
                divResultadoDirecciones.Visible = false;
                txtCuilUsuarioResp.Text = "";
                txtCuitEmpresa.Text = "";
                lblResultadoAsignarResponsable.Text = "";
                // limpiar();
            }

        }

        public static ReglaDeNegocios Bl = new ReglaDeNegocios();

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetDeptos()
        {
            try
            {
                var oExcep = new List<Exception>();

                DataTable dtDptos = Bl.BlGetDeptartamentos("X");
                var listaDptos = new List<Departamento>();
                foreach (DataRow row in dtDptos.Rows)
                {
                    listaDptos.Add(new Departamento
                    {
                        IdDepartamento = int.Parse(row["id_departamento"].ToString()),
                        NombreDepartamento = row["n_departamento"].ToString()
                    });
                }

                var deptos = listaDptos.Select(m => new {DisplayText = m.NombreDepartamento, Value = m.IdDepartamento});

                return new {Result = "OK", Options = deptos};
            }
            catch (Exception ex)
            {
                return new {Result = "ERROR", Message = ex.Message};
            }
        }

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void SetDepartamento(string pIdDepto)
        {
            //es necesario setear el control texbox de latitud y long. para tomar los valores del lado del servidor.
            ddlDepartamento.SelectedValue = pIdDepto;
            //ddlDepartamentos.SelectedValue  = pIdDepto;
            _txtMostrar.Text = pIdDepto;
        }


        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetLocalidades(int pIdDepto)
        {
            try
            {
                var oExcep = new List<Exception>();

                DataTable dtLocalidades = Bl.BlGetLocalidades(pIdDepto.ToString());
                var listaDptos = new List<Localidad>();
                foreach (DataRow row in dtLocalidades.Rows)
                {
                    listaDptos.Add(new Localidad
                    {
                        IdLocalidad = int.Parse(row["id_localidad"].ToString()),
                        NombreLocalidad = row["n_localidad"].ToString()
                    });
                }

                var localidades = listaDptos.Select(m => new {DisplayText = m.NombreLocalidad, Value = m.IdLocalidad});


                return new {Result = "OK", Options = localidades};
            }
            catch (Exception ex)
            {
                return new {Result = "ERROR", Message = ex.Message};
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] BuscarProducto(string prefixText, int count)
        {
            List<Producto> _productos = Bl.BlGetProductosbeta(prefixText.ToUpper()).ToList();

            string[] lista = new string[_productos.Count];
            var contador = 0;
            foreach (var row_producto in _productos)
            {
                lista[contador] =
                    AutoCompleteExtender.CreateAutoCompleteItem(row_producto.NProducto, row_producto.IdProducto);
                contador++;
            }

            return lista;
        }

        protected void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {

        }


        private void GuardarContacto()
        {
            // contacto

            var entidad = "2006439862900"; //idSedeSeleccionada;
            var resultado = "";

            resultado =
                Bl.BlRegistrarContacto(new Comunicacion()
                {
                    IdEntidad = entidad,
                    CodAreaCel = "0351",
                    CodAreaTelFijo = "0351",
                    EMail = "facu@gmail.com    ",
                    Facebook = "",
                    NroCel = "2392086",
                    NroTelfijo = "425138",
                    PagWeb = "www.google.com.ar"
                });


            //cargo Contacto nuevo

            if (resultado == "OK")
            {
                //se cargó el contacto con exito.

            }


        }

        protected void btnContacto_OnClick(object sender, EventArgs e)
        {
            GuardarContacto();
        }


        protected void btnAbrirTab_OnClick_OnClick(object sender, EventArgs e)
        {
            AbrirOtraPestania();


        }

        private void AbrirOtraPestania()
        {
            Response.Redirect("MisTramites.aspx?Exito=1");

        }

        protected void btnAbrirTab2_OnClick_OnClick(object sender, EventArgs e)
        {

            Response.Redirect("ReporteGeneral.aspx");
        }

        protected void btnObtenerValorCombo_OnClick(object sender, EventArgs e)
        {
            //var valor = txtMostrar.Text;
            //txtMostrar.Text = "SelectedValue : " +  ddlDepartamentos.SelectedValue + "  SelectedItem.Value : " +ddlDepartamentos.SelectedItem.Value ;
        }







        protected void btnAbrirMisTramites_OnClick(object sender, EventArgs e)
        {
            var dt = Bl.BlGetComEmpresa(246101.ToString());
            var lista = new List<Trs>();
            var t1 = new Trs
            {
                NroTransaccion = "00000000010919652016",
                Link = "https://tasas.cba.gov.ar/GenerarCedulon.aspx?id=00000000010919652016"
            };
            var t3 = new Trs
            {
                NroTransaccion = "00000000010941712016",
                Link = "https://tasas.cba.gov.ar/GenerarCedulon.aspx?id=00000000010941712016"
            };
            var t2 = new Trs
            {
                NroTransaccion = "00000000010943852016",
                Link = "https://tasas.cba.gov.ar/GenerarCedulon.aspx?id=00000000010943852016"
            };

            lista.Add(t1);
            lista.Add(t2);
            lista.Add(t3);
            Session["ListaTrs"] = lista;
            Response.Redirect("ListaTrsReempa.aspx");
            //Response.Redirect("MisTramites.aspx?Exito=1");
        }

        protected void btnScript_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                "err_msg",
                "alert('Ejecuto con exito waso!)');",
                true);
        }



        public void ImprimirReporteTramite()
        {
            //var tramiteDto = new InscripcionSifcosDto();
            //
            var lista = Bl.GetInscripcionSifcosDto(4).ToList();
            if (lista.Count == 0)
                return;
            InscripcionSifcosDto tramiteDto = lista[0];


            tramiteDto.ActividadPrimaria = "ACtividad 1";
            tramiteDto.ActividadSecundaria = "Actividad 2";
            tramiteDto.NroHabMunicipal = "55";



            var lis = new List<Producto>();
            DataTable dtProductosTramite = Bl.BlGetProductosTramite(tramiteDto.NroTramiteSifcos);
            foreach (DataRow row in dtProductosTramite.Rows)
            {
                lis.Add(new Producto
                    {IdProducto = row["idproducto"].ToString(), NProducto = row["nproducto"].ToString()});
            }

            DataTable dtContacto = Bl.BlGetComEmpresa(tramiteDto.NroTramiteSifcos);

            foreach (DataRow row in dtContacto.Rows)
            {
                switch (row["ID_TIPO_COMUNICACION"].ToString())
                {
                    case "01": //TELEFONO FIJO
                        tramiteDto.CodAreaTelFijo = row["COD_AREA"].ToString();
                        tramiteDto.TelFijo = row["NRO_MAIL"].ToString();
                        break;
                    case "07": //CELULAR
                        tramiteDto.CodAreaCelular = row["COD_AREA"].ToString();
                        tramiteDto.Celular = row["NRO_MAIL"].ToString();
                        break;
                    case "11": //EMAIL
                        tramiteDto.EmailEstablecimiento = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString())
                            ? row["NRO_MAIL"].ToString()
                            : "no registrado";
                        break;
                    case "12": //PAGINA WEB
                        tramiteDto.WebPage = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString())
                            ? row["NRO_MAIL"].ToString()
                            : "no registrado";
                        break;
                    case "17": //FACEBOOK
                        tramiteDto.Facebook = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString())
                            ? row["NRO_MAIL"].ToString()
                            : "no registrado";
                        break;

                }
            }

            tramiteDto.CelularGestor = "0351-1520168";
            tramiteDto.CelularRepLegal = "0351-15203858";



            var dtSuperficie = Bl.BlGetSuperficeByNroTramite(Convert.ToInt64(tramiteDto.NroTramiteSifcos));

            foreach (DataRow row in dtSuperficie.Rows)
            {
                switch (row["id_tipo_superficie"].ToString())
                {
                    case "1": //SUP. ADMINISTRACION 
                        tramiteDto.SupAdministracion = string.IsNullOrEmpty(row["superficie"].ToString())
                            ? 0
                            : long.Parse(row["superficie"].ToString());
                        break;
                    case "2": //SUP. VENTAS
                        tramiteDto.SupVentas = string.IsNullOrEmpty(row["superficie"].ToString())
                            ? 0
                            : long.Parse(row["superficie"].ToString());
                        break;
                    case "3": //SUP. DEPOSITO
                        tramiteDto.SupDeposito = string.IsNullOrEmpty(row["superficie"].ToString())
                            ? 0
                            : long.Parse(row["superficie"].ToString());
                        break;
                }
            }
            // Creo el nuevo reporte con NOMBRE DEL REPORTE

            var nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoS.rdlc";


            var reporte = new ReporteGeneral(nombreReporteRdlc, lis, TipoArchivoEnum.Pdf);


            reporte.AddParameter("parametro_Titulo_reporte",
                "Comprobante de Trámite - " + tramiteDto.NombreTipoTramite.ToUpper());
            reporte.AddParameter("nroTramiteSifcos", tramiteDto.NroTramiteSifcos);
            reporte.AddParameter("paramatro_dom1_departamento", "PRUEBA");
            reporte.AddParameter("paramatro_dom1_localidad", "PRUEBA");
            reporte.AddParameter("paramatro_dom1_barrio", "PRUEBA");
            reporte.AddParameter("paramatro_dom1_calle", "PRUEBA");
            reporte.AddParameter("paramatro_dom1_nroCalle", "PRUEBA");
            reporte.AddParameter("paramatro_dom1_dpto", "PRUEBA");
            reporte.AddParameter("paramatro_dom1_piso", "PRUEBA");
            reporte.AddParameter("paramatro_dom1_codPos", "PRUEBA");
            reporte.AddParameter("paramatro_dom1_oficina", tramiteDto.Oficina);
            reporte.AddParameter("paramatro_dom1_local", tramiteDto.Local);
            reporte.AddParameter("paramatro_dom1_stand", tramiteDto.Stand);
            reporte.AddParameter("paramatro_contacto_email",
                !string.IsNullOrEmpty(tramiteDto.EmailEstablecimiento)
                    ? tramiteDto.EmailEstablecimiento
                    : "no encontrado");
            reporte.AddParameter("paramatro_contacto_facebook",
                !string.IsNullOrEmpty(tramiteDto.Facebook) ? tramiteDto.Facebook : "no encontrado");
            reporte.AddParameter("paramatro_contacto_WebPage",
                !string.IsNullOrEmpty(tramiteDto.WebPage) ? tramiteDto.WebPage : "no encontrado");
            reporte.AddParameter("paramatro_contacto_celular",
                !string.IsNullOrEmpty(tramiteDto.Celular)
                    ? "(" + tramiteDto.CodAreaCelular + ") " + tramiteDto.Celular
                    : "no registrado");
            reporte.AddParameter("paramatro_contacto_telFijo",
                !string.IsNullOrEmpty(tramiteDto.TelFijo)
                    ? "(" + tramiteDto.CodAreaTelFijo + ") " + tramiteDto.TelFijo
                    : "no registrado");
            reporte.AddParameter("paramatro_dom2_departamento", "PRUEBA");
            reporte.AddParameter("paramatro_dom2_localidad", "PRUEBA");
            reporte.AddParameter("paramatro_dom2_barrio", "PRUEBA");
            reporte.AddParameter("paramatro_dom2_calle", "PRUEBA");
            reporte.AddParameter("paramatro_dom2_nroCalle", "PRUEBA");
            reporte.AddParameter("paramatro_dom2_dpto", "PRUEBA");
            reporte.AddParameter("paramatro_dom2_piso", "PRUEBA");
            reporte.AddParameter("paramatro_dom2_codPos", "PRUEBA");
            reporte.AddParameter("paramatro_dom2_oficina", tramiteDto.Stand);
            reporte.AddParameter("paramatro_dom2_local", tramiteDto.Local);
            reporte.AddParameter("paramatro_dom2_stand", tramiteDto.Oficina);
            reporte.AddParameter("paramatro_fecInicioAct", tramiteDto.FecIniTramite.ToShortDateString());
            reporte.AddParameter("paramatro_nroHabMunicipal", tramiteDto.NroHabMunicipal);
            reporte.AddParameter("paramatro_nroDGR", tramiteDto.NroDGR);
            reporte.AddParameter("paramatro_supVenta",
                tramiteDto.SupVentas != null ? tramiteDto.SupVentas.Value.ToString() : "-");
            reporte.AddParameter("paramatro_supDeposito",
                tramiteDto.SupDeposito != null ? tramiteDto.SupDeposito.Value.ToString() : "-");
            reporte.AddParameter("paramatro_supAdm",
                tramiteDto.SupAdministracion != null ? tramiteDto.SupAdministracion.Value.ToString() : "-");
            reporte.AddParameter("paramatro_cantPersTotal", tramiteDto.CantTotalpers);
            reporte.AddParameter("paramatro_cantPersRelacionDep", tramiteDto.CantPersRelDep);
            reporte.AddParameter("paramatro_poseeCobertura", tramiteDto.Cobertura);
            reporte.AddParameter("paramatro_realizoCapacitacion", tramiteDto.CapacUltAnio);
            reporte.AddParameter("paramatro_poseeSeguro", tramiteDto.Seguro);
            reporte.AddParameter("paramatro_origenProveedor", tramiteDto.NombreOrigenProveedor);
            reporte.AddParameter("parametro_empresa_cuit", tramiteDto.CUIT);
            reporte.AddParameter("parametro_empresa_razonSocial", tramiteDto.RazonSocial);
            reporte.AddParameter("parametro_empresa_AnioOperativo", DateTime.Now.Year.ToString());
            reporte.AddParameter("parametro_actividadPrimaria", tramiteDto.ActividadPrimaria);
            reporte.AddParameter("parametro_actividadSecundaria", tramiteDto.ActividadSecundaria);
            reporte.AddParameter("parametro_estado_tramite", tramiteDto.NombreEstadoActual);
            reporte.AddParameter("parametro_fecha_vencimiento",
                tramiteDto.FecVencimiento.Day + "/" + tramiteDto.FecVencimiento.Month + "/" +
                tramiteDto.FecVencimiento.Year);

            //cargo los datos del gestor y responsable
            reporte.AddParameter("parametro_gestor_nombre", tramiteDto.NombreYApellidoGestor);
            reporte.AddParameter("parametro_gestor_dni", tramiteDto.DniGestor);
            reporte.AddParameter("parametro_gestor_telefono", "PRUEBA"); //tramiteDto.CelularGestor);
            reporte.AddParameter("parametro_gestor_tipo", tramiteDto.NombreTipoGestor);
            reporte.AddParameter("parametro_responsable_nombre", tramiteDto.NombreYApellidoRepLegal);
            reporte.AddParameter("parametro_responsable_dni", tramiteDto.DniRepLegal);
            reporte.AddParameter("parametro_responsable_telefono", tramiteDto.CelularRepLegal);
            reporte.AddParameter("parametro_responsable_cargo", tramiteDto.NombreCargoRepLegal);

            var inmueble = tramiteDto.Propietario == "S"
                ? "Propietario"
                : "Inquilino con Alquiler de " + tramiteDto.RangoAlquiler;
            reporte.AddParameter("parametro_propietario", inmueble);
            reporte.AddParameter("parametro_nombre_fantasia", tramiteDto.NombreFantasia);

            // Guardo datos del reporte en sessión
            Session["ReporteGeneral"] = reporte;

            //LEVANTA LA PAGINA RerporteGeneral
            Response.Redirect("ReporteSIFCoS.aspx");

        }

        public Usuario UsuarioCidiLogueado
        {
            get
            {
                return Session["UsuarioCiDiLogueado"] == null
                    ? new Usuario()
                    : (Usuario) Session["UsuarioCiDiLogueado"];
            }
            set { Session["UsuarioCiDiLogueado"] = value; }
        }

        protected Usuario ObtenerUsuarioCuil(String cuil)
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


            // UsuarioCidiLogueado = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Basicos_CUIL, entrada);

            UsuarioCidiLogueado =
                Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Basicos_Domicilio_CUIL,
                    entrada);

            if (UsuarioCidiLogueado.Respuesta.Resultado == Config.CiDi_OK)
            {
                usu = UsuarioCidiLogueado;
                return usu;

            }

            return UsuarioCidiLogueado;

        }

        protected void btnImprimirTramite_OnClick(object sender, EventArgs e)
        {
            ImprimirReporteTramite();
        }




        //protected void btnConsultarDuplicados_OnClick(object sender, EventArgs e)
        //{
        //        refrescarGrilla();
        //       // divMensajeError.Visible = false;


        //}
        //private int calcularIndexPagina(int indexActual)
        //{
        //    //PORQUE LA PAGINACIÓN ESTÁ SETEADA EN 10 REGISTROS POR PAGINA.
        //    if (indexActual < gvDuplicados.PageSize)
        //        return indexActual;
        //    var resto = indexActual % gvDuplicados.PageSize;

        //    return resto;
        //    //var paginaActual = (indexActual - resto) / gvResultado.PageSize;
        //    //return paginaActual;
        //}
        //private void refrescarGrilla()
        //{
        //    var dtConsulta = Bl.BLConsultarDuplicados(txtFechaDesde.Text,txtFechaHasta.Text);
        //    Session["Duplicados"] = dtConsulta;
        //    gvDuplicados.PagerSettings.Mode = PagerButtons.Numeric;

        //    if (dtConsulta.Rows.Count > 0)
        //    {
        //        var indexPaginado = calcularIndexPagina(gvDuplicados.SelectedIndex);// calculo el indice que corresponse según la paginación seleccionada de la grilla en la que estemos.
        //        gvDuplicados.PagerSettings.PageButtonCount =
        //            int.Parse(Math.Ceiling((double)(dtConsulta.Rows.Count / (double)gvDuplicados.PageSize)).ToString());
        //        gvDuplicados.PagerSettings.Visible = false;
        //        gvDuplicados.DataSource = dtConsulta;
        //        gvDuplicados.DataBind();
        //        lblTitulocantRegistros.Visible = true;
        //        lblTotalRegistrosGrilla.Text = dtConsulta.Rows.Count.ToString();
        //        var cantBotones = gvDuplicados.PagerSettings.PageButtonCount;
        //        var listaNumeros = new ArrayList();

        //        for (int i = 0; i < cantBotones; i++)
        //        {
        //            var datos = new
        //            {
        //                nroPagina = i + 1
        //            };
        //            listaNumeros.Add(datos);
        //        }
        //        rptBotonesPaginacion.DataSource = listaNumeros;
        //        rptBotonesPaginacion.DataBind();

        //    }
        //    else
        //    {
        //        lblTotalRegistrosGrilla.Text = "No se encontraron registros que coincidan con el filtro de búsqueda";
        //        lblTitulocantRegistros.Visible = false;
        //    }

        //}


        //protected void rptBotonesPaginacion_OnItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
        //    gvDuplicados.PageIndex = nroPagina - 1;

        //}

        protected void btnNroPagina_OnClick(object sender, EventArgs e)
        {
            banderaPrimeraCargaPagina = true;

            var btn = (LinkButton) sender;
            //guardo el comando del boton de pagina seleccinoado
            commandoBotonPaginaSeleccionado = btn.CommandArgument;
        }

        //protected void btnLimpiar_OnClick(object sender, EventArgs e)
        //{
        //    txtFechaDesde.Text = "";
        //    txtFechaHasta.Text = "";
        //    gvDuplicados.DataSource = null;
        //    gvDuplicados.DataBind();
        //    lblTitulocantRegistros.Visible = false;
        //    lblTotalRegistrosGrilla.Text = "";
        //    lblTitulocantRegistros.Visible = false;
        //    lblTotalRegistrosGrilla.Text = "";
        //    txtFechaDesde.Focus();
        //}

        //protected void limpiar()
        //{
        //    txtFechaDesde.Text = "";
        //    txtFechaHasta.Text = "";
        //    gvDuplicados.DataSource = null;
        //    gvDuplicados.DataBind();
        //    lblTitulocantRegistros.Visible = false;
        //    lblTotalRegistrosGrilla.Text = "";
        //    lblTitulocantRegistros.Visible = false;
        //    lblTotalRegistrosGrilla.Text = "";
        //    txtFechaDesde.Focus();
        //}

        protected void btnBuscarCIDI_OnClick(object sender, EventArgs e)
        {
            ObtenerUsuarioCuil(txtCuilCIDI.Text);
            if (UsuarioCidiLogueado.Apellido != null)
            {
                txtEmailCIDI.Text = UsuarioCidiLogueado.Email;
                txtCelularCIDI.Text = UsuarioCidiLogueado.CelFormateado;
                txtNomYApeCIDI.Text = UsuarioCidiLogueado.NombreFormateado;
                txtCalleCIDI.Text = UsuarioCidiLogueado.Domicilio.Calle;
                txtAlturaCIDI.Text = UsuarioCidiLogueado.Domicilio.Altura + " " + UsuarioCidiLogueado.Domicilio.Depto;
                txtDeptoCIDI.Text = UsuarioCidiLogueado.Domicilio.Departamento;
                txtLocalidadCIDI.Text = UsuarioCidiLogueado.Domicilio.Localidad;
            }
            else
            {
                txtEmailCIDI.Text = "no existe";
                txtCelularCIDI.Text = "no existe";
                txtNomYApeCIDI.Text = "no existe";
                txtAlturaCIDI.Text = "no existe";
                txtCalleCIDI.Text = "no existe";
                txtDeptoCIDI.Text = "no existe";
                txtLocalidadCIDI.Text = "no existe";
            }


        }

        protected void btnConsultaDNI_OnClick(object sender, EventArgs e)
        {
            var dtConsultaPersonas = Bl.BlGetPersonasRcivil2(txtDNIPersona.Text, txtIdSexo.Text);
            if (dtConsultaPersonas.Rows.Count > 0)
            {
                GVPersonasRcivil.DataSource = dtConsultaPersonas;
                GVPersonasRcivil.DataBind();
            }
            else
            {
                lblResultadoPrueba.Text = "No se encontraron registros cargados con ese dni y ese sexo.";
                MostrarOcultarModal(divModalResultado, true);
            }
        }

        protected void InsertaCUIL_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCUILG.Text) || string.IsNullOrEmpty(txtDNIG.Text) ||
                string.IsNullOrEmpty(txtSEXOG.Text) || string.IsNullOrEmpty(txtID_NUMEROG.Text) ||
                string.IsNullOrEmpty(txtPaisG.Text))
            {
                lblResultadoPrueba.Text = "Debe ingresar todos los valores obligatorios.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            var InsertarCUIL = Bl.BlGenerarCUIL(txtCUILG.Text.Trim(), Int64.Parse(txtDNIG.Text.Trim()), txtSEXOG.Text,
                Int64.Parse(txtID_NUMEROG.Text.Trim()), txtPaisG.Text.Trim());
            lblResultadoGenCUIL.Text = InsertarCUIL;

        }

        protected void btnLimpiarDatos_OnClick(object sender, EventArgs e)
        {
            GVPersonasRcivil.DataSource = null;
            GVPersonasRcivil.DataBind();
            txtDNIPersona.Text = "";
            txtDNIPersona.Focus();
            txtIdSexo.Text = "";
            txtCUILG.Text = "";
            txtDNIG.Text = "";
            txtSEXOG.Text = "";
            txtID_NUMEROG.Text = "";
            txtPaisG.Text = "";
            lblResultadoGenCUIL.Text = "";
            Response.Redirect("Prueba.aspx");
        }

        protected void btnConsultaSQL_OnClick(object sender, EventArgs e)
        {
            ResultadoRule result = new ResultadoRule();
            if (ddlBaseDatos.SelectedValue == "0")
            {
                divMensajeErrorSQL.Visible = true;
                lblMensajeErrorSQL.Text = "Obligatorio ingresar BD.";
                btnLimpiarSQL.Focus();
                return;

            }
            var resultado = Bl.BlConsultaSQL(txtSQL.Text,ddlBaseDatos.SelectedValue,out result);
            if (result.OcurrioError)
            {
                lblRegistrosSQL.Text = result.MensajeError;
                return;
            }
            if (resultado.Rows.Count > 0 && resultado!=null)
                {
                    GVResultadoSQL.DataSource = resultado;
                    GVResultadoSQL.DataBind();
                    lblCantRegistrosSQL.Text = resultado.Rows.Count.ToString();
                    lblCantRegistrosSQL.Visible = true;
                    lblRegistrosSQL.Visible = true;
                    GVResultadoSQL.Focus();
                }
            
            
        }

        protected void btnConsultarDirecciones_OnClick(object sender, EventArgs e)
        {
            String pCuit = txtCuit.Text.Trim();
            var pNroSifcos = txtNroSifcosC.Text.Trim();
            if (string.IsNullOrEmpty(pNroSifcos))
            {
                pNroSifcos = "0";
            }

            lblResultadoEstadoComercio.Text = "";
            rptBotonesPaginacion.DataSource = null;
            rptBotonesPaginacion.DataBind();
            gvDirecciones.DataSource = null;
            gvDirecciones.DataBind();
            var ConsultaDirecciones = Bl.BlGetTramitesSifcos(pCuit, Int64.Parse(pNroSifcos));
            if (ConsultaDirecciones.Rows.Count > 0)
            {
                divResultadoDirecciones.Visible = true;
                gvDirecciones.PagerSettings.Visible = false;
                gvDirecciones.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double) (ConsultaDirecciones.Rows.Count / (double) gvDirecciones.PageSize))
                        .ToString());

                lblTotalRegistrosGrilla.Visible = true;
                lblTotalRegistrosGrilla.Text = ConsultaDirecciones.Rows.Count.ToString();
                lblTitulocantRegistros.Visible = true;
                lblTitulocantRegistros.Focus();

                rptBotonesPaginacion.Visible = true;
                var cantBotones = gvDirecciones.PagerSettings.PageButtonCount;
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
                gvDirecciones.DataSource = ConsultaDirecciones;
                gvDirecciones.DataBind();
                gvDirecciones.Visible = true;
                gvDirecciones.Focus();
            }
            else
            {
                lblResultadoPrueba.Text = "No se encontraron sucursales para el Cuit ingresado.";
                MostrarOcultarModal(divModalResultado, true);
                divResultadoDirecciones.Visible = false;
                gvDirecciones.Focus();


            }



        }

        protected void btnLimpiar_OnClick(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void LimpiarControles()
        {

            txtCuit.Text = "";
            lblResultadoEstadoComercio.Text = "";

            lblTotalRegistrosGrilla.Visible = false;
            lblTitulocantRegistros.Visible = false;
            TITULO.Visible = false;

            txtCuit.Focus();
            rptBotonesPaginacion.Visible = false;
            Response.Redirect("Prueba.aspx");

        }


        protected void rptBotonesPaginacion_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                var btn = (LinkButton) e.Item.FindControl("btnNroPagina");
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
            if (indexActual < gvDirecciones.PageSize)
                return indexActual;
            var resto = indexActual % gvDirecciones.PageSize;

            return resto;
            //var paginaActual = (indexActual - resto) / gvResultado.PageSize;
            //return paginaActual;
        }

        protected void rptBotonesPaginacion_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int nroPagina = Convert.ToInt32(e.CommandArgument.ToString());
            gvDirecciones.PageIndex = nroPagina - 1;
            RefrescarGrillaDirecciones();
        }

        protected void gvDirecciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDirecciones.PageIndex = e.NewPageIndex;
            RefrescarGrillaDirecciones();
        }

        private void RefrescarGrillaDirecciones()
        {
            gvDirecciones.PagerSettings.Mode = PagerButtons.Numeric;

            var pCuit = txtCuit.Text;
            var pNroSifcos = txtNroSifcosC.Text;
            if (string.IsNullOrEmpty(pNroSifcos))
            {
                pNroSifcos = "0";
            }

            var Sucursales = Bl.BlGetTramitesSifcos(pCuit, Int64.Parse(pNroSifcos));
            if (Sucursales.Rows.Count > 0)
            {
                gvDirecciones.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double) (Sucursales.Rows.Count / (double) gvDirecciones.PageSize))
                        .ToString());
                gvDirecciones.PagerSettings.Visible = false;
                gvDirecciones.DataSource = Sucursales;
                gvDirecciones.DataBind();
                lblTotalRegistrosGrilla.Visible = true;
                lblTitulocantRegistros.Visible = true;
                lblTotalRegistrosGrilla.Text = Sucursales.Rows.Count.ToString();
                var cantBotones = gvDirecciones.PagerSettings.PageButtonCount;
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
                lblResultadoEstadoComercio.Text = "No se encontraron registros que coincidan con el filtro de búsqueda";
                lblTitulocantRegistros.Visible = false;
            }

        }


        protected void btnLimpiarSQL_OnClick(object sender, EventArgs e)
        {
            GVResultadoSQL.DataSource = null;
            GVResultadoSQL.DataBind();
            gvDirecciones.DataSource = null;
            gvDirecciones.DataBind();
            txtSQL.Text = "";
            txtSQL.Focus();
            lblCantRegistrosSQL.Visible = false;
            lblRegistrosSQL.Visible = false;
        }

        protected void btnCambiarTitular_OnClick(object sender, EventArgs e)
        {
            var cambiarTitular = Bl.BlCambiarTitularTRS(txtTRSNroLiq.Text, txtTRSSexo.Text, txtTRSCUIL.Text,
                Int64.Parse(txtTRSDNI.Text));
            lblResultadoPrueba.Text = cambiarTitular;
            MostrarOcultarModal(divModalResultado, true);
        }

        protected void btnLimpiarDatos2_OnClick(object sender, EventArgs e)
        {
            txtTRSCUIL.Text = "";
            txtTRSDNI.Text = "";
            txtTRSNroLiq.Text = "";
            txtTRSSexo.Text = "";
            lblResultadoCambiarTitularTRS.Text = "";
            txtTRSNroLiq.Focus();

        }

        protected void btnBuscarRCIVILNOMYAPE_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombreRCIVIL.Text))
            {
                lblResultadoPrueba.Text = "El campo NOMBRE es obligatorio.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            if (String.IsNullOrEmpty(txtApellidoRCIVIL.Text))
            {
                lblResultadoPrueba.Text = "El campo APELLIDO es obligatorio.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            var dtConsultaPorNomYApe = Bl.BlGetPersonasRcivil3(txtNombreRCIVIL.Text, txtApellidoRCIVIL.Text);
            if (dtConsultaPorNomYApe.Rows.Count > 0)
            {
                GVRCIVIL.DataSource = dtConsultaPorNomYApe;
                GVRCIVIL.DataBind();
            }
            else
            {
                lblResultadoPrueba.Text = "No se encontraron registros existentes.";
                MostrarOcultarModal(divModalResultado, true);
            }
        }

        protected void btnLimpiarEliminarEstado_OnClick(object sender, EventArgs e)
        {
            lblResultadoEliminarEstado.Text = "";
            txtNroTramiteEliminarEstado.Text = "";
            Response.Redirect("Prueba.aspx");
            txtNroTramiteEliminarEstado.Focus();
        }

        protected void btnActEstadoVerificado_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNroTramiteEliminarEstado.Text))
            {
                lblResultadoPrueba.Text = "El campo nro. de trámite es obligatorio.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            DataTable Estado = Bl.BlGetHistEstados(Int64.Parse(txtNroTramiteEliminarEstado.Text));
            if (Estado.Rows[0]["estado"].ToString() != "VERIFICADO BOCA")
            {
                var ActEstadoVerificado = Bl.BlActEstadoVerificado(txtNroTramiteEliminarEstado.Text);
                if (ActEstadoVerificado == "OK")
                {
                    lblResultadoPrueba.Text = "El trámite cambio a VERIFICADO BOCA";
                    MostrarOcultarModal(divModalResultado, true);
                }
                else
                {
                    lblResultadoPrueba.Text =
                        "Hubo Error en el cambio de estado no se realizó la modificación.";
                    MostrarOcultarModal(divModalResultado, true);
                }

            }
            else
            {
                lblResultadoPrueba.Text = "El trámite esta VERIFICADO BOCA.";
                MostrarOcultarModal(divModalResultado, true);
            }

        }

        protected void btnActEstadoVerMinisterio_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNroTramiteEliminarEstado.Text))
            {
                lblResultadoPrueba.Text = "El campo nro. de trámite es obligatorio.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            DataTable Estado = Bl.BlGetHistEstados(Int64.Parse(txtNroTramiteEliminarEstado.Text));
            if (Estado.Rows[0]["estado"].ToString() != "VERIFICADO MINISTERIO")
            {
                var ActEstadoVerificado = Bl.BlActEstadoVerificado(txtNroTramiteEliminarEstado.Text);
                if (ActEstadoVerificado == "OK")
                {
                    lblResultadoPrueba.Text = "El trámite cambio a VERIFICADO MINISTERIO";
                    MostrarOcultarModal(divModalResultado, true);
                }
                else
                {
                    lblResultadoPrueba.Text =
                        "Hubo Error en el cambio de estado no se realizó la modificación.";
                    MostrarOcultarModal(divModalResultado, true);
                }

            }
            else
            {
                lblResultadoPrueba.Text = "El trámite esta VERIFICADO MINISTERIO.";
                MostrarOcultarModal(divModalResultado, true);
            }
        }

        protected void btnActEstadoRechazado_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNroTramiteEliminarEstado.Text))
            {
                lblResultadoPrueba.Text = "El campo nro. de trámite es obligatorio";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            DataTable Estado = Bl.BlGetHistEstados(Int64.Parse(txtNroTramiteEliminarEstado.Text));
            if (Estado.Rows[0]["estado"].ToString() != "RECHAZADO BOCA")
            {
                var ActEstadoRechazado = Bl.BlActEstadoRechazado(txtNroTramiteEliminarEstado.Text);
                if (ActEstadoRechazado == "OK")
                {
                    lblResultadoPrueba.Text = "El trámite cambio a RECHAZADO BOCA";
                    MostrarOcultarModal(divModalResultado, true);
                }
                else
                {
                    lblResultadoPrueba.Text =
                        "Hubo Error en el cambio de estado no se realizó la modificación.";
                    MostrarOcultarModal(divModalResultado, true);
                }
            }
            else
            {
                lblResultadoPrueba.Text = "El trámite esta RECHAZADO BOCA.";
                MostrarOcultarModal(divModalResultado, true);
            }

        }

        protected void btnActEstadoRechMinisterio_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNroTramiteEliminarEstado.Text))
            {
                lblResultadoPrueba.Text = "El campo nro. de trámite es obligatorio";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            DataTable Estado = Bl.BlGetHistEstados(Int64.Parse(txtNroTramiteEliminarEstado.Text));
            if (Estado.Rows[0]["estado"].ToString() != "RECHAZADO MINISTERIO")
            {
                var ActEstadoRechazado = Bl.BlActEstadoRechazado(txtNroTramiteEliminarEstado.Text);
                if (ActEstadoRechazado == "OK")
                {
                    lblResultadoPrueba.Text = "El trámite cambio a RECHAZADO MINISTERIO";
                    MostrarOcultarModal(divModalResultado, true);
                }
                else
                {
                    lblResultadoPrueba.Text =
                        "Hubo Error en el cambio de estado no se realizó la modificación.";
                    MostrarOcultarModal(divModalResultado, true);
                }
            }
            else
            {
                lblResultadoPrueba.Text = "El trámite esta RECHAZADO MINISTERIO.";
                MostrarOcultarModal(divModalResultado, true);
            }
        }

        protected void btnGenerarRazonSocial_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCUITIPJ.Text) || string.IsNullOrEmpty(txtRazonSocialIPJ.Text))
            {
                lblResultadoPrueba.Text = "Debe ingresar todos los valores obligatorios.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            var GenerarRazonSocial = Bl.BlGenerarRazonSocial(txtCUITIPJ.Text.Trim(), txtRazonSocialIPJ.Text.ToUpper(),
                txtIngBrutoIPJ.Text.Trim());
            if (GenerarRazonSocial == "OK")
            {
                lblResultadoPrueba.Text = "El Cuit fue dado de alta en base de datos";
                MostrarOcultarModal(divModalResultado, true);
            }
            else
            {
                lblResultadoPrueba.Text = GenerarRazonSocial;
            }

        }

        protected void btnLimpiarDatosRazonSocial_OnClick(object sender, EventArgs e)
        {
            lblResultadoGenerarRazonSocial.Text = "";
            txtNroTramiteEliminarEstado.Text = "";
            Response.Redirect("Prueba.aspx");
            txtNroTramiteEliminarEstado.Focus();
        }


        protected void btnLimpiarPantalla_OnClick(object sender, EventArgs e)
        {
            txtDNIPersona.Text = "";
            txtIdSexo.Text = "";
            lblResConsultaRCIVIL.Text = "";
            Response.Redirect("Prueba.aspx");
        }

        protected void btnLimpiarPantalla2_OnClick(object sender, EventArgs e)
        {
            txtNombreRCIVIL.Text = "";
            txtApellidoRCIVIL.Text = "";
            lblResultadoNomYApeRcivil.Text = "";
            Response.Redirect("Prueba.aspx");
        }

        protected void btnGetConceptos_Click(object sender, EventArgs e)
        {
            grdConceptos.DataSource = Bl.BlGetConceptosAFecha(DateTime.Now);
            grdConceptos.DataBind();
        }

        protected void btnGetUrlTrs_Click(object sender, EventArgs e)
        {
            string a = SingletonParametroGeneral.GetInstance().IdConceptoTasaAlta;
            lbltrs.Text = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;
        }

        protected void btnGenerarTrs_Click(object sender, EventArgs e)
        {
            //string a = SingletonParametroGeneral.GetInstance().IdConceptoTasaAlta;
            //lbltrs.Text = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;
            string strConcpeto = "";
            string strMonto = "";
            Int64 outNroTramite;
            bool nroTramiteOK = false;
            nroTramiteOK = long.TryParse(txtNroTramite.Text, out outNroTramite);

            if (!nroTramiteOK)
            {
                lblretornoTrs.Text = "Nro tramite invalido";
                return;
            }


            //Obtengo el Número de trámite
            var _tramite = Bl.GetInscripcionSifcosDto(Convert.ToInt64(txtNroTramite.Text)).ToList()[0];
            int IdTipoTramiteTrs = 4;

            if (_tramite.IdTipoTramite == "1" || _tramite.IdTipoTramite == "5" || _tramite.IdTipoTramite == "7" ||
                _tramite.IdTipoTramite == "9")
                IdTipoTramiteTrs = 1;

            string oFechaVenc;
            string oHashTrx;
            string oIdTransaccion;
            string oNroLiqOriginal;
            string strIdConcepto = SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;
            String fecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoReempadronamiento;

            lblretornoTrs.Text = Bl.BlSolicitarTrs(IdTipoTramiteTrs, _tramite.CUIT, master.UsuarioCidiLogueado.CUIL,
                out oFechaVenc, out oHashTrx, out oIdTransaccion, out oNroLiqOriginal, out strIdConcepto,
                out fecDesdeConcepto, out strMonto, out strConcpeto);

            lblnrotransacTrs.Text = oIdTransaccion;
            lblHashTrs.Text = oHashTrx;

        }


        public class Departamento
        {
            public int IdDepartamento { get; set; }
            public string NombreDepartamento { get; set; }
        }

        public class Localidad
        {
            public int IdLocalidad { get; set; }
            public string NombreLocalidad { get; set; }
        }

        protected void btnEliminarTramite_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNroTramiteElim.Text))
            {
                lblResultadoPrueba.Text = "Debe ingresar NRO TRAMITE.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            var EliminarTramite = Bl.BlEliminarTramite(txtNroTramiteElim.Text);
            if (EliminarTramite == "OK")
            {
                lblResultadoPrueba.Text = "El trámite fue eliminado correctamente";
                MostrarOcultarModal(divModalResultado, true);
                
            }
            else
            {
                lblResultadoPrueba.Text = EliminarTramite;
                MostrarOcultarModal(divModalResultado, true);
            }

        }

        protected void btnEliminarNotificaciones_OnClick(object sender, EventArgs e)
        {
            var EliminarNotificaciones = Bl.BlEliminarNotificaciones();
            if (EliminarNotificaciones == "OK")
            {
                lblResultadoPrueba.Text = "Las notificaciones fueron eliminadas correctamente.";
                MostrarOcultarModal(divModalResultado, true);

            }
            else
            {
                lblResultadoPrueba.Text = EliminarNotificaciones;
                MostrarOcultarModal(divModalResultado, true);
            }
        }

        protected void btnAsignarNroSifcos_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNroTramiteAsig.Text))
            {
                lblResultadoPrueba.Text = "Debe ingresar NRO TRAMITE para asignarle el nro sifcos.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }
            if (string.IsNullOrEmpty(txtNroSifcosAsig.Text))
            {
                lblResultadoPrueba.Text = "Debe ingresar NRO SIFCOS para asignarle el nro sifcos.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            var AsignarNroSifcos = Bl.BlAsignarNroSifcos(txtNroTramiteAsig.Text,txtNroSifcosAsig.Text,UsuarioCidiLogueado.CUIL);
            if (AsignarNroSifcos == "OK")
            {
                lblResultadoPrueba.Text = "El Nro Sifcos fue asignado correctamente";
                MostrarOcultarModal(divModalResultado, true);

            }
            else
            {
                lblResultadoPrueba.Text = "Error en asignar nro Sifcos: " + AsignarNroSifcos;
                MostrarOcultarModal(divModalResultado, true);
            }

        }

        protected void btnAsignarResponsable_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCuilUsuarioResp.Text))
            {
                lblResultadoPrueba.Text = "Debe ingresar cuil como responsable la empresa.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }
            if (string.IsNullOrEmpty(txtCuitEmpresa.Text))
            {
                lblResultadoPrueba.Text = "Debe ingresar cuit de la empresa que desea asignar.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            if (ddlRolAsignar.SelectedValue=="0")
            {
                lblResultadoPrueba.Text = "Debe Seleccionar un Rol.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }
            string pIdRol = "0";

            if (ddlRolAsignar.SelectedValue != "0")
            {
                switch (ddlRolAsignar.SelectedValue)
                {
                    case "1":
                        pIdRol = "10";
                        break;
                    case "2":
                        pIdRol = "11";
                        break;
                    case "3":
                        pIdRol = "12";
                        break;
                    case "4":
                        pIdRol = "13";
                        break;
                    case "5":
                        pIdRol = "14";
                        break;
                    case "6":
                        pIdRol = "15";
                        break;
                    case "7":
                        pIdRol = "16";
                        break;
                    case "8":
                        pIdRol = "17";
                        break;
                    case "9":
                        pIdRol = "18";
                        break;
                    case "10":
                        pIdRol = "19";
                        break;
                }
 
            }

            
                



            var AsignarResponsable = Bl.BlAsignarResponsable(txtCuitEmpresa.Text, txtCuilUsuarioResp.Text,pIdRol);
            if (AsignarResponsable == "OK")
            {
                lblResultadoPrueba.Text = "El CUIT nro: " + txtCuitEmpresa.Text + " Se le asigno el CUIL de la persona: " + txtCuilUsuarioResp.Text;
                MostrarOcultarModal(divModalResultado, true);

            }
            else
            {
                lblResultadoPrueba.Text = "Error en asignar Responsable: El Rol '" + ddlRolAsignar.SelectedItem.Text + "' ya existe con ese Usuario Cidi, elija otro rol";
                MostrarOcultarModal(divModalResultado, true);
            }

        }

        protected void btnQuitarResponsable_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCuilUsuarioResp.Text))
            {
                lblResultadoPrueba.Text = "Debe ingresar cuil como responsable la empresa.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }
            if (string.IsNullOrEmpty(txtCuitEmpresa.Text))
            {
                lblResultadoPrueba.Text = "Debe ingresar cuit de la empresa que desea asignar.";
                MostrarOcultarModal(divModalResultado, true);
                return;
            }

            var EliminarResponsable = Bl.BlEliminarResponsable(txtCuitEmpresa.Text, txtCuilUsuarioResp.Text);
            if (EliminarResponsable == "OK")
            {
                lblResultadoPrueba.Text = "Se elimino la relacion entre El CUIT nro: " + txtCuitEmpresa.Text + " y la persona: " + txtCuilUsuarioResp.Text;
                MostrarOcultarModal(divModalResultado, true);

            }
            else
            {
                lblResultadoPrueba.Text = "Error al Elimnar Relacion: " + EliminarResponsable;
                MostrarOcultarModal(divModalResultado, true);
            }

        }
        protected void btnSalirPrueba_OnClick(object sender, EventArgs e)
        {
            lblResultadoPrueba.Text = "";
            MostrarOcultarModal(divModalResultado, false);
        }
        
        

        #region METODO MOSTRAR MODAL

        private void MostrarOcultarModal(HtmlControl divModal, bool mostrar)
        {
            //diver.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarModal";
                string[] listaStrings = divModal.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                );
                divModal.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                divModal.Attributes.Add("class", String.Join(" ", divModal
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "mostrarModal" })
                    .ToArray()
                ));
            }
        }

        #endregion

        
    }

}