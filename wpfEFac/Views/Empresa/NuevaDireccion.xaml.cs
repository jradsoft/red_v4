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

namespace wpfEFac.Views.Empresa
{
    /// <summary>
    /// Lógica de interacción para NuevaDireccion.xaml
    /// </summary>
    public partial class NuevaDireccion : Window
    {

        private eFacDBEntities entidad;
       
        public NuevaDireccion()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();


            wpfEFac.Models.Estado es = new wpfEFac.Models.Estado();
            wpfEFac.Models.Paises p = new Models.Paises();
            cmbPais.ItemsSource = entidad.Paises;
            cmbPais.DisplayMemberPath = "strNombrePais";
            cmbPais.SelectedValuePath = "intID";

            cmbPais.SelectedValue = p.intID;
            cmbEstado.ItemsSource = entidad.Estado;
            cmbEstado.DisplayMemberPath = "strNombreEstado";
            cmbEstado.SelectedValuePath = "intID";
            cmbEstado.SelectedValue = es.intID;
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {

            try {

                string strMunicipio = "";
                string strLocalidad = "";
                string strColonia = "";



                if (txtMunicipio.Text != "")
                {
                    strMunicipio = txtMunicipio.SelectedValue + "-" + txtMunicipio.Text.Split('-')[1];


                }
                if (txtPoblacion.Text != "")
                {
                    strLocalidad = txtPoblacion.SelectedValue + "-" + txtPoblacion.Text.Split('-')[1];
                }
                if (txtColonia.Text != "")
                {
                    strColonia = txtColonia.SelectedValue + "-" + txtColonia.Text.Split('-')[1];
                }

                string Calle = txtCalle.Text;
                string NoExterior = txtNoExterior.Text;
                string NoInterior = txtNoInterior.Text;
                string Colonia = strColonia;
                int Pais = Convert.ToInt32(cmbPais.SelectedValue);
                int Estado = Convert.ToInt32(cmbEstado.SelectedValue.ToString());
                string Municipio = strMunicipio;
                string Poblacion = strLocalidad; ;
                string Codigo = txtCP.Text.Trim();

                BusClientes bus = new BusClientes();

                if (bus.AgregarDireccionFiscal(Calle,
    NoExterior, NoInterior, Colonia, Pais, Estado, Municipio, Poblacion, Codigo, Convert.ToInt32(App.Current.Properties["idEmpresa"])))
                {
                    MessageBox.Show( "La nueva dirección ha sido Registrada Exitosamente", "Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                  
                }
                else
                {
                    MessageBox.Show("Ocurrio un Error durante el registro, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
            }
            catch (Exception ex) { }

        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {
           // DialogResult = false;
               
        }

       

        

       

        private void cmbEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var idEstadoOr = cmbEstado.SelectedValue;

                string value = idEstadoOr.ToString();

                int value2 = int.Parse(value);

                var getValue = entidad.Estado.FirstOrDefault(edo => edo.intID == value2);

                string strEstadoOr = getValue.strNombreEstado.ToString().Split('-')[0];

                wpfEFac.Models.Localidad loc = new Models.Localidad();
                txtPoblacion.ItemsSource = entidad.Localidad.Where(a => a.str_descripcion.Contains(strEstadoOr + "-"));
                txtPoblacion.DisplayMemberPath = "str_descripcion";
                txtPoblacion.SelectedValuePath = "str_codigo";
                txtPoblacion.SelectedValue = loc.id;


                wpfEFac.Models.Municipio mun = new Models.Municipio();
                txtMunicipio.ItemsSource = entidad.Municipio.Where(a => a.str_descripcion.Contains(strEstadoOr + "-"));
                txtMunicipio.DisplayMemberPath = "str_descripcion";
                txtMunicipio.SelectedValuePath = "str_codigo";
                txtMunicipio.SelectedValue = mun.id;

            }
            catch (Exception ex)
            {

            }
        }

        private void txtCP_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int chars = txtCP.Text.Length;

                if (chars == 5)
                {
                    wpfEFac.Models.Colonia col = new Models.Colonia();

                    string strCP = txtCP.Text;

                    txtColonia.ItemsSource = entidad.Colonia.Where(a => a.str_descripcion.Contains(strCP + "-"));
                    txtColonia.DisplayMemberPath = "str_descripcion";
                    txtColonia.SelectedValuePath = "str_codigo";
                    txtColonia.SelectedValue = col.id;


                }

            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error buscar Colonia " + ex );


            }
        }
    }
}
