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
    /// Interaction logic for ConfigRegional.xaml
    /// </summary>
    public partial class ConfigRegional : Page
    {    
        public ConfigRegional()
        {
            InitializeComponent();
        }

        private void frame1_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void frame1_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.frmParametros.Source = new Uri("/Views/Regional/ConfigParametros.xaml",UriKind.Relative);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.frmParametros.Source = new Uri("/Views/Paises/ConfigPaises.xaml",UriKind.Relative);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.frmParametros.Source = new Uri("/Views/Estado/ConfigEstados.xaml",UriKind.Relative);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.frmParametros.Source = new Uri("/Views/Poblaciones/ConfigPoblaciones.xaml",UriKind.Relative);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            this.frmParametros.Source = new Uri("/Views/Municipios/ConfigMunicipios.xaml",UriKind.Relative);
        }        
    }
}
