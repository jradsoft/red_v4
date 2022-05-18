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

namespace wpfEFac.Views.Vehiculos
{
    /// <summary>
    /// Lógica de interacción para AgregarVehiculo.xaml
    /// </summary>
    public partial class AgregarVehiculo : Window
    {
        public AgregarVehiculo()
        {
            InitializeComponent();
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            try {

                BusClientes vehiculos = new BusClientes();

                if (vehiculos.AgregarVehiculo(txtPlaca.Text,txtModelo.Text, txtAño.Text,txtPoliza.Text, txtAseguradoraCarga.Text, txtAeguradoraRepCiv.Text, cmbTipo.Text))
                {
                    MessageBox.Show("\"El vehiculo fue Registrado exitosamente\"", "Registrado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("\"Ocurrio un problema durante el registro, por favor vuelva a intentarlo\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DialogResult = true;

                
            
            }
            catch (Exception ex) { 
            
            
            }
        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
