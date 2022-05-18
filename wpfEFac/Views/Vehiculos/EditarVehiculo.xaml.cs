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
    /// Lógica de interacción para EditarVehiculo.xaml
    /// </summary>
    public partial class EditarVehiculo : Window
    {

        private eFacDBEntities entidad;
        EditarVehiculoViewModels epvm;
        private string placa;


        public EditarVehiculo(string placa)
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
            epvm = new EditarVehiculoViewModels();
            this.placa = placa;
            this.Loaded += EditarVehiculo_Loaded;  
        }

        protected void EditarVehiculo_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var pro = epvm.GetVehiculo(placa);
                txtPlaca.Text = pro.strPlaca;
                txtModelo.Text = pro.strModelo;
                txtAño.Text = pro.strAno;
                txtPoliza.Text = pro.strNoPoliza;
                txtAseguradoraCarga.Text = pro.strAseguradoraCarga;
                txtAseguradoraRepCiv.Text = pro.strAseguradoraRepCivil;
                cmbTipo.Text = pro.strTipoVehiculo;
            }
            catch (Exception ex) {

                MessageBox.Show("Error " + ex);
            }
           

           



        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            try {
                BusClientes bus = new BusClientes();
                if (bus.EditarVehiculo(placa,
                    txtPlaca.Text,
                    txtModelo.Text,
                    txtAño.Text,
                    txtPoliza.Text,
                    txtAseguradoraCarga.Text,
                    txtAseguradoraRepCiv.Text,
                    cmbTipo.Text

                    )) {
                        MessageBox.Show("El Vehiculo se ha Editado Corrrectamente", "Editado", MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                
                }
                else
                {
                    MessageBox.Show("Ocurrio un Error durante la Edicion, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
            
            }
            catch (Exception ex) { 
            
            }

        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
