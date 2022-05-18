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
using System.Collections.ObjectModel;
using wpfEFac.Models;

namespace wpfEFac.Views.General
{
    /// <summary>
    /// Interaction logic for BuscarClienteWindow.xaml
    /// </summary>
    public partial class BuscarClienteWindow : Window
    {

        private PreFacturaViewModel ctx;
        private IEnumerable<wpfEFac.Models.Clientes> clientes;
        private string rfcCliente;

        public string RFCCliente { get { return rfcCliente; } }

        public BuscarClienteWindow()
        {
            InitializeComponent();
            ctx = new PreFacturaViewModel();
            clientes = ctx.GetClientes();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtgClientes.ItemsSource = clientes;
            LoadTiposInscripcion();
        }

        public void LoadTiposInscripcion() 
        {
            cmbTipoInscripcion.ItemsSource = ctx.GetTipoInscripcion();
            cmbTipoInscripcion.SelectedIndex = 0;
        }

        private void txtRFC_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgClientes.ItemsSource = null;
            dtgClientes.ItemsSource = ctx.buscar(txtRFC.Text, txtNombreComercial.Text, txtRazonSocial.Text, cmbTipoInscripcion.SelectedItem.ToString());
        }

        private void txtRazonSocial_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgClientes.ItemsSource = null;
            dtgClientes.ItemsSource = ctx.buscar(txtRFC.Text, txtNombreComercial.Text, txtRazonSocial.Text, cmbTipoInscripcion.SelectedItem.ToString());
        }

        private void txtNombreComercial_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgClientes.ItemsSource = null;
            dtgClientes.ItemsSource = ctx.buscar(txtRFC.Text, txtNombreComercial.Text, txtRazonSocial.Text, cmbTipoInscripcion.SelectedItem.ToString());
        }

        private void cmbTipoInscripcion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtgClientes.ItemsSource = null;
            dtgClientes.ItemsSource = ctx.buscar(txtRFC.Text, txtNombreComercial.Text, txtRazonSocial.Text, cmbTipoInscripcion.SelectedItem.ToString());
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgClientes.SelectedItem != null)
	        {
                rfcCliente = ((wpfEFac.Models.Clientes)dtgClientes.SelectedItem).strRFC;
                this.DialogResult = true;
	        }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente", "Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

    }
}
