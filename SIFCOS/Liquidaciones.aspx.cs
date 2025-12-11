using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using BL_SIFCOS;
using DA_SIFCOS;
using DA_SIFCOS.Entidades;

namespace SIFCOS
{
    public partial class Liquidaciones : System.Web.UI.Page
    {
        public static ReglaDeNegocios Bl = new ReglaDeNegocios();
        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarOcultarModalEnConstruccion(true);
            ReglaDeNegocios Bl = new ReglaDeNegocios();
            //List<DataAccessSifcos.SifLiquidaciones> sliq = Bl.BlGetLiquidaciones(null,null,null,null,null,null,null,null,null,null,null,null,null,null,null, out Ex);
            // List<Rubro> lt = Bl.BlGetRubrosComercio("BAZAR");
            //List<Exception> Ex = new List<Exception>();
            //List<DataAccessSifcos.SifLiquidaciones> sliq = Bl.BlGetLiquidaciones(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, out Ex);
            //Response.Write(sliq.Count);

            if (Request.QueryString["ultiq"] != null)
            {

                int UltimoCodLiq = 0;
                int UltimoTipoLiq = 0;
                if (HttpContext.Current.Session["UltimoCodLiq"] != null)
                {
                    int IdLiq;
                    bool IdLiqOK;
                    IdLiqOK = int.TryParse(HttpContext.Current.Session["UltimoCodLiq"].ToString(), out IdLiq);
                    if (IdLiqOK)
                    {
                        UltimoCodLiq = IdLiq;
                        filIdCodLiq.Value = UltimoCodLiq.ToString();
                    }
                }

                if (HttpContext.Current.Session["UltimoTipoLiq"] != null)
                {
                    int IdTipLiq;
                    bool IdTipLiqOK;
                    IdTipLiqOK = int.TryParse(HttpContext.Current.Session["UltimoTipoLiq"].ToString(), out IdTipLiq);
                    if (IdTipLiqOK)
                    {
                        UltimoTipoLiq = IdTipLiq;
                        //cmbTipoLiq.DataValueField = UltimoCodLiq.ToString();
                        cmbTipoLiq.SelectedIndex = 1;
                    }
                }




                //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:consultarLiquidaciones(" + UltimoTipoLiq + "," + UltimoCodLiq + "); ", true);

            }
        }
        private void MostrarOcultarModalEnConstruccion(bool mostrar)
        {
            if (mostrar)
            {
                var classname = "mostrarInfoDeudaTramite";
                string[] listaStrings = ModalPaginaConstruccion.Attributes["class"].Split(' ');
                var listaStr = String.Join(" ", listaStrings
                    .Except(new string[] { "", classname })
                    .Concat(new string[] { classname })
                    .ToArray()
                );
                ModalPaginaConstruccion.Attributes.Add("class", listaStr);
            }
            else
            {
                //oculta la Modal 
                ModalPaginaConstruccion.Attributes.Add("class", String.Join(" ", ModalPaginaConstruccion
                    .Attributes["class"]
                    .Split(' ')
                    .Except(new string[] { "", "mostrarInfoDeudaTramite" })
                    .ToArray()
                ));
            }
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetLiquidaciones(int jtStartIndex, int jtPageSize, string jtSorting, string pIdTipoTramite, string pCodigoLiq)
        {

            try
            {
                List<Exception> oExcep = new List<Exception>();

                int total = 0;

                //TODO: Revisar reemplazos
                List<Exception> Ex = new List<Exception>();
                int idTipoTramite;
                bool isNumeric = int.TryParse(pIdTipoTramite, out idTipoTramite);
                if (!isNumeric)
                {
                    return new { Result = "ERROR", Message = "No se pudo determinar el tipo tramite" };
                }

                int? pCodigoLiquidacion = null;
                int codigoLiq;
                if (!string.IsNullOrEmpty(pCodigoLiq))
                {
                    isNumeric = int.TryParse(pCodigoLiq, out codigoLiq);
                    if (!isNumeric)
                    {
                        return new { Result = "ERROR", Message = "No se pudo determinar el código de liquidación" };
                    }
                    pCodigoLiquidacion = codigoLiq;
                }

                List<SifLiquidaciones> obj = Bl.BlGetLiquidaciones(idTipoTramite, pCodigoLiquidacion, out Ex);

                List<SifLiquidaciones> objSort = LinqHelper.OrderBy(obj.AsQueryable(), jtSorting).ToList();
                total = obj.Count;
                obj = objSort.GetRange(jtStartIndex, (jtStartIndex + jtPageSize) > total ? (total - jtStartIndex) : jtPageSize);
                var retorno =
                   from o in obj.AsEnumerable()
                   select new
                   {
                       IdLiquidacion = o.IdLiquidacion,
                       NroSifcosDesde = o.NroSifcosDesde,
                       NroSifcosHasta = o.NroSifcosHasta,
                       FecDesde = o.FecDesde,
                       FecHasta = o.FecHasta,
                       NTipoTramite = o.NTipoTramite,
                       IdTipoTramite = o.IdTipoTramite,
                       NroExpediente = o.NroExpediente,
                       NroResolucion = o.NroResolucion,
                       FechaResolucion = o.FechaResolucion
                   };
                return new { Result = "OK", Records = retorno, TotalRecordCount = total };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetLiqOrganismos(int jtStartIndex, int jtPageSize, string jtSorting, string pIdLiquidacion, string pIdOrganismoSup)
        {

            try
            {
                int IdLiquidacion;
                bool isNumeric = int.TryParse(pIdLiquidacion, out IdLiquidacion);
                if (!isNumeric)
                {
                    return new { Result = "ERROR", Message = "No se pudo obtener IdLiquidacion" };
                }

                int IdOrganismoSup;
                isNumeric = int.TryParse(pIdOrganismoSup, out IdOrganismoSup);
                if (!isNumeric)
                {
                    return new { Result = "ERROR", Message = "No se pudo obtener IdOrganismoSup" };
                }


                List<Exception> oExcep = new List<Exception>();

                int total = 0;



                //TODO: Revisar reemplazos
                List<Exception> Ex = new List<Exception>();
                List<SifLiqOrganismos> obj = Bl.BlLiqOrganismosGet(Convert.ToInt32(IdLiquidacion), IdOrganismoSup, out Ex);

                List<SifLiqOrganismos> objSort = LinqHelper.OrderBy(obj.AsQueryable(), jtSorting).ToList();
                total = obj.Count;
                obj = objSort.GetRange(jtStartIndex, (jtStartIndex + jtPageSize) > total ? (total - jtStartIndex) : jtPageSize);
                var retorno =
                   from o in obj.AsEnumerable()
                   select new
                   {
                       IdLiquidacion = o.IdLiquidacion,
                       IdLiqOrganismo = o.IdLiqOrganismo,
                       IdOrganismo = o.IdOrganismo,
                       TotalLiquidado = o.TotalLiquidado,
                       Cantidad = o.Cantidad,
                       RazonSocial = o.RazonSocial
                   };
                return new { Result = "OK", Records = retorno, TotalRecordCount = total };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetLiqOrganismosSup(int jtStartIndex, int jtPageSize, string pIdLiquidacion)
        {

            try
            {
                int IdLiquidacion;
                bool isNumeric = int.TryParse(pIdLiquidacion, out IdLiquidacion);
                if (!isNumeric)
                {
                    return new { Result = "ERROR", Message = "No se pudo obtener IdLiquidacion" };
                }


                List<Exception> oExcep = new List<Exception>();

                int total = 0;



                //TODO: Revisar reemplazos
                List<Exception> Ex = new List<Exception>();
                List<SifLiqOrganismos> obj = Bl.BlLiqOrganismosSupGet(Convert.ToInt32(IdLiquidacion), out Ex);

                List<SifLiqOrganismos> objSort = LinqHelper.OrderBy(obj.AsQueryable(), "IdOrganismo").ToList();
                total = obj.Count;
                obj = objSort.GetRange(jtStartIndex, (jtStartIndex + jtPageSize) > total ? (total - jtStartIndex) : jtPageSize);
                var retorno =
                   from o in obj.AsEnumerable()
                   select new
                   {
                       IdLiquidacion = o.IdLiquidacion,
                       IdOrganismo = o.IdOrganismo,
                       TotalLiquidado = o.TotalLiquidado,
                       Cantidad = o.Cantidad,
                       RazonSocial = o.RazonSocial
                   };
                return new { Result = "OK", Records = retorno, TotalRecordCount = total };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
        [WebMethod(EnableSession = true)]
        public static object GuardarDetalleLiquidacion(SifLiquidaciones record)
        {
            try
            {
                List<Exception> oExcep = new List<Exception>();
                //Parámetro FecResolucion

                DateTime FecRes;
                bool FecResOK;
                FecResOK = DateTime.TryParse(record.strFechaResolucion, out FecRes);

                if (FecResOK)
                    record.FechaResolucion = FecRes;
                else
                    return new { Result = "ERROR", Mensaje = "Error. Formato de fecha incorrecto", IdLiquidacion = 0 };
                // RetornoNeg ret = EmplazamientosCalderasNeg.Guardar(record);
                List<Exception> Ex = new List<Exception>();
                bool resultado = Bl.LiquidacionGrabarDatosExtras(record.IdLiquidacion, record.NroResolucion, record.FechaResolucion, record.NroExpediente, out Ex);
                if (resultado)
                {
                    // record.IdEmplazamientoCaldera = ret.Clave;
                    return new { Result = "OK" };
                }
                else
                {
                    return new { Result = "ERROR", Message = Ex[0].Message };
                }
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        //
        [WebMethod(EnableSession = true)]
        public static object BorrarUltimaLiquidacion(SifLiquidaciones record)
        {
            try
            {
                List<Exception> oExcep = new List<Exception>();

                bool resultado = Bl.BorrarLiquidacion(record.IdLiquidacion, out oExcep);


                if (resultado == true)
                    return new { Result = "OK" };
                else if (oExcep.Count > 0)
                {
                    return new { Result = "ERROR", Message = oExcep[0].Message };
                }
                else
                {
                    return new { Result = "ERROR", Message = "Sólo puede borrarse la última liquidación." };
                }

            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
        protected void btnPagConstruccion_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }

}