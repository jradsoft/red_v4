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

namespace wpfEFac.Views.Usuarios
{
    /// <summary>
    /// Lógica de interacción para AgregarUsuarios.xaml
    /// </summary>
    public partial class AgregarUsuarios : Window
    {
        public AgregarUsuarios()
        {
            InitializeComponent();
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtNombre.Text != string.Empty && PwbPassword.Password != string.Empty && txtIDGrupo.Text != string.Empty
                && txtEmail.Text != string.Empty)
            {
                eFacDBEntities entidad = new eFacDBEntities();

                var num = entidad.Usuarios.Count();
                num += 1;

                int Id = num;
                string Password = PwbPassword.Password;
                string Nombre = Convert.ToString(txtNombre.Text);
                int IdGrupo = int.Parse(txtIDGrupo.Text);              
                string Email = txtEmail.Text.ToString();

                BusClientes usuarios = new BusClientes();

                if (usuarios.AgregarUsuario(Id, Nombre, Password, IdGrupo,Convert.ToInt32(App.Current.Properties["idEmpresa"]),Email))
                {
                    MessageBox.Show("\"El usuaio fue Registrado exitosamente\"", "Registrado", MessageBoxButton.OK, MessageBoxImage.Information);                    
                }
                else
                {
                    MessageBox.Show("\"Ocurrio un problema durante el registro, por favor vuelva a intentarlo\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public bool ValidarPassword()
        {
            if (string.IsNullOrWhiteSpace(PwbPassword.Password))
            {                
                lblPassword.Content = "*Campo requerido, ingresa una Contraseña";
                lblPassword.Foreground = new SolidColorBrush(Colors.Red);
                PwbPassword.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                lblPassword.Content = string.Empty;
                PwbPassword.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
            return true;
        }

        public bool ValidarIDGrupo()
        {
            if (string.IsNullOrWhiteSpace(txtIDGrupo.Text))
            {
                lblIDGrupo.Content = "*Campo requerido, ingresa un Nombre";
                lblIDGrupo.Foreground = new SolidColorBrush(Colors.Red);
                txtIDGrupo.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                lblIDGrupo.Content = string.Empty;
                txtIDGrupo.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
            return true;
        }

        public bool ValidarEmail()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblEmail.Content = "*Campo requerido, ingresa un Nombre";
                lblEmail.Foreground = new SolidColorBrush(Colors.Red);
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                lblEmail.Content = string.Empty;
                txtEmail.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
            return true;
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarNombre();
        }

        private void txtIDGrupo_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarIDGrupo();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarEmail();
        }
    }
}
