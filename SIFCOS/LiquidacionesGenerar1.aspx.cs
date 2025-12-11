using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using BL_SIFCOS;
using DA_SIFCOS.Entities.Ciudadano_Digital;

namespace SIFCOS
{
    public partial class LiquidacionesGenerar : System.Web.UI.Page
    {
        public static ReglaDeNegocios Bl = new ReglaDeNegocios();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Obtiene el ultimo nro de sifcos a liquidar (+1)
        /// Autor: (IB)
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object ObtenerLiqAltasUltimoNroSifcos()
        {
            List<Exception> oExcep = new List<Exception>();
            try
            {
                //TODO: Controlar tipo dato de organismo loguado
                int nroSifcosDesde = Bl.BlLiquidacionesAltasGetUltima();
                return new { Result = "OK", NroSifcosDesde = nroSifcosDesde };

            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        /// <summary>
        /// Genera una nueva liquidación de altas
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GenerarAltas(string pNroSifcosDesde, string pNroSifcosHasta)
        {
            List<Exception> oExcep = new List<Exception>();
            try
            {
                //TODO: Controlar tipo dato de organismo loguado
                Usuario usrLog = (Usuario)HttpContext.Current.Session["UsuarioCiDiLogueado"];

                string mensaje = "";

                //Parámetro nroSifcosDesde
                int nroSifcosDesde = 0;
                int NroSif;
                bool NroSifOK;
                NroSifOK = int.TryParse(pNroSifcosDesde, out NroSif);

                if (NroSifOK)
                    nroSifcosDesde = NroSif;
                else
                    return new { Result = "ERROR", Mensaje = "Error. Nro Sifcos Desde incorrecto", IdLiquidacion = 0 };

                //Parámetro nroSifcosHasta
                int nroSifcosHasta = 0;
                NroSifOK = int.TryParse(pNroSifcosHasta, out NroSif);
                if (NroSifOK)
                    nroSifcosHasta = NroSif;
                else
                    return new { Result = "ERROR", Mensaje = "Error. Nro Sifcos Hasta incorrecto", IdLiquidacion = 0 };

                int liq = Bl.BlGenerarLiquidacionAlta(nroSifcosDesde, nroSifcosHasta, usrLog.CUIL, out mensaje);

                if (liq != 0)
                {
                    HttpContext.Current.Session["UltimoCodLiq"] = liq.ToString();
                    HttpContext.Current.Session["UltimoTipoLiq"] = "1";
                    return new { Result = "OK", Mensaje = mensaje, IdLiquidacion = liq.ToString() };
                }

                return new { Result = "ERROR", Mensaje = mensaje, IdLiquidacion = 0 };


            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Mensaje = ex.Message, IdLiquidacion = 0 };
            }
        }

        /// <summary>
        /// Genera una nueva liquidación de reempadronamientos
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GenerarReempa(string pFecHasta)
        {
            List<Exception> oExcep = new List<Exception>();
            try
            {
                //TODO: Controlar tipo dato de organismo loguado
                Usuario usrLog = (Usuario)HttpContext.Current.Session["UsuarioCiDiLogueado"];
                //DateTime fecHasta = Convert.ToDateTime(pFecHasta);
                //Parámetro FecHasta
                DateTime FecHasta;
                DateTime FHast;
                bool FHastOK;
                FHastOK = DateTime.TryParse(pFecHasta, out FHast);

                if (FHastOK)
                    FecHasta = FHast;
                else
                    return new { Result = "ERROR", Mensaje = "Error. Formato de fecha incorrecto", IdLiquidacion = 0 };

                string mensaje = "";
                int liq = Bl.BlGenerarLiquidacionReempa(FecHasta, usrLog.CUIL, out mensaje);

                if (liq != 0)
                {
                    HttpContext.Current.Session["UltimoCodLiq"] = liq.ToString();
                    HttpContext.Current.Session["UltimoTipoLiq"] = "4";
                }

                return new { Result = "OK", Mensaje = mensaje, IdLiquidacion = liq.ToString() };


            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Mensaje = ex.Message, IdLiquidacion = 0 };
            }
        }
    }
}