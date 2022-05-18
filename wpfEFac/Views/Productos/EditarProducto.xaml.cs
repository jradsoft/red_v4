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
    /// Interaction logic for EditarProducto.xaml
    /// </summary>
    public partial class EditarProducto : Window
    {

        private eFacDBEntities entidad;
        EditarProductosViewModel epvm;
        private int id;

        public EditarProducto(int id)
        {
            InitializeComponent();
            entidad = new eFacDBEntities();
            epvm = new EditarProductosViewModel();
            this.id = id;
            this.Loaded += EditarProducto_Loaded;  
        }

        protected void EditarProducto_Loaded(object sender, RoutedEventArgs e)
        {
            var pro = epvm.GetProductos(id);

            
            txtCodigo.Text = pro.strCodigo;
            txtCodigoBarras.Text = pro.strCodigoBarras;
            txtNombre.Text = pro.strNombre;
            txtNombreCorto.Text = pro.strNombreCorto;
            txtDescripcion.Text = pro.strDescripcion;
            txtDescuento.Text = pro.dcmDescuent.ToString();

            txtPrecio1.Text = pro.dcmPrecio1.ToString();
            txtPrecio2.Text = pro.dcmPrecio2.ToString();
            txtPrecio3.Text = pro.dcmPrecio3.ToString();
            txtPrecio4.Text = pro.dcmPrecio4.ToString();
            txtPrecio5.Text = pro.dcmPrecio5.ToString();

            /*
             * 
             */
            
            cmbCategoria.ItemsSource = epvm.GetCategorias();
            cmbCategoria.DisplayMemberPath = "strNombre";
            cmbCategoria.SelectedValuePath = "intID";
            
            cmbCategoria.SelectedValue = pro.Categorias.intID;
            cmbUnidad.ItemsSource = epvm.GetUnidad();
            cmbUnidad.DisplayMemberPath = "strDescripcion";
            cmbUnidad.SelectedValuePath = "intId";
            cmbUnidad.SelectedValue = pro.UnidadMedida.intId;
            

            cmbIVA.Text = pro.gravaIva;
            txtIva.Text = pro.porcIva.ToString();

             cmbretIva.Text = pro.gravaRetIva;
            txtretIva.Text = pro.porcRetIva.ToString();

            cmbretISR.Text = pro.gravaRetIsr;
            txtretIsr.Text = pro.porcRetIsr.ToString();

            cmbIeps.Text = pro.gravaIeps;
            txtretIeps.Text = pro.porcIeps.ToString();

            txtExistencias.Text = pro.existencias.ToString();
            txtStockMin.Text = pro.stockMin.ToString();
            txtStockMax.Text = pro.stockMax.ToString();
            txtPuntoReorden.Text = pro.puntoReorden.ToString();

           
            
            
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            int ID = id;


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
            if (bus.EditarProducto(

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
                 puntoReorden
                 
                
                ))
            {
                MessageBox.Show("El Producto se ha Editado Corrrectamente", "Editado", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Ocurrio un Error durante la Edicion, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void LoadCategoria()
        {
            cmbCategoria.ItemsSource = epvm.GetCategoria();
            cmbCategoria.DisplayMemberPath = "strNombre";
            cmbCategoria.SelectedValuePath = "strID";
            cmbCategoria.SelectedIndex = 0;
            cmbUnidad.ItemsSource = epvm.GetUnidad();
            cmbUnidad.DisplayMemberPath = "strDescripcion";
            cmbUnidad.SelectedValuePath = "strId";
            cmbUnidad.SelectedIndex = 0;
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
    }
}
