using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Diagnostics;
using AppComunicacion;
using DA_SIFCOS.Entities.Ciudadano_Digital;
using DA_SIFCOS.Models;

//using DataAccessSIIC.Entidades.CiDi; no lo usaba 


namespace DA_SIFCOS.Entidades
{
    public class Helper
    {

        //public string MostrarDomicilio(HttpSessionStateBase session, HttpRequestBase request, string idEntidad) 
        //{
        //    // MOSTRAR DOMICILIO REGISTRADO:
        //    // Con este código devuelve la url del iframe para mostrar los datos del domicilio registrado.

        //    string tokenCidi = request.Cookies["CiDi"].Value.ToString(); // Es el token que se guarda en las cookies al estar logueado con ciudadano digital.
        //    Configuracion config = new Configuracion();
        //    config.AppId = Config.CiDiIdAplicacion.ToString();  // Es el Id de aplicación de Cidi
        //    config.AppKey = Config.CiDiKeyAplicacion;       //Key generada al dar de alta la aplicación en cidi
        //    config.AppPass = Config.CiDiPassAplicacion; //Contraseña generada al dar de alta la aplicación en cidi

        //    config.Entorno = WebConfigurationManager.AppSettings["Entorno"].ToString();
        //    ServicioComunicacion service = new AppComunicacion.ServicioComunicacion(config);
        //    Usuario usuarioLogueado = (Usuario)session["UsuarioCiDiLogueado"];

        //    String url = service.Domicilios(usuarioLogueado.CUIL, AppComunicacion.ApiModels.Domicilio.IdVin.ToString(), RolesDomicilio.CONSULTAR_DOMICILIO);

        //    return url;

        //}


        public static string AltaDomicilio(HttpSessionStateBase session, HttpRequestBase request, string idEntidad)
        {
            string url = String.Empty;
            try
            {
                if (request != null)
                {
                    string tokenCidi = request.Cookies["CiDi"].Value.ToString(); // Es el token que se guarda en las cookies al estar logueado con ciudadano digital.
                    Configuracion config = new Configuracion();
                    config.AppId = Config.CiDiIdAplicacion.ToString();  // Es el Id de aplicación de Cidi
                    config.AppKey = Config.CiDiKeyAplicacion;       //Key generada al dar de alta la aplicación en cidi
                    config.AppPass = Config.CiDiPassAplicacion; //Contraseña generada al dar de alta la aplicación en cidi

                    config.Entorno = WebConfigurationManager.AppSettings["Entorno"].ToString();
                    ServicioComunicacion service = new AppComunicacion.ServicioComunicacion(config);
                    Usuario usuarioLogueado = (Usuario)session["UsuarioCiDiLogueado"];

                    //Con este código se obtiene el objeto domicilio de la dll AppComunicacion.
                    // Obtiene todos los datos necesarios del domicilio registrado anteriormente(por ej el ID_VIN).
                    
                    //SI EL DOMICILIO NO EXISTE, DEVUELVO UNA URL PARA REGISTRARLO

                    url = service.Domicilios(usuarioLogueado.CUIL, idEntidad, RolesDomicilio.ALTA_DOMICILIO_GENERICO, 3, JurisdiccionDomicilio.PROVINCIAL);

                    
                    
                }
            }
            catch (Exception ex)
            {
                url = "" + ex;
            }
            Debug.WriteLine("la url en AltaDomicilio es " + url);
            return url;

        }

        public static string AltaDomiciliolegal(HttpSessionStateBase session, HttpRequestBase request, string idEntidad)
        {
            string url = String.Empty;
            try
            {
                if (request != null)
                {
                    string tokenCidi = request.Cookies["CiDi"].Value.ToString(); // Es el token que se guarda en las cookies al estar logueado con ciudadano digital.
                    Configuracion config = new Configuracion();
                    config.AppId = Config.CiDiIdAplicacion.ToString();  // Es el Id de aplicación de Cidi
                    config.AppKey = Config.CiDiKeyAplicacion;       //Key generada al dar de alta la aplicación en cidi
                    config.AppPass = Config.CiDiPassAplicacion; //Contraseña generada al dar de alta la aplicación en cidi

                    config.Entorno = WebConfigurationManager.AppSettings["Entorno"].ToString();
                    ServicioComunicacion service = new AppComunicacion.ServicioComunicacion(config);
                    Usuario usuarioLogueado = (Usuario)session["UsuarioCiDiLogueado"];

                    //Con este código se obtiene el objeto domicilio de la dll AppComunicacion.
                    // Obtiene todos los datos necesarios del domicilio registrado anteriormente(por ej el ID_VIN).

                    //SI EL DOMICILIO NO EXISTE, DEVUELVO UNA URL PARA REGISTRARLO

                    url = service.Domicilios(usuarioLogueado.CUIL, idEntidad, RolesDomicilio.ALTA_DOMICILIO_GENERICO, 3, JurisdiccionDomicilio.NACIONAL);



                }
            }
            catch (Exception ex)
            {
                url = "" + ex;
            }
            Debug.WriteLine("la url en AltaDomicilio es " + url);
            return url;

        }

        public static AppComunicacion.ApiModels.Domicilio ComprobarDomicilio(HttpSessionStateBase session, HttpRequestBase request, string idEntidad)
        {
            try
            {
                
                    string tokenCidi = request.Cookies["CiDi"].Value.ToString(); // Es el token que se guarda en las cookies al estar logueado con ciudadano digital.
                    Configuracion config = new Configuracion();
                    config.AppId = Config.CiDiIdAplicacion.ToString();  // Es el Id de aplicación de Cidi
                    config.AppKey = Config.CiDiKeyAplicacion;       //Key generada al dar de alta la aplicación en cidi
                    config.AppPass = Config.CiDiPassAplicacion; //Contraseña generada al dar de alta la aplicación en cidi

                    config.Entorno = WebConfigurationManager.AppSettings["Entorno"].ToString();
                    ServicioComunicacion service = new AppComunicacion.ServicioComunicacion(config);
                    Usuario usuarioLogueado = (Usuario)session["UsuarioCiDiLogueado"];

                    //Con este código se obtiene el objeto domicilio de la dll AppComunicacion.
                    // Obtiene todos los datos necesarios del domicilio registrado anteriormente(por ej el ID_VIN).

                    var domicilioJSON = service.ApiDomiciliosJSON(tokenCidi, idEntidad, RolesAPIDomicilios.CONSULTAR_DOMICILIO_GEN, Convert.ToInt32(config.AppId), 3, usuarioLogueado.CUIL);
                    AppComunicacion.ApiModels.Domicilio domicilio = new JavaScriptSerializer().Deserialize<AppComunicacion.ApiModels.Domicilio>(domicilioJSON);


                    return domicilio;
                
            }
            catch (Exception)
            {
                return null;
            }
            

        }


        public static string getURLDomicilio(HttpSessionStateBase session, HttpRequestBase request, string idEntidad)
        {
            string url = String.Empty;
            try
            {
                if (request != null)
                {
                    string tokenCidi = request.Cookies["CiDi"].Value.ToString(); // Es el token que se guarda en las cookies al estar logueado con ciudadano digital.
                    Configuracion config = new Configuracion();
                    config.AppId = Config.CiDiIdAplicacion.ToString();  // Es el Id de aplicación de Cidi
                    config.AppKey = Config.CiDiKeyAplicacion;       //Key generada al dar de alta la aplicación en cidi
                    config.AppPass = Config.CiDiPassAplicacion; //Contraseña generada al dar de alta la aplicación en cidi

                    config.Entorno = WebConfigurationManager.AppSettings["Entorno"].ToString();
                    ServicioComunicacion service = new AppComunicacion.ServicioComunicacion(config);
                    Usuario usuarioLogueado = (Usuario)session["UsuarioCiDiLogueado"];

                    //Con este código se obtiene el objeto domicilio de la dll AppComunicacion.
                    // Obtiene todos los datos necesarios del domicilio registrado anteriormente(por ej el ID_VIN).

                    String UserCuil = "20286577769";
                    if (usuarioLogueado != null)
                    {
                        UserCuil = usuarioLogueado.CUIL;
                    }

                    var domicilioJSON = service.ApiDomiciliosJSON(tokenCidi, idEntidad, RolesAPIDomicilios.CONSULTAR_DOMICILIO_GEN, Convert.ToInt32(config.AppId), 3, UserCuil);
                    AppComunicacion.ApiModels.Domicilio domicilio = new JavaScriptSerializer().Deserialize<AppComunicacion.ApiModels.Domicilio>(domicilioJSON);


                    if (domicilio == null)
                    {   //SI EL DOMICILIO NO EXISTE, DEVUELVO UNA URL PARA REGISTRARLO
                        url = service.Domicilios(UserCuil, idEntidad, RolesDomicilio.ALTA_DOMICILIO_GENERICO, 3, JurisdiccionDomicilio.PROVINCIAL);
                        
                    }
                    else
                    {   // EN EL CASO QUE EL DOMICILIO EXISTA DEVUELVO UNA URL PARA MOSTRARLO EN EL IFRAME
                        url = service.Domicilios(UserCuil, domicilio.IdVin.ToString(), RolesDomicilio.CONSULTAR_DOMICILIO);
                    }
                }
            }
            catch (Exception ex)
            {
                url = ""+ex;
            }
            Debug.WriteLine("la url en getURLDomicilio es " + url);
            return url;

        }

        public static int getIdVinDomicilio(HttpSessionStateBase session, HttpRequestBase request, string idEntidad)
        {
            int result=0;
            try
            {
                string tokenCidi = request.Cookies["CiDi"].Value.ToString();
                Configuracion config = new Configuracion();
                config.AppId = Config.CiDiIdAplicacion.ToString();
                config.AppKey = Config.CiDiKeyAplicacion;
                config.AppPass = Config.CiDiPassAplicacion;

                config.Entorno = WebConfigurationManager.AppSettings["Entorno"].ToString();
                ServicioComunicacion service = new AppComunicacion.ServicioComunicacion(config);
                Usuario usuarioLogueado = (Usuario)session["UsuarioCiDiLogueado"];

                String UserCuil = "20286577769";
                if (usuarioLogueado != null)
                {
                    UserCuil = usuarioLogueado.CUIL;
                }

                var domicilioJSON = service.ApiDomiciliosJSON(tokenCidi, idEntidad, RolesAPIDomicilios.CONSULTAR_DOMICILIO_GEN, Convert.ToInt32(config.AppId), 3, UserCuil);
                AppComunicacion.ApiModels.Domicilio domicilio = new JavaScriptSerializer().Deserialize<AppComunicacion.ApiModels.Domicilio>(domicilioJSON);
                if (domicilio != null)
                {
                    result = domicilio.IdVin;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }



        public static AppComunicacion.ApiModels.Domicilio getDomicilio(HttpSessionStateBase session, HttpRequestBase request, string idEntidad)
        {
            AppComunicacion.ApiModels.Domicilio domicilio;
            try
            {
                string tokenCidi = request.Cookies["CiDi"].Value.ToString();
                Configuracion config = new Configuracion();
                config.AppId = Config.CiDiIdAplicacion.ToString();
                config.AppKey = Config.CiDiKeyAplicacion;
                config.AppPass = Config.CiDiPassAplicacion;

                config.Entorno = WebConfigurationManager.AppSettings["Entorno"].ToString();
                ServicioComunicacion service = new AppComunicacion.ServicioComunicacion(config);
                Usuario usuarioLogueado = (Usuario)session["UsuarioCiDiLogueado"];

                String UserCuil = "20286577769";
                if (usuarioLogueado != null)
                {
                    UserCuil=usuarioLogueado.CUIL;
                }

                var domicilioJSON = service.ApiDomiciliosJSON(tokenCidi, idEntidad, RolesAPIDomicilios.CONSULTAR_DOMICILIO_GEN, Convert.ToInt32(config.AppId), 3, UserCuil);
                domicilio = new JavaScriptSerializer().Deserialize<AppComunicacion.ApiModels.Domicilio>(domicilioJSON);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return domicilio;
        }




    }
}