using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI;
using DA_SIFCOS.Entidades;
using Microsoft.Reporting.WebForms;

namespace SIFCOS
{
    /// <summary>
    /// Página general para mostrar reportes
    /// </summary>
    public partial class ReporteGeneral2 : System.Web.UI.Page
    {
      
        
        private TipoArchivoEnum _tipoArchivo;

        private string _nombreReporte;

        private string _nombreArchivo;

        private List<ReportParameter> _listaParametros;

        private List<ReportDataSource> _listaDataSources;

        

        public virtual string NombreReporte
        {
            get
            {
                return _nombreReporte;
            }
            set
            {
                _nombreReporte = value;
            }
        }

        public virtual string NombreArchivo
        {
            get
            {
                return _nombreArchivo;
            }
            set
            {
                _nombreArchivo = value;
            }
        }

        public virtual List<ReportParameter> ListaParametros
        {
            get
            {
                return _listaParametros;
            }
            set
            {
                _listaParametros = value;
            }
        }

        public virtual List<ReportDataSource> ListaDataSources
        {
            get
            {
                return _listaDataSources;
            }
            set
            {
                _listaDataSources = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReporteGeneral2 reporteGeneral2 = (ReporteGeneral2)Session["ReporteGeneral2"];
                if (reporteGeneral2 != null)
                {
                    ImprimirReporteGeneral(reporteGeneral2);
                }
            }
        }

        public ReporteGeneral2(string nombreReporte, TipoArchivoEnum tipo, string nombreArchivo)
        {
            _nombreReporte = nombreReporte;
            _listaParametros = new List<ReportParameter>();
            _listaDataSources = new List<ReportDataSource>();
            _tipoArchivo = tipo;
            _nombreArchivo = nombreArchivo;
        }

        public ReporteGeneral2()
        {
            _listaParametros = new List<ReportParameter>();
        }

        public void AddParameter(string nombreParametro, string value)
        {
            _listaParametros.Add(new ReportParameter(nombreParametro, value));
        }

        public void AddDataSource(string nombreDataSource, object dataSource)
        {
            _listaDataSources.Add(new ReportDataSource(nombreDataSource, dataSource));
        }

        private void ImprimirReporteGeneral(ReporteGeneral2 reporteGral)
        {
            Assembly assembly = Assembly.Load("SIFCOS");
            Stream manifestResourceStream = assembly.GetManifestResourceStream(reporteGral._nombreReporte);
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.Visible = true;
            reportViewer.Page = (Page)HttpContext.Current.Handler;
            reportViewer.ID = "Test";
            reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.LoadReportDefinition((Stream)(object)manifestResourceStream);
            if (reporteGral._listaParametros != null)
            {
                reportViewer.LocalReport.SetParameters((IEnumerable<ReportParameter>)reporteGral._listaParametros);
            }
            ((Collection<ReportDataSource>)(object)reportViewer.LocalReport.DataSources).Clear();
            if (reporteGral.ListaDataSources != null)
            {
                foreach (ReportDataSource listaDataSource in reporteGral.ListaDataSources)
                {
                    ((Collection<ReportDataSource>)(object)reportViewer.LocalReport.DataSources).Add(listaDataSource);
                }
                reportViewer.DataBind();
            }
            reportViewer.LocalReport.Refresh();
            string format = "";
            string text = "";
            if (!string.IsNullOrEmpty(reporteGral._nombreArchivo))
            {
                text = reporteGral._nombreArchivo;
            }
            switch (reporteGral._tipoArchivo)
            {

                case TipoArchivoEnum.Pdf:
                    format = "PDF";
                    text = "InvitacionPremiosAlComercio_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                    base.Response.AddHeader("content-disposition", "attachment; filename=" + text + ".pdf");
                    break;

            }
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] buffer = reportViewer.LocalReport.Render(format, null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            base.Response.ContentType = mimeType;
            base.Response.BinaryWrite(buffer);
            base.Response.Buffer = true;
            base.Response.End();
        }
	}
}