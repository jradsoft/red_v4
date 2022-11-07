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
    /// Lógica de interacción para AgregarSerieFolio.xaml
    /// </summary>
    public partial class AgregarSerieFolio : Window
    {

        private eFacDBEntities entidad;
        private EditarEmpresaViewModel ctx;


        public AgregarSerieFolio()
        {
            InitializeComponent();
            ctx = new EditarEmpresaViewModel();
            entidad = new eFacDBEntities();
        }

        private void btnAddNewSerie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtFolioInicial.Text != string.Empty && txtSerie.Text != string.Empty &&
                     txtFolioActual.Text != string.Empty)
                {


                  

                   

                   // int ID = intIdFolio;
                    int ID_Certificado = 1;
                    string Inicial = txtFolioInicial.Text;
                    string Final = "50000";//txtFolioFinal.Text;
                    string NoAprobacion = "100564995";//txtNoAprobacion.Text;
                    string Serie = txtSerie.Text;
                    string AnioAprobacion = "2022";//txtAnioAprobacion.Text;
                    int ID_Empresa = 1;
                    string FolioActual = txtFolioActual.Text;

                    BusClientes bus = new BusClientes();
                    if (bus.AgregarFolios(ID_Certificado, Inicial, Final, NoAprobacion, Serie, AnioAprobacion, ID_Empresa, FolioActual,cmbTipoComprobante.Text,txtDescripcion.Text))
                    {
                        MessageBox.Show("El Folio han sido Agregado Exitosamente", "Folio Agregado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un Error durante el Registro, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    this.Close();
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex + " Ocurrio un Error durante el Registro, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*
        private int GetID_Certificado()
        {
            var ID_Certificados = entidad.Certificates.Count();
            ID_Certificados += 1;
            return ID_Certificados;
        }

        private int GetID_Empresa()
        {
            var idEmpresa = entidad.Empresa.Count();
            idEmpresa += 1;
            return idEmpresa;
        }*/

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtFolioInicial_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtFolioInicial.Text, "[^0-9]"))
            {
                //MessageBox.Show("Please enter only numbers.");
                txtFolioInicial.Text = txtFolioInicial.Text.Remove(txtFolioInicial.Text.Length - 1);
            }

        }

        private void txtFolioActual_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtFolioActual.Text, "[^0-9]"))
            {
                //MessageBox.Show("Please enter only numbers.");
                txtFolioActual.Text = txtFolioActual.Text.Remove(txtFolioActual.Text.Length - 1);
            }
        }
    }
}
