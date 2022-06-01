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
using wpfEFac.Views.General;
using wpfEFac.Helpers;
using System.Collections.ObjectModel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Data.OleDb;


namespace wpfEFac.Views.PuntoVenta
{
    /// <summary>
    /// Interaction logic for NuevaFacturaWindow.xaml
    /// </summary>
    public partial class PreFactura : Page
    {
        private eFacDBEntities mydb;
        private PreFacturaViewModel pfvm;
       
        private const string whiteSpace = " ";
        public bool IsEditMode { get; set; }
        private ObservableCollection<CarritoComprasEntry> conceptos;
        private int intID;
        private string strSerie;
        private string uuidSave;
        private string strFolio ;
        private Models.Empresa empresa;
        private Models.Clientes clientes;
        private string strProveedor;
        private string strNumero;
        private string strNumeroContrato;
        private string intEstimacion;
        //private string strObservaciones;
        private System.Data.Objects.DataClasses.EntityCollection<Detalle_Factura> entityCollection;
        //public System.Data.Objects.DataClasses.EntityCollection<Traslados> traslados;
        private int intTipoComprobante;
        private string strRetencionIva;
        private string strRetencionIsr;
        private string jsonString = "";
        private string jsonGrid = "";
        private string urlXMLfactoraje = "";
        private string urlPDFfactoraje = "";
        List<dataCartaPorte.UbicacionOrigen> myUbicacionesOr = new List<dataCartaPorte.UbicacionOrigen>();
        List<dataCartaPorte.UbicacionDestino> myUbicacionesDes = new List<dataCartaPorte.UbicacionDestino>();
        List<dataCartaPorte.Remolque> myRemolque = new List<dataCartaPorte.Remolque>();
        List<dataCartaPorte.TiposFigura> myTiposFigura = new List<dataCartaPorte.TiposFigura>();
        List<string> lstXML = new List<string>();
       
        public PreFactura()
        {
            InitializeComponent();
            pfvm = new PreFacturaViewModel();
            conceptos = new ObservableCollection<CarritoComprasEntry>();

            dtpFechaSal_lleg.SelectedDate = DateTime.Now;

            //LoadSerieAndFolio();
            

            dtgConceptos.ItemsSource = conceptos;
            conceptos.CollectionChanged += conceptos_CollectionChanged;

            // txbFecha.Text = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");

           
            

         
            
        }


        public PreFactura(int intIdTipoComprobante, List<Relacionados>RelacionadosUUID)
        {
            InitializeComponent();
            pfvm = new PreFacturaViewModel();
            mydb = new eFacDBEntities();
            conceptos = new ObservableCollection<CarritoComprasEntry>();

            dtpFechaSal_lleg.SelectedDate = DateTime.Now;
           
            LoadSerieAndFolio(intIdTipoComprobante);

           


            dtgConceptos.ItemsSource = conceptos;
            conceptos.CollectionChanged += conceptos_CollectionChanged;

           // txbFecha.Text = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss");
            this.intTipoComprobante = intIdTipoComprobante;
            txtDivisa.Text = "MXN";


            try
            {
                foreach (Relacionados item in RelacionadosUUID)
                {

                    uuidSave += item.idFact + "|";

                }

                cmbTipoRelacion.Visibility = System.Windows.Visibility.Visible;
                txbTipoRelacion.Visibility = System.Windows.Visibility.Visible;
          

            }
            catch (Exception) { }



            try
            {
                wpfEFac.Models.Estado c = new Models.Estado();
                wpfEFac.Models.Configuracion_Vehicular conf = new Models.Configuracion_Vehicular();
                wpfEFac.Models.TipoRemolque t = new Models.TipoRemolque();

                cmbEstadoOr.ItemsSource = mydb.Estado.OrderBy(x => x.strNombreEstado);
                cmbEstadoOr.DisplayMemberPath = "strNombreEstado";
                cmbEstadoOr.SelectedValuePath = "intID";
                cmbEstadoOr.SelectedValue = c.intID;



                cmbEstadoDes.ItemsSource = mydb.Estado.OrderBy(x => x.strNombreEstado);
                cmbEstadoDes.DisplayMemberPath = "strNombreEstado";
                cmbEstadoDes.SelectedValuePath = "intID";
                cmbEstadoDes.SelectedValue = c.intID;

                cmbConfVehi.ItemsSource = mydb.Configuracion_Vehicular.OrderBy(x => x.str_descripcion);
                cmbConfVehi.DisplayMemberPath = "str_descripcion";
                cmbConfVehi.SelectedValuePath = "str_codigo";
                cmbConfVehi.SelectedValue = conf.id;


                cmbUnidadPeso.ItemsSource = mydb.UnidadMedida.Where(u =>u.intId>1000);
                cmbUnidadPeso.DisplayMemberPath = "strDescripcion";
                cmbUnidadPeso.SelectedValuePath = "intId";
                cmbUnidadPeso.SelectedValue = conf.id;

                cmbTipoRemolque.ItemsSource = mydb.TipoRemolque.OrderBy(x => x.str_descripcion);
                cmbTipoRemolque.DisplayMemberPath = "str_descripcion";
                cmbTipoRemolque.SelectedValuePath = "str_codigo";
                cmbTipoRemolque.SelectedValue = conf.id;



                cmbUbicaciones.ItemsSource = mydb.Clientes.Where(a => a.strTipodeInscripcion == "Origen" || a.strTipodeInscripcion == "Destino" || a.strTipodeInscripcion == "01-Operador" || a.strTipodeInscripcion == "02-Propietario" || a.strTipodeInscripcion == "03-Arrendador" || a.strTipodeInscripcion == "04-Notificado");
                cmbUbicaciones.DisplayMemberPath = "strNombreComercial";
                cmbUbicaciones.SelectedValuePath = "intID";
                cmbUbicaciones.SelectedValue = conf.id;

                atcUbicaciones.ItemsSource = mydb.Clientes.Where(a => a.strTipodeInscripcion == "Origen" || a.strTipodeInscripcion == "Destino" || a.strTipodeInscripcion == "01-Operador" || a.strTipodeInscripcion == "02-Propietario" || a.strTipodeInscripcion == "03-Arrendador" || a.strTipodeInscripcion == "04-Notificado");
                atcVehiculoTracto.ItemsSource = mydb.Vehiculos.Where(a => a.strTipoVehiculo == "Tracto");
                atcVehiculoRemolque.ItemsSource = mydb.Vehiculos.Where(a => a.strTipoVehiculo == "Remolque");
                

            }
            catch (Exception ex) { 
            
            }
     
          
            
                
            
           
         
            
            if (this.intTipoComprobante == 1)
        
            {

                grpDatosFiscales.Header = "F A C T U R A";
                
                

              
            }

            if (this.intTipoComprobante == 2) 
            {
                grpDatosFiscales.Header = "N O T A   D E   C R E D I T O";
                
            }

            if (this.intTipoComprobante == 3) 
            {
                grpDatosFiscales.Header = "N O T A   D E   C A R G O";
                
              
            }
            if (this.intTipoComprobante == 4)
            {
                grpDatosFiscales.Header = "R E C I B O   D E   C O M I S I O N";
                
              
            }

            if (this.intTipoComprobante == 5)
            {
                grpDatosFiscales.Header = "C A R T A  P O R T E";

                spDestino.Visibility = System.Windows.Visibility.Visible;
                spOrigen.Visibility = System.Windows.Visibility.Visible;
                spRecoger.Visibility = System.Windows.Visibility.Visible;
                //txtRecogerEn.Visibility = System.Windows.Visibility.Visible;
                //txtOrigen.Visibility = System.Windows.Visibility.Visible;
                
                //lblEncabezado.Text = "VALOR UNIT. CUOTA CONV. POR TON. O CARGA FRAC.";
              ///  lblObservaciones.Text = "VALOR DECLARADO";
               // lblObservacionesAdicionales.Text = "OBSERVACIONES ADICIONALES";

            }

            if (this.intTipoComprobante == 6)
            {
                grpDestino.Visibility = System.Windows.Visibility.Visible;
                lblRecogerEn.Visibility = System.Windows.Visibility.Visible;
                lblOrigen.Visibility = System.Windows.Visibility.Visible;
                txtRecogerEn.Visibility = System.Windows.Visibility.Visible;
                txtOrigen.Visibility = System.Windows.Visibility.Visible;

                grpDatosFiscales.Header = "O R D E N   D E   C A R G A";
                
                lblRetIsr.Text = "Ret ISR";
                lblTotal.Text = "Total";



            }
            if (this.intTipoComprobante == 7)
            {
                grpDatosFiscales.Header = "COMPLEMENTO CARTA PORTE";

                xpnAddenda.Visibility = System.Windows.Visibility.Visible;
                spEncabezado.Visibility = System.Windows.Visibility.Collapsed;
                spObservaciones.Visibility = System.Windows.Visibility.Collapsed;
                //spOrigenCCP.Visibility = System.Windows.Visibility.Visible;
               // spDestinoCCP.Visibility = System.Windows.Visibility.Visible;
                //xpnAddenda.Visibility = System.Windows.Visibility.Visible;
                spPermisos.Visibility = System.Windows.Visibility.Visible;
                spVehiculo.Visibility = System.Windows.Visibility.Visible;

                // lblRetIsr.Text = "Ret ISR";
                //  lblTotal.Text = "Total";

            }

            
            
        }

        public PreFactura(int intID, string strSerie, string strFolio, Models.Empresa empresa,
            Models.Clientes clientes, string strProveedor, string strNumero,
            string strNumeroContrato, string intEstimaion, string strObservaciones,
            System.Data.Objects.DataClasses.EntityCollection<Detalle_Factura> conceptos, 
            Factura f)
        {
            InitializeComponent();
            pfvm = new PreFacturaViewModel();
            mydb = new eFacDBEntities();
            LoadSerieAndFolio(f.intID_Tipo_CFD);
            this.conceptos = new ObservableCollection<CarritoComprasEntry>();
            dtgConceptos.ItemsSource = this.conceptos;

            try
            {
                wpfEFac.Models.Estado c = new Models.Estado();
                wpfEFac.Models.Configuracion_Vehicular conf = new Models.Configuracion_Vehicular();
                wpfEFac.Models.TipoRemolque t = new Models.TipoRemolque();

                cmbEstadoOr.ItemsSource = mydb.Estado.OrderBy(x => x.strNombreEstado);
                cmbEstadoOr.DisplayMemberPath = "strNombreEstado";
                cmbEstadoOr.SelectedValuePath = "intID";
                cmbEstadoOr.SelectedValue = c.intID;



                cmbEstadoDes.ItemsSource = mydb.Estado.OrderBy(x => x.strNombreEstado);
                cmbEstadoDes.DisplayMemberPath = "strNombreEstado";
                cmbEstadoDes.SelectedValuePath = "intID";
                cmbEstadoDes.SelectedValue = c.intID;

                cmbConfVehi.ItemsSource = mydb.Configuracion_Vehicular.OrderBy(x => x.str_descripcion);
                cmbConfVehi.DisplayMemberPath = "str_descripcion";
                cmbConfVehi.SelectedValuePath = "str_codigo";
                cmbConfVehi.SelectedValue = conf.id;

                cmbTipoRemolque.ItemsSource = mydb.TipoRemolque.OrderBy(x => x.str_descripcion);
                cmbTipoRemolque.DisplayMemberPath = "str_descripcion";
                cmbTipoRemolque.SelectedValuePath = "str_codigo";
                cmbTipoRemolque.SelectedValue = conf.id;


                cmbUnidadPeso.ItemsSource = mydb.UnidadMedida.Where(u => u.intId > 1000);
                cmbUnidadPeso.DisplayMemberPath = "strDescripcion";
                cmbUnidadPeso.SelectedValuePath = "intId";
                cmbUnidadPeso.SelectedValue = conf.id;


                cmbUbicaciones.ItemsSource = mydb.Clientes.Where(a => a.strTipodeInscripcion == "Origen" || a.strTipodeInscripcion == "Destino" || a.strTipodeInscripcion == "01-Operador" || a.strTipodeInscripcion == "02-Propietario" || a.strTipodeInscripcion == "03-Arrendador" || a.strTipodeInscripcion == "04-Notificado");
                cmbUbicaciones.DisplayMemberPath = "strNombreComercial";
                cmbUbicaciones.SelectedValuePath = "intID";
                cmbUbicaciones.SelectedValue = conf.id;

                atcUbicaciones.ItemsSource = mydb.Clientes.Where(a => a.strTipodeInscripcion == "Origen" || a.strTipodeInscripcion == "Destino" || a.strTipodeInscripcion == "01-Operador" || a.strTipodeInscripcion == "02-Propietario" || a.strTipodeInscripcion == "03-Arrendador" || a.strTipodeInscripcion == "04-Notificado");
                atcVehiculoTracto.ItemsSource = mydb.Vehiculos.Where(a => a.strTipoVehiculo == "Tracto");
                atcVehiculoRemolque.ItemsSource = mydb.Vehiculos.Where(a => a.strTipoVehiculo == "Remolque");
            }
            catch (Exception ex)
            {

            }
           


            xpnDetalleFactura.Width = 600;
            xpnAddenda.Visibility = System.Windows.Visibility.Collapsed;
            xpnAddenda.Width = 450;


            


            this.conceptos.CollectionChanged += conceptos_CollectionChanged;
            this.intTipoComprobante = f.intID_Tipo_CFD;
            this.IsEditMode = true;
            // TODO: Complete member initialization
            this.intID = intID;
            this.strSerie = strSerie;
            this.strFolio = strFolio;



            //
            this.empresa = empresa;
            this.clientes = clientes;
            this.strProveedor = strProveedor;
            this.strNumero = strNumero;
            this.strNumeroContrato = strNumeroContrato;
            this.intEstimacion = intEstimaion;

            this.txtEncabezado.Text = strObservaciones;
            this.txtObservaciones.Text = strProveedor;
            this.txtImpuestosAdicionales.Text = strNumero;

            this.txtOrigen.Text = f.Origen;
            this.txtRecogerEn.Text = f.RecogerEn;
            this.txtDestino.Text = f.Destino;
            this.txtDestinatario.Text = f.Destinatario;
            this.txtRfcDestinatario.Text = f.rfcDestinatario;
            this.txtDomicilioDestinatario.Text = f.domicilioDestinatario;
            this.txtEntregarEn.Text = f.EntregarEn;

            urlXMLfactoraje = f.strXMLpath;
            this.entityCollection = conceptos;
            this.txtSubtotal.Text = f.dcmSubTotal.ToString();
            this.txtIva.Text = f.dcmIVA.ToString();
            this.txtTotal.Text = f.dcmTotal.ToString();


            /*
             * 
             */
            txtSerie.Text = f.strSerie;
            txtFolio.Text = f.strFolio;
            //cmbFormaPago.Text = f.strForma_Pago;

            if (f.MetodoPago.Equals("PUE"))
                cmbMetodoPago.SelectedIndex = 0;
            if (f.MetodoPago.Equals("PPD"))
                cmbMetodoPago.SelectedIndex = 1;


            txtCondicionesPago.Text = f.CondPago;

            string DataPublicoGeneral = f.Destino;

            cmbPeriocidad.Text =  DataPublicoGeneral.Split('/')[0];
            txtMes.Text = DataPublicoGeneral.Split('/')[1];
            txtAno.Text = DataPublicoGeneral.Split('/')[2];
            
            
            
            if (f.strForma_Pago.Equals("01"))
                cmbFormaPago.SelectedIndex = 0;
            if (f.strForma_Pago.Equals("02"))
                cmbFormaPago.SelectedIndex = 1;
            if (f.strForma_Pago.Equals("03"))
                cmbFormaPago.SelectedIndex = 2;
            if (f.strForma_Pago.Equals("04"))
                cmbFormaPago.SelectedIndex = 3;
            if (f.strForma_Pago.Equals("05"))
                cmbFormaPago.SelectedIndex = 4;
            if (f.strForma_Pago.Equals("06"))
                cmbFormaPago.SelectedIndex = 5;
            if (f.strForma_Pago.Equals("08"))
                cmbFormaPago.SelectedIndex = 6;
            if (f.strForma_Pago.Equals("12"))
                cmbFormaPago.SelectedIndex = 7;
            if (f.strForma_Pago.Equals("13"))
                cmbFormaPago.SelectedIndex = 8;
            if (f.strForma_Pago.Equals("14"))
                cmbFormaPago.SelectedIndex = 9;
            if (f.strForma_Pago.Equals("15"))
                cmbFormaPago.SelectedIndex = 10;
            if (f.strForma_Pago.Equals("17"))
                cmbFormaPago.SelectedIndex = 11;
            if (f.strForma_Pago.Equals("23"))
                cmbFormaPago.SelectedIndex = 12;
            if (f.strForma_Pago.Equals("24"))
                cmbFormaPago.SelectedIndex = 13;
            if (f.strForma_Pago.Equals("25"))
                cmbFormaPago.SelectedIndex = 14;
            if (f.strForma_Pago.Equals("26"))
                cmbFormaPago.SelectedIndex = 15;
            if (f.strForma_Pago.Equals("27"))
                cmbFormaPago.SelectedIndex = 16;
            if (f.strForma_Pago.Equals("28"))
                cmbFormaPago.SelectedIndex = 17;
            if (f.strForma_Pago.Equals("29"))
                cmbFormaPago.SelectedIndex = 18;
            if (f.strForma_Pago.Equals("30"))
                cmbFormaPago.SelectedIndex = 19;
            if (f.strForma_Pago.Equals("31"))
                cmbFormaPago.SelectedIndex = 20;
            if (f.strForma_Pago.Equals("99"))
                cmbFormaPago.SelectedIndex = 21;
                          

            //cmbMetodoPago.Text = f.MetodoPago;
            //txtMotivoDescuento.Text = f.MotivoDesc;
            txtDivisa.Text = f.Divisa;
            txtTipoCambio.Text = f.TipoCambio.Value.ToString("#0.0000");

            if (f.MotivoDesc.Equals("G01"))
                cmbUsoCfdi.SelectedIndex = 0;
            if (f.MotivoDesc.Equals("G02"))
                cmbUsoCfdi.SelectedIndex = 1;
            if (f.MotivoDesc.Equals("G03"))
                cmbUsoCfdi.SelectedIndex = 2;
            if (f.MotivoDesc.Equals("I01"))
                cmbUsoCfdi.SelectedIndex = 3;
            if (f.MotivoDesc.Equals("I02"))
                cmbUsoCfdi.SelectedIndex = 4;
            if (f.MotivoDesc.Equals("I03"))
                cmbUsoCfdi.SelectedIndex = 5;
            if (f.MotivoDesc.Equals("I04"))
                cmbUsoCfdi.SelectedIndex = 6;
            if (f.MotivoDesc.Equals("I05"))
                cmbUsoCfdi.SelectedIndex = 7;
            if (f.MotivoDesc.Equals("I06"))
                cmbUsoCfdi.SelectedIndex = 8;
            if (f.MotivoDesc.Equals("I07"))
                cmbUsoCfdi.SelectedIndex = 9;
            if (f.MotivoDesc.Equals("I08"))
                cmbUsoCfdi.SelectedIndex = 10;
            if (f.MotivoDesc.Equals("D01"))
                cmbUsoCfdi.SelectedIndex = 11;
            if (f.MotivoDesc.Equals("D02"))
                cmbUsoCfdi.SelectedIndex = 12;
            if (f.MotivoDesc.Equals("D03"))
                cmbUsoCfdi.SelectedIndex = 13;
            if (f.MotivoDesc.Equals("D04"))
                cmbUsoCfdi.SelectedIndex = 14;
            if (f.MotivoDesc.Equals("D05"))
                cmbUsoCfdi.SelectedIndex = 15;
            if (f.MotivoDesc.Equals("D06"))
                cmbUsoCfdi.SelectedIndex = 16;
            if (f.MotivoDesc.Equals("D07"))
                cmbUsoCfdi.SelectedIndex = 17;
            if (f.MotivoDesc.Equals("D08"))
                cmbUsoCfdi.SelectedIndex = 18;
            if (f.MotivoDesc.Equals("D09"))
                cmbUsoCfdi.SelectedIndex = 19;
            if (f.MotivoDesc.Equals("D10"))
                cmbUsoCfdi.SelectedIndex = 20;
            if (f.MotivoDesc.Equals("P01"))
                cmbUsoCfdi.SelectedIndex = 21;

            txtOrigen.Text = f.Origen;
            txtPedido.Text = f.RecogerEn;
            txtCC.Text = f.Destino;

            /*
             * 
             */

            if (this.intTipoComprobante == 1) 
            {
                grpDatosFiscales.Header = "F A C T U R A";
                

            }

            if (this.intTipoComprobante == 2) 
            {
                grpDatosFiscales.Header = "N O T A   D E   C R E D I T O";
                

            }

            if (this.intTipoComprobante == 3)
            {
                grpDatosFiscales.Header = "A R R E N D A M I E N T O";
                

            }
            if (this.intTipoComprobante == 4)
            {
                grpDatosFiscales.Header = "H O N O R A R I O S";
                

            }
            if (this.intTipoComprobante == 5)
            {
                grpDatosFiscales.Header = "C A R T A  P O R T E";
                grpDestino.Visibility = System.Windows.Visibility.Visible;
                lblRecogerEn.Visibility = System.Windows.Visibility.Visible;
                lblOrigen.Visibility = System.Windows.Visibility.Visible;
                txtRecogerEn.Visibility = System.Windows.Visibility.Visible;
                txtOrigen.Visibility = System.Windows.Visibility.Visible;

                lblEncabezado.Text = "VALOR UNIT. CUOTA CONV. POR TON. O CARGA FRAC.";
                lblObservaciones.Text = "VALOR DECLARADO";
                lblObservacionesAdicionales.Text = "OBSERVACIONES ADICIONALES";

            }
            if (this.intTipoComprobante == 6)
            {
                grpDestino.Visibility = System.Windows.Visibility.Visible;
                lblRecogerEn.Visibility = System.Windows.Visibility.Visible;
                lblOrigen.Visibility = System.Windows.Visibility.Visible;
                txtRecogerEn.Visibility = System.Windows.Visibility.Visible;
                txtOrigen.Visibility = System.Windows.Visibility.Visible;

                grpDatosFiscales.Header = "O R D E N   D E   C A R G A";
                

            }
            if (this.intTipoComprobante == 7)
            {
                grpDatosFiscales.Header = "COMPLEMENTO CARTA PORTE";
                
            }





            if (f.strCadenaOriginal.Contains("TranspInternac"))
            {
                chbAddCCP.IsChecked = true;
                fillEditCP(f);
               


            }


        }

        private void LoadSerieAndFolio(int TipoComprobante)
        {
            
            try
            {

                eFacDBEntities mydb = new eFacDBEntities();
                Models.CFD MyCFD = mydb.CFD.FirstOrDefault(f => f.intID == TipoComprobante);
                Models.Folios myFolio = mydb.Folios.FirstOrDefault(f => f.intID == MyCFD.intIdFolio);

                String Serie = myFolio.strSerie;
                int Folio = myFolio.intFolioActual;
                txtSerie.Text = Serie;
                txtFolio.Text = Folio.ToString();

            }
            catch (Exception)
            {
                txtSerie.Text = "";
                txtFolio.Text = "";
            }
        }

        public void conceptos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (conceptos.Count>0)
            {
                CalcularTotal();
                /*
                if (this.intTipoComprobante == 1) CalcularTotal();
                if (this.intTipoComprobante == 2) CalcularTotal();
                
                if ((this.intTipoComprobante == 3) )   CalcularTotal_RecHon();
                if ((this.intTipoComprobante == 4)) CalcularTotal_RecHon();

                if ((this.intTipoComprobante == 5)) CalcularTotal_RecHon();

                

                if ((this.intTipoComprobante == 6) ) CalcularTotal();
                if ((this.intTipoComprobante == 7)) CalcularTotal();
                */
            }
            else
            {
                //txtNumeroLetra.Text =
                
            }
            ValidarConceptos();
        }

        private void CalcularTotal()
        {
            decimal importe = 0;
            decimal descuento = 0;
            decimal subtotal=0;
            decimal iva=0;
            decimal total = 0;
            decimal retIva = 0;
            decimal retIsr = 0;
            decimal retIeps = 0;
            decimal granTotal = 0;
            
            
            txtRetIsr.Text = "0.00";
            txtRetIva.Text = "0.00";
            
            foreach (var item in conceptos)
            {
                decimal importePartida = (Decimal.Parse(item.FormatImporte.ToString()));
                //importe += (item.PrecioUnitario * item.Cantidad);
                importe += importePartida; //(Decimal.Parse(item.FormatImporte.ToString()));
                descuento += item.Descuento;

                iva += (importePartida * item.IVA);
                retIva += (importePartida * item.retIVA);
                retIsr += (importePartida * item.retISR);
                retIeps += (importePartida * item.retIEPS);
            }
            subtotal = importe ;

           // iva = iva - descuento;

            total = subtotal + iva;
            granTotal = total - (retIva + retIsr + retIeps);

            decimal subtotal1 = importe + descuento;

            txtImporte.Text = subtotal1.ToString("N"); //importe.ToString("N");
            txtDescuento.Text = descuento.ToString("N");
            
            txtSubtotal.Text = subtotal.ToString("N");
            txtIva.Text = iva.ToString("N");
            txtTotal.Text = total.ToString("N");

            
            txtRetIva.Text = retIva.ToString("N");
            txtRetIsr.Text = retIsr.ToString("N");
            txtRetIeps.Text = retIeps.ToString("N");

            txtGranTotal.Text = granTotal.ToString("N");

            

            //txtTotal.Text =total.ToString("N");

            ShowImporteConLetra(txtTotal.Text, "PESOS");
        }


       
       

        

        private void CalcularTotal_RecHon()
        {
            decimal importe = 0;
            decimal iva = 0;
            decimal subtotal = 0;

            decimal retIva = 0;
            decimal retIsr = 0;
            decimal total = 0;
            
            foreach (var item in conceptos)
            {
                importe += item.Importe;
                
            }

            iva = importe * (decimal)0.16;
            subtotal = importe + iva;

            if (this.strRetencionIva == "Si")
            {
                 retIva = importe * (decimal)0.04;
               // retIva = importe * (decimal)0.10666666;
            }
            else
                retIva = 0;

            if (this.strRetencionIsr == "Si")
                retIsr = importe * (decimal)0.10;
            else
                retIsr = 0;
            
            total = subtotal - (retIva + retIsr);
            
           // total = subtotal;


            txtSubtotal.Text = importe.ToString("N");
            txtIva.Text = iva.ToString("N");
            txtIva.Text = subtotal.ToString("N");

            txtRetIva.Text = retIva.ToString("N");
            txtRetIsr.Text = retIsr.ToString("N");
            
            

            txtTotal.Text = total.ToString("N");

            ShowImporteConLetra(txtTotal.Text, "PESOS");
        }
        private void ShowImporteConLetra(string numero, string divisa)
        {
            //txtNumeroLetra.Text = ConvertidorNumeroLetra.NumeroALetras(numero, divisa);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                ShowEmpresa();
                GetClientes();    
            }
            else
            {
                ShowEmpresa(empresa);
                GetClientes(clientes);
                ShowConceptos();
                ShowTraslados();
                ShowAdds();
            }
        }

        private void ShowTraslados()
        {
            //throw new NotImplementedException();
        }

        private void ShowAdds()
        {
            //txbSerie.Text = strSerie;
            
            //txbFolio.Text = strFolio;
            CalcularTotal();

            //txtObservaciones.Text = strObservaciones;
            /*
            if (this.intTipoComprobante==1)   CalcularTotal();
            if (this.intTipoComprobante == 2) CalcularTotal();


            if ((this.intTipoComprobante == 3)) CalcularTotal_RecHon();


            if ((this.intTipoComprobante == 4)) CalcularTotal_RecHon();

            if (this.intTipoComprobante == 5) CalcularTotal_RecHon();
            if (this.intTipoComprobante == 6) CalcularTotal();
            if (this.intTipoComprobante == 7) CalcularTotal();
             */
        }

        private void ShowConceptos()
        {
            foreach (var item in entityCollection)
            {
                CarritoComprasEntry entry = new CarritoComprasEntry();
                entry.Cantidad = item.dcmCantidad;
                entry.Unidad = item.strUnidad;
                entry.Codigo = item.Productos.strCodigo;
                entry.intID = item.Productos.intID;
                entry.Nombre = item.strConcepto;
                entry.PrecioUnitario = item.dcmPrecioUnitario;
                entry.FormatPrecioUnitario = item.dcmPrecioUnitario.ToString("N");
                entry.isMercancia = item.strPatida;
                entry.IVA = item.dcmIVA;
                entry.retIVA = item.retIVA.Value;
                entry.retISR = item.retISR.Value;
                entry.retIEPS = item.retIEPS.Value;
                entry.Descuento = item.dcmDescuento;
                entry.FormatDescuento = item.dcmDescuento + " %";
                entry.Importe = item.dcmImporte;
                entry.FormatImporte = item.dcmImporte.ToString("N");

                conceptos.Add(entry);
            }
        }

        private void ShowEmpresa(Models.Empresa empresa)
        {
            var emp = empresa;
            /*
            txbNombreEmpresa.Text = emp.strRazonSocial;
            txbRFC.Text = emp.strRFC;

            var direccion = pfvm.GetDireccionEmpresa(emp.intID);

            txbDomicilio.Text = "Calle:" + direccion.strCalle + whiteSpace + "Numero: "
                + direccion.strNoInterior + whiteSpace + direccion.strNoExterior + whiteSpace
                + "Colonia: " + direccion.strColonia + whiteSpace +
                "CP:" + direccion.strCodigoPostal + whiteSpace + direccion.strPoblacionLocalidad +
                whiteSpace + direccion.strMunicipio + whiteSpace + direccion.Estado.strNombreEstado +
                whiteSpace + direccion.Paises.strNombrePais;
             */
        }

        private void ShowEmpresa()
        {
            try
            {
                /*
                var emp = pfvm.GetEmpresa(Convert.ToInt32(App.Current.Properties["idUsuario"]));
             
                txbNombreEmpresa.Text = emp.strRazonSocial;
                txbRFC.Text = emp.strRFC;

                var direccion = pfvm.GetDireccionEmpresa(emp.intID);

                txbDomicilio.Text = "Calle:" + direccion.strCalle + whiteSpace + "Numero: "
                    + direccion.strNoInterior + whiteSpace + direccion.strNoExterior + whiteSpace
                    + "Colonia: " + direccion.strColonia + whiteSpace +
                    "CP:" + direccion.strCodigoPostal + whiteSpace + direccion.strPoblacionLocalidad +
                    whiteSpace + direccion.strMunicipio + whiteSpace + direccion.Estado.strNombreEstado +
                    whiteSpace + direccion.Paises.strNombrePais;
                 */
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al cagar la empresa intentelo mas tarde (error: " + error.Message + ")");
            }
            
        }

        private void GetClientes(Models.Clientes clientes)
        {
            GetClientes();
            atcNombreCliente.Text = clientes.strNombreComercial;
            atcRFC.Text = clientes.strRFC;
            atcNombreCliente.SelectedItem = clientes;
        }

        private void GetClientes()
        {
            var clientes = pfvm.GetClientes();
            atcNombreCliente.ItemsSource = clientes;
            atcRFC.ItemsSource = clientes;
        }

        private void btnAgregarCarrito_Click(object sender, RoutedEventArgs e)
        {
            AddProductoWindow carrito = new AddProductoWindow();
            carrito.Show();
            carrito.EntryAdd += (s, args) =>
            {
                conceptos.Add(carrito.entry);
            };
        }

        bool isAddenda = false;


        private void fillJson() {
            try{
            dataCartaPorte.Root myRoot = new dataCartaPorte.Root();

            List<Item> myDataGrid = new List<Item>();

           // List<dataGridCP.DataCP> myDataGrid = new List<dataGridCP.DataCP>();


           
            myRoot.TranspInternac = "No";
            myRoot.TotalDistRec = 1;


            foreach (var item in dtgUbicaciones.Items)
            {

                Item concepto = item as Item;


                //myTiposFigura.Add(
                //new dataCartaPorte.TiposFigura
                //{
                //    RFCFigura = txtRFCOp.Text,
                //    NumLicencia = txtLicOp.Text,
                //    TipoFigura = cmbTipoFigura.Text
                //});

                myDataGrid.Add(new Item
                {

                intID = concepto.intID,
                strRFC = concepto.strRFC,
                strRazonSocial = concepto.strRazonSocial,
                strDescripcion = concepto.strDescripcion,
                strTipo = concepto.strTipo,
                dtmFecha = concepto.dtmFecha,
                idOrigen = concepto.idOrigen,
                idDestino = concepto.idDestino,
                dcmDistancia = concepto.dcmDistancia,
            });

                if(concepto.strTipo !="Remolque"){
                int idCliente = int.Parse(concepto.intID);
                Models.Clientes getPerson = mydb.Clientes.Where(i => i.intID == idCliente ).First();
                Models.Direcciones_Fiscales getDireccion = mydb.Direcciones_Fiscales.Where(i => i.strIDCliente == getPerson.intID).First();
                Estado getEdo = mydb.Estado.Where(e => e.intID == getDireccion.intIDEstado).First();


                string strEdo = getEdo.strNombreEstado.Split('-')[0];
                string Municipio = "";
                string Localidad = "";
                string Colonia = "";
                if (!string.IsNullOrEmpty(getDireccion.strMunicipio))
                {
                    Municipio = getDireccion.strMunicipio.Split('-')[0];
                }
                if (!string.IsNullOrEmpty(getDireccion.strPoblacionLocalidad))
                {
                    Localidad = getDireccion.strPoblacionLocalidad.Split('-')[0];
                }
                if (!string.IsNullOrEmpty(getDireccion.strColonia))
                {
                    Colonia = getDireccion.strColonia.Split('-')[0];
                }

                if (getPerson.strTipodeInscripcion == "Origen")
                {
                    dataCartaPorte.Origen myDataOrigen = new dataCartaPorte.Origen();
                    myDataOrigen.IDUbicacion = concepto.idOrigen; //txtIdOrigen.Text;
                    myDataOrigen.RFCRemitenteDestinatario = getPerson.strRFC; //txtRfcRemitente.Text;
                    myDataOrigen.NombreRemitenteDestinatario = getPerson.strRazonSocial;

                    myDataOrigen.TipoUbicacion = getPerson.strTipodeInscripcion;
                    myDataOrigen.FechaHoraSalidaLlegada = DateTime.Parse(concepto.dtmFecha); //DateTime.Parse(dtpFechaSal_lleg.ToString());



                    dataCartaPorte.DomicilioOrigen myDomOri = new dataCartaPorte.DomicilioOrigen();
                    myDomOri.Pais = "MEX";///cmbPaisOr.Text;
                    myDomOri.NumeroExterior = getDireccion.strNoExterior;
                    myDomOri.Municipio = Municipio; //value3;//cmbMunicipioOr.Text;
                    myDomOri.Localidad = Localidad;//value2;//cmbLocOr.Text;
                    myDomOri.Estado = strEdo;
                    myDomOri.Colonia = Colonia;
                    myDomOri.CodigoPostal = getDireccion.strCodigoPostal; //txtCpOr.Text;
                    myDomOri.Calle = getDireccion.strCalle;


                    myUbicacionesOr.Add(new dataCartaPorte.UbicacionOrigen
                    {
                        // DistanciaRecorridaOrigen = decimal.Parse(txtDistRecoOr.Text),
                        // TipoEstacionOrigen = txtTipoEstOr.Text.Substring(0, 2),
                        // DistanciaRecorridaDestino = decimal.Parse(txtDistRecoDes.Text),
                        // TipoEstacionDestino = txtTipoEstDes.Text.Substring(0, 2),
                        Origen = myDataOrigen,
                        DomicilioOri = myDomOri
                        // DomicilioDest = null,
                        // Destino = null,

                    });
                }



                if (getPerson.strTipodeInscripcion == "Destino")
                {
                    dataCartaPorte.DomicilioDestino myDomDest = new dataCartaPorte.DomicilioDestino();
                    myDomDest.Pais = "MEX";
                    myDomDest.NumeroExterior = getDireccion.strNoExterior;
                    myDomDest.Municipio = Municipio; //cmbMunicipioDes.Text;
                    myDomDest.Localidad = Localidad;//cmbLocDes.Text;
                    myDomDest.Estado = strEdo; ;
                    myDomDest.Colonia = Colonia;//cmbColDes.Text;
                    myDomDest.CodigoPostal = getDireccion.strCodigoPostal;
                    myDomDest.Calle = getDireccion.strCalle;
                    myDomDest.Referencia = "";

                    dataCartaPorte.Destino myDataDestino = new dataCartaPorte.Destino();
                    myDataDestino.IDUbicacion = concepto.idDestino;
                    myDataDestino.TipoUbicacion = "Destino";
                    myDataDestino.RFCRemitenteDestinatario = getPerson.strRFC;
                    myDataDestino.NombreRemitenteDestinatario = getPerson.strRazonSocial;
                    myDataDestino.DistanciaRecorrida = concepto.dcmDistancia;
                    // myDataDestino.NumRegIdTrib = txtIdRegi.Text;
                    // myDataDestino.ResidenciaFiscal = "MEX";
                    myDataDestino.FechaHoraSalidaLlegada = DateTime.Parse(concepto.dtmFecha);

                    myUbicacionesDes.Add(new dataCartaPorte.UbicacionDestino
                    {
                        // DistanciaRecorridaOrigen = decimal.Parse(txtDistRecoOr.Text),
                        //  TipoEstacionOrigen = txtTipoEstOr.Text.Substring(0, 2),
                        //  DistanciaRecorridaDestino = decimal.Parse(txtDistRecoDes.Text),
                        //   TipoEstacionDestino = txtTipoEstDes.Text.Substring(0, 2),

                        Destino = myDataDestino,
                        DomicilioDest = myDomDest


                    });
                }


                if (getPerson.strTipodeInscripcion.Contains("01-Operador") || getPerson.strTipodeInscripcion.Contains("02-Propietario") || getPerson.strTipodeInscripcion.Contains("03-Arrendador") || getPerson.strTipodeInscripcion.Contains("04-Notificado"))
                {
                    myTiposFigura.Add(
                    new dataCartaPorte.TiposFigura
                    {
                        RFCFigura = getPerson.strRFC,
                        NumLicencia = getPerson.strTelefonoMovil,
                        TipoFigura = cmbTipoFigura.Text.ToString().Split('-')[0],
                        NombreFigura = getPerson.strRazonSocial
                    });
                }}

                if (concepto.strTipo == "Remolque") {

                    myRemolque.Add(new dataCartaPorte.Remolque
                    {


                        Placa = concepto.strRFC,
                        SubTipoRem = concepto.strRazonSocial


                    });
                
                
                }
            
            
            }






            myRoot.UbicacionOr = myUbicacionesOr;
            myRoot.UbicacionDes = myUbicacionesDes;
           
            dataCartaPorte.Mercancancias myMercancias = new dataCartaPorte.Mercancancias();

            List<dataCartaPorte.Mercancia> myMercancia = new List<dataCartaPorte.Mercancia>();
            
                dataCartaPorte.CantidadTransporta myCantidadTransporta = new dataCartaPorte.CantidadTransporta();


            decimal dcmPesoNetoTotal = 0;
            int dcmTotalMercancia = 0;
            string strUnidadMedida = "";

            foreach (var item in dtgConceptos.Items)
            {

                CarritoComprasEntry concepto = item as CarritoComprasEntry;


                
                if (concepto.isMercancia == "1")
                {

                    myCantidadTransporta.Cantidad = concepto.Cantidad;
                    myCantidadTransporta.IDOrigen = concepto.IdOrigen;
                    myCantidadTransporta.IDDestino = concepto.IdDestino;

                    dcmTotalMercancia++;
                    strUnidadMedida = concepto.Unidad;
                    myMercancia.Add(new dataCartaPorte.Mercancia()
                    {

                        Cantidad = concepto.Cantidad,
                        ClaveUnidad = concepto.Unidad,
                        Descripcion = concepto.Nombre,
                        BienesTransp = concepto.claveSat.ToString(),
                        //  ValorMercancia = decimal.Parse(concepto.Importe.ToString()),
                        PesoEnKg = concepto.pesoKg,
                        CantidadTransporta = myCantidadTransporta,
                        MaterialPeligroso = concepto.strMP,
                        ClaveMatPel = concepto.strClvMP,
                        Embalaje = concepto.Embalaje
                        /// Moneda = "MXN"
                        //Importe = concepto.Importe.ToString()
                        //Importe = concepto.FormatImporte.ToString()
                    });
                }

                dcmPesoNetoTotal += concepto.pesoKg;

            }

                

            myMercancias.NumTotalMercancias = dcmTotalMercancia; //int.Parse(dcmTotalMercanica.ToString());
            myMercancias.UnidadPeso = cmbUnidadPeso.Text; //strUnidadMedida.ToString.Split('-')[0];  //"XBX";
            myMercancias.PesoBrutoTotal = dcmPesoNetoTotal; //decimal.Parse(txtPesoNetoTotal.Text);
            myMercancias.Mercancia = myMercancia;
            myRoot.Mecancancias = myMercancias;



            dataCartaPorte.Autotransporte myAutoFede = new dataCartaPorte.Autotransporte();
            dataCartaPorte.IdentificacionVehicular myIdentificiacionVeicular = new dataCartaPorte.IdentificacionVehicular();
            dataCartaPorte.Seguros mySeguro = new dataCartaPorte.Seguros();
            
            myAutoFede.PermSCT = txtPermisoSCT.Text;
            myAutoFede.NumPermisoSCT = txtNoPermSCT.Text;

            string strConfVehi = "";
            var idConfVehi = cmbConfVehi.SelectedValue;

            if (idConfVehi != null)
            {
                strConfVehi = idConfVehi.ToString();
            }

            myIdentificiacionVeicular.PlacaVM = atcVehiculoTracto.Text;
            myIdentificiacionVeicular.ConfigVehicular = strConfVehi;
            myIdentificiacionVeicular.AnioModeloVM = int.Parse(txtAnoModel.Text);

            mySeguro.AseguraCarga = txtAseguraCarga.Text;
            mySeguro.AseguraRespCivil = txtAeguraRespCivil.Text;
            mySeguro.PolizaRespCivil = txtPolRespCivil.Text;

            
            //myAutoFede.NumPolizaSeguro = txtNumPolSeg.Text;
           
            //myAutoFede.NombreAseg = txtNoAsegu.Text;
            myAutoFede.IdentificacionVehicular = myIdentificiacionVeicular;
            myAutoFede.Seguros = mySeguro;
            myAutoFede.Remolques = myRemolque;


            myRoot.Autotransporte = myAutoFede;

           // List<dataCartaPorte.TiposFigura> myFig = new List<dataCartaPorte.TiposFigura>();
          dataCartaPorte.FiguraTransporte myFigTransp = new dataCartaPorte.FiguraTransporte();



               
             

            //myFig.Add(new dataCartaPorte.TiposFigura
            //{
            //    RFCFigura = txtRFCOp.Text,
            //    NumLicencia = txtLicOp.Text,
            //    TipoFigura = cmbTipoFigura.Text.ToString().Split('-')[0],
            //    NombreFigura = txtNombreOp.Text



            //});



           myFigTransp.TiposFigura = myTiposFigura;
            
         //   myFigTransp.CveTransporte = "01";
          //  myFigTransp.Operadores = myOperadores;

           myRoot.FiguraTransporte = myFigTransp;

            jsonString = JsonConvert.SerializeObject(myRoot);
            jsonGrid = JsonConvert.SerializeObject(myDataGrid);

            Console.WriteLine("json " + jsonString);
           
            }catch(Exception ex){

                //Console.WriteLine("error crear json "+ex);
                //throw new InvalidOperationException("err/or json " + ex);
                MessageBox.Show("error json " + ex);
            
            }
        
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarDatos()) 
            {
                if (!IsEditMode)
                {
                    try
                    {

                        if (chbAddCCP.IsChecked==true)
                        {
                            fillJson();
                        }

                        if (chbAddFactoraje.IsChecked == true)
                        {

                            jsonString = "Factoraje|" + urlXMLfactoraje + "|" + urlPDFfactoraje;
                        }

                       // if (isAddenda) getConceptosAddenda();

                        CFDModels cfd = new CFDModels();
                        Models.Empresa emisor;
                        Models.Clientes receptor;
                        Direcciones_Fiscales direccionEmisor;
                        Direcciones_Fiscales direccionReceptor;
                        GetEncabezadoCFD(out emisor, out receptor, out direccionEmisor, out direccionReceptor);
                        dlleFac.ComprobanteFiscalDigital co = new dlleFac.ComprobanteFiscalDigital();
                        List<dlleFac.Concepto> conceptosDll = new List<dlleFac.Concepto>();
                        List<ConceptoPreFactura> conceptosPrefactura = new List<ConceptoPreFactura>();


                        var direccion = direccionEmisor;

                        txtOrigen.Text = direccion.strCalle + whiteSpace 
                            + direccion.strNoInterior + whiteSpace + direccion.strNoExterior + whiteSpace
                            + direccion.strColonia + whiteSpace + direccion.strCodigoPostal + whiteSpace +
                            whiteSpace + direccion.strMunicipio + whiteSpace + direccion.Estado.strNombreEstado +
                            whiteSpace + direccion.Paises.strNombrePais;

                        RetriveComprobanteFiscalDigital(this.intTipoComprobante, cfd, emisor, receptor, direccionEmisor, direccionReceptor, co, conceptosDll, conceptosPrefactura);

                        string subtotal = "";
                        string descuento = "";
                        string iva = "";
                        string total = "";
                        string retIva = "";
                        string retIsr = "";
                        string retIeps = "";

                            descuento = txtDescuento.Text;
                            
                            subtotal = txtSubtotal.Text;
                        
                            iva = txtIva.Text;
                            total = txtGranTotal.Text;

                            retIva = txtRetIva.Text;
                            retIsr = txtRetIsr.Text;
                            retIeps = txtRetIeps.Text;

                        
                            
                        

                        if (cfd.SaveFactura(this.intTipoComprobante,
                            emisor.intID,
                            co.Serie,
                            co.Folio,
                            DateTime.Parse(co.Fecha.Replace("T", " ")),
                            Convert.ToInt32(App.Current.Properties["idUsuario"]),
                            receptor.intID,
                           // co.FormaPago,
                            cmbFormaPago.Text.Substring(0, 2),
                            txtEncabezado.Text,
                            decimal.Parse(subtotal),
                            decimal.Parse(descuento),
                            decimal.Parse(iva),
                            decimal.Parse(total),
                            jsonGrid,//txtObservaciones.Text,
                            "PENDIENTE",//txtImpuestosAdicionales.Text,
                            uuidSave,
                            cmbTipoRelacion.Text.Substring(0,2),                            
                            emisor.CFD.First().intID,
                            jsonString,
                            conceptosPrefactura,
                            decimal.Parse(retIva),
                            decimal.Parse(retIsr),
                            decimal.Parse(retIeps),
                            txtCondicionesPago.Text,
                            cmbMetodoPago.Text.Substring(0, 3),
                            cmbUsoCfdi.Text.Substring(0, 3),
                            txtDivisa.Text,
                            decimal.Parse(txtTipoCambio.Text),
                            txtOrigen.Text,//txtProveeedor.Text,
                            txtRecogerEn.Text,//txtPedido.Text,
                            cmbPeriocidad.Text + "/" + txtMes.Text + "/" + txtAno.Text,//txtCC.Text,
                            txtDestinatario.Text,
                            txtRfcDestinatario.Text,
                            txtDomicilioDestinatario.Text,
                            txtEntregarEn.Text, null,
                            urlXMLfactoraje
                            
                            ))
                        {
                            
                            MessageBox.Show("Guardado con exito", "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.NavigationService.Navigate(new DefaultPuntoVenta());
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
                else
                {
                    try
                    {
                        if (chbAddCCP.IsChecked == true)
                        {
                            fillJson();
                        }
                       

                        CFDModels cfd = new CFDModels();
                        Models.Empresa emisor;
                        Models.Clientes receptor;
                        Direcciones_Fiscales direccionEmisor;
                        Direcciones_Fiscales direccionReceptor;
                        GetEncabezadoCFD(out emisor, out receptor, out direccionEmisor, out direccionReceptor);
                        dlleFac.ComprobanteFiscalDigital co = new dlleFac.ComprobanteFiscalDigital();
                        List<dlleFac.Concepto> conceptosDll = new List<dlleFac.Concepto>();
                        List<ConceptoPreFactura> conceptosPrefactura = new List<ConceptoPreFactura>();
                       
                        RetriveComprobanteFiscalDigital(this.intTipoComprobante, cfd, emisor, receptor, direccionEmisor, direccionReceptor, co, conceptosDll, conceptosPrefactura);
                        var direccion = direccionEmisor;

                        string subtotal = "";
                        string descuento = "";
                        string iva = "";
                        string total = "";
                        string retIva = "";
                        string retIsr = "";
                        string retIeps = "";

                        descuento = txtDescuento.Text;

                        subtotal = txtSubtotal.Text;

                        iva = txtIva.Text;
                        total = txtGranTotal.Text;

                        retIva = txtRetIva.Text;
                        retIsr = txtRetIsr.Text;
                        retIeps = txtRetIeps.Text;

                        txtOrigen.Text = direccion.strCalle + whiteSpace
                            + direccion.strNoInterior + whiteSpace + direccion.strNoExterior + whiteSpace
                            + direccion.strColonia + whiteSpace + direccion.strCodigoPostal + whiteSpace +
                            whiteSpace + direccion.strMunicipio + whiteSpace + direccion.Estado.strNombreEstado +
                            whiteSpace + direccion.Paises.strNombrePais;
                        

                        

                        if (cfd.UpdateFactura(intID, this.intTipoComprobante,
                            emisor.intID,
                            co.Serie,
                            co.Folio,
                            DateTime.Parse(co.Fecha.Replace("T", " ")),
                            Convert.ToInt32(App.Current.Properties["idUsuario"]),
                            receptor.intID,
                            //co.FormaPago,
                            cmbFormaPago.Text.Substring(0, 2),
                            txtEncabezado.Text,
                            decimal.Parse(subtotal),
                            decimal.Parse(descuento),
                            decimal.Parse(iva),
                            decimal.Parse(total),
                            jsonGrid,//txtObservaciones.Text,
                            "PENDIENTE",//txtImpuestosAdicionales.Text,
                            uuidSave,
                            cmbTipoRelacion.Text.Substring(0, 2),
                            emisor.CFD.First().intID,
                            jsonString,
                            conceptosPrefactura,
                            decimal.Parse(retIva),
                            decimal.Parse(retIsr),
                            decimal.Parse(retIeps),
                            txtCondicionesPago.Text,
                            cmbMetodoPago.Text.Substring(0, 3),
                            cmbUsoCfdi.Text.Substring(0,3),
                            txtDivisa.Text,
                            decimal.Parse(txtTipoCambio.Text),
                            txtOrigen.Text,//txtProveeedor.Text,
                            txtPedido.Text,
                            cmbPeriocidad.Text + "/" + txtMes.Text + "/" + txtAno.Text,
                            txtDestinatario.Text,
                            txtRfcDestinatario.Text,
                            txtDomicilioDestinatario.Text,
                            txtEntregarEn.Text, null
                            
                            
                            
                           ))
                        {
                            MessageBox.Show("Guardado con exito", "Guardar", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.NavigationService.Navigate(new DefaultPuntoVenta());
                        }
                        else
                        {
                            MessageBox.Show("Lo sentimos ocurrio un error intentelo de nuevo", "Guardar",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception exp)
                    {

                    }
                }
            }
        }


        List<AddendaEntry> myAddenda = new List<AddendaEntry>();


        //private void getAddendaFactura(int idFactura)
        //{

        //    eFacDBEntities db = new eFacDBEntities();
        //    List<Addendas> myDicAddenda = db.Addendas.Where(a => a.idFactura == idFactura).ToList();

        //    List<AddendaEntry> myListAddenda = new List<AddendaEntry>();


        //    foreach (Addendas item in myDicAddenda)
        //    {

        //        string tipoComprobante="1";

        //        if (intTipoComprobante == 2)

        //            tipoComprobante = "8";
        //        if (item.idPos == 1)
        //        {
        //            myListAddenda.Add(new AddendaEntry()
        //            {


        //                idAddenda = item.idAddenda,
        //                idPos = item.idPos,
        //                Descripcion = tipoComprobante,
        //                Default = item.Default

        //            });

        //        }

        //        else
        //        {
        //            myListAddenda.Add(new AddendaEntry()
        //            {


        //                idAddenda = item.idAddenda,
        //                idPos = item.idPos,
        //                Descripcion = item.Descripcion,
        //                Default = item.Default

        //            });
        //        }
        //    }

        //    //dtgAddenda.ItemsSource = myListAddenda;
        //}

        //private void getAddendaDefault(int idAddenda)
        //{

        //    eFacDBEntities db = new eFacDBEntities();
        //    List<DicAddenda> myDicAddenda = db.DicAddenda.Where(a => a.idAddenda == idAddenda).ToList();

        //    List<AddendaEntry> myListAddenda = new List<AddendaEntry>();

        //    foreach (DicAddenda item in myDicAddenda)
        //    {
        //        string tipoComprobante = "1";

        //        if (intTipoComprobante == 2)

        //            tipoComprobante = "9";

        //        if (intTipoComprobante == 3)

        //            tipoComprobante = "8";

        //        if (item.idPos == 1)
        //        {
        //            myListAddenda.Add(new AddendaEntry()
        //            {


        //                idAddenda = item.idAddenda,
        //                idPos = item.idPos,
        //                Descripcion = item.Descripcion,
        //                Default = tipoComprobante//item.Default

        //            });

        //        }

        //        else
        //        {
        //            myListAddenda.Add(new AddendaEntry()
        //            {


        //                idAddenda = item.idAddenda,
        //                idPos = item.idPos,
        //                Descripcion = item.Descripcion,
        //                Default = item.Default

        //            });
        //        }
        //    }

        //  //  dtgAddenda.ItemsSource = myListAddenda;
        //}

        //private void getConceptosAddenda()
        //{

        //    foreach (var item in dtgAddenda.Items)
        //    {
        //        AddendaEntry concepto = item as AddendaEntry;
        //        if (concepto != null)
        //        {
        //            myAddenda.Add(new AddendaEntry()
        //            {
        //                idAddenda = concepto.idAddenda,
        //                idPos = concepto.idPos,
        //                Descripcion = concepto.Descripcion,
        //                Default = concepto.Default
        //            });
        //        }


        //    }
        //}


        private void RetriveComprobanteFiscalDigital(int intTipoComprobante, CFDModels cfd, Models.Empresa emisor, Models.Clientes receptor, Direcciones_Fiscales direccionEmisor, Direcciones_Fiscales direccionReceptor, dlleFac.ComprobanteFiscalDigital co, List<dlleFac.Concepto> conceptos, List<ConceptoPreFactura> conceptoPrefactura)
        {
            GetConceptosFromGrid(conceptos, conceptoPrefactura);

            //dlleFac.Impuestos impuestos;
            dlleFac.Emisor emisorCFD;
            dlleFac.DomicilioFiscal domicilioEmisor;
            dlleFac.DomicilioFiscal expedidoEn;
            dlleFac.Receptor receptorCFD;
            dlleFac.DomicilioFiscal domicilioFiscalReceptor;

            GetValuesCFD(cfd, emisor, receptor, direccionEmisor, direccionReceptor, co, conceptos, out emisorCFD, out domicilioEmisor, out expedidoEn, out receptorCFD, out domicilioFiscalReceptor);

            //string emisorNumeroAprovacion = emisor.Folios.SingleOrDefault(f => f.chrStatus == "A" && f.intIDtipoCFD == intTipoComprobante).intNumero_Aprovacion.ToString();
            string emisorNumeroAprovacion = emisor.CFD.SingleOrDefault(f => f.intID == intTipoComprobante).Folios.intNumero_Aprovacion.ToString();
            //string emisorAñoAprobacion = emisor.Folios.SingleOrDefault(f => f.chrStatus == "A" && f.intIDtipoCFD == intTipoComprobante).strAño_Aprovacion.ToString();
            string emisorAñoAprobacion = emisor.CFD.SingleOrDefault(f => f.intID == intTipoComprobante).Folios.strAño_Aprovacion.ToString();
            
            cfd.FillComprobanteFiscal(
                co,
                "3.0",
                txtSerie.Text,
                txtFolio.Text,
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                emisorNumeroAprovacion,
                emisorAñoAprobacion, // No HardCode
                emisor.CFD.First().strTipoCFD,
                cmbFormaPago.Text,
                decimal.Parse(txtSubtotal.Text).ToString(),
                decimal.Parse(txtIva.Text).ToString(),
                co.Total = decimal.Parse(txtTotal.Text).ToString(),
                emisorCFD,
                domicilioEmisor,
                expedidoEn,
                receptorCFD,
                domicilioFiscalReceptor,
                conceptos
                
                );
        }

        private void GetValuesCFD(CFDModels cfd, Models.Empresa emisor, Models.Clientes receptor, Direcciones_Fiscales direccionEmisor,
            Direcciones_Fiscales direccionReceptor, dlleFac.ComprobanteFiscalDigital co, List<dlleFac.Concepto> conceptos,
            out dlleFac.Emisor emisorCFD, out dlleFac.DomicilioFiscal domicilioEmisor, 
            out dlleFac.DomicilioFiscal expedidoEn, out dlleFac.Receptor receptorCFD, out dlleFac.DomicilioFiscal domicilioFiscalReceptor)
        {
            //impuestos = new dlleFac.Impuestos();
            
            //impuestos.Traslados = new List<dlleFac.Traslado>();
            
            //impuestos.Retenciones = new List<dlleFac.Retencion>();
            
            
            //decimal totalTraslado = decimal.Parse(txtIVA_Subtotal.Text);
            
            
            //impuestos.TotalTraslados = totalTraslado.ToString();

            //impuestos.TotalRetenido = (decimal.Parse(txtRetIsr.Text.ToString()) + decimal.Parse(txtTotal_RetIva.Text.ToString())).ToString();
            
            decimal tasa = 0;
            decimal importeTotal = 0;

            foreach (var item in dtgConceptos.Items)
            {
                CarritoComprasEntry concepto = item as CarritoComprasEntry;
                decimal conceptoIVA = decimal.Parse(concepto.IVA.ToString());
                decimal importeTraslado = (concepto.Importe * (concepto.IVA / 100));
                decimal totalImpuestoTraslado = decimal.Parse(txtIva.Text);
                tasa = decimal.Parse(concepto.IVA.ToString());
                importeTotal += importeTraslado;
            }

            /*
            dlleFac.Traslado tr = new dlleFac.Traslado();
            tr.TipoImpuesto = "IVA";
            tr.Tasa = tasa.ToString("F");
            tr.Importe = importeTotal.ToString("F");
            tr.TotalImpuestosTraslados = totalTraslado.ToString("F");


            dlleFac.Retencion retIVA = new dlleFac.Retencion();
            retIVA.TipoImpuesto = "IVA";
            retIVA.Importe = txtTotal_RetIva.Text.ToString();


            dlleFac.Retencion retISR = new dlleFac.Retencion();
            retISR.TipoImpuesto = "ISR";
            retISR.Importe = txtRetIsr.Text.ToString();
            

            co.Impuestos = impuestos;

            co.Impuestos.Traslados.Add(tr);
            co.Impuestos.Retenciones.Add(retIVA);
            co.Impuestos.Retenciones.Add(retISR);
            */
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


            cfd.FillComprobanteFiscal(co, "3.0", txtSerie.Text,
                txtFolio.Text,
                DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss"),
                emisor.Folios.First(f => f.chrStatus == "A").intNumero_Aprovacion.ToString(),
                emisor.Folios.First(f => f.chrStatus == "A").strAño_Aprovacion.ToString(), // No HardCode
                emisor.CFD.First().strTipoCFD,
                cmbFormaPago.Text,
                decimal.Parse(txtSubtotal.Text).ToString(),
                decimal.Parse(txtIva.Text).ToString(),
                co.Total = decimal.Parse(txtTotal.Text).ToString(),
                emisorCFD,
                domicilioEmisor,
                expedidoEn,
                receptorCFD,
                domicilioFiscalReceptor,
                conceptos
                );
        }

        private void GetEncabezadoCFD(out Models.Empresa emisor, out Models.Clientes receptor, out Direcciones_Fiscales direccionEmisor, out Direcciones_Fiscales direccionReceptor)
        {
            emisor = pfvm.GetEmpresa(Convert.ToInt32(App.Current.Properties["idUsuario"]));
          //  emisor = (wpfEFac.Models.Clientes)atcNombreCliente1.SelectedItem;
            receptor = (wpfEFac.Models.Clientes)atcNombreCliente.SelectedItem;
            direccionEmisor = pfvm.GetDireccionEmpresa(emisor.intID);
            direccionReceptor = pfvm.GetDireccionCliente(receptor.intID);
        }

        private void GetConceptosFromGrid(List<dlleFac.Concepto> conceptos, List<ConceptoPreFactura> conceptosPrefactura)
        {
            foreach (var item in dtgConceptos.Items)
            {
                
                CarritoComprasEntry concepto = item as CarritoComprasEntry;

                

                if (concepto.isMercancia == "0")
                {
                    conceptos.Add(new dlleFac.Concepto()
                    {
                        Cantidad = concepto.Cantidad.ToString(),
                        UnidadMedida = concepto.Unidad,
                        NoIdentificacion = concepto.Codigo,
                        Descripcion = concepto.Nombre,
                        Descuento = concepto.Descuento.ToString(),
                        ValorUnitario = concepto.PrecioUnitario.ToString(),
                        //Importe = concepto.Importe.ToString()
                        Importe = concepto.FormatImporte.ToString()
                    });

                    ConceptoPreFactura conceptoPrefactura =
                                    new ConceptoPreFactura();

                    conceptoPrefactura.intIdProducto = concepto.intID;
                    conceptoPrefactura.intCantidad = concepto.Cantidad;
                    conceptoPrefactura.dcmDescuento = decimal.Parse(concepto.Descuento.ToString().Replace("%", string.Empty).Trim());
                    conceptoPrefactura.dcmImporte = decimal.Parse(concepto.FormatImporte);
                    conceptoPrefactura.strUnidad = concepto.Unidad;
                    conceptoPrefactura.strConcepto = concepto.Nombre;
                    conceptoPrefactura.dcmPrecioUnitario = concepto.PrecioUnitario;
                    conceptoPrefactura.dcmIVA = concepto.IVA;
                    conceptoPrefactura.dcmRetIVA = concepto.retIVA;
                    conceptoPrefactura.dcmRetISR = concepto.retISR;
                    conceptoPrefactura.dcmRetIEPS = concepto.retIEPS;

                    conceptoPrefactura.strPartida = concepto.isMercancia; //string.Empty;

                    conceptosPrefactura.Add(conceptoPrefactura);
                }
            }
        }

        private bool ValidarDatos()
        {
            bool cliente = ValidarCliente();
            bool concepto = ValidarConceptos();
            return (cliente && concepto);

        }

        private bool ValidarConceptos()
        {
            if (dtgConceptos.Items.Count > 0) 
            {
                dtgConceptos.BorderBrush = new DataGrid().BorderBrush;
                return true;
            }
            else
            {
                MessageBox.Show("No hay ningun concepto");
                dtgConceptos.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
        }

        private bool ValidarCliente()
        {
            if (atcNombreCliente.SelectedItem != null)
	        {
                atcNombreCliente.BorderBrush = new SolidColorBrush(Colors.Gray);
                return true;
	        }
            else
            {
                MessageBox.Show("No selecciono un cliente");
                atcNombreCliente.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            
        }

        private bool ValidarUbicacion()
        {
            if (atcUbicaciones.SelectedItem != null)
            {
                atcUbicaciones.BorderBrush = new SolidColorBrush(Colors.Gray);
                return true;
            }
            else
            {
                MessageBox.Show("No selecciono un cliente");
                atcUbicaciones.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }

        }

        private List<Detalle_Factura> GetConceptosReporte(ItemCollection itemCollection)
        {
            List<Detalle_Factura> result = new List<Detalle_Factura>();

            foreach (var item in itemCollection)
            {
                CarritoComprasEntry concepto = item as CarritoComprasEntry;
                //Thest this should change to the data base

                result.Add(new Detalle_Factura()
                {
                    dcmCantidad = concepto.Cantidad,
                    dcmDescuento = decimal.Parse(concepto.Descuento.ToString().Replace(" %", string.Empty)),
                    dcmImporte = concepto.Importe,
                    strUnidad = concepto.Unidad,
                    strConcepto = concepto.Nombre,
                    dcmPrecioUnitario = concepto.PrecioUnitario,
                    dcmIVA = concepto.IVA
                });
            }

            return result;
        }

        private List<string> GetAttach()
        {
            return new List<string>()
            {
                //"Muestra.pdf",//This has to change to the files generated by the app. 
                //"Muestra.xml"

            };
        }

        private List<string> GetRecivers(ConfiguracionEmail configuracionEmail, string emailCliente)
        {
            return new List<string>()
            {
                emailCliente,
                configuracionEmail.strE_MailContador,
                configuracionEmail.strE_MailRespaldo,
                configuracionEmail.Empresa.strEmail
            };
        }

        private void atcRFC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (atcRFC.SelectedItem != null)
            {
                atcNombreCliente.SelectedItem = atcRFC.SelectedItem;
                
                txtDestinatario.Text = atcNombreCliente.Text;

                ShowDireccionCliente(((wpfEFac.Models.Clientes)(atcRFC.SelectedItem)).intID);
            }
        }

        private void atcNombreCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidarCliente();
            if (atcNombreCliente.SelectedItem != null)
            {
                wpfEFac.Models.Clientes myc = ((wpfEFac.Models.Clientes)atcNombreCliente.SelectedItem);
                
                this.strRetencionIva = myc.chrRetencionIVA;
                this.strRetencionIsr = myc.chrRetencionISR;

                if (myc.strRFC == "XAXX010101000")
                {

                    txtPeriocidad.Visibility = System.Windows.Visibility.Visible;
                    cmbPeriocidad.Visibility = System.Windows.Visibility.Visible;
                    txbMes.Visibility = System.Windows.Visibility.Visible;
                    txtMes.Visibility = System.Windows.Visibility.Visible;
                    txbAno.Visibility = System.Windows.Visibility.Visible;
                    txtAno.Visibility = System.Windows.Visibility.Visible;
                    if (!IsEditMode)
                    {
                        txtMes.Text = DateTime.Now.Month.ToString("00");
                        txtAno.Text = DateTime.Now.Year.ToString();
                    }
                    
                }
                else {

                    txtPeriocidad.Visibility = System.Windows.Visibility.Collapsed;
                    cmbPeriocidad.Visibility = System.Windows.Visibility.Collapsed;
                    txbMes.Visibility = System.Windows.Visibility.Collapsed;
                    txtMes.Visibility = System.Windows.Visibility.Collapsed;
                    txbAno.Visibility = System.Windows.Visibility.Collapsed;
                    txtAno.Visibility = System.Windows.Visibility.Collapsed;
                
                }

                ShowAdds();

                atcRFC.SelectedItem = atcNombreCliente.SelectedItem;
                ShowDireccionCliente(((wpfEFac.Models.Clientes)atcNombreCliente.SelectedItem).intID);
            }
        }

        private void ShowDireccionCliente(int idCliente)
        {
            var direccion = pfvm.GetDireccionCliente(idCliente);

            if (direccion.Estado!= null)
            {
                
                txtDomicilioCliente.Text = direccion.strCalle + whiteSpace  
                + direccion.strNoInterior + whiteSpace + direccion.strNoExterior + whiteSpace
                +  direccion.strColonia + whiteSpace + direccion.strPoblacionLocalidad +
                 whiteSpace + direccion.strMunicipio + whiteSpace +
                direccion.Estado.strNombreEstado + whiteSpace +"C.P."+ direccion.strCodigoPostal +
                whiteSpace + direccion.Paises.strNombrePais;

                txtDomicilioDestinatario.Text = txtDomicilioCliente.Text;
            }

        }

        private void btnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            BuscarClienteWindow buscarCliente = new BuscarClienteWindow();
            if (buscarCliente.ShowDialog().Value) 
            {
                atcRFC.Text = buscarCliente.RFCCliente;
                txtRfcDestinatario.Text = atcRFC.Text;

                atcRFC.Text = buscarCliente.RFCCliente;

                /*
                int idAddenda = ((wpfEFac.Models.Clientes)atcNombreCliente.SelectedItem).idAddenda.Value;
                if (idAddenda > 0)
                {
                    isAddenda = true;
                    getAddendaDefault(idAddenda);
                    xpnDetalleFactura.Width = 550;
                    xpnAddenda.Visibility = System.Windows.Visibility.Visible;
                    xpnAddenda.Width = 380;

                }
                else
                {
                    isAddenda = false;
                    //dtgAddenda.Items.Clear();
                    xpnDetalleFactura.Width = 900;
                    xpnAddenda.Width = 0;
                    xpnAddenda.Visibility = System.Windows.Visibility.Hidden;

                }*/
            }
        }


        private void EditarEntry_Click(object sender, RoutedEventArgs e)
        {
            if (dtgConceptos.SelectedItem != null)
            {
                AddProductoWindow carrito =
                    new AddProductoWindow((CarritoComprasEntry)dtgConceptos.SelectedItem);
                carrito.Show();
            }
        }

        private void BorrarEntry_Click(object sender, RoutedEventArgs e)
        {
            if (dtgConceptos.SelectedItem!=null)
            {
                conceptos.Remove((CarritoComprasEntry)dtgConceptos.SelectedItem);
                CalcularTotal();
            }
        }

        private void atcNombreCliente_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void atcRFC_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBuscarDestino_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void btnSaveOperdor_Click(object sender, RoutedEventArgs e)
        {

            try
            {


                myTiposFigura.Add(
                new dataCartaPorte.TiposFigura
                {
                    RFCFigura = txtRFCOp.Text,
                    NumLicencia = txtLicOp.Text,
                    TipoFigura = cmbTipoFigura.Text
                });
                MessageBox.Show("Operador agregado con éxito!");

                txtAddOp.Text = "Operador agregado "+myTiposFigura.Count.ToString();
            }
            catch (Exception ex) { 
            
            }
        }

 

        private void btnSaveUbicacionOr_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //var idEstadoOr = cmbEstadoOr.SelectedValue;

             //   string value = idEstadoOr.ToString();

                var idLocalidadOr = cmbLocOr.SelectedValue;

                //string.IsNullOrEmpty(f.Clientes.strWebSite) ? "NO IDENTIFICADO" : f.Clientes.strWebSite;
                string value2 = string.IsNullOrEmpty(cmbLocOr.Text) ? "" : idLocalidadOr.ToString();

                var idMunicipioOr = cmbMunicipioOr.SelectedValue;

                string value3 = string.IsNullOrEmpty(cmbMunicipioOr.Text) ? "" : idMunicipioOr.ToString();

                var idColoniaOr = cmbColOr.SelectedValue;

                string value4 = string.IsNullOrEmpty(cmbColOr.Text) ? "" : idColoniaOr.ToString(); 


                dataCartaPorte.Origen myDataOrigen = new dataCartaPorte.Origen();

                myDataOrigen.IDUbicacion = txtIdOrigen.Text;
                myDataOrigen.RFCRemitenteDestinatario = txtRfcRemitente.Text;
                myDataOrigen.TipoUbicacion = "Origen";
                myDataOrigen.FechaHoraSalidaLlegada = DateTime.Parse(dtpFechaSal_lleg.ToString());

                dataCartaPorte.DomicilioOrigen myDomOri = new dataCartaPorte.DomicilioOrigen();
                myDomOri.Pais = cmbPaisOr.Text;
                myDomOri.NumeroExterior = txtNoExO.Text;
                myDomOri.Municipio = value3;//cmbMunicipioOr.Text;
                myDomOri.Localidad = value2;//cmbLocOr.Text;
                myDomOri.Estado = cmbEstadoOr.Text.ToString().Split('-')[0];;
                myDomOri.Colonia = value4;
                myDomOri.CodigoPostal = txtCpOr.Text;
                myDomOri.Calle = txtCalleOr.Text;

               
                myUbicacionesOr.Add(new dataCartaPorte.UbicacionOrigen
                {
                   // DistanciaRecorridaOrigen = decimal.Parse(txtDistRecoOr.Text),
                   // TipoEstacionOrigen = txtTipoEstOr.Text.Substring(0, 2),
                   // DistanciaRecorridaDestino = decimal.Parse(txtDistRecoDes.Text),
                   // TipoEstacionDestino = txtTipoEstDes.Text.Substring(0, 2),
                    Origen = myDataOrigen,
                    DomicilioOri = myDomOri
                   // DomicilioDest = null,
                   // Destino = null,

                });

                MessageBox.Show("Ubicacion origen agregada con éxito!");
                txtAddOr.Text = "Origen agregado "+ myUbicacionesOr.Count.ToString();
            }
            catch (Exception ex)
            {


            }
        }

        private void btnSaveUbicacionDes_Click(object sender, RoutedEventArgs e)
        {
           // var idEstadoDes = cmbEstadoDes.SelectedValue;

          //  string value = idEstadoDes.ToString();

            var idLocalidadDes = cmbLocDes.SelectedValue;

            string value2 = string.IsNullOrEmpty(cmbLocDes.Text) ? "" : idLocalidadDes.ToString();

            var idMunicipioDes = cmbMunicipioDes.SelectedValue;

            string value3 = string.IsNullOrEmpty(cmbMunicipioDes.Text) ? "" : idMunicipioDes.ToString();

            var idColoniaDes = cmbColDes.SelectedValue;

            string value4 = string.IsNullOrEmpty(cmbColDes.Text) ? "" : idColoniaDes.ToString();

            dataCartaPorte.DomicilioDestino myDomDest = new dataCartaPorte.DomicilioDestino();
            myDomDest.Pais = cmbPaisDes.Text;
            myDomDest.NumeroExterior = txtNoExD.Text;
            myDomDest.Municipio = value3; //cmbMunicipioDes.Text;
            myDomDest.Localidad = value2;//cmbLocDes.Text;
            myDomDest.Estado = cmbEstadoDes.Text.ToString().Split('-')[0];;
            myDomDest.Colonia = value4;//cmbColDes.Text;
            myDomDest.CodigoPostal = txtCpDes.Text;
            myDomDest.Calle = txtCalleDes.Text;
            myDomDest.Referencia = txtRefDes.Text;

            dataCartaPorte.Destino myDataDestino = new dataCartaPorte.Destino();
            myDataDestino.IDUbicacion = txtIdDestino.Text;
            myDataDestino.TipoUbicacion = "Destino";
            myDataDestino.RFCRemitenteDestinatario = txtRfcDest.Text;
            myDataDestino.DistanciaRecorrida = decimal.Parse(txtDistRecoDes.Text);
           // myDataDestino.NumRegIdTrib = txtIdRegi.Text;
           // myDataDestino.ResidenciaFiscal = "MEX";
            myDataDestino.FechaHoraSalidaLlegada = DateTime.Parse(dtpFechaLlegada.ToString());

            myUbicacionesDes.Add(new dataCartaPorte.UbicacionDestino
            {
               // DistanciaRecorridaOrigen = decimal.Parse(txtDistRecoOr.Text),
              //  TipoEstacionOrigen = txtTipoEstOr.Text.Substring(0, 2),
              //  DistanciaRecorridaDestino = decimal.Parse(txtDistRecoDes.Text),
             //   TipoEstacionDestino = txtTipoEstDes.Text.Substring(0, 2),
            
                Destino = myDataDestino,
                DomicilioDest = myDomDest
                

            });

            MessageBox.Show("Ubicacion destino agregada con éxito!");

            txtAddDes.Text = "Destino agregado "+myUbicacionesDes.Count.ToString();
        }

        private void chbAddCCP_Unchecked(object sender, RoutedEventArgs e)
        {
            xpnAddenda.Visibility = System.Windows.Visibility.Collapsed;
            spEncabezado.Visibility = System.Windows.Visibility.Visible;
            spObservaciones.Visibility = System.Windows.Visibility.Visible;
            spOrigenCCP.Visibility = System.Windows.Visibility.Collapsed;
            spDestinoCCP.Visibility = System.Windows.Visibility.Collapsed;
            xpnAddenda.Visibility = System.Windows.Visibility.Collapsed;
            spPermisos.Visibility = System.Windows.Visibility.Collapsed;
            spVehiculo.Visibility = System.Windows.Visibility.Collapsed;
            txbBusarXML.Visibility = System.Windows.Visibility.Collapsed;
            bttBuscarXML.Visibility = System.Windows.Visibility.Collapsed;
            spUnidad.Visibility = System.Windows.Visibility.Collapsed;
            dtgUbicaciones.Visibility = System.Windows.Visibility.Collapsed;
            spRemolque.Visibility = System.Windows.Visibility.Collapsed;
            txbBusarExcel.Visibility = System.Windows.Visibility.Collapsed;
            bttBuscarExcel.Visibility = System.Windows.Visibility.Collapsed;
        }

        /*
         
          <TextBlock HorizontalAlignment="Center" Visibility="Visible" x:Name="txbBusarExcel" VerticalAlignment="Top" Text="Buscar Excel:" Width="70"/>
                        <Button  VerticalAlignment="Center" Name="bttBuscarExcel" Visibility="Visible" Content="Buscar..." Width="50" Height="30" Click="bttBuscarExcel_Click"/>
                        <TextBlock HorizontalAlignment="Center" Visibility="Visible" x:Name="txbBusarXML" VerticalAlignment="Top" Text="Buscar XML:" Width="70"/>
                        <Button  VerticalAlignment="Center" Name="bttBuscarXML" Visibility="Visible" Content="Buscar..." Width="50" Height="30" Click="bttBuscarXML_Click"/>
                        <TextBlock HorizontalAlignment="Center" Visibility="Visible" x:Name="txbBusarPDF" VerticalAlignment="Top" Text="Buscar PDF:" Width="70"/>
                        <Button  VerticalAlignment="Center" Name="bttBuscarPDF" Visibility="Visible" Content="Buscar..." Width="50" Height="30" Click="bttBuscarPDF_Click"/>
         
         
         
         
         */

        private void chbAddCCP_Checked(object sender, RoutedEventArgs e)
        {
            xpnAddenda.Visibility = System.Windows.Visibility.Visible;
            spEncabezado.Visibility = System.Windows.Visibility.Collapsed;
            spObservaciones.Visibility = System.Windows.Visibility.Collapsed;

          //  spOrigenCCP.Visibility = System.Windows.Visibility.Visible;
          //  spDestinoCCP.Visibility = System.Windows.Visibility.Visible;
            dtgUbicaciones.Visibility = System.Windows.Visibility.Visible;
            xpnAddenda.Visibility = System.Windows.Visibility.Visible;
            spUnidad.Visibility = System.Windows.Visibility.Visible;
            spPermisos.Visibility = System.Windows.Visibility.Visible;
            spVehiculo.Visibility = System.Windows.Visibility.Visible;
            txbBusarXML.Visibility = System.Windows.Visibility.Visible;
            bttBuscarXML.Visibility = System.Windows.Visibility.Visible;
            txbBusarExcel.Visibility = System.Windows.Visibility.Visible;
            bttBuscarExcel.Visibility = System.Windows.Visibility.Visible;
            spRemolque.Visibility = System.Windows.Visibility.Visible;
        }

        private void bttBuscarXML_Click(object sender, RoutedEventArgs e)
        {
           
            try{
                Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                Nullable<bool> result = null;

                if (chbAddFactoraje.IsChecked == true)
                {

                    ofd.FileName = "XML_factoraje";
                    ofd.DefaultExt = ".xml";
                    ofd.Filter = "Documentos XML (.xml)|*.xml";

                    result = ofd.ShowDialog();

                    if (result == true)
                    {


                    }

                    urlXMLfactoraje = ofd.FileName;
                }




               }
            catch(Exception ex){
                MessageBox.Show("Erro cargar archivo " + ex);
            
            }
           
           
        }

        private void bttBuscarPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                Nullable<bool> result = null;

                if (chbAddFactoraje.IsChecked == true)
                {

                    ofd.FileName = "PDF_factoraje";
                    ofd.DefaultExt = ".pdf";
                    ofd.Filter = "Documentos PDF (.pdf)|*.pdf";

                    result = ofd.ShowDialog();

                    if (result == true)
                    {


                    }

                    urlPDFfactoraje = ofd.FileName;
                }
            }
            catch (Exception ex) { }

        }


        public dlleFac.Comprobante DeserializeCFD32(string xmlFile)
        {


            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new
            XmlSerializer(typeof(dlleFac.Comprobante));

            // A FileStream is needed to read the XML document.

            FileStream fs = new FileStream(xmlFile, FileMode.Open);
            XmlReader reader = new XmlTextReader(fs);

            // Declare an object variable of the type to be deserialized.
            dlleFac.Comprobante myComprobante = null;

            // Use the Deserialize method to restore the object's state.
            try
            {

                myComprobante = (dlleFac.Comprobante)serializer.Deserialize(reader);

            }
            catch (Exception e)
            {

            }
            fs.Close();
            reader.Close();

            return myComprobante;

        }

        private void cmbEstadoOr_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void cmbLocOr_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void btnSaveRemolque_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                dtgUbicaciones.Items.Add(new Item()
                {

                    intID = "0",
                    strRFC = atcVehiculoRemolque.Text,
                    strRazonSocial = cmbTipoRemolque.SelectedValue.ToString(),
                    strDescripcion = cmbTipoRemolque.Text,
                    strTipo = "Remolque",
                    dtmFecha ="",
                    idOrigen = "",
                    idDestino = "",
                    dcmDistancia = 0,

                });

                //var idTipo = cmbTipoRemolque.SelectedValue;

                //string value = idTipo.ToString();


                //myRemolque.Add(new dataCartaPorte.Remolque
                //{


                //    Placa = txtPlacaRemolque.Text,
                //    SubTipoRem = value


                //});

                //MessageBox.Show("Remolque agregado con éxito!");
               
            }
            catch (Exception ex) {
                MessageBox.Show("error remolque " + ex );
            }

        }

        private void cmbEstadoOr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var idEstadoOr = cmbEstadoOr.SelectedValue;

                string value = idEstadoOr.ToString();

                int value2 = int.Parse(value);

                var getValue = mydb.Estado.FirstOrDefault(edo => edo.intID == value2);

                string strEstadoOr = getValue.strNombreEstado.ToString().Split('-')[0];
                wpfEFac.Models.Localidad loc = new Models.Localidad();
                cmbLocOr.ItemsSource = mydb.Localidad.Where(a => a.str_descripcion.Contains(strEstadoOr+"-"));

                cmbLocOr.DisplayMemberPath = "str_descripcion";
                cmbLocOr.SelectedValuePath = "str_codigo";
                cmbLocOr.SelectedValue = loc.id;


                wpfEFac.Models.Municipio mun = new Models.Municipio();
                cmbMunicipioOr.ItemsSource = mydb.Municipio.Where(a => a.str_descripcion.Contains(strEstadoOr + "-"));
                cmbMunicipioOr.DisplayMemberPath = "str_descripcion";
                cmbMunicipioOr.SelectedValuePath = "str_codigo";
                cmbMunicipioOr.SelectedValue = mun.id;
            }
            catch (Exception ex) { }
        }

        private void txtCpOr_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int chars = txtCpOr.Text.Length;
                
                if (chars == 5)
                {
                    wpfEFac.Models.Colonia col = new Models.Colonia();

                    string strCP = txtCpOr.Text;

                    cmbColOr.ItemsSource = mydb.Colonia.Where(a => a.str_descripcion.Contains(strCP + "-"));
                    cmbColOr.DisplayMemberPath = "str_descripcion";
                    cmbColOr.SelectedValuePath = "str_codigo";
                    cmbColOr.SelectedValue = col.id;


                }
               
            }
            catch (Exception ex) {

                //MessageBox.Show("Error buscar Colonia " + ex );

            
            }
        }

        private void txtCpDes_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int chars = txtCpDes.Text.Length;

                if (chars == 5)
                {
                    wpfEFac.Models.Colonia col = new Models.Colonia();

                    string strCP = txtCpDes.Text;

                    cmbColDes.ItemsSource = mydb.Colonia.Where(a => a.str_descripcion.Contains(strCP + "-"));
                    cmbColDes.DisplayMemberPath = "str_descripcion";
                    cmbColDes.SelectedValuePath = "str_codigo";
                    cmbColDes.SelectedValue = col.id;


                }

            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error buscar Colonia " + ex );


            }

        }

        private void cmbEstadoDes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {

                var idEstadoDes = cmbEstadoDes.SelectedValue;

                string value = idEstadoDes.ToString();

                int value2 = int.Parse(value);

                var getValue = mydb.Estado.FirstOrDefault(edo => edo.intID == value2);

                string strEstadoDes = getValue.strNombreEstado.ToString().Split('-')[0];
                wpfEFac.Models.Localidad loc = new Models.Localidad();

                cmbLocDes.ItemsSource = mydb.Localidad.Where(a => a.str_descripcion.Contains(strEstadoDes + "-"));

                cmbLocDes.DisplayMemberPath = "str_descripcion";
                cmbLocDes.SelectedValuePath = "str_codigo";
                cmbLocDes.SelectedValue = loc.id;


                wpfEFac.Models.Municipio mun = new Models.Municipio();
                cmbMunicipioDes.ItemsSource = mydb.Municipio.Where(a => a.str_descripcion.Contains(strEstadoDes + "-"));
                cmbMunicipioDes.DisplayMemberPath = "str_descripcion";
                cmbMunicipioDes.SelectedValuePath = "str_codigo";
                cmbMunicipioDes.SelectedValue = mun.id;
            }
            catch (Exception ex) { }

        }

        private void chbAddXML_Unchecked(object sender, RoutedEventArgs e)
        {
            txbBusarXML.Visibility = System.Windows.Visibility.Collapsed;
            bttBuscarXML.Visibility = System.Windows.Visibility.Collapsed;
            bttBuscarPDF.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void chbAddXML_Checked(object sender, RoutedEventArgs e)
        {
            txbBusarXML.Visibility = System.Windows.Visibility.Visible;
            bttBuscarXML.Visibility = System.Windows.Visibility.Visible;
            bttBuscarPDF.Visibility = System.Windows.Visibility.Visible;
        }

        private void chbSPC_Unchecked(object sender, RoutedEventArgs e)
        {

        }



        private void chbSPC_Checked(object sender, RoutedEventArgs e)
        {
            AddDataSPC dataspc = new AddDataSPC();
            dataspc.ShowDialog();

            jsonString = dataspc.TheValue;


        }



        public void fillEditCP(Factura f) {

            try
            {
                dataCartaPorte.Root dataJson = JsonConvert.DeserializeObject<dataCartaPorte.Root>(f.strCadenaOriginal);

                List<Item> dataJsonGrid = JsonConvert.DeserializeObject<List<Item>>(f.strProveedor);
               // dtgUbicaciones.ItemsSource = dataJsonGrid.;
                
                

                txtPermisoSCT.Text = dataJson.Autotransporte.PermSCT;
                txtNoPermSCT.Text = dataJson.Autotransporte.NumPermisoSCT;
                txtAeguraRespCivil.Text = dataJson.Autotransporte.Seguros.AseguraRespCivil;
                txtPolRespCivil.Text = dataJson.Autotransporte.Seguros.PolizaRespCivil;
                txtAseguraCarga.Text = dataJson.Autotransporte.Seguros.AseguraCarga;
                atcVehiculoTracto.Text = dataJson.Autotransporte.IdentificacionVehicular.PlacaVM;
                cmbConfVehi.SelectedValue = dataJson.Autotransporte.IdentificacionVehicular.ConfigVehicular;
                txtAnoModel.Text = dataJson.Autotransporte.IdentificacionVehicular.AnioModeloVM.ToString();
                txtPesoNetoTotal.Text = dataJson.Mecancancias.PesoBrutoTotal.ToString();
                cmbUnidadPeso.Text = dataJson.Mecancancias.UnidadPeso;




                foreach (var item in dataJson.Mecancancias.Mercancia) //dtgConceptos.Items)
                {
                    conceptos.Add(new CarritoComprasEntry()

                    {

                        Cantidad = item.Cantidad,
                        Unidad = item.ClaveUnidad,
                        claveSat = item.BienesTransp,
                        pesoKg = item.PesoEnKg,
                        Nombre = item.Descripcion,
                        PrecioUnitario = decimal.Parse("0.00"),
                        FormatPrecioUnitario = "0.00",
                        FormatImporte = "0.00",
                        Descuento = decimal.Parse("0.00"),
                        IVA = decimal.Parse("0.00"),
                        retIVA = decimal.Parse("0.00"),
                        retISR = decimal.Parse("0.00"),
                        retIEPS = decimal.Parse("0.00"),
                        isMercancia = "1",
                        Importe = decimal.Parse("0.00"),
                        IdOrigen = item.CantidadTransporta.IDOrigen,
                        IdDestino = item.CantidadTransporta.IDDestino
                    }


                        );
                    dtgConceptos.ItemsSource = conceptos;
                    conceptos.CollectionChanged += conceptos_CollectionChanged;
                }

                foreach(var i in dataJsonGrid)
                {

                    dtgUbicaciones.Items.Add(new Item()
                    {

                        intID = i.intID,//cmbUbicaciones.SelectedValue.ToString(),
                        strRFC = i.strRFC,//getValue.strRFC,
                        strRazonSocial = i.strRazonSocial, //getValue.strRazonSocial,
                        strDescripcion = i.strDescripcion, //getValue.strNombreComercial,
                        strTipo = i.strTipo,//getValue.strTipodeInscripcion,
                        dtmFecha = i.dtmFecha,// dtpFechaSal_lleg.ToString(),
                        idOrigen = i.idOrigen,// idOr,
                        idDestino = i.idDestino,//idDest,
                        dcmDistancia = i.dcmDistancia//dcmDitRec,

                    });
                
                
                
                }


               // txtRFCOp.Text = dataJson.FiguraTransporte.TiposFigura.FirstOrDefault().RFCFigura;
               // txtNombreOp.Text = dataJson.FiguraTransporte.TiposFigura.FirstOrDefault().NombreFigura;
               // txtLicOp.Text = dataJson.FiguraTransporte.TiposFigura.FirstOrDefault().NumLicencia;
               // cmbTipoFigura.SelectedValue = dataJson.FiguraTransporte.TiposFigura.FirstOrDefault().TipoFigura;




            }
            catch (Exception ex) {
                MessageBox.Show("Error al cagar la carta porte intentelo mas tarde (error: " + ex.Message + ")");
            
            }
        
        }

        private void bttEliminarUbicacion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = dtgUbicaciones.SelectedItem;
                if (selectedItem != null)
                {
                    dtgUbicaciones.Items.Remove(selectedItem);

                   

                }
            }
            catch (Exception ex)
            {
            }
        }

        public class Item
        {
            public string intID { get; set; }
            public string strRFC { get; set; }
            public string strRazonSocial { get; set; }
            public string strDescripcion { get; set; }
            public string strTipo { get; set; }
            public string dtmFecha { get; set; }
            public string idOrigen { get; set; }
            public string idDestino { get; set; }
            public decimal dcmDistancia { get; set; }
            

        }

        private void btnAddUbicacion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string idOr = "";
                string idDest = "";
                decimal dcmDitRec = 0;

               var receptor = (wpfEFac.Models.Clientes)atcUbicaciones.SelectedItem;


              // var rep = atcUbicaciones.SelectedItem;


              //  var getEntity = cmbUbicaciones.SelectedValue;

              //  string value = getEntity.ToString();

               // int idEntity = int.Parse(value);

              // var getValue = mydb.Clientes.FirstOrDefault(idCli => idCli.intID == receptor.intID);

               if (receptor.strTipodeInscripcion.Equals("Origen"))
                {

                    idOr = txtIdOrigen.Text;

                }

               if (receptor.strTipodeInscripcion.Equals("Destino"))
                {

                    idDest = txtIdDestino.Text;
                    if (!string.IsNullOrEmpty(txtDistRecoDes.Text))
                    {
                        dcmDitRec = decimal.Parse(txtDistRecoDes.Text);
                    }
                    else {
                        throw new InvalidOperationException("Ingrese Distancia recorrida");
                    }

                }
                





                dtgUbicaciones.Items.Add(new Item()
                {

                    //intID = cmbUbicaciones.SelectedValue.ToString(),
                    intID = receptor.intID.ToString(),
                    strRFC = receptor.strRFC,
                    strRazonSocial = receptor.strRazonSocial,
                    strDescripcion = receptor.strNombreComercial,
                    strTipo = receptor.strTipodeInscripcion,
                    dtmFecha = dtpFechaSal_lleg.ToString(),
                    idOrigen = idOr,
                    idDestino = idDest,
                    dcmDistancia = dcmDitRec,

                });

            }
            catch (Exception ex) {

                MessageBox.Show("Error al agregar ubicacion (error: " + ex.Message + ")");
            
            
            
            }
        }

        private void chbAddFactoraje_Checked(object sender, RoutedEventArgs e)
        {
            txbBusarXML.Visibility = System.Windows.Visibility.Visible;
            bttBuscarXML.Visibility = System.Windows.Visibility.Visible;
            txbBusarPDF.Visibility = System.Windows.Visibility.Visible;
            bttBuscarPDF.Visibility = System.Windows.Visibility.Visible;
        }

        private void chbAddFactoraje_Unchecked(object sender, RoutedEventArgs e)
        {
            txbBusarXML.Visibility = System.Windows.Visibility.Collapsed;
            txbBusarPDF.Visibility = System.Windows.Visibility.Collapsed;
            bttBuscarXML.Visibility = System.Windows.Visibility.Collapsed;
            bttBuscarPDF.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void bttBuscarExcel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = null;

            if (chbAddCCP.IsChecked == true)
            {



                ofd.FileName = "Mercancias";
                ofd.DefaultExt = ".xls";
                ofd.Filter = "All Files|*.*";
                // ofd.Filter = "Excel Worksheets|*.xls";

                result = ofd.ShowDialog();

                if (result == true)
                {

                    Excel.Application excelApp = new Excel.Application();
                    if (excelApp != null)
                    {
                        Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(@"" + ofd.FileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                        Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                        Excel.Range excelRange = excelWorksheet.UsedRange;
                        int rowCount = excelRange.Rows.Count;
                        int colCount = excelRange.Columns.Count;
                        decimal dcmCantidad = 0;
                        string strUnidadMedida = "";
                        string strCodigoSat = "";
                        decimal dcmPesoKG = 0;
                        string strDescripcion = "";
                        decimal PesoTotal = 0;


                        for (int i = 1; i <= rowCount; i++)
                        {
                            for (int j = 1; j <= colCount; j++)
                            {
                                Excel.Range range = (excelWorksheet.Cells[i, j] as Excel.Range);
                                string cellValue = range.Value.ToString();
                                if (j == 1)
                                {
                                    dcmCantidad = decimal.Parse(cellValue);
                                }

                                if (j == 2)
                                {
                                    strUnidadMedida = cellValue;
                                }

                                if (j == 3)
                                {
                                    strCodigoSat = cellValue;
                                }

                                if (j == 4)
                                {
                                    dcmPesoKG = decimal.Parse(cellValue);
                                    PesoTotal += dcmPesoKG;

                                }
                                if (j == 5)
                                {
                                    strDescripcion = cellValue;

                                    conceptos.Add(new CarritoComprasEntry()

                                    {

                                        Cantidad = dcmCantidad,
                                        Unidad = strUnidadMedida,
                                        claveSat = strCodigoSat,
                                        pesoKg = dcmPesoKG,
                                        Nombre = strDescripcion,
                                        PrecioUnitario = decimal.Parse("0.00"),
                                        FormatPrecioUnitario = "0.00",
                                        FormatImporte = "0.00",
                                        Descuento = decimal.Parse("0.00"),
                                        IVA = decimal.Parse("0.00"),
                                        retIVA = decimal.Parse("0.00"),
                                        retISR = decimal.Parse("0.00"),
                                        retIEPS = decimal.Parse("0.00"),
                                        isMercancia = "1",
                                        Importe = decimal.Parse("0.00"),
                                        IdOrigen = txtIdOrigen.Text,
                                        IdDestino = txtIdDestino.Text
                                    }


                                        );

                                    txtPesoNetoTotal.Text = PesoTotal.ToString();
                                    dtgConceptos.ItemsSource = conceptos;
                                    conceptos.CollectionChanged += conceptos_CollectionChanged;


                                }

                                //do anything
                            }
                        }

                        excelWorkbook.Close();
                        excelApp.Quit();
                    }



                }




                //ofd.FileName = "Prueba";
                //ofd.DefaultExt = ".xml";
                //ofd.Filter = "Documentos XML (.xml)|*.xml";

                //result = ofd.ShowDialog();

                //if (result == true)
                //{


                //    dlleFac.Comprobante myComprobante = DeserializeCFD32(ofd.FileName);

                //    foreach (var item in myComprobante.Conceptos)
                //    {

                //        conceptos.Add(new CarritoComprasEntry()

                //            {

                //                Cantidad = item.Cantidad,
                //                Unidad = item.ClaveUnidad,
                //                claveSat = item.ClaveProdServ,
                //                pesoKg = decimal.Parse("0.001"),
                //                Nombre = item.Descripcion,
                //                PrecioUnitario = decimal.Parse("0.00"),
                //                FormatPrecioUnitario = item.ValorUnitario.ToString("N"),
                //                FormatImporte = "0.00",
                //                Descuento = decimal.Parse("0.00"),
                //                IVA = decimal.Parse("0.00"),
                //                retIVA = decimal.Parse("0.00"),
                //                retISR = decimal.Parse("0.00"),
                //                retIEPS = decimal.Parse("0.00"),
                //                isMercancia = "1",
                //                Importe = item.Importe,
                //                IdOrigen = txtIdOrigen.Text,
                //                IdDestino = txtIdDestino.Text
                //            }


                //            );


                //        dtgConceptos.ItemsSource = conceptos;
                //        conceptos.CollectionChanged += conceptos_CollectionChanged;

                //    }
                //} 



            }
        }
        wpfEFac.Models.Clientes myc = null;
        wpfEFac.Models.Vehiculos myv = null;

        int value = 0;

        private void atcUbicaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (atcUbicaciones.SelectedItem != null)
            {
                myc = ((wpfEFac.Models.Clientes)atcUbicaciones.SelectedItem);
                atcUbicaciones.Text = myc.strNombreComercial;
                // atcRFC.Text = clientes.strRFC;
                 // atcUbicaciones.SelectedItem = clientes;

            }
        }

        private void atcUbicaciones_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void atcVehiculoTracto_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void atcVehiculoTracto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (atcVehiculoTracto.SelectedItem != null)
            {
                myv = ((wpfEFac.Models.Vehiculos)atcVehiculoTracto.SelectedItem);
                atcVehiculoTracto.Text = myv.strPlaca;
                txtAnoModel.Text = myv.strAno;
                txtAeguraRespCivil.Text = myv.strAseguradoraRepCivil;
                txtPolRespCivil.Text = myv.strNoPoliza;
                txtAseguraCarga.Text = myv.strAseguradoraCarga;
                // atcRFC.Text = clientes.strRFC;
                // atcUbicaciones.SelectedItem = clientes;

            }
            else {
                atcVehiculoTracto.Text = "";
                txtAnoModel.Text = "";
                txtAeguraRespCivil.Text = "";
                txtPolRespCivil.Text = "";
                txtAseguraCarga.Text = "";
            
            
            }
        }

        private void atcVehiculoRemolque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (atcVehiculoRemolque.SelectedItem != null)
            {
                myv = ((wpfEFac.Models.Vehiculos)atcVehiculoRemolque.SelectedItem);
                atcVehiculoRemolque.Text = myv.strPlaca;
            }
            else {

                atcVehiculoRemolque.Text = "";
            
            }

        }

       






    }
}

