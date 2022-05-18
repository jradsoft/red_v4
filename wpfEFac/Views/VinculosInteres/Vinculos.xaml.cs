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

namespace wpfEFac.Views.VinculosInteres
{
    /// <summary>
    /// Interaction logic for Vinculos.xaml
    /// </summary>
    public partial class Vinculos : Page
    {
        public string url = "";

        public Vinculos()
        {
            url = App.Current.Properties["liga"].ToString();
            InitializeComponent();
            wbrPrincipal.Source = new Uri(url);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            url = App.Current.Properties["liga"].ToString();
            wbrPrincipal.Source = new Uri(url);
        }

    }
}
