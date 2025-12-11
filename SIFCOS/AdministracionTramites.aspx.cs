using BL_SIFCOS;
using CryptoManagerV4._0.Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using DA_SIFCOS.Entities.CDDAutorizador;
using DA_SIFCOS.Entities.CDDPost;
using DA_SIFCOS.Entidades;
using DA_SIFCOS.Entities.Errores;
using DA_SIFCOS.Utils;
using DA_SIFCOS.Entities.CDDResponse;
using DA_SIFCOS.Entities.Excepcion;
using CryptoManagerV4._0.General;
using iTextSharp.text.pdf;
using iTextSharp.text;
using AjaxControlToolkit;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;
using AppComunicacion;

namespace SIFCOS
{
    public partial class AdministracionTramites : System.Web.UI.Page
    {
        private const String Key_Cif_Decif = "Warrior2025";
        private string commandoBotonPaginaSeleccionado = "";
        private bool banderaPrimeraCargaPagina = false;
        protected DataTable DtConsulta = new DataTable();
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();
        protected DataTable DtGrilla = new DataTable();
        public Principal master;

        private CryptoDiffieHellman ObjDiffieHellman { get; set; }
        private Autorizador ObjAutorizador { get; set; }
        private CDDPost RequestPost { get; set; }
        public CngKey Private_Key { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            master = (Principal)Page.Master;
            var lstRolesNoAutorizados = new List<string>();
            //lstRolesNoAutorizados.Add("Administrador General");
            lstRolesNoAutorizados.Add("Boca de Recepcion");
            //lstRolesNoAutorizados.Add("Secretaria de comercio");
            lstRolesNoAutorizados.Add("Gestor");//usuario comun;
            lstRolesNoAutorizados.Add("Sin Asignar");
            divMjeExitoRepLegal.Visible = false;
            divMjeErrorRepLegal.Visible = false;
            divMensajeError.Visible = false;
            divMensajeErrorBaja.Visible = false;
            divMensajeExito.Visible = false;
            divMensajeErrorBaja.Visible = false;
            ocultarPaneles(false);
            //lblSinResultado.Text = "";
            if (lstRolesNoAutorizados.Contains(master.RolUsuario))
            {
                Response.Redirect("Solicitud.aspx");
            }
            tramiteDto = new InscripcionSifcosDto();
            if (!IsPostBack)
            {
                Seleccion = "";
                MostrarOcultarDiv(false);
                CargarInfoBoca();
                divPantallaInfoBoca.Visible = true;
                divPantallaConsulta.Visible = true;
                divPantallaResultado.Visible = false;
                divPantallaVerTramite.Visible = false;

                //cargarComboOrdenConsulta();
                //cargarComboEstadosTramite();

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
        private int IdDocumentoCDD
        {
            get { return (int)Session["IdDocumentoCDD"]; }
            set { Session["IdDocumentoCDD"] = value; }
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
            divPantallaResultado.Visible = false;

        }

        protected void gvResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var consultaTramite = (consultaTramite)e.Row.DataItem;

                var btnCambiarEstado = (Button)e.Row.FindControl("btnCambiarEstado");
                var btnImprimirTasa = (Button)e.Row.FindControl("BtnImprimirTasa");
                var btnModificarTramite = (Button)e.Row.FindControl("BtnModificar");
                var btnAsignarNroSifcos = (Button)e.Row.FindControl("btnAsignarNroSifcos");
                var btnEnviar = (ImageButton)e.Row.FindControl("btnEnviar");
                var btnEnviar2 = (ImageButton)e.Row.FindControl("btnEnviar2");
                var btnNotificado = (ImageButton)e.Row.FindControl("btnNotificado");

                if (consultaTramite != null)
                {
                    switch (consultaTramite.estado)
                    {
                        case "CARGADO":
                            consultaTramite.id_estado = "1";
                            break;
                        case "VERIFICADO BOCA":
                            consultaTramite.id_estado = "2";
                            break;
                        case "RECHAZADO BOCA":
                            consultaTramite.id_estado = "3";
                            break;
                        case "VERIFICADO MINISTERIO":
                            consultaTramite.id_estado = "4";
                            break;
                        case "RECHAZADO MINISTERIO":
                            consultaTramite.id_estado = "5";
                            break;
                        case "AUTORIZADO MINISTERIO":
                            consultaTramite.id_estado = "6";
                            break;
                        case "REIMPRESION VERIFICADA":
                            consultaTramite.id_estado = "12";
                            break;
                        case "REIMPRESION AUTORIZADA":
                            consultaTramite.id_estado = "13";
                            break;
                        case "BAJA VERIFICADA":
                            consultaTramite.id_estado = "14";
                            break;
                        case "BAJA AUTORIZADA":
                            consultaTramite.id_estado = "15";
                            break;
                    }
                    //if (consultaTramite.Tipo_Tramite == "ALTA" && consultaTramite.estado == "CARGADO")
                    //{
                    //    //List<Trs> dtDeudaTramite = Bl.BlGetTasasPagadasSinUsarCombo(consultaTramite.CUIT);
                    //    DataTable dtDeudaTramite = Bl.BlGetDeudaTramite(Int64.Parse(consultaTramite.Nro_tramite));
                    //    btn.CssClass = dtDeudaTramite.Rows.Count == 0 ? "botonAdvertencia" : "botonOk";
                    //}
                    //else
                    //{
                    //    //Si es Reempadronamiento , ó Alta con estado Verificado en Boca , nunca va a tener deuda el tramite porque siempre se asocia a una tasa paga.
                    //    btn.CssClass = "botonOk";
                    //}

                    /* Si el estado actual del tramite es :RECHAZADO BOCA ,RECHAZADO MINISTERIO , AUTORIZADO MINISTERIO , RECHAZADO SIN TASA PAGA , RECHAZADO CON DEV DE TASA, ANULADO
					   no debe poder cambiar el estado. Se oculta el boton
					*/
                    if (consultaTramite.estado == "AUTORIZADO MINISTERIO" ||
                        consultaTramite.estado == "RECHAZADO SIN TASA PAGA" ||
                        consultaTramite.estado == "RECHAZADO CON DEV DE TASA" ||
                        consultaTramite.estado == "RECHAZADO MINISTERIO")
                    {
                        btnCambiarEstado.Visible = false;
                    }
                    if (consultaTramite.estado == "ANULADO")
                    {
                        btnImprimirTasa.Visible = false;
                        btnModificarTramite.Visible = false;
                        btnAsignarNroSifcos.Visible = false;
                        btnCambiarEstado.Visible = false;
                    }
                    if (consultaTramite.Nro_Sifcos != "SIN ASIGNAR")
                    {
                        btnAsignarNroSifcos.Visible = false;

                    }
                    //actualizacion julio 2025
                    //Consultar el ultimo tramite autorizado del contribuyente y verificar si esta vencido
                    //Si esta Vencido Verificar si ya ha sido notificado, si no hay sido notificado habilitar el boton notificar
                    //Para notificar se consulta al representante legal del tramite y se notifica al cuil del mismo 
                    DateTime? fecVto = new DateTime();
                    String Notificado = string.Empty;
                    if (consultaTramite.idEntidad == null && !string.IsNullOrEmpty(consultaTramite.Nro_Sifcos))
                    {
                        consultaTramite.idEntidad = Bl.BlGetIdEntidad(consultaTramite.Nro_tramite);
                    }

                    Notificado = Bl.BlgetNotificado(consultaTramite.idEntidad);
                    if (!string.IsNullOrEmpty(consultaTramite.Nro_Sifcos) && consultaTramite.Nro_Sifcos != "SIN ASIGNAR")
                    {
                        fecVto = Bl.BlGetFechaUltimoTramiteSifcosNuevo(consultaTramite.Nro_Sifcos);
                        if (string.IsNullOrEmpty(fecVto.ToString()))
                        {
                            fecVto = Bl.BlGetFechaUltimoTramiteSifcosViejo(consultaTramite.Nro_Sifcos);
                        }

                    }

                    if (!string.IsNullOrEmpty(Notificado) && consultaTramite.Vto_Tramite.ToString().Substring(0, 10) == fecVto.ToString().Substring(0, 10))
                    {
                        btnNotificado.Visible = true;
                        btnEnviar.Visible = false;

                        if (!string.IsNullOrEmpty(Notificado))
                        {
                            btnNotificado.ToolTip = "Fue notificado el " + Notificado;
                        }


                    }
                    else
                    {
                        btnNotificado.Visible = false;
                    }
                    //consulta por la fecha de vencimiento del tramite que sea menor a la fecha de hoy y
                    //ademas que sea la fecha de vencimiento del ultimo tramite realizado
                    if (fecVto < DateTime.Now && consultaTramite.Vto_Tramite.ToString().Substring(0, 10) == fecVto.ToString().Substring(0, 10) && string.IsNullOrEmpty(Notificado))
                    {
                        btnEnviar.Visible = true;
                        btnEnviar2.Visible = false;
                        e.Row.Cells[7].ForeColor = Color.Red;
                        e.Row.Cells[7].BorderColor = Color.Black;
                        e.Row.Cells[7].Font.Bold = true;
                    }
                    else
                    {
                        btnEnviar.Visible = false;
                        btnEnviar2.Visible = false;
                        e.Row.Cells[7].ForeColor = Color.Black;
                        e.Row.Cells[7].BorderColor = Color.Empty;
                        e.Row.Cells[7].Font.Bold = false;
                    }

                    /* Si el tramite esta cargado y no tiene numero sifcos notificar luego de 18 dias de generado */
                    if (consultaTramite.Tipo_Tramite == "ALTA" && consultaTramite.estado != "AUTORIZADO POR MINISTERIO")
                    {
                        consultaTramite.idEntidad = Bl.BlGetIdEntidad(consultaTramite.Nro_tramite);
                        // Fecha que se obtiene de la fecha de alta del tramite mas 18 dias para notificar
                        var vtoTramiteAlta = Bl.BlGetFechaVtoTramiteAltaSinNroSifcos(consultaTramite.idEntidad);
                        if (!string.IsNullOrEmpty(vtoTramiteAlta))
                        {
                            if (DateTime.Parse(vtoTramiteAlta) < DateTime.Now && string.IsNullOrEmpty(Notificado))
                            {
                                btnEnviar2.Visible = true;
                                btnEnviar2.ToolTip = "Posee TRS sin pagar";
                                btnEnviar.Visible = false;
                                btnNotificado.Visible = false;
                                e.Row.Cells[7].ForeColor = Color.Red;
                                e.Row.Cells[7].BorderColor = Color.Black;
                                e.Row.Cells[7].Font.Bold = true;

                            }
                        }


                    }

                    if (consultaTramite.Tipo_Tramite == "BAJA" || consultaTramite.Tipo_Tramite == "REIMPRESION DE OBLEA SIN TRS PAGA" || consultaTramite.Tipo_Tramite == "REIMPRESION DE CERTIFICADO SIN TRS PAGA"
                        || consultaTramite.Tipo_Tramite == "REIMPRESION DE OBLEA Y CERT SIN TRS PAGA" || consultaTramite.Tipo_Tramite == "REIMPRESION DE OBLEA CON TRS PAGA")
                    {

                        btnEnviar2.Visible = false;
                        btnEnviar.Visible = false;
                        btnNotificado.Visible = false;
                        e.Row.Cells[7].ForeColor = Color.Black;
                        e.Row.Cells[7].BorderColor = Color.Empty;
                        e.Row.Cells[7].Font.Bold = false;

                    }



                    /* Si el tramite es reempadronamiento o baja no se muestra el boton de imprimir TRS */
                    if (consultaTramite.Tipo_Tramite == "BAJA" || consultaTramite.Tipo_Tramite == "REEMPADRONAMIENTO")
                    {
                        btnImprimirTasa.Visible = false;
                    }
                    //    /* Si el tramite es una reimpresion sin TRS PAGA se cargan solo los estado reimpresion verificada o rechazado boca*/
                    //    if (consultaTramite.Tipo_Tramite == "REIMPRESION DE OBLEA SIN TRS PAGA" ||
                    //        consultaTramite.Tipo_Tramite == "REIMPRESION DE OBLEA Y CERT SIN TRS PAGA" ||
                    //        consultaTramite.Tipo_Tramite == "REIMPRESION DE CERTIFICADO SIN TRS PAGA")
                    //    {
                    //        cargarComboEstados_2(consultaTramite.id_estado);
                    //        mostrarTRSPAGAS.Visible = false;
                    //        txtModalDescripcionEstado.Text = "El tramite solo debe verificarse sin asignar una TRS DISPONIBLE PAGADA";
                    //    }
                    //    else
                    //    {
                    //            if (consultaTramite.Tipo_Tramite == "ALTA")

                    //            {
                    //                cargarComboEstados_0(consultaTramite.id_estado);
                    //                mostrarTRSPAGAS.Visible = true;
                    //                txtModalDescripcionEstado.Text = "El tramite debe verificarse asignando una TRS DISPONIBLE PAGADA";
                    //            }
                    //            else
                    //            {
                    //                if (consultaTramite.Tipo_Tramite != "BAJA")
                    //                {
                    //                    cargarComboEstados_1(consultaTramite.id_estado);
                    //                    mostrarTRSPAGAS.Visible = true;
                    //                    txtModalDescripcionEstado.Text = "El tramite debe verificarse asignando una TRS DISPONIBLE PAGADA";
                    //                }
                    //            }
                    //    }
                    //if (consultaTramite.Tipo_Tramite == "BAJA")
                    //    {
                    //        cargarComboEstados_3(consultaTramite.id_estado);
                    //        mostrarTRSPAGAS.Visible = false;
                    //        txtModalDescripcionEstado.Text = "El tramite solo debe verificarse sin asignar una TRS DISPONIBLE PAGADA";
                    //    }

                }

                var hiddenControl = (HiddenField)e.Row.FindControl("hiddenIdEstadoTramiteSeleccionado");
                if (consultaTramite != null)
                {
                    hiddenControl.Value = consultaTramite.id_estado;
                }
                var btnVerDoc1 = (Button)e.Row.FindControl("btnVerDoc1");
                var btnVerDoc2 = (Button)e.Row.FindControl("btnVerDoc2");
                var btnVerDoc3 = (Button)e.Row.FindControl("btnVerDoc3");
                var btnVerDoc4 = (Button)e.Row.FindControl("btnVerDoc4");

                IdDocumentoCDD = Bl.blGetIdDocumentoCDD(int.Parse(consultaTramite.Nro_tramite), 1);
                if (IdDocumentoCDD != 0)
                {
                    //btnVerDoc1.CssClass = "botonDocumentacion";
                    btnVerDoc1.ToolTip = "Descargar Documentacion Adjuntada";
                }
                else
                {
                    btnVerDoc1.Visible = false;
                    //btnVerDoc1.CssClass = "botonNoArchivo";
                    //btnVerDoc1.ToolTip = "Su Documento no fue adjuntado o esta dañado, intente adjuntarlo nuevamente";
                }

                IdDocumentoCDD = Bl.blGetIdDocumentoCDD(int.Parse(consultaTramite.Nro_tramite), 2);
                if (IdDocumentoCDD != 0)
                {
                    //btnVerDoc2.CssClass = "botonDocumentacion";
                    btnVerDoc2.ToolTip = "Descargar Documentacion Adjuntada";
                }
                else
                {
                    //btnVerDoc2.CssClass = "botonNoArchivo";
                    btnVerDoc2.Visible = false;
                    //btnVerDoc2.ToolTip = "Su Documento no fue adjuntado o esta dañado, intente adjuntarlo nuevamente";
                }

                //- lt : nuevos botones en boca ministerio

                IdDocumentoCDD = Bl.blGetIdDocumentoCDD(int.Parse(consultaTramite.Nro_tramite), 3);
                if (IdDocumentoCDD != 0)
                {
                    //btnVerDoc1.CssClass = "botonDocumentacion";
                    btnVerDoc3.ToolTip = "Descargar Documentacion Adjuntada";
                }
                else
                {
                    btnVerDoc3.Visible = false;
                    //btnVerDoc1.CssClass = "botonNoArchivo";
                    //btnVerDoc1.ToolTip = "Su Documento no fue adjuntado o esta dañado, intente adjuntarlo nuevamente";
                }

                IdDocumentoCDD = Bl.blGetIdDocumentoCDD(int.Parse(consultaTramite.Nro_tramite), 4);
                if (IdDocumentoCDD != 0)
                {
                    //btnVerDoc2.CssClass = "botonDocumentacion";
                    btnVerDoc4.ToolTip = "Descargar Documentacion Adjuntada";
                }
                else
                {
                    //btnVerDoc2.CssClass = "botonNoArchivo";
                    btnVerDoc4.Visible = false;
                    //btnVerDoc2.ToolTip = "Su Documento no fue adjuntado o esta dañado, intente adjuntarlo nuevamente";
                }





                IdDocumentoCDD = 0;
            }


        }

        protected void gvResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvResultado.PageIndex = e.NewPageIndex;
            RefrescarGrilla();
        }
       
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string[] BuscarRazonSocial(string prefixText)
        {
            List<PersonaJuridica> _RazonSocial = Bl_static.BlGetRazonSocial(prefixText.ToUpper()).ToList();

            string[] lista = new string[_RazonSocial.Count];
            var contador = 0;
            foreach (var row_RazonSocial in _RazonSocial)
            {
                lista[contador] =
                    AutoCompleteExtender.CreateAutoCompleteItem(row_RazonSocial.RazonSocial, row_RazonSocial.Cuit);
                contador++;
            }

            return lista;
        }
        private void RefrescarGrilla()
        {
            String FechaDesde = txtFechaDesde.Text.Trim();
            String FechaHasta = txtFechaHasta.Text.Trim();
            String IdEstado = ddlEstadoTramite.SelectedValue.ToString();
            String TipoTramite = "0";
            if (ddlTipoTramite.SelectedValue != "0")
            {
                TipoTramite = ddlTipoTramite.SelectedItem.Text;
            }

            var listaTramites = Bl.BlGetTramitesBoca(txtNroTramite.Text, txtNroSifcos.Text, txtCuit.Text, txtRazonSocial.Text.ToUpper(), Int64.Parse(ddlOrdenConsulta.SelectedValue), FechaDesde, FechaHasta, IdEstado, TipoTramite);
            Session["ListaTramites"] = listaTramites;
            gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

            if (listaTramites.Count > 0)
            {

                gvResultado.PagerSettings.PageButtonCount =
                    int.Parse(Math.Ceiling((double)(listaTramites.Count / (double)gvResultado.PageSize)).ToString());
                gvResultado.PagerSettings.Visible = false;
                gvResultado.DataSource = listaTramites;
                gvResultado.DataBind();
                lblTitulocantRegistros.Visible = true;
                lblTotalRegistrosGrilla.Text = listaTramites.Count.ToString();
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
                listaTramites = Bl.BlGetTramitesBaja(txtNroTramite.Text, txtNroSifcos.Text, txtCuit.Text, txtRazonSocial.Text.ToUpper(), Int64.Parse(ddlOrdenConsulta.SelectedValue), Int64.Parse(IdEstado));
                Session["ListaTramites"] = listaTramites;
                gvResultado.PagerSettings.Mode = PagerButtons.Numeric;

                if (listaTramites.Count > 0)
                {

                    gvResultado.PagerSettings.PageButtonCount =
                        int.Parse(
                            Math.Ceiling((double)(listaTramites.Count / (double)gvResultado.PageSize)).ToString());
                    gvResultado.PagerSettings.Visible = false;
                    gvResultado.DataSource = listaTramites;
                    gvResultado.DataBind();
                    lblTitulocantRegistros.Visible = true;
                    lblTotalRegistrosGrilla.Text = listaTramites.Count.ToString();
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
            divPantallaResultado.Visible = true;
        }
        private void cargarSoloTRSpagas()
        {
            var lista = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
            ddlListaTRS.Items.Clear();
            if (lista.Count == 0)
                return;
            tramiteDto = lista[0];
            List<Trs> ListaTRS = Bl.BlGetTasasPagadasSinUsarCombo(tramiteDto.CUIT);
            if (ListaTRS.Count > 0)
            {
                ddlListaTRS.DataSource = ListaTRS;
                ddlListaTRS.DataTextField = "NombreFormateado";
                ddlListaTRS.DataValueField = "NroTransaccion";
                ddlListaTRS.DataBind();
                btnGuardarEstado.Visible = true;
            }
            else
            {
                ddlListaTRS.Items.Insert(0, "NO POSEE TASAS PAGAS SIN USAR");
            }


        }
        private void cargarTRSpagas()
        {
            var lista = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
            ddlListaTRS.Items.Clear();
            if (lista.Count == 0)
                return;
            tramiteDto = lista[0];
            List<Trs> ListaTRS = Bl.BlGetTasasPagadasSinUsarCombo(tramiteDto.CUIT);
            if (ListaTRS.Count > 0)
            {
                ddlListaTRS.DataSource = ListaTRS;
                ddlListaTRS.DataTextField = "NombreFormateado";
                ddlListaTRS.DataValueField = "NroTransaccion";
                ddlListaTRS.DataBind();
                btnGuardarEstado.Visible = true;
            }
            else
            {
                if (ddlEstados.SelectedItem.Text == "VERIFICADO BOCA")
                {
                    btnGuardarEstado.Visible = false;

                }
                else
                {
                    btnGuardarEstado.Visible = true;
                }
                ddlListaTRS.Items.Insert(0, "NO POSEE TASAS PAGAS SIN USAR");
            }


        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            int b1 = 0;
            int b = 0;
            if (!string.IsNullOrEmpty(txtFechaDesde.Text) && !string.IsNullOrEmpty(txtFechaHasta.Text))
            {
                b1 = 1;
            }
            if (string.IsNullOrEmpty(txtCuit.Text) && string.IsNullOrEmpty(txtNroSifcos.Text) &&
                string.IsNullOrEmpty(txtRazonSocial.Text) && string.IsNullOrEmpty(txtNroTramite.Text))
            {
                b = 1;

            }

            if (b1 == 1)
            {
                if (DateTime.Parse(txtFechaDesde.Text) > DateTime.Parse(txtFechaHasta.Text))
                {
                    lblMensajeError.Text = "La Fecha Hasta debe ser mayor a la Fecha Desde.";
                    divMensajeError.Visible = true;
                    return;
                }

            }

            if (b == 1 && ddlEstadoTramite.SelectedValue == "0" && ddlTipoTramite.SelectedValue == "0")
            {
                lblMensajeError.Text = "Debe ingresar al menos un filtro de búsqueda.";
                divMensajeError.Visible = true;
                return;
            }


            if (b == 1 && ddlEstadoTramite.SelectedValue != "0" || ddlTipoTramite.SelectedValue != "0")
            {
                HabilitarDesHabilitarCampos(false);
                RefrescarGrilla();
                btnConsultar.Enabled = false;
                divMensajeError.Visible = false;
            }

            if (b == 0)
            {
                HabilitarDesHabilitarCampos(false);
                RefrescarGrilla();
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
            if (indexActual < gvResultado.PageSize)
                return indexActual;
            var resto = indexActual % gvResultado.PageSize;

            return resto;

        }

        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var acciones = new List<string> { "Ver", "Imprimir", "CambiarEstado", "Modificar", "Deuda", "AsignarNroSifcos", "ImprimirTasa",
                "VerDocumentacion1", "VerDocumentacion2", "VerDocumentacion3", "VerDocumentacion4" ,"EnviarNotificacion","EnviarNotificacionTRS","GEO"};
            if (!acciones.Contains(e.CommandName))
                return;

            gvResultado.SelectedIndex = calcularIndexPagina(Convert.ToInt32(e.CommandArgument));


            var indexPaginado = gvResultado.SelectedIndex;// calculo el indice que corresponse según la paginación seleccionada de la grilla en la que estemos.

            var NroSifcos = gvResultado.Rows[indexPaginado].Cells[1].Text;
            var CUIT = gvResultado.Rows[indexPaginado].Cells[2].Text;


            txtDivNroTramite.Text = gvResultado.Rows[indexPaginado].Cells[0].Text;
            txtDivCuit.Text = gvResultado.Rows[indexPaginado].Cells[2].Text;
            txtDivRazonSocial.Text = gvResultado.Rows[indexPaginado].Cells[3].Text;
            //txtDivDomicilio = gvResultado.Rows[indexPaginado].Cells[4].Text;
            txtDivTipoTramite.Text = gvResultado.Rows[indexPaginado].Cells[5].Text;
            txtDivEstadoTramite.Text = gvResultado.Rows[indexPaginado].Cells[6].Text;
            txtDivFechaUltimoCambio.Text = gvResultado.Rows[indexPaginado].Cells[7].Text;


            txtNroTramite2.Text = gvResultado.Rows[indexPaginado].Cells[0].Text;
            txtRazonSocial2.Text = gvResultado.Rows[indexPaginado].Cells[3].Text;
            txtEstadoTramite2.Text = gvResultado.Rows[indexPaginado].Cells[6].Text;
            txtFechaUltCambio2.Text = gvResultado.Rows[indexPaginado].Cells[7].Text;

            if (gvResultado.SelectedValue != null)
                EntidadSeleccionada = int.Parse(gvResultado.SelectedValue.ToString());

            var IdEntidad = Bl.BlGetIdEntidad(EntidadSeleccionada.ToString());


            switch (e.CommandName)
            {
                case "Ver":

                    VerTramite(EstadoAbmcEnum.VIENDO);
                    break;
                case "Imprimir":

                    ImprimirTramite(EstadoAbmcEnum.VIENDO);
                    break;
                case "CambiarEstado":
                    //GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    String ESTADO = "";

                    if (gvResultado.Rows[indexPaginado].Cells[6].Text != null)
                    {
                        switch (gvResultado.Rows[indexPaginado].Cells[6].Text)
                        {
                            case "CARGADO":
                                ESTADO = "1";
                                break;
                            case "VERIFICADO BOCA":
                                ESTADO = "2";
                                break;
                            case "RECHAZADO BOCA":
                                ESTADO = "3";
                                break;
                            case "VERIFICADO MINISTERIO":
                                ESTADO = "4";
                                break;
                            case "RECHAZADO MINISTERIO":
                                ESTADO = "5";
                                break;
                            case "AUTORIZADO POR MINISTERIO":
                                ESTADO = "6";
                                break;
                            case "MODIFICADO":
                                ESTADO = "9";
                                break;
                            case "REIMPRESION VERIFICADA":
                                ESTADO = "12";
                                break;
                            case "REIMPRESION AUTORIZADA":
                                ESTADO = "13";
                                break;
                            case "BAJA VERIFICADA":
                                ESTADO = "14";
                                break;
                            case "BAJA AUTORIZADA":
                                ESTADO = "15";
                                break;
                        }




                        /* Si el tramite es una reimpresion sin TRS PAGA se cargan solo los estado reimpresion verificada o rechazado boca*/
                        if (gvResultado.Rows[indexPaginado].Cells[5].Text == "REIMPRESION DE OBLEA SIN TRS PAGA" ||
                            gvResultado.Rows[indexPaginado].Cells[5].Text == "REIMPRESION DE OBLEA Y CERT SIN TRS PAGA" ||
                            gvResultado.Rows[indexPaginado].Cells[5].Text == "REIMPRESION DE CERTIFICADO SIN TRS PAGA")
                        {
                            cargarComboEstados_2(ESTADO);
                            mostrarTRSPAGAS.Visible = false;
                            txtModalDescripcionEstado.Text =
                                "El tramite solo debe verificarse sin asignar una TRS DISPONIBLE PAGADA";
                        }
                        else
                        {
                            if (gvResultado.Rows[indexPaginado].Cells[5].Text == "ALTA")
                            {
                                cargarComboEstados_0(ESTADO);
                                mostrarTRSPAGAS.Visible = true;
                                txtModalDescripcionEstado.Text = "El tramite debe verificarse asignando una TRS DISPONIBLE PAGADA";
                            }
                            else
                            {
                                if (gvResultado.Rows[indexPaginado].Cells[5].Text != "BAJA")
                                {
                                    cargarComboEstados_1(ESTADO);
                                    mostrarTRSPAGAS.Visible = true;
                                    txtModalDescripcionEstado.Text = "El tramite debe verificarse asignando una TRS DISPONIBLE PAGADA";
                                }
                            }
                        }
                        if (gvResultado.Rows[indexPaginado].Cells[5].Text == "BAJA")
                        {
                            cargarComboEstados_3(ESTADO);
                            mostrarTRSPAGAS.Visible = false;
                            txtModalDescripcionEstado.Text =
                                "El tramite solo debe verificarse sin asignar una TRS DISPONIBLE PAGADA";
                        }
                    }

                    //-LT LO DE ARRIBA LO AGREGUE
                    cargarSoloTRSpagas();
                    MostrarOcultarModalCambiarEstadoTramite(true);
                    break;
                case "Modificar":
                    MostrarOcultarModalModificarTramite(true);
                    EditarTramite(EstadoAbmcEnum.EDITANDO);
                    break;
                case "Deuda":
                    MostrarOcultarModalInfoDeudaTramite(true);
                    CargarDeudaTramite();
                    break;
                case "ImprimirTasa":

                    ImprimirTasa();
                    break;
                case "EnviarNotificacion":
                    EnviarNotificacionCIDI(CUIT, IdEntidad);
                    break;
                case "EnviarNotificacionTRS":
                    EnviarNotificacionTRSVencida(CUIT, IdEntidad);
                    break;
                case "VerDocumentacion1":
                    tramiteDto.NroTramiteSifcos = EntidadSeleccionada.ToString();
                    ImprimirDoc(1);
                    break;
                case "VerDocumentacion2":
                    tramiteDto.NroTramiteSifcos = EntidadSeleccionada.ToString();
                    ImprimirDoc(2);
                    break;
                case "VerDocumentacion3":
                    tramiteDto.NroTramiteSifcos = EntidadSeleccionada.ToString();
                    ImprimirDoc(3);
                    break;
                case "VerDocumentacion4":
                    tramiteDto.NroTramiteSifcos = EntidadSeleccionada.ToString();
                    ImprimirDoc(4);
                    break;
                case "GEO":
                    MostrarOcultardivModalMapa(true);
                    ConsultarDomComercio(EntidadSeleccionada);
                    break;
                case "AsignarNroSifcos":
                    btnConfirmarNroSifcos.Visible = true;
                    var tramite = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList()[0];
                    //if (gvResultado.Rows[indexPaginado].Cells[4].Text == "VERIFICADO BOCA" && !verificarDeuda())
                    if (!verificarDeuda())
                    {
                        //AUTOR:IB Control de obligatoriedad de rubro


                        string VerificarRubro = WebConfigurationManager.AppSettings["VerificarRubro"];
                        if (VerificarRubro == "1")
                        {
                            //string IdActPrimaria = "";
                            //string IdActSecundaria = "";
                            //IdActPrimaria = tramite.IdActividadPrimaria;
                            //IdActSecundaria = tramite.IdActividadSecundaria;
                            //IdActPrimaria = (IdActPrimaria == "0") ? "" : IdActPrimaria;
                            //IdActSecundaria = (IdActSecundaria == "0") ? "" : IdActSecundaria;

                            //if (!String.IsNullOrEmpty(IdActPrimaria) && !tramite.IdRubroPrimario.HasValue)
                            //{
                            //    divMensajeError.Visible = true;
                            //    lblMensajeError.Text = "Debe seleccionar un rubro primario para la actividad " + tramite.ActividadPrimaria;
                            //    break;
                            //}

                            //if (!String.IsNullOrEmpty(IdActSecundaria) && !tramite.IdRubroSecundario.HasValue)
                            //{
                            //    divMensajeError.Visible = true;
                            //    lblMensajeError.Text = "Debe seleccionar un rubro secundario para la actividad " + tramite.ActividadPrimaria;
                            //    break;
                            //}

                        }
                        //Fin control de obligatoriedad de rubro

                        switch (tramite.NombreEstadoActual)
                        {
                            case "VERIFICADO BOCA":
                                if (tramite.NroSifcos == "0" || string.IsNullOrEmpty(tramite.NroSifcos))
                                {
                                    MostrarOcultarModalAsignarNroSifcos(true);
                                }
                                else
                                {
                                    divMensajeError.Visible = true;
                                    lblMensajeError.Text = "Ya tiene asignado el N° SIFCoS : " + tramite.NroSifcos;
                                }
                                break;
                            case "VERIFICADO MINISTERIO":
                                if (tramite.NroSifcos == "0" || string.IsNullOrEmpty(tramite.NroSifcos))
                                {
                                    MostrarOcultarModalAsignarNroSifcos(true);
                                }
                                else
                                {
                                    divMensajeError.Visible = true;
                                    lblMensajeError.Text = "Ya tiene asignado el N° SIFCoS : " + tramite.NroSifcos;
                                }
                                break;
                            case "AUTORIZADO POR MINISTERIO":
                                if (tramite.NroSifcos == "0" || string.IsNullOrEmpty(tramite.NroSifcos))
                                {
                                    MostrarOcultarModalAsignarNroSifcos(true);
                                }
                                else
                                {
                                    divMensajeError.Visible = true;
                                    lblMensajeError.Text = "Ya tiene asignado el N° SIFCoS : " + tramite.NroSifcos;
                                }
                                break;
                            default:
                                divMensajeError.Visible = true;
                                lblMensajeError.Text =
                                    "No se puede asignar N° SIFCoS  porque no esta la documentación verificada o no abono la tasa retributiva";
                                break;

                        }
                    }
                    else
                    {
                        divMensajeError.Visible = true;
                        lblMensajeError.Text = "No se puede asignar N° SIFCoS  el tramite no tiene abonado la tasa retributiva";
                    }
                    break;


            }
        }

        private void ConsultarDomComercio(int entidadSeleccionada)
        {
            //obtengo las URL para setear el domicilio a traves de la API 
            HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
            var Requestwrapper = new HttpRequestWrapper(Request);
            var tramite = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
            if (tramite.Count == 0)
                return;
            InscripcionSifcosDto tramiteDto = tramite[0];
            UrlDomComercio = Helper.getURLDomicilio(sessionBase, Requestwrapper, "SIF" + tramiteDto.CUIT + tramiteDto.NroSifcos);
            idVinComercio = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIF" + tramiteDto.CUIT + tramiteDto.NroSifcos);
            DomComercio = Helper.getDomicilio(sessionBase, Requestwrapper, "SIF" + tramiteDto.CUIT + tramiteDto.NroSifcos);
            //if (idVinComercio == 0)
            //{
            //    divMensajeError.Visible = true;
            //    lblMensajeError.Text = "La geolocalizacion del comercio no esta registrada.";
            //    MostrarOcultardivModalMapa(false);
            //    return;
            //}

        }
        //Actualizacion agosto 2025: se agrega la posibilidad de correccion de domicilio para corregir la geolocalizacion
        protected void btnActualizarDomicilio_OnClick(object sender, EventArgs e)
        {

            var tram = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList()[0];
            tramiteDto.NroTramiteSifcos = tram.NroTramiteSifcos;
            tramiteDto.IdEntidad = tram.IdEntidad;
            tramiteDto.CUIT = tram.CUIT;
            tramiteDto.IdSede = tram.IdSede;
            tramiteDto.IdTipoTramite = tram.IdTipoTramite;
            tramiteDto.NroSifcos = tram.NroSifcos;
            tramiteDto.CuilUsuarioCidi = UsuarioCidiLogueado.CUIL;

            tramiteDto.IdVinDomLocal = idVinComercio;
            tramiteDto.Longitud = DomComercio.Longitud;
            tramiteDto.Latitud = DomComercio.Latitud;

            var Resultado = Bl.BlModificarDomicilioLocaldelTramite(tramiteDto); //OK1

            if (Resultado == "OK1")
            {
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;
                lblMensajeExito.Text = "Se actualizó el domicilio del comercio con éxito.";

            }
            else
            {
                divMensajeExito.Visible = false;
                divMensajeError.Visible = true;
                lblMensajeError.Text = "Error en la actualización del domicilio: " + Resultado;

            }
            MostrarOcultardivModalMapa(false);
        }

        protected void EnviarNotificacionCIDI(String CUIT, String IdEntidad)
        {
            var destinatario = Bl.BlGetDestinatario(CUIT);

            if (string.IsNullOrEmpty(destinatario))
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "El Responsable Legal del tramite no fue cargado.";
                return;
            }
            Session["IdEntidad"] = IdEntidad;

            var Hash = Cifrar(destinatario);

            Response.Redirect("Enviar.aspx?destinatario=" + Hash);
        }

        protected void EnviarNotificacionTRSVencida(String CUIT, String IdEntidad)
        {
            var destinatario = Bl.BlGetDestinatario(CUIT);

            if (string.IsNullOrEmpty(destinatario))
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "El Responsable Legal del tramite no fue cargado.";
                return;
            }
            Session["IdEntidad"] = IdEntidad;

            var Hash = Cifrar(destinatario);

            Response.Redirect("Enviar2.aspx?destinatario=" + Hash);
        }
        public string Cifrar(string pTextoACifrar)
        {
            string key = Key_Cif_Decif;
            byte[] clearBytes = Encoding.Unicode.GetBytes(pTextoACifrar);

            using (Aes encriptadorAES = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encriptadorAES.Key = pdb.GetBytes(32);
                encriptadorAES.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encriptadorAES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    pTextoACifrar = Convert.ToBase64String(ms.ToArray());
                }
            }

            return pTextoACifrar;
        }

        private void ImprimirTasa()
        {
            try
            {
                // CONCEPTO
                //ID : 76010000  | NOMBRE : Art. 76 - Inc. 1 - Tasa retributiva por derecho de inscripcion en SIFCoS (Inscripcion)
                //ID : 76020000  | NOMBRE : Art. 76 - Inc. 2 - Por renovacion anual del derecho de inscripcion en el SIFCoS  (Reempadronamiento)
                var _tramite = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList()[0];
                int IdTipoTramiteTrs = 4;
                string strIdConcepto = SingletonParametroGeneral.GetInstance().IdConceptoTasaReempadronamiento;
                String fecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoReempadronamiento;
                string urlTrs = SingletonParametroGeneral.GetInstance().UrlGeneracionTrs;
                string strConcpeto = "";
                string strMonto = "";

                //String idConcepto = _tramite.IdTipoTramite;
                //Int64 importeConcepto = 250;
                //String fecDesdeConcepto = SingletonParametroGeneral.GetInstance().FecDesdeConceptoAlta;
                switch (_tramite.IdTipoTramite)
                {
                    case "1":
                        IdTipoTramiteTrs = 1;
                        break;
                    case "2":
                        IdTipoTramiteTrs = 4;
                        break;
                    case "4":
                        IdTipoTramiteTrs = 4;
                        break;
                    case "5":
                        IdTipoTramiteTrs = 1;
                        break;
                    case "7":
                        IdTipoTramiteTrs = 1;
                        break;
                    case "9":
                        IdTipoTramiteTrs = 1;
                        break;
                    default:
                        lblMensajeError.Text = "EL TRÁMITE NO DEBE IMPRIMIR LA TASA";
                        divMensajeError.Visible = true;
                        return;

                }

                /*
               var conceptoTasa = new ConceptoTasa { id_concepto = idConcepto, fec_desde = DateTime.Parse(fecDesdeConcepto) };
               string nroTransaccion = null;

               var resultado = Bl.GenerarTransaccionTRS(_tramite.CUIT, master.UsuarioCidiLogueado.Id_Sexo,
                   master.UsuarioCidiLogueado.NroDocumento, "ARG",
                   master.UsuarioCidiLogueado.Id_Numero, Int64.Parse(conceptoTasa.id_concepto), conceptoTasa.fec_desde,
                   "057", 1, importeConcepto, "", DateTime.Now.Year.ToString(), out nroTransaccion);
               */

                string oFechaVenc;
                string oHashTrx;
                string oIdTransaccion;
                string oNroLiqOriginal;


                var resultado = Bl.BlSolicitarTrs(IdTipoTramiteTrs, _tramite.CUIT, master.UsuarioCidiLogueado.CUIL,
                    out oFechaVenc, out oHashTrx, out oIdTransaccion, out oNroLiqOriginal, out strIdConcepto, out fecDesdeConcepto, out strMonto, out strConcpeto);

                if (resultado != "OK")
                {
                    lblMensajeError.Text = resultado;
                    divMensajeError.Visible = true;
                    return;
                }

                if (oIdTransaccion == null)
                {
                    lblMensajeError.Text = "DatoS : CUIT: " + _tramite.CUIT + " - id_sexo:" + master.UsuarioCidiLogueado.Id_Sexo + " - dni_cidi:" +
                    master.UsuarioCidiLogueado.NroDocumento + " - pais:" + "ARG" + " - ID_NRO cidi:" +
                    master.UsuarioCidiLogueado.Id_Numero + " - ID_CONCEPTO :" + strIdConcepto + " - FECHA DESDE: " + fecDesdeConcepto + "  " +
                    "057, 1, 150, , 2017";
                    divMensajeError.Visible = true;
                    return;
                }
                TipoTramiteEnum _tipo = _tramite.IdTipoTramite != "4" ? TipoTramiteEnum.Instripcion_Sifcos : TipoTramiteEnum.Reempadronamiento;
                //Bl.BlActualizarTasas(Int64.Parse(_tramite.NroTramiteSifcos), nroTransaccion, _tipo);//se asigna la nueva transaccion al tramite
                if (_tramite.IdTipoTramite == "1" || _tramite.IdTipoTramite == "2" || _tramite.IdTipoTramite == "4" || _tramite.IdTipoTramite == "5" || _tramite.IdTipoTramite == "7" || _tramite.IdTipoTramite == "9")
                {
                    Response.Redirect(urlTrs + oHashTrx + "/" + oIdTransaccion);
                }
                lblMensajeExito.Text = "IMPRESIÓN DE LA TASA CON ÉXITO";
                divMensajeExito.Visible = true;


            }
            catch (Exception e)
            {
                lblMensajeError.Text = "Ocurrió un Error al Imprimir la TASA. Por favor intentelo más tarde.";
                divMensajeError.Visible = false;
            }
        }

        private void VerTramite(EstadoAbmcEnum estado)
        {
            divPantallaVerTramite.Visible = true;
            CambiarEstado(estado);
            CargarVerTramite();

        }

        private void ImprimirTramite(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
            ImprimirReporteTramite();

        }

        private void CargarDeudaTramite()
        {
            DataTable dtDeudaTramite = Bl.BlGetDeudaTramite(Int64.Parse(EntidadSeleccionada.ToString()));

            if (dtDeudaTramite.Rows.Count != 0)
            {
                divGrillaTasas.Visible = true;
                GVDeuda.DataSource = dtDeudaTramite;
                GVDeuda.DataBind();
                divMensajeExitoInfoDeuda.Visible = true;
                divMensajeErrorInfoDeuda.Visible = false;
                lblMensajeExitoInfoDeuda.Text = "El trámite ha sido abonado con la/las siguiente/s tasa/s. No posee deuda.";

            }
            else
            {
                divGrillaTasas.Visible = false;
                GVDeuda.DataSource = null;
                GVDeuda.DataBind();
                divMensajeExitoInfoDeuda.Visible = false;
                divMensajeErrorInfoDeuda.Visible = true;
                lblMensajeErrorInfoDeuda.Text = "El trámite no posee tasas cargadas.";

            }


        }

        private void EditarTramite(EstadoAbmcEnum estado)
        {
            CambiarEstado(estado);
            CargarTramiteModificar();

            divMensajeErrorModalModificar.Visible = false;
            divMensajeExitoModalModificar.Visible = false;


            divSeccionDomEstab.Visible = false;
            divSeccionDomLegal.Visible = false;
            divSeccionConEstab.Visible = false;
            divSeccionInfoGral.Visible = false;
            divSeccionProdAct.Visible = false;
            divSeccionRepLegal.Visible = false;
        }

        protected void cargarComboEstados_0(string idEstadoTramiteActual) // si el tramite es una alta 
        {
            List<string> listEstados = new List<string>();

            DataTable dt = new DataTable();
            DataTable dtEstados = new DataTable();

            var column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "ID_ESTADO_TRAMITE";
            dtEstados.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "N_ESTADO_TRAMITE";
            dtEstados.Columns.Add(column);

            dt = Bl.BlGetEstados_br();


            /*Cargo el combo con los estados posible a tranferir según el estado actualo*/
            switch (int.Parse(idEstadoTramiteActual))
            {

                case 1://	CARGADO
                    if (IdOrganismoUsuarioLogueado == "1")
                    {

                        //si es ministerio , muestra verificado ministerio(4),REIMRESION VERIFICADA(12) ANULADO(10) y rechazado CON DEV DE TASA(11)
                        listEstados = new List<string> { "4", "10", "11" };
                    }
                    else
                    {
                        //si otra boca que no sea ministerio , VERIFICADO BOCA
                        listEstados = new List<string> { "2" };
                    }

                    break;
                case 2://	VERIFICADO BOCA
                    listEstados = new List<string> { "5" };
                    break;

                case 3://	RECHAZADO BOCA
                    /*No puede pasar a ninun estado*/
                    break;

                case 4://	VERIFICADO MINISTERIO
                    /*lt: modificacion 8/2/2021
					pasa al estado autorizado por ministerio */
                    listEstados = new List<string> { "6" };

                    break;

                case 5: //	RECHAZADO MINISTERIO
                    /*No puede pasar a ninun estado*/
                    break;
                case 6: //	AUTORIZADO POR MINISTERIO
                    /*No puede pasar a ninun estado*/
                    break;
                case 7: //	RECHAZADO SIN TASA PAGA
                    /*No puede pasar a ninun estado*/
                    break;
                case 8: //	ADEUDA
                    /*No puede pasar a ninun estado*/
                    break;
                case 9://	MODIFICADO
                    if (IdOrganismoUsuarioLogueado == "1")
                    {

                        //si es ministerio , muestra verificado ministerio(4),REIMRESION VERIFICADA(12) ANULADO(10) y rechazado CON DEV DE TASA(11)
                        listEstados = new List<string> { "4", "10", "11" };
                    }
                    else
                    {
                        //si otra boca que no sea ministerio , VERIFICADO BOCA
                        listEstados = new List<string> { "2" };
                    }
                    break;
                case 10: //	DADO DE BAJA
                    /*No puede pasar a ninun estado*/
                    break;
                case 11: //	RECHAZADO CON DEV DE TASA
                    /*No puede pasar a ninun estado*/
                    break;
                case 12: //	REIMPRESION VERIFICADA
                    /*No puede pasar a ninun estado*/
                    break;
                case 13: //	REIMPRESION AUTORIZADA    
                    /*No puede pasar a ninun estado*/
                    break;
                case 14: // BAJA VERIFICADA
                    if (IdOrganismoUsuarioLogueado == "1")
                    {
                        //si es ministerio , muestra BAJA AUTORIZADA(15)  Y RECHAZADO MINISTERIO(5)
                        listEstados = new List<string> { "15", "5" };
                    }
                    break;
                case 15: // BAJA AUTORIZADA
                    /*No puede pasar a ninun estado*/
                    break;
            }


            DataRow newRow;
            foreach (DataRow row in dt.Rows)
            {
                foreach (string idEstado in listEstados.ToList())
                {
                    if (row["id_estado_tramite"].ToString() == idEstado)
                    {
                        newRow = dtEstados.NewRow();
                        newRow["ID_ESTADO_TRAMITE"] = row["ID_ESTADO_TRAMITE"];
                        newRow["N_ESTADO_TRAMITE"] = row["N_ESTADO_TRAMITE"];
                        dtEstados.Rows.Add(newRow);
                    }
                }
            }

            dtEstados.DefaultView.Sort = "N_ESTADO_TRAMITE DESC";

            ddlEstados.DataSource = dtEstados;
            ddlEstados.DataTextField = "n_estado_tramite";
            ddlEstados.DataValueField = "id_estado_tramite";
            ddlEstados.DataBind();

        }

        protected void cargarComboEstados_1(string idEstadoTramiteActual) // si el tramite es reimpresion con tasa paga
        {
            List<string> listEstados = new List<string>();

            DataTable dt = new DataTable();
            DataTable dtEstados = new DataTable();

            var column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "ID_ESTADO_TRAMITE";
            dtEstados.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "N_ESTADO_TRAMITE";
            dtEstados.Columns.Add(column);

            dt = Bl.BlGetEstados_br();

            /*Cargo el combo con los estados posible a tranferir según el estado actualo*/
            switch (int.Parse(idEstadoTramiteActual))
            {

                case 1://	CARGADO
                    if (IdOrganismoUsuarioLogueado == "1")
                    {

                        //si es ministerio , muestra REIMRESION VERIFICADA(12) ANULADO(10) y rechazado CON DEV DE TASA(11)
                        listEstados = new List<string> { "12", "10", "11" };
                    }
                    else
                    {
                        //si otra boca que no sea ministerio , VERIFICADO BOCA
                        listEstados = new List<string> { "2" };
                    }
                    break;
                case 2://	VERIFICADO BOCA
                    listEstados = new List<string> { "5" };
                    break;

                case 3://	RECHAZADO BOCA
                    /*No puede pasar a ninun estado*/
                    break;

                case 4://	VERIFICADO MINISTERIO
                    /*lt: modificacion 8/2/2021
					pasa al estado autorizado por ministerio */
                    //listEstados = new List<string> { "6" };
                    break;
                case 5: //	RECHAZADO MINISTERIO
                    /*No puede pasar a ninun estado*/
                    break;
                case 6: //	AUTORIZADO POR MINISTERIO
                    /*No puede pasar a ninun estado*/
                    break;
                case 7: //	RECHAZADO SIN TASA PAGA
                    /*No puede pasar a ninun estado*/
                    break;
                case 8: //	ADEUDA
                    /*No puede pasar a ninun estado*/
                    break;
                case 9://	MODIFICADO
                    /*No puede pasar a ninun estado*/
                    break;
                case 10: //	DADO DE BAJA
                    /*No puede pasar a ninun estado*/
                    break;
                case 11: //	RECHAZADO CON DEV DE TASA
                    /*No puede pasar a ninun estado*/
                    break;
                case 12: //	REIMPRESION VERIFICADA
                    if (IdOrganismoUsuarioLogueado == "1")
                    {
                        //si es ministerio , muestra REIMRESION AUTORIZADA (13) y rechazado ministerio (5)
                        listEstados = new List<string> { "13", "5" };
                    }
                    break;
                case 13: //	REIMPRESION AUTORIZADA    
                    /*No puede pasar a ninun estado*/
                    break;
                case 14: // BAJA VERIFICADA
                    if (IdOrganismoUsuarioLogueado == "1")
                    {
                        //si es ministerio , muestra verificado ministerio y rechazado ministerio
                        listEstados = new List<string> { "15", "5" };
                    }
                    break;
                case 15: // BAJA AUTORIZADA
                    /*No puede pasar a ninun estado*/
                    break;
            }


            DataRow newRow;
            foreach (DataRow row in dt.Rows)
            {
                foreach (string idEstado in listEstados.ToList())
                {
                    if (row["id_estado_tramite"].ToString() == idEstado)
                    {
                        newRow = dtEstados.NewRow();
                        newRow["ID_ESTADO_TRAMITE"] = row["ID_ESTADO_TRAMITE"];
                        newRow["N_ESTADO_TRAMITE"] = row["N_ESTADO_TRAMITE"];
                        dtEstados.Rows.Add(newRow);
                    }
                }
            }

            dtEstados.DefaultView.Sort = "N_ESTADO_TRAMITE DESC";

            ddlEstados.DataSource = dtEstados;
            ddlEstados.DataTextField = "n_estado_tramite";
            ddlEstados.DataValueField = "id_estado_tramite";
            ddlEstados.DataBind();

        }

        protected void cargarComboEstados_2(string idEstadoTramiteActual) //si el tramite es reimpresion sin tasa paga 
        {
            List<string> listEstados = new List<string>();

            DataTable dt = new DataTable();
            DataTable dtEstados = new DataTable();

            var column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "ID_ESTADO_TRAMITE";
            dtEstados.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "N_ESTADO_TRAMITE";
            dtEstados.Columns.Add(column);

            dt = Bl.BlGetEstados_br();


            /*Cargo el combo con los estados posible a tranferir según el estado actualo*/
            switch (int.Parse(idEstadoTramiteActual))
            {

                case 1://	CARGADO
                    if (IdOrganismoUsuarioLogueado == "1")
                    {
                        //si es ministerio , muestra REIMPRESION VERIFICADA
                        listEstados = new List<string> { "12" };
                    }
                    else
                    {
                        //si otra boca que no sea ministerio , IDEM
                        listEstados = new List<string> { "12" };
                    }

                    break;
                case 2://	VERIFICADO BOCA
                    /*No puede pasar a ninun estado*/
                    break;

                case 3://	RECHAZADO BOCA
                    /*No puede pasar a ninun estado*/
                    break;

                case 4://	VERIFICADO MINISTERIO
                    /*lt: modificacion 8/2/2021
					pasa al estado aprobado por ministerio */
                    listEstados = new List<string> { "6" };
                    break;

                case 5: //	RECHAZADO MINISTERIO
                    /*No puede pasar a ninun estado*/
                    break;
                case 6: //	AUTORIZADO POR MINISTERIO
                    /*No puede pasar a ninun estado*/
                    break;
                case 7: //	RECHAZADO SIN TASA PAGA
                    /*No puede pasar a ninun estado*/
                    break;
                case 8: //	ADEUDA
                    /*No puede pasar a ninun estado*/
                    break;
                case 9://	MODIFICADO
                    /*No puede pasar a ninun estado*/
                    break;
                case 10: //	DADO DE BAJAv
                    /*No puede pasar a ninun estado*/
                    break;
                case 11: //	RECHAZADO CON DEV DE TASA
                    /*No puede pasar a ninun estado*/
                    break;
                case 12: //	REIMPRESION VERIFICADA
                    if (IdOrganismoUsuarioLogueado == "1")
                    {
                        //si es ministerio , muestra REIMRESION AUTORIZADA (13) y rechazado ministerio (5)
                        listEstados = new List<string> { "13" };
                    }
                    break;
                case 13: //	REIMPRESION AUTORIZADA    

                    break;
            }


            DataRow newRow;
            foreach (DataRow row in dt.Rows)
            {
                foreach (string idEstado in listEstados.ToList())
                {
                    if (row["id_estado_tramite"].ToString() == idEstado)
                    {
                        newRow = dtEstados.NewRow();
                        newRow["ID_ESTADO_TRAMITE"] = row["ID_ESTADO_TRAMITE"];
                        newRow["N_ESTADO_TRAMITE"] = row["N_ESTADO_TRAMITE"];
                        dtEstados.Rows.Add(newRow);
                    }
                }
            }

            dtEstados.DefaultView.Sort = "N_ESTADO_TRAMITE DESC";

            ddlEstados.DataSource = dtEstados;
            ddlEstados.DataTextField = "n_estado_tramite";
            ddlEstados.DataValueField = "id_estado_tramite";
            ddlEstados.DataBind();

        }

        protected void cargarComboEstados_3(string idEstadoTramiteActual) //si el tramite es baja
        {
            List<string> listEstados = new List<string>();

            DataTable dt = new DataTable();
            DataTable dtEstados = new DataTable();

            var column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "ID_ESTADO_TRAMITE";
            dtEstados.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "N_ESTADO_TRAMITE";
            dtEstados.Columns.Add(column);

            dt = Bl.BlGetEstados_br();




            /*Cargo el combo con los estados posible a tranferir según el estado actualo*/
            switch (int.Parse(idEstadoTramiteActual))
            {

                case 1://	CARGADO
                    if (IdOrganismoUsuarioLogueado == "1")
                    {
                        //si es ministerio , muestra BAJA VERIFICADA
                        listEstados = new List<string> { "14" };
                    }
                    else
                    {
                        //si otra boca que no sea ministerio , IDEM
                        listEstados = new List<string> { "14" };
                    }

                    break;
                case 2://	VERIFICADO BOCA
                    /*No puede pasar a ninun estado*/
                    break;

                case 3://	RECHAZADO BOCA
                    /*No puede pasar a ninun estado*/
                    break;

                case 4://	VERIFICADO MINISTERIO
                    /*No puede pasar a ninun estado*/
                    break;

                case 5: //	RECHAZADO MINISTERIO
                    /*No puede pasar a ninun estado*/
                    break;
                case 6: //	AUTORIZADO POR MINISTERIO
                    /*No puede pasar a ninun estado*/
                    break;
                case 7: //	RECHAZADO SIN TASA PAGA
                    /*No puede pasar a ninun estado*/
                    break;
                case 8: //	ADEUDA
                    /*No puede pasar a ninun estado*/
                    break;
                case 9://	MODIFICADO
                    /*No puede pasar a ninun estado*/
                    break;
                case 10: //	DADO DE BAJA
                    /*No puede pasar a ninun estado*/
                    break;
                case 11: //	RECHAZADO CON DEV DE TASA
                    /*No puede pasar a ninun estado*/
                    break;
                case 12: //	REIMPRESION VERIFICADA
                    listEstados = new List<string> { "13" };
                    break;
                case 13: //	REIMPRESION AUTORIZADA    
                    /*No puede pasar a ninun estado*/
                    break;
                case 14: // BAJA VERIFICADA PUEDE PASAR A AUTORIZADA
                    listEstados = new List<string> { "15" };
                    break;
            }


            DataRow newRow;
            foreach (DataRow row in dt.Rows)
            {
                foreach (string idEstado in listEstados.ToList())
                {
                    if (row["id_estado_tramite"].ToString() == idEstado)
                    {
                        newRow = dtEstados.NewRow();
                        newRow["ID_ESTADO_TRAMITE"] = row["ID_ESTADO_TRAMITE"];
                        newRow["N_ESTADO_TRAMITE"] = row["N_ESTADO_TRAMITE"];
                        dtEstados.Rows.Add(newRow);
                    }
                }
            }

            dtEstados.DefaultView.Sort = "N_ESTADO_TRAMITE DESC";

            ddlEstados.DataSource = dtEstados;
            ddlEstados.DataTextField = "n_estado_tramite";
            ddlEstados.DataValueField = "id_estado_tramite";
            ddlEstados.DataBind();

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
            //contacto
            txtEmailMd.Text = "";
            txtFacebookMd.Text = "";
            txtWebPageMd.Text = "";
            txtCelularMd.Text = "";
            txtTelFijoMd.Text = "";
            txtCodAreaCelularMd.Text = "";
            txtCodAreaTelFijoMd.Text = "";
            //domicilio establecimiento
            //txtCalle.Text = "";
            //txtNroCalle.Text = "";
            //txtCodPos.Text = "";
            //txtPiso.Text = "";
            //txtNroDepto.Text = "";
            //txtOficina.Text = "";
            //txtStand.Text = "";
            //txtLocal.Text = "";
            ////domicilio legal
            //txtCalleLegal.Text = "";
            //txtNroCalleLegal.Text = "";
            //txtCodPosLegal.Text = "";
            //txtPisoLegal.Text = "";
            //txtNroDptoLegal.Text = "";
            //info general
            txtNombreFantasia.Text = "";
            txtFechaIniAct.Text = "";
            txtNroHabMun.Text = "";
            txtNroDGR.Text = "";
            txtM2Venta.Text = "";
            txtM2Admin.Text = "";
            txtM2Dep.Text = "";
            txtCantPersRel.Text = "";
            txtCantPersTotal.Text = "";
            rbInquilino.Checked = false;
            rbPropietario.Checked = false;
            rb1015.Checked = false;
            rb1520.Checked = false;
            rb5.Checked = false;
            rb510.Checked = false;
            rbNoCap.Checked = false;
            rbSiCap.Checked = false;
            rbNoCobertura.Checked = false;
            rbSiCobertura.Checked = false;
            rbNoSeg.Checked = false;
            rbSiSeg.Checked = false;
            chkprov.Checked = false;
            ChkNacional.Checked = false;
            ChkInter.Checked = false;
            //representante legal
            txtNombreRepresentante.Text = "";
            txtApellidoRepresentante.Text = "";
            txtSexoRepresentante.Text = "";
        }

        private void HabilitarDesHabilitarCampos(bool valor)
        {
            txtRazonSocial.Enabled = valor;
            txtCuit.Enabled = valor;
            txtNroSifcos.Enabled = valor;
            txtNroTramite.Enabled = valor;
        }

        private void MostrarOcultarDiv(bool valor)
        {
            divMensajeError.Visible = valor;
            divMensajeExito.Visible = valor;
            divMensajeErrorBaja.Visible = valor;
            divMensajeExitoBaja.Visible = valor;
            divMensaejeErrorModal.Visible = valor;

        }

        protected void btnImprimir_OnClick(object sender, EventArgs e)
        {
            ImprimirReporteTramite();
        }

        private void ImprimirReporteTramite()
        {
            var lista = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
            if (lista.Count == 0)
                return;
            InscripcionSifcosDto tramiteDto = lista[0];


            var lis = new List<Producto>();
            DataTable dtProductosTramite = Bl.BlGetProductosTramite(tramiteDto.NroTramiteSifcos);
            foreach (DataRow row in dtProductosTramite.Rows)
            {
                lis.Add(new Producto { IdProducto = row["idproducto"].ToString(), NProducto = row["nproducto"].ToString() });
            }

            DataTable dtContacto = Bl.BlGetComEmpresa(EntidadSeleccionada.ToString());
            if (dtContacto.Rows.Count > 0)
            {
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
                            tramiteDto.EmailEstablecimiento = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;
                        case "12": //PAGINA WEB
                            tramiteDto.WebPage = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;
                        case "17": //FACEBOOK
                            tramiteDto.Facebook = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;

                    }
                }

            }

            tramiteDto.CelularGestor = UsuarioCidiGestor != null ? UsuarioCidiGestor.CelFormateado : " - ";
            tramiteDto.CelularRepLegal = UsuarioCidiRep != null ? UsuarioCidiRep.CelFormateado : " - ";
            /*
            var domicilio1 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLocal.Value.ToString());
            var domicilio2 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLegal.Value.ToString());
            */
            var domicilio1 = new AppComunicacion.ApiModels.Domicilio();
            if (tramiteDto.IdVinDomLocal.HasValue)
            {
                domicilio1 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLocal.Value.ToString());
            }

            var domicilio2 = new AppComunicacion.ApiModels.Domicilio();
            if (tramiteDto.IdVinDomLegal.HasValue)
            {
                domicilio2 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLegal.Value.ToString());
            }




            var dtSuperficie = Bl.BlGetSuperficeByNroTramite(Convert.ToInt64(tramiteDto.NroTramiteSifcos));

            foreach (DataRow row in dtSuperficie.Rows)
            {
                switch (row["id_tipo_superficie"].ToString())
                {
                    case "1": //SUP. ADMINISTRACION 
                        tramiteDto.SupAdministracion = string.IsNullOrEmpty(row["superficie"].ToString()) ? 0 : long.Parse(row["superficie"].ToString());
                        break;
                    case "2"://SUP. VENTAS
                        tramiteDto.SupVentas = string.IsNullOrEmpty(row["superficie"].ToString()) ? 0 : long.Parse(row["superficie"].ToString());
                        break;
                    case "3"://SUP. DEPOSITO
                        tramiteDto.SupDeposito = string.IsNullOrEmpty(row["superficie"].ToString()) ? 0 : long.Parse(row["superficie"].ToString());
                        break;
                }
            }


            var nombreReporteRdlc = "";

            var tituloReporte = tramiteDto.NombreTipoTramite.ToUpper();

            switch (tramiteDto.IdTipoTramite)
            {
                case "1":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSAlta.rdlc";
                    break;
                case "2":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSBaja.rdlc";
                    break;
                case "4":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReempadronamiento.rdlc";
                    break;
                case "5":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresion.rdlc";
                    tituloReporte = "REIMPRESION DE OBLEA";
                    break;
                case "6":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresionSinTRS.rdlc";
                    tituloReporte = "REIMPRESION DE OBLEA";
                    break;
                case "7":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresion.rdlc";
                    tituloReporte = "REIMPRESION DE CERTIFICADO";
                    break;
                case "8":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresionSinTRS.rdlc";
                    tituloReporte = "REIMPRESION DE CERTIFICADO";
                    break;
                case "9":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresion.rdlc";
                    tituloReporte = "REIMPRESION DE OBLEA Y CERTIFICADO";
                    break;
                case "10":
                    nombreReporteRdlc = "SIFCOS.rptTramiteSIFCoSReimpresionSinTRS.rdlc";
                    tituloReporte = "REIMPRESION DE OBLEA Y CERTIFICADO";
                    break;
                default:
                    tituloReporte = tramiteDto.NombreTipoTramite.ToUpper();
                    break;
            }

            var reporte = new ReporteGeneral(nombreReporteRdlc, lis, TipoArchivoEnum.Pdf);
            reporte.AddParameter("parametro_Titulo_reporte", "Comprobante de Trámite - " + tituloReporte);
            if (nombreReporteRdlc == "SIFCOS.rptTramiteSIFCoSBaja.rdlc")
            {
                reporte.AddParameter("parametro_fecha_baja", tramiteDto.FecVencimiento.ToString());
            }
            else
            {
                reporte.AddParameter("parametro_fecha_vencimiento", tramiteDto.FecVencimiento.Day + "/" + tramiteDto.FecVencimiento.Month + "/" + tramiteDto.FecVencimiento.Year);
            }

            reporte.AddParameter("nroTramiteSifcos", tramiteDto.NroTramiteSifcos);
            reporte.AddParameter("paramatro_dom1_departamento", domicilio1.Departamento != null ? domicilio1.Departamento.Nombre : " - ");
            reporte.AddParameter("paramatro_dom1_localidad", domicilio1.Localidad != null ? domicilio1.Localidad.Nombre : " - ");
            reporte.AddParameter("paramatro_dom1_barrio", domicilio1.Barrio != null ? domicilio1.Barrio.Nombre : " - ");
            reporte.AddParameter("paramatro_dom1_calle", domicilio1.Calle!=null?domicilio1.Calle.Nombre:" - ");
            reporte.AddParameter("paramatro_dom1_nroCalle", domicilio1.Altura);
            reporte.AddParameter("paramatro_dom1_dpto", domicilio1.Dpto);
            reporte.AddParameter("paramatro_dom1_piso", domicilio1.Piso);
            reporte.AddParameter("paramatro_dom1_codPos", domicilio1.CodigoPostal);
            reporte.AddParameter("paramatro_dom1_oficina", tramiteDto.Oficina);
            reporte.AddParameter("paramatro_dom1_local", tramiteDto.Local);
            reporte.AddParameter("paramatro_dom1_stand", tramiteDto.Stand);
            reporte.AddParameter("paramatro_contacto_email", !string.IsNullOrEmpty(tramiteDto.EmailEstablecimiento) ? tramiteDto.EmailEstablecimiento : " - ");
            reporte.AddParameter("paramatro_contacto_facebook", !string.IsNullOrEmpty(tramiteDto.Facebook) ? tramiteDto.Facebook : " - ");
            reporte.AddParameter("paramatro_contacto_WebPage", !string.IsNullOrEmpty(tramiteDto.WebPage) ? tramiteDto.WebPage : " - ");
            reporte.AddParameter("paramatro_contacto_celular", !string.IsNullOrEmpty(tramiteDto.Celular) ? "(" + tramiteDto.CodAreaCelular + ") " + tramiteDto.Celular : " - ");
            reporte.AddParameter("paramatro_contacto_telFijo", !string.IsNullOrEmpty(tramiteDto.TelFijo) ? "(" + tramiteDto.CodAreaTelFijo + ") " + tramiteDto.TelFijo : " - ");
            reporte.AddParameter("paramatro_dom2_departamento", domicilio2.Departamento != null ? domicilio2.Departamento.Nombre : " - ");
            reporte.AddParameter("paramatro_dom2_localidad", domicilio2.Localidad != null ? domicilio2.Localidad.Nombre : " - ");
            reporte.AddParameter("paramatro_dom2_barrio", domicilio2.Barrio != null ? domicilio2.Barrio.Nombre: " - ");
            reporte.AddParameter("paramatro_dom2_calle", domicilio2.Calle!=null? domicilio2.Calle.Nombre:" - ");
            reporte.AddParameter("paramatro_dom2_nroCalle", domicilio2.Altura);
            reporte.AddParameter("paramatro_dom2_dpto", domicilio2.Dpto);
            reporte.AddParameter("paramatro_dom2_piso", domicilio2.Piso);
            reporte.AddParameter("paramatro_dom2_codPos", domicilio2.CodigoPostal);
            reporte.AddParameter("paramatro_dom2_oficina", tramiteDto.Stand);
            reporte.AddParameter("paramatro_dom2_local", tramiteDto.Local);
            reporte.AddParameter("paramatro_dom2_stand", tramiteDto.Oficina);
            reporte.AddParameter("paramatro_fecInicioAct", tramiteDto.FecIniTramite.ToShortDateString());
            reporte.AddParameter("paramatro_nroHabMunicipal", tramiteDto.NroHabMunicipal);
            reporte.AddParameter("paramatro_nroDGR", tramiteDto.NroDGR);
            reporte.AddParameter("paramatro_supVenta", tramiteDto.SupVentas != null ? tramiteDto.SupVentas.Value.ToString() : "-");
            reporte.AddParameter("paramatro_supDeposito", tramiteDto.SupDeposito != null ? tramiteDto.SupDeposito.Value.ToString() : "-");
            reporte.AddParameter("paramatro_supAdm", tramiteDto.SupAdministracion != null ? tramiteDto.SupAdministracion.Value.ToString() : "-");
            reporte.AddParameter("paramatro_cantPersTotal", tramiteDto.CantTotalpers);
            reporte.AddParameter("paramatro_cantPersRelacionDep", tramiteDto.CantPersRelDep);
            reporte.AddParameter("paramatro_poseeCobertura", tramiteDto.Cobertura != "" ? tramiteDto.Cobertura : "N");
            reporte.AddParameter("paramatro_realizoCapacitacion", tramiteDto.CapacUltAnio != "" ? tramiteDto.CapacUltAnio : "N");
            reporte.AddParameter("paramatro_poseeSeguro", tramiteDto.Seguro != "" ? tramiteDto.Seguro : "N");
            reporte.AddParameter("paramatro_origenProveedor", tramiteDto.NombreOrigenProveedor);
            reporte.AddParameter("parametro_empresa_cuit", tramiteDto.CUIT);
            reporte.AddParameter("parametro_empresa_razonSocial", tramiteDto.RazonSocial);
            reporte.AddParameter("parametro_empresa_AnioOperativo", DateTime.Now.Year.ToString());
            reporte.AddParameter("parametro_actividadPrimaria", tramiteDto.ActividadPrimaria);
            reporte.AddParameter("parametro_actividadSecundaria", tramiteDto.ActividadSecundaria);
            reporte.AddParameter("parametro_estado_tramite", tramiteDto.NombreEstadoActual);

            //cargo los datos del gestor y responsable
            reporte.AddParameter("parametro_gestor_nombre", tramiteDto.NombreYApellidoGestor);
            reporte.AddParameter("parametro_gestor_dni", tramiteDto.CuilUsuarioCidi);
            reporte.AddParameter("parametro_gestor_telefono", tramiteDto.CelularGestor);
            reporte.AddParameter("parametro_gestor_tipo", tramiteDto.NombreTipoGestor);
            reporte.AddParameter("parametro_responsable_nombre", tramiteDto.NombreYApellidoRepLegal);
            reporte.AddParameter("parametro_responsable_dni", tramiteDto.DniRepLegal);
            reporte.AddParameter("parametro_responsable_telefono", tramiteDto.CelularRepLegal);
            reporte.AddParameter("parametro_responsable_cargo", tramiteDto.NombreCargoRepLegal);
            reporte.AddParameter("parametro_rango_alq", tramiteDto.RangoAlquiler);
            var inmueble = tramiteDto.Propietario == "S"
               ? "Propietario"
               : "Inquilino";
            reporte.AddParameter("parametro_propietario", inmueble);
            reporte.AddParameter("parametro_nombre_fantasia", tramiteDto.NombreFantasia);

            // Guardo datos del reporte en sessión
            Session["ReporteGeneral"] = reporte;

            //LEVANTA LA PAGINA RerporteGeneral
            Response.Redirect("ReporteGeneral.aspx");
        }

        public AppComunicacion.ApiModels.Domicilio consultarDomicilioByIdVin(string idVinculacion)
        {
            var Domicilio = Bl.BlGetDomEmpresaByIdVin(idVinculacion);
            
            return Domicilio;
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

        public DataTable DtActividades
        {
            get
            {
                return Session["ACTIVIDADES"] == null ? new DataTable() : (DataTable)Session["ACTIVIDADES"];
            }
            set
            {
                Session["ACTIVIDADES"] = value;
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

        private void MostrarOcultarModalCambiarEstadoTramite(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarCambiarEstado";
                string[] listaStrings = modalCambiarEstadoTramite.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalCambiarEstadoTramite.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalCambiarEstadoTramite.Attributes.Add("class", String.Join(" ", modalCambiarEstadoTramite
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarCambiarEstado" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultarModalAsignarNroSifcos(bool mostrar)
        {
            divExitoAsignarNroSifcos.Visible = false;
            divErrorAsignarNroSifcos.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarAsignarNroSifcos";
                string[] listaStrings = modalAsignarNroSifcos.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalAsignarNroSifcos.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalAsignarNroSifcos.Attributes.Add("class", String.Join(" ", modalAsignarNroSifcos
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarAsignarNroSifcos" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultarModalInfoDeudaTramite(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarInfoDeudaTramite";
                string[] listaStrings = ModalInfoDeudaTramite.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                ModalInfoDeudaTramite.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                ModalInfoDeudaTramite.Attributes.Add("class", String.Join(" ", ModalInfoDeudaTramite
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarInfoDeudaTramite" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultarModalModificarTramite(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarModificarTramite";
                string[] listaStrings = modalModificarTramite.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                modalModificarTramite.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                modalModificarTramite.Attributes.Add("class", String.Join(" ", modalModificarTramite
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarModificarTramite" })
                          .ToArray()
                  ));
            }
        }

        private void MostrarOcultardivModalMapa(bool mostrar)
        {
            divMensaejeErrorModal.Visible = false;
            if (mostrar)
            {
                var classname = "mostrarModal";
                string[] listaStrings = divModalMostrarMapa.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                    );
                divModalMostrarMapa.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                divModalMostrarMapa.Attributes.Add("class", String.Join(" ", divModalMostrarMapa
                          .Attributes["class"]
                          .Split(' ')
                          .Except(new string[] { "", "mostrarModal" })
                          .ToArray()
                  ));
            }
        }

        protected void btnCancelarEstado_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalCambiarEstadoTramite(false);
        }

        protected void btnVolverVer_OnClick(object sender, EventArgs e)
        {
            divPantallaVerTramite.Visible = false;
            divPantallaResultado.Visible = true;
            divPantallaConsulta.Visible = true;
        }

        protected void btnVolverConsultaTramite_OnClick(object sender, EventArgs e)
        {
            divPantallaVerTramite.Visible = false;
            divPantallaResultado.Visible = true;
        }

        protected void btnGuardarEstado_OnClick(object sender, EventArgs e)
        {
            //9	MODIFICADO
            //6	AUTORIZADO POR MINISTERIO
            //7	BAJA DEFINITIVA
            //8	ADEUDA
            if (ddlEstados.SelectedValue == "0")
            {
                divMensaejeErrorModal.Visible = true;
                lblMensajeErrorModal.Text =
                    "Debe seleccionar un Estado. Si no aparece algún estado posible es porque el estado actual del trámite seleccionado no le permite cambiar de estado.";
                return;
            }
            var _tramite = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList()[0];
            if (_tramite.IdTipoTramite == "2" || _tramite.IdTipoTramite == "6" || _tramite.IdTipoTramite == "8" || _tramite.IdTipoTramite == "10") //si el tramite es baja guardar estado sin asignar TASA igual si el tramite es reimpresion de certificado sin tasa paga
            {
                guardarEstado2(); // SE ACTUALIZA EL ESTADO SIN AGREGAR UNA TASA PAGA DISPONIBLE
                return;
            }
            //- MODIFICACION 8/2/21 lt: 
            if ((_tramite.IdTipoTramite == "5" || _tramite.IdTipoTramite == "7" || _tramite.IdTipoTramite == "9") && (ddlEstados.SelectedItem.Text == "REIMPRESION VERIFICADA" || ddlEstados.SelectedItem.Text == "REIMPRESION AUTORIZADA"))
            {
                guardarEstado(); // SE GUARDA ESTADO Y SE VERIFICA QUE TENGA UNA TASA DISPONIBLE SIN USAR PARA ASIGNAR AL TRAMITE
                return;
            }

            if (ddlListaTRS.SelectedValue == "NO POSEE TASAS PAGAS SIN USAR" && ddlEstados.SelectedItem.Text != "ANULADO")
            {
                divMensaejeErrorModal.Visible = true;
                lblMensajeErrorModal.Text = "Debe seleccionar una Tasa Paga Disponible.";
                return;
            }
            switch (ddlEstados.SelectedItem.Text)
            {
                case "VERIFICADO BOCA":
                    // if (!verificarDeuda())
                    guardarEstado();
                    break;
                case "VERIFICADO MINISTERIO":
                    //  if (!verificarDeuda())
                    guardarEstado();
                    break;
                //Autor: (IB)
                case "AUTORIZADO POR MINISTERIO":
                    //COntrolar que haya seleccionado un rubro para cada actividad 

                    guardarEstado();
                    break;

                default:
                    guardarEstado();
                    break;
            }

            RefrescarGrilla();
        }

        protected void guardarEstado()//guardar estado con asignacion de TASA DISPONIBLE
        {

            if ((ddlListaTRS.SelectedValue == null || ddlListaTRS.SelectedValue == "NO POSEE TASAS PAGAS SIN USAR") && ddlEstados.SelectedItem.Text != "ANULADO")
            {
                lblMensajeError.Text = "No Existen Tasas Disponibles pagadas para el comercio.";
                divMensajeError.Visible = true;
                txtModalDescripcionEstado.Text = "";
                ddlEstados.Items.Clear();
                RefrescarGrilla();
                MostrarOcultarModalCambiarEstadoTramite(false);
                return;

            }
            var lista = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
            if (lista.Count == 0)
                return;
            InscripcionSifcosDto tramiteDto = lista[0];
            // int verificado = Bl.BlGetCantTasasPagadas(tramiteDto.CUIT);


            TipoTramiteEnum _tipo = tramiteDto.IdTipoTramite != "4" ? TipoTramiteEnum.Instripcion_Sifcos : TipoTramiteEnum.Reempadronamiento;

            if (_tipo == TipoTramiteEnum.Instripcion_Sifcos)
            {
                //1 - Actualizo la tasa a PAGADA en  T_SIF_TASAS , asociada al tramite.
                bool actualizartasatramite = Bl.BlActualizarTasas(EntidadSeleccionada, ddlListaTRS.SelectedValue, "S");

                //2 - Asigno el ir_organismo_alta al tramite.
                if (string.IsNullOrEmpty(IdOrganismoUsuarioLogueado))
                {
                    IdOrganismoUsuarioLogueado = "1";//seteo por defecto el ministerio.
                }
                string r = Bl.BlActualizarOrganismoAlta(EntidadSeleccionada, Convert.ToInt32(IdOrganismoUsuarioLogueado));
            }

            if (tramiteDto.IdTipoTramite == "5" || tramiteDto.IdTipoTramite == "7" || tramiteDto.IdTipoTramite == "9") // reimpresion de oblea/certificado con TASA PAGA
            {
                //1 - Actualizo la tasa a PAGADA en  T_SIF_TASAS , asociada al tramite.
                bool actualizartasatramite = Bl.BlActualizarTasas(EntidadSeleccionada, ddlListaTRS.SelectedValue, "S");

                //2 - Asigno el ir_organismo_alta al tramite.
                if (string.IsNullOrEmpty(IdOrganismoUsuarioLogueado))
                {
                    IdOrganismoUsuarioLogueado = "1";//seteo por defecto el ministerio.
                }
                string r = Bl.BlActualizarOrganismoAlta(EntidadSeleccionada, Convert.ToInt32(IdOrganismoUsuarioLogueado));

            }


            bool Resultado = Bl.BlRegistrarEstado(EntidadSeleccionada, Int64.Parse(ddlEstados.SelectedValue.ToString()), txtModalDescripcionEstado.Text, UsuarioCidiLogueado.CUIL);

            MostrarOcultarModalCambiarEstadoTramite(false);
            if (Resultado)
            {
                lblMensajeExito.Text = "Se registro el cambio de Estado con Exito.";
                divMensajeExito.Visible = true;
                txtModalDescripcionEstado.Text = "";
                ddlEstados.Items.Clear();

                RefrescarGrilla();
            }
            else
            {
                lblMensajeError.Text = "No se puede realizar el cambio de estado porque la entidad no posee tasas pagas disponibles.";
                divMensajeError.Visible = true;
                MostrarOcultarModalCambiarEstadoTramite(false);
            }



        }
        protected void guardarEstado2()// guardar estado sin asignar TRS
        {
            bool Resultado = Bl.BlRegistrarEstado(EntidadSeleccionada, Int64.Parse(ddlEstados.SelectedValue.ToString()), txtModalDescripcionEstado.Text, UsuarioCidiLogueado.CUIL);

            MostrarOcultarModalCambiarEstadoTramite(false);
            if (Resultado)
            {
                lblMensajeExito.Text = "Se registro el cambio de Estado con Exito.";
                divMensajeExito.Visible = true;
                txtModalDescripcionEstado.Text = "";
                ddlEstados.Items.Clear();

                RefrescarGrilla();
            }
            else
            {
                lblMensajeError.Text = "No se puede realizar el cambio de estado.";
                divMensajeError.Visible = true;
                MostrarOcultarModalCambiarEstadoTramite(false);
            }



        }

        /// <summary>
        /// Devuelve TRUE si debe tasas y FALSE si está al día.
        /// </summary>
        /// <returns></returns>
        protected bool verificarDeuda()
        {
            DataTable dtDeudaTramite = Bl.BlGetDeudaTramite(EntidadSeleccionada);

            //si el tramite tiene tasa pagada asignada, entonces no adeuda.

            var tieneTasaAsignada = dtDeudaTramite.Rows.Count == 0;
            if (tieneTasaAsignada)
            {
                divMensajeExitoBaja.Visible = false;
                divMensajeErrorBaja.Visible = true;
                divExitoAsignarNroSifcos.Visible = false;
                divErrorAsignarNroSifcos.Visible = true;
                lblMensajeErrorBaja.Text = "El trámite tiene deuda y no puede ser dado de baja hasta que no abone sus tasas.";
                lblDivMensajeError1.Text = "El trámite tiene deuda y no puede ser dado de baja hasta que no abone sus tasas.";
                return true;
            }
            return false;

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

        private void CargarVerTramite()
        {
            var lista = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
            if (lista.Count == 0)
                return;
            InscripcionSifcosDto tramiteDto = lista[0];
            AppComunicacion.ApiModels.Domicilio domicilio1 = new AppComunicacion.ApiModels.Domicilio();
            lblDomProvincia.Text = "S/D";
            lblDomDepartamento.Text = "S/D";
            lblDomLocalidad.Text = "S/D";
            lblDomBarrio.Text = "S/D";
            lblDomCalle.Text = "S/D";
            lblDomNro.Text = "S/D";
            lblDomDepto.Text = "S/D";
            lblDomPiso.Text = "S/D";
            lblDomCodPostal.Text = "S/D";
            if (tramiteDto.IdVinDomLocal != null)
            {
                domicilio1 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLocal.Value.ToString());
                if (domicilio1.Provincia != null)
                {
                    lblDomProvincia.Text = domicilio1.Provincia.Nombre;
                }

                if (domicilio1.Departamento != null)
                {
                    lblDomDepartamento.Text = domicilio1.Departamento.Nombre;
                }

                if (domicilio1.Localidad != null)
                {
                    lblDomLocalidad.Text = domicilio1.Localidad.Nombre;
                }

                if (domicilio1.Barrio != null)
                {
                    lblDomBarrio.Text = domicilio1.Barrio.Nombre;
                }

                if (domicilio1.Calle != null)
                {
                    lblDomCalle.Text = domicilio1.Calle.Nombre;
                }

                lblDomNro.Text = domicilio1.Altura;
                lblDomDepto.Text = domicilio1.Dpto;
                lblDomPiso.Text = domicilio1.Piso;
                lblDomCodPostal.Text = domicilio1.CodigoPostal;
            }
            






            DataTable dtInformeTramite = Bl.BlGetInfoGralTramite(EntidadSeleccionada);

            if (dtInformeTramite.Rows.Count > 0)
            {
                lblApeYNomRL.Text = dtInformeTramite.Rows[0]["rep_legal"].ToString();
                lblDniRL.Text = dtInformeTramite.Rows[0]["dni_rep_legal"].ToString();
                lblApeYNomGestor.Text = dtInformeTramite.Rows[0]["gestor"].ToString();
                lblDniGestor.Text = dtInformeTramite.Rows[0]["dni_gestor"].ToString();
                lblActividadPrimaria.Text = dtInformeTramite.Rows[0]["actividad_pri"].ToString();
                lblActividadSecundaria.Text = dtInformeTramite.Rows[0]["actividad_sec"].ToString();
                lblNroTramite.Text = EntidadSeleccionada.ToString();
                lblFecInicioActividad.Text = dtInformeTramite.Rows[0]["fec_inicio_act"].ToString();
                lblFechaAltaTramite.Text = dtInformeTramite.Rows[0]["fec_alta"].ToString();
                lblNroHabilitacionMunicipal.Text = dtInformeTramite.Rows[0]["nro_hab_municipal"].ToString();
                lblNroDGR.Text = dtInformeTramite.Rows[0]["nro_ingbruto"].ToString();
                var nroSifcos = dtInformeTramite.Rows[0]["nro_sifcos"].ToString();
                if (nroSifcos.Substring(0, 1) == "0")
                {
                    nroSifcos = "NO ASIGNADO";

                }
                lblTituloEmpresa.Text = "RAZON SOCIAL: " + dtInformeTramite.Rows[0]["razon_social"] + " | CUIT: " + dtInformeTramite.Rows[0]["cuit"] + " | NRO. SIFCOS: " + nroSifcos;
                var CuilRepLegal = Bl.BlGetDestinatario(dtInformeTramite.Rows[0]["cuit"].ToString());
                var CuilGestor = Bl.BlGetCuilGestor(dtInformeTramite.Rows[0]["cuit"].ToString());
                Usuario Gestor = ObtenerUsuarioCuil(CuilGestor);
                if (Gestor != null)
                {
                    lblEmailGestor.Text = Gestor.Email;
                }
                Usuario RepLegal = ObtenerUsuarioCuil(CuilRepLegal);
                if (RepLegal != null)
                {
                    lblEmailRepLegal.Text = RepLegal.Email;
                }
            }




            DtProductosTramite = Bl.BlGetProductosTramite(EntidadSeleccionada.ToString());

            gvProductos.DataSource = DtProductosTramite;
            gvProductos.DataBind();

            DataTable dtHR = Bl.BlGetHistEstados(EntidadSeleccionada);

            DataTable dtContacto = Bl.BlGetComEmpresa(EntidadSeleccionada.ToString());
            if (dtContacto.Rows.Count > 0)
            {
                foreach (DataRow row in dtContacto.Rows)
                {
                    switch (row["ID_TIPO_COMUNICACION"].ToString())
                    {
                        case "01": //TELEFONO FIJO
                            lblTelFijo.Text = row["COD_AREA"].ToString() + " " + row["NRO_MAIL"].ToString();
                            break;
                        case "07": //CELULAR
                            lblCelular.Text = row["COD_AREA"].ToString() + " " + row["NRO_MAIL"].ToString();
                            break;
                        case "11": //EMAIL
                            lblEmailC.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;
                        case "12": //PAGINA WEB
                            lblWebPage.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;
                        case "17": //FACEBOOK
                            lblFacebook.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString()) ? row["NRO_MAIL"].ToString() : " - ";
                            break;

                    }
                }

            }
            else
            {
                lblEmailC.Text = " - ";
                lblCelular.Text = " - ";
                lblTelFijo.Text = " - ";
                lblWebPage.Text = " - ";
                lblFacebook.Text = " - ";
            }




            Usuario UsuHR;

            foreach (DataRow row in dtHR.Rows)
            {
                UsuHR = ObtenerUsuarioHojaRuta(row["usuario"].ToString());
                row["usuario"] = UsuHR.NombreFormateado;
            }



            gvHojaRuta.DataSource = dtHR;
            gvHojaRuta.DataBind();

        }

        private void CargarProductos()
        {
            ddlProductos.DataSource = Bl.BlGetProductos("");
            ddlProductos.DataValueField = "IdProducto";
            ddlProductos.DataTextField = "NProducto";
            ddlProductos.DataBind();
        }

        private void CargarTramiteModificar()
        {
            divMensajeErrorModalModificar.Visible = false;
            divMensajeExitoModalModificar.Visible = false;
            //var tramiteDto = new InscripcionSifcosDto();
            var lista = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList();
            if (lista.Count == 0)
                return;
            InscripcionSifcosDto tramiteDto = lista[0];
            //cargar encabezado
            txtModalNroTramite.Text = tramiteDto.NroTramiteSifcos;
            txtModalRazonSocial.Text = tramiteDto.RazonSocial;
            txtModalCuit.Text = tramiteDto.CUIT;

            switch (Seleccion)
            {
                case "DomEstab":
                    //PARA CARGAR EL DOMICILIO LOCAL DEL ESTABLECIMIENTO
                    divSeccionDomEstab.Visible = true;
                    //       var domicilio1 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLocal.Value.ToString());


                    //obtengo las URL para setear el domicilio a traves de la API 
                    HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
                    var Requestwrapper = new HttpRequestWrapper(Request);

                    UrlDomComercio = Helper.getURLDomicilio(sessionBase, Requestwrapper, "SIF" + tramiteDto.CUIT + tramiteDto.NroSifcos);
                    idVinComercio = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIF" + tramiteDto.CUIT + tramiteDto.NroSifcos);


                    break;
                case "DomLegal":
                    //PARA CARGAR EL DOMICILIO LEGAL DEL ESTABLECIMIENTO
                    divSeccionDomLegal.Visible = true;
                    //var domicilio2 = consultarDomicilioByIdVin(tramiteDto.IdVinDomLegal.Value.ToString());

                    HttpSessionStateBase sessionBase1 = new HttpSessionStateWrapper(Page.Session);
                    var Requestwrapper1 = new HttpRequestWrapper(Request);


                    UrlDomLegal = Helper.getURLDomicilio(sessionBase1, Requestwrapper1, "SIFLEG" + tramiteDto.CUIT);
                    idVinLegal = Helper.getIdVinDomicilio(sessionBase1, Requestwrapper1, "SIFLEG" + tramiteDto.CUIT);



                    break;
                //case "UbicacionMapa":
                //    // PARA UBICAR EL COMERCIO EN EL MAPA
                //    divSeccionUbicacionMapa.Visible = true;
                //    DataTable dtUbicacion = Bl.BlGetUbicacionMapa(EntidadSeleccionada);
                //    if (dtUbicacion.Rows.Count > 0)
                //    {
                //        foreach (DataRow row in dtUbicacion.Rows)
                //        {
                //            txtLatitud.Text = row["latitud"].ToString();
                //            txtLongitud.Text = row["longitud"].ToString();
                //        }
                //    }

                //    break;
                case "ConEstab":
                    // PARA CARGAR CONTACTO
                    divSeccionConEstab.Visible = true;
                    DataTable dtContacto = Bl.BlGetComEmpresa(EntidadSeleccionada.ToString());
                    if (dtContacto.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtContacto.Rows)
                        {
                            switch (row["ID_TIPO_COMUNICACION"].ToString())
                            {
                                case "01": //TELEFONO FIJO
                                    txtCodAreaTelFijoMd.Text = row["COD_AREA"].ToString();
                                    txtTelFijoMd.Text = row["NRO_MAIL"].ToString();
                                    break;
                                case "07": //CELULAR
                                    txtCodAreaCelularMd.Text = row["COD_AREA"].ToString();
                                    txtCelularMd.Text = row["NRO_MAIL"].ToString();
                                    break;
                                case "11": //EMAIL
                                    txtEmailMd.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString())
                                        ? row["NRO_MAIL"].ToString()
                                        : " - ";
                                    break;
                                case "12": //PAGINA WEB
                                    txtWebPageMd.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString())
                                        ? row["NRO_MAIL"].ToString()
                                        : " - ";
                                    break;
                                case "17": //FACEBOOK
                                    txtFacebookMd.Text = !string.IsNullOrEmpty(row["NRO_MAIL"].ToString())
                                        ? row["NRO_MAIL"].ToString()
                                        : " - ";
                                    break;

                            }
                        }

                    }
                    else
                    {
                        txtEmailMd.Text = " - ";
                        txtCelularMd.Text = " - ";
                        txtTelFijoMd.Text = " - ";
                        txtWebPageMd.Text = " - ";
                        txtFacebookMd.Text = " - ";
                    }
                    break;
                case "ProdAct":
                    //PARA CARGAR Actividades y Productos
                    CargarProductos();
                    Session["ListProductos"] = null;
                    var ListProductos = new List<Producto>();
                    DtProductos = Bl.BlGetProductosTramite(tramiteDto.NroTramiteSifcos);
                    foreach (DataRow row in DtProductos.Rows)
                    {
                        ListProductos.Add(new Producto
                        {
                            IdProducto = row["IdProducto"].ToString(),
                            NProducto = row["NProducto"].ToString()
                        });
                    }

                    Session["ListProductos"] = ListProductos;

                    gvProducto.DataSource = ListProductos;
                    gvProducto.DataBind();
                    gvProducto.Visible = true;

                    CargarActividadesSegunProductos();

                    //(IB): Asignación al combo de la actividad primaria seleccionada
                    if (tramiteDto.IdActividadPrimaria != null)
                    {
                        //Buscamos el IdActividad se encuentra en la lista de actividades posibles según producto lo seteamos
                        System.Web.UI.WebControls.ListItem crItem = null;
                        crItem = ddlActividadPrimaria.Items.FindByValue(tramiteDto.IdActividadPrimaria);

                        if (crItem != null)
                            ddlActividadPrimaria.SelectedValue = crItem.Value;
                    }
                    //(IB): Asignación al combo de la actividad Secundaria seleccionada
                    if (tramiteDto.IdActividadSecundaria != null)
                    {
                        //Buscamos el IdActividad se encuentra en la lista de actividades posibles según producto lo seteamos
                        System.Web.UI.WebControls.ListItem crItem = null;
                        crItem = ddlActividadSecundaria.Items.FindByValue(tramiteDto.IdActividadSecundaria);

                        if (crItem != null)
                            ddlActividadSecundaria.SelectedValue = crItem.Value;
                    }
                    // ddlActividadPrimaria.Items.Add(tramiteDto.ActividadPrimaria);
                    // ddlActividadSecundaria.Items.Add(tramiteDto.ActividadSecundaria);
                    break;
                case "Rubros":
                    //(IB) Lógica de rubros

                    //Obtengo la lista completa de rubros y actividades
                    List<RubroActividad> lRa = Bl.BlGetRubrosActividad();

                    //Obtengo la lista completa de rubros
                    List<Rubro> lRubrosDistinct = lRa.Select(g => new Rubro { IdRubro = g.IdRubro.ToString(), NRubro = g.NRubro }).ToList();
                    lRubrosDistinct = lRubrosDistinct.GroupBy(x => x.IdRubro).Select(x => x.First()).ToList();

                    //Bindeo el combo de todos los rubros
                    ddlRubrosPri.DataSource = lRubrosDistinct;
                    ddlRubrosPri.DataValueField = "IdRubro";
                    ddlRubrosPri.DataTextField = "NRubro";
                    ddlRubrosPri.DataBind();
                    ddlRubrosPri.Items.Insert(0, "NINGUNO");

                    //Bindeo el combo de todos los rubros
                    ddlRubrosSec.DataSource = lRubrosDistinct;
                    ddlRubrosSec.DataValueField = "IdRubro";
                    ddlRubrosSec.DataTextField = "NRubro";
                    ddlRubrosSec.DataBind();
                    ddlRubrosSec.Items.Insert(0, "NINGUNO");

                    //Seteo los valores de rubro de la db
                    lblRubroPriMsg.Text = (!string.IsNullOrEmpty(tramiteDto.RubroPrimario)) ? tramiteDto.RubroPrimario : "NINGUNO";
                    hdRubroPri.Value = (tramiteDto.IdRubroPrimario.HasValue) ? tramiteDto.IdRubroPrimario.ToString() : "NINGUNO";
                    hdRubroPriOrigen.Value = "Db";

                    lblRubroSecMsg.Text = (!string.IsNullOrEmpty(tramiteDto.RubroSecundario)) ? tramiteDto.RubroSecundario : "NINGUNO";
                    hdRubroSec.Value = (tramiteDto.IdRubroSecundario.HasValue) ? tramiteDto.IdRubroSecundario.ToString() : "NINGUNO";
                    hdRubroSecOrigen.Value = "Db";

                    if (tramiteDto.IdRubroPrimario.HasValue)
                        hdRubroPri.Value = tramiteDto.IdRubroPrimario.ToString();


                    pnlRubroPri.Visible = true;
                    if (String.IsNullOrEmpty(tramiteDto.IdActividadPrimaria) || tramiteDto.IdActividadPrimaria == "0")
                    {
                        lblRubroPriMsg.Text = "Para elegir el rubro principal debe tener seleccionada una actividad principal";

                        pnlRubroPri.Visible = false;
                    }
                    else
                    {
                        //Hay una actividad primaria seleccionada. Podemos mostrar la seleccion del rubro principal
                        lblActividadPpal.Text = tramiteDto.ActividadPrimaria;
                        //Obtengo la lista completa para rubros según actividad primaria                    
                        List<Rubro> lRaPri = lRa.Where(x => x.IdActividadClanae == tramiteDto.IdActividadPrimaria).Select(g => new Rubro { IdRubro = g.IdRubro.ToString(), NRubro = g.NRubro }).ToList();
                        lRaPri = lRaPri.GroupBy(x => x.IdRubro).Select(x => x.First()).ToList();

                        ddlRubrosPriAct.DataSource = lRaPri;
                        ddlRubrosPriAct.DataValueField = "IdRubro";
                        ddlRubrosPriAct.DataTextField = "NRubro";
                        ddlRubrosPriAct.DataBind();
                        ddlRubrosPriAct.Items.Insert(0, "NINGUNO");
                    }

                    pnlRubroSec.Visible = true;
                    if (String.IsNullOrEmpty(tramiteDto.IdActividadSecundaria) || tramiteDto.IdActividadSecundaria == "0")
                    {
                        lblRubroSecMsg.Text = "Para elegir el rubro secundario debe tener seleccionada una actividad secundaria";
                        pnlRubroSec.Visible = false;
                    }
                    else
                    {
                        //Hay una actividad Secundaria seleccionada. Podemos mostrar la seleccion del rubro secundario
                        lblActividadSec.Text = tramiteDto.ActividadSecundaria;
                        //Obtengo la lista completa para rubros según actividad Secundaria                    
                        List<Rubro> lRaSec = lRa.Where(x => x.IdActividadClanae == tramiteDto.IdActividadSecundaria).Select(g => new Rubro { IdRubro = g.IdRubro.ToString(), NRubro = g.NRubro }).ToList();
                        lRaSec = lRaSec.GroupBy(x => x.IdRubro).Select(x => x.First()).ToList();

                        ddlRubrosSecAct.DataSource = lRaSec;
                        ddlRubrosSecAct.DataValueField = "IdRubro";
                        ddlRubrosSecAct.DataTextField = "NRubro";
                        ddlRubrosSecAct.DataBind();
                        ddlRubrosSecAct.Items.Insert(0, "NINGUNO");
                    }

                    break;




                case "InfoGral":
                    //PARA CARGAR INFORMACIÓN GENERAL
                    divSeccionInfoGral.Visible = true;
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
                    LeerOrigenProveedor(tramiteDto);
                    txtNombreFantasia.Text = tramiteDto.NombreFantasia;
                    txtFechaIniAct.Text = tramiteDto.FecIniTramite.ToShortDateString();
                    txtNroHabMun.Text = tramiteDto.NroHabMunicipal;
                    txtNroDGR.Text = tramiteDto.NroDGR;
                    txtM2Venta.Text = tramiteDto.SupVentas != null ? tramiteDto.SupVentas.Value.ToString() : "-";
                    txtM2Dep.Text = tramiteDto.SupDeposito != null ? tramiteDto.SupDeposito.Value.ToString() : "-";
                    txtM2Admin.Text = tramiteDto.SupAdministracion != null ? tramiteDto.SupAdministracion.Value.ToString() : "-";
                    txtCantPersTotal.Text = tramiteDto.CantTotalpers;
                    txtCantPersRel.Text = tramiteDto.CantPersRelDep;
                    rbSiCobertura.Checked = tramiteDto.Cobertura == "S";
                    rbNoCobertura.Checked = tramiteDto.Cobertura == "N";
                    rbSiCap.Checked = tramiteDto.CapacUltAnio == "S";
                    rbNoCap.Checked = tramiteDto.CapacUltAnio == "N";
                    rbSiSeg.Checked = tramiteDto.Seguro == "S";
                    rbNoSeg.Checked = tramiteDto.Seguro == "N";
                    rbPropietario.Checked = tramiteDto.Propietario == "S";
                    rbInquilino.Checked = tramiteDto.Propietario == "N";
                    break;
                case "RepLegal":
                    // PARA CARGAR REPRESENTANTE LEGAL
                    divSeccionRepLegal.Visible = true;
                    break;

            }

        }

        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
            DtConsulta = new DataTable();
            gvResultado.DataSource = null;
            gvResultado.DataBind();
            txtNroSifcos.Text = "";
            txtNroTramite.Text = "";
            txtCuit.Text = "";
            txtRazonSocial.Text = "";
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            divFiltroFecha.Visible = false;
            chkFiltroFecha.Checked = false;
            HabilitarDesHabilitarCampos(true);
            txtRazonSocial.Focus();
            divPantallaResultado.Visible = false;
            btnConsultar.Enabled = true;
            MostrarOcultarDiv(false);
        }



        protected void btnCancelarInfoDeuda_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalInfoDeudaTramite(false);
        }

        protected void btnVolver_OnClick(object sender, EventArgs e)
        {
            txtApellidoRepresentante.Text = "";
            txtNombreRepresentante.Text = "";
            txtApellidoRepresentante.Text = "";
            txtCuilRepresentante.Text = "";
            MostrarOcultarModalModificarTramite(false);
            divPantallaResultado.Visible = true;
            divPantallaConsulta.Visible = true;
            RefrescarGrilla();
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

        protected void btnAgregarProd_Click(object sender, EventArgs e)
        {
            divSeccionProdAct.Visible = true;
            divGuardarCambios.Visible = true;

            //   MostrarPestaniaActiva(liActividadProducto);
            if (string.IsNullOrEmpty(ddlProductos.SelectedValue))
                return;


            if (!ValidarExistenciaProductoEnGrilla())
                return;// sino pasó la validación se interrumpe el agregado del producto

            var dr = DtProductosTramite.NewRow();
            dr["IdProducto"] = ddlProductos.SelectedValue;
            dr["NProducto"] = ddlProductos.SelectedItem.Text;
            //dr["ID_TRAMITE_SIFCOS"] = DtProductosTramite.Rows[0]["ID_TRAMITE_SIFCOS"].ToString();

            DtProductosTramite.Rows.Add(dr);
            gvProducto.DataSource = DtProductosTramite;
            gvProducto.DataBind();
            gvProducto.Visible = true;

            if (chkConfirmarListaDeProducto.Checked)
                CargarActividadesSegunProductos();
        }

        private bool ValidarExistenciaProductoEnGrilla()
        {

            //el combo Productos tiene ya seleccionado el producto a agregar.
            foreach (DataRow row in DtProductosTramite.Rows)
            {
                if (row["IdProducto"].ToString() == ddlProductos.SelectedValue)
                {
                    lblMensajeError.Text = "El Producto que desea cargar a la lista, ya existe en la misma.";
                    divMensajeError.Visible = true;
                    return false;
                }
            }
            return true;
        }

        private void CargarActividadesSegunProductos()
        {
            var lst = new List<int>();
            foreach (DataRow dr in DtProductos.Rows)
            {
                lst.Add(int.Parse(dr["IdProducto"].ToString()));
            }

            DtActividades = Bl.BlGetActividadesProducto(lst);
            DataRow _ravi = DtActividades.NewRow();
            _ravi["ID_ACTIVIDAD"] = "0";
            _ravi["N_ACTIVIDAD"] = "SIN ASIGNAR";
            DtActividades.Rows.Add(_ravi);

            ddlActividadPrimaria.DataSource = DtActividades;
            ddlActividadPrimaria.DataValueField = "ID_ACTIVIDAD";
            ddlActividadPrimaria.DataTextField = "N_ACTIVIDAD";
            ddlActividadPrimaria.DataBind();
            ddlActividadPrimaria.SelectedValue = "0";

            ddlActividadSecundaria.DataSource = DtActividades;
            ddlActividadSecundaria.DataValueField = "ID_ACTIVIDAD";
            ddlActividadSecundaria.DataTextField = "N_ACTIVIDAD";
            ddlActividadSecundaria.DataBind();
            ddlActividadSecundaria.SelectedValue = "0";

        }

        protected void gvProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvProducto_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            divSeccionProdAct.Visible = true;
            var acciones = new List<string> { "QuitarProducto" };

            if (!acciones.Contains(e.CommandName))
                return;

            gvProducto.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            var idProductoSeleccionado = "0";

            if (gvProducto.SelectedValue != null)
                idProductoSeleccionado = gvProducto.SelectedValue.ToString();

            switch (e.CommandName)
            {

                case "QuitarProducto":

                    foreach (DataRow row in DtProductosTramite.Rows)
                    {
                        if (row["IdProducto"].ToString() == idProductoSeleccionado)
                        {
                            var resultado = Bl.BlNoConfirmarProducto(idProductoSeleccionado, txtModalNroTramite.Text);
                            DtProductosTramite.Rows.Remove(row);
                            break;
                        }
                    }
                    gvProducto.DataSource = DtProductosTramite;
                    gvProducto.DataBind();
                    gvProducto.Visible = true;

                    if (chkConfirmarListaDeProducto.Checked)
                        CargarActividadesSegunProductos();


                    break;
            }
        }

        protected void chkConfirmarListaDeProducto_OnCheckedChanged(object sender, EventArgs e)
        {
            divSeccionProdAct.Visible = true;
            divGuardarCambios.Visible = true;
            if (chkConfirmarListaDeProducto.Checked)
            {
                if (DtProductosTramite.Rows.Count == 0)
                {
                    lblMensajeError.Text =
                        "Debe cargar al menos un producto para poder confirmar la lista de productos.";
                    divMensajeError.Visible = true;
                    return;
                }
                ddlActividadPrimaria.Enabled = true;
                ddlActividadSecundaria.Enabled = true;
                CargarActividadesSegunProductos();
            }
            else
            {
                ddlActividadPrimaria.Enabled = false;
                ddlActividadPrimaria.Items.Clear();
                ddlActividadSecundaria.Enabled = false;
                ddlActividadSecundaria.Items.Clear();
                DtActividades = null;

            }
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

        public InscripcionSifcos ObjetoInscripcion
        {
            get
            {
                return Session["ObjetoInscripcion"] == null ? ObjetoInscripcion = new InscripcionSifcos() : (InscripcionSifcos)Session["ObjetoInscripcion"];
            }
            set
            {
                Session["ObjetoInscripcion"] = value;
            }
        }

        public int? IdEntidadContacto
        {
            get
            {
                return Session["IdEntidadContacto"] == null ? new int?() : (int)Session["IdEntidadContacto"];
            }
            set
            {
                Session["IdEntidadContacto"] = value;
            }
        }

        public DataTable DtEmpresa
        {
            get
            {
                return Session["DtEmpresa"] == null ? new DataTable() : (DataTable)Session["DtEmpresa"];
            }
            set
            {
                Session["DtEmpresa"] = value;
            }
        }

        protected void btnConfirmarNroSifcos_OnClick(object sender, EventArgs e)
        {
            var nro_sifcos = Bl.BlAsignarNrosifcos(EntidadSeleccionada, txtDescripcion.Text, UsuarioCidiLogueado.CUIL);
            if (nro_sifcos != "")
            {
                divExitoAsignarNroSifcos.Visible = true;
                divErrorAsignarNroSifcos.Visible = false;
                lblMensajeExitoModalAsignarNroSifcos.Text = "Se asignó al trámite nro: " + EntidadSeleccionada + " el número SIFCoS: " + nro_sifcos;
                btnConfirmarNroSifcos.Visible = false;
                btnCancelarAsignacionNroSifcos.Text = "Aceptar";
            }
            else
            {
                divExitoAsignarNroSifcos.Visible = false;
                divErrorAsignarNroSifcos.Visible = true;
                lblDivMensajeError1.Text = "El trámite nro: " + EntidadSeleccionada + " no pudo ser asignado el numero de sifcos";
            }


        }

        /// <summary>
        /// Limpia los controles TextBox, DropDownList, checkbox que están marcados como requerido(en rojo) y los cambia al color por defecto.
        /// </summary>
        private void LimpiarControlesRequeridos()
        {
            var controles = new List<Control>();

            //Listado de controles que nos Requeridos.
            controles.Add(txtCuit);

            //contacto
            controles.Add(txtCodAreaTelFijoMd);
            controles.Add(txtTelFijoMd);
            controles.Add(txtCodAreaCelularMd);
            controles.Add(txtCelularMd);
            controles.Add(txtEmailMd);

            //información adicional

            controles.Add(txtM2Venta);
            controles.Add(txtM2Dep);
            controles.Add(txtM2Admin);
            controles.Add(txtCantPersRel);
            controles.Add(txtCantPersTotal);
            controles.Add(txtNroHabMun);

            controles.Add(lblInmueble);

            foreach (Control control in controles)
            {
                if (control is TextBox)
                    QuitarCssClass("campoRequerido", ((TextBox)control));

                else if (control is DropDownList)
                    QuitarCssClass("campoRequerido", ((DropDownList)control));

                else if (control is CheckBox)
                    QuitarCssClass("campoRequerido", ((CheckBox)control));
                else if (control is Label)
                    QuitarCssClass("campoRequerido", ((Label)control));
            }
        }

        /// <summary>
        /// AGregar una CssClass a un control web de ASP.NET.
        /// </summary>
        /// <param name="nombreClass"></param>
        /// <param name="control"></param>
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

        private bool ValidarDatosRequeridos()
        {
            LimpiarControlesRequeridos();

            switch (Seleccion)
            {
                case "DomEstab":
                    //PARA MODIFICAR EL DOMICILIO LOCAL DEL ESTABLECIMIENTO
                    divSeccionDomEstab.Visible = true;

                    break;
                case "DomLegal":
                    //PARA MODIFICAR EL DOMICILIO LEGAL DEL ESTABLECIMIENTO
                    divSeccionDomLegal.Visible = true;


                    break;
                case "ConEstab":
                    // CONTACTO
                    divSeccionConEstab.Visible = true;
                    if (string.IsNullOrEmpty(txtEmailMd.Text))
                    {
                        lblMensajeErrorModalModificar.Text = "Debe ingresar el Email con que trabaja el establecimiento.";
                        AgregarCssClass("campoRequerido", txtEmailMd);
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }

                    if ((string.IsNullOrEmpty(txtCodAreaTelFijoMd.Text) || string.IsNullOrEmpty(txtTelFijoMd.Text)) &&
                        (string.IsNullOrEmpty(txtCodAreaCelularMd.Text) || string.IsNullOrEmpty(txtCelularMd.Text)))
                    {
                        lblMensajeErrorModalModificar.Text = "Debe ingresar al menos Telefono Fijo ó Celular.";
                        if (string.IsNullOrEmpty(txtCodAreaTelFijoMd.Text) || string.IsNullOrEmpty(txtTelFijoMd.Text))
                        {
                            AgregarCssClass("campoRequerido", txtCodAreaTelFijoMd);
                            AgregarCssClass("campoRequerido", txtTelFijoMd);
                        }
                        if (string.IsNullOrEmpty(txtCodAreaCelularMd.Text) || string.IsNullOrEmpty(txtCelularMd.Text))
                        {
                            AgregarCssClass("campoRequerido", txtCelularMd);
                            AgregarCssClass("campoRequerido", txtCodAreaCelularMd);
                        }

                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }
                    break;
                case "ProdAct":
                    //Actividades y Productos
                    divSeccionProdAct.Visible = true;
                    if (string.IsNullOrEmpty(ddlActividadPrimaria.SelectedValue) ||
                        ddlActividadPrimaria.SelectedValue == "0")
                    {
                        lblMensajeErrorModalModificar.Text = "Debe seleccionar Actividad Primaria.";
                        AgregarCssClass("campoRequerido", ddlActividadPrimaria);
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }
                    break;
                case "Rubros":
                    //(IB) Selección de Rubro principal y secundario
                    divSeccionRubro.Visible = true;


                    break;
                //case "UbicacionMapa":
                //    //UBICACION EN EL MAPA
                //    divSeccionUbicacionMapa.Visible = true;
                //    if (string.IsNullOrEmpty(txtLatitud.Text) || string.IsNullOrEmpty(txtLongitud.Text))
                //    {
                //        lblMensajeErrorModalModificar.Text = "Debe ingresar una direccion valida en el mapa.";
                //        AgregarCssClass("campoRequerido", txtLatitud);
                //        AgregarCssClass("campoRequerido", txtLongitud);
                //        return false;
                //    }
                //    break;

                case "InfoGral":
                    //INFORMACIÓN GENERAL
                    divSeccionInfoGral.Visible = true;
                    if (string.IsNullOrEmpty(txtFechaIniAct.Text))
                    {
                        lblMensajeErrorModalModificar.Text = "Debe ingresar la Fecha de Inicio de Actividad.";
                        AgregarCssClass("campoRequerido", txtFechaIniAct);
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }

                    if (string.IsNullOrEmpty(txtNroHabMun.Text))
                    {
                        lblMensajeErrorModalModificar.Text = "Debe ingresar el Nro de Habilitación Municipal.";
                        AgregarCssClass("campoRequerido", txtNroHabMun);
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }
                    if (string.IsNullOrEmpty(txtM2Venta.Text))
                    {
                        lblMensajeErrorModalModificar.Text = "Debe ingresar el valor la Superficie de venta.";
                        AgregarCssClass("campoRequerido", txtM2Venta);
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }
                    else
                    {
                        if (double.Parse(txtM2Venta.Text) < 1)
                        {
                            lblMensajeErrorModalModificar.Text = "La Superficie de venta debe ser mayor o igual a 1.";
                            AgregarCssClass("campoRequerido", txtM2Venta);
                            divMensajeErrorModalModificar.Visible = true;
                            return false;
                        }
                    }

                    if (string.IsNullOrEmpty(txtCantPersTotal.Text))
                    {
                        lblMensajeErrorModalModificar.Text = "La Cantidad de Personal Total es un campo obligatorio.";
                        AgregarCssClass("campoRequerido", txtCantPersTotal);
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }
                    else
                    {
                        if (double.Parse(txtCantPersTotal.Text) < 1)
                        {
                            lblMensajeErrorModalModificar.Text = "La Cantidad de Personal Total debe ser un valor superior ó igual a 1.";
                            AgregarCssClass("campoRequerido", txtCantPersTotal);
                            divMensajeErrorModalModificar.Visible = true;
                            return false;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtCantPersRel.Text))
                    {
                        if (double.Parse(txtCantPersRel.Text) > double.Parse(txtCantPersTotal.Text))
                        {
                            lblMensajeErrorModalModificar.Text = "La Cantidad de Personal en Relación de Dependencia no puede superar a la Cantidad de Personal Total.";
                            AgregarCssClass("campoRequerido", txtCantPersRel);
                            divMensajeErrorModalModificar.Visible = true;
                            return false;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtM2Admin.Text))
                    {
                        if (long.Parse(txtM2Admin.Text) < 1)
                        {
                            lblMensajeErrorModalModificar.Text = "Los valores de las Superficies Administración debe ser 1 o mayor.";
                            divMensajeErrorModalModificar.Visible = true;
                            return false;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtM2Venta.Text))
                    {
                        if (long.Parse(txtM2Venta.Text) < 1)
                        {
                            lblMensajeErrorModalModificar.Text = "Los valores de las Superficies de Venta debe ser 1 o mayor.";
                            divMensajeErrorModalModificar.Visible = true;
                            return false;
                        }
                    }
                    if (!string.IsNullOrEmpty(txtM2Dep.Text))
                    {
                        if (long.Parse(txtM2Dep.Text) < 1)
                        {
                            lblMensajeErrorModalModificar.Text = "Los valores de las Superficies de Deposito debe ser 1 o mayor.";
                            divMensajeErrorModalModificar.Visible = true;
                            return false;
                        }
                    }


                    if (!rbInquilino.Checked && !rbPropietario.Checked)
                    {
                        lblMensajeErrorModalModificar.Text = "Debe indicar si es Propietario o Inquilino.";
                        divMensajeErrorModalModificar.Visible = true;

                        return false;
                    }

                    if (rbInquilino.Checked)
                    {
                        if (!rb1015.Checked && !rb1520.Checked && !rb5.Checked && !rb510.Checked)
                        {
                            lblMensajeErrorModalModificar.Text = "Si es Inquilino, debe seleccionar un monto de alquiler.";
                            divMensajeErrorModalModificar.Visible = true;
                            return false;
                        }
                    }


                    //INFORMACIÓN ADICIONAL
                    if (!rbSiCap.Checked && !rbNoCap.Checked)
                    {
                        lblMensajeErrorModalModificar.Text = "Debe indicar si realizó Capacitación o no.";
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }
                    if (!rbSiCobertura.Checked && !rbNoCobertura.Checked)
                    {
                        lblMensajeErrorModalModificar.Text = "Debe indicar si posee Cobertura o no.";
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }

                    if (!rbSiSeg.Checked && !rbNoSeg.Checked)
                    {
                        lblMensajeErrorModalModificar.Text = "Debe indicar si posee Seguro o no.";
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }
                    if (!chkprov.Checked && !ChkInter.Checked && !ChkNacional.Checked)
                    {
                        lblMensajeErrorModalModificar.Text = "Debe indicar al menos una opción de 'Origen proveedor'.";
                        divMensajeErrorModalModificar.Visible = true;
                        return false;
                    }
                    break;
                case "RepLegal":
                    // REPRESENTANTE LEGAL
                    divSeccionRepLegal.Visible = true;
                    if (string.IsNullOrEmpty(txtApellidoRepresentante.Text) && string.IsNullOrEmpty(txtNombreRepresentante.Text))
                    {
                        lblMensajeErrorModalModificar.Text = "Debe ingresar representante Válido.";
                        divMensajeErrorModalModificar.Visible = true;
                        AgregarCssClass("campoRequerido", txtNombreRepresentante);
                        AgregarCssClass("campoRequerido", txtApellidoRepresentante);
                        return false;
                    }
                    break;

            }

            return true;

        }

        protected void btnCancelarAsignacionNroSifcos_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalAsignarNroSifcos(false);
            btnConfirmarNroSifcos.Visible = true;
        }

        private void LeerOrigenProveedor(InscripcionSifcosDto tramiteDto)
        {
            switch (tramiteDto.NombreOrigenProveedor)
            {
                case "PROVINCIAL": //PROVINCIAL
                    chkprov.Checked = true;
                    break;
                case "NACIONAL": //NACIONAL
                    ChkNacional.Checked = true;
                    break;
                case "INTERNACIONAL": //INTERNACIONAL
                    ChkInter.Checked = true;
                    break;
                case "PROVINCIAL - NACIONAL": //PROVINCIAL - NACIONAL
                    chkprov.Checked = true;
                    ChkNacional.Checked = true;
                    break;
                case "NACIONAL - INTERNACIONAL": //NACIONAL - INTERNACIONAL
                    ChkNacional.Checked = true;
                    ChkInter.Checked = true;
                    break;
                case "PROVINCIAL - INTERNACIONAL": //PROVINCIAL - INTERNACIONAL
                    chkprov.Checked = true;
                    ChkInter.Checked = true;
                    break;
                case "PROVINCIAL - NACIONAL - INTERNACIONAL": //PROVINCIAL - NACIONAL - INTERNACIONAL
                    chkprov.Checked = true;
                    ChkNacional.Checked = true;
                    ChkInter.Checked = true;
                    break;
            }
        }

        private void CargarOrigenProveedor()
        {
            int idOrigenProveedor = 0;
            if (chkprov.Checked && !ChkNacional.Checked && !ChkInter.Checked)
                idOrigenProveedor = 1;
            if (!chkprov.Checked && ChkNacional.Checked && !ChkInter.Checked)
                idOrigenProveedor = 2;
            if (!chkprov.Checked && !ChkNacional.Checked && ChkInter.Checked)
                idOrigenProveedor = 3;
            if (chkprov.Checked && ChkNacional.Checked && !ChkInter.Checked)
                idOrigenProveedor = 12;
            if (chkprov.Checked && ChkNacional.Checked && ChkInter.Checked)
                idOrigenProveedor = 123;
            if (!chkprov.Checked && ChkNacional.Checked && ChkInter.Checked)
                idOrigenProveedor = 23;
            if (chkprov.Checked && !ChkNacional.Checked && ChkInter.Checked)
                idOrigenProveedor = 13;
            tramiteDto.IdOrigenProveedor = idOrigenProveedor != 0 ? idOrigenProveedor : 1;
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

        public Usuario UsuarioCidiRep
        {
            get
            {
                return Session["UsuarioCidiRep"] == null ? new Usuario() : (Usuario)Session["UsuarioCidiRep"];

            }
            set
            {
                Session["UsuarioCidiRep"] = value;
            }
        }

        public Usuario UsuarioCidiGestor
        {
            get
            {
                return Session["UsuarioCidiGestor"] == null ? new Usuario() : (Usuario)Session["UsuarioCidiGestor"];

            }
            set
            {
                Session["UsuarioCidiGestor"] = value;
            }
        }


        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTRSpagas();

        }

        protected void btnBuscarRepresentante_Click(object sender, EventArgs e)
        {
            divMensajeExitoModalModificar.Visible = false;
            divMensajeErrorModalModificar.Visible = false;
            divMjeErrorRepLegal.Visible = false;
            divSeccionRepLegal.Visible = true;
            divGuardarCambios.Visible = true;
            ObtenerUsuarioRepresentante();

        }

        protected void ObtenerUsuarioRepresentante()
        {
            if (txtCuilRepresentante.Text.Trim().Length != 11)
            {
                //mensaje de error.
                lblErrorRepLegal.Text = "El formato de CUIT no es correcto.";
                AgregarCssClass("campoRequerido", txtCuilRepresentante);
                divMjeErrorRepLegal.Visible = true;
                return;
            }

            if (string.IsNullOrEmpty(txtCuilRepresentante.Text))
            {
                lblErrorRepLegal.Text = "Debe ingresar cuil del Representante";
                AgregarCssClass("campoRequerido", txtCuilRepresentante);
                divMjeErrorRepLegal.Visible = true;
                return;
            }

            //buscar en RCIVIL
            string DNI;
            string digitoVerificador = txtCuilRepresentante.Text.Substring(2, 1);
            if (digitoVerificador == "0")
            {
                DNI = txtCuilRepresentante.Text.Substring(3, 7);
            }
            else
            {
                DNI = txtCuilRepresentante.Text.Substring(2, 8);
            }
            var resultado = Bl.BlGetPersonasRcivil2(DNI, ddlSexoRP.SelectedValue);

            if (resultado.Rows.Count != 0)
            {
                divMensaejeErrorModal.Visible = false;
                txtNombreRepresentante.Text = resultado.Rows[0]["nombre"].ToString();
                txtApellidoRepresentante.Text = resultado.Rows[0]["apellido"].ToString();
                txtSexoRepresentante.Text = resultado.Rows[0]["tipo_sexo"].ToString();
            }
            else
            {

                String PSexo = ddlSexoRP.SelectedValue;

                var dtConsultaPersonas = Bl.BlGetPersonasRcivil2(DNI, PSexo);
                if (dtConsultaPersonas.Rows.Count > 0)
                {
                    string Pcuil = txtCuilRepresentante.Text;
                    string Pdni = DNI;
                    string Pidnumero = dtConsultaPersonas.Rows[0]["id_numero"].ToString();
                    string Pcod_pais = dtConsultaPersonas.Rows[0]["pai_cod_pais"].ToString();
                    var InsertarCUIL = Bl.BlGenerarCUIL(Pcuil, Int64.Parse(Pdni), PSexo, Int64.Parse(Pidnumero), Pcod_pais);
                    if (InsertarCUIL == "OK-CUIL")
                    {
                        divMjeErrorRepLegal.Visible = false;
                        divMjeExitoRepLegal.Visible = true;
                        lblExitoRepLegal.Text =
                            "La persona no estaba cargada en base de datos y fue agregada automaticamente.";
                        dtConsultaPersonas = Bl.BlGetPersonasRcivil2(DNI, PSexo);

                        if (dtConsultaPersonas.Rows.Count != 0)
                        {
                            divMensaejeErrorModal.Visible = false;
                            txtNombreRepresentante.Text = dtConsultaPersonas.Rows[0]["nombre"].ToString();
                            txtApellidoRepresentante.Text = dtConsultaPersonas.Rows[0]["apellido"].ToString();
                            txtSexoRepresentante.Text = dtConsultaPersonas.Rows[0]["tipo_sexo"].ToString();
                        }
                    }



                }
                else
                {
                    divMjeErrorRepLegal.Visible = true;
                    divMjeExitoRepLegal.Visible = false;
                    lblErrorRepLegal.Text = "La persona no ha sido encontrada";
                    txtApellidoRepresentante.Text = "";
                    txtNombreRepresentante.Text = "";
                    txtSexoRepresentante.Text = "";


                }
                return;
            }

            //buscar en CIDI
            string urlapi = WebConfigurationManager.AppSettings["CiDiUrl"].ToString();
            Entrada entrada = new Entrada();
            entrada.IdAplicacion = Config.CiDiIdAplicacion;
            entrada.Contrasenia = Config.CiDiPassAplicacion;
            entrada.HashCookie = Request.Cookies["CiDi"].Value.ToString();
            entrada.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entrada.TokenValue = Config.ObtenerToken_SHA1(entrada.TimeStamp);
            entrada.Cuil = txtCuilRepresentante.Text;
            UsuarioCidiRep = Config.LlamarWebAPI<Entrada, Usuario>(APICuenta.Usuario.Obtener_Usuario_Basicos_CUIL, entrada);

        }

        protected void btnDomEstab_Click(object sender, EventArgs e)
        {
            divMensajeExitoModalModificar.Visible = false;
            divMensajeErrorModalModificar.Visible = false;
            divSeccionDomEstab.Visible = true;
            divSeccionConEstab.Visible = false;
            divSeccionDomLegal.Visible = false;
            divSeccionInfoGral.Visible = false;
            divSeccionProdAct.Visible = false;
            divSeccionRubro.Visible = false;
            divSeccionRepLegal.Visible = false;
            divGuardarCambios.Visible = true;
            Seleccion = "DomEstab";
            CargarTramiteModificar();
        }

        protected void btnDomLegal_Click(object sender, EventArgs e)
        {
            divMensajeExitoModalModificar.Visible = false;
            divMensajeErrorModalModificar.Visible = false;
            divSeccionDomEstab.Visible = false;
            divSeccionConEstab.Visible = false;
            divSeccionDomLegal.Visible = true;
            divSeccionInfoGral.Visible = false;
            divSeccionProdAct.Visible = false;
            divSeccionRepLegal.Visible = false;
            divGuardarCambios.Visible = true;
            Seleccion = "DomLegal";
            CargarTramiteModificar();

        }

        protected void btnConEstab_Click(object sender, EventArgs e)
        {
            divMensajeExitoModalModificar.Visible = false;
            divMensajeErrorModalModificar.Visible = false;
            divSeccionDomEstab.Visible = false;
            divSeccionConEstab.Visible = true;
            divSeccionDomLegal.Visible = false;
            divSeccionInfoGral.Visible = false;
            divSeccionProdAct.Visible = false;
            divSeccionRepLegal.Visible = false;
            divSeccionRubro.Visible = false;
            divSeccionRubro.Visible = false;
            divGuardarCambios.Visible = true;
            Seleccion = "ConEstab";
            CargarTramiteModificar();


        }


        protected void btnInfoGral_Click(object sender, EventArgs e)
        {
            divMensajeExitoModalModificar.Visible = false;
            divMensajeErrorModalModificar.Visible = false;
            divSeccionDomEstab.Visible = false;
            divSeccionConEstab.Visible = false;
            divSeccionDomLegal.Visible = false;
            divSeccionInfoGral.Visible = true;
            divSeccionProdAct.Visible = false;
            divSeccionRubro.Visible = false;
            divSeccionRepLegal.Visible = false;
            divGuardarCambios.Visible = true;
            Seleccion = "InfoGral";
            CargarTramiteModificar();

        }

        protected void btnRepLegal_Click(object sender, EventArgs e)
        {
            divMensajeExitoModalModificar.Visible = false;
            divMensajeErrorModalModificar.Visible = false;
            divSeccionDomEstab.Visible = false;
            divSeccionConEstab.Visible = false;
            divSeccionDomLegal.Visible = false;
            divSeccionInfoGral.Visible = false;
            divSeccionProdAct.Visible = false;
            divSeccionRubro.Visible = false;
            divSeccionRepLegal.Visible = true;
            divGuardarCambios.Visible = true;
            Seleccion = "RepLegal";
            CargarTramiteModificar();
        }

        protected void btnProdAct_Click(object sender, EventArgs e)
        {
            divMensajeExitoModalModificar.Visible = false;
            divMensajeErrorModalModificar.Visible = false;
            divSeccionDomEstab.Visible = false;
            divSeccionConEstab.Visible = false;
            divSeccionDomLegal.Visible = false;
            divSeccionInfoGral.Visible = false;
            divSeccionProdAct.Visible = true;
            divSeccionRubro.Visible = false;
            divSeccionRepLegal.Visible = false;
            divGuardarCambios.Visible = true;
            Seleccion = "ProdAct";
            CargarTramiteModificar();
        }
        /// <summary>
        /// IB: Click en el btn Rubros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRubro_Click(object sender, EventArgs e)
        {
            divMensajeExitoModalModificar.Visible = false;
            divMensajeErrorModalModificar.Visible = false;
            divSeccionDomEstab.Visible = false;
            divSeccionConEstab.Visible = false;
            divSeccionDomLegal.Visible = false;
            divSeccionInfoGral.Visible = false;
            divSeccionRubro.Visible = true;
            divSeccionRepLegal.Visible = false;
            divGuardarCambios.Visible = true;
            divSeccionProdAct.Visible = false;
            Seleccion = "Rubros";
            CargarTramiteModificar();
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (!ValidarDatosRequeridos())
            {
                divMensajeExitoModalModificar.Visible = false;
                return;
            }
            //Domicilio domicilio1 = new Domicilio();
            //Domicilio domicilio2 = new Domicilio();

            var tram = Bl.GetInscripcionSifcosDto(EntidadSeleccionada).ToList()[0];
            tramiteDto.NroTramiteSifcos = tram.NroTramiteSifcos;
            tramiteDto.IdEntidad = tram.IdEntidad;
            tramiteDto.CUIT = tram.CUIT;
            tramiteDto.IdSede = tram.IdSede;
            tramiteDto.IdTipoTramite = tram.IdTipoTramite;
            tramiteDto.NroSifcos = tram.NroSifcos;

            tramiteDto.Longitud = tram.Longitud;
            tramiteDto.Latitud = tram.Latitud;
            tramiteDto.CuilUsuarioCidi = UsuarioCidiLogueado.CUIL;

            var Resultado = "";
            switch (Seleccion)
            {
                case "DomEstab":


                    //PARA MODIFICAR EL DOMICILIO LOCAL DEL ESTABLECIMIENTO
                    //--ver aca que le paso

                    HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
                    var Requestwrapper = new HttpRequestWrapper(Request);
                    sessionBase = new HttpSessionStateWrapper(Page.Session);
                    Requestwrapper = new HttpRequestWrapper(Request);
                    DomComercio = Helper.getDomicilio(sessionBase, Requestwrapper, "SIF" + tramiteDto.CUIT + tramiteDto.NroSifcos);
                    tramiteDto.Latitud = DomComercio.Latitud;
                    tramiteDto.Longitud = DomComercio.Longitud;
                    idVinComercio = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIF" + tramiteDto.CUIT + tramiteDto.NroSifcos);
                    tramiteDto.IdVinDomLocal = idVinComercio;

                    Resultado = Bl.BlModificarDomicilioLocaldelTramite(tramiteDto); //OK1
                    break;
                case "DomLegal":  //PARA MODIFICAR EL DOMICILIO LEGAL DEL ESTABLECIMIENTO
                                  //cargo los datos domicilio legal
                                  //- ver aca que le paso

                    HttpSessionStateBase sessionBase1 = new HttpSessionStateWrapper(Page.Session);
                    var Requestwrapper1 = new HttpRequestWrapper(Request);
                    sessionBase1 = new HttpSessionStateWrapper(Page.Session);
                    Requestwrapper1 = new HttpRequestWrapper(Request);

                    DomLegal = Helper.getDomicilio(sessionBase1, Requestwrapper1, "SIFLEG" + tramiteDto.CUIT);
                    idVinLegal = Helper.getIdVinDomicilio(sessionBase1, Requestwrapper1, "SIFLEG" + tramiteDto.CUIT);

                    tramiteDto.IdVinDomLegal = idVinLegal;

                    Resultado = Bl.BlModificarDomicilioLegaldelTramite(tramiteDto); //OK2

                    break;

                case "ConEstab":
                    //cargo los datos contacto
                    tramiteDto.EmailEstablecimiento = txtEmailMd.Text;
                    tramiteDto.CodAreaCelular = txtCodAreaCelularMd.Text;
                    tramiteDto.Celular = txtCelularMd.Text;
                    tramiteDto.CodAreaTelFijo = txtCodAreaTelFijoMd.Text;
                    tramiteDto.TelFijo = txtTelFijoMd.Text;
                    tramiteDto.WebPage = txtWebPageMd.Text;
                    tramiteDto.Facebook = txtFacebookMd.Text;
                    // MODIFICAR EL CONTACTO
                    Resultado = Bl.BlModificarContacto(tramiteDto); //OK6

                    break;
                case "ProdAct":
                    //cargo productos

                    var ListProductos = new List<Producto>();
                    //foreach (Producto Pr in ListProductos)
                    //{
                    //    var vaciarRelaciones = Bl.BlEliminarRelProd(Pr.IdProducto);
                    //}

                    foreach (DataRow row in DtProductos.Rows)
                    {
                        ListProductos.Add(new Producto { IdProducto = row["IdProducto"].ToString(), NProducto = row["NProducto"].ToString() });
                    }

                    tramiteDto.Productos = ListProductos;

                    //cargo actividades
                    tramiteDto.ActividadPrimaria = (ddlActividadPrimaria.SelectedValue != "0") ? ddlActividadPrimaria.SelectedValue : null;
                    tramiteDto.ActividadSecundaria = (ddlActividadSecundaria.SelectedValue != "0") ? ddlActividadSecundaria.SelectedValue : null;
                    Resultado = Bl.BlModificarProdAct(tramiteDto); //OK4
                    //Actividades y Productos
                    break;
                case "Rubros":
                    //(IB) Guardo Rubros
                    //termino de asignar valores que me interesan para rubros
                    tramiteDto.IdActividadPrimaria = tram.IdActividadPrimaria;
                    tramiteDto.IdActividadSecundaria = tram.IdActividadSecundaria;
                    tramiteDto.ActividadPrimaria = tram.ActividadPrimaria;
                    tramiteDto.ActividadSecundaria = tram.ActividadSecundaria;
                    tramiteDto.IdRubroPrimario = tram.IdRubroPrimario;
                    tramiteDto.IdRubroSecundario = tram.IdRubroSecundario;
                    tramiteDto.RubroPrimario = tram.RubroPrimario;
                    tramiteDto.RubroSecundario = tram.RubroSecundario;

                    if (string.IsNullOrEmpty(tramiteDto.IdActividadPrimaria))
                        break;

                    //Obtengo el valor original
                    int? IdRubroPrimario = tramiteDto.IdRubroPrimario;


                    //Si ha cambiado la definición de rubro con respecto a la DB
                    if (hdRubroPriOrigen.Value != "Db")
                    {
                        //Si se eligió un rubro del combo con todos los rubros.
                        if (hdRubroPriOrigen.Value == "Rubros" && hdRubroPri.Value != "0" && hdRubroPri.Value != "NINGUNO")
                        {
                            //Intengar asociar ese rubro con la actividad PRINCIPAL
                            Bl.BlSetRubroActividad(Convert.ToInt32(hdRubroPri.Value), tramiteDto.IdActividadPrimaria);
                        }
                        //El nuevo valor de rubro debe ser null
                        if (hdRubroPri.Value == "NINGUNO") IdRubroPrimario = null;
                        else
                            IdRubroPrimario = Convert.ToInt32(hdRubroPri.Value);


                    }


                    if (string.IsNullOrEmpty(tramiteDto.IdActividadSecundaria))
                        break;

                    //Obtengo el valor original
                    int? IdRubroSecundario = tramiteDto.IdRubroSecundario;

                    //Si ha cambiado la definición de rubro con respecto a la DB
                    if (hdRubroSecOrigen.Value != "Db")
                    {
                        //Si se eligió un rubro del combo con todos los rubros.
                        if (hdRubroSecOrigen.Value == "Rubros" && hdRubroSec.Value != "0" && hdRubroSec.Value != "NINGUNO")
                        {
                            //Intengar asociar ese rubro con la actividad Secundaria
                            Bl.BlSetRubroActividad(Convert.ToInt32(hdRubroSec.Value), tramiteDto.IdActividadSecundaria);
                        }
                        //El nuevo valor de rubro debe ser null
                        if (hdRubroSec.Value == "NINGUNO") IdRubroSecundario = null;
                        else
                            IdRubroSecundario = Convert.ToInt32(hdRubroSec.Value);


                    }

                    //Grabo rubro primario o secundario
                    if (hdRubroPriOrigen.Value != "Db" || hdRubroSecOrigen.Value != "Db")
                    {
                        var t = Bl.BlActualizarRubrosPriSec(IdRubroPrimario, IdRubroSecundario, Convert.ToInt32(tramiteDto.NroTramiteSifcos));
                        if (t == true)
                        {
                            Resultado = "OK8";
                        }
                    }
                    divSeccionRubro.Visible = false;
                    break;
                case "InfoGral":
                    //cargo informacion general
                    DateTime outNumeroDateTime;
                    DateTime.TryParse(txtFechaIniAct.Text, out outNumeroDateTime);
                    tramiteDto.FechaIniActividad = (outNumeroDateTime == DateTime.MinValue ? new DateTime(2016, 1, 1) : outNumeroDateTime).ToString();
                    //tramiteDto.FechaIniActividad = txtFechaIniAct.Text;
                    if (!string.IsNullOrEmpty(txtNombreFantasia.Text))
                    {
                        tramiteDto.NombreFantasia = txtNombreFantasia.Text;
                    }
                    tramiteDto.NroHabMunicipal = txtNroHabMun.Text;
                    tramiteDto.NroDGR = txtNroDGR.Text;
                    tramiteDto.SupAdministracion = txtM2Admin.Text != "" ? (long?)Int64.Parse(txtM2Admin.Text) : null;
                    tramiteDto.SupVentas = txtM2Venta.Text != "" ? (long?)Int64.Parse(txtM2Venta.Text) : null;
                    tramiteDto.SupDeposito = txtM2Dep.Text != "" ? (long?)Int64.Parse(txtM2Dep.Text) : null;
                    var prop = rbPropietario.Checked ? tramiteDto.Propietario = "S" : tramiteDto.Propietario = "N";
                    tramiteDto.CantTotalpers = txtCantPersTotal.Text;
                    tramiteDto.CantPersRelDep = txtCantPersRel.Text;
                    var cob = rbSiCobertura.Checked ? tramiteDto.Cobertura = "S" : tramiteDto.Cobertura = "N";
                    var cap = rbSiCap.Checked ? tramiteDto.CapacUltAnio = "S" : tramiteDto.CapacUltAnio = "N";
                    var seg = rbSiSeg.Checked ? tramiteDto.Seguro = "S" : tramiteDto.Seguro = "N";

                    CargarOrigenProveedor();
                    if (rbPropietario.Checked)
                    {
                        tramiteDto.RangoAlquiler = " - ";
                        limpiarRBRangoAlquiler(false);
                    }
                    tramiteDto.RangoAlquiler = rb5.Checked ? "$5000" : " - ";
                    if (string.IsNullOrEmpty(tramiteDto.RangoAlquiler))
                        tramiteDto.RangoAlquiler = rb510.Checked ? "$5000 a $10000" : " - ";
                    if (string.IsNullOrEmpty(tramiteDto.RangoAlquiler))
                        tramiteDto.RangoAlquiler = rb1015.Checked ? "$10000 a $15000" : " - ";
                    if (string.IsNullOrEmpty(tramiteDto.RangoAlquiler))
                        tramiteDto.RangoAlquiler = rb1520.Checked ? "más de $15000" : " - ";
                    Resultado = Bl.BlModificarInfoGral(tramiteDto); //OK3
                    //INFORMACIÓN GENERAL
                    break;
                case "RepLegal":
                    tramiteDto.CuilRepLegal = txtCuilRepresentante.Text;
                    tramiteDto.IdCargo = Int64.Parse(ddlCargoOcupa.SelectedValue);
                    Resultado = Bl.BlModificarRepLegal(tramiteDto); //OK5
                    //PARA MODIFICAR REPRESENTANTE LEGAL
                    break;
                case "ERROR":
                    Resultado = "ERROR";
                    break;
            }


            if (Resultado == "ERROR2")
            {

                lblMensajeError.Text = "No se guardaron los productos del tramite.";
                divMensajeExito.Visible = false;
                divMensajeError.Visible = true;
            }

            if (Resultado == "ERROR")
            {

                lblMensajeError.Text = "La actualizacion del tramite encontro errores... Vuelva a intentar.";
                divMensajeExito.Visible = false;
                divMensajeError.Visible = true;
            }
            else
            {
                divMensajeExito.Visible = true;
                divMensajeError.Visible = false;
                LimpiarCamporFormuario();
                switch (Resultado)
                {
                    case "OK1":
                        lblMensajeExito.Text = "Se modificó con éxito el domicilio del Comercio. ";
                        break;
                    case "OK2":
                        lblMensajeExito.Text = "Se modificó con éxito el domicilio legal del Comercio. ";
                        break;
                    case "OK3":
                        lblMensajeExito.Text = "Se modificó con éxito la información general del trámite. ";
                        break;
                    case "OK4":
                        lblMensajeExito.Text = "Se modificó con éxito los productos y actividades del trámite. ";
                        break;
                    case "OK5":
                        lblMensajeExito.Text = "Se modificó con éxito el representante legal del trámite. ";
                        break;
                    case "OK6":
                        lblMensajeExito.Text = "Se modificó con éxito el contacto del trámite. ";
                        break;
                    case "OK7":
                        lblMensajeExito.Text = "Se modificó con éxito la ubicación del comercio. ";
                        break;
                    case "OK8":
                        lblMensajeExito.Text = "Se modificaron con éxito los rubros del comercio. ";
                        break;

                }


            }
            MostrarOcultarModalModificarTramite(false);
            RefrescarGrilla();

        }

        private void limpiarRBRangoAlquiler(bool var)
        {
            rb5.Checked = var;
            rb510.Checked = var;
            rb1015.Checked = var;
            rb1520.Checked = var;
        }

        protected void ocultarPaneles(bool var)
        {
            divSeccionDomEstab.Visible = var;
            divSeccionConEstab.Visible = var;
            divSeccionDomLegal.Visible = var;
            divSeccionInfoGral.Visible = var;
            divSeccionProdAct.Visible = var;
            divSeccionRepLegal.Visible = var;
            divGuardarCambios.Visible = var;
            //divSeccionRubro.Visible = var;

        }
        /// <summary>
        /// (IB)
        /// Seleccionar un rubro principal desde el combo de todos los rubros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeleccionarRubrosPri_Click(object sender, EventArgs e)
        {
            divGuardarCambios.Visible = true;
            lblRubroPriMsg.Text = ddlRubrosPri.SelectedItem.Text;
            hdRubroPri.Value = ddlRubrosPri.SelectedItem.Value;
            hdRubroPriOrigen.Value = "Rubros";
        }
        /// <summary>
        /// (IB)
        /// Seleccionar un rubro principal desde el combo de los rubros asociados a la actividad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeleccionarRubrosPriAct_Click(object sender, EventArgs e)
        {
            divGuardarCambios.Visible = true;
            lblRubroPriMsg.Text = ddlRubrosPriAct.SelectedItem.Text;
            hdRubroPri.Value = ddlRubrosPriAct.SelectedItem.Value;
            hdRubroPriOrigen.Value = "RubrosAct";
        }
        /// <summary>
        /// (IB)
        /// Seleccionar un rubro Secundario desde el combo de todos los rubros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeleccionarRubrosSec_Click(object sender, EventArgs e)
        {
            divGuardarCambios.Visible = true;
            lblRubroSecMsg.Text = ddlRubrosSec.SelectedItem.Text;
            hdRubroSec.Value = ddlRubrosSec.SelectedItem.Value;
            hdRubroSecOrigen.Value = "Rubros";
        }
        /// <summary>
        /// (IB)
        /// Seleccionar un rubro Secundario desde el combo de los rubros asociados a la actividad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeleccionarRubrosSecAct_Click(object sender, EventArgs e)
        {
            divGuardarCambios.Visible = true;
            lblRubroSecMsg.Text = ddlRubrosSecAct.SelectedItem.Text;
            hdRubroSec.Value = ddlRubrosSecAct.SelectedItem.Value;
            hdRubroSecOrigen.Value = "RubrosAct";
        }

        #region api nueva de domicilio

        public string UrlDomLegal
        {
            get { return (string)Session["UrlDomLegal"]; }
            set { Session["UrlDomLegal"] = value; }
        }
        public string UrlDomComercio
        {
            get { return (string)Session["UrlDomComercio"]; }
            set { Session["UrlDomComercio"] = value; }
        }

        public int idVinLegal
        {
            get { return (int)Session["IdVinLegal"]; }
            set { Session["IdVinLegal"] = value; }
        }
        public int idVinComercio
        {
            get { return (int)Session["idVinComercio"]; }
            set { Session["idVinComercio"] = value; }
        }

        public AppComunicacion.ApiModels.Domicilio DomComercio
        {
            get { return (AppComunicacion.ApiModels.Domicilio)Session["DomComercio"]; }
            set { Session["DomComercio"] = value; }
        }
        public AppComunicacion.ApiModels.Domicilio DomLegal
        {
            get { return (AppComunicacion.ApiModels.Domicilio)Session["DomLegal"]; }
            set { Session["DomLegal"] = value; }
        }

        //--Para Botones modificar
        protected void btnModificarDomComercio_OnClick(object sender, EventArgs e)
        {
            AltaDomComercio();
            divErrorCargaDomEstab.Visible = false;
            divExitoCargaDomEstab.Visible = false;
            divSeccionDomEstab.Visible = true;
            divGuardarCambios.Visible = true;
        }
        protected void btnModificarDomLegal_OnClick(object sender, EventArgs e)
        {
            AltaDomLegal();
            divErrorCargaDomLegal.Visible = false;
            divExitoCargaDomLegal.Visible = false;
            divSeccionDomLegal.Visible = true;
            divGuardarCambios.Visible = true;
        }

        protected void AltaDomComercio()
        {
            List<InscripcionSifcosDto> list = Bl.GetInscripcionSifcosDto(EntidadSeleccionada);
            string idEntidad = list[0].IdEntidad;
            HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
            var Requestwrapper = new HttpRequestWrapper(Request);

            UrlDomComercio = Helper.AltaDomicilio(sessionBase, Requestwrapper, "SIF" + list[0].CUIT + list[0].NroSifcos);
            idVinComercio = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIF" + list[0].CUIT + list[0].NroSifcos);


        }

        protected void AltaDomLegal()
        {
            List<InscripcionSifcosDto> list = Bl.GetInscripcionSifcosDto(EntidadSeleccionada);
            string idEntidad = list[0].IdEntidad;

            HttpSessionStateBase sessionBase = new HttpSessionStateWrapper(Page.Session);
            var Requestwrapper = new HttpRequestWrapper(Request);

            UrlDomLegal = Helper.AltaDomiciliolegal(sessionBase, Requestwrapper, "SIFLEG" + list[0].CUIT);
            idVinComercio = Helper.getIdVinDomicilio(sessionBase, Requestwrapper, "SIFLEG" + list[0].CUIT);


        }

        //--Para Botones salir
        protected void btnSalirDomEstab_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalModificarTramite(true);
            divSeccionDomEstab.Visible = false;
        }
        protected void btnSalirDomLegal_OnClick(object sender, EventArgs e)
        {
            MostrarOcultarModalModificarTramite(true);
            divSeccionDomLegal.Visible = false;
        }



        #endregion


        private void ImprimirDoc(int opcion)
        {

            IdDocumentoCDD = Bl.blGetIdDocumentoCDD(EntidadSeleccionada, opcion);

            if (IdDocumentoCDD > 0)
            {
                var _respLogin = new CDDResponseLogin();
                var _response = new CDDResponseConsulta();

                try
                {
                    _respLogin = Get_Authorize_Web_Api_CDD();

                    if (_respLogin != null && _respLogin.Codigo_Resultado.Equals("SEO"))
                    {
                        if (Set_Parameters_Request_Post(_respLogin.Llave_BLOB_Login))
                        {
                            _response = Get_Document_Web_Api_CDD();

                            if (_response != null && _response.Codigo_Resultado.Equals("SEO"))
                            {
                                Download_Document(_response);
                            }
                            else
                            {
                                Response.Write("<script>alert('Codigo Error: " + _response.Codigo_Resultado +
                                               " Descripcion: " + _response.Detalle_Resultado + "');</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Codigo Error: " + _respLogin.Codigo_Resultado + " Descripcion: " +
                                       _respLogin.Detalle_Resultado + "');</script>");
                    }
                }
                catch (CDDException waEx)
                {
                    Response.Write("<script>alert('Codigo Error: " + waEx.ErrorCode + " Descripcion: " +
                                   waEx.ErrorDescription + "');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Codigo Error: " + ex.InnerException + "');</script>");
                }

            }
            else
            {
                divMensajeError.Visible = true;
                lblMensajeError.Text = "No se Encontró documentacion adjunta para el trámite seleccionado";
            }

        }

        internal void Download_Document(CDDResponseConsulta _p_response)
        {
            try
            {
                divMensajeError.Visible = false;
                String pathImg = String.Empty;

                // Desencriptado de la Documentación y Preview
                var objCryptoHash = new CryptoManagerV4._0.Clases.CryptoHash();
                String mensaje = String.Empty;

                if (_p_response.Documentacion.Imagen_Documentacion != null)
                {
                    pathImg = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
                        "documentacion." + _p_response.Documentacion.Extension.ToLower());

                    divMensajeError.Visible = false;
                    lblMensajeError.Text = String.Empty;
                    lblMensajeError.Visible = false;

                    if (_p_response.Documentacion.Extension.ToUpper().Equals("PDF"))
                    {
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment; filename= Documentacion_" + tramiteDto.CUIT + ".pdf");
                        Response.BinaryWrite(_p_response.Documentacion.Imagen_Documentacion);
                        Response.Flush();

                    }
                    else if (_p_response.Documentacion.Extension.ToUpper().Equals("DOC") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("DOCX") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("XLS") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("XLSX") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("RAR") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("ZIP"))
                    {
                        _p_response.Documentacion.Imagen_Documentacion =
                            objCryptoHash.Descifrar_Datos(_p_response.Documentacion.Imagen_Documentacion, out mensaje);

                        File.WriteAllBytes(pathImg, _p_response.Documentacion.Imagen_Documentacion);
                        divMensajeExito.Visible = true;
                        lblMensajeExito.Text = "El documento se descargó con éxito en " + pathImg;

                    }
                    else if (_p_response.Documentacion.Extension.ToUpper().Equals("PNG") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("JPG") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("JPEG") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("BMP") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("GIF") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("TIFF") ||
                             _p_response.Documentacion.Extension.ToUpper().Equals("TIF"))
                    {
                        _p_response.Documentacion.Imagen_Documentacion =
                            objCryptoHash.Descifrar_Datos(_p_response.Documentacion.Imagen_Documentacion, out mensaje);

                        iTextSharp.text.Image image =
                            iTextSharp.text.Image.GetInstance(_p_response.Documentacion.Imagen_Documentacion);

                        using (FileStream fs = new FileStream(pathImg, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            using (iTextSharp.text.Document doc = new Document(image))
                            {
                                doc.SetPageSize(new iTextSharp.text.Rectangle(0, 0, image.Width, image.Height, 0));
                                doc.NewPage();

                                using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                                {
                                    doc.Open();
                                    image.SetAbsolutePosition(0, 0);
                                    writer.DirectContent.AddImage(image);
                                    doc.Close();
                                }
                            }
                        }

                        var arrDocImg = File.ReadAllBytes(pathImg);

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + RequestPost.N_Documento + ".pdf");
                        Response.BinaryWrite(arrDocImg);
                        Response.Flush();
                    }
                }
                else
                {
                    Response.Write("<script>alert('Imagen nula!');</script>");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

        }
        internal CDDResponseConsulta Get_Document_Web_Api_CDD()
        {
            var respuesta = new CDDResponseConsulta();
            var cddMapError = new CDDMapError();

            try
            {
                respuesta = MapeadorWebApi.ConsumirWebApi<CDDPost, CDDResponseConsulta>(
                    MapeadorWebApi.Obtener_Documento, RequestPost);
            }
            catch (CDDException waEx)
            {
                respuesta.Codigo_Resultado = waEx.ErrorCode;
                respuesta.Detalle_Resultado = waEx.ErrorDescription;
            }
            catch (TimeoutException ex)
            {
                cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

                respuesta.Codigo_Resultado = cddMapError.Codigo_WA_Error;
                respuesta.Detalle_Resultado = cddMapError.Descripcion_WA_Error + " | Descripcion: TimeOut. " +
                                              ex.StackTrace;
            }
            catch (WebException ex)
            {
                cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

                respuesta.Codigo_Resultado = cddMapError.Codigo_WA_Error;
                respuesta.Detalle_Resultado = cddMapError.Descripcion_WA_Error + " | Descripcion: WebException." +
                                              ex.StackTrace;
            }
            catch (Exception ex)
            {
                cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

                respuesta.Codigo_Resultado = cddMapError.Codigo_WA_Error;
                respuesta.Detalle_Resultado = cddMapError.Descripcion_WA_Error + " | Descripcion: Exception." +
                                              ex.StackTrace;
            }

            return respuesta;
        }

        internal CDDResponseLogin Get_Authorize_Web_Api_CDD()
        {
            var _cddRespLogin = new CDDResponseLogin();
            var _cddMapError = new CDDMapError();

            try
            {
                Initialize_Authorize();

                _cddRespLogin =
                    MapeadorWebApi.ConsumirWebApi<Autorizador, CDDResponseLogin>(MapeadorWebApi.Autorizar_Solicitud,
                        ObjAutorizador);

                if (_cddRespLogin != null)
                {
                    if (_cddRespLogin.Codigo_Resultado.Equals("SEO"))
                        Complete_Authorize(_cddRespLogin.Llave_BLOB_Login);
                }
            }
            catch (CDDException waEx)
            {
                _cddRespLogin.Codigo_Resultado = waEx.ErrorCode;
                _cddRespLogin.Detalle_Resultado = waEx.ErrorDescription;
            }
            catch (TimeoutException ex)
            {
                _cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

                _cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
                _cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: TimeOut. " +
                                                  ex.StackTrace;
            }
            catch (WebException ex)
            {
                _cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

                _cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
                _cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: WebException." +
                                                  ex.StackTrace;
            }
            catch (Exception ex)
            {
                _cddMapError = new CDDMapError(EnumCDDError.FATAL_END_OPERATION_EXCEPTION.GetValue<int>());

                _cddRespLogin.Codigo_Resultado = _cddMapError.Codigo_WA_Error;
                _cddRespLogin.Detalle_Resultado = _cddMapError.Descripcion_WA_Error + " | Descripcion: Exception." +
                                                  ex.StackTrace;
            }

            return _cddRespLogin;
        }
        internal bool Set_Parameters_Request_Post(byte[] _p_Blob_Key)
        {
            bool _continuar = true;
            RequestPost = new CDDPost();

            RequestPost.Id_Aplicacion_Origen = ObjAutorizador.Id_Aplicacion_Origen;
            RequestPost.Pwd_Aplicacion = ObjAutorizador.Pwd_Aplicacion;
            RequestPost.IdUsuario = ObjAutorizador.Id_Usuario;
            RequestPost.Shared_Key = _p_Blob_Key;
            RequestPost.Id_Documento = IdDocumentoCDD;
            RequestPost.N_Documento = "Documentacion_" + tramiteDto.CUIT;

            /* 
			 * Asignar el valor del Tipo de Documento en el Web.config según la asignacion de permisos.
			 * Solicitar los permisos a DesarrolloCiDi@cba.gov.ar
			 */
            if (ConfigurationManager.AppSettings["Id_Tipo_Documento"] != null)
                RequestPost.Id_Catalogo = Convert.ToInt32(MapeadorWebApi.Id_Tipo_Documento);
            else
                RequestPost.Id_Catalogo = 0;

            return _continuar;
        }
        private void Initialize_Authorize()
        {
            ObjDiffieHellman = new CryptoDiffieHellman();
            ObjAutorizador = new Autorizador();

            var _helper = new Helper();
            var _cmd = new CryptoManagerData();

            String _mensaje = String.Empty;

            try
            {
                ObjAutorizador.Id_Aplicacion_Origen = Convert.ToInt32(MapeadorWebApi.Id_Aplicacion_Origen);
                ObjAutorizador.Pwd_Aplicacion = MapeadorWebApi.Pwd_Aplicacion_Origen;
                ObjAutorizador.Key_Aplicacion = MapeadorWebApi.Key_Aplicacion_Origen;
                ObjAutorizador.Operacion = "1"; // 1 = Consulta
                ObjAutorizador.Id_Usuario = MapeadorWebApi.User_Aplicacion_Origen;
                ObjAutorizador.Time_Stamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                try
                {
                    ObjAutorizador.Token = _cmd.Get_Token(ObjAutorizador.Time_Stamp, ObjAutorizador.Key_Aplicacion);

                    if (!string.IsNullOrEmpty(_mensaje)) throw new Exception(_mensaje);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // Creo la llave CNG
                Private_Key = ObjDiffieHellman.Create_Key_ECCDHP521();

                // Creo la llave Blob pública
                ObjAutorizador.Public_Blob_Key = ObjDiffieHellman.Export_Key_Material();

                // Llave compartida
                ObjAutorizador.Shared_Key = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Complete_Authorize(byte[] pPublicBlobKey)
        {
            try
            {
                // Creo la llave compartida
                ObjAutorizador.Shared_Key = ObjDiffieHellman.Share_Key_Generate(pPublicBlobKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void chkFiltroFecha_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkFiltroFecha.Checked)
            {
                divFiltroFecha.Visible = true;
            }

            if (!chkFiltroFecha.Checked)
            {
                divFiltroFecha.Visible = false;
            }

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

        protected void btnCerrarMapa_OnClick(object sender, EventArgs e)
        {
            MostrarOcultardivModalMapa(false);
        }

        protected void ObtenerTasas()
        {
            //var listaTRSUsadas = Bl.BlGetTasasAsignadas(tramiteDto.NroTramiteSifcos);
            //if (listaTRSUsadas.Rows.Count > 0)
            //{
            //    GVTrsUsadas.DataSource = listaTRSUsadas;
            //    GVTrsUsadas.DataBind();
            //    lblSinResultado.Text = "";
            //}
            //else
            //{
            //    GVTrsUsadas.DataSource = null;
            //    GVTrsUsadas.DataBind();
            //    lblSinResultado.Text = "No tiene tasas asociadas al trámite.";
            //}
        }

        protected void rbAlquiler_OnCheckedChanged(object sender, EventArgs e)
        {
            if (rbPropietario.Checked)
            {
                divMostrarAlquiler.Visible = false;
            }

            if (rbInquilino.Checked)
            {
                divMostrarAlquiler.Visible = true;
            }

        }
    }
}