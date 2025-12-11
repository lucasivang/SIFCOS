using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace DA_SIFCOS.Utils
{
    public class MapeadorWebApi
    {
        #region SETTINGS

        /// <summary>
        /// Path para consumir la Web Api CDD.
        /// </summary>
        public static String RouteWebApiCdd
        {
            get
            {
                if (ConfigurationManager.AppSettings["Entorno"].ToString() == "produccion")
                {
                    return ConfigurationManager.AppSettings["Url_WebApi_CDD_Produccion"].ToString();
                }
                return ConfigurationManager.AppSettings["Url_WebApi_CDD_Desarrollo"].ToString();
            }
        }

        /// <summary>
        /// Path para consumir la Web Api CiDi.
        /// </summary>
        public static String RouteWebApiCiDi
        {
            get
            {
                return ConfigurationManager.AppSettings["CiudadanoDigital_URL_Api_Documentacion"].ToString();
            }
        }

        /// <summary>
        /// Identificador de Aplicacion de Origen.
        /// </summary>
        public static String Id_Aplicacion_Origen
        {
            get
            {
                return ConfigurationManager.AppSettings["Id_App"].ToString();
            }
        }

        /// <summary>
        /// Password de Aplicacion de Origen.
        /// </summary>
        public static String Pwd_Aplicacion_Origen
        {
            get
            {
                return ConfigurationManager.AppSettings["Pwd_App"].ToString();
            }
        }

        /// <summary>
        /// Key de Aplicacion de Origen.
        /// </summary>
        public static String Key_Aplicacion_Origen
        {
            get
            {
                return ConfigurationManager.AppSettings["Key_App"].ToString();
            }
        }

        /// <summary>
        /// Key de Aplicacion de Origen.
        /// </summary>
        public static String User_Aplicacion_Origen
        {
            get
            {
                return ConfigurationManager.AppSettings["Id_Usuario"].ToString();
            }
        }

        /// <summary>
        /// Identificador del documento.
        /// </summary>
        public static String Id_Tipo_Documento
        {
            get
            {
                return ConfigurationManager.AppSettings["Id_Tipo_Documento"].ToString();
            }
        }
        

        public static String Nombre_Tipo_Documento
        {
            get
            {
                return ConfigurationManager.AppSettings["Nombre_Tipo_Documento"].ToString();
            }
        }

        public static String Id_Ubicacion_Fisica
        {
            get
            {
                return ConfigurationManager.AppSettings["Id_Ubicacion_Fisica"].ToString();
            }
        }

        #endregion

        #region MAPPING CIDI

        public static String Obtener_Documento_Web_Api_CiDi
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCiDi + "api/Documentacion/Obtener_Documento";
            }
        }

        public static String Guardar_Documento_Web_Api_CiDi
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCiDi + "api/Documentacion/Guardar_Documento";
            }
        }

        #endregion

        #region MAPPING CDD

        public static String Autorizar_Solicitud
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Authorize/Autorizar_Solicitud";
            }
        }

        public static String Autorizar_S_Solicitud
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Authorize/Autorizar_Solicitud_S_Key";
            }
        }

        public static String Obtener_Permisos_Catalogos
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Obtener_Permisos_Catalogo_Por_Id_Aplicacion";
            }
        }

        public static String Obtener_Listado_Documentos
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Obtener_Listado_Documentos";
            }
        }

        public static String Obtener_Listado_Documentos_Con_Preview
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Obtener_Listado_Documentos_Con_Preview";
            }
        }

        public static String Obtener_Documento
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Obtener_Documento";
            }
        }

        public static String Obtener_S_Documento
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Obtener_S_Documento";
            }
        }

        public static String Obtener_Documento_S_Blob
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Obtener_Documento_S_Blob";
            }
        }

        public static String Obtener_Documento_Large_File_Data
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Obtener_Large_Data_File_Documento";
            }
        }

        public static String Guardar_Documento
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Guardar_Documento";
            }
        }

        public static String Guardar_S_Documento
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Guardar_S_Documento";
            }
        }

        public static String Obtener_Documentos_Expediente_Suac
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Obtener_Documentos_Expediente_Suac";
            }
        }

        public static String Guardar_Expediente_Suac
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "Documentacion/Guardar_Expediente_Suac";
            }
        }

        #endregion

        #region LSD DOCUMENTOS

        public static String Obtener_Imagen_Documento
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "DocumentacionLSD/Obtener_Imagen_Documento";
            }

        }

        public static String Obtener_LSD_Documento
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "DocumentacionLSD/Obtener_Documento";
            }
        }

        public static String Guardar_LSD_Documento
        {
            get
            {
                return MapeadorWebApi.RouteWebApiCdd + "DocumentacionLSD/Guardar_Documento";
            }
        }

        #endregion

        #region HTTP METHODS

        /// <summary>
        /// Realiza la llamada a la Web API, serializa la Entrada y deserializa la Respuesta.
        /// </summary>
        /// <typeparam name="TEntrada">Declarar el objeto de Entrada al método.</typeparam>
        /// <typeparam name="TRespuesta">Declarar el objeto de Respuesta al método.</typeparam>
        /// <param name="Accion">Recibe la acción específica del controlador de la WebAPI.</param>
        /// <param name="tEntrada">Objeto de entrada de la WebAPI , especificado en TEntrada.</param>
        /// <returns>Objeto de salida de la WebAPI, especificado en TRespuesta.</returns>
        public static TRespuesta ConsumirWebApi<TEntrada, TRespuesta>(String Accion, TEntrada tEntrada)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Accion);
            httpWebRequest.ContentType = "application/json; charset=utf-8";

            String rawjson = JsonConvert.SerializeObject(tEntrada);
            httpWebRequest.Method = "POST";

            var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());

            streamWriter.Write(rawjson);
            streamWriter.Flush();
            streamWriter.Close();

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());
            var result = streamReader.ReadToEnd();

            TRespuesta respuesta = JsonConvert.DeserializeObject<TRespuesta>(result);

            return respuesta;
        }

        #endregion
    }
}