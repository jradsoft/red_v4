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
using wpfEFac.Views.Grupos;
using wpfEFac.Models;

namespace wpfEFac
{
    /// <summary>
    /// Interaction logic for ConfigGrupos.xaml
    /// </summary>
    public partial class ConfigGrupos : Page
    {
        private eFacDBEntities entidad;

        public ConfigGrupos()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
        }

        private void bttAgregarGrupo_Click(object sender, RoutedEventArgs e)
        {
            AgregarGrupos agregarGrupos = new AgregarGrupos();
            if (agregarGrupos.ShowDialog().Value)
            {
                dtgGruposTrabajo.ItemsSource = null;
                dtgGruposTrabajo.ItemsSource = entidad.Grupos;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dtgGruposTrabajo.ItemsSource = entidad.Grupos;
        }

        private void bttEditar_Click(object sender, RoutedEventArgs e)
        {
            Grupos editar = (Grupos)dtgGruposTrabajo.SelectedItem;
            if (dtgGruposTrabajo.SelectedItem != null)
            {
                EditarGrupo grupo = new EditarGrupo(editar.intID);
                if (grupo.ShowDialog().Value)
                {
                    dtgGruposTrabajo.ItemsSource = null;
                    dtgGruposTrabajo.ItemsSource = entidad.Grupos;
                }
            }
        }

        private void ReloadGrupos()
        {
            dtgGruposTrabajo.ItemsSource = null;
            entidad = new eFacDBEntities();
            dtgGruposTrabajo.ItemsSource = entidad.Grupos;
        }

        private void bttBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgGruposTrabajo.SelectedItem != null)
            {
                Grupos del = (Grupos)dtgGruposTrabajo.SelectedItem;

                if (del != null)
                {
                    MessageBoxResult result =
                    MessageBox.Show("¿Desea Eliminar el Grupo?", "Eliminar", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.OK)
                    {
                        DataGrupos dg = new DataGrupos();
                        if (dg.DeleteGrupo(del.intID))
                        {
                            MessageBox.Show("Grupo Eliminado");
                            ReloadGrupos();
                        }
                        else
                        {
                            MessageBox.Show("El Grupo no puede ser Eliminado en estos momentos, por favor intentelo más tarde");
                        }
                    }                    
                }
            }
        }

        
    }
}
