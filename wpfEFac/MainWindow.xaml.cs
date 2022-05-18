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
using wpfEFac.Views.Login;
using wpfEFac.Models;
using wpfEFac.Helpers;
using System.Windows.Threading;
using System.Threading;

namespace wpfEFac
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private eFacDBEntities db;
        private Uri current;
        private PreFacturaViewModel pfvm;
        public wpfEFac.Helpers.BannerTotales banner;
        DispatcherTimer dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            pfvm = new PreFacturaViewModel();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Start();
            db = new eFacDBEntities();
            Empresa empresa = db.Empresa.First(emp => emp.intID == 1);
            string strEmpresa = empresa.strRazonSocial;

            this.Title = "<MyFacturaE/> " + strEmpresa;

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Usuarios iduser;
            Login user = new Login();
            iduser = user.GetUsuario(Convert.ToInt32(App.Current.Properties["idUsuario"]));
            mtdMostrarBannerEmpresa(iduser.intID_Empresa);
            mtdMostrarBannerCoDi(iduser.intID_Empresa);
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new eFacDBEntities();

                if (App.Current.Properties["idUsuario"] != null)
                {
                    Usuarios iduser;
                    Login user = new Login();
                    iduser = user.GetUsuario(Convert.ToInt32(App.Current.Properties["idUsuario"]));
                    mtdMostrarBannerEmpresa(iduser.intID_Empresa);
                    mtdMostrarBannerCoDi(iduser.intID_Empresa);
                    GetClientes();
                }
                else
                {
                    App.Current.Shutdown();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }


            current =
            this.ContentFrame.CurrentSource;

            //LoginWindow lw = new LoginWindow();
            //lw.Owner = this;
            //if (lw.ShowDialog().Value)
            //{
            //    this.WindowState = WindowState.Maximized;
            //}
            //else
            //{
            //    this.Close();
            //}
        }

        private void mtdMostrarBannerEmpresa(int intID_empresa)
        {
            db = new eFacDBEntities();

            Empresa empresa = db.Empresa.First(emp => emp.intID == intID_empresa);

            CFD cfd = db.CFD.First(c => c.intID == empresa.intID_CFD);

            txbNombreEmpresa.Text = empresa.strNombreComercial;
            txbRFC.Text = "RFC: " + empresa.strRFC;
            txbCDF.Text = "CFD: " + cfd.strDescripcion;
            txbCertificados.Text = "Certificado: " + empresa.Certificates.First(c => c.chrStatus == "A").strNumeroCertificadoSelloDigital;

        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //current = this.ContentFrame.CurrentSource;
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //this.ContentFrame.Source = new Uri("/Views/Page1.xaml", UriKind.Relative);
        }

        private void MenuPuntoVenta_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Source = new Uri("/Views/PuntoVenta/DefaultPuntoVenta.xaml", UriKind.Relative);
        }

        private void MenuComprobantesDigitales_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Source = new Uri("/Views/CFD/CFD.xaml", UriKind.Relative);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Source = new Uri("/Views/WebBrowser/Browser.xaml", UriKind.RelativeOrAbsolute);
        }

        public void mtdMostrarBannerCoDi(int intID_empresa)
        {
            db = new eFacDBEntities();
            var alldatos = from todos in db.Factura
                           where todos.intID_Empresa == intID_empresa
                           select new
                           {
                               todos.intID_Tipo_CFD,
                               todos.dcmTotal,
                               todos.chrStatus,
                               todos.dtmFecha
                           };

            List<Helpers.BannerTotales> banner = new List<Helpers.BannerTotales>();

            var tipocfd = from tipo in db.CFD
                          select new
                          {
                              tipo.intID
                          };

            for (int i = 1; i <= tipocfd.Count(); i++)
            {
                int intIncre = i;

                var descripcion = (from desc in db.CFD
                                   where desc.intID == intIncre
                                   select new
                                   {
                                       desc.strDescripcion
                                   }).Single();

                var datoshoy = from hoy in alldatos
                               where (hoy.intID_Tipo_CFD == intIncre) &
                               (hoy.dtmFecha.Year == DateTime.Today.Year & hoy.dtmFecha.Month == DateTime.Today.Month & hoy.dtmFecha.Day == DateTime.Today.Day)
                               select hoy;

                var datosmes = from mes in alldatos
                               where (mes.intID_Tipo_CFD == intIncre) & (mes.dtmFecha.Month == DateTime.Today.Month)
                               select mes;

                var datosaut = from aut in alldatos
                               where aut.intID_Tipo_CFD == intIncre & (aut.chrStatus == "A")
                               select aut;

                var datospend = from pend in alldatos
                                where pend.intID_Tipo_CFD == intIncre & (pend.chrStatus == "P")
                                select pend;

                var datostotal = from total in alldatos
                                 where total.intID_Tipo_CFD == intIncre
                                 select new
                                 {
                                     total.dcmTotal
                                 };

                float dcmTotal = 0;
                foreach (var dato in datostotal)
                {
                    dcmTotal = dcmTotal + (float)dato.dcmTotal;
                }
                try
                {
                    int datoshoyCount = datoshoy.Count();
                    int datosmesCount = datosmes.Count();
                    int datosautCount = datosaut.Count();
                    int datospendCount = datospend.Count();

                    banner.Add(new Helpers.BannerTotales()
                    {
                        Descripcion = descripcion.strDescripcion,
                        Hoy = datoshoyCount,
                        Mes = datosmesCount,
                        Aut = datosautCount,
                        Pend = datospendCount,
                        Env = 0,
                        Total = decimal.Parse(dcmTotal.ToString())
                    });
                }
                catch (Exception)
                {
                    
                }

                
            }

            dtgComprobantesDigitales.ItemsSource = banner;
            dtgComprobantesDigitales.CanUserAddRows = false;

        }

        private void MenuConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Source = new Uri("/Views/Configuracion.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Close();
        }

        private void GetClientes()
        {
            var clientes = pfvm.GetClientes();
            atcNom_Cliente.ItemsSource = clientes;
        }

        private void MenuReporte_Click(object sender, RoutedEventArgs e)
        {
            this.ContentFrame.Source = new Uri("/Views/ReporteMensual/ReporteMensual.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Comprobantes_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["liga"] = "https://www.acceso.sat.gob.mx/_mem_bin/FormsLogin.asp?/Acceso/CertiSAT.asp";
            this.ContentFrame.Navigate(new Views.VinculosInteres.Vinculos());

        }

        private void Validador_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["liga"] = "https://www.consulta.sat.gob.mx/SICOFI_WEB/ModuloECFD_Plus/ValidadorComprobantes/Validador.html";
            this.ContentFrame.Navigate(new Views.VinculosInteres.Vinculos());
        }

        private void Solicitud_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Sicofi_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["liga"] = "https://www.consulta.sat.gob.mx/SICOFI_WEB/moduloECFD_Plus/acceso.asp";
            this.ContentFrame.Navigate(new Views.VinculosInteres.Vinculos());
        }


        private void Fiel_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["liga"] = "http://www.sat.gob.mx/sitio_internet/e_sat/tu_firma/default.asp";
            this.ContentFrame.Navigate(new Views.VinculosInteres.Vinculos());
        }

        

        private void atcNom_Cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var datoscliente = from cliente in db.Clientes
                               where cliente.intID == ((Clientes)(atcNom_Cliente.SelectedItem)).intID
                               select cliente;

            if (atcNom_Cliente.SelectedItem != null)
            {
                txbClaveBaseDatos.Text = datoscliente.First().intID.ToString();
                txbNombreBaseDatos.Text = datoscliente.First().strNombreComercial;
                txbRFCBaseDatos.Text = datoscliente.First().strRFC;
                txbGiroBaseDatos.Text = datoscliente.First().strGiro;
                txbTelefonoBaseDatos.Text = datoscliente.First().strTelefono;
                txbEmailBaseDatos.Text = datoscliente.First().strEmail;
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


        private void videoSolSellos(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["liga"] = "http://www.youtube.com/watch?v=Qy65UOMOEY8&feature=related";
            this.ContentFrame.Navigate(new Views.VinculosInteres.Vinculos());

        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            ExecuteCommandSync("SOLCEDI");
        }
    }
}
