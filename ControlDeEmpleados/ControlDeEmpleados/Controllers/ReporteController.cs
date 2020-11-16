
using ControlDeEmpleados.Reportes;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlDeEmpleados.Models;
namespace ControlDeEmpleados.Controllers
{
    public class ReporteController : Controller
    {
        QuickfatControlEmplEntities1 db = new QuickfatControlEmplEntities1();

        public ActionResult ReportViewer()
        {

            ViewData["reportUrl"] = "../Reportes/View/local/rpt_AsociadosAudiencias/";
            return View();
        }

        public ActionResult Impresion()
        {
            var obj = db.Configuracion.Where(x => x.Nombre == "Nombre de La empresa").ToList().FirstOrDefault();
            Globales.Empresa = obj.Valor;
            var request = Request;
            ReportData reportData = new ReportData();

            //Construccion de clase reportData, que encapsula los parametros del reporte
            var mode = (request.QueryString["rptmode"] + "").Trim();
            reportData.IsLocal = mode == "local" ? true : false;
            reportData.ReportName = request.QueryString["reportname"] + "";
            string dquerystr = request.QueryString["parameters"] + "";
            if (!String.IsNullOrEmpty(dquerystr.Trim()))
            {
                var param1 = dquerystr.Split(',');
                foreach (string pm in param1)
                {
                    var rp = new Parameter();
                    var kd = pm.Split('=');
                    if (kd[0].Substring(0, 2) == "rp")
                    {
                        rp.ParameterName = kd[0].Replace("rp", "");
                        if (kd.Length > 1)
                            rp.Value = kd[1];
                        reportData.ReportParameters.Add(rp);
                    }
                    else if (kd[0].Substring(0, 2) == "dp")
                    {
                        rp.ParameterName = kd[0].Replace("dp", "");
                        if (kd.Length > 1)
                            rp.Value = kd[1];
                        reportData.DataParameters.Add(rp);
                    }
                }
            }

            string path = Path.Combine(Server.MapPath("~/Reportes/Reports"), reportData.ReportName + ".rdlc");
            LocalReport reporte = new LocalReport();
            reporte.ReportPath = path;
            //Se habilita el uso de imganes externa
            reporte.EnableExternalImages = true;
            // Load the dataSource.            
            List<ReportDataSource> source;
            DataSetsQueries.setDataSource(out source, ref reportData);
            foreach (var item in source)
            {
                reporte.DataSources.Add(item);
            }

            // Set report parameters.
            var rpPms = reporte.GetParameters();
            foreach (var rpm in rpPms)
            {
                var p = reportData.ReportParameters.SingleOrDefault(o => o.ParameterName.ToLower() == rpm.Name.ToLower());
                if (p != null)
                {
                    ReportParameter rp = new ReportParameter(rpm.Name, p.Value);
                    reporte.SetParameters(rp);
                }
            }

            if (Globales.ReporteNombre == true)
            {
                reporte.SetParameters(new ReportParameter("NombreReporteMostrar", reportData.ReportName));
               
            }
            else
            {
                reporte.SetParameters(new ReportParameter("NombreReporteMostrar", ""));
            }

            reporte.SetParameters(new ReportParameter("Nombre_Empresa",Globales.Empresa));
            //reporte.SetParameters(new ReportParameter("Nombre_Empresa", Globales.Empresa));

            try
            {
                reporte.SetParameters(new ReportParameter("LogoImagenPath", Globales.LogoPath));
            }
            catch
            {
            }

            try
            {
                reporte.SetParameters(new ReportParameter("FirmaReciboPath", Globales.LogoPathFirmaRecibos));
            }
            catch
            {
            }

            //Construccion del pdf            
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo;
            bool Recibos = false;
            bool Landscape = false;
            bool Ajustado = false;
            bool Oficio_lateral = false;
            bool Ajustado2 = false;
            bool Landscape2 = false;
            bool Ajustado3 = false;
            bool SinMargenes = false;
            switch (reportData.ReportName)
            {
                case "rpt_Cotizaciones":
                    Ajustado = true;
                    break;
               




                default:
                    Landscape = false;
                    break;

            }
            if (Landscape)
            {
                deviceInfo =

           "<DeviceInfo>" +
           "  <OutputFormat>PDF</OutputFormat>" +
           "  <PageWidth>27.94cm</PageWidth>" +
           "  <PageHeight>21.59cm</PageHeight>" +
           "  <MarginTop>0.6cm</MarginTop>" +
           "  <MarginLeft>1.0cm</MarginLeft>" +
           "  <MarginRight>0.00cm</MarginRight>" +
           "  <MarginBottom>0.00cm</MarginBottom>" +
           "</DeviceInfo>";
            }
            else if (Landscape2)
            {
                deviceInfo =

           "<DeviceInfo>" +
           "  <OutputFormat>PDF</OutputFormat>" +
           "  <PageWidth>27.94cm</PageWidth>" +
           "  <PageHeight>21.59cm</PageHeight>" +
           "  <MarginTop>0.6cm</MarginTop>" +
           "  <MarginLeft>0.6cm</MarginLeft>" +
           "  <MarginRight>0.00cm</MarginRight>" +
           "  <MarginBottom>0.00cm</MarginBottom>" +
           "</DeviceInfo>";
            }
            else if (Ajustado)
            {
                deviceInfo =

           "<DeviceInfo>" +
           "  <OutputFormat>PDF</OutputFormat>" +
           "  <PageWidth>21.59cm</PageWidth>" +
           "  <PageHeight>27.94cm</PageHeight>" +
           "  <MarginTop>0.5cm</MarginTop>" +
           "  <MarginLeft>0.5cm</MarginLeft>" +
           "  <MarginRight>0.5cm</MarginRight>" +
           "  <MarginBottom>0.5cm</MarginBottom>" +
           "</DeviceInfo>";
            }
            else if (Oficio_lateral)
            {
                deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>35.56cm</PageWidth>" +
            "  <PageHeight>21.59cm</PageHeight>" +
            "  <MarginTop>0.5cm</MarginTop>" +
            "  <MarginLeft>0.5cm</MarginLeft>" +
            "  <MarginRight>0.5cm</MarginRight>" +
            "  <MarginBottom>0.5cm</MarginBottom>" +
            "</DeviceInfo>";
            }
            else if (Ajustado2)
            {
                deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>21.59cm</PageWidth>" +
                "  <PageHeight>27.94cm</PageHeight>" +
                "  <MarginTop>1.5cm</MarginTop>" +
                "  <MarginLeft>1.5cm</MarginLeft>" +
                "  <MarginRight>1.5cm</MarginRight>" +
                "  <MarginBottom>1.5cm</MarginBottom>" +
                "</DeviceInfo>";
            }
            else if (Recibos)
            {
                deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>14.00cm</PageWidth>" +
                "  <PageHeight>10.80cm</PageHeight>" +
                "  <MarginTop>0.5cm</MarginTop>" +
                "  <MarginLeft>0.5cm</MarginLeft>" +
                "  <MarginRight>0.5cm</MarginRight>" +
                "  <MarginBottom>0.5cm</MarginBottom>" +
                "</DeviceInfo>";
            }
            // Ismar Romero paralas facturas.
            else if (Ajustado3)
            {
                deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>21.59cm</PageWidth>" +
                "  <PageHeight>27.94cm</PageHeight>" +
                "  <MarginTop>0.3cm</MarginTop>" +
                "  <MarginLeft>2cm</MarginLeft>" +
                "  <MarginRight>2m</MarginRight>" +
                "  <MarginBottom>0.3cm</MarginBottom>" +
                "</DeviceInfo>";
            }
            else if (SinMargenes)
            {
                deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>27.94cm</PageWidth>" +
                "  <PageHeight>11.59.94cm</PageHeight>" +
                "  <MarginTop>0.5cm</MarginTop>" +
                "  <MarginLeft>0.5cm</MarginLeft>" +
                "  <MarginRight>0.5cm</MarginRight>" +
                "  <MarginBottom>0.5cm</MarginBottom>" +
                "</DeviceInfo>";
            }

            else
            {
                deviceInfo =
           "<DeviceInfo>" +
           "  <OutputFormat>PDF</OutputFormat>" +
           "  <PageWidth>21.59cm</PageWidth>" +
           "  <PageHeight>27.94cm</PageHeight>" +
           "  <MarginTop>2cm</MarginTop>" +
           "  <MarginLeft>2cm</MarginLeft>" +
           "  <MarginRight>2cm</MarginRight>" +
           "  <MarginBottom>2cm</MarginBottom>" +
           "</DeviceInfo>";
            }

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = reporte.Render(
               "PDF",
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }
    }
}