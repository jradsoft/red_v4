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
using wpfEFac.Models;

namespace wpfEFac.Views.ImportarDatos
{
    /// <summary>
    /// Interaction logic for CFD.xaml
    /// </summary>
    public partial class ImportarDatos : Page
    {
        public ImportarDatos()
        {
            InitializeComponent();
        }

        private void btnExaminarCer_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Datos";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Excel formato CSV(.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                txtCsvFilePath.Text = dlg.FileName;
                
            }
        }

        private void cmdGuardar_Click(object sender, RoutedEventArgs e)
        {
            ConnectorImportCsv myImport = new ConnectorImportCsv();
            string tipoOperacion = "";
            
            if (rdbProductos.IsChecked == true) tipoOperacion = "Producto";
            if (rdbClientes.IsChecked == true) tipoOperacion = "Cliente";

            myImport.importCsv(txtCsvFilePath.Text, tipoOperacion);
        }

    }
}
