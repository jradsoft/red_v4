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

namespace wpfEFac.Views.Paises
{
    /// <summary>
    /// Interaction logic for AgregarPais.xaml
    /// </summary>
    public partial class AgregarPais : Window
    {

        private eFacDBEntities entidad;

        public AgregarPais()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == string.Empty)
            {
                lblNombre.Content = "* Campo requerido, debes poner un Nombre";
                lblNombre.Foreground = new SolidColorBrush(Colors.Red);
                txtNombre.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {                
                //var numero = entidad.Paises.Count();
                //numero += 1;
                //string ID = numero.ToString();
                throw new NotImplementedException();
                int ID = 1; // cambiar por el metodo de obtener los IDs
                string Nombre = txtNombre.Text;

                BusClientes bus = new BusClientes();

                if (bus.AgregarPais(/*ID,*/ Nombre))
                {
                    MessageBox.Show("El pais fue registrado exitosamente !!", "Registrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ocurrio un Error durante el registro, vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                DialogResult = true;
            }
        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
