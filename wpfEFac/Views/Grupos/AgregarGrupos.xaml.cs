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

namespace wpfEFac.Views.Grupos
{
    /// <summary>
    /// Interaction logic for AgregarGrupos.xaml
    /// </summary>
    public partial class AgregarGrupos : Window
    {
        private eFacDBEntities entidad;

        public AgregarGrupos()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtNombre.Text != string.Empty && txtFecha_Creacion.Text != string.Empty)
            {
                var result = entidad.Grupos.Count();
                result += 1;

                string Nombre = txtNombre.Text;
                string ID = result.ToString();
                DateTime FechaCreacion = Convert.ToDateTime(txtFecha_Creacion.Text);                                
               
                BusClientes bus = new BusClientes();
                if (bus.AgregarGrupo(/*ID,*/ Nombre, FechaCreacion, Convert.ToInt32(App.Current.Properties["idEmpresa"])))
                {
                    MessageBox.Show("El Grupo fue Registrado Exitosamente !!", "Registrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ocurrio un Problema durante el registro, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DialogResult = true;
            }
        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public bool ValidarNombre()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblNombre.Content = "*Campo requerido, ingresa un Nombre";
                lblNombre.Foreground = new SolidColorBrush(Colors.Red);
                txtNombre.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                lblNombre.Content = string.Empty;
                txtNombre.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
            return true;
        }        

        public bool ValidarFechaCreacion()
        {
            if (string.IsNullOrWhiteSpace(txtFecha_Creacion.Text))
            {
                lblCreacion.Content = "*Campo requerido, ingresa una Fecha de Creacion";
                lblCreacion.Foreground = new SolidColorBrush(Colors.Red);
                txtFecha_Creacion.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                lblCreacion.Content = string.Empty;
                txtFecha_Creacion.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
            return true;
        }        

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarNombre();
        }

        private void txtFecha_Creacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarFechaCreacion();
        }
    }
}
