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
using System.Globalization;
using Newtonsoft.Json;


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
        string txbFecha = "";
        string rfcOrde = "";
        string rfcBen = "";
        private string jsonGrid = "";
        Factura myFact;

        public PaymentFactura(Factura f)
        {
            myFact = f;

            InitializeComponent();
            entidad = new eFacDBEntities();
            pfvm = new PreFacturaViewModel();

          //  txtMonto.IsEnabled = false;

         
             this.dtpFechaPago.SelectedDate = DateTime.Now;

             wpfEFac.Models.Clientes c = new Models.Clientes();
             cmbClientes.ItemsSource = entidad.Clientes.OrderBy(x => x.strNombreComercial);
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

             if (f != null) {

                 try
                 {

                     wpfEFac.Models.Clientes edtCli = new Models.Clientes();
                     cmbClientes.ItemsSource = entidad.Clientes.Where(e=> e.intID == f.intID_Cliente);
                     cmbClientes.DisplayMemberPath = "strRazonSocial";
                     cmbClientes.SelectedValuePath = "intID";
                     cmbClientes.SelectedValue = edtCli.intID;


                     DataCompPago.DataComplementoPago dataComPago = JsonConvert.DeserializeObject<DataCompPago.DataComplementoPago>(f.strCadenaOriginal);

                     //cmbClientes.Visibility = System.Windows.Visibility.Hidden;
                     dtpFechaPago.Text = dataComPago.dtmFechaPago;
                     txtMonto.Text = dataComPago.dcmTotal.ToString();
                     cmbRfcOrigen.SelectedValue = dataComPago.rfcCtaOrdenante;
                     cmbRfcDestino.SelectedValue = dataComPago.rfcCtaBeneficiario;
                     txtNuOperacion.Text = dataComPago.strNumOperacion;


                     wpfEFac.Models.Factura fy = new Models.Factura();
                     cmbFacturas.ItemsSource = entidad.Factura.Where(a => a.intID_Cliente == f.intID_Cliente && a.intID_Tipo_CFD != 6 && a.chrStatus == "A" && a.strNumero != "PAGADA");
                     //Folios myFolio = f.Certificates.Folios.Where(p=>p.chrStatus == "A" && p.intIDtipoCFD == f.intID_Tipo_CFD).First();
                     cmbFacturas.DisplayMemberPath = "strFolio";
                     cmbFacturas.SelectedValuePath = "intID";
                     cmbFacturas.SelectedValue = fy.intID;


                     foreach (var i in dataComPago.fillData)
                     {

                         dtgFacturasPago.Items.Add(new Item()
                         {

                             intId_factura = i.intId_factura,
                             strFolio = i.strFolio,
                             strSerie = i.strSerie,
                             strUUID = i.strUUID,
                             dtmFecha = i.dtmFecha,
                             strFormaPago = i.strFormaPago,
                             dcmImporte = i.dcmImporte,
                             dcmPagado = i.dcmPagado,
                             dcmPendiente = i.dcmPendiente,
                             dcmMontoFact = i.dcmMontoFact



                         });


                     }

                 }
                 catch (Exception ex) {

                     MessageBox.Show("error al recuperar datos de complemento " + ex);
                 }
                 



             }
            
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
            public string strUUID { get; set; }
            public string dtmFecha { get; set; }
            public string strFormaPago { get; set; }
            public decimal dcmImporte { get; set; }
            public decimal dcmPagado { get; set; }
            public decimal dcmPendiente { get; set; }
            public decimal dcmMontoFact { get; set; }
            public string strMoneda { get; set; }
            

        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                

                var idFactura = cmbFacturas.SelectedValue;
                int facturaId = int.Parse(idFactura.ToString());


                var getFactura = entidad.Factura.Where(a => a.intID == facturaId).FirstOrDefault();


                

                            SumaTotal(decimal.Parse(txtPagado.Text));
                        
                        
                
                        
                    
                



                dtgFacturasPago.Items.Add(new Item()
                {

                    intId_factura = facturaId,
                    strSerie = getFactura.strSerie,
                    strFolio = getFactura.strFolio,
                    strUUID = getFactura.strSelloDigital,
                    dtmFecha = getFactura.dtmFecha.ToString(),
                    strFormaPago = cmbFormaPago.Text,
                    dcmImporte = decimal.Parse(txtSanterior.Text),
                    dcmPagado = decimal.Parse(txtPagado.Text),
                    dcmPendiente = decimal.Parse(txtInsouto.Text),
                    dcmMontoFact = decimal.Parse(txtMontoFactura.Text),
                    strMoneda = getFactura.Divisa
                });

                txtSanterior.Text = "0.00";
                txtPagado.Text = "0.00";
                txtInsouto.Text = "0.00";
                txtMontoFactura.Text = "0.00";

            }
            catch (Exception ex) {

                MessageBox.Show("Seleccione facturas...");
            
            }
            


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

            List<Detalle_Factura> detfact = db.Detalle_Factura.Where(df => df.strConcepto.Contains(idFacDet) && df.intID_Producto==1500).ToList();
            int intPart = 1;
            foreach (var item in detfact)
            {
                intPart++;
               

            }
            
            decimal saldoPendiente=0;
            if (getFactura.dcmDescuento > 0) {

                saldoPendiente = getFactura.dcmDescuento.Value;
            }
            else {
                saldoPendiente = getFactura.dcmTotal;
            }
            

            decimal santerior = saldoPendiente;
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

            if (myFact != null) {

                idCLiente = myFact.intID_Cliente;
            
            }

            wpfEFac.Models.Factura f = new Models.Factura();
            cmbFacturas.ItemsSource = entidad.Factura.Where(a => a.intID_Cliente == idCLiente && a.intID_Tipo_CFD != 6  && a.chrStatus == "A" && a.strNumero != "PAGADA");
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
                    var id = (Item)selectedItem;

                    Factura updateStatus = db.Factura.FirstOrDefault(fd => fd.intID == id.intId_factura);  // busca facturas de complemento
                    updateStatus.strNumero = "";                                                // edita PAGADO O PARCIAL
                    updateStatus.dtmFechaEnvio = null;                                          // elimina fecha de pago
                    db.SaveChanges();


                    wpfEFac.Models.Factura f = new Models.Factura();
                    cmbFacturas.ItemsSource = entidad.Factura.Where(a => a.intID_Cliente == myFact.intID_Cliente && a.intID_Tipo_CFD != 6 && a.chrStatus == "A" && a.strNumero != "PAGADA");
                    //Folios myFolio = f.Certificates.Folios.Where(p=>p.chrStatus == "A" && p.intIDtipoCFD == f.intID_Tipo_CFD).First();
                    cmbFacturas.DisplayMemberPath = "strFolio";
                    cmbFacturas.SelectedValuePath = "intID";
                    cmbFacturas.SelectedValue = f.intID;
                    
                  
                   
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




            try
            {

                if (myFact != null) {
                    if (cmbClientes.SelectedItem== null) {
                        throw new Exception("Seleccione Cliente.");
                    
                    }
                
                }


            DateTime FechaPago = dtpFechaPago.SelectedDate.Value;

            int intHR = int.Parse(txthr.Text);
            int intMin = int.Parse(txtmin.Text);
            string HoraMin = " " + txthr.Text.ToString().PadLeft(2, '0') + ":" + txtmin.Text.ToString().PadLeft(2, '0');
            
            txbFecha = FechaPago.ToString("yyyy-MM-dd") + HoraMin;

            if (cmbRfcOrigen.SelectedItem != null)
            {

                rfcOrde = cmbRfcOrigen.SelectedValue.ToString();
            }
            if (cmbRfcDestino.SelectedItem != null)
            {

                rfcBen = cmbRfcDestino.SelectedValue.ToString();
            }



             fillJson();


             DateTime dt = DateTime.ParseExact(txbFecha, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.CurrentCulture);

            DateTime fechaPago = dt;

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

            string isFactoraje = "";

            if (chbFactoraje.IsChecked == true) {
                isFactoraje = "FACTORAJE";
            
            }
            if (myFact == null)
            {

                if (cfd.SaveFactura(6,
                    emisor.intID,
                    co.Serie,
                    co.Folio,
                    fechaPago,// DateTime.Parse(co.Fecha.Replace("T", " ")),
                    Convert.ToInt32(App.Current.Properties["idUsuario"]),
                    receptor.intID,
                    // co.FormaPago,
                    cmbFormaPago.Text.Substring(0, 2),
                    isFactoraje,// txtEncabezado.Text,
                    decimal.Parse("0.000000"),
                    decimal.Parse("0.000000"),
                    decimal.Parse("0.000000"),
                    decimal.Parse(txtMonto.Text),
                    "",//txtObservaciones.Text,
                    "",//txtImpuestosAdicionales.Text,
                    "",//uuidSave,
                    "",//cmbTipoRelacion.Text.Substring(0,2),                            
                    emisor.CFD.First().intID,
                    jsonGrid,
                    conceptosPrefactura,
                    decimal.Parse("0.000000"),
                    decimal.Parse("0.000000"),
                    decimal.Parse("0.000000"),
                    txtParcialidad.Text,//txtCondicionesPago.Text,
                    "PPD",//cmbMetodoPago.Text.Substring(0, 3),
                    "CP01",//cmbUsoCfdi.Text.Substring(0, 3),
                    "XXX",//txtDivisa.Text,
                    decimal.Parse("0.000000"),
                    rfcOrde,//txtProveeedor.Text,
                    txtNuOperacion.Text,//txtPedido.Text,
                    rfcBen,//txtCC.Text,
                    "",//txtDestinatario.Text,
                    "",//txtRfcDestinatario.Text,
                    "",//txtDomicilioDestinatario.Text,
                    "",//txtEntregarEn.Text, 
                    null,
                    ""
                    //myAddenda
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
                        else
                        {
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
            else {

                

                if (cfd.UpdateFactura(myFact.intID, 6,
                          emisor.intID,
                          co.Serie,
                          co.Folio,
                          fechaPago,// DateTime.Parse(co.Fecha.Replace("T", " ")),
                          Convert.ToInt32(App.Current.Properties["idUsuario"]),
                          receptor.intID,
                                    // co.FormaPago,
                          cmbFormaPago.Text.Substring(0, 2),
                          isFactoraje,// txtEncabezado.Text,
                          decimal.Parse("0.000000"),
                          decimal.Parse("0.000000"),
                          decimal.Parse("0.000000"),
                          decimal.Parse(txtMonto.Text),
                          "",//txtObservaciones.Text,
                          "",//txtImpuestosAdicionales.Text,
                          "",//uuidSave,
                          "",//cmbTipoRelacion.Text.Substring(0,2),                            
                          emisor.CFD.First().intID,
                          jsonGrid,
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
                          null
                         
                                    
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
                        else
                        {
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
            receptor = (wpfEFac.Models.Clientes)cmbClientes.SelectedItem;

            direccionReceptor = pfvm.GetDireccionCliente(receptor.intID);
            direccionEmisor = pfvm.GetDireccionEmpresa(emisor.intID);
           
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
                conceptoPrefactura.strPartida = concepto.strUUID + "|" + concepto.strFolio + "|" + concepto.strSerie + "|" + concepto.strFormaPago + "|" + concepto.dcmMontoFact + "|" + cmbMoneda.Text + "|" + txtTipoCambio.Text;
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

        private void txthr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

           
            
        }

       

        private void txtmin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

        }

        private static readonly Regex _regex = new Regex("[^0-9]+");

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void chbTodas_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void chbTodas_Checked(object sender, RoutedEventArgs e)
        {
            wpfEFac.Models.Factura f = new Models.Factura();
            cmbFacturas.ItemsSource = entidad.Factura.Where(a => a.chrStatus == "A");
            //Folios myFolio = f.Certificates.Folios.Where(p=>p.chrStatus == "A" && p.intIDtipoCFD == f.intID_Tipo_CFD).First();
            cmbFacturas.DisplayMemberPath = "strFolio";
            cmbFacturas.SelectedValuePath = "intID";
            cmbFacturas.SelectedValue = f.intID;
        }

        private void fillJson() {
             
            try
            {

                DataCompPago.DataComplementoPago myDatagrid = new DataCompPago.DataComplementoPago();
                List<DataCompPago.fillDataCompPago> fillData = new List<DataCompPago.fillDataCompPago>();
                List<DataCompPago.DataCompPagoImpuestosDR> fillDataImpDR = null;
                List<DataCompPago.DataCompPagoImpuestosP> fillDataImpP = new List<DataCompPago.DataCompPagoImpuestosP>();

                List<ImpuestosVarios> lstImpVarios = null;

                 decimal dcmTotalTraslado = 0;
                 decimal dcmTrasladoBase = 0;
                 decimal dcmTotalRetIva = 0;
                 decimal dcmTotalRetIsr = 0;
                 decimal dcmTotalRetIeps = 0;
                 decimal dcmSaldoInsoluto = 0;
                 decimal dcmPagado = 0;


                foreach (var item in dtgFacturasPago.Items) {



                    fillDataImpDR = new List<DataCompPago.DataCompPagoImpuestosDR>();
                    
                     Item concepto = item as Item;
                     lstImpVarios = new List<ImpuestosVarios>();


                     dcmSaldoInsoluto = concepto.dcmPendiente;
                     dcmPagado = concepto.dcmPagado;

                     var detallFactura = entidad.Detalle_Factura.Where(d=> d.intID_Factura== concepto.intId_factura);

                    
                  
                    foreach(var it in detallFactura){

                        decimal dcmBaseDRtotal = 0;
                        decimal dcmImporteDRtotal = 0;

                        var valueIva = lstImpVarios.Find(i => i.porcImp == it.dcmIVA);
                        var valueRetIva = lstImpVarios.Find(i => i.porcImp == it.retIVA);
                        var valueRetIsr = lstImpVarios.Find(i => i.porcImp == it.retISR);
                        var valueRetIeps = lstImpVarios.Find(i => i.porcImp == it.retIEPS);


                        
                       if (it.dcmIVA == decimal.Parse("0.16")) {

                           dcmTotalTraslado += it.dcmImporte * it.dcmIVA.Value;
                           dcmTrasladoBase += it.dcmImporte;

                           try
                           {

                               if (valueIva.porcImp == it.dcmIVA)
                               {
                                   valueIva.sumaBaseImp = it.dcmImporte + valueIva.sumaBaseImp;
                                   valueIva.sumaImporteImp = it.dcmImporte * it.dcmIVA.Value + valueIva.sumaImporteImp;


                               }

                           }
                           catch (Exception e)
                           {

                               lstImpVarios.Add(new ImpuestosVarios()
                               {
                                   porcImp = it.dcmIVA.Value,
                                   TypeIva = true,
                                   TypeRetIva = false,
                                   TypeRetIsr = false,
                                   TypeRetIeps = false,
                                   sumaBaseImp = it.dcmImporte,
                                   sumaImporteImp = it.dcmImporte * it.dcmIVA.Value


                               });
                           }


                        }

                       if (it.retIVA>0)
                       {

                           
                           dcmTotalRetIva += it.dcmImporte * it.retIVA.Value;

                           try {

                               if (valueRetIva.porcImp == it.retIVA)
                               {
                                   valueRetIva.sumaBaseImp = it.dcmImporte + valueRetIva.sumaBaseImp;
                                   valueRetIva.sumaImporteImp = it.dcmImporte * it.retIVA.Value + valueRetIva.sumaImporteImp;


                               }
                           
                           }catch(Exception e){

                               lstImpVarios.Add(new ImpuestosVarios()
                               {
                                   porcImp = it.retIVA.Value,
                                   TypeIva = false,
                                   TypeRetIva=true,
                                   TypeRetIsr = false,
                                   TypeRetIeps= false,
                                   sumaBaseImp =  it.dcmImporte,
                                   sumaImporteImp = it.dcmImporte * it.retIVA.Value


                               });
                           }
                           
                       }

                       if (it.retISR > 0)
                       {
                           dcmTotalRetIsr += it.dcmImporte * it.retISR.Value;

                           try
                           {

                               if (valueRetIsr.porcImp == it.retISR)
                               {
                                   valueRetIsr.sumaBaseImp = it.dcmImporte + valueRetIsr.sumaBaseImp;
                                   valueRetIsr.sumaImporteImp = it.dcmImporte * it.retISR.Value + valueRetIsr.sumaImporteImp;


                               }

                           }
                           catch (Exception e)
                           {

                               lstImpVarios.Add(new ImpuestosVarios()
                               {
                                   porcImp = it.retISR.Value,
                                   TypeIva = false,
                                   TypeRetIva = false,
                                   TypeRetIsr = true,
                                   TypeRetIeps = false,
                                   sumaBaseImp = it.dcmImporte,
                                   sumaImporteImp = it.dcmImporte * it.retISR.Value


                               });
                           }

                           

                       }
                       if (it.retIEPS > 0)
                       {
                           dcmTotalRetIeps += it.dcmImporte * it.retIEPS.Value;

                           try
                           {

                               if (valueRetIeps.porcImp == it.retIEPS)
                               {
                                   valueRetIeps.sumaBaseImp = it.dcmImporte + valueRetIeps.sumaBaseImp;
                                   valueRetIeps.sumaImporteImp = it.dcmImporte * it.retIEPS.Value + valueRetIeps.sumaImporteImp;
                               }

                           }
                           catch (Exception e)
                           {

                               lstImpVarios.Add(new ImpuestosVarios()
                               {
                                   porcImp = it.retIEPS.Value,
                                   TypeIva = false,
                                   TypeRetIva = false,
                                   TypeRetIsr = false,
                                   TypeRetIeps = true,
                                   sumaBaseImp = it.dcmImporte,
                                   sumaImporteImp = it.dcmImporte * it.retIEPS.Value


                               });
                           }

                       }





                     


                        //if (valueIVA == null && it.dcmIVA > 0)
                        //{
                           

                        //    dcmBaseDRtotal = 0;
                        //    dcmImporteDRtotal = 0;
                          
                        //    dcmBaseDRtotal = it.dcmImporte;
                        //    dcmImporteDRtotal = it.dcmImporte * it.dcmIVA.Value;
                          
                        //    fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //    {
                        //        dcmTasaOCuotaDR = it.dcmIVA.Value,
                        //        dcmBaseDR = decimal.Parse(dcmBaseDRtotal.ToString("#0.00")),
                        //        dcmImporteDR = decimal.Parse(dcmImporteDRtotal.ToString("#0.00")),
                        //        strTipoFactorDR = "Tasa",
                        //        strImpuestoDR = "002",
                        //        boolTraslado = true,
                        //        boolRetencion = false


                        //    });

                        

                            
                        //}
                        //else
                        //{

                        //    if (it.dcmIVA > 0)
                        //    {
                        //        if (it.dcmIVA == valueIVA.dcmTasaOCuotaDR)
                        //        {

                        //            valueIVA.dcmBaseDR = it.dcmImporte;
                        //            valueIVA.dcmImporteDR = it.dcmImporte * it.dcmIVA.Value;
                                    
                                 
                        //            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //            {
                        //                dcmTasaOCuotaDR = it.dcmIVA.Value,
                        //                dcmBaseDR = decimal.Parse(valueIVA.dcmBaseDR.ToString("#0.00")),
                        //                dcmImporteDR = decimal.Parse(valueIVA.dcmImporteDR.ToString("#0.00")),
                        //                strTipoFactorDR = "Tasa",
                        //                strImpuestoDR = "002",
                        //                boolTraslado = true,
                        //                boolRetencion = false

                        //            });

                                  
                        //        }
                        //        else
                        //        {
                                   

                        //            dcmBaseDRtotal = 0;
                        //            dcmImporteDRtotal = 0;
                                   
                        //            dcmBaseDRtotal = it.dcmImporte;
                        //            dcmImporteDRtotal = it.dcmImporte * it.dcmIVA.Value;

                                   
                        //            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //            {
                        //                dcmTasaOCuotaDR = it.dcmIVA.Value,
                        //                dcmBaseDR = decimal.Parse(dcmBaseDRtotal.ToString("#0.00")),
                        //                dcmImporteDR = decimal.Parse(dcmImporteDRtotal.ToString("#0.00")),
                        //                strTipoFactorDR = "Tasa",
                        //                strImpuestoDR = "002",
                        //                boolTraslado = true,
                        //                boolRetencion = false


                        //            });

                                   

                        //        }

                        //    }
                        //}

                        //if (valueRetIva == null && it.retIVA > 0)
                        //{
                           

                        //    dcmBaseDRtotal = 0;
                        //    dcmImporteDRtotal = 0;
                          
                        //    dcmBaseDRtotal += it.dcmImporte;
                        //    dcmImporteDRtotal += it.dcmImporte * it.retIVA.Value;

                           
                        //    fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //    {
                        //        dcmTasaOCuotaDR = it.retIVA.Value,
                        //        dcmBaseDR = decimal.Parse(dcmBaseDRtotal.ToString("#0.00")),
                        //        dcmImporteDR = decimal.Parse(dcmImporteDRtotal.ToString("#0.00")),
                        //        strTipoFactorDR = "Tasa",
                        //        strImpuestoDR = "002",
                        //        boolTraslado = false,
                        //        boolRetencion = true



                        //    });

                            
                        //}
                        //else
                        //{
                        //    if (it.retIVA > 0)
                        //    {
                        //        if (it.retIVA == valueRetIva.dcmTasaOCuotaDR)
                        //        {

                                  
                        //            valueRetIva.dcmBaseDR += it.dcmImporte;
                        //            valueRetIva.dcmImporteDR += it.dcmImporte * it.retIVA.Value;

                                  
                        //            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //            {
                        //                dcmTasaOCuotaDR = it.retIVA.Value,
                        //                dcmBaseDR = decimal.Parse(valueRetIva.dcmBaseDR.ToString("#0.00")),
                        //                dcmImporteDR = decimal.Parse(valueRetIva.dcmImporteDR.ToString("#0.00")),
                        //                strTipoFactorDR = "Tasa",
                        //                strImpuestoDR = "002",
                        //                boolTraslado = false,
                        //                boolRetencion = true

                        //            });

                                   


                        //        }
                        //        else
                        //        {
                                   

                        //            dcmBaseDRtotal = 0;
                        //            dcmImporteDRtotal = 0;
                                
                        //            dcmBaseDRtotal += it.dcmImporte;
                        //            dcmImporteDRtotal += it.dcmImporte * it.retIVA.Value;

                        //            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //            {
                        //                dcmTasaOCuotaDR = it.retIVA.Value,
                        //                dcmBaseDR = decimal.Parse(dcmBaseDRtotal.ToString("#0.00")),
                        //                dcmImporteDR = decimal.Parse(dcmImporteDRtotal.ToString("#0.00")),
                        //                strTipoFactorDR = "Tasa",
                        //                strImpuestoDR = "002",
                        //                boolTraslado = false,
                        //                boolRetencion = true



                        //            });

                                   


                        //        }
                        //    }
                        //}

                        //if (valueRetIsr == null && it.retISR > 0)
                        //{

                         
                        //    dcmBaseDRtotal = 0;
                        //    dcmImporteDRtotal = 0;
                           
                        //    dcmBaseDRtotal += it.dcmImporte;
                        //    dcmImporteDRtotal += it.dcmImporte * it.retISR.Value;


                        //    fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //    {
                        //        dcmTasaOCuotaDR = it.retISR.Value,
                        //        dcmBaseDR = decimal.Parse(dcmBaseDRtotal.ToString("#0.00")),
                        //        dcmImporteDR = decimal.Parse(dcmImporteDRtotal.ToString("#0.00")),
                        //        strTipoFactorDR = "Tasa",
                        //        strImpuestoDR = "001",
                        //        boolTraslado = false,
                        //        boolRetencion = true



                        //    });

                           
                        //}
                        //else
                        //{
                        //    if (it.retISR > 0)
                        //    {
                        //        if (it.retISR == valueRetIsr.dcmTasaOCuotaDR)
                        //        {

                                  

                        //            valueRetIsr.dcmBaseDR += it.dcmImporte;
                        //            valueRetIsr.dcmImporteDR += it.dcmImporte * it.retISR.Value;

                                  

                        //            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //            {
                        //                dcmTasaOCuotaDR = it.retISR.Value,
                        //                dcmBaseDR = decimal.Parse(valueRetIsr.dcmBaseDR.ToString("#0.00")),
                        //                dcmImporteDR = decimal.Parse(valueRetIsr.dcmImporteDR.ToString("#0.00")),
                        //                strTipoFactorDR = "Tasa",
                        //                strImpuestoDR = "001",
                        //                boolTraslado = false,
                        //                boolRetencion = true

                        //            });


                                   

                        //        }
                        //        else
                        //        {
                                  

                        //            dcmBaseDRtotal = 0;
                        //            dcmImporteDRtotal = 0;

                                 
                        //            dcmBaseDRtotal += it.dcmImporte;
                        //            dcmImporteDRtotal += it.dcmImporte * it.retIVA.Value;

                                 
                        //            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //            {
                        //                dcmTasaOCuotaDR = it.retISR.Value,
                        //                dcmBaseDR = decimal.Parse(dcmBaseDRtotal.ToString("#0.00")),
                        //                dcmImporteDR = decimal.Parse(dcmImporteDRtotal.ToString("#0.00")),
                        //                strTipoFactorDR = "Tasa",
                        //                strImpuestoDR = "001",
                        //                boolTraslado = false,
                        //                boolRetencion = true



                        //            });

                                  


                        //        }
                        //    }
                        //}

                        //if (valueRetIeps == null && it.retIEPS > 0)
                        //{
                           
                           

                        //    dcmBaseDRtotal += it.dcmImporte;
                        //    dcmImporteDRtotal += it.dcmImporte * it.retIEPS.Value;


                          
                        //    fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //    {
                        //        dcmTasaOCuotaDR = it.retIEPS.Value,
                        //        dcmBaseDR = decimal.Parse(dcmBaseDRtotal.ToString("#0.00")),
                        //        dcmImporteDR = decimal.Parse(dcmImporteDRtotal.ToString("#0.00")),
                        //        strTipoFactorDR = "Tasa",
                        //        strImpuestoDR = "003",
                        //        boolTraslado = false,
                        //        boolRetencion = true

                        //    });

                            
                        //}
                        //else
                        //{
                        //    if (it.retIEPS > 0)
                        //    {
                        //        if (it.retIEPS == valueRetIeps.dcmTasaOCuotaDR)
                        //        {
                                 

                        //            valueRetIeps.dcmBaseDR += it.dcmImporte;
                        //            valueRetIeps.dcmImporteDR += it.dcmImporte * it.retIEPS.Value;

                        //            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //            {
                        //                dcmTasaOCuotaDR = it.retIEPS.Value,
                        //                dcmBaseDR = decimal.Parse(valueRetIeps.dcmBaseDR.ToString("#0.00")),
                        //                dcmImporteDR = decimal.Parse(valueRetIeps.dcmImporteDR.ToString("#0.00")),
                        //                strTipoFactorDR = "Tasa",
                        //                strImpuestoDR = "003",
                        //                boolTraslado = false,
                        //                boolRetencion = true

                        //            });

                                   


                        //        }
                        //        else
                        //        {
                                   

                        //            dcmBaseDRtotal = 0;
                        //            dcmImporteDRtotal = 0;
                                  

                        //            dcmBaseDRtotal += it.dcmImporte;
                        //            dcmImporteDRtotal += it.dcmImporte * it.retIVA.Value;

                                  

                        //            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                        //            {
                        //                dcmTasaOCuotaDR = it.retIEPS.Value,
                        //                dcmBaseDR = decimal.Parse(dcmBaseDRtotal.ToString("#0.00")),
                        //                dcmImporteDR = decimal.Parse(dcmImporteDRtotal.ToString("#0.00")),
                        //                strTipoFactorDR = "Tasa",
                        //                strImpuestoDR = "003",
                        //                boolTraslado = false,
                        //                boolRetencion = true



                        //            });

                                   



                        //        }
                        //    }
                        //}
                        
                   
                        
                    
                    }

                    foreach(var i in lstImpVarios){

                        if (dcmSaldoInsoluto > 0)
                        {


                            decimal dcmBaseIva = dcmPagado / decimal.Parse("1.16");
                            decimal dcmImpIva = dcmBaseIva * decimal.Parse("0.16");


                            if (i.TypeIva)
                            {

                                fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                                {
                                    dcmTasaOCuotaDR = i.porcImp,
                                    dcmBaseDR = decimal.Parse(dcmBaseIva.ToString("#0.00")),
                                    dcmImporteDR = decimal.Parse(dcmImpIva.ToString("#0.00")),
                                    strTipoFactorDR = "Tasa",
                                    strImpuestoDR = "002",
                                    boolTraslado = true,
                                    boolRetencion = false


                                });
                            }
                        
                        
                        }
                        else
                        {
                            if (i.TypeIva)
                            {

                                fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                                {
                                    dcmTasaOCuotaDR = i.porcImp,
                                    dcmBaseDR = decimal.Parse(i.sumaBaseImp.ToString("#0.00")),
                                    dcmImporteDR = decimal.Parse(i.sumaImporteImp.ToString("#0.00")),
                                    strTipoFactorDR = "Tasa",
                                    strImpuestoDR = "002",
                                    boolTraslado = true,
                                    boolRetencion = false


                                });
                            }
                        }
                        if (i.TypeRetIva) {

                            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                            {
                                dcmTasaOCuotaDR = i.porcImp,
                                dcmBaseDR = decimal.Parse(i.sumaBaseImp.ToString("#0.00")),
                                dcmImporteDR = decimal.Parse(i.sumaImporteImp.ToString("#0.00")),
                                strTipoFactorDR = "Tasa",
                                strImpuestoDR = "002",
                                boolTraslado = false,
                                boolRetencion = true

                            });
                        }
                        if(i.TypeRetIsr){

                            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                            {
                                dcmTasaOCuotaDR = i.porcImp,
                                dcmBaseDR = decimal.Parse(i.sumaBaseImp.ToString("#0.00")),
                                dcmImporteDR = decimal.Parse(i.sumaImporteImp.ToString("#0.00")),
                                strTipoFactorDR = "Tasa",
                                strImpuestoDR = "001",
                                boolTraslado = false,
                                boolRetencion = true

                            });
                        }
                        if (i.TypeRetIeps)
                        {
                            fillDataImpDR.Add(new DataCompPago.DataCompPagoImpuestosDR()
                            {
                                dcmTasaOCuotaDR = i.porcImp,
                                dcmBaseDR = decimal.Parse(i.sumaBaseImp.ToString("#0.00")),
                                dcmImporteDR = decimal.Parse(i.sumaImporteImp.ToString("#0.00")),
                                strTipoFactorDR = "Tasa",
                                strImpuestoDR = "003",
                                boolTraslado = false,
                                boolRetencion = true



                            });
                        
                        
                        }
                    
                    
                    
                    }




                  


                     fillData.Add(new DataCompPago.fillDataCompPago { 
                           intId_factura = concepto.intId_factura,
                           strFolio = concepto.strFolio,
                           strSerie = concepto.strSerie,
                           strUUID = concepto.strUUID,
                           dtmFecha = concepto.dtmFecha,
                           strMoneda = concepto.strMoneda,
                           strFormaPago = concepto.strFormaPago,
                           dcmImporte = concepto.dcmImporte,
                           dcmPagado = concepto.dcmPagado,
                           dcmPendiente = concepto.dcmPendiente,
                           dcmMontoFact = concepto.dcmMontoFact,
                           fillDataImpuestosDR = fillDataImpDR
                     
                     
                     
                     });
                 }



                if (dcmSaldoInsoluto > 0)
                {


                    decimal dcmBaseIva = dcmPagado / decimal.Parse("1.16");
                    decimal dcmImpIva = dcmBaseIva * decimal.Parse("0.16");

                    fillDataImpP.Add(new DataCompPago.DataCompPagoImpuestosP()
                    {

                        dcmTasaOCuotaP = decimal.Parse("0.16"),
                        dcmBaseP = decimal.Parse(dcmBaseIva.ToString("#0.00")),
                        dcmImporteP = decimal.Parse(dcmImpIva.ToString("#0.00")),
                        strTipoFactorP = "Tasa",
                        strImpuestoP = "002",
                        boolTraslado = true,
                        boolRetencion = false

                    });



                }
                else
                {

                    if (dcmTotalTraslado > 0)
                    {
                        fillDataImpP.Add(new DataCompPago.DataCompPagoImpuestosP()
                        {

                            dcmTasaOCuotaP = decimal.Parse("0.16"),
                            dcmBaseP = decimal.Parse(dcmTrasladoBase.ToString("#0.00")),
                            dcmImporteP = decimal.Parse(dcmTotalTraslado.ToString("#0.00")),
                            strTipoFactorP = "Tasa",
                            strImpuestoP = "002",
                            boolTraslado = true,
                            boolRetencion = false

                        });
                    }
                }

                if (dcmTotalRetIva > 0)
                {
                    fillDataImpP.Add(new DataCompPago.DataCompPagoImpuestosP()
                    {

                        dcmTasaOCuotaP = decimal.Parse("0.0"),
                        dcmBaseP = decimal.Parse(dcmTotalRetIva.ToString("#0.00")),
                        dcmImporteP = decimal.Parse(dcmTotalRetIva.ToString("#0.00")),
                        strTipoFactorP = "Tasa",
                        strImpuestoP = "002",
                        boolTraslado = false,
                        boolRetencion = true

                    });
                }

                if (dcmTotalRetIsr > 0)
                {
                    fillDataImpP.Add(new DataCompPago.DataCompPagoImpuestosP()
                    {

                        dcmTasaOCuotaP = decimal.Parse("0.0"),
                        dcmBaseP = decimal.Parse(dcmTotalRetIsr.ToString("#0.00")),
                        dcmImporteP = decimal.Parse(dcmTotalRetIsr.ToString("#0.00")),
                        strTipoFactorP = "Tasa",
                        strImpuestoP = "001",
                        boolTraslado = false,
                        boolRetencion = true

                    });
                }
                if (dcmTotalRetIeps > 0)
                {



                    fillDataImpP.Add(new DataCompPago.DataCompPagoImpuestosP()
                    {

                        dcmTasaOCuotaP = decimal.Parse("0.0"),
                        dcmBaseP = decimal.Parse(dcmTotalRetIeps.ToString("#0.00")),
                        dcmImporteP = decimal.Parse(dcmTotalRetIeps.ToString("#0.00")),
                        strTipoFactorP = "Tasa",
                        strImpuestoP = "003",
                        boolTraslado = false,
                        boolRetencion = true

                    });
                }

              
                





                myDatagrid.fillDataImpuestosP = fillDataImpP;
                myDatagrid.fillData = fillData;
                myDatagrid.dcmTotal = decimal.Parse(txtMonto.Text);
                myDatagrid.dtmFechaPago = txbFecha;
                myDatagrid.rfcCtaOrdenante = rfcOrde;
                myDatagrid.rfcCtaBeneficiario = rfcBen;
                myDatagrid.strNumOperacion = txtNuOperacion.Text;
                myDatagrid.strMoneda = cmbMoneda.Text;
                myDatagrid.strTipoCambio = txtTipoCambio.Text;


                jsonGrid = JsonConvert.SerializeObject(myDatagrid);

            }
            catch (Exception ex) { }
        
        
        
        }

        private void cmbMoneda_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbMoneda.SelectedIndex == 1)
            {

                txbTipoCambio.Visibility = System.Windows.Visibility.Visible;
                txtTipoCambio.Visibility = System.Windows.Visibility.Visible;


            }
            else {

                txbTipoCambio.Visibility = System.Windows.Visibility.Collapsed;
                txtTipoCambio.Visibility = System.Windows.Visibility.Collapsed;
                txtTipoCambio.Text = "1";
            }
        }


       

    }
}
