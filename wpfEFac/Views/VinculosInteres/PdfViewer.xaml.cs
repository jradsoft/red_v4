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

namespace wpfEFac.Views.VinculosInteres
{
    /// <summary>
    /// Interaction logic for PdfViewer.xaml
    /// </summary>
    public partial class PdfViewer : Window
    {
        public string url = "";
        public PdfViewer()
        {
            url = App.Current.Properties["liga"].ToString();
            InitializeComponent();
            wbrPrincipal.Source = new Uri(url);
            
        }

        

    }
}
