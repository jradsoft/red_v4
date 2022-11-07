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
    /// Interaction logic for Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        private eFacDBEntities entidad;
        string CerPath;
        public Registro()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
           
            this.Loaded += Registro_Loaded;
        }

        protected void Registro_Loaded(object sender, RoutedEventArgs e)
        {
            bttGuardar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnGuardarCer.IsEnabled = false;
            tbiRegistroFolios.IsEnabled = false;
            tbiRegistroCertificados.IsEnabled = false;

            cmbPais.ItemsSource = entidad.Paises;
            cmbPais.DisplayMemberPath = "strNombrePais";
            cmbEstado.ItemsSource = entidad.Estado;
            cmbEstado.DisplayMemberPath = "strNombreEstado";
        }

        private void bttExaminar_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
            file.DefaultExt = ".jpg";
            file.FileName = "Logo de la Empresa";
            file.Filter = "Logo(.jpg)|*.jpg|Files All|*.*";

            Nullable<bool> result = file.ShowDialog();

            if (result == true)
            {
                txtLogo.Text = file.FileName;
            }
        }

        private void bttExaminarCedula_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
            file.FileName = "Cedula";
            file.DefaultExt = ".jpg";
            file.Filter = "Cedula(.jpg)|*.jpg|Files All|*.*";

            Nullable<bool> result = file.ShowDialog();

            if (result == true)
            {
                txtCedula.Text = file.FileName;
            }
            bttGuardar.IsEnabled = true;
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtRFC.Text != string.Empty && txtRazonSocial.Text != string.Empty && txtNombreComercial.Text != string.Empty &&
                txtGiro.Text != string.Empty && cmbTipoComprobante.Text != string.Empty)
            {
                var idEmpresa = GetID_Empresa();

                string ID = idEmpresa.ToString();
                string RFC = txtRFC.Text;
                string RazonSocial = txtRazonSocial.Text;
                string NombreComercial = txtNombreComercial.Text;
                string Giro = txtGiro.Text;
                string TipoComprobante = cmbTipoComprobante.Text;
                string Calle = txtCalleReceptor.Text;
                string NoExterior = txtNoExterior.Text;
                string NoInterior = txtNoInterior.Text;
                string Colonia = txtColonia.Text;
                int Pais = int.Parse(cmbPais.SelectedValuePath);
                int Estado = int.Parse(cmbEstado.SelectedValuePath);
                string Municipio = txtMunicipio.Text;
                string Poblacion = txtPoblacion.Text;
                string CP = txtCP.Text;
                string Telefono = txtTelefono.Text;
                string Telefono2 = txtTelefono1.Text;
                string Celular = txtCelular.Text;
                string Email = txtEmail1.Text;
                string Email1 = txtEmail2.Text;
                string WebSite = txtWebSite.Text;
                string Logo = txtLogo.Text;
                string Cedula = txtCedula.Text;

                BusClientes bus = new BusClientes();
                if (bus.AgregarEmpresa(/*ID,*/ RFC, RazonSocial, NombreComercial, Giro, TipoComprobante, Calle,
                    NoExterior, NoInterior, Colonia, Pais, Estado, Municipio, Poblacion, CP, Telefono, Telefono2,
                    Celular, Email, Email1, WebSite, Logo, Cedula))
                {
                    MessageBox.Show("La Empresa " + RazonSocial + " ha sido Registrada Exitosamente", "Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBox.Show("Por favor Registra tu Certificado", "Registro", MessageBoxButton.OK, MessageBoxImage.Information);
                    tbiRegistroEmpresa.IsEnabled = false;
                    bttGuardar.IsEnabled = false;
                    bttcancelar.IsEnabled = false;
                    tbiRegistroCertificados.IsEnabled = true;
                    btnGuardarCer.IsEnabled = true;
                    btnCancelarCer.IsEnabled = true;
                    tbiRegistroCertificados.IsSelected = true;
                }
                else
                {
                    MessageBox.Show("Ocurrio un Error durante el registro, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ValidarRFC();
                MessageBox.Show("Debes llenar todos los Campos requeridos, por favor llenalos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        private int GetID_Empresa()
        {
            var idEmpresa = entidad.Empresa.Count();
            idEmpresa += 1;
            return idEmpresa;
        }

        private void bttcancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();            
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtFolioInicial.Text != string.Empty && txtFolioFinal.Text != string.Empty &&
                txtNoAprobacion.Text != string.Empty && txtSerie.Text != string.Empty &&
                txtAnioAprobacion.Text != string.Empty && txtFolioActual.Text != string.Empty)
            {
                var ID_Folios = entidad.Folios.Count();
                ID_Folios += 1;

                var IDCertificado = GetID_Certificado();
                IDCertificado -= 1;

                var IDEmpresa = GetID_Empresa();
                IDEmpresa -= 1;

                int ID = ID_Folios;
                int ID_Certificado = IDCertificado;
                string Inicial = txtFolioInicial.Text;
                string Final = txtFolioFinal.Text;
                string NoAprobacion = txtNoAprobacion.Text;
                string Serie = txtSerie.Text;
                string AnioAprobacion = txtAnioAprobacion.Text;
                int ID_Empresa = IDEmpresa;
                string FolioActual = txtFolioActual.Text;

                BusClientes bus = new BusClientes();
                //if (bus.AgregarFolios(/*ID,*/ ID_Certificado, Inicial, Final, NoAprobacion, Serie, AnioAprobacion, ID_Empresa, FolioActual))
                //{
                //    MessageBox.Show("Los Folios han sido Registrados Exitosamente", "Registrados", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //else
                //{
                //    MessageBox.Show("Ocurrio un Error durante el Registro, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
                this.Close();
            }
        }

        private void btnExaminar_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.FileName = "Certificado";
            open.DefaultExt = ".cer";
            open.Filter = "Certificado Sello Digital(.cer)|*.cer|Files All|*.*";

            Nullable<bool> result = open.ShowDialog();

            if (result == true)
            {
                txtLlaveCertificadoSelloDigital.Text = open.SafeFileName;
                CerPath = open.FileName;
            }
        }        

        private void btnExaminarKey_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open = new Microsoft.Win32.OpenFileDialog();
            open.FileName = "Llave Privada";
            open.DefaultExt = ".key";
            open.Filter = "Llave Privada(.key)|*.key|Files All|*.*";

            Nullable<bool> result = open.ShowDialog();

            if (result == true)
            {
                txtKEY.Text = open.FileName;
            }
        }

        private void btnGuardarCer_Click(object sender, RoutedEventArgs e)
        {
            if (txtLlaveCertificadoSelloDigital.Text != string.Empty && txtLlaveCertificado.Text != string.Empty &&
                txtKEY.Text != string.Empty && pwbContraseniaSAT.Password != string.Empty &&
                dtpDesde.Text != string.Empty && dtpHasta.Text != string.Empty)
            {
                var ID_Certificados = GetID_Certificado();

                var idEmpresa = GetID_Empresa();
                idEmpresa -= 1;

                string ID = ID_Certificados.ToString();
                string CertificadoSelloDigital = txtLlaveCertificadoSelloDigital.Text;
                string LlaveCertificado = txtLlaveCertificado.Text;
                string CSD = CerPath;
                string Key = txtKEY.Text;
                string ContraseniaSAT = pwbContraseniaSAT.Password;
                string ValidoDesde = dtpDesde.Text;
                string ValidoHasta = dtpHasta.Text;
                string FechaSubida = DateTime.Now.ToShortDateString();
                int Empresa = idEmpresa;

                BusClientes bus = new BusClientes();
                if (bus.AgregarCertificados(/*ID,*/ CertificadoSelloDigital, LlaveCertificado, CSD, Key, ContraseniaSAT,
                    ValidoDesde, ValidoHasta, FechaSubida, Empresa))
                {
                    MessageBox.Show("El Certificado " + CertificadoSelloDigital + " ha sido Registrado Exitosamente", "Registrado", MessageBoxButton.OK, MessageBoxImage.Information);
                    MessageBox.Show("Por favor Registra tus Folios", "Registrado", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnGuardarCer.IsEnabled = false;
                    btnCancelarCer.IsEnabled = false;
                    tbiRegistroCertificados.IsEnabled = false;
                    btnGuardar.IsEnabled = true;
                    btnCancelar.IsEnabled = true;
                    tbiRegistroFolios.IsEnabled = true;
                    tbiRegistroFolios.IsSelected = true;
                }
                else
                {
                    MessageBox.Show("Ocurrio un Error durante el Registro, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private int GetID_Certificado()
        {
            var ID_Certificados = entidad.Certificates.Count();
            ID_Certificados += 1;
            return ID_Certificados;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelarCer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidarRFC()
        {
            if (string.IsNullOrWhiteSpace(txtRFC.Text))
            {
                txtRFC.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                txtRFC.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
            return true;
        }

        private void txtRFC_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarRFC();
        }

        public bool ValidarRazonSocial()
        {
            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
            {
                txtRazonSocial.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                txtRazonSocial.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
            return true;
        }

        public bool ValidarNombreComercial()
        {
            if (string.IsNullOrWhiteSpace(txtNombreComercial.Text))
            {
                txtNombreComercial.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                txtNombreComercial.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
            return true;
        }

        public bool ValidarGiro()
        {
            if (string.IsNullOrWhiteSpace(txtGiro.Text))
            {
                txtGiro.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                txtGiro.BorderBrush = new SolidColorBrush(Colors.LightGray);                    
            }
            return true;
        }
    } 
}
