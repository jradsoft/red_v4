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

namespace wpfEFac.Views.Productos
{
    /// <summary>
    /// Interaction logic for UnidadMedidaWindow.xaml
    /// </summary>
    public partial class UnidadMedidaWindow : Window
    {
        private wpfEFac.Models.eFacDBEntities db;

        public UnidadMedidaWindow()
        {
            InitializeComponent();
            db = new Models.eFacDBEntities();
        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void bttnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatosValidos())
            {
                UnidadMedida um = new UnidadMedida();
                um.intId = Identifier.GetID(typeof(UnidadMedida).Name, Convert.ToInt32(App.Current.Properties["idEmpresa"]), um, EntityEnum.UnidadMedida, db);
                um.strDescripcion = txtNombreUnidadMedida.Text;
                um.intID_Empresa = Convert.ToInt32(App.Current.Properties["idEmpresa"]);

                db.UnidadMedida.AddObject(um);
                db.SaveChanges();
                this.DialogResult = true;
            }
        }

        private bool ValidarDatosValidos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreUnidadMedida.Text))
            {
                lblError.Content = "*Campo requerido, escribir el nombre de la categoria";
                lblError.Foreground = new SolidColorBrush(Colors.Red);
                txtNombreUnidadMedida.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                lblError.Content = "";
                txtNombreUnidadMedida.BorderBrush = new TextBox().BorderBrush;
                return true;
            }
        }
    }
}
