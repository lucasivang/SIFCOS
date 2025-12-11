using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Web.UI.HtmlControls;
using BL_SIFCOS;
using DA_SIFCOS;

namespace SIFCOS
{
    public partial class Enviar2 : System.Web.UI.Page
    {
        private const String Key_Cif_Decif = "Warrior2025";
        public ReglaDeNegocios Bl = new ReglaDeNegocios();
        private void MostrarOcultarModal(HtmlControl divModal, bool mostrar)
        {
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
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Hash = Request.QueryString["destinatario"];
                var destinatario = Descifrar(Hash);
                var token = await ObtenerTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    await EnviarNotificacionAsync(token, destinatario);

                }
            }
            

        }
        // Notificacion CIDI
        public async Task EnviarNotificacionAsync(string token,string destinatario)
        {
            var url = "https://alimentos.cba.gov.ar/api/ApiPublica/notificacionesCIDI";
            var datos = new
            {
                Destinatario = destinatario,
                Asunto = "Notificación de deuda!!!",
                Contenido = "Desde la Secretaría de Comercio, dependiente del Ministerio de Producción,Ciencia e Innovación Tecnológica del <br />" +
                            "Gobierno de la Provincia de Córdoba, nos dirigimos a Ud.a fin de informarle que, según nuestros antecedentes <br />" +
                            "<b><u> NO HA CUMPLIDO CON EL PAGO DE LA TASA RETRIBUTIVA DE SERVICIOS </u> <br />" +
                            "   Es por ello que lo INVITAMOS a regularizar su situación lo antes posible, evitando así incurrir en infracción. <br />" +
                            "Ud.podrá corroborar su situación ingresando al sitio WEB oficial del Gobierno de la Provincia de Córdoba <br />" +
                            "con su Ciudadano Digital Nivel 2 y dentro de mis tramites imprimir nuevamente la tasa correspondiente para abonar " +
                            "<br /><b><a href=https://sifcos.cba.gov.ar>Ir a Sifcos</a></b><br /><br />" +
                            "QUEDA UD. DEBIDAMENTE NOTIFICADO.<br /><br /> Saludos  Atte. <br /><br />" +
                            "Secretaria de Comercio  - Ministerio de Producción,Ciencia e Innovación Tecnológica.",
                EsHtml = "false",
                Firma = "Secretaria de Comercio",
                Ente = "Gobierno de la Provincia de Córdoba",
                Application = ConfigurationManager.AppSettings["CiDiIdAplicacion"],
                Password = ConfigurationManager.AppSettings["CiDiPassAplicacion"],
                Key = ConfigurationManager.AppSettings["CiDiKeyAplicacion"]
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonData = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var resultado = await response.Content.ReadAsStringAsync();
                String IdEntidad = (String)Session["IdEntidad"];

                if (response.IsSuccessStatusCode)

                {
                    var marcarNotificado = Bl.BlRegistrarNotificacionCIDI(IdEntidad);
                    MostrarOcultarModal(modalNotificacionEnvioCIDI, true);
                    MostrarOcultarModal(modalNotificacionNOenvioCIDI, false);
                    Session["IdEntidad"] =null;
                }
                else 
                {
                    MostrarOcultarModal(modalNotificacionEnvioCIDI, false);
                    MostrarOcultarModal(modalNotificacionNOenvioCIDI, true);
                }
                // Mostrar resultado
                //int reintentos = 0;
                //while (response.ReasonPhrase == "Unauthorized" && reintentos < 2)
                //{
                //    token = await ObtenerTokenAsync();
                //    reintentos++;
                //    response = await client.PostAsync(url, jsonData);
                //}

                //if (string.IsNullOrEmpty(token))
                //{
                //    Console.WriteLine("No se pudo obtener un token válido.");
                //    return;
                //}

                Console.WriteLine(resultado);
                
            }
        }

        
        public async Task<string> ObtenerTokenAsync()
        {
            var url = "https://alimentos.cba.gov.ar/api/auth/IniciarSesionApi";
            var usuario = "Ferbudassi";
            var contraseña = "Xz492C8aAl1m";
            var credenciales = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{usuario}:{contraseña}"));

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credenciales);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(url, null);
                var contenido = await response.Content.ReadAsStringAsync();

                var json = JObject.Parse(contenido);
                var token = json["Object"]?["Token"]?.ToString();
                return token;
            }
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("AdministracionTramites.aspx");
        }

        protected void btnNoEnvio_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("AdministracionTramites.aspx");
        }
        public string Descifrar(string pTextoCifrado)
        {

            string key = Key_Cif_Decif;

            pTextoCifrado = pTextoCifrado.Replace(" ", "+");
            byte[] arrBytesCifrados = Convert.FromBase64String(pTextoCifrado);

            using (Aes encriptadorAES = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encriptadorAES.Key = pdb.GetBytes(32);
                encriptadorAES.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encriptadorAES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(arrBytesCifrados, 0, arrBytesCifrados.Length);
                        cs.Close();
                    }

                    pTextoCifrado = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return pTextoCifrado;
        }
    }
}