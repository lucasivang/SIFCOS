using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using NPOI.HSSF.UserModel;
using System.IO;
using BL_SIFCOS;
using DA_SIFCOS;
using DA_SIFCOS.Entidades;

namespace SIFCOS
{
    public partial class LiqTramites2Excel : System.Web.UI.Page
    {
        public static ReglaDeNegocios Bl = new ReglaDeNegocios();
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerarArchivoDetallado();
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object SetVarsReporte(string IdLiqOrg, string DescriFiltro, string NombreArchivo)
        {
            try
            {
                HttpContext.Current.Session.Add("IdLiqOrg", IdLiqOrg);
                HttpContext.Current.Session.Add("DescriFiltro", DescriFiltro);
                HttpContext.Current.Session.Add("NombreArchivo", NombreArchivo);
                HttpContext.Current.Session.Add("TipoReporte", "Boca");
                /*
                HttpContext.Current.Session.Add("pFechaDesde", FechaDesde);
                HttpContext.Current.Session.Add("pFechaHasta", FechaHasta);
                HttpContext.Current.Session.Add("pIdActividadClanae", IdActividadClanae);
                HttpContext.Current.Session.Add("pActividadClanae", ActividadClanae);
                HttpContext.Current.Session.Add("pMesDia", MesDia);
                */
                object ret = new { Result = "OK" };
                return ret;
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object SetVarsReporteBocaSup(string IdOrganismo, string IdLiquidacion, string DescriFiltro, string NombreArchivo)
        {
            try
            {
                HttpContext.Current.Session.Add("IdOrganismo", IdOrganismo);
                HttpContext.Current.Session.Add("IdLiquidacion", IdLiquidacion);
                HttpContext.Current.Session.Add("DescriFiltro", DescriFiltro);
                HttpContext.Current.Session.Add("NombreArchivo", NombreArchivo);
                HttpContext.Current.Session.Add("TipoReporte", "BocaSup");

                object ret = new { Result = "OK" };
                return ret;
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }
        private void GenerarArchivoDetallado()
        {
            try
            {
                List<Exception> oExcep = new List<Exception>();

                //Determino el tipo de reporte
                string tipoReporte = "";
                if (String.IsNullOrEmpty(HttpContext.Current.Session["TipoReporte"].ToString()))
                {
                    throw new Exception("Error. No se pudo determinar el tipo de reporte");
                }
                tipoReporte = HttpContext.Current.Session["TipoReporte"].ToString();
                
                string strDescriFiltro = "";
                string strNombreArchivo = "";
                string NombreAch = "";
                string Titulo = "";

                List<SifLiqTramites> obj = new List<SifLiqTramites>();
                if (!String.IsNullOrEmpty(HttpContext.Current.Session["DescriFiltro"].ToString()))
                    strDescriFiltro = HttpContext.Current.Session["DescriFiltro"].ToString();

                if (!String.IsNullOrEmpty(HttpContext.Current.Session["NombreArchivo"].ToString()))
                    strNombreArchivo = HttpContext.Current.Session["NombreArchivo"].ToString();

                NombreAch = strNombreArchivo;


                if (tipoReporte == "Boca")
                {
                    string strIdLiqOrg = "";
                    if (!String.IsNullOrEmpty(HttpContext.Current.Session["IdLiqOrg"].ToString()))
                        strIdLiqOrg = HttpContext.Current.Session["IdLiqOrg"].ToString();
              
                    //Parámetro IdLiqOrganismo
                    int IdLiqOrganismo = 0;
                    int IdLiqOrg;
                    bool IdLiqOrgOK;
                    IdLiqOrgOK = int.TryParse(strIdLiqOrg, out IdLiqOrg);

                    if (IdLiqOrgOK)
                        IdLiqOrganismo = IdLiqOrg;
                    List<Exception> Ex = new List<Exception>();
                    obj = Bl.BlLiqTramitesGet(IdLiqOrganismo, null, null, out Ex);
                }
                else if (tipoReporte == "BocaSup")
                {
                    //Tipo de reporte Boca superior
                    string strIdLiq = "";
                    if (!String.IsNullOrEmpty(HttpContext.Current.Session["IdLiquidacion"].ToString()))
                        strIdLiq = HttpContext.Current.Session["IdLiquidacion"].ToString();

                    //Parámetro IdLiquidacion
                    int IdLiquidacion = 0;
                    int IdLiq;
                    bool IdLiqOK;
                    IdLiqOK = int.TryParse(strIdLiq, out IdLiq);

                    if (IdLiqOK)
                        IdLiquidacion = IdLiq;

                    string strIdOrg = "";
                    if (!String.IsNullOrEmpty(HttpContext.Current.Session["IdOrganismo"].ToString()))
                        strIdOrg = HttpContext.Current.Session["IdOrganismo"].ToString();

                    //Parámetro IdOrganismo
                    int IdOrganismo = 0;
                    int IdOrg;
                    bool IdOrgOK;
                    IdOrgOK = int.TryParse(strIdOrg, out IdOrg);

                    if (IdOrgOK)
                        IdOrganismo = IdOrg;
                    
                    List<Exception> Ex = new List<Exception>();
                    obj = Bl.BlLiqTramitesGet(null, IdOrganismo, IdLiquidacion, out Ex);

                }
                else {
                    throw new Exception("Error. Tipo de reporte " + tipoReporte + " inválido");
                }




                if (oExcep.Count > 0)
                {
                    return;
                }

                var workbook = new HSSFWorkbook();
                var rowIndex = 0;
                var numPagina = 1;

                #region HeaderLabel Cell Style
                var headerLabelCellStyle = workbook.CreateCellStyle();
                //headerLabelCellStyle.Alignment = HorizontalAlignment.CENTER;
                headerLabelCellStyle.BorderBottom = NPOI.SS.UserModel.CellBorderType.MEDIUM;
                headerLabelCellStyle.BorderTop = NPOI.SS.UserModel.CellBorderType.MEDIUM;
                headerLabelCellStyle.BorderLeft = NPOI.SS.UserModel.CellBorderType.MEDIUM;
                headerLabelCellStyle.BorderRight = NPOI.SS.UserModel.CellBorderType.MEDIUM;
                var headerLabelFont = workbook.CreateFont();
                headerLabelFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.BOLD;
                headerLabelCellStyle.SetFont(headerLabelFont);
                #endregion

                rowIndex = 0;

                NPOI.SS.UserModel.Sheet sheetDetalle = workbook.CreateSheet(NombreAch);
                NPOI.SS.UserModel.Row rowprincipal;
                NPOI.SS.UserModel.Cell cellprincipal;
                NPOI.SS.UserModel.Row row;


                foreach (SifLiqTramites item in obj)
                {
                    if (rowIndex == 0)
                    {
                        //sheetDetalle = workbook.CreateSheet("Tramites");
                        //Armo Titulo Principal:
                        rowprincipal = sheetDetalle.CreateRow(rowIndex);
                        cellprincipal = rowprincipal.CreateCell(0);
                        cellprincipal.SetCellValue(Titulo);
                        //cellprincipal.CellStyle = bold;
                        cellprincipal.CellStyle = headerLabelCellStyle;
                        rowIndex++;
                        row = sheetDetalle.CreateRow(rowIndex);

                        row = sheetDetalle.CreateRow(rowIndex);

                        row.CreateCell(0).SetCellValue("Nro Sifcos");
                        row.CreateCell(1).SetCellValue("Cuit");
                        row.CreateCell(2).SetCellValue("Razon Social");
                        row.CreateCell(3).SetCellValue("NroTramite");

                        row.CreateCell(4).SetCellValue("Fec Ini Tram");
                        row.CreateCell(5).SetCellValue("Alta");
                        row.CreateCell(6).SetCellValue("Vencimiento");
                        row.CreateCell(7).SetCellValue("Importe");

                        row.CreateCell(8).SetCellValue("Calle");
                        row.CreateCell(9).SetCellValue("Altura");
                        row.CreateCell(10).SetCellValue("Piso");
                        row.CreateCell(11).SetCellValue("Depto");
                        row.CreateCell(12).SetCellValue("Local");
                        row.CreateCell(13).SetCellValue("Stand");
                        row.CreateCell(14).SetCellValue("Torre");
                        row.CreateCell(15).SetCellValue("Manzana");
                        row.CreateCell(16).SetCellValue("Lote");
                        row.CreateCell(17).SetCellValue("Cpa");
                        row.CreateCell(18).SetCellValue("Boca");
                        row.CreateCell(19).SetCellValue("CuitBoca");
                        row.CreateCell(20).SetCellValue("BocaSuperior");
                        row.CreateCell(21).SetCellValue("CuitBocaSuperior");
                        row.CreateCell(22).SetCellValue("NroLiqOriginal");
                        rowIndex++;
                    }

                    row = sheetDetalle.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(item.NroSifcos);
                    row.CreateCell(1).SetCellValue(item.Cuit);
                    row.CreateCell(2).SetCellValue(item.RazonSocial);
                    row.CreateCell(3).SetCellValue(item.NroTramiteSifcos);

                    row.CreateCell(4).SetCellValue(item.FecIniTramite.ToString("dd/MM/yyyy"));
                    row.CreateCell(5).SetCellValue(item.FecAlta.ToString("dd/MM/yyyy"));
                    row.CreateCell(6).SetCellValue(item.FecVencimiento.ToString("dd/MM/yyyy"));
                    row.CreateCell(7).SetCellValue(item.MontoLiquidado.ToString());

                    row.CreateCell(8).SetCellValue(item.Calle);
                    row.CreateCell(9).SetCellValue((item.Altura.HasValue)?item.Altura.ToString():"");
                    row.CreateCell(10).SetCellValue(item.Piso);
                    row.CreateCell(11).SetCellValue(item.Depto);
                    row.CreateCell(12).SetCellValue(item.Local);
                    row.CreateCell(13).SetCellValue(item.Torre);
                    row.CreateCell(14).SetCellValue(item.Mzna);
                    row.CreateCell(15).SetCellValue(item.Lote);
                    row.CreateCell(16).SetCellValue(item.Localidad);
                    row.CreateCell(17).SetCellValue(item.Cpa);
                    row.CreateCell(18).SetCellValue(item.Boca);
                    row.CreateCell(19).SetCellValue(item.CuitBoca);
                    row.CreateCell(20).SetCellValue(item.BocaSuperior);
                    row.CreateCell(21).SetCellValue(item.CuitBocaSuperior);
                    row.CreateCell(22).SetCellValue(item.NroLiquidacionOriginal);

                     rowIndex++;

                    //Evaluo si llego a 30.000, para agregar otra hoja:
                    if (rowIndex >= 30000)
                    {
                        numPagina++;
                        sheetDetalle = workbook.CreateSheet(NombreAch + " Hoja " + numPagina.ToString());
                        rowIndex = 0;
                    }
                }

                // Add row indicating date/time report was generated...


                sheetDetalle.CreateRow(rowIndex + 1).CreateCell(0).SetCellValue(strDescriFiltro);
                //sheetDetalle.CreateRow(rowIndex + 2).CreateCell(0).SetCellValue("Completo en NO significa solo dibujo de oblea. En SI significa dibujo de oblea y acceso a través de ella al panel público del comercio");
                sheetDetalle.CreateRow(rowIndex + 3).CreateCell(0).SetCellValue("Reporte generado el " + DateTime.Now.ToString());


                //---------------------------------------------------
                //---------------------------------------------------
                //Grabacion del Archivo:
                // Save the Excel spreadsheet to a MemoryStream and return it to the client
                using (var exportData = new MemoryStream())
                {
                    workbook.Write(exportData);

                    string saveAsFileName = NombreAch + " " +
                                            string.Format("{0:d}.xls", DateTime.Now).Replace("/", "-");

                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                    Response.Clear();
                    Response.BinaryWrite(exportData.GetBuffer());
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                return;
            }

        }
    }
}