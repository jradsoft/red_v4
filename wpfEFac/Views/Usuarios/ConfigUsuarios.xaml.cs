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
using wpfEFac.Views.Usuarios;
using wpfEFac.Models;

namespace wpfEFac
{
    /// <summary>
    /// Interaction logic for ConfigUsuarios.xaml
    /// </summary>
    public partial class ConfigUsuarios : Page
    {
        private eFacDBEntities entidad;
        public ConfigUsuarios()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
        }

        private void dtgUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void bttAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            AgregarUsuarios agregarUsuarios = new AgregarUsuarios();
            if (agregarUsuarios.ShowDialog().Value)
            {
                dtgUsuarios.ItemsSource = null;
                dtgUsuarios.ItemsSource = entidad.Usuarios;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dtgUsuarios.ItemsSource = entidad.Usuarios;
        }

        private void bttEditar_Click(object sender, RoutedEventArgs e)
        {
            Usuarios editar = (Usuarios)dtgUsuarios.SelectedItem;

            if (dtgUsuarios.SelectedItem != null)
            {
                EditarUsuario usuario = new EditarUsuario(editar.intID);
                if (usuario.ShowDialog().Value)
                {
                    dtgUsuarios.ItemsSource = null;
                    dtgUsuarios.ItemsSource = entidad.Usuarios;
                }
            }
        }

        private void ReloadUsuarios()
        {
            dtgUsuarios.ItemsSource = null;
            entidad = new eFacDBEntities();
            dtgUsuarios.ItemsSource = entidad.Usuarios;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dtgUsuarios.SelectedItem != null)
            {
                Usuarios del = (Usuarios)dtgUsuarios.SelectedItem;

                if (del != null)
                {
                    MessageBoxResult result =
                    MessageBox.Show("Desea elimira el Usuario?", "Eliminar", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.OK)
                    {

                        DataUsuario du = new DataUsuario();
                        if (du.DeleteUsuario(del.intID))
                        {
                            MessageBox.Show("Usuario Eliminado");
                            ReloadUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("El Usuario no puede ser eliminado en estos momentos, por favor intentelo más tarde");
                        }
                    }
                }
            }
        }
    }
}
