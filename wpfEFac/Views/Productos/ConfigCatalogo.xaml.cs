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
using wpfEFac.Views.Productos;
using wpfEFac.Models;

namespace wpfEFac
{
    /// <summary>
    /// Interaction logic for ConfigCatalogo.xaml
    /// </summary>
    public partial class ConfigCatalogo : Page
    {
        private eFacDBEntities entidad;
        public ConfigCatalogo()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
        }

        private void bttNuevoProducto_Click(object sender, RoutedEventArgs e)
        {
            AgregarProductos agregarProductos = new AgregarProductos();
            if (agregarProductos.ShowDialog().Value)
            {
                dtgProductos.ItemsSource = null;
                entidad = new eFacDBEntities();
                dtgProductos.ItemsSource = entidad.Productos;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dtgProductos.ItemsSource = entidad.Productos;                        
        }

        private void dtgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void bttEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProductos.SelectedItem != null)
            {
                Productos edi = (Productos)dtgProductos.SelectedItem;                
                EditarProducto editar = new EditarProducto(edi.intID);
                if (editar.ShowDialog().Value)
                {
                    ReloadProductos();
                }
                
            }
        }

        private void ReloadProductos()
        {
            dtgProductos.ItemsSource = null;
            entidad = new eFacDBEntities();
            dtgProductos.ItemsSource = entidad.Productos;
        }

        private void bttEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (dtgProductos.SelectedItem != null)
            {
                Productos edi = (Productos)dtgProductos.SelectedItem;
                if (edi != null)
                {
                    MessageBoxResult resultado =
                    MessageBox.Show("Desea eliminar el producto", "Eliminar",
                        MessageBoxButton.OKCancel, MessageBoxImage.Information);

                    if (resultado == MessageBoxResult.OK)
                    {
                        DataProductos dp = new DataProductos();

                        if (dp.EliminarProducto(edi.intID))
                        {
                            MessageBox.Show("Producto eliminado");
                            ReloadProductos();
                        }
                        else
                        {
                            MessageBox.Show("Lo sentimos no se pudo eliminar esto se puede deber a que el producto ya este asociado a un CFD intentelo mas tarde");
                        }
                    }
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBuscar.Text == string.Empty)
            {


                dtgProductos.ItemsSource = null;
                dtgProductos.ItemsSource = entidad.Productos.Where(Cli => Cli.intID_Empresa != 2).OrderBy(d => d.strDescripcion).ToList();
            }

            else
            {
                dtgProductos.ItemsSource = null;
                dtgProductos.ItemsSource = entidad.Productos.Where(Cli => Cli.strDescripcion.ToUpper().Contains(txtBuscar.Text.ToUpper()) || Cli.strNombre.ToUpper().Contains(txtBuscar.Text.ToUpper()) || Cli.strNombreCorto.ToUpper().Contains(txtBuscar.Text.ToUpper()) && Cli.intID_Empresa == 1).OrderBy(d => d.strDescripcion).ToList();
            }
        }
            
    }
}
