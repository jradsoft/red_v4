using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

using wpfEFac.Models; 

namespace wpfEFac.Views.Reports
{
    /// <summary>
    /// Interaction logic for CFD.xaml
    /// </summary>
    public partial class CFD : Window
    {

        wpfEFac.Helpers.CFDReporte reporte;

        public CFD(wpfEFac.Helpers.CFDReporte reporte)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(CFD_Loaded);
            PrepareReport(reporte);
            this.reporte = reporte;
        }

        private void CFD_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshReport();
        }

        private void RefreshReport()
        {
            this.viewerInstance.RefreshReport();
        }

        private void PrepareReport(Helpers.CFDReporte reporte)
        {
            Microsoft.Reporting.WinForms.ReportDataSource CFDDataSource = new ReportDataSource("CDFDataSource", new List<Helpers.CFDReporte>(){ reporte });

            Microsoft.Reporting.WinForms.ReportDataSource Conceptos = new ReportDataSource("ConceptosDataSet", reporte.GetConceptos());
            
            this.viewerInstance.ProcessingMode = ProcessingMode.Local;

            //this.viewerInstance.LocalReport.ReportEmbeddedResource = "wpfEFac.Reports.ComprobanteFiscalDigital.rdlc";
            this.viewerInstance.LocalReport.ReportEmbeddedResource = "wpfEFac.Reports.ReciboArrendamiento.rdlc";
            this.viewerInstance.LocalReport.EnableExternalImages = true;
            this.viewerInstance.LocalReport.DataSources.Add(CFDDataSource);
            this.viewerInstance.LocalReport.DataSources.Add(Conceptos);

        }

        public void DoPDF(Helpers.CFDReporte reporte) 
        {
            //Create ReportViewer
            Microsoft.Reporting.WinForms.ReportViewer viewer = new Microsoft.Reporting.WinForms.ReportViewer();

            Microsoft.Reporting.WinForms.ReportDataSource CFDDataSource = new ReportDataSource("CDFDataSource", new List<Helpers.CFDReporte>() { reporte });

            Microsoft.Reporting.WinForms.ReportDataSource Conceptos = new ReportDataSource("ConceptosDataSet", reporte.GetConceptos());

            //viewer.LocalReport.ReportEmbeddedResource = "wpfEFac.Reports.ComprobanteFiscalDigital.rdlc";
            viewer.LocalReport.ReportEmbeddedResource = "wpfEFac.Reports.ReciboArrendamiento.rdlc";
            viewer.LocalReport.EnableExternalImages = true;
            viewer.LocalReport.DataSources.Add(CFDDataSource);
            viewer.LocalReport.DataSources.Add(Conceptos);

            //Export to PDF
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Microsoft.Reporting.WinForms.Warning[] warnings;

            byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "CFD" + "_" + reporte.RFCEmisor + "_" + (reporte.Fecha.Split('T')[0].Replace("Fecha: ",string.Empty).Replace("-","_")); // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "Documentos PDF (.pdf)|*.pdf"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                //Creatr PDF file on disk
                string pdfPath = @filename;
                System.IO.FileStream pdfFile = new System.IO.FileStream(pdfPath, System.IO.FileMode.Create);
                pdfFile.Write(pdfContent, 0, pdfContent.Length);
                pdfFile.Close();

                //Open PDF file
                System.Diagnostics.Process.Start(pdfPath);

            }

            
        }


        public string DoPDF(Helpers.CFDReporte infoReporte, string p)
        {
            Microsoft.Reporting.WinForms.ReportViewer viewer = new Microsoft.Reporting.WinForms.ReportViewer();

            Microsoft.Reporting.WinForms.ReportDataSource CFDDataSource = new ReportDataSource("CDFDataSource", new List<Helpers.CFDReporte>() { reporte });

            Microsoft.Reporting.WinForms.ReportDataSource Conceptos = new ReportDataSource("ConceptosDataSet", reporte.GetConceptos());

            //viewer.LocalReport.ReportEmbeddedResource = "wpfEFac.Reports.ComprobanteFiscalDigital.rdlc";
            viewer.LocalReport.ReportEmbeddedResource = "wpfEFac.Reports.ReciboArrendamiento.rdlc";
            viewer.LocalReport.EnableExternalImages = true;
            viewer.LocalReport.DataSources.Add(CFDDataSource);
            viewer.LocalReport.DataSources.Add(Conceptos);

            //Export to PDF
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Microsoft.Reporting.WinForms.Warning[] warnings;

            byte[] pdfContent = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            string pdfPath = @p + ".pdf";
            System.IO.FileStream pdfFile = new System.IO.FileStream(pdfPath, System.IO.FileMode.Create);
            pdfFile.Write(pdfContent, 0, pdfContent.Length);
            pdfFile.Close();

            return pdfPath;
        }
    }
}
