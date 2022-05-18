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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Windows.Controls.Primitives;
using System.Net.Mail;
using wpfEFac.Models;

namespace wpfEFac.Views.ReporteMensual
{
    /// <summary>
    /// Interaction logic for ReporteMensual.xaml
    /// </summary>
    public partial class ReporteMensual : Page
    {
        public ReporteMensual()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.dtpFechaReporte.SelectedDate = DateTime.Now;
            this.dtpFechaReporteFin.SelectedDate = DateTime.Now;
        }

        private void dtpFechaReporte_Loaded(object sender, RoutedEventArgs e)
        {
            FieldInfo fiTextBox = dtpFechaReporte.GetType()
                .GetField("_textBox", BindingFlags.Instance | BindingFlags.NonPublic);

            if (fiTextBox != null)
            {
                DatePickerTextBox dateTextBox =
                  (DatePickerTextBox)fiTextBox.GetValue(dtpFechaReporte);

                if (dateTextBox != null)
                {
                    PropertyInfo piWatermark = dateTextBox.GetType()
                      .GetProperty("Watermark", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (piWatermark != null)
                    {
                        piWatermark.SetValue(dateTextBox, "Selecciona fecha", null);
                    }
                }
            }
        }

        private void dtpFechaReporteFin_Loaded(object sender, RoutedEventArgs e)
        {
            FieldInfo fiTextBox = dtpFechaReporteFin.GetType()
                .GetField("_textBox", BindingFlags.Instance | BindingFlags.NonPublic);

            if (fiTextBox != null)
            {
                DatePickerTextBox dateTextBox =
                  (DatePickerTextBox)fiTextBox.GetValue(dtpFechaReporteFin);

                if (dateTextBox != null)
                {
                    PropertyInfo piWatermark = dateTextBox.GetType()
                      .GetProperty("Watermark", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (piWatermark != null)
                    {
                        piWatermark.SetValue(dateTextBox, "Selecciona fecha", null);
                    }
                }
            }
        }

        private void bttGenerar_Click(object sender, RoutedEventArgs e)
        {
            wpfEFac.Models.ReporteMensual rm = new Models.ReporteMensual();

            wpfEFac.Models.Usuarios iduser;
            wpfEFac.Models.Login user = new wpfEFac.Models.Login();
            iduser = user.GetUsuario(Convert.ToInt32(App.Current.Properties["idUsuario"]));
            bool enviarMail = false;
            int valueReport =0;


            if (repVentasCancelacion.IsChecked == true) {
                valueReport = 1;
            }


            if (dtpFechaReporte.SelectedDate.HasValue)
            {
                var reportData = rm.GetReporteData(dtpFechaReporte.SelectedDate.Value, dtpFechaReporteFin.SelectedDate.Value, iduser.Empresa.intID, valueReport); ;
                var reportDataVentas = rm.GetReporteDataVentas(dtpFechaReporte.SelectedDate.Value, dtpFechaReporteFin.SelectedDate.Value, iduser.Empresa.intID, valueReport);

                //if (repVentas.IsChecked == true) 
                //reportDataVentas = rm.GetReporteDataVentas(dtpFechaReporte.SelectedDate.Value, iduser.Empresa.intID);

                if (chkEnviarMail.IsChecked == true)
                    enviarMail = true;

                if (repMensual.IsChecked == true)

                    if (reportDataVentas.Count > 0)
                        rm.GenerateMonthReport(reportData, 0, enviarMail);
                    else
                        MessageBox.Show("No existen facturas en este periodo...");
                else
                {
                    if (reportDataVentas.Count > 0)
                    {
                        string fileVentas = rm.GenerateMonthReportVentas(reportDataVentas, 1, enviarMail);
                        if ((repVentas.IsChecked == true) && (enviarMail))
                            sendMail(fileVentas, "adsoft@live.com.mx");
                    }
                    else
                        MessageBox.Show("No existen facturas en este periodo...");
                }

                //rm.GenerateMonthReport
            }
        }


        private int sendMail(string xmlPath, string mailContador)
        {

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential("adesoft.cfdi@gmail.com", "superputote");

            mail.To.Add("adesoft@live.com.mx");



            
            if (mailContador != String.Empty) mail.CC.Add(mailContador);
            


            /*
             * Attachments
             */

            
            mail.Attachments.Add(new Attachment(xmlPath));

            string Asunto = "Reporte Mensual de Facturación ";

            
            mail.Subject = Asunto;

            mail.From = new System.Net.Mail.MailAddress("adesoft.cfdi@gmail.com");
            mail.IsBodyHtml = true;
            mail.Body = "Servicio proporcionado por ADESOFT SA de CV" + " www.adesoft.com.mx";

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            try
            {
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
            
            return 0;

        }

        private void dtpFechaReporteFin_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

    }
}
