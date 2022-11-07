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
using System.Threading;

namespace wpfEFac.Views.Clientes
{
    /// <summary>
    /// Interaction logic for EditarCliente.xaml
    /// </summary>
    public partial class EditarCliente : Window
    {
        private eFacDBEntities entidad;
        private wpfEFac.Models.EditarClienteViewModel cliente;
        private int id;

        public EditarCliente(int id)
        {
            

            InitializeComponent();
            this.id = id;
            entidad = new eFacDBEntities();
            cliente = new Models.EditarClienteViewModel();
            this.Loaded += EditarCliente_Loaded;

            try
            {
                txtGiro.Items.Clear();
                txtGiro.ItemsSource = FactCat.getListRegimen();
                txtGiro.DisplayMemberPath = "descripcion";
                txtGiro.SelectedValuePath = "clave";
            }
            catch (Exception ex) { }
     



            var cli = cliente.GetCliente(id);
            //var RFC = cliente.GetRFC(cli.strRFC);
            //var Razon = cliente.GetRazonSocial(cli.strRazonSocial);
            //var comercial = cliente.GetNombreComercial(cli.strNombreComercial);
            //var giro = cliente.GetGiro(cli.strGiro);
            //var tipo = cliente.GetTipoInscripcion(cli.strTipodeInscripcion);
            var calle = cliente.GetCalle(cli.intID);
            //var telefono = cliente.GetTelefono(cli.strTelefono);
            //var telefonomovil = cliente.GetTelefonoMovil(cli.strTelefonoMovil);
            //var Email = cliente.GetEmail(cli.strEmail);
            //var contacto = cliente.GetContacto(cli.strContacto);
            //var web = cliente.GetWeb(cli.strWebSite);

            //*******************************************************************
            txtRFC.Text = cli.strRFC;
            txtRazonSocial.Text = cli.strRazonSocial;
            txtNombreComercial.Text = cli.strNombreComercial;
            
            if (cli.strTipodeInscripcion == "Persona Física")
            {
                cmbTipoComprobante.SelectedIndex = 0;}
            if (cli.strTipodeInscripcion == "Persona Moral")
            {
                cmbTipoComprobante.SelectedIndex = 1;
            }
            if (cli.strTipodeInscripcion == "Origen")
            {
                cmbTipoComprobante.SelectedIndex = 2;
            }
            if (cli.strTipodeInscripcion == "Destino")
            {
                cmbTipoComprobante.SelectedIndex = 3;
            }
            if (cli.strTipodeInscripcion == "01-Operador")
            {
                cmbTipoComprobante.SelectedIndex = 4;
            }
            if (cli.strTipodeInscripcion == "02-Propietario")
            {
                cmbTipoComprobante.SelectedIndex = 5;
            }
            if (cli.strTipodeInscripcion == "03-Arrendador")
            {
                cmbTipoComprobante.SelectedIndex = 6;
            }
            if (cli.strTipodeInscripcion == "04-Notificado")
            {
                cmbTipoComprobante.SelectedIndex = 7;
            }
           // cmbTipoComprobante.Text = cli.strTipodeInscripcion;
            txtCalleReceptor.Text = calle.strCalle;
            txtNoExterior.Text = calle.strNoExterior;
            txtNoInterior.Text = calle.strNoInterior == null ? "<No tiene>" : calle.strNoInterior;
            txtColonia.Text = calle.strColonia == null ? "<No tiene>" : calle.strColonia;
            txtMunicipio.Text = calle.strMunicipio == null ? "<No tiene>" : calle.strMunicipio;
            txtPoblacion.Text = calle.strPoblacionLocalidad == null ? "<No tiene>" : calle.strPoblacionLocalidad;
            txtCP.Text = calle.strCodigoPostal == null ? "<No tiene>" : calle.strCodigoPostal;
            txtTelefono.Text = cli.strTelefono;
            txtCelular.Text = cli.strTelefonoMovil == null ? "<No tiene>" : cli.strTelefonoMovil;
            txtEmail1.Text = cli.strEmail;
            txtContacto.Text = cli.strContacto;
            if (cli.chrRetencionIVA == "Si")
            cmbRetencionIVA.SelectedIndex = 0;
            else
            cmbRetencionIVA.SelectedIndex = 1;
            txtWebSite.Text = cli.strWebSite;
            cmbPais.ItemsSource = entidad.Paises;
            cmbPais.DisplayMemberPath = "strNombrePais";
            cmbEstado.ItemsSource = entidad.Estado;
            cmbEstado.DisplayMemberPath = "strNombreEstado";
            cmbPais.SelectedValuePath = "intID";
            cmbEstado.SelectedValuePath = "intID";

            cmbPais.SelectedValue = calle.Paises.intID;
            cmbEstado.SelectedValue = calle.Estado.intID;
            //cmbAddenda.SelectedIndex = cli.idAddenda.Value== null ? 0: cli.idAddenda.Value;
        }

        protected void EditarCliente_Loaded(object sender, RoutedEventArgs e)
        {
            
            var cli = cliente.GetCliente(id);
            //var RFC = cliente.GetRFC(cli.strRFC);
            //var Razon = cliente.GetRazonSocial(cli.strRazonSocial);
            //var comercial = cliente.GetNombreComercial(cli.strNombreComercial);
            //var giro = cliente.GetGiro(cli.strGiro);
            //var tipo = cliente.GetTipoInscripcion(cli.strTipodeInscripcion);
            var calle = cliente.GetCalle(cli.intID);
            //var telefono = cliente.GetTelefono(cli.strTelefono);
            //var telefonomovil = cliente.GetTelefonoMovil(cli.strTelefonoMovil);
            //var Email = cliente.GetEmail(cli.strEmail);
            //var contacto = cliente.GetContacto(cli.strContacto);
            //var web = cliente.GetWeb(cli.strWebSite);

            //*******************************************************************
            txtRFC.Text = cli.strRFC;
            txtRazonSocial.Text = cli.strRazonSocial;
            txtNombreComercial.Text = cli.strNombreComercial;
            txtGiro.SelectedValue = cli.strGiro;
            //if (cli.strTipodeInscripcion == "Persona Moral")
            //    cmbTipoComprobante.SelectedIndex = 1;
            //else
            //    cmbTipoComprobante.SelectedIndex = 0;
            txtCalleReceptor.Text = calle.strCalle;
            txtNoExterior.Text = calle.strNoExterior;
            txtNoInterior.Text = calle.strNoInterior == null ? "<No tiene>" : calle.strNoInterior;
            txtColonia.Text = calle.strColonia == null ? "<No tiene>" : calle.strColonia;
            txtMunicipio.Text = calle.strMunicipio == null ? "<No tiene>" : calle.strMunicipio;
            txtPoblacion.Text = calle.strPoblacionLocalidad == null ? "<No tiene>" : calle.strPoblacionLocalidad;
            txtCP.Text = calle.strCodigoPostal == null ? "<No tiene>" : calle.strCodigoPostal;
            txtTelefono.Text = cli.strTelefono;
            txtCelular.Text = cli.strTelefonoMovil == null ? "<No tiene>" : cli.strTelefonoMovil;
            txtEmail1.Text = cli.strEmail;
            txtContacto.Text = cli.strContacto;
            
            if (cli.chrRetencionIVA == "Si")
                cmbRetencionIVA.SelectedIndex = 0;
            else
                cmbRetencionIVA.SelectedIndex = 1;

            if (cli.chrRetencionISR == "Si")
                cmbRetencionISR.SelectedIndex = 0;
            else
                cmbRetencionISR.SelectedIndex = 1;

            txtWebSite.Text = cli.strWebSite;
            cmbPais.ItemsSource = entidad.Paises;
            cmbPais.DisplayMemberPath = "strNombrePais";            
            cmbEstado.ItemsSource = entidad.Estado;
            cmbEstado.DisplayMemberPath = "strNombreEstado";
            cmbPais.SelectedValuePath = "intID";
            cmbEstado.SelectedValuePath = "intID";

            cmbPais.SelectedValue = calle.Paises.intID;
            cmbEstado.SelectedValue = calle.Estado.intID;
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {

          
            int ID = id;
            string RFC = txtRFC.Text;
            string Razon = txtRazonSocial.Text;
            string Comercial = txtNombreComercial.Text;
            string Giro = txtGiro.SelectedValue.ToString();
            string Tipo = cmbTipoComprobante.Text;
            string Calle = txtCalleReceptor.Text;
            string Exterior = txtNoExterior.Text;
            string Interior = txtNoInterior.Text;
            string Colonia = txtColonia.Text;
            int Pais = Convert.ToInt32(cmbPais.SelectedValue.ToString());
            int Estado = Convert.ToInt32(cmbEstado.SelectedValue.ToString());
            string Municipio = txtMunicipio.Text;
            string Poblacion = txtPoblacion.Text;
            string Codigo = txtCP.Text;
            string Casa = txtTelefono.Text;
            string Oficina = txtCelular.Text;
            string Email = txtEmail1.Text;
            string Contacto = txtContacto.Text;
            string IVA = cmbRetencionIVA.Text;
            string ISR = cmbRetencionISR.Text;
            string Web = txtWebSite.Text;
            int Addenda = cmbAddenda.SelectedIndex;

            BusClientes bus = new BusClientes();
            if (bus.EditarCliente(id,RFC.Trim(), Razon.Trim(), Comercial, Giro, Tipo, Calle, Exterior, Interior, Colonia, Pais, Estado,
                Municipio, Poblacion, Codigo, Casa, Oficina, Email, Contacto, IVA, ISR, Web, Addenda))
            {
                MessageBox.Show("El Cliente se Edito Correctamente", "Editado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ocurrio un Error durante la Edicion, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DialogResult = true;
        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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

                    txtColonialst.ItemsSource = entidad.Colonia.Where(a => a.str_descripcion.Contains(strCP + "-"));
                    txtColonialst.DisplayMemberPath = "str_descripcion";
                    txtColonialst.SelectedValuePath = "str_codigo";
                    txtColonialst.SelectedValue = col.id;


                }

            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error buscar Colonia " + ex );


            }
        }


       

        private void txtColonialst_DropDownClosed(object sender, EventArgs e)
        {
            string strColonia = "";



            if (txtColonialst.Text != "")
            {

                strColonia = txtColonialst.SelectedValue + "";
                txtColonia.Text = strColonia + "-" + txtColonialst.Text.Split('-')[1];
            }

        }

        private void cmbEstado_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                var idEstadoOr = cmbEstado.SelectedValue;

                string value = idEstadoOr.ToString();

                int value2 = int.Parse(value);

                var getValue = entidad.Estado.FirstOrDefault(edo => edo.intID == value2);

                string strEstadoOr = getValue.strNombreEstado.ToString().Split('-')[0];

                wpfEFac.Models.Localidad loc = new Models.Localidad();
                txtPoblacionlst.ItemsSource = entidad.Localidad.Where(a => a.str_descripcion.Contains(strEstadoOr + "-"));
                txtPoblacionlst.DisplayMemberPath = "str_descripcion";
                txtPoblacionlst.SelectedValuePath = "str_codigo";
                txtPoblacionlst.SelectedValue = loc.id;




                wpfEFac.Models.Municipio mun = new Models.Municipio();
                txtMunicipiolst.ItemsSource = entidad.Municipio.Where(a => a.str_descripcion.Contains(strEstadoOr + "-"));
                txtMunicipiolst.DisplayMemberPath = "str_descripcion";
                txtMunicipiolst.SelectedValuePath = "str_codigo";
                txtMunicipiolst.SelectedValue = mun.id;

            }
            catch (Exception ex)
            {

            }
        }

        private void txtMunicipiolst_DropDownClosed(object sender, EventArgs e)
        {
            string strMunicipio = "";

            if (txtMunicipiolst.Text != "")
            {
                strMunicipio = txtMunicipiolst.SelectedValue + "-" + txtMunicipiolst.Text.Split('-')[1];
                txtMunicipio.Text = strMunicipio;
            }
   
        }

        private void txtPoblacionlst_DropDownClosed(object sender, EventArgs e)
        {
            string strLocalidad = "";
            if (txtPoblacionlst.Text != "")
            {
                strLocalidad = txtPoblacionlst.SelectedValue + "-" + txtPoblacionlst.Text.Split('-')[1];
                txtPoblacion.Text = strLocalidad;
            }

        }
        
    }
}
