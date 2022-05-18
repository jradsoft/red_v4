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
using wpfEFac.Helpers;
using System.Text.RegularExpressions;


namespace wpfEFac.Views.PuntoVenta
{
    /// <summary>
    /// Interaction logic for PaymentFactura.xaml
    /// </summary>
    public partial class PaymentFactura : Window
    {
        private PreFacturaViewModel pfvm;
        private eFacDBEntities entidad;
        eFacDBEntities db = new eFacDBEntities();
        private decimal sumaTotal;
        Factura myF;
        public PaymentFactura()
        {


            InitializeComponent();
            entidad = new eFacDBEntities();
            pfvm = new PreFacturaViewModel();

            txtMonto.IsEnabled = false;

         
             this.dtpFechaPago.SelectedDate = DateTime.Now;

             wpfEFac.Models.Clientes c = new Models.Clientes();
             cmbClientes.ItemsSource = entidad.Clientes.OrderBy(x => x.strRazonSocial);
             cmbClientes.DisplayMemberPath = "strRazonSocial";
             cmbClientes.SelectedValuePath = "intID";
             cmbClientes.SelectedValue = c.intID;


             cmbRfcOrigen.Items.Clear();
             cmbRfcOrigen.ItemsSource = FactCat.getListBancos();
             cmbRfcOrigen.DisplayMemberPath = "descripcion";
             cmbRfcOrigen.SelectedValuePath = "clave";

             cmbRfcDestino.Items.Clear();
             cmbRfcDestino.ItemsSource = FactCat.getListBancos();
             cmbRfcDestino.DisplayMemberPath = "descripcion";
             cmbRfcDestino.SelectedValuePath = "clave";
            
        }

        void cmdAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                eFacDBEntities db = new eFacDBEntities();

                Factura factura = db.Factura.SingleOrDefault(fac => fac.intID == myF.intID);

                factura.strNumero = cmbStausPago.Text;
                DateTime FechaPago = dtpFechaPago.SelectedDate.Value;

                String txbFecha = FechaPago.ToString("yyyy-MM-dd");
          
                DateTime fechaAprobacion = DateTime.Parse(txbFecha);



                factura.strNumeroContrato = cmbFormaPago.Text;
                factura.dtmFechaEnvio = fechaAprobacion;

                db.SaveChanges();
                MessageBox.Show("La fecha de pago fue agregada con exito! ");

                DialogResult = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                DialogResult = false;
            }

        }

        void cmdSalir_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        void txtMonto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void txtSanterior_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void txtPagado_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                decimal dcmSanterior = decimal.Parse(txtSanterior.Text);
                decimal dcmPagado = decimal.Parse(txtPagado.Text);
                decimal totalInsoluto = dcmSanterior - dcmPagado;

                txtInsouto.Text = totalInsoluto.ToString("N2");
            }catch(Exception ex){}


        }

        void txtInsouto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void txtNuOperacion_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void txtParcialidad_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void btnTimbrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {

        }

        void btnEmail_Click(object sender, RoutedEventArgs e)
        {

        }

        void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }


        public class Item
        {
            public int intId_factura { get; set; }
            public string strFolio { get; set; }
            public string strSerie { get; set; }
            public string strUUID{ get; set; }
            public string dtmFecha { get; set; }
            public decimal dcmImporte { get; set; }
            public decimal dcmPagado { get; set; }
            public decimal dcmPendiente { get; set; }

        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {

          
            wpfEFac.Models.Factura f = new Models.Factura();

            var idFactura = cmbFacturas.SelectedValue;
            int facturaId = int.Parse(idFactura.ToString());


            var getFactura = entidad.Factura.Where(a=>a.intID==facturaId).FirstOrDefault();

            SumaTotal(decimal.Parse(txtPagado.Text));

            
           

            dtgFacturasPago.Items.Add(new Item()
            {
                
                intId_factura = facturaId,
                strSerie = getFactura.strSerie,
                strFolio = getFactura.strFolio ,
                strUUID=getFactura.strSelloDigital,
                dtmFecha = getFactura.dtmFecha.ToString(),
                dcmImporte = decimal.Parse(txtSanterior.Text),
                dcmPagado = decimal.Parse(txtPagado.Text),
                dcmPendiente= decimal.Parse(txtInsouto.Text)
            });

            txtSanterior.Text = "0.00";
            txtPagado.Text = "0.00";
            txtInsouto.Text = "0.00";
            txtMontoFactura.Text = "0.00";


        }


        void SumaTotal(decimal dcmImporte) {


            sumaTotal += dcmImporte;
            string currencyValue = sumaTotal.ToString("N2");
            txtMonto.Text = currencyValue;
        
        
        
        }


        void cmbFacturas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  var currentSelectedIndex = cmbFacturas.SelectedIndex;

            var idFactura = cmbFacturas.SelectedValue;
            int facturaId = 0;
            try
            {
                 facturaId = int.Parse(idFactura.ToString());
            }catch(Exception ex){
            
            }

            var getFactura = entidad.Factura.Where(a => a.intID == facturaId).FirstOrDefault();


            string idFacDet = getFactura.intID.ToString();

            List<Detalle_Factura> detfact = db.Detalle_Factura.Where(df => df.strConcepto.Contains(idFacDet)).ToList();
            int intPart = 1;
            foreach (var item in detfact)
            {
                intPart++;
               

            }

            decimal santerior = getFactura.dcmTotal - getFactura.dcmDescuento.Value;
            string strSaldoAnterior = santerior.ToString("N2");
            string strPagado = santerior.ToString("N2");
             string strInsoluto = decimal.Parse("0").ToString("N2");
            /*
             try
             {
                  intPart = int.Parse(getFactura.CondPago);
                  intPart++;
             }
             catch {

                 intPart = 1;
             
             }*/


             txtParcialidad.Text = "" + intPart;

             txtMontoFactura.Text = getFactura.dcmTotal.ToString("N2");
             txtSanterior.Text = strSaldoAnterior;
             txtPagado.Text = strPagado;
             txtInsouto.Text = strInsoluto;

        }

        void cmbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ClientID =cmbClientes.SelectedValue;
            int idCLiente = int.Parse(ClientID.ToString());
            wpfEFac.Models.Factura f = new Models.Factura();
            cmbFacturas.ItemsSource = entidad.Factura.Where(a => a.intID_Cliente == idCLiente && a.chrStatus == "A" && a.strNumero != "PAGADA");
            //Folios myFolio = f.Certificates.Folios.Where(p=>p.chrStatus == "A" && p.intIDtipoCFD == f.intID_Tipo_CFD).First();
            cmbFacturas.DisplayMemberPath = "strFolio";
            cmbFacturas.SelectedValuePath = "intID";
            cmbFacturas.SelectedValue = f.intID;
        }

        void dtgFacturasPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        void dtgFacturasPago_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }

        void dtgFacturasPago_Loaded(object sender, RoutedEventArgs e)
        {

        }

        void bttEliminarPrePago_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = dtgFacturasPago.SelectedItem;
                if (selectedItem != null)
                {
                    dtgFacturasPago.Items.Remove(selectedItem);

                    decimal sum = 0m;
                    for (int i = 0; i < dtgFacturasPago.Items.Count; i++)
                    {
                        sumaTotal = 0;
                        sum += (decimal.Parse((dtgFacturasPago.Columns[4].GetCellContent(dtgFacturasPago.Items[i]) as TextBlock).Text));
                    }

                    sumaTotal = sum;
                    txtMonto.Text = sum.ToString("N2");

                }
            }
            catch (Exception ex) { 
            }

           
        }

        void dtgFacturasPago_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }

        void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            DateTime FechaPago = dtpFechaPago.SelectedDate.Value;

            String txbFecha = FechaPago.ToString("yyyy-MM-dd");

            DateTime fechaPago = DateTime.Parse(txbFecha);

            CFDModels cfd = new CFDModels();
            Models.Empresa emisor;
            Models.Clientes receptor;
            Direcciones_Fiscales direccionEmisor;
            Direcciones_Fiscales direccionReceptor;
           GetEncabezadoCFD(out emisor, out receptor, out direccionEmisor, out direccionReceptor);
            dlleFac.ComprobanteFiscalDigital co = new dlleFac.ComprobanteFiscalDigital();
            List<dlleFac.Concepto> conceptosDll = new List<dlleFac.Concepto>();
            List<ConceptoPreFactura> conceptosPrefactura = new List<ConceptoPreFactura>();

            RetriveComprobanteFiscalDigital(6, cfd, emisor, receptor, direccionEmisor, direccionReceptor, co, conceptosDll, conceptosPrefactura);

           

            try{

                string rfcOrde = "";
                string rfcBen = "";
                if (cmbRfcOrigen.SelectedItem != null) { 
                
                    rfcOrde= cmbRfcOrigen.SelectedValue.ToString();
                }
                if (cmbRfcDestino.SelectedItem != null)
                {

                    rfcBen = cmbRfcDestino.SelectedValue.ToString();
                }

                
                



                        if (cfd.SaveFactura(6,
                            emisor.intID,
                            co.Serie,
                            co.Folio,
                            fechaPago,// DateTime.Parse(co.Fecha.Replace("T", " ")),
                            Convert.ToInt32(App.Current.Properties["idUsuario"]),
                            receptor.intID,
                           // co.FormaPago,
                            cmbFormaPago.Text.Substring(0, 2),
                           "",// txtEncabezado.Text,
                            decimal.Parse("0.000000"),
                            decimal.Parse("0.000000"),
                            decimal.Parse("0.000000"),
                            decimal.Parse(txtMonto.Text),
                            "",//txtObservaciones.Text,
                            "",//txtImpuestosAdicionales.Text,
                            "",//uuidSave,
                            "",//cmbTipoRelacion.Text.Substring(0,2),                            
                            emisor.CFD.First().intID,
                            "",
                            conceptosPrefactura,
                            decimal.Parse("0.000000"),
                            decimal.Parse("0.000000"),
                            decimal.Parse("0.000000"),
                            txtParcialidad.Text,//txtCondicionesPago.Text,
                            "PPD",//cmbMetodoPago.Text.Substring(0, 3),
                            "P01",//cmbUsoCfdi.Text.Substring(0, 3),
                            "XXX",//txtDivisa.Text,
                            decimal.Parse("0.000000"),
                            rfcOrde,//txtProveeedor.Text,
                            txtNuOperacion.Text,//txtPedido.Text,
                            rfcBen,//txtCC.Text,
                            "",//txtDestinatario.Text,
                            "",//txtRfcDestinatario.Text,
                            "",//txtDomicilioDestinatario.Text,
                            "",//txtEntregarEn.Text, 
                            null//myAddenda
                            ))

                        {


                           

                            foreach (var item in dtgFacturasPago.Items)
                            {
                               
                                Item concepto = item as Item;
                                Factura factura = db.Factura.Where(fac => fac.intID == concepto.intId_factura).First();
                                if (concepto.dcmPendiente > 0)
                                {
                                    factura.dcmDescuento = concepto.dcmPendiente;
                                    factura.strNumero = "PAGO PARCIAL";
                                    factura.dtmFechaEnvio = fechaPago;
                                    db.SaveChanges();
                                }
                                else {
                                    factura.dcmDescuento = decimal.Parse("0");
                                    factura.strNumero = "PAGADA";
                                    factura.dtmFechaEnvio = fechaPago;
                                    db.SaveChanges();
                                
                                }
                               
                            }


                            
                            MessageBox.Show("Guardado con exito", "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);

                            DialogResult = true;
                            
                        }
                        else
                        {
                            MessageBox.Show("Lo sentimos ocurrio un error intentelo de nuevo", "Guardar",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Lo sentimos ocurrio un error inesperado: " + error.Message, "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                    }

        }

        
        void GetEncabezadoCFD(out Models.Empresa emisor, out Models.Clientes receptor, out Direcciones_Fiscales direccionEmisor, out Direcciones_Fiscales direccionReceptor)
        {
            emisor = pfvm.GetEmpresa(Convert.ToInt32(App.Current.Properties["idUsuario"]));
            //  emisor = (wpfEFac.Models.Clientes)atcNombreCliente1.SelectedItem;
            receptor = (wpfEFac.Models.Clientes)cmbClientes.SelectedItem;
            direccionEmisor = pfvm.GetDireccionEmpresa(emisor.intID);
            direccionReceptor = pfvm.GetDireccionCliente(receptor.intID);
        }

        void RetriveComprobanteFiscalDigital(int intTipoComprobante, CFDModels cfd, Models.Empresa emisor, Models.Clientes receptor, Direcciones_Fiscales direccionEmisor, Direcciones_Fiscales direccionReceptor, dlleFac.ComprobanteFiscalDigital co, List<dlleFac.Concepto> conceptos, List<ConceptoPreFactura> conceptoPrefactura)
        {
            GetConceptosFromGrid(conceptos, conceptoPrefactura);

            //dlleFac.Impuestos impuestos;
            dlleFac.Emisor emisorCFD;
            dlleFac.DomicilioFiscal domicilioEmisor;
            dlleFac.DomicilioFiscal expedidoEn;
            dlleFac.Receptor receptorCFD;
            dlleFac.DomicilioFiscal domicilioFiscalReceptor;

            GetValuesCFD(cfd, emisor, receptor, direccionEmisor, direccionReceptor, co, conceptos, out emisorCFD, out domicilioEmisor, out expedidoEn, out receptorCFD, out domicilioFiscalReceptor);

            
            //string emisorNumeroAprovacion = emisor.CFD.SingleOrDefault(f => f.intID == intTipoComprobante).Folios.intNumero_Aprovacion.ToString();
            
            //string emisorAñoAprobacion = emisor.CFD.SingleOrDefault(f => f.intID == intTipoComprobante).Folios.strAño_Aprovacion.ToString();


           

            Folios fol = db.Folios.Where(p => p.intID == 6).First();

         
             

            cfd.FillComprobanteFiscal(
                co,
                "3.0",
                fol.strSerie,
                fol.intFolioActual.ToString(),
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                "",//emisorNumeroAprovacion,
                "",//emisorAñoAprobacion, // No HardCode
                emisor.CFD.First().strTipoCFD,
                cmbFormaPago.Text,
                decimal.Parse("0.00").ToString(),
                decimal.Parse("0.00").ToString(),
                co.Total = decimal.Parse(txtMonto.Text).ToString(),
                emisorCFD,
                domicilioEmisor,
                expedidoEn,
                receptorCFD,
                domicilioFiscalReceptor,
                conceptos

                );
        }



        void GetValuesCFD(CFDModels cfd, Models.Empresa emisor, Models.Clientes receptor, Direcciones_Fiscales direccionEmisor,
           Direcciones_Fiscales direccionReceptor, dlleFac.ComprobanteFiscalDigital co, List<dlleFac.Concepto> conceptos,
           out dlleFac.Emisor emisorCFD, out dlleFac.DomicilioFiscal domicilioEmisor,
           out dlleFac.DomicilioFiscal expedidoEn, out dlleFac.Receptor receptorCFD, out dlleFac.DomicilioFiscal domicilioFiscalReceptor)
        {
       
            emisorCFD = new dlleFac.Emisor() { RFCEmisor = emisor.strRFC, NombreEmisor = emisor.strRazonSocial };

            domicilioEmisor = new dlleFac.DomicilioFiscal()
            {
                Calle = direccionEmisor.strCalle,
                NumeroExterior = direccionEmisor.strNoExterior,
                Colonia = direccionEmisor.strColonia,
                Localidad = direccionEmisor.strPoblacionLocalidad,
                Referencia = null,
                Municipio = direccionEmisor.strMunicipio,
                Estado = direccionEmisor.Estado.strNombreEstado,
                Pais = direccionEmisor.Paises.strNombrePais,
                CodigoPostal = direccionEmisor.strCodigoPostal
            };

            expedidoEn = new dlleFac.DomicilioFiscal()
            {
                Calle = direccionEmisor.strCalle,
                NumeroExterior = direccionEmisor.strNoExterior,
                Colonia = direccionEmisor.strColonia,
                Localidad = direccionEmisor.strPoblacionLocalidad,
                Referencia = null,
                Municipio = direccionEmisor.strMunicipio,
                Estado = direccionEmisor.Estado.strNombreEstado,
                Pais = direccionEmisor.Paises.strNombrePais,
                CodigoPostal = direccionEmisor.strCodigoPostal
            };

            receptorCFD = new dlleFac.Receptor()
            {
                RFCReceptor = receptor.strRFC,
                NombreReceptor = receptor.strRazonSocial
            };

            domicilioFiscalReceptor = new dlleFac.DomicilioFiscal()
            {
                Calle = direccionReceptor.strCalle,
                NumeroExterior = direccionReceptor.strNoExterior,
                Colonia = direccionReceptor.strColonia,
                Localidad = direccionReceptor.strPoblacionLocalidad,
                Referencia = null,
                Municipio = direccionReceptor.strMunicipio,
                Estado = direccionReceptor.Estado.strNombreEstado,
                Pais = direccionReceptor.Paises.strNombrePais,
                CodigoPostal = direccionReceptor.strCodigoPostal
            };

        
        }


         void GetConceptosFromGrid(List<dlleFac.Concepto> conceptos, List<ConceptoPreFactura> conceptosPrefactura)
        {
            foreach (var item in dtgFacturasPago.Items)
            {
                Item concepto = item as Item;
                
              
                ConceptoPreFactura conceptoPrefactura =
                                new ConceptoPreFactura();

                conceptoPrefactura.intIdProducto = 1500; //concepto.intID;
                conceptoPrefactura.intCantidad = 1;
                conceptoPrefactura.dcmDescuento = concepto.dcmPendiente;
                conceptoPrefactura.dcmImporte = concepto.dcmPagado;
                conceptoPrefactura.strUnidad = "ACT-Actividad";
                conceptoPrefactura.strConcepto = concepto.intId_factura.ToString();
                conceptoPrefactura.dcmPrecioUnitario = concepto.dcmImporte;
                conceptoPrefactura.dcmIVA = decimal.Parse("0.00");
                conceptoPrefactura.dcmRetIVA = decimal.Parse("0.00");
                conceptoPrefactura.dcmRetISR = decimal.Parse("0.00");
                conceptoPrefactura.dcmRetIEPS = decimal.Parse("0.00");

                conceptoPrefactura.strPartida = concepto.strUUID+"|"+concepto.strFolio+"|"+concepto.strSerie;

                conceptosPrefactura.Add(conceptoPrefactura);
            }
        }

         void txtSanterior_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        void txtSanterior_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtSanterior.Text)){
                txtSanterior.Text = "0.00";
            }

        }

        void txtPagado_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            if (regex.IsMatch(e.Text) && !(e.Text == "." && ((TextBox)sender).Text.Contains(e.Text)))
                e.Handled = false;

            else
                e.Handled = true;
           
        }

        void txtPagado_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPagado.Text))
            {
                txtPagado.Text = "0.00";
            }
        }

        void txtInsouto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        void txtInsouto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInsouto.Text))
            {
                txtInsouto.Text = "0.00";
            }

        }
    }
}
