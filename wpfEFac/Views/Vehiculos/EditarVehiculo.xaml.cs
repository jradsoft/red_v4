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
            wpfEFac.Models.Configuracion_Vehicular conf = new Models.Configuracion_Vehicular();
            wpfEFac.Models.TipoRemolque confRemolque = new Models.TipoRemolque();

            cmbConfVehicular.ItemsSource = entidad.Configuracion_Vehicular.OrderBy(x => x.str_descripcion);
            cmbConfVehicular.DisplayMemberPath = "str_descripcion";
            cmbConfVehicular.SelectedValuePath = "str_codigo";
            cmbConfVehicular.SelectedValue = conf.id;

            cmbConfRemolque.ItemsSource = entidad.TipoRemolque.OrderBy(x => x.str_descripcion);
            cmbConfRemolque.DisplayMemberPath = "str_descripcion";
            cmbConfRemolque.SelectedValuePath = "str_codigo";
            cmbConfRemolque.SelectedValue = confRemolque.id;
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
                txtNoEconomico.Text = pro.NoEconomico;
                if (cmbTipo.Text =="Tracto")
                {
                    cmbConfVehicular.Text = pro.strConfigVehicular;
                }
                else {
                    cmbConfVehicular.Visibility = System.Windows.Visibility.Collapsed;
                    cmbConfRemolque.Visibility = System.Windows.Visibility.Visible;
                    cmbConfRemolque.Text = pro.strConfigVehicular;

                
                }

            }
            catch (Exception ex) {

                MessageBox.Show("Error " + ex);
            }
           

           



        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            try {

                string strConfigVehicular ="";

                if (cmbTipo.SelectedIndex == 0)
                {
                    strConfigVehicular = cmbConfVehicular.Text;
                }
                else
                {
                    strConfigVehicular = cmbConfRemolque.Text;

                }


                BusClientes bus = new BusClientes();
                if (bus.EditarVehiculo(placa,
                    txtPlaca.Text,
                    txtModelo.Text,
                    txtAño.Text,
                    txtPoliza.Text,
                    txtAseguradoraCarga.Text,
                    txtAseguradoraRepCiv.Text,
                    cmbTipo.Text,
                    strConfigVehicular,
                    txtNoEconomico.Text

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

        private void cmbTipo_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbTipo.SelectedIndex == 0)
            {

                cmbConfVehicular.Visibility = System.Windows.Visibility.Visible;
                cmbConfRemolque.Visibility = System.Windows.Visibility.Collapsed;
            }
            else {
                cmbConfVehicular.Visibility = System.Windows.Visibility.Collapsed;
                cmbConfRemolque.Visibility = System.Windows.Visibility.Visible;
            
            }
        }

        private void cmbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipo.SelectedIndex == 0)
            {

             //   cmbConfVehicular.Visibility = System.Windows.Visibility.Visible;
             //   cmbConfRemolque.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
              //  cmbConfVehicular.Visibility = System.Windows.Visibility.Collapsed;
              //  cmbConfRemolque.Visibility = System.Windows.Visibility.Visible;

            }
        }
    }
}
