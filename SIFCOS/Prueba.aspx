<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Prueba.aspx.cs" Inherits="SIFCOS.Prueba" EnableEventValidation="false"
    UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="BotonUnClick" Namespace="BotonUnClick" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <%--<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCuxWCu971i-L-O1ui-jzI72cX1Tjr1kwU&v=3.exp&sensor=false&libraries=places"></script>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">

    <div class="portlet-body form">
        <!-- BEGIN FORM-->
        <div class="form-body">
            <%--<asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>--%>
                    <%--MODAL RESULTADO --%>
                    <div id="divModalResultado" runat="server" class="w3-modal">
                        <div class="w3-modal-content w3-animate-top w3-card-8">
                            <header class="w3-container w3-teal">
                        <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                            <li style="float: left;">
                                <h3>RESULTADO</h3>
                            </li>
                            <li style="float: right;">
                                <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                            </li>   
                        </ul>
                    </header>
                            <div class="w3-container">
                                <br />
                                <div class="row">
                                    <div class="alert alert-warning alert-dismissable">
                                        <h4>
                                            <asp:Label ID="lblResultadoPrueba" runat="server" Text=""></asp:Label>
                                        </h4>
                                    </div>
                                </div>
                                <br />
                            </div>
                            <footer class="w3-container w3-teal">
                        <asp:Button runat="server" ID="btnSalirPrueba"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="SALIR" OnClick="btnSalirPrueba_OnClick"/>
         
                </footer>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <h3 class="form-section">--- PAGINA AUXILIAR PARA SISTEMAS ---
                            </h3>
                        </div>
                    </div>


                    <div class="form-actions">
                        <asp:ScriptManager runat="server" ID="ScriptManager1">
                        </asp:ScriptManager>
                        <div class="row">
                            <div class="col-md-12">
                                <h4 class="form-section">
                                    <i class="fa fa-arrow-circle-right"></i>OBTENER LOS DATOS USUARIO CIDI INGRESANDO
                                EL CUIL
                                </h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    Ingrese Cuil</label>
                                <asp:TextBox runat="server" ID="txtCuilCIDI" MaxLength="11">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnBuscarCIDI" class="btn   btn-circle" Text="Buscar" runat="server"
                                    OnClick="btnBuscarCIDI_OnClick"></asp:Button>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <label>
                                    APELLIDO, NOMBRE</label>
                                <asp:TextBox runat="server" ID="txtNomYApeCIDI"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label>
                                    CELULAR</label>
                                <asp:TextBox runat="server" ID="txtCelularCIDI"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label>
                                    Email</label>
                                <asp:TextBox runat="server" ID="txtEmailCIDI" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4">
                                <label>
                                    Calle</label>
                                <asp:TextBox runat="server" ID="txtCalleCIDI"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    Altura</label>
                                <asp:TextBox runat="server" ID="txtAlturaCIDI"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label>
                                    Departamento</label>
                                <asp:TextBox runat="server" ID="txtDeptoCIDI" Width="200px"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label>
                                    Localidad</label>
                                <asp:TextBox runat="server" ID="txtLocalidadCIDI" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <h4 class="form-section">
                                    <i class="fa fa-arrow-circle-right"></i>OBTENER LOS DATOS DE LAS PERSONAS CON EL
                                APELLIDO Y NOMBRE
                                </h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>
                                    Ingrese NOMBRE</label>
                                <asp:TextBox runat="server" ID="txtNombreRCIVIL">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label>
                                    Ingrese APELLIDO</label>
                                <asp:TextBox runat="server" ID="txtApellidoRCIVIL">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnBuscarRCIVIL" class="btn   btn-circle" Text="Buscar" runat="server"
                                    OnClick="btnBuscarRCIVILNOMYAPE_OnClick"></asp:Button>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnLimpiarPantalla2" class="btn   btn-circle" Text="Limpiar Pantalla"
                                    runat="server" OnClick="btnLimpiarPantalla2_OnClick"></asp:Button>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="text-align: center">
                                <asp:Label runat="server" ID="lblResultadoNomYApeRcivil">
                                </asp:Label>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GVRCIVIL" Style="overflow: scroll;" CssClass="table table-striped table-bordered table-advance table-hover"
                                    runat="server" AllowPaging="True" DataKeyNames="NRO_DOCUMENTO" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CUIL" HeaderText="CUIL" />
                                        <asp:BoundField DataField="ID_SEXO" HeaderText="ID_SEXO" />
                                        <asp:BoundField DataField="NRO_DOCUMENTO" HeaderText="NRO_DOCUMENTO" />
                                        <asp:BoundField DataField="PAI_COD_PAIS" HeaderText="PAI_COD_PAIS" />
                                        <asp:BoundField DataField="ID_NUMERO" HeaderText="ID_NUMERO" />
                                        <asp:BoundField DataField="NOV_NOMBRE" HeaderText="NOV_NOMBRE" />
                                        <asp:BoundField DataField="NOV_APELLIDO" HeaderText="NOV_APELLIDO" />
                                        <asp:BoundField DataField="FECHA_NACIMIENTO" HeaderText="FECHA_NACIMIENTO" />
                                        <asp:BoundField DataField="ID_TIPO_DOCUMENTO" HeaderText="ID_TIPO_DOCUMENTO" />
                                        <asp:BoundField DataField="PAI_COD_PAIS_ORIGEN" HeaderText="PAI_COD_PAIS_ORIGEN" />
                                        <asp:BoundField DataField="PAI_COD_PAIS_TD" HeaderText="PAI_COD_PAIS_TD" Visible="False" />
                                        <asp:BoundField DataField="N_TIPO_DOCUMENTO" HeaderText="N_TIPO_DOCUMENTO" />
                                        <asp:BoundField DataField="NOV_FUENTE_INFO" HeaderText="NOV_FUENTE_INFO" Visible="False" />
                                        <asp:BoundField DataField="FECHA_DEFUNCION" HeaderText="FECHA_DEFUNCION" Visible="False" />
                                        <asp:BoundField DataField="PAI_COD_PAIS_NACIONALIDAD" HeaderText="PAI_COD_PAIS_NACIONALIDAD" />
                                        <asp:BoundField DataField="ID_ESTADO_CIVIL" HeaderText="ID_ESTADO_CIVIL" Visible="False" />
                                        <asp:BoundField DataField="LOC_ID_LOCALIDAD" HeaderText="LOC_ID_LOCALIDAD" />
                                        <asp:BoundField DataField="FEC_ACTUALIZACION" HeaderText="FEC_ACTUALIZACION" Visible="False" />
                                        <asp:BoundField DataField="PK_RENAPER" HeaderText="PK_RENAPER" Visible="False" />
                                        <asp:BoundField DataField="FALLECIDO" HeaderText="FALLECIDO" Visible="False" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <h4 class="form-section">
                                    <i class="fa fa-arrow-circle-right"></i>CARGA DE CUIL EN RCIVIL
                                </h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label>
                                    DNI</label><br />
                                <asp:TextBox runat="server" ID="txtDNIPersona">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label>
                                    Sexo</label><br />
                                <asp:TextBox runat="server" ID="txtIdSexo">
                                </asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <asp:Button ID="btnConsultaDNI" class="btn   btn-circle" Text="Consultar en RCIVIL"
                                        runat="server" OnClick="btnConsultaDNI_OnClick"></asp:Button>
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnLimpiarPantalla" class="btn   btn-circle" Text="Limpiar Pantalla"
                                        runat="server" OnClick="btnLimpiarPantalla_OnClick"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="text-align: center">
                                <asp:Label runat="server" ID="lblResConsultaRCIVIL">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GVPersonasRcivil" Style="overflow: scroll;" CssClass="table table-striped table-bordered table-advance table-hover"
                                    runat="server" AllowPaging="True" DataKeyNames="NRO_DOCUMENTO" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CUIL" HeaderText="CUIL" />
                                        <asp:BoundField DataField="ID_SEXO" HeaderText="ID_SEXO" />
                                        <asp:BoundField DataField="NRO_DOCUMENTO" HeaderText="NRO_DOCUMENTO" />
                                        <asp:BoundField DataField="PAI_COD_PAIS" HeaderText="PAI_COD_PAIS" />
                                        <asp:BoundField DataField="ID_NUMERO" HeaderText="ID_NUMERO" />
                                        <asp:BoundField DataField="NOV_NOMBRE" HeaderText="NOV_NOMBRE" />
                                        <asp:BoundField DataField="NOV_APELLIDO" HeaderText="NOV_APELLIDO" />
                                        <asp:BoundField DataField="FECHA_NACIMIENTO" HeaderText="FECHA_NACIMIENTO" />
                                        <asp:BoundField DataField="ID_TIPO_DOCUMENTO" HeaderText="ID_TIPO_DOCUMENTO" />
                                        <asp:BoundField DataField="PAI_COD_PAIS_ORIGEN" HeaderText="PAI_COD_PAIS_ORIGEN" />
                                        <asp:BoundField DataField="PAI_COD_PAIS_TD" HeaderText="PAI_COD_PAIS_TD" Visible="False" />
                                        <asp:BoundField DataField="N_TIPO_DOCUMENTO" HeaderText="N_TIPO_DOCUMENTO" Visible="False" />
                                        <asp:BoundField DataField="NOV_FUENTE_INFO" HeaderText="NOV_FUENTE_INFO" Visible="False" />
                                        <asp:BoundField DataField="FECHA_DEFUNCION" HeaderText="FECHA_DEFUNCION" Visible="False" />
                                        <asp:BoundField DataField="PAI_COD_PAIS_NACIONALIDAD" HeaderText="PAI_COD_PAIS_NACIONALIDAD" />
                                        <asp:BoundField DataField="ID_ESTADO_CIVIL" HeaderText="ID_ESTADO_CIVIL" Visible="False" />
                                        <asp:BoundField DataField="LOC_ID_LOCALIDAD" HeaderText="LOC_ID_LOCALIDAD" Visible="False" />
                                        <asp:BoundField DataField="FEC_ACTUALIZACION" HeaderText="FEC_ACTUALIZACION" Visible="False" />
                                        <asp:BoundField DataField="PK_RENAPER" HeaderText="PK_RENAPER" Visible="False" />
                                        <asp:BoundField DataField="FALLECIDO" HeaderText="FALLECIDO" Visible="False" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <h4 class="form-section">
                                    <i class="fa fa-arrow-circle-right"></i>GENERAR EL CUIL A LA PERSONA INGRESADA
                                </h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <label>
                                    CUIL (SIN GUIONES)</label><br />
                                <asp:TextBox runat="server" ID="txtCUILG" MaxLength="11">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    DNI</label><br />
                                <asp:TextBox runat="server" ID="txtDNIG">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    SEXO [M:01/F:02]</label><br />
                                <asp:TextBox runat="server" ID="txtSEXOG">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    ID_NUMERO (DEF 0)</label><br />
                                <asp:TextBox runat="server" ID="txtID_NUMEROG" Text="0">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label>
                                    PAIS (DEF ARG)</label><br />
                                <asp:TextBox runat="server" ID="txtPaisG" Text="ARG">
                                </asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <asp:Button ID="InsertaCUIL" class="btn   btn-circle" Text="insertar Cuil" runat="server"
                                        OnClick="InsertaCUIL_OnClick"></asp:Button>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <asp:Button ID="btnLimpiarDatos" class="btn   btn-circle" Text="Limpiar Generar CUIL"
                                            runat="server" OnClick="btnLimpiarDatos_OnClick"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12" style="text-align: center">
                                <asp:Label runat="server" ID="lblResultadoGenCUIL">
                                </asp:Label>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <h4 class="form-section">
                                    <i class="fa fa-arrow-circle-right"></i>PROCESO PARA CAMBIAR UN ESTADO DE VERIFICADO
                                A RECHAZADO Y VICEVERSA
                                </h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label>
                                    NRO TRAMITE</label><br />
                                <asp:TextBox runat="server" ID="txtNroTramiteEliminarEstado">
                                </asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button ID="btnActEstadoVerificado" class="btn   btn-circle" Text="Cambiar Estado a Verificado Boca"
                                    runat="server" OnClick="btnActEstadoVerificado_OnClick"></asp:Button>
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnActEstadoRechazado" class="btn   btn-circle" Text="Cambiar Estado a Rechazado Boca"
                                    runat="server" OnClick="btnActEstadoRechazado_OnClick"></asp:Button>
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnActEstadoVerMinisterio" class="btn   btn-circle" Text="Cambiar Estado a Verificado Ministerio"
                                    runat="server" OnClick="btnActEstadoVerMinisterio_OnClick"></asp:Button>
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnActEstadoRechMinisterio" class="btn   btn-circle" Text="Cambiar Estado a Rechazado Ministerio"
                                    runat="server" OnClick="btnActEstadoRechMinisterio_OnClick"></asp:Button>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Button ID="btnLimpiarEliminarEstado" class="btn   btn-circle" Text="Limpiar datos"
                                    runat="server" OnClick="btnLimpiarEliminarEstado_OnClick"></asp:Button>
                            </div>
                        </div>
                    </div>
                    
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Label runat="server" ID="lblResultadoEliminarEstado">
                            </asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>CAMBIAR TITULARIDAD A LAS TASAS
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>
                                NRO LIQUIDACION</label><br />
                            <asp:TextBox runat="server" ID="txtTRSNroLiq">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label>
                                CUIL (SIN GUIONES)</label><br />
                            <asp:TextBox runat="server" ID="txtTRSCUIL">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label>
                                DNI</label><br />
                            <asp:TextBox runat="server" ID="txtTRSDNI">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label>
                                SEXO [M:01/F:02]</label><br />
                            <asp:TextBox runat="server" ID="txtTRSSexo">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Button ID="btnCambiarTitular" class="btn   btn-circle" Text="Cambiar Titular"
                                    runat="server" OnClick="btnCambiarTitular_OnClick"></asp:Button>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <asp:Button ID="btnLimpiarDatos2" class="btn   btn-circle" Text="Limpiar Cambiar Titular"
                                        runat="server" OnClick="btnLimpiarDatos2_OnClick"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Label runat="server" ID="lblResultadoCambiarTitularTRS">
                            </asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>GENERAR EL CUIT Y RAZON SOCIAL EN BASE DE
                            DATOS
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>
                                CUIT (SIN GUIONES) (OBLIGATORIO)</label><br />
                            <asp:TextBox runat="server" ID="txtCUITIPJ" MaxLength="11">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label>
                                RAZON SOCIAL (OBLIGATORIO)</label><br />
                            <asp:TextBox runat="server" ID="txtRazonSocialIPJ">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label>
                                NRO INGRESO BRUTO (OPCIONAL)</label><br />
                            <asp:TextBox runat="server" ID="txtIngBrutoIPJ">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <asp:Button ID="btnGenerarRazonSocial" class="btn   btn-circle" Text="Generar CUIT/CUIL"
                                    runat="server" OnClick="btnGenerarRazonSocial_OnClick"></asp:Button>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <asp:Button ID="btnLimpiarDatosRazonSocial" class="btn   btn-circle" Text="Limpiar Generar RAZON SOCIAL"
                                        runat="server" OnClick="btnLimpiarDatosRazonSocial_OnClick"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Label runat="server" ID="lblResultadoGenerarRazonSocial">
                            </asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>ELIMINAR UN TRAMITE A PARTIR DEL NRO TRAMITE
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>
                                NRO TRAMITE</label><br />
                            <asp:TextBox runat="server" ID="txtNroTramiteElim"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Button ID="btnEliminarTramite" class="btn   btn-circle" Text="ELIMINAR TRAMITE"
                                runat="server" OnClick="btnEliminarTramite_OnClick"></asp:Button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Label runat="server" ID="lblResultadoEliminarTramite">
                            </asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>LIMPIAR CAMPO NOTIFICACIONES (RESENIA)
                            </h4>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Button ID="btnEliminarNotificaciones" class="btn   btn-circle" Text="ELIMINAR NOTIFICACIONES"
                                        runat="server" OnClick="btnEliminarNotificaciones_OnClick"></asp:Button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Label runat="server" ID="Label1">
                            </asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>ASIGNAR UN NUMERO SIFCOS A UN TRAMITE
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>
                                NRO SIFCOS</label><br />
                            <asp:TextBox runat="server" ID="txtNroSifcosAsig"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="txtNroSifcosAsig_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtNroSifcosAsig">
                            </Ajx:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-3">
                            <label>
                                NRO TRAMITE</label><br />
                            <asp:TextBox runat="server" ID="txtNroTramiteAsig"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="txtNroTramiteAsig_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtNroTramiteAsig">
                            </Ajx:FilteredTextBoxExtender>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Button ID="btnAsinarNroSifcos" class="btn   btn-circle" Text="ASIGNAR NRO SIFCOS"
                                runat="server" OnClick="btnAsignarNroSifcos_OnClick"></asp:Button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Label runat="server" ID="lblResultadoAsignarNroSifcos">
                            </asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>ASIGNAR UN RESPONSABLE LEGAL A UNA EMPRESA HASTA 10 GESTORES PERMITE
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label>
                                USUARIO CIDI</label><br />
                            <asp:TextBox runat="server" ID="txtCuilUsuarioResp"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="txtCuilUsuarioResp_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtCuilUsuarioResp">
                            </Ajx:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-3">
                            <label>
                                CUIT EMPRESA</label><br />
                            <asp:TextBox runat="server" ID="txtCuitEmpresa"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="txtCuitEmpresa_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtCuitEmpresa">
                            </Ajx:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-3">
                            <label>
                                ROL A ASIGNAR</label><br />
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRolAsignar">
                                    <asp:ListItem Text="SELECCIONE UN ROL" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 8" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 9" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="GESTOR 10" Value="10"></asp:ListItem>
                             </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Button ID="btnAsignarResponsable" class="btn   btn-circle" Text="ASIGNAR RESPONSABLE"
                                runat="server" OnClick="btnAsignarResponsable_OnClick"></asp:Button>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnQuitarResponsable" class="btn   btn-circle" Text="ANULAR RESPONSABLE"
                                runat="server" OnClick="btnQuitarResponsable_OnClick"></asp:Button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Label runat="server" ID="lblResultadoAsignarResponsable">
                            </asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>CONSULTAR TRAMITES DE UN COMERCIO POR CUIT
                            O NRO SIFCOS
                            </h4>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            <label>
                                CUIT del Comercio:</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCuit" MaxLength="11"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="txtCuit_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtCuit">
                            </Ajx:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-2">
                            <label>
                                NRO SIFCOS:</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNroSifcosC" MaxLength="10"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="txtNroSifcosC_FilteredTextBoxExtender" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txtNroSifcosC">
                            </Ajx:FilteredTextBoxExtender>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button ID="btnConsultar" Text="Consultar Comercio" runat="server" CssClass="form-control btn"
                                OnClick="btnConsultarDirecciones_OnClick" />
                        </div>
                    </div>
                    <br />
                    <div id="divResultadoEstadoComercio" runat="server" class="note note-info">
                        <h4 class="block">Información</h4>
                        <p>
                            <asp:Label runat="server" ID="lblResultadoEstadoComercio"></asp:Label>
                        </p>
                    </div>
                    <div class="row" id="divResultadoDirecciones" runat="server">
                        <div class="row" id="TITULO" runat="server">
                            <div class="col-md-12" style="text-align: center">
                                <h4>TRAMITES REALIZADOS SEGUN CUIT O NRO SIFCOS:</h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvDirecciones" Style="overflow: scroll;" runat="server" CssClass="table table-striped table-bordered table-advance table-hover"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="5" DataKeyNames="Nro_Tramite"
                                    OnPageIndexChanging="gvDirecciones_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="Nro_Tramite" HeaderText="NRO TRAMITE" />
                                        <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                                        <asp:BoundField DataField="Nro_Sifcos" HeaderText="NRO SIFCOS" />
                                        <asp:BoundField DataField="Boca_recepcion" HeaderText="BOCA RECEPCION" />
                                        <asp:BoundField DataField="fec_vencimiento" HeaderText="FEC VTO" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="domicilio" HeaderText="DOMICILIO" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <h6>
                                    <asp:Label runat="server" ID="lblTitulocantRegistros" Text="Total de registros encontrados : "></asp:Label><asp:Label
                                        runat="server" ID="lblTotalRegistrosGrilla" Text="0"></asp:Label></h6>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="pagination pull-right">
                                    <asp:Repeater ID="rptBotonesPaginacion" OnItemDataBound="rptBotonesPaginacion_OnItemDataBound"
                                        OnItemCommand="rptBotonesPaginacion_OnItemCommand" runat="server">
                                        <ItemTemplate>
                                            <li class="paginate_button">
                                                <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_OnClick" CommandArgument='<%# Bind("nroPagina") %>'
                                                    runat="server" class="btn btn-default "><%# Eval("nroPagina")%></asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="row">
                    <div class="col-md-12">
                        <h4 class="form-section">
                            <i class="fa fa-arrow-circle-right"></i>CONSULTAS A BASE
                        </h4>
                    </div>
                </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label>
                                CONSULTA SQL</label><br />
                            <asp:TextBox runat="server" ID="txtSQL" Width="1000px" Rows="5" TextMode="MultiLine">
                            </asp:TextBox>
                        </div>
                    </div>
                    <br />
                        <div class="row">
                            <div class="col-md-6">
                                <label>BASE DE DATOS:</label>
                                <asp:DropDownList ID="ddlBaseDatos" CssClass="form-control select2me" runat="server">
                                    <asp:ListItem Text="SELECCIONE UNA OPCION" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="INDUSTRIA" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="SIFCOS" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="RUAMI" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="PROMIND" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="ALIMENTOS" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="SIGAL" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="PERSONAL" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="CONTROLMICYM" Value="8"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Button ID="btnConsultaSQL" class="btn   btn-circle" Text="CONSULTAR" runat="server"
                                OnClick="btnConsultaSQL_OnClick"></asp:Button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div id="divMensajeErrorSQL" runat="server" class="alert alert-danger alert-dismissable" visible="False">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                </button>
                                <strong>Error! </strong>
                                <asp:Label runat="server" ID="lblMensajeErrorSQL"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="overflow: scroll;">
                            <label>
                                RESULTADO</label>
                            <asp:GridView ID="GVResultadoSQL" CssClass="table table-striped table-bordered table-advance table-hover"
                                runat="server" AutoGenerateColumns="True">
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h6>
                                <asp:Label runat="server" ID="lblRegistrosSQL" Text="Total de registros encontrados : "></asp:Label><asp:Label
                                    runat="server" ID="lblCantRegistrosSQL" Text="0"></asp:Label></h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Button ID="btnLimpiarSQL" class="btn   btn-circle" Text="Limpiar consulta" runat="server"
                                OnClick="btnLimpiarSQL_OnClick"></asp:Button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>OBTENER CONCEPTOS VIGENTES
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">

                            <asp:GridView ID="grdConceptos" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="id_concepto" HeaderText="id_concepto" />
                                    <asp:BoundField DataField="fec_desde" HeaderText="fec_desde" />
                                    <asp:BoundField DataField="fec_hasta" HeaderText="fec_hasta" />
                                    <asp:BoundField DataField="precio_base" HeaderText="precio_base" />
                                    <asp:BoundField DataField="NTipoConcepto" HeaderText="NTipoConcepto" />
                                </Columns>
                            </asp:GridView>
                            <br />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnGetConceptos" class="btn   btn-circle"
                                Text="Buscar conceptos vigentes" runat="server" OnClick="btnGetConceptos_Click"></asp:Button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>OBTENER URL TRS
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">

                            <asp:Label ID="lbltrs" runat="server" Text="..."></asp:Label>

                            <br />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnGetUrlTrs" class="btn   btn-circle"
                                Text="Obtener Url Trs" runat="server" OnClick="btnGetUrlTrs_Click"></asp:Button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h4 class="form-section">
                                <i class="fa fa-arrow-circle-right"></i>Generar trs
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtNroTramite" runat="server"></asp:TextBox>
                            <asp:Label ID="lblretornoTrs" runat="server" Text="Retorno: "></asp:Label>
                            <asp:Label ID="lblnrotransacTrs" runat="server" Text="Nro Transaccion"></asp:Label>
                            <asp:Label ID="lblHashTrs" runat="server" Text="Hash"></asp:Label>
                            <br />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnGenerarTrs" class="btn   btn-circle"
                                Text="Obtener Url Trs" runat="server" OnClick="btnGenerarTrs_Click"></asp:Button>
                        </div>
                    </div>
               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
            <!-- END FORM-->
            
        </div>
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
    <%--<script src="Scripts/mapaSifcos.js" type="text/javascript"></script>--%>
    <script language="javascript" type="text/javascript">

        $("#ConenedorPrincipal_Button3").click(function () {
            $('#ConenedorPrincipal_Button3').attr('disabled', true);
        });


        function ace1_itemSelected(sender, e) {
            $('#ConenedorPrincipal_ace1Value').val(e.get_value());
        }
        $(function () {
            $(".progress").each(function () {
                $(this).progressbar({
                    value: parseInt($(this).find('.progress-label').text())
                });
            });
        });
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
        $(document).ready(function () {


            //function cargarDeptos() {
            //    $.ajax({
            //        type: "POST",
            //        url: "Prueba.aspx/GetDeptos",
            //        async: false,
            //        data: {},
            //        contentType: "application/json; charset=utf-8",
            //        dataType: "json",
            //        success: function (msg) {
            //            //console.log(msg);
            //            if (msg.d.Options.length > 0) {
            //                oData = msg.d.Options;
            //                $.each(oData, function (i, item) {
            //                    //console.log(item);
            //                    $('#ConenedorPrincipal_ddlDepartamentos').append($('<option>', {
            //                        value: item.Value,
            //                        text: item.DisplayText
            //                    }));
            //                });
            //            }
            //        }
            //    });
            //}

            //function cargarLocalidades(idDepto) {

            //    //Para pasar parametros via object:
            //    var obj = new Object();
            //    obj.pIdDepto = idDepto;

            //    var params = JSON.stringify(obj); //deserealiza el objeto en una cadena de caracteres.

            //    $.ajax({
            //        type: "POST",
            //        url: "Prueba.aspx/GetLocalidades",
            //        async: false,
            //        data: params,
            //        contentType: "application/json; charset=utf-8",
            //        dataType: "json",
            //        success: function (msg) {
            //            console.log(msg);
            //            if (msg.d.Options.length > 0) {
            //                oData = msg.d.Options;

            //                //Vaciar items anteriores:
            //                $('#ddlLocalidades').empty();

            //                $.each(oData, function (i, item) {
            //                    console.log(item);
            //                    $('#ddlLocalidades').append($('<option>', {
            //                        value: item.Value,
            //                        text: item.DisplayText
            //                    }));
            //                });
            //            }
            //        }
            //    });
            //}



            //$("#ConenedorPrincipal_ddlDepartamentos").change(function () {
            //    $("#ConenedorPrincipal_ddlDepartamentos option:selected").each(function () {
            //        elegido = $(this).val();
            //        console.log(elegido);
            //        cargarLocalidades(elegido);

            //        var obj = new Object();
            //        obj.pIdDepto = elegido;
            //        var params = JSON.stringify(obj); //deserealiza el objeto en una cadena de caracteres.

            //        //es necesario setear el control texbox de latitud y long. para tomar los valores del lado del servidor.
            //        $.ajax({
            //            type: "POST",
            //            url: "Prueba.aspx/SetDepartamento",
            //            async: false,
            //            data: params,
            //            contentType: "application/json; charset=utf-8",
            //            dataType: "json"
            //        });
            //    });
            //});

            //cargarDeptos();
            //$("#spanMensajeEmail").hide();
            //$("#ConenedorPrincipal_txtPrueba").focusout(function () {
            //    var emailAddress = $("#ConenedorPrincipal_txtPrueba").val();
            //    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
            //    var rta = pattern.test(emailAddress);
            //    if (rta == false) {
            //        $("#spanMensajeEmail").show();
            //    }
            //    if (rta == true) {
            //        $("#spanMensajeEmail").hide();
            //    }
            //    //                $('#ConenedorPrincipal_txtPrueba').filter(function() {
            //    //                    var emil = $('#ConenedorPrincipal_txtPrueba').val();
            //    //                    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
            //    //                    if (!emailReg.test(emil)) {
            //    //                       
            //    //                    } else {
            //    //                     
            //    //                    }
            //    //                });

            //    $('#_txtDireccion').live("keydown", function (e) {
            //        if (e.keyCode == 13) {
            //            AutocompletarYubicarMarker();
            //            return false; // prevent the button click from happening
            //        }
            //    });




            //});
            //inicializarMapa();
        });




    </script>
</asp:Content>
