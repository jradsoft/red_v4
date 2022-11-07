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
using System.Windows.Forms;
using wpfEFac.Models;
using System.Text.RegularExpressions;
using wpfEFac.Helpers;

namespace wpfEFac.Views.Empresa
{
    /// <summary>
    /// Interaction logic for EditarEmpresa.xaml
    /// </summary>
    public partial class EditarEmpresa : Window
    {
        private EditarEmpresaViewModel ctx;
        private int id;
        eFacDBEntities db;

        public EditarEmpresa(int id)  
        {
            InitializeComponent();
            db = new eFacDBEntities();
            this.id = id;
            ctx = new EditarEmpresaViewModel();
            this.Loaded += EditarEmpresa_Loaded;

            cmbRegimenFiscal.Items.Clear();
            cmbRegimenFiscal.ItemsSource = FactCat.getListRegimen();
            cmbRegimenFiscal.DisplayMemberPath = "descripcion";
            cmbRegimenFiscal.SelectedValuePath = "clave";

            
        }

        protected void EditarEmpresa_Loaded(object sender, RoutedEventArgs e)
        {
        
            var emp = ctx.GetEmpresa(id);

            var direccion = ctx.GetDireccion(emp.intID);

            LoadEmpresa(emp);

            LoadCombos(emp);

            LoadDireccionFiscal(direccion);

            LoadCorreos(emp);

            LoadCertificado(emp);

            LoadFolios(emp);
            
        }

        private void LoadFolios(Models.Empresa empresa)
        {
            this.txtNumeroAprobacion.Text = empresa.Folios.FirstOrDefault().intNumero_Aprovacion.ToString();
            this.txtSerie.Text = empresa.Folios.FirstOrDefault().strSerie;
            this.txtAñoAprobacion.Text = empresa.Folios.FirstOrDefault().strAño_Aprovacion;
            this.txtFolioInicio.Text = empresa.Folios.FirstOrDefault().intFolio_Inicial.ToString();
            this.txtFolioFinal.Text = empresa.Folios.FirstOrDefault().intFolio_Final.ToString();
            this.txtFolioActual.Text = empresa.Folios.FirstOrDefault().intFolioActual.ToString();

            cmbSerie.ItemsSource = ctx.GetFolios();
            cmbSerie.DisplayMemberPath = "strSerie";
            cmbSerie.SelectedValuePath = "intID";
            cmbSerie.SelectedValue = empresa.Folios.FirstOrDefault().intID;
        }

        private void LoadCertificado(Models.Empresa empresa)
        {
            this.txtNumeroCertificadoSello.Text = empresa.Certificates.FirstOrDefault().strNumeroCertificadoSelloDigital;
            this.txtRutaCer.Text = empresa.Certificates.FirstOrDefault().strCertificadoSelloDigitalPath;
            this.txtArchivoKey.Text = empresa.Certificates.FirstOrDefault().strLlaveCertificadoPath;
            this.pswContraseña.Password = empresa.Certificates.FirstOrDefault().strContraseñaSAT;
            this.dtpFechaValidacion.SelectedDate = empresa.Certificates.FirstOrDefault().dtmValidoDesde;
        }

        private void LoadCorreos(Models.Empresa emp)
        {
            try
            {
                txtCorreoContador.Text = emp.ConfiguracionEmail.Single(ce => ce.intID_Empresa == emp.intID).strE_MailContador;
                txtCorreoRespaldo.Text = emp.ConfiguracionEmail.Single(ce => ce.intID_Empresa == emp.intID).strE_MailRespaldo;
                pswPasswordCorreo.Password = emp.ConfiguracionEmail.Single(ce => ce.intID_Empresa == emp.intID).strPasswordEmail;
                txtPuerto.Text = emp.ConfiguracionEmail.Single(ce => ce.intID_Empresa == emp.intID).intPort.ToString();
                txtHost.Text = emp.ConfiguracionEmail.Single(ce => ce.intID_Empresa == emp.intID).strSMTPHost;
                //rdbEnableSSL.IsChecked = emp.ConfiguracionEmail.Single(ce => ce.intID_Empresa == emp.intID).;
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show("No existe la configuracion de correo");
            }
            
        }

        private void LoadDireccionFiscal(Direcciones_Fiscales direccion)
        {
            txtCalleReceptor.Text = direccion.strCalle;

            txtNoInterior.Text = direccion.strNoInterior;

            txtNoExterior.Text = direccion.strNoExterior == null ? "<No tiene>" : direccion.strNoExterior;

            txtPoblacion.Text = direccion.strPoblacionLocalidad;

            txtCP.Text = direccion.strCodigoPostal;

            txtColonia.Text = direccion.strColonia;

            txtMunicipo.Text = direccion.strMunicipio;

            cmbEstado.ItemsSource = ctx.GetEstado();
            cmbEstado.DisplayMemberPath = "strNombreEstado";
            cmbEstado.SelectedValuePath = "intID";
            cmbEstado.SelectedValue = direccion.Estado.intID;

            cmbPais.ItemsSource = ctx.GetPais();
            cmbPais.DisplayMemberPath = "strNombrePais";
            cmbPais.SelectedValuePath = "intID";
            cmbPais.SelectedValue = direccion.Paises.intID;
        }

        private void LoadEmpresa(Models.Empresa emp)
        {
            txtRFC.Text = emp.strRFC;

            txtRazonSocial.Text = emp.strRazonSocial;

            txtNombreComercial.Text = emp.strNombreComercial;

            txtGiro.Text = emp.strGiro;

            txtTelefono.Text = emp.strTelefono;

            txtCelular.Text = emp.strTelefonoMovil;

            txtEmailEmpresa.Text = emp.strEmail;

            txtEmailContador.Text = emp.strEmail2 == null ? "<No tiene>" : emp.strEmail2;

            cmbRegimenFiscal.SelectedValue = emp.strWebSite;

           // txtWebSite.Text = emp.strWebSite == null ? "<No tiene>" : emp.strWebSite;

            txtTelefono1.Password = emp.strTelefono2;

            txtLogo.Text = emp.strLogo;

            txtCedula.Text = emp.strCedula;

            txtRutaPDF.Text = emp.strDirectorioPDF;

            txtRutaXML.Text = emp.strDirectorioXML;

           
        }

        private void LoadCombos(Models.Empresa emp)
        {
            
            
            cmbTipoComprobante.ItemsSource = ctx.GetCFD();
            cmbTipoComprobante.DisplayMemberPath = "strDescripcion";
            cmbTipoComprobante.SelectedValuePath = "intID";
            cmbTipoComprobante.SelectedValue = emp.intID_CFD;

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtEmail1_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void bttExaminar_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.FileName = "Logo";
            ofd.DefaultExt = ".jpg";
            ofd.Filter = "Imagenes JPG(*.jpg)|*.jpg|Imagenes PNG(*.png)|*.png|Imagenes GIF(*.gif)|*.gif|Imagenes JPE(*.jpe)|*jpe|Imagenes BMP(*.bmp)|*.bmp|Imagenes DIB(*.dib)|*bid|Imagenes TIF(*.tif)|*tif|Imagenes WMF(*.wmf)|*.wmf|Imagenes RAS(*.ras)|*.ras|Imagenes EPS(*.eps)|*.eps|Imagenes PCX(*.pcx)|*.pcx|Imagenes PCD(*.pcd)|*.pcd|Imagenes TGA(*.tga)|*.tga|Todos los Archivos|*.*";

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                txtLogo.Text = ofd.FileName;               
            }
        }

        private void bttExaminarCedula_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.FileName = "Cedula";
            ofd.DefaultExt = ".jpg";
            ofd.Filter = "Imagenes JPG(*.jpg)|*.jpg|Imagenes PNG(*.png)|*.png|Imagenes GIF(*.gif)|*.gif|Imagenes JPE(*.jpe)|*jpe|Imagenes BMP(*.bmp)|*.bmp|Imagenes DIB(*.dib)|*bid|Imagenes TIF(*.tif)|*tif|Imagenes WMF(*.wmf)|*.wmf|Imagenes RAS(*.ras)|*.ras|Imagenes EPS(*.eps)|*.eps|Imagenes PCX(*.pcx)|*.pcx|Imagenes PCD(*.pcd)|*.pcd|Imagenes TGA(*.tga)|*.tga|Todos los Archivos|*.*";

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                txtCedula.Text = ofd.FileName;
            }
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               

                var regimenFiscal = cmbRegimenFiscal.SelectedValue.ToString();
               // System.Windows.MessageBox.Show(regimenFiscal);


                if (ctx.UpdateEmpresa(id, txtRazonSocial.Text,
                    txtNombreComercial.Text, txtGiro.Text, txtRFC.Text,
                    Convert.ToInt32(cmbTipoComprobante.SelectedValue),
                    txtTelefono.Text, txtCelular.Text, regimenFiscal, txtTelefono1.Password,
                    txtLogo.Text, txtCedula.Text, txtRutaXML.Text,
                    txtRutaPDF.Text, txtCalleReceptor.Text, txtColonia.Text,
                    txtNoInterior.Text, txtNoExterior.Text,
                    Convert.ToInt32(cmbPais.SelectedValue),
                    Convert.ToInt32(cmbEstado.SelectedValue),
                    txtMunicipo.Text, txtPoblacion.Text, txtCP.Text, "1", txtEmailEmpresa.Text, txtEmailContador.Text))
                {
                    System.Windows.MessageBox.Show("Se guardo con exito", "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true ;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Error al guardar intentelo mas tarde", "Errorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetValuesEmpresa(Models.Empresa emp)
        {
            throw new NotImplementedException();
        }

        private void bttcancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnRutaXML_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbdDialog = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult resultado = fbdDialog.ShowDialog();
            if (resultado == System.Windows.Forms.DialogResult.OK)
            {
                txtRutaXML.Text = fbdDialog.SelectedPath;
            }

        }

        private void btnRutaPDF_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbdDialog = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult resultado = fbdDialog.ShowDialog();
            if (resultado == System.Windows.Forms.DialogResult.OK)
            {
                txtRutaPDF.Text = fbdDialog.SelectedPath;
            }
        }

        private void bttGuardarCorreo_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarConfiguracionCorreoElectronico())
            {
                try 
	            {	        
		            var emp = ctx.GetEmpresa(id);

                    if (ctx.UpdateConfiguracionCorreoElectronico(emp, emp.ConfiguracionEmail.Single(ce => ce.intID_Empresa == emp.intID).intID,
                        txtHost.Text, int.Parse(txtPuerto.Text), txtCorreoRespaldo.Text,
                        txtCorreoContador.Text, pswPasswordCorreo.Password, txtEmail1.Text, txtEmail2.Text, rdbEnableSSL.IsChecked.Value))
                    {
                        System.Windows.MessageBox.Show("Se guardaron los cambios", "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        throw new ApplicationException();
                    }
	            }
	            catch (Exception)
	            {
                    System.Windows.MessageBox.Show("Ocurrio un error inesperado intentelo mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
	            }
            }
            else
            {
                System.Windows.MessageBox.Show("Error de validacion verifique los datos de correo electronico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidarConfiguracionCorreoElectronico()
        {
            return ValidarHost() && ValidarPuerto() &&
                ValidarCuentaCorreoRespaldo() &&
                ValidarCuentaCorreoContador() &&
                ValidarPassword() &&
                ValidarCorreoEmpresarial() &&
                ValidarCorreoAlternativo();
        }

        private bool ValidarCorreoEmpresarial()
        {
            return Validador.ValidarCorreoElectronico(txtEmail1.Text);
        }

        private bool ValidarCorreoAlternativo()
        {
            return Validador.ValidarCorreoElectronico(txtEmail2.Text);
        }

        private bool ValidarPassword()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pswPasswordCorreo.Password))
                {
                    throw new ApplicationException("El password no puede ser vacio");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ValidarCuentaCorreoContador()
        {
            return Validador.ValidarCorreoElectronico(txtCorreoContador.Text);
        }

        private bool ValidarCuentaCorreoRespaldo()
        {
            return Validador.ValidarCorreoElectronico(txtCorreoRespaldo.Text);
        }

        private bool ValidarPuerto()
        {
            try
            {
                Validador.TextBoxNumeroEntero(txtPuerto, string.Empty);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ValidarHost()
        {
            return (!string.IsNullOrWhiteSpace(txtHost.Text));
        }

        private void txtHost_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarHost())
            {
                txtHost.BorderBrush = new System.Windows.Controls.TextBox().BorderBrush;
                txtErrorHost.Content = string.Empty;
            }
            else
            {
                txtHost.BorderBrush = new SolidColorBrush(Colors.Red);
                txtErrorHost.Content = "El host es invalido por favor corrija los errores";
            }
        }

        private void txtPuerto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarPuerto())
            {
                txtPuerto.BorderBrush = new System.Windows.Controls.TextBox().BorderBrush;
                txtErrorPuerto.Content = string.Empty;
            }
            else
            {
                txtPuerto.BorderBrush = new SolidColorBrush(Colors.Red);
                txtErrorPuerto.Content = "El puerto no es un numero valido";
            }
        }

        private void txtCorreoRespaldo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCuentaCorreoRespaldo())
            {
                txtCorreoRespaldo.BorderBrush = new System.Windows.Controls.TextBox().BorderBrush;
                txtErrorCorreoRespaldo.Content = string.Empty;
            }
            else
            {
                txtCorreoRespaldo.BorderBrush = new SolidColorBrush(Colors.Red);
                txtErrorCorreoRespaldo.Content = "El correo de respaldo no es una direccion valida";
            }
        }

        private void txtCorreoContador_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCuentaCorreoContador())
            {
                txtCorreoContador.BorderBrush = new System.Windows.Controls.TextBox().BorderBrush;
                txtErrorCorreoContador.Content = string.Empty;
            }
            else
            {
                txtCorreoContador.BorderBrush = new SolidColorBrush(Colors.Red);
                txtErrorCorreoContador.Content = "El correo del contador no es una direccion valida";
            }

        }

        private void pswPasswordCorreo_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ValidarPassword())
            {
                pswPasswordCorreo.BorderBrush = new System.Windows.Controls.TextBox().BorderBrush;
                txtErrorPaassword.Content = string.Empty;
            }
            else
            {
                pswPasswordCorreo.BorderBrush = new SolidColorBrush(Colors.Red);
                txtErrorPaassword.Content = "El password es un valor requerido";
            }
        }

        private void txtEmail1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (ValidarCorreoEmpresarial())
            {
                txtEmail1.BorderBrush = new System.Windows.Controls.TextBox().BorderBrush;
                txtErrorEmailEmpresarial.Content = string.Empty;
            }
            else
            {
                txtEmail1.BorderBrush = new SolidColorBrush(Colors.Red);
                txtErrorEmailEmpresarial.Content = "El Correo empresarial es invalido";
            }
        }

        private void txtEmail2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCorreoAlternativo())
            {
                txtEmail2.BorderBrush = new System.Windows.Controls.TextBox().BorderBrush;
                txtErrorEmailAlternativo.Content = string.Empty;
            }
            else
            {
                txtEmail2.BorderBrush = new SolidColorBrush(Colors.Red);
                txtErrorEmailAlternativo.Content = "El Correo empresarial es invalido";
            }
        }

        private void bttBuscarCer_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.DefaultExt = ".cer";
            ofd.Filter = "Certificado de sello digital (*.cer)|*.cer|Todos los Archivos|*.*";

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                txtRutaCer.Text = ofd.FileName;
            }
        }

        private void bttBuscarKey_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.DefaultExt = ".key";
            ofd.Filter = "Llave privada (*.key)|*.key|Todos los Archivos|*.*";

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                txtArchivoKey.Text = ofd.FileName;
            }
        }

        private void bttGuardarCertificado_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCertificado())
            {
                var emp = ctx.GetEmpresa(id);

                if (ctx.UpdateCer(emp, txtNumeroCertificadoSello.Text, txtRutaCer.Text,
                    txtArchivoKey.Text, pswContraseña.Password, dtpFechaValidacion.SelectedDate.Value,
                    dtpFechaValidacion.SelectedDate.Value.AddYears(2), DateTime.Now))
	            {
                    System.Windows.MessageBox.Show("Se guardaron los cambios",
                        "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);
	            }
                else
                {
                    System.Windows.MessageBox.Show("Error al guardar verifique sus datos",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool ValidarCertificado()
        {
            if (string.IsNullOrWhiteSpace(txtNumeroCertificadoSello.Text))
            {
                System.Windows.MessageBox.Show("Error el numero de certificado"+
                " no pede ser vacio",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRutaCer.Text))
            {
                System.Windows.MessageBox.Show("Error la ruta del archivo .cer" +
                " no puede quedar vacia",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtArchivoKey.Text))
            {
                System.Windows.MessageBox.Show("Error la ruta del archivo .key" +
                " no puede quedar vacia",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(pswContraseña.Password))
            {
                System.Windows.MessageBox.Show("Error la contraseña" +
                " no puede quedar vacia",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (dtpFechaValidacion.SelectedDate.HasValue == false)
            {
                System.Windows.MessageBox.Show("Error la fecha de validacion" +
                " no puede quedar vacia",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void bttGuardarFolio_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFolio())
            {
                var emp = ctx.GetEmpresa(id);

                if (ctx.UpdateFolios(emp, int.Parse(txtFolioInicio.Text),
                    int.Parse(txtFolioFinal.Text), int.Parse(txtNumeroAprobacion.Text),
                    txtSerie.Text, txtAñoAprobacion.Text, int.Parse(txtFolioActual.Text)))
                {
                    System.Windows.MessageBox.Show("Se guardaron los cambios",
                        "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Error al guardar verifique sus datos",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool ValidarFolio()
        {
            int numeroAprobacion;
            int añoAprobacion;
            int folioInicial;
            int folioFinal;
            int folioActual;

            if (!int.TryParse(txtNumeroAprobacion.Text, out numeroAprobacion))
            {
                System.Windows.MessageBox.Show("Error el numero de aprobacion debe ser un numero",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            /*
            if (string.IsNullOrWhiteSpace(txtSerie.Text))
            {
                System.Windows.MessageBox.Show("Error la serie no puede quedar vacia",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            */
            if (!int.TryParse(txtAñoAprobacion.Text, out añoAprobacion))
            {
                System.Windows.MessageBox.Show("Error el año de aprobacion debe ser un numero",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(txtFolioInicio.Text, out folioInicial))
            {
                System.Windows.MessageBox.Show("Error el folio inicial debe ser un numero",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(txtFolioFinal.Text, out folioFinal))
            {
                System.Windows.MessageBox.Show("Error el folio final debe ser un numero",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(txtFolioActual.Text, out folioActual))
            {
                System.Windows.MessageBox.Show("Error el folio actual debe ser un numero",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void bttAddDireccion_Click(object sender, RoutedEventArgs e)
        {
            NuevaDireccion newDir = new NuevaDireccion();
            newDir.Show();
        }

        private void btnAgregarFolios_Click(object sender, RoutedEventArgs e)
        {
            AgregarSerieFolio newSF = new AgregarSerieFolio();
            newSF.Closed += (s, args) =>
            {
                if (newSF.DialogResult.Value)
                {
                    this.Loaded += EditarEmpresa_Loaded;
                }
            };
            newSF.ShowDialog();
        }

        private void cmbSerie_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                var idFolio = cmbSerie.SelectedValue.ToString();

                int intFolio = int.Parse(idFolio);

                var value = db.Folios.FirstOrDefault(x => x.intID == intFolio);

                this.txtIdFolio.Text = intFolio.ToString();
                this.txtNumeroAprobacion.Text = value.intNumero_Aprovacion.ToString();
                this.txtSerie.Text = value.strSerie;
                this.txtAñoAprobacion.Text = value.strAño_Aprovacion;
                this.txtFolioInicio.Text = value.intFolio_Inicial.ToString();
                this.txtFolioFinal.Text = value.intFolio_Final.ToString();
                this.txtFolioActual.Text = value.intFolioActual.ToString();
            }
            catch (Exception ex) { }
        }

    }
}
