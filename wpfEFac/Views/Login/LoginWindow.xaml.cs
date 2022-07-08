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
using wpfEFac.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;

namespace wpfEFac.Views.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginViewModel ctx;
        private eFacDBEntities db;
        public LoginWindow()
        {
            InitializeComponent();
            ctx = new LoginViewModel();
            db = new eFacDBEntities();
           
            try
            {
                cmbEmpresa.ItemsSource = ctx.GetEmpresas();
                cmbEmpresa.SelectedValuePath = "intID";
                cmbEmpresa.DisplayMemberPath = "strRazonSocial";

                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedIndex = 0;
                }
                
                getFolios();
                getCFD();
                getProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void bttOK_Click(object sender, RoutedEventArgs e)
        {
            int idUsuario;
            
            if (ValidarLogin())
            {
                try
                {
                    
                    int idEmpresa = int.Parse(cmbEmpresa.SelectedValue.ToString());
                    idUsuario = ctx.DoLogin(
                        txtUsuario.Text,
                        pwbContraseña.Password,
                        idEmpresa
                        );

                    getUser();
                }
                catch (Exception)
                {
                    idUsuario = default(int);
                }

                if (idUsuario != 0)
                {
                    this.Topmost = false;
                    App.Current.Properties["idUsuario"] = idUsuario;
                    App.Current.Properties["idEmpresa"] = cmbEmpresa.SelectedValue;
                    MainWindow mw = new MainWindow();
                    mw.Owner = this;
                    mw.Show();
                    this.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("El usuario no existe por favor verifique sus datos", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } 
        }

        private bool ValidarLogin()
        {
            return (ValidarUsuario() &&
            ValidarPassword());
        }

        private bool ValidarPassword()
        {
            if (string.IsNullOrWhiteSpace(pwbContraseña.Password))
            {
                lblPassword.Content = "*Debes introducir la contraseña de Usuario";
                lblPassword.Foreground = new SolidColorBrush(Colors.Red);
                pwbContraseña.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                lblPassword.Content = string.Empty;
                pwbContraseña.BorderBrush = new PasswordBox().BorderBrush;
            }

            return true;
        }

        private bool ValidarUsuario()
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                lblUsuario.Content = "*Debes introducir un Nombre de Usuario";
                lblUsuario.Foreground = new SolidColorBrush(Colors.Red);
                txtUsuario.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                lblUsuario.Content = string.Empty;
                txtUsuario.BorderBrush = new TextBox().BorderBrush;
            }
            return true;
        }

        private void bttCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void pwbContraseña_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidarPassword();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsuario.Focus();
        }

        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarUsuario();
            
        }

        private void getUser()
        {
            string strRFC = UserPac.rfc;

           
                const string URL = "http://adesoft-ws.appspot.com/getuser";
                string urlParameters = "?rfc=" + strRFC;

            
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    string strValue = dataObjects.ToString();
                    
                    string strUs = strValue.Split('|').ElementAt(0);
                    string strPass = strValue.Split('|').ElementAt(1);
                    string idEquipo = strValue.Split('|').ElementAt(2);

                    strUs = strUs.Replace(" ", "");
                    strPass = strPass.Replace(" ", "");
                    if (strUs == "ADE012345V6")
                    {
                        idEquipo = "5c2abb474acf4a98b0ee0ef2415ff8c"; //idEquipo.Replace(" ", "");
                    }
                    else
                    {
                        idEquipo = "5c2abb474acf4a98b0ee0ef2415ff8c2"; //idEquipo.Replace(" ", "");
                    }

                    UserPac.user = strUs;
                    UserPac.passwd = strPass;
                    UserPac.idEquipo = idEquipo; 
                }
                else
                {
                    MessageBox.Show((int)response.StatusCode + " - " + response.ReasonPhrase);
                }
            }

        private void getCFD()
        {

            try
            {
                var myCFD = db.CFD.SingleOrDefault(c => c.intID == 6);
                myCFD.strDescripcion = "PAGO";
                myCFD.strTipoCFD = "pago";
                myCFD.intIdFolio = 6;
                myCFD.templateReportH = "templateFacturaHPago_33.xslt";
                myCFD.templateReportHdemo = "templateFacturaHPago_33.xslt";
                myCFD.templateReport = "templatePagoConceptos33.xslt";
                myCFD.templateReportDemo = "templatePagoConceptos33.xslt";
                db.SaveChanges();


            }
            catch (Exception ex)
            {

                wpfEFac.Models.CFD p = new wpfEFac.Models.CFD();
                p.intID = 6;
                p.strDescripcion = "PAGO";
                p.strTipoCFD = "pago";
                p.intId_Empresa = 1;
                p.intIdFolio = 6;
                p.templateReportH = "templateFacturaHPago_33.xslt";
                p.templateReportHdemo = "templateFacturaHPago_33.xslt";
                p.templateReport = "templatePagoConceptos33.xslt";
                p.templateReportDemo = "templatePagoConceptos33.xslt";
               // p.iva = decimal.Parse("0.16");
                //p.retIva = decimal.Parse("0.00");
               // p.retIsr = decimal.Parse("0.00");

                db.CFD.AddObject(p);
                db.SaveChanges();



            }

            //   Factura factura = db.Factura.SingleOrDefault(fac => fac.intID == f.intID);// busca y edita status de comprobante

            //    factura.chrStatus = "C";
            //   factura.dtmFechaCancelacion = DateTime.Now;
            //  factura.strNumero = "CANCELADA";
            //  factura.dtmFechaEnvio = DateTime.Now; 

        }

        private void getFolios()
        {

            try
            {
                var myFolios = db.Folios.SingleOrDefault(c => c.intID == 6);
                //myFolios.intID = 6;
                myFolios.intID_Certificate = 1;
                myFolios.intFolio_Inicial = 1;
                myFolios.intFolio_Final = 50000;
                myFolios.intNumero_Aprovacion = 123456;
                myFolios.strSerie = "P";
                myFolios.strAño_Aprovacion = "2021";
                myFolios.intID_Empresa = 1;
                myFolios.chrStatus = "A";
                // myFolios.intFolioActual = 1;

                db.SaveChanges();


            }
            catch (Exception ex)
            {

                wpfEFac.Models.Folios F = new wpfEFac.Models.Folios();
                F.intID = 6;
                F.intID_Certificate = 1;
                F.intFolio_Inicial = 1;
                F.intFolio_Final = 50000;
                F.intNumero_Aprovacion = 123456;
                F.strSerie = "P";
                F.strAño_Aprovacion = "2021";
                F.intID_Empresa = 1;
                F.chrStatus = "A";
                F.intFolioActual = 1;

                db.Folios.AddObject(F);
                db.SaveChanges();



            }

            //   Factura factura = db.Factura.SingleOrDefault(fac => fac.intID == f.intID);// busca y edita status de comprobante

            //    factura.chrStatus = "C";
            //   factura.dtmFechaCancelacion = DateTime.Now;
            //  factura.strNumero = "CANCELADA";
            //  factura.dtmFechaEnvio = DateTime.Now; 

        }

        private void getProduct()
        {

            try
            {
                var myPro = db.Productos.SingleOrDefault(c => c.strCodigoBarras.Equals("84111506") && c.strNombre.Contains("Pago"));
                myPro.intID_Categoria = 1;
                myPro.strNombre = "Pago";
                myPro.strNombreCorto = "";
                myPro.strCodigo = "84111506";
                myPro.dcmPrecio1 = decimal.Parse("0.00");
                myPro.dcmPrecio2 = decimal.Parse("0.00");
                myPro.dcmPrecio3 = decimal.Parse("0.00");
                myPro.dcmPrecio4 = decimal.Parse("0.00");
                myPro.dcmPrecio5 = decimal.Parse("0.00");
                myPro.dcmDescuent = decimal.Parse("0.00");
                myPro.intUnidad = 1;
                myPro.strDescripcion = "Pago";
                myPro.intID_Empresa = 1;
                myPro.strCodigoBarras = "84111506";
                myPro.gravaIva = "N";
                myPro.porcIva = decimal.Parse("0.00");
                myPro.gravaIeps = "N";
                myPro.porcIeps = decimal.Parse("0.00");
                myPro.gravaRetIsr = "N";
                myPro.porcRetIsr = decimal.Parse("0.00");
                myPro.gravaRetIva = "N";
                myPro.porcRetIva = decimal.Parse("0.00");
                myPro.existencias = 0;
                myPro.stockMin = 0;
                myPro.stockMax = 0;
                myPro.puntoReorden = 0;

                db.SaveChanges();


            }
            catch (Exception ex)
            {
                Models.Productos gtp = db.Productos.OrderByDescending(p => p.intID).FirstOrDefault();
                wpfEFac.Models.Productos pro = new wpfEFac.Models.Productos();
                int intGet = 0;
                if (gtp != null)
                {


                    intGet = gtp.intID + 1;
                }
                else
                {
                    intGet = 1;

                }

                pro.intID = intGet;
                pro.intID_Categoria = 1;
                pro.strNombre = "Pago";
                pro.strNombreCorto = "";
                pro.strCodigo = "84111506";
                pro.dcmPrecio1 = decimal.Parse("0.00");
                pro.dcmPrecio2 = decimal.Parse("0.00");
                pro.dcmPrecio3 = decimal.Parse("0.00");
                pro.dcmPrecio4 = decimal.Parse("0.00");
                pro.dcmPrecio5 = decimal.Parse("0.00");
                pro.dcmDescuent = decimal.Parse("0.00");
                pro.intUnidad = 1;
                pro.strDescripcion = "Pago";
                pro.intID_Empresa = 1;
                pro.strCodigoBarras = "84111506";
                pro.gravaIva = "N";
                pro.porcIva = decimal.Parse("0.00");
                pro.gravaIeps = "N";
                pro.porcIeps = decimal.Parse("0.00");
                pro.gravaRetIsr = "N";
                pro.porcRetIsr = decimal.Parse("0.00");
                pro.gravaRetIva = "N";
                pro.porcRetIva = decimal.Parse("0.00");
                pro.existencias = 0;
                pro.stockMin = 0;
                pro.stockMax = 0;
                pro.puntoReorden = 0;


                db.Productos.AddObject(pro);
                db.SaveChanges();



            }

            //   Factura factura = db.Factura.SingleOrDefault(fac => fac.intID == f.intID);// busca y edita status de comprobante

            //    factura.chrStatus = "C";
            //   factura.dtmFechaCancelacion = DateTime.Now;
            //  factura.strNumero = "CANCELADA";
            //  factura.dtmFechaEnvio = DateTime.Now; 

        }
    }
}
