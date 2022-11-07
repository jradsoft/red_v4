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

        private eFacDBEntities mydb;

        public AgregarVehiculo()
        {
            InitializeComponent();
            mydb = new eFacDBEntities();

            wpfEFac.Models.Configuracion_Vehicular conf = new Models.Configuracion_Vehicular();
            wpfEFac.Models.TipoRemolque confRemolque = new Models.TipoRemolque();

            cmbConfVehicular.ItemsSource = mydb.Configuracion_Vehicular.OrderBy(x => x.str_descripcion);
            cmbConfVehicular.DisplayMemberPath = "str_descripcion";
            cmbConfVehicular.SelectedValuePath = "str_codigo";
            cmbConfVehicular.SelectedValue = conf.id;

            cmbConfRemolque.ItemsSource = mydb.TipoRemolque.OrderBy(x => x.str_descripcion);
            cmbConfRemolque.DisplayMemberPath = "str_descripcion";
            cmbConfRemolque.SelectedValuePath = "str_codigo";
            cmbConfRemolque.SelectedValue = confRemolque.id;
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            try {

                BusClientes vehiculos = new BusClientes();


                string strConfigVehicular = "";

                if (cmbTipo.SelectedIndex == 0)
                {
                    strConfigVehicular = cmbConfVehicular.Text;
                }
                else
                {
                    strConfigVehicular = cmbConfRemolque.Text;

                }



                if (vehiculos.AgregarVehiculo(txtPlaca.Text, txtModelo.Text, txtAño.Text, txtPoliza.Text, txtAseguradoraCarga.Text, txtAeguradoraRepCiv.Text, cmbTipo.Text, strConfigVehicular, txtNoEconomico.Text))
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

        private void cmbTipo_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbTipo.SelectedIndex == 0)
            {

                cmbConfVehicular.Visibility = System.Windows.Visibility.Visible;
                cmbConfRemolque.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                cmbConfVehicular.Visibility = System.Windows.Visibility.Collapsed;
                cmbConfRemolque.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void cmbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipo.SelectedIndex == 0)
            {

              //  cmbConfVehicular.Visibility = System.Windows.Visibility.Visible;
              //  cmbConfRemolque.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
               // cmbConfVehicular.Visibility = System.Windows.Visibility.Collapsed;
               // cmbConfRemolque.Visibility = System.Windows.Visibility.Visible;

            }
        }
    }
}
