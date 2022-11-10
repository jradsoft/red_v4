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
using wpfEFac.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace wpfEFac.Views.PuntoVenta
{
    /// <summary>
    /// Lógica de interacción para SolicitudesCancelacion.xaml
    /// </summary>
    public partial class SolicitudesCancelacion : Window
    {
        String  keyB64, cerB64;
        string strFileCER = "";
        string strFileKEY = "";
        string strPasswd = "";

        string strFileCERpem = "";
        string strFileKEYpem = "";
        eFacDBEntities db = new eFacDBEntities();
        Certificates lstCert = null;

        public SolicitudesCancelacion()
        {
            InitializeComponent();
            keyB64 = "";
            cerB64 = "";



            lstCert = db.Certificates.FirstOrDefault();

            string pathMyFacturaE = lstCert.strCertificadoSelloDigitalPath;

            int posSlash = pathMyFacturaE.LastIndexOf("\\");
            pathMyFacturaE = pathMyFacturaE.Substring(0, posSlash + 1);

            strFileCER = lstCert.strCertificadoSelloDigitalPath;
            strFileKEY = lstCert.strLlaveCertificadoPath;
            strPasswd = lstCert.strContraseñaSAT;
            strFileCERpem = lstCert.strCertificadoSelloDigitalPath+".PEM";
            strFileKEYpem = lstCert.strLlaveCertificadoPath + ".PEM";



            ExecuteCommandSync(pathMyFacturaE + "openssl pkcs8 -inform DER -in " + strFileKEY + "  -out " + strFileKEYpem + " -passin pass:" + strPasswd);
            ExecuteCommandSync(pathMyFacturaE + "openssl x509 -in " + strFileCER + " -inform d -out " + strFileCERpem);




            consultaWS();
        }

        private void bttRechazar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bttAceptar_Click(object sender, RoutedEventArgs e)
        {

        }


        public void consultaWS() {


            try
            {

                keyB64 = File.ReadAllText(strFileKEYpem);


                 
                cerB64 = File.ReadAllText(strFileCERpem);

          
             

                ServicioTimbradoWS myService = new ServicioTimbradoWS();
                RespuestaPendientes response = myService.consultarAutorizacionesPendientes(UserPac.idEquipo,keyB64,cerB64);/*Produccion*/
                //RespuestaPendientes response = myService.consultarAutorizacionesPendientes("0955d485e26c486392909ee79f5ad5c3", keyB64, cerB64);/*Test*/

                string estatus = response.code + "/" + response.message;


                txbMesssage.Text = response.message;
                


            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex);

            }
        
        
        
        }


        private string ExecuteCommandSync(object command)
        {
            string result = "";

            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                //MessageBox.Show(result);
            }
            catch (Exception objException)
            {
                // Log the exception
            }

            return result;
        }
    }
}
