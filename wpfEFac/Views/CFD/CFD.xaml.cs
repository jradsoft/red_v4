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

namespace wpfEFac.Views.CFD
{
    /// <summary>
    /// Interaction logic for CFD.xaml
    /// </summary>
    public partial class CFD : Page
    {
        public CFD()
        {
            InitializeComponent();
        }

        private void btnExaminarCer_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Certificado";
            dlg.DefaultExt = ".cer";
            dlg.Filter = "Certificado de seguridad(.cer)|*.cer|Todos los archivos|*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                txtCerFilePath.Text = dlg.FileName;
            }
        }

        private void btnExaminarKey_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Key";
            dlg.DefaultExt = ".key";
            dlg.Filter = "Llave privada de certificado(.key)|*.key|Todos los archivos|*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                txtKeyFilePath.Text = dlg.FileName;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
