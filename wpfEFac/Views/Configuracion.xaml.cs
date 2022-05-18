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

namespace wpfEFac
{
    /// <summary>
    /// Interaction logic for PuntoVenta.xaml
    /// </summary>
    public partial class PuntoVenta : Page
    {
        public PuntoVenta()
        {
            InitializeComponent();
        }
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.frmEmpresa.Source = new Uri("/Views/Clientes/DirectorioClientes.xaml", UriKind.Relative);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.frmEmpresa.Source = new Uri("/Views/Productos/ConfigCatalogo.xaml", UriKind.Relative);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.frmEmpresa.Source = new Uri("/Views/Usuarios/ConfigUsuarios.xaml", UriKind.Relative);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.frmEmpresa.Source = new Uri("/Views/Empresa/ConfigEmpresa.xaml", UriKind.Relative);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            this.frmEmpresa.Source = new Uri("/Views/Grupos/ConfigGrupos.xaml", UriKind.Relative);
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {               
            this.frmEmpresa.Source = new Uri("/Views/ConfigRegional.xaml", UriKind.Relative);            
        }

        private void MenuImportar_Click(object sender, RoutedEventArgs e)
        {
            this.frmEmpresa.Source = new Uri("/Views/ImportarDatos/ImportarDatos.xaml", UriKind.Relative);
        }

        private void MenuVehiculos_Click(object sender, RoutedEventArgs e)
        {
            this.frmEmpresa.Source = new Uri("/Views/Vehiculos/ConfigVehiculos.xaml", UriKind.Relative);
        }
    }
}
