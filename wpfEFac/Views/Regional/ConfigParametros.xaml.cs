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
using wpfEFac.Views.Regional;

namespace wpfEFac
{
    /// <summary>
    /// Interaction logic for ConfigParametros.xaml
    /// </summary>
    public partial class ConfigParametros : Page
    {
        public ConfigParametros()
        {
            InitializeComponent();
        }

        private void bttEditar_Click(object sender, RoutedEventArgs e)
        {
            EditarParametros editarParametros = new EditarParametros();
            editarParametros.Show();
        }
    }
}
