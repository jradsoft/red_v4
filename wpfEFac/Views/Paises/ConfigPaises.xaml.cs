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
using wpfEFac.Views.Paises;

namespace wpfEFac
{
    /// <summary>
    /// Interaction logic for ConfigPaises.xaml
    /// </summary>
    public partial class ConfigPaises : Page
    {
        private eFacDBEntities entidad;

        public ConfigPaises()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dtgPaises.ItemsSource = entidad.Paises;
        }

        private void bttNuevoPais_Click(object sender, RoutedEventArgs e)
        {
            AgregarPais ventanaAgregar = new AgregarPais();
            if (ventanaAgregar.ShowDialog().Value)
            {
                dtgPaises.ItemsSource = null;
                dtgPaises.ItemsSource = entidad.Paises;
            }
        }

        private void bttEditar_Click(object sender, RoutedEventArgs e)
        {
            Paises editar = (Paises)dtgPaises.SelectedItem;
            if (dtgPaises.SelectedItem != null)
            {
                EditarPais pais = new EditarPais(editar.intID);
                if (pais.ShowDialog().Value)
                {
                    dtgPaises.ItemsSource = null;
                    dtgPaises.ItemsSource = entidad.Paises;
                }
            }
        }
    }
}
