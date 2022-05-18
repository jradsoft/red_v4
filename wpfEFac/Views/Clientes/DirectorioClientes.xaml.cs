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
using wpfEFac.Views.Clientes;
using wpfEFac.Models;

namespace wpfEFac
{
    /// <summary>
    /// Interaction logic for DirectorioClientes.xaml
    /// </summary>
    public partial class DirectorioClientes : Page
    {
        private eFacDBEntities entidad;
        private string rfc;
        public DirectorioClientes()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
        }

        private void bttNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            AgregarCliente agregarCliente = new AgregarCliente();

            bool? resultado = agregarCliente.ShowDialog().Value;

            if (resultado.HasValue)
            {
                if (resultado.Value)
                {
                    dtgDirectorio.ItemsSource = null;
                    entidad = new eFacDBEntities();
                    dtgDirectorio.ItemsSource = entidad.Clientes;    
                }
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {                       
            dtgDirectorio.ItemsSource = entidad.Clientes;             
        }

        private void bttBuscar_Click(object sender, RoutedEventArgs e)
        {
            BusClientes bus = new BusClientes();
            string buscar = Convert.ToString(txtBuscar.Text);
            //bus.ObtenerCliente(buscar);            
        }
       
        private void bttEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgDirectorio.SelectedItem != null)
	        {
                Clientes editar = (Clientes)dtgDirectorio.SelectedItem;

                EditarCliente editarcliente = new EditarCliente(editar.intID);
                
                bool? result = editarcliente.ShowDialog().Value;
                
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        ReloadClientes();  
                    }
                }
	        }
        }

        private void ReloadClientes()
        {
            dtgDirectorio.ItemsSource = null;
            entidad = new eFacDBEntities();
            dtgDirectorio.ItemsSource = entidad.Clientes;
        }

        private void bttBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgDirectorio.SelectedItem != null)
            {
                Clientes editar = (Clientes)dtgDirectorio.SelectedItem;
                if (editar != null)
                {
                    MessageBoxResult resultado =
                    MessageBox.Show("¿Desea eliminar el cliente?", "Eliminar",
                        MessageBoxButton.OKCancel, MessageBoxImage.Information);

                    if (resultado == MessageBoxResult.OK)
                    {

                        BusClientes bus = new BusClientes();
                        if (bus.EliminarCliente(editar.intID))
                        {
                            MessageBox.Show("Cliente eliminado");
                            ReloadClientes();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar intentelo mas tarde");
                        }
                    }
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBuscar.Text == string.Empty)
            {
                dtgDirectorio.ItemsSource = null;
                dtgDirectorio.ItemsSource = entidad.Clientes.Where(Cli => Cli.intID_Empresa != 2).OrderBy(d => d.strNombreComercial).ToList();
            }

            else
            {
                dtgDirectorio.ItemsSource = null;
                dtgDirectorio.ItemsSource = entidad.Clientes.Where(Cli => Cli.strNombreComercial.ToUpper().Contains(txtBuscar.Text.ToUpper()) || Cli.strRazonSocial.ToUpper().Contains(txtBuscar.Text.ToUpper()) || Cli.strRFC.ToUpper().Contains(txtBuscar.Text.ToUpper()) && Cli.intID_Empresa == 1).OrderBy(d => d.strRazonSocial).ToList();
            }

        }
    }
}
