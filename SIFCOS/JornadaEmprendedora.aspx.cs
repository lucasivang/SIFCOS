using AjaxControlToolkit;
using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;
using System.Net.Mail;
using System.Net;
using BL_SIFCOS;
using DA_SIFCOS.Entidades;
using DA_SIFCOS;

namespace SIFCOS
{
    public partial class JornadaEmprendedora : System.Web.UI.Page
    {
        public ResultadoRule Res = new ResultadoRule();
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        public static ReglaDeNegocios Bl_static = new ReglaDeNegocios();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                divMensajeError.Visible = false;
                divMensajeExito.Visible = false;
                string sHash = Request.QueryString["key"];
                if (!String.IsNullOrEmpty(sHash))
                {
                    FormInscripcion.Visible = false;
                    Session["sHash"] = sHash;
                    String NroInscripcion = Decrypt(sHash);
                    RegistrarAcceso(NroInscripcion);
                }
                else
                {
                    FormInscripcion.Visible = true;
                    divAcciones.Visible = false;
                }



            }

        }

        protected void ddlFiltroBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlFiltroBusqueda.SelectedValue)
            {
                case "01":
                    divAcciones.Visible = true;
                    divCUIT.Visible = true;
                    divCUIL.Visible = false;
                    break;
                case "02":
                    divCUIT.Visible = false;
                    divCUIL.Visible = true;
                    divAcciones.Visible = true;
                    break;

                case "00":
                    divMensajeError.Visible = true;
                    lblMensajeError.Text = "Debe seleccionar un metodo de busqueda.";
                    break;
            }

            ddlFiltroBusqueda.Enabled = false;

        }

        private void RegistrarAcceso(String NroInscripcion)
        {
            Inscripcion = new List<InscripcionEvento>();
            ResultadoRule Result = new ResultadoRule();
            Inscripcion = Bl.BlConsultaInscripcionByNro(NroInscripcion.Substring(0, 6));
            if (Inscripcion.Count == 0)
            {
                DivAccesoDenegado.Visible = true;
                DivAccesoPermitido.Visible = false;

                return;
            }
            else
            {
                InscripcionEvento Entrada = Inscripcion.ToList()[0];
                var FechaEvento = DateTime.Parse("10/10/2024 18:30:00");
                //var FechaVIP = DateTime.Parse("10/10/2024 17:45:00");
                var FechaActual = DateTime.Now;
                //if (FechaVIP < FechaActual)
                //{
                //    if (Entrada.Estado != "PRESENTE_VIP")
                //    {
                //        Result = Bl.BlRegistrarAccesoVIP(Entrada);
                //        MostrarOcultarModalAcceso(false);
                //    }

                //    else
                //    {
                //        Result.OcurrioError = true;
                //    }

                //    if (Result.OcurrioError)
                //    {

                //        DivAccesoDenegado.Visible = true;
                //        DivAccesoPermitido.Visible = false;
                //        DivAcceso.Visible = false;

                //    }
                //    else
                //    {
                //        DivAccesoDenegado.Visible = false;
                //        DivAccesoPermitido.Visible = true;
                //        DivAcceso.Visible = false;
                //        lblApellidoyNombreAcceso.Text = Entrada.Apellido;
                //        lblDNIAcceso.Text = Entrada.CUIL.Substring(2, 8);
                //        lblAccesoCena.Text = "ACCESO VIP";
                //        lblAccesoCena.Visible = true;
                //    }

                //}
                //else
                //{
                //    MostrarOcultarModalAcceso(true);

                //}
                if (FechaEvento < FechaActual)
                {
                    if (Entrada.Estado != "PRESENTE_INVITADO")
                    {
                        Result = Bl.BlRegistrarAccesoEvento(Entrada);
                        MostrarOcultarModalAcceso(false);
                    }

                    else
                    {
                        Result.OcurrioError = true;
                    }

                    if (Result.OcurrioError)
                    {

                        DivAccesoDenegado.Visible = true;
                        DivAccesoPermitido.Visible = false;
                        DivAcceso.Visible = false;

                    }
                    else
                    {
                        DivAccesoDenegado.Visible = false;
                        DivAccesoPermitido.Visible = true;
                        DivAcceso.Visible = false;
                        lblApellidoyNombreAcceso.Text = Entrada.Apellido + ", " + Entrada.Nombre;
                        lblDNIAcceso.Text = Entrada.CUIL.Substring(3, 8);
                        lblAccesoCena.Text = "";
                        lblAccesoCena.Visible = false;
                    }
                }
                else
                {
                    MostrarOcultarModalAcceso(true);

                }



            }

            MostrarOcultarModalRegistrarIngreso(true);
            divMensajeError.Visible = false;


            ResultadoRule res = new ResultadoRule();

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

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCuit.Text) && ddlFiltroBusqueda.SelectedValue == "01")
            {
                lblMensajeError.Text = "Debe ingresar un CUIT o Razon Social.";
                divMensajeError.Visible = true;
                return;

            }

            if (string.IsNullOrEmpty(txtCUIL.Text) && divCUIL.Visible)
            {
                lblMensajeError.Text = "CUIL es un dato obligatorio.";
                divMensajeError.Visible = true;
                txtCUIL.Focus();
                return;
            }





            var pCuit = txtCuit.Text.Trim();
            var PCuil = txtCUIL.Text.Trim();

            var GetPersona = Bl.BlGetInscripcionByCUIL(PCuil);
            if (GetPersona.Rows.Count == 0)
            {
                if (ddlFiltroBusqueda.SelectedValue == "01" && !string.IsNullOrEmpty(pCuit) && !divCUIL.Visible)
                {
                    var DtEmpresa = Bl.BlGetEmpresaEnRentas(pCuit);
                    foreach (DataRow row in DtEmpresa.Rows)
                    {
                        if (DtEmpresa.Rows.Count > 0)
                        {
                            divResultados.Visible = true;
                            divResultados2.Visible = false;
                            lblCuit.Text = row["CUIT"].ToString().Replace("-", "");
                            lblRazonSocial.Text = row["RAZON_SOCIAL"].ToString();
                            divCUIL.Visible = true;
                            divMensajeError.Visible = false;
                        }
                    }

                    if (DtEmpresa.Rows.Count == 0)
                    {
                        lblMensajeError.Text = "No se encontro el comercio ingresado.";
                        divMensajeError.Visible = true;
                        return;
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(txtCUIL.Text))
                    {
                        divResultados.Visible = false;
                        divResultados2.Visible = false;
                        lblMensajeError.Text = "Debe ingresar un CUIL.";
                        divMensajeError.Visible = true;
                        return;
                    }
                    PersonaFisica Persona = Bl.BlGetPersonasRcivil_CUIL(PCuil);
                    if (string.IsNullOrEmpty(Persona.Apellido) && string.IsNullOrEmpty(Persona.Nombre))
                    {
                        divResultados.Visible = false;
                        divResultados2.Visible = false;
                        txtEmail.Text = "";
                        txtCelular.Text = "";
                        txtCodAreaCel.Text = "";
                        lblMensajeError.Text = "La persona no fue encontrada con ese cuil ingresado.";
                        divMensajeError.Visible = true;
                        return;

                    }

                    lblNombre.Text = Persona.Nombre;
                    lblApellido.Text = Persona.Apellido;
                    lblDNI.Text = Persona.NroDocumento;
                    lblEmail.Text = txtEmail.Text;
                    lblTelefono.Text = "(" + txtCodAreaCel.Text + ")" + txtCelular.Text;
                    divMensajeError.Visible = false;
                    divAcciones.Visible = false;
                    divCUIL.Visible = false;
                    divCUIT.Visible = false;
                    divRegistro.Visible = true;
                    divInscripcion.Visible = true;
                    divResultados2.Visible = true;
                    var NroInscripcion = Bl.BlGetUltimoNroInscripcion();
                    int AuxNro = int.Parse(NroInscripcion.ToString()) + 1;
                    lblInscripcion.Text = AuxNro.ToString();


                }
                if (ddlFiltroBusqueda.SelectedValue == "02")
                {
                    PersonaFisica Persona = Bl.BlGetPersonasRcivil_CUIL(PCuil);
                    if (String.IsNullOrEmpty(Persona.Apellido) && String.IsNullOrEmpty(Persona.Nombre))
                    {
                        lblMensajeError.Text = "Debe ingresar CUIL válido.";
                        divMensajeError.Visible = true;
                        return;
                    }


                    divResultados.Visible = false;
                    divResultados2.Visible = true;
                    lblNombre.Text = Persona.Nombre;
                    lblApellido.Text = Persona.Apellido;
                    lblDNI.Text = Persona.NroDocumento;
                    divAcciones.Visible = false;
                    divCUIL.Visible = false;
                    divCUIT.Visible = false;
                    divRegistro.Visible = true;
                    divInscripcion.Visible = true;
                }
                btnImprimir.Visible = false;
            }
            else
            {
                foreach (DataRow row in GetPersona.Rows)
                {
                    //if (!string.IsNullOrEmpty(row["FECHA_ALTA"].ToString()))
                    //{
                    //    btnImprimir2.Visible = true;
                    //}
                    lblNombre.Text = row["NOMBRE_FANTASIA"].ToString();
                    lblDNI.Text = row["CUIT_PERS_JURIDICA"].ToString().Substring(2, 8);
                    if (!string.IsNullOrEmpty(row["NRO_DGR"].ToString()))
                    {
                        divResultados.Visible = true;
                        lblRazonSocial.Text = row["RAZON_SOCIAL"].ToString();
                        lblCuit.Text = row["NRO_DGR"].ToString();
                    }
                    ResultadoRule result = new ResultadoRule();
                    var contactos = Bl.BlConsultaContactosEstab(row["id_entidad"].ToString(), out result);
                    if (contactos != null)
                    {
                        if (contactos.Count > 0)
                        {
                            foreach (var contacto in contactos)
                            {
                                if (contacto.IdTipoComunicacion == "11" &&
                                    (contacto.TablaOrigen == "T_ENTIDADES_DIA_COMERCIO"))
                                {
                                    lblEmail.Text = contacto.NroMail;

                                }
                                if (contacto.IdTipoComunicacion == "07" &&
                                    (contacto.TablaOrigen == "T_ENTIDADES_DIA_COMERCIO"))
                                {
                                    lblTelefono.Text = "(" + contacto.CodArea + ")" + contacto.NroMail;

                                }

                            }

                        }

                    }

                }

                divResultados2.Visible = true;
                divCUIT.Visible = false;
                divCUIL.Visible = false;
                divAcciones.Visible = false;
                divRegistro.Visible = true;
                btnRegistrar.Enabled = false;
                btnImprimir.Visible = true;
                divMensajeExito.Visible = true;
                lblMensajeExito.Text = "Ya se encuentra registrado en el sistema, puede imprimir la entrada si no la posee.";
            }






        }

        private void MostrarOcultarModalRegistrarIngreso(bool mostrar)
        {

            if (mostrar)
            {
                var classname = "mostrarModal";
                string[] listaStrings = divModalRegistrarIngreso.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                );
                divModalRegistrarIngreso.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                divModalRegistrarIngreso.Attributes.Add("class", String.Join(" ", divModalRegistrarIngreso
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "mostrarModal" })
                    .ToArray()
                ));
            }

        }

        private void MostrarOcultarModalAcceso(bool mostrar)
        {

            if (mostrar)
            {
                var classname = "mostrarModal";
                string[] listaStrings = DivAcceso.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                );
                DivAcceso.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                DivAcceso.Attributes.Add("class", String.Join(" ", DivAcceso
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "mostrarModal" })
                    .ToArray()
                ));
            }

        }

        protected void btnLimpiarFiltros_OnClick(object sender, EventArgs e)
        {
            txtCuit.Text = "";
            lblRazonSocial.Text = "";
            txtCelular.Text = "";
            lblEmail.Text = "";
            txtEmail.Text = "";
            txtCodAreaCel.Text = "";
            txtCelular.Text = "";
            btnConsultar.Enabled = true;
            txtCuit.Enabled = true;
            txtCUIL.Text = "";
            ddlFiltroBusqueda.Enabled = true;
            divAcciones.Visible = false;
            divRegistro.Visible = false;
            divCUIT.Visible = false;
            divCUIL.Visible = false;
            divResultados.Visible = false;
            divResultados2.Visible = false;
            divInscripcion.Visible = false;
            divMensajeError.Visible = false;
            divMensajeExito.Visible = false;
            ddlFiltroBusqueda.SelectedValue = "00";

            btnRegistrar.Enabled = true;

        }

        protected void btnRegistrar_OnClick(object sender, EventArgs e)
        {
            InscripcionEvento Obj = new InscripcionEvento();
            Obj.Nombre = lblNombre.Text;
            Obj.Apellido = lblApellido.Text;
            Obj.NroInscripcion = lblInscripcion.Text;
            Obj.Razon_Social = lblRazonSocial.Text;
            Obj.CUIT = lblCuit.Text;
            Obj.CUIL = txtCUIL.Text;
            Obj.VIP = "NO";
            var GetPersona = Bl.BlGetInscripcionByCUIL(Obj.CUIL);
            if (GetPersona.Rows.Count == 0)
            {
                //registro contacto
                Comunicacion Contacto = new Comunicacion();
                Contacto.EMail = lblEmail.Text;
                Contacto.IdEntidad = Obj.NroInscripcion;
                Contacto.origenTabla = "T_ENTIDADES_JOR_EMP";
                var RegEmail = Bl.BlRegistrarContacto(Contacto);
                if (RegEmail == "OK")
                {
                    Contacto.EMail = null;
                    Contacto.NroCel = txtCelular.Text.Trim();
                    Contacto.CodAreaCel = txtCodAreaCel.Text.Trim();
                    var RegCel = Bl.BlRegistrarContacto(Contacto);

                }

                //var ActContacto = Bl.BlActualizarContacto(Contacto);

                // registro entrada
                var registro = Bl.BlRegistrarInvitacion(Obj);
                if (registro.OcurrioError)
                {
                    divMensajeError.Visible = true;
                    divMensajeExito.Visible = false;
                    lblMensajeError.Text = "No se pudo completar el registro. Error: " + registro.MensajeError;
                }
                else
                {
                    divMensajeError.Visible = false;
                    divMensajeExito.Visible = true;
                    lblMensajeExito.Text = "Se completo con éxito su registro.";

                    LimpiarDatos();

                    GeneraCodigoQR(Obj.NroInscripcion);
                    ImprimirInvitacionEstudiante(Obj.NroInscripcion);


                }

            }
            else
            {
                divMensajeError.Visible = true;
                divMensajeExito.Visible = false;
                lblMensajeError.Text = "Ya existe el registro de la persona consultada. ";

            }


        }

        protected void LimpiarDatos()
        {
            txtCuit.Text = "";
            lblRazonSocial.Text = "";
            txtCelular.Text = "";
            lblEmail.Text = "";
            txtEmail.Text = "";
            txtCodAreaCel.Text = "";
            txtCelular.Text = "";
            btnConsultar.Enabled = true;
            txtCuit.Enabled = true;
            txtCUIL.Text = "";
            ddlFiltroBusqueda.Enabled = true;
            divAcciones.Visible = false;
            divRegistro.Visible = false;
            divCUIT.Visible = false;
            divCUIL.Visible = false;
            divResultados.Visible = false;
            divResultados2.Visible = false;
            divInscripcion.Visible = false;
            divMensajeError.Visible = false;
            divMensajeExito.Visible = false;
            ddlFiltroBusqueda.SelectedValue = "00";
        }

        protected void btnImprimir_OnClick(object sender, EventArgs e)
        {
            var GetPersona = Bl.BlGetInscripcionByCUIL(txtCUIL.Text);
            String NroInvitacion = "";
            foreach (DataRow Persona in GetPersona.Rows)
            {
                NroInvitacion = Persona["ID_ENTIDAD"].ToString();
                if (Persona["prioridad"].ToString() == "1")
                {
                    ImprimirInvitacionEmprendedor(NroInvitacion);
                }
                else
                {
                    ImprimirInvitacionEstudiante(NroInvitacion);
                }
            }


        }

        //protected void btnImprimir2_OnClick(object sender, EventArgs e)
        //{
        //    var GetPersona = Bl.BlGetInscripcionByCUIL(txtCUIL.Text);
        //    String NroInvitacion = "";
        //    foreach (DataRow Persona in GetPersona.Rows)
        //    {
        //        NroInvitacion = Persona["ID_USUARIO"].ToString();
        //    }

        //    ImprimirInvitacion2(NroInvitacion);
        //}

        protected void btnSalir_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("JornadaEmprendedora.aspx");
        }



        #region Metodos para Certificado

        public List<InscripcionEvento> Inscripcion
        {
            get
            {
                return Session["Inscripcion"] == null ? null : (List<InscripcionEvento>)Session["Inscripcion"];
            }
            set { Session["Inscripcion"] = value; }
        }

        public Bitmap GeneraCodigoQR(string TextoCodificar)
        {
            //Instancia del objeto encargado de codificar el codigo QR
            QRCodeEncoder CodigoQR = new QRCodeEncoder();

            //configuracion de las propiedades del objeto.
            CodigoQR.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            CodigoQR.QRCodeScale = 4;
            CodigoQR.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M;
            CodigoQR.QRCodeVersion = 0;
            CodigoQR.QRCodeBackgroundColor = System.Drawing.Color.White;
            CodigoQR.QRCodeForegroundColor = System.Drawing.Color.Black;



            //Se retorna el Codigo QR codificado en formato de arreglo de bytes.
            return CodigoQR.Encode(TextoCodificar, System.Text.Encoding.UTF8);
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            //Se crea un buffer de lectura
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //Se guarda la imagen que es enviada como parametro a traves de la funcion Save del componente de imagen en el buffer de lectura
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            //Se retorna la imagen en formato de arreglo de bytes
            return ms.ToArray();
        }

        private void ImprimirInvitacionEmprendedor(String NroInscripcion)
        {

            Inscripcion = Bl.BlConsultaInscripcionByNro(NroInscripcion);

            ResultadoRule res = new ResultadoRule();

            if (Inscripcion == null)
            {
                return;
            }


            var nombreReporteRdlc = "SIFCOS.TarjetaInvitacionVIP.rdlc";

            /*Creo y Cargo el Reporte*/
            var reporte = new ReporteGeneral2(nombreReporteRdlc, TipoArchivoEnum.Pdf, nombreReporteRdlc);



            //Se crea un arreglo de tipo byte que contendra el texto codificado en QR.
            String hash = HttpUtility.UrlEncode(Encrypt(Inscripcion[0].NroInscripcion.ToString() + "VIP"));

            Bitmap Arreglo_Imagen = GeneraCodigoQR("https://sifcos.cba.gov.ar/JornadaEmprendedora.aspx?key=" + hash);

            var listCodigo = new List<CodigoQr>();
            listCodigo.Add(new CodigoQr { UrlCodigoQr = "", ImgBmp = imageToByteArray(Arreglo_Imagen) });
            reporte.AddDataSource("dataset", listCodigo);



            // Guardo datos del reporte en sessión
            Session["ReporteGeneral2"] = reporte;

            //LEVANTA LA PAGINA RerporteGeneral
            Response.Redirect("ReporteGeneral2.aspx");
        }

        private void ImprimirInvitacionEstudiante(String NroInscripcion)
        {

            Inscripcion = Bl.BlConsultaInscripcionByNro(NroInscripcion);

            ResultadoRule res = new ResultadoRule();

            if (Inscripcion == null)
            {
                return;
            }


            var nombreReporteRdlc = "SIFCOS.TarjetaInvitacion.rdlc";

            /*Creo y Cargo el Reporte*/
            var reporte = new ReporteGeneral2(nombreReporteRdlc, TipoArchivoEnum.Pdf, nombreReporteRdlc);



            //Se crea un arreglo de tipo byte que contendra el texto codificado en QR.
            String hash = HttpUtility.UrlEncode(Encrypt(Inscripcion[0].NroInscripcion.ToString()));

            Bitmap Arreglo_Imagen = GeneraCodigoQR("https://sifcos.cba.gov.ar/PremiosAlComercio.aspx?key=" + hash);

            var listCodigo = new List<CodigoQr>();
            listCodigo.Add(new CodigoQr { UrlCodigoQr = "", ImgBmp = imageToByteArray(Arreglo_Imagen) });
            reporte.AddDataSource("dataset", listCodigo);

            // Guardo datos del reporte en sessión
            Session["ReporteGeneral2"] = reporte;

            //LEVANTA LA PAGINA RerporteGeneral
            Response.Redirect("ReporteGeneral2.aspx");
        }

        #endregion

        #region Encriprtacion/Desencriptacion
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        #endregion
    }
}