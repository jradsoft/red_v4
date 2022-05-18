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
using wpfEFac.Views.Empresa;
using wpfEFac.Models;

namespace wpfEFac
{
    /// <summary>
    /// Interaction logic for ConfigEmpresa.xaml
    /// </summary>
    public partial class ConfigEmpresa : Page
    {
        private PreFacturaViewModel data;
        private DataPreFactura prefectura;
        private int id;

        public ConfigEmpresa()
        {
            InitializeComponent();
            LoadInfoEmpresa();


            
        }

        private void LoadInfoEmpresa()
        {
            data = new PreFacturaViewModel();
            prefectura = new DataPreFactura();

            var empresa =
            data.GetEmpresa(Convert.ToInt32
            (App.Current.Properties["idUsuario"]));

            var direccion =
            prefectura.GetDireccionesFiscalesEmpresa(Convert.ToInt32
            (App.Current.Properties["idEmpresa"]));

            id = empresa.intID;
            txbProducto.Text = empresa.strRazonSocial;
            txbCodi.Text = empresa.strNombreComercial;
            txbArticulos.Text = empresa.strGiro;
            txbHonorarios.Text = empresa.intID_CFD.ToString();
            txbConocido.Text = direccion.strCalle;
            txbConocido1.Text = direccion.strNoExterior;
            txbNo_Interior.Text = direccion.strNoInterior;
            txbConocido2.Text = direccion.strColonia;
            txbMexico.Text = direccion.intIDPais.ToString();
            txbAguascalientes.Text = direccion.intIDEstado.ToString();
            txbMunicipioD.Text = direccion.strMunicipio;
            txbDelegacion.Text = direccion.strPoblacionLocalidad;
            txb00500.Text = direccion.strCodigoPostal;
            txb525551426682.Text = empresa.strTelefono;
            txb52.Password = empresa.strTelefono2;
            txb521.Text = empresa.strTelefonoMovil;
            txbcprenegtz.Text = empresa.strEmail;
            txbEmail2D.Text = empresa.strEmail2;
            txbWebSiteD.Text = empresa.strWebSite;
        }

        private void bttEditar_Click(object sender, RoutedEventArgs e)
        {
            EditarEmpresa editarEmpresa = new EditarEmpresa(id);
            Nullable<bool> resultado = editarEmpresa.ShowDialog();

            if (resultado.Value)
            {
                LoadInfoEmpresa();
            }
        }

        private void bttAgregar_Click(object sender, RoutedEventArgs e)
        {
            Registro objRegistro = new Registro();
            objRegistro.Show();
        }
    }
}
