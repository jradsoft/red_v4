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

using cancelacion;
using System.Xml.Serialization;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

namespace wpfEFac.Views.PuntoVenta
{
    /// <summary>
    /// Interaction logic for CancelaCFDi.xaml
    /// </summary>
    public partial class CancelaCFDi : Window
    {
        string UUID;
        string passwdCer = "";
        byte[] certificado;
        Factura myfi;
        String acuseCancelacion;
        string strPasswd = "";
        string strFileCER = "";
        string strFileKEY = "";
        decimal dcmTotal = 0;
        string keyB64 = "";
        string cerB64 = "";

        public CancelaCFDi(Factura f)
        {
            InitializeComponent();
            string xmlPath = f.strXMLpath;
            string strXML;
            string pathMyFacturaE = f.Certificates.strCertificadoSelloDigitalPath;
           

             strPasswd = f.Certificates.strContraseñaSAT;
             strFileCER = f.Certificates.strCertificadoSelloDigitalPath;
             strFileKEY = f.Certificates.strLlaveCertificadoPath;
      
            
            try
            {
                this.myfi = f;
                int posSlash = pathMyFacturaE.LastIndexOf("\\");
                pathMyFacturaE = pathMyFacturaE.Substring(0, posSlash+1);

               

                try
                {
                    //certificado = System.IO.File.ReadAllBytes(pathMyFacturaE + "cert.pfx");

                   
                    keyB64 = System.Convert.ToBase64String(
                                System.IO.File.ReadAllBytes(strFileKEY)
                            );

                    cerB64 = System.Convert.ToBase64String(
                               System.IO.File.ReadAllBytes(strFileCER)
                       );
                }
                catch (Exception e)
                {

                    

                    //string strFileCERPEM = strFileCER + ".PEM";
                    //string strFileKEYPEM = strFileKEY + ".PEM";
                    
                    
                    
                    //ExecuteCommandSync(pathMyFacturaE + "openssl pkcs8 -inform DER -in " + strFileKEY + "  -out " + strFileKEYPEM + " -passin pass:" + strPasswd);
                    //ExecuteCommandSync(pathMyFacturaE + "openssl x509 -in " + strFileCER + " -inform d -out " + strFileCERPEM);

                    //ExecuteCommandSync(pathMyFacturaE + "openssl pkcs12 -export -in " + strFileCERPEM + " -inkey " + strFileKEYPEM + " -passin pass:" + strPasswd + " -passout pass:adsoft -out " + pathMyFacturaE + "cert.pfx");

                    //certificado = System.IO.File.ReadAllBytes(pathMyFacturaE + "cert.pfx");
                    
                }
            
                //certificado = File.ReadAllBytes("c:\\myfacturae\\cert.zip");

                try
                {

                    txtRFC.Text = f.Empresa.strRFC;
                    txtRFCreceptor.Text = f.Clientes.strRFC;
                    txtPasswd.Text = "adsoft";



                    //strXML = System.IO.File.ReadAllText(f.strXMLpath, UTF8Encoding.UTF8);
                    //int pos = strXML.IndexOf("UUID=");
                    //UUID = strXML.Substring(pos + 6, 36);




                    txtUUID.Text = f.strSelloDigital;// UUID; //myTFD[2].Value;
                    txtTotal2.Text = f.dcmTotal.ToString();



                }
                catch (Exception e)
                {
                    MessageBox.Show("No se encontro XML, teclear UUID...");
                }



            }
            catch(Exception ex)
            {
                MessageBox.Show("No se encontro el certificado cert.zip en la carpeta del sistema");
                
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

        private void cmdCancelarCFDi_Click(object sender, RoutedEventArgs e)
        {
            //CFDiService myService = new CFDiService();  Edicom
            //SIFEIService myService = new SIFEIService();  //Sifei Pruebas

          //  SIFEIService myService = new SIFEIService();

            ServicioTimbradoWS myService = new ServicioTimbradoWS();

           // Cancelacion myService = new Cancelacion();
            string strRFC = UserPac.rfc;
            string strPass = UserPac.passwd;
            string strUs = UserPac.user;
            string strEquipo = UserPac.idEquipo;
          
        //    CancelaResponse acuseCancelacion = null;     Edicom
            string[] uuid = new string[1];
            uuid[0] = txtUUID.Text;
            string strUUID = txtUUID.Text;
            string rfc = txtRFC.Text;
            passwdCer = txtPasswd.Text;
            string strRfcReceptor = txtRFCreceptor.Text;
            double dcmTotal = double.Parse(txtTotal2.Text);

            string strMotivo = cmbMotivoCancelacion.Text.Substring(0, 2);
            string uuidRelacion = txtRelacionUUID.Text.Trim();
          //  myService.cancelaCFDi("ADE1004192v6", "putote", "ADE1004192V6", uuid, fiel, "passwd");



     

                try
                {






                    RespuestaCancelar acuseCancelacion = myService.cancelar2(strEquipo, keyB64, cerB64, strPasswd, strUUID, rfc, strRfcReceptor, dcmTotal, strMotivo, uuidRelacion); /*Produccion*/
                   // RespuestaCancelar acuseCancelacion = myService.cancelar2("0955d485e26c486392909ee79f5ad5c3", keyB64, cerB64, strPasswd, strUUID, rfc, strRfcReceptor, dcmTotal, strMotivo, uuidRelacion); /*Test*/




                    if (acuseCancelacion.code == "201")
                    {

                        MessageBox.Show("UUID Cancelado...");

                        sendMail(
                                      myfi.Empresa.strTelefono,
                                      myfi.Empresa.strTelefono2,
                                       uuid[0],
                                       myfi.Empresa.strNombreComercial,
                                       myfi.strSerie,
                                       myfi.strFolio,
                                       myfi.Clientes.strEmail,
                                       myfi.Empresa.strEmail,
                                       myfi.Empresa.strEmail2,
                                       acuseCancelacion.message,
                                       cmbMotivoCancelacion.Text,
                                       txtRelacionUUID.Text
                                       );

                        DialogResult = true;



                    }
                    else
                    {

                        MessageBox.Show("Verifique codigo de error..." + acuseCancelacion.message);

                    }

                  

                  

                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    DialogResult = false;
                }


        }



        private int sendMail(
            string emailEmpresa,
            string passEmailEmpresa,
            string uuidCancelado,
            string empresa, string Serie, String Folio,
            string mailCliente, string mailRespaldo, string mailContador, string acuseWS, string strMotivo, string uuidRelacionado)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential(emailEmpresa, passEmailEmpresa);


            if (mailCliente != String.Empty) mail.CC.Add(mailCliente);
            if (mailContador != String.Empty) mail.CC.Add(mailContador);
            if (mailRespaldo != String.Empty) mail.CC.Add(mailRespaldo);


            string Asunto = empresa + " - Servicio de Cancelación CFDi ";

            if ((Serie != String.Empty) || (Folio != String.Empty))
                Asunto += Serie + Folio;


            mail.Subject = Asunto;

            mail.From = new System.Net.Mail.MailAddress(emailEmpresa);
            mail.IsBodyHtml = true;

            DateTime fechaCancelacion = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss"));


            mail.Body = "<b> <center>" + "Acuse de cancelación " + " </b> </center>" +
                        "Fecha y hora de cancelación : " + fechaCancelacion + "<br>" + "<br>" +
                        "UUID Cancelado : " + uuidCancelado + "<br>" + "<br>" +
                        "Motivo : " + strMotivo + "<br>" + "<br>" +
                        "UUID Relacionado : " + uuidRelacionado + "<br>" + "<br>" +


                        "Sello Digital del SAT : " + acuseWS + "<br>" + "<br>" +


                "Servicio proporcionado por ADESOFT" + " ";

            // mySendMail.AttachmentPath1 = xmlPath;
            //mySendMail.AttachmentPath2 = pdfPath;


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

        public string getCertificado(string strFileCER, string strPasswd2)
        {
            /*
             * Obtencion del CERTIFICADO
             */

            System.Security.Cryptography.X509Certificates.X509Certificate2 objCert =
                new System.Security.Cryptography.X509Certificates.X509Certificate2(strFileCER, strPasswd2);

            //objCert.RawData.ToString();



            return (Convert.ToBase64String(objCert.RawData));
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public dlleFac.Comprobante DeserializeCFD32(string xmlFile)
        {


            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new
            XmlSerializer(typeof(dlleFac.Comprobante));

            // A FileStream is needed to read the XML document.

            FileStream fs = new FileStream(xmlFile, FileMode.Open);
            XmlReader reader = new XmlTextReader(fs);

            // Declare an object variable of the type to be deserialized.
            dlleFac.Comprobante myComprobante = null;

            // Use the Deserialize method to restore the object's state.
            try
            {

                myComprobante = (dlleFac.Comprobante)serializer.Deserialize(reader);

            }
            catch (Exception e)
            {

            }
            fs.Close();
            reader.Close();

            return myComprobante;

        }

        private void cmdSalir_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
