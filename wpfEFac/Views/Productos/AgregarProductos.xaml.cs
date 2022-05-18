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

namespace wpfEFac.Views.Productos
{    
    /// <summary>
    /// Interaction logic for AgregarProductos.xaml
    /// </summary>
    public partial class AgregarProductos : Window
    {
        private eFacDBEntities entidad;
        private EditarProductosViewModel epvm;

        public AgregarProductos()
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
            epvm = new EditarProductosViewModel();

            txtPrecio1.Text = "0.0";
            txtPrecio2.Text = "0.0";
            txtPrecio3.Text = "0.0";
            txtPrecio4.Text = "0.0";
            txtPrecio5.Text = "0.0";

            cmbIVA.Text = "S";
            txtIva.Text = "0.16";
            
            cmbretIva.Text = "N";
            txtretIva.Text = "0.0";

            cmbretISR.Text = "N";
            txtretIsr.Text = "0.0";

            cmbIeps.Text = "N";
            txtretIeps.Text = "0.0";
            
            txtExistencias.Text = "0.0";
            txtStockMin.Text = "0.0";
            txtStockMax.Text = "0.0";
            txtPuntoReorden.Text = "0.0";
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValoresValidos())
            {
                string Codigo = txtCodigo.Text;
                string CodigoBarras = txtCodigoBarras.Text;
                string Nombre = txtNombre.Text;
                string NombreCorto = txtNombreCorto.Text;
                string Descripcion = txtDescripcion.Text;
                decimal Descuento = Convert.ToDecimal(txtDescuento.Text);

                decimal Precio1 = Convert.ToDecimal(txtPrecio1.Text);
                decimal Precio2 = Convert.ToDecimal(txtPrecio2.Text);
                decimal Precio3 = Convert.ToDecimal(txtPrecio3.Text);
                decimal Precio4 = Convert.ToDecimal(txtPrecio4.Text);
                decimal Precio5 = Convert.ToDecimal(txtPrecio5.Text);


                /*
                 * 
                 */

                int Categoria = Convert.ToInt32(cmbCategoria.SelectedValue.ToString());
                int Unidad = Convert.ToInt32(cmbUnidad.SelectedValue.ToString());

                string IVA = cmbIVA.Text;
                decimal porcIVA = Convert.ToDecimal(txtIva.Text);

                string retIVA = cmbretIva.Text;
                decimal porcRetIVA = Convert.ToDecimal(txtretIva.Text);
                
                string retISR = cmbretISR.Text;
                decimal porcRetISR = Convert.ToDecimal(txtretIsr.Text);
                
                string retIeps = cmbIeps.Text;
                decimal porcRetIeps = Convert.ToDecimal(txtretIeps.Text);

                decimal existencias = Convert.ToDecimal(txtExistencias.Text);
                decimal stockMin = Convert.ToDecimal(txtStockMin.Text);
                decimal stockMax = Convert.ToDecimal(txtStockMax.Text);
                decimal puntoReorden = Convert.ToDecimal(txtPuntoReorden.Text);

                BusClientes bus = new BusClientes();
                if (bus.AgregarProducto(/*ID,*/
                    
                 Codigo,
                 CodigoBarras,
                 Nombre,
                 NombreCorto,
                 Descripcion,
                 Descuento,

                 Precio1,
                 Precio2,
                 Precio3,
                 Precio4,
                 Precio5,

                 Categoria,
                 Unidad,

                 IVA,
                 porcIVA,

                 retIVA,
                 porcRetIVA,
                
                 retISR,
                 porcRetISR,
                
                 retIeps,
                 porcRetIeps,

                 existencias,
                 stockMin,
                 stockMax,
                 puntoReorden,
                 Convert.ToInt32(App.Current.Properties["idEmpresa"])
                 )
              )
                {
                    MessageBox.Show("\"El producto " + Nombre + " se guardo correctamente\"", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("\"Error al intentar guardar, vuela a registrar su producto\"", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Existen errores de validacion por favor corrigalos");
            }
        }

        private bool ValoresValidos()
        {
            return ValidarNombreProducto() & ValidarPrecioProducto() & ValidarCodigoProducto();
        }

        private bool ValidarNombreProducto()
        {
            txtNombre.BorderBrush = new TextBox().BorderBrush;
            lblNombreRequerido.Visibility = System.Windows.Visibility.Collapsed;
            bool nombreValido = !string.IsNullOrWhiteSpace(txtNombre.Text);

            if (!nombreValido)
            {
                lblNombreRequerido.Visibility = System.Windows.Visibility.Visible;
                lblNombreRequerido.Content = "*Campo requerido, debes poner el Nombre de tu Producto";
                lblNombreRequerido.Foreground = new SolidColorBrush(Colors.Red);
                txtNombre.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            return nombreValido;
        }

        private bool ValidarCodigoProducto()
        {
            txtCodigo.BorderBrush = new TextBox().BorderBrush;
            lblCodigoRequerido.Visibility = System.Windows.Visibility.Collapsed;
            bool codigoValido = !string.IsNullOrWhiteSpace(txtCodigo.Text);

            if (!codigoValido)
            {
                lblCodigoRequerido.Visibility = System.Windows.Visibility.Visible;
                lblCodigoRequerido.Content = "*Campo requerido, debes poner el Codigo de tu Producto";
                lblCodigoRequerido.Foreground = new SolidColorBrush(Colors.Red);
                txtCodigo.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            return codigoValido;
        }

        private bool ValidarPrecioProducto()
        {
            txtPrecio1.BorderBrush = new TextBox().BorderBrush;
            lblPrecioRequerido.Visibility = System.Windows.Visibility.Collapsed;
            bool precioValido = !string.IsNullOrWhiteSpace(txtPrecio1.Text) & wpfEFac.Helpers.Validador.TryParseTextBoxToDecimal(txtPrecio1.Text);

            if (!precioValido)
            {
                lblPrecioRequerido.Visibility = System.Windows.Visibility.Visible;
                lblPrecioRequerido.Content = "*La cantidad no es decimal, por favor vuelva a introducirla";
                lblPrecioRequerido.Foreground = new SolidColorBrush(Colors.Red);
                txtPrecio1.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            return precioValido;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //cmbIva.SelectedIndex = 0;            
            txtDescuento.Text = "0";
            LoadCategoria();
        }

        private void LoadCategoria()
        {
            cmbCategoria.ItemsSource = epvm.GetCategoria();
            cmbCategoria.DisplayMemberPath = "strNombre";
            cmbCategoria.SelectedValuePath = "intID";
            cmbCategoria.SelectedIndex = 0;
            cmbUnidad.ItemsSource = epvm.GetUnidad();
            cmbUnidad.DisplayMemberPath = "strDescripcion";
            cmbUnidad.SelectedValuePath = "intId";
            cmbUnidad.SelectedIndex = 0;
        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnNuevaCategoria_Click(object sender, RoutedEventArgs e)
        {
            CategoriaWindow cw = new CategoriaWindow();
            cw.Closed += (s, args) =>
            {
                if (cw.DialogResult.Value) 
                {
                    LoadCategoria();
                }
            };
            cw.ShowDialog();
        }

        private void btnNuevaUnidad_Click(object sender, RoutedEventArgs e)
        {
            UnidadMedidaWindow uw = new UnidadMedidaWindow();
            uw.Closed += (s, args) =>
            {
                if (uw.DialogResult.Value)
                {
                    LoadCategoria();
                }
            };
            uw.ShowDialog();
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarNombreProducto();
        }

        private void txtCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarCodigoProducto();
        }

        private void txtPrecio_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarPrecioProducto();
        }
    }
}