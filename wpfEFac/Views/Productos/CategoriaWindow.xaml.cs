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
    /// Interaction logic for CategoriaWindow.xaml
    /// </summary>
    public partial class CategoriaWindow : Window
    {
        private wpfEFac.Models.eFacDBEntities db;
        

        public CategoriaWindow()
        {
            InitializeComponent();
            db = new Models.eFacDBEntities();
        }

        private void bttnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarValores())
	        {
                Categorias nueva = new Models.Categorias();
                nueva.intID = Identifier.GetID(typeof(Categorias).Name, Convert.ToInt32(App.Current.Properties["idEmpresa"]), nueva, EntityEnum.Categorias, db); // Aqui va el metodo de obtener ID.
                nueva.strNombre = txtNombreCategoria.Text;
                nueva.intID_Empresa = Convert.ToInt32(App.Current.Properties["idEmpresa"]);

                db.Categorias.AddObject(nueva);
                db.SaveChanges();
                this.DialogResult = true;
	        }
        }

        private bool ValidarValores()
        {
            if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
            {
                lblError.Content = "*Campo requerido, escribir el nombre de la categoria";
                lblError.Foreground = new SolidColorBrush(Colors.Red);
                txtNombreCategoria.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                lblError.Content = "";
                txtNombreCategoria.BorderBrush = new TextBox().BorderBrush;
                return true;
            }
            
        }

        private string GetIDCategoria()
        {
            return txtNombreCategoria.Text + db.Categorias.Count();
        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
