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
using System.Reflection;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using wpfEFac.Models;
using wpfEFac.Views.PuntoVenta;
using System.Transactions;
using wpfEFac.Helpers;
using GalaSoft.MvvmLight.Messaging;
using System.Xml.Xsl;
using System.Xml.Schema;
using System.IO;
using ThoughtWorks.QRCode.Codec;
using GenCode128;
using System.Net.Mail;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace wpfEFac.Views
{
    /// <summary>
    /// Interaction logic for DefaultPuntoVenta.xaml
    /// </summary>
    public partial class DefaultPuntoVenta : Page
    {
        private DataGridColumn currentSortColumn;
        private PreFacturaViewModel pfvm;
        private eFacDBEntities db;

        private ListSortDirection currentSortDirection;

        //private PageC

        public DefaultPuntoVenta()
        {
            InitializeComponent();

            DataContext = new PuntoVentaViewModel();

            pfvm = new PreFacturaViewModel();


            db = new eFacDBEntities();

            wpfEFac.Models.CFD myCfd = new Models.CFD();
            cmbTipoComprobante.ItemsSource = db.CFD;
            cmbTipoComprobante.DisplayMemberPath = "strDescripcion";
            cmbTipoComprobante.SelectedValuePath = "intID";
            cmbTipoComprobante.SelectedValue = myCfd.intID;
            cmbTipoComprobante.SelectedIndex = 0;

            dtpFechaFGInicio.SelectedDate = DateTime.Now;
            dtpFechaFGFin.SelectedDate = DateTime.Now;


            buscar();
        }

        private void dtpInicio_Loaded(object sender, RoutedEventArgs e)
        {
            //eFacDBEntities db = new eFacDBEntities();
            //List<Factura> fact = db.Factura.OrderByDescending(p => p.dtmFecha).ToList();

            //ICollectionView facturas = CollectionViewSource.GetDefaultView(fact);
            //dtgFacturasHistorico.ItemsSource = facturas;
            //facturas.Filter = TextFilter;
        }
      



        private void CheckCFDStatus(DataGrid dataGrid)
        {
            Factura f = (Factura)dataGrid.SelectedItem;
            DisableMenuItems();

            if (f != null)
            {
                switch (f.chrStatus)
                {
                    case "P":
                        {
                            mniAprovar.IsEnabled = true;
                            mniImprimir.IsEnabled = true;
                            mniComntario.IsEnabled = true;
                            
                        }
                        break;
                    case "A":
                        {
                            mniXML.IsEnabled =
                            mniPDF.IsEnabled =
                            mniImprimir.IsEnabled =
                            mniEmail.IsEnabled =
                            mniCancelar.IsEnabled = true;
                            mniPayment.IsEnabled = true;
                            mniComntario.IsEnabled = true;

                        } break;
                    case "E":
                        {
                            mniEmail.IsEnabled = true;
                        }
                        break;
                    case "C":
                        {
                            mniXML.IsEnabled =
                            mniPDF.IsEnabled = true;
                            mniComntario.IsEnabled = true;
                            mniImprimir.IsEnabled = true;
                        }
                        break;
                    default:

                        break;
                }


            }
        }

        private void DisableMenuItems()
        {
            mniAprovar.IsEnabled = mniCancelar.IsEnabled = mniImprimir.IsEnabled = mniEmail.IsEnabled =
                mniPDF.IsEnabled = mniXML.IsEnabled = false;
        }

        private void dtgFacturasHistorico_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            if (currentSortColumn != null)
            {
                currentSortColumn.SortDirection = currentSortDirection;
            }
        }

     
        private void hplNuevaFactura_Click(object sender, RoutedEventArgs e)
        {

            if (this.dtgFacturasHistorico.SelectedItem != null)
            {

                IList items = dtgFacturasHistorico.SelectedItems;

                MessageBoxResult result = MessageBox.Show("El comprobante lleva " + items.Count+ " documentos Relacionados", "CFDi Relacionado",
                            MessageBoxButton.OKCancel, MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    List<Relacionados> itemsId = new List<Relacionados>();

                   

                    foreach (Factura item in items)
                    {
                        itemsId.Add(new Relacionados
                        {

                            idFact = item.intID
                        });
                    }


                    PreFactura newPreFactura = new PreFactura(1, itemsId);

                    this.NavigationService.Navigate(newPreFactura);

                }
                else {

                    PreFactura newPreFactura = new PreFactura(1, null);

                    this.NavigationService.Navigate(newPreFactura);
                
                }

            }
            else {

                PreFactura newPreFactura = new PreFactura(1, null);

                this.NavigationService.Navigate(newPreFactura);
            
            }
            
        }

        private void mniAprovar_Click(object sender, RoutedEventArgs e)
        {
            if (this.dtgFacturasHistorico.SelectedItem != null)
            {
                Factura f = (Factura)dtgFacturasHistorico.SelectedItem;
                if ((f.intID_Tipo_CFD == 1) || (f.intID_Tipo_CFD == 2) || (f.intID_Tipo_CFD == 3) || (f.intID_Tipo_CFD == 4) || (f.intID_Tipo_CFD == 5) || (f.intID_Tipo_CFD == 6) || (f.intID_Tipo_CFD == 7))
                {
                    if (f.chrStatus == "P")
                    {
                        MessageBoxResult result = MessageBox.Show("Desea aprobar el comprobante", "Aprobar",
                            MessageBoxButton.OKCancel, MessageBoxImage.Information);

                        if (result == MessageBoxResult.OK)
                        {

                            MessageBoxResult resultSAT = MessageBox.Show("Esta acción enviará su Comprobante al SAT, esta seguro? ", "Aprobar",
                            MessageBoxButton.OKCancel, MessageBoxImage.Information);

                            if (resultSAT == MessageBoxResult.OK)
                            {

                                eFacDBEntities db = new eFacDBEntities();

                                db.Connection.Open();
                                if ((f.intID_Tipo_CFD == 1) || (f.intID_Tipo_CFD == 2) || (f.intID_Tipo_CFD == 3) || (f.intID_Tipo_CFD == 4) || (f.intID_Tipo_CFD == 5))
                                {
                                    AprobarFactura(f, db, true);
                                }
                                if(f.intID_Tipo_CFD==6) {

                                    AprobarPago(f, db, true);
                                }

                                if (f.intID_Tipo_CFD == 7)
                                {

                                    AprobarCartaPorte(f, db, true);
                                }



                                this.DataContext = null;

                                this.DataContext = new PuntoVentaViewModel();
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("Solo se puede aprobar Comprobantes con valor Fiscal");
            }
        }

        private void AprobarFactura(Factura f, eFacDBEntities db, bool Aprobado)
        {
            try
            {

                string strUs = UserPac.user;
                string strValue = UserPac.passwd;
                string idEquipo = UserPac.idEquipo;
                
                /*
                if (Aprobado)
                {
                    wsAdesoftSecurity.ServiceSecurityClient mySecurity = new wsAdesoftSecurity.ServiceSecurityClient();
                    strValue = mySecurity.verifyUser(f.Empresa.strRFC, "adsoftsito", "Guebos1#", 1);
                    strUs = string.Empty;
                    if (strValue == string.Empty)
                    {
                        throw new Exception("Error de comunicacion con el Servidor, favor de contactar a su proveedor para verificar el estado de su cuenta.");
                    }
                    else
                    {
                        strUs = strValue.Split('|').ElementAt(0);
                        strValue = strValue.Split('|').ElementAt(1);
                    }
                }
                */

                Factura factura = db.Factura.Where(fac => fac.intID == f.intID).First();

                var direccionEmisor = pfvm.GetDireccionEmpresa(f.Empresa.intID);
                var direccionEmision = pfvm.GetDireccionEmpresaEmision(f.Empresa.intID);
                //var direccionEmisor = pfvm.GetDireccionCliente(f.Clientes1.intID);
                var direccionReceptor = pfvm.GetDireccionCliente(f.Clientes.intID);



                var conceptos = f.Detalle_Factura;

                //List<dlleFac.Concepto> c = new List<dlleFac.Concepto>();
                List<dlleFac.ComprobanteConcepto> myConceptos = new List<dlleFac.ComprobanteConcepto>();



                dlleFac.Comprobante myCFD20 = new dlleFac.Comprobante();
                FillCFD30(f, direccionEmisor, direccionEmision, direccionReceptor, myConceptos, myCFD20);

                

                dlleFac.Factura objFac;

                try
                {
                    //using (TransactionScope scope = new TransactionScope())
                    // {
                    dlleFac.MyFactE myf = new dlleFac.MyFactE();
                    objFac = null;

                    try
                    {
                        objFac = myf.createXML30(
                            f.intID_Tipo_CFD,
                            f.strFolio,
                            f.Empresa.strRFC,
                            f.Certificates.strCertificadoSelloDigitalPath,
                             f.Certificates.strLlaveCertificadoPath,
                             f.Certificates.strContraseñaSAT,
                             f.Empresa.strDirectorioXML,
                             f.Empresa.strDirectorioPDF,
                             myCFD20,
                             f.strObervaciones,
                             f.strProveedor,
                             f.strNumero,
                             f.CFD.strDescripcion,
                             Aprobado == true ? f.CFD.templateReport : f.CFD.templateReportHdemo,
                             Aprobado == true ? f.CFD.templateReport : f.CFD.templateReportDemo,
                             Aprobado,
                            f.Origen,
                        f.RecogerEn,
                        f.Destino,
                        f.Destinatario,
                        f.rfcDestinatario,
                        f.domicilioDestinatario,
                        f.EntregarEn,
                        false,
                        strUs,
                        strValue,
                        idEquipo

                             );
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Ocurrio el siguiente error: " + e.Message);
                    }


                    if (objFac != null)
                    {

                        string strQRCode = "";

                        if (objFac.UUID == "")
                        {

                            string strUUID = "00000000-0000-0000-0000-000000000000";


                            string strSello = "EbDUVW/pHFjejpFmvrYUnfk1OcSzVvYUBNHvKOmIAy2ZANnAkR5u5pCW1GYHhddbZZ2itKovWbBIeAVIDEjYg97OPQpwOk06MzFseKzK9eHG8rpLHDVoY/uh36C1R8ujRvPOfP9/KkOdX/PYx1L5OK7v4dy0X/F2wsh6AbLOwi0MyIsivZwTpGD+x6lYFFEU4EiGIZ8l+93XDPJNIHR76K53ip5MWL0HIZBi0Ocd0wLa2XqU5AGrkoeo4cdh4b4Snwr+mx/+jOo7MiZrguvZ3GN1tNHrw2QUzE7UzubnT5VjdGcZcobSCslDkLoYfNZllbHGRryIpnmCECr8sinvalor";

                            //Ejemplo:https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=5803EB8D-81CD-4557-8719-26632D2FA434&re=XOCD720319T86&rr=CARR861127SB0&tt=0000014300.000000&fe=rH8/bw==

                            strQRCode = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" + strUUID +//+ objFac.UUID +
                                               "&re=" + myCFD20.Emisor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&rr=" + myCFD20.Receptor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&tt=" + myCFD20.Total.ToString("#0.000000").PadLeft(17, '0') +
                                               "&fe=" + strSello.Substring(strSello.Length - 8, 8);
                            //"&fe=" + objFac.sello.Substring(objFac.sello.Length,-8);

                        }
                        else
                        {

                            strQRCode = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" + objFac.UUID +
                                               "&re=" + myCFD20.Emisor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&rr=" + myCFD20.Receptor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&tt=" + myCFD20.Total.ToString("#0.000000").PadLeft(17, '0') +
                                               "&fe=" + objFac.sello.Substring(objFac.sello.Length - 8, 8);


                        }
                       




                        getQRCode(strQRCode);

                       


                        string myXMLPDF = myf.createPDF30(
                            f.intID_Tipo_CFD,
                            f.strFolio,
                            f.CFD.strDescripcion,
                            f.Empresa.strDirectorioXML,
                            f.Empresa.strDirectorioPDF,
                            f.Empresa.strRFC,
                            f.Clientes.strRFC,
                            f.CFD.templateReportH,
                            f.CFD.templateReport,
                            Aprobado);

                        string[] words = myXMLPDF.Split('|');
                        string myXML = words[0];
                        string myPDF = words[1];

                        if (Aprobado)
                        {

                            factura.chrStatus = "A";
                            factura.dtmFechaAprovacion = objFac.fechaAprobacion;
                            factura.strFolio = objFac.folio;
                            factura.strSerie = objFac.serie;
                            factura.strSelloDigital = factura.strSelloDigital = objFac.UUID;
                            factura.Certificates.strNumeroCertificadoSelloDigital = objFac.noCertificado;
                            factura.strCadenaOriginal = objFac.cadenaOriginal;
                            factura.strXMLpath = myXML;// objFac.fileXMLpath;
                            factura.strPDFpath = myPDF;
                            if (f.MetodoPago.Equals("PUE")) {
                                factura.strNumero = "PAGADA";
                                factura.dtmFechaEnvio = objFac.fechaAprobacion;    
                            }

                            db.SaveChanges();

                            //      scope.Complete();
                        }
                        else
                        {
                            factura.strPDFdemoPath = myPDF;
                            db.SaveChanges();
                            db.AcceptAllChanges();
                        }



                        if (Aprobado)
                        {
                            //MessageBox.Show("Test");
                            MessageBox.Show("Se creo el archivo xml y PDF", objFac.filePath);
                            db.AcceptAllChanges();

                            pfvm.UpdateFolioActual(f.intID_Certificate, f.intID_Tipo_CFD);

                           // cmbMes.SelectedIndex = DateTime.Now.Month - 1;
                            //txtAaaa.Text = DateTime.Now.Year.ToString();
                            buscar();

                            try
                            {
                                sendMail(factura.strXMLpath, factura.strPDFpath,
                                   f.Empresa.strNombreComercial,
                                   f.strSerie,
                                   f.strFolio,
                                   f.Clientes.strEmail, f.Empresa.strEmail, f.Empresa.strEmail2, f.Empresa.strTelefono, f.Empresa.strTelefono2);
                            }
                            catch (Exception ex) {

                                MessageBox.Show(ex.Message, "Error envio Correo", MessageBoxButton.OK, MessageBoxImage.Error); 
                            
                            }
                        }
                    }


                }
                catch (Exception e)
                {
                    throw new Exception();
                }
                finally
                {
                    db.Connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);   // Reforma Validador - update
            }
        }

        private void AprobarPago(Factura f, eFacDBEntities db, bool Aprobado)
        {
            try
            {

                string strUs = UserPac.user;
                string strValue = UserPac.passwd;
                string idEquipo = UserPac.idEquipo;

            

                Factura factura = db.Factura.Where(fac => fac.intID == f.intID).First();

                var direccionEmisor = pfvm.GetDireccionEmpresa(f.Empresa.intID);
                var direccionEmision = pfvm.GetDireccionEmpresaEmision(f.Empresa.intID);
                //var direccionEmisor = pfvm.GetDireccionCliente(f.Clientes1.intID);
                var direccionReceptor = pfvm.GetDireccionCliente(f.Clientes.intID);



                var conceptos = f.Detalle_Factura;

                //List<dlleFac.Concepto> c = new List<dlleFac.Concepto>();
                List<dlleFac.ComprobanteConcepto> myConceptos = new List<dlleFac.ComprobanteConcepto>();



                dlleFac.Comprobante myCFD20 = new dlleFac.Comprobante();


                FillCFDPago30(f, direccionEmisor, direccionEmision, direccionReceptor, myConceptos, myCFD20);



                dlleFac.Factura objFac;

                try
                {
                    //using (TransactionScope scope = new TransactionScope())
                    // {
                    dlleFac.MyFactE myf = new dlleFac.MyFactE();
                    objFac = null;

                    try
                    {
                        objFac = myf.createXML30(
                            f.intID_Tipo_CFD,
                            f.strFolio,
                            f.Empresa.strRFC,
                            f.Certificates.strCertificadoSelloDigitalPath,
                             f.Certificates.strLlaveCertificadoPath,
                             f.Certificates.strContraseñaSAT,
                             f.Empresa.strDirectorioXML,
                             f.Empresa.strDirectorioPDF,
                             myCFD20,
                             f.strObervaciones,
                             f.strProveedor,
                             f.strNumero,
                             f.CFD.strDescripcion,
                             Aprobado == true ? f.CFD.templateReport : f.CFD.templateReportHdemo,
                             Aprobado == true ? f.CFD.templateReport : f.CFD.templateReportDemo,
                             Aprobado,
                            f.Origen,
                        f.RecogerEn,
                        f.Destino,
                        f.Destinatario,
                        f.rfcDestinatario,
                        f.domicilioDestinatario,
                        f.EntregarEn,
                        false,
                        strUs,
                        strValue,
                        idEquipo

                             );
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Ocurrio el siguiente error: " + e.Message);
                    }


                    if (objFac != null)
                    {

                        string strQRCode = "";

                        if (objFac.UUID == "")
                        {

                            string strUUID = "00000000-0000-0000-0000-000000000000";


                            string strSello = "EbDUVW/pHFjejpFmvrYUnfk1OcSzVvYUBNHvKOmIAy2ZANnAkR5u5pCW1GYHhddbZZ2itKovWbBIeAVIDEjYg97OPQpwOk06MzFseKzK9eHG8rpLHDVoY/uh36C1R8ujRvPOfP9/KkOdX/PYx1L5OK7v4dy0X/F2wsh6AbLOwi0MyIsivZwTpGD+x6lYFFEU4EiGIZ8l+93XDPJNIHR76K53ip5MWL0HIZBi0Ocd0wLa2XqU5AGrkoeo4cdh4b4Snwr+mx/+jOo7MiZrguvZ3GN1tNHrw2QUzE7UzubnT5VjdGcZcobSCslDkLoYfNZllbHGRryIpnmCECr8sinvalor";

                            //Ejemplo:https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=5803EB8D-81CD-4557-8719-26632D2FA434&re=XOCD720319T86&rr=CARR861127SB0&tt=0000014300.000000&fe=rH8/bw==

                            strQRCode = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" + strUUID +//+ objFac.UUID +
                                               "&re=" + myCFD20.Emisor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&rr=" + myCFD20.Receptor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&tt=" + myCFD20.Total.ToString("#0.000000").PadLeft(17, '0') +
                                               "&fe=" + strSello.Substring(strSello.Length - 8, 8);
                            //"&fe=" + objFac.sello.Substring(objFac.sello.Length,-8);

                        }
                        else
                        {

                            strQRCode = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" + objFac.UUID +
                                               "&re=" + myCFD20.Emisor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&rr=" + myCFD20.Receptor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&tt=" + myCFD20.Total.ToString("#0.000000").PadLeft(17, '0') +
                                               "&fe=" + objFac.sello.Substring(objFac.sello.Length - 8, 8);


                        }





                        getQRCode(strQRCode);




                        string myXMLPDF = myf.createPDF30(
                            f.intID_Tipo_CFD,
                            f.strFolio,
                            f.CFD.strDescripcion,
                            f.Empresa.strDirectorioXML,
                            f.Empresa.strDirectorioPDF,
                            f.Empresa.strRFC,
                            f.Clientes.strRFC,
                            f.CFD.templateReportH,
                            f.CFD.templateReport,
                            Aprobado);

                        string[] words = myXMLPDF.Split('|');
                        string myXML = words[0];
                        string myPDF = words[1];

                        if (Aprobado)
                        {

                            factura.chrStatus = "A";
                            factura.dtmFechaAprovacion = objFac.fechaAprobacion;
                            factura.strFolio = objFac.folio;
                            factura.strSerie = objFac.serie;
                            factura.strSelloDigital = factura.strSelloDigital = objFac.UUID;
                            factura.Certificates.strNumeroCertificadoSelloDigital = objFac.noCertificado;
                            factura.strCadenaOriginal = objFac.cadenaOriginal;
                            factura.strXMLpath = myXML;// objFac.fileXMLpath;
                            factura.strPDFpath = myPDF;
                            
                            




                            db.SaveChanges();

                        
                        }
                        else
                        {
                            factura.strPDFdemoPath = myPDF;
                            db.SaveChanges();
                            db.AcceptAllChanges();
                        }



                        if (Aprobado)
                        {
                            MessageBox.Show("Se creo el archivo xml y PDF", objFac.filePath);
                            db.AcceptAllChanges();

                            pfvm.UpdateFolioActual(f.intID_Certificate, f.intID_Tipo_CFD);

                           // cmbMes.SelectedIndex = DateTime.Now.Month - 1;
                            //txtAaaa.Text = DateTime.Now.Year.ToString();
                            buscar();
                            try
                            {
                                sendMail(factura.strXMLpath, factura.strPDFpath,
                                   f.Empresa.strNombreComercial,
                                   f.strSerie,
                                   f.strFolio,
                                   f.Clientes.strEmail, f.Empresa.strEmail, f.Empresa.strEmail2, f.Empresa.strTelefono, f.Empresa.strTelefono2);
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, "Error envio Correo", MessageBoxButton.OK, MessageBoxImage.Error);

                            }



                        }
                    }


                }
                catch (Exception e)
                {
                    throw new Exception();
                }
                finally
                {
                    db.Connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);   // Reforma Validador - update
            }
        }

        private void AprobarCartaPorte(Factura f, eFacDBEntities db, bool Aprobado)
        {
            try
            {

                string strUs = UserPac.user;
                string strValue = UserPac.passwd;
                string idEquipo = UserPac.idEquipo;



                Factura factura = db.Factura.Where(fac => fac.intID == f.intID).First();

                var direccionEmisor = pfvm.GetDireccionEmpresa(f.Empresa.intID);
                var direccionEmision = pfvm.GetDireccionEmpresaEmision(f.Empresa.intID);
                //var direccionEmisor = pfvm.GetDireccionCliente(f.Clientes1.intID);
                var direccionReceptor = pfvm.GetDireccionCliente(f.Clientes.intID);



                var conceptos = f.Detalle_Factura;

                //List<dlleFac.Concepto> c = new List<dlleFac.Concepto>();
                List<dlleFac.ComprobanteConcepto> myConceptos = new List<dlleFac.ComprobanteConcepto>();



                dlleFac.Comprobante myCFD20 = new dlleFac.Comprobante();


                FillCFDCartaPorte30(f, direccionEmisor, direccionEmision, direccionReceptor, myConceptos, myCFD20);



                dlleFac.Factura objFac;

                try
                {
                    //using (TransactionScope scope = new TransactionScope())
                    // {
                    dlleFac.MyFactE myf = new dlleFac.MyFactE();
                    objFac = null;

                    try
                    {
                        objFac = myf.createXML30(
                            f.intID_Tipo_CFD,
                            f.strFolio,
                            f.Empresa.strRFC,
                            f.Certificates.strCertificadoSelloDigitalPath,
                             f.Certificates.strLlaveCertificadoPath,
                             f.Certificates.strContraseñaSAT,
                             f.Empresa.strDirectorioXML,
                             f.Empresa.strDirectorioPDF,
                             myCFD20,
                             f.strObervaciones,
                             f.strProveedor,
                             f.strNumero,
                             f.CFD.strDescripcion,
                             Aprobado == true ? f.CFD.templateReport : f.CFD.templateReportHdemo,
                             Aprobado == true ? f.CFD.templateReport : f.CFD.templateReportDemo,
                             Aprobado,
                            f.Origen,
                        f.RecogerEn,
                        f.Destino,
                        f.Destinatario,
                        f.rfcDestinatario,
                        f.domicilioDestinatario,
                        f.EntregarEn,
                        false,
                        strUs,
                        strValue,
                        idEquipo

                             );
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Ocurrio el siguiente error: " + e.Message);
                    }


                    if (objFac != null)
                    {

                        string strQRCode = "";

                        if (objFac.UUID == "")
                        {

                            string strUUID = "00000000-0000-0000-0000-000000000000";


                            string strSello = "EbDUVW/pHFjejpFmvrYUnfk1OcSzVvYUBNHvKOmIAy2ZANnAkR5u5pCW1GYHhddbZZ2itKovWbBIeAVIDEjYg97OPQpwOk06MzFseKzK9eHG8rpLHDVoY/uh36C1R8ujRvPOfP9/KkOdX/PYx1L5OK7v4dy0X/F2wsh6AbLOwi0MyIsivZwTpGD+x6lYFFEU4EiGIZ8l+93XDPJNIHR76K53ip5MWL0HIZBi0Ocd0wLa2XqU5AGrkoeo4cdh4b4Snwr+mx/+jOo7MiZrguvZ3GN1tNHrw2QUzE7UzubnT5VjdGcZcobSCslDkLoYfNZllbHGRryIpnmCECr8sinvalor";

                            //Ejemplo:https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=5803EB8D-81CD-4557-8719-26632D2FA434&re=XOCD720319T86&rr=CARR861127SB0&tt=0000014300.000000&fe=rH8/bw==

                            strQRCode = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" + strUUID +//+ objFac.UUID +
                                               "&re=" + myCFD20.Emisor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&rr=" + myCFD20.Receptor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&tt=" + myCFD20.Total.ToString("#0.000000").PadLeft(17, '0') +
                                               "&fe=" + strSello.Substring(strSello.Length - 8, 8);
                            //"&fe=" + objFac.sello.Substring(objFac.sello.Length,-8);

                        }
                        else
                        {

                            strQRCode = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" + objFac.UUID +
                                               "&re=" + myCFD20.Emisor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&rr=" + myCFD20.Receptor.Rfc.Replace("Ñ", "N").Replace("ñ", "n") +
                                               "&tt=" + myCFD20.Total.ToString("#0.000000").PadLeft(17, '0') +
                                               "&fe=" + objFac.sello.Substring(objFac.sello.Length - 8, 8);


                        }





                        getQRCode(strQRCode);




                        string myXMLPDF = myf.createPDF30(
                            f.intID_Tipo_CFD,
                            f.strFolio,
                            f.CFD.strDescripcion,
                            f.Empresa.strDirectorioXML,
                            f.Empresa.strDirectorioPDF,
                            f.Empresa.strRFC,
                            f.Clientes.strRFC,
                            f.CFD.templateReportH,
                            f.CFD.templateReport,
                            Aprobado);

                        string[] words = myXMLPDF.Split('|');
                        string myXML = words[0];
                        string myPDF = words[1];

                        if (Aprobado)
                        {

                            factura.chrStatus = "A";
                            factura.dtmFechaAprovacion = objFac.fechaAprobacion;
                            factura.strFolio = objFac.folio;
                            factura.strSerie = objFac.serie;
                            factura.strSelloDigital = factura.strSelloDigital = objFac.UUID;
                            factura.Certificates.strNumeroCertificadoSelloDigital = objFac.noCertificado;
                            factura.strCadenaOriginal = objFac.cadenaOriginal;
                            factura.strXMLpath = myXML;// objFac.fileXMLpath;
                            factura.strPDFpath = myPDF;

                            db.SaveChanges();

                         
                        }
                        else
                        {
                            factura.strPDFdemoPath = myPDF;
                            db.SaveChanges();
                            db.AcceptAllChanges();
                        }



                        if (Aprobado)
                        {
                            MessageBox.Show("Se creo el archivo xml y PDF", objFac.filePath);
                            db.AcceptAllChanges();

                            pfvm.UpdateFolioActual(f.intID_Certificate, f.intID_Tipo_CFD);

                          //  cmbMes.SelectedIndex = DateTime.Now.Month - 1;
                         //   txtAaaa.Text = DateTime.Now.Year.ToString();
                            buscar();


                            try
                            {
                                sendMail(factura.strXMLpath, factura.strPDFpath,
                                   f.Empresa.strNombreComercial,
                                   f.strSerie,
                                   f.strFolio,
                                   f.Clientes.strEmail, f.Empresa.strEmail, f.Empresa.strEmail2, f.Empresa.strTelefono, f.Empresa.strTelefono2);
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, "Error envio Correo", MessageBoxButton.OK, MessageBoxImage.Error);

                            }
                        }
                    }


                }
                catch (Exception e)
                {
                    throw new Exception();
                }
                finally
                {
                    db.Connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);   // Reforma Validador - update
            }
        }

        private int sendMail(string xmlPath, string pdfPath, string empresa, string Serie, String Folio,
            string mailCliente, string mailRespaldo, string mailContador, string emailRespaldo, string passEmailRespaldo)
        {

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential(emailRespaldo, passEmailRespaldo);


            //mail.To.Add("adesoft@live.com.mx");



            if (mailCliente != String.Empty) mail.CC.Add(mailCliente);
            if (mailContador != String.Empty) mail.CC.Add(mailContador);
            if (mailRespaldo != String.Empty) mail.CC.Add(mailRespaldo);


            /*
             * Attachments
             */
            try
            {
            mail.Attachments.Add(new Attachment(pdfPath));
            mail.Attachments.Add(new Attachment(xmlPath));

            string Asunto = empresa + " - CFDi ";

            if ((Serie != String.Empty) || (Folio != String.Empty))
                Asunto += Serie + Folio;

            mail.Subject = Asunto;

            mail.From = new System.Net.Mail.MailAddress(emailRespaldo);
            mail.IsBodyHtml = true;
            mail.Body = "Servicio proporcionado por ADESOFT";

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            //smtp.Port = 25;
            
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }



            /*
            WPFEmailer mySendMail = new WPFEmailer();

            string Asunto = empresa + " - CFDi ";

            if ((Serie != String.Empty) || (Folio != String.Empty))
                Asunto += Serie + Folio;

            mySendMail.Host = "smtp.gmail.com";
            mySendMail.User = "adesoft.cfdi@gmail.com";
            mySendMail.Password = "superputote";
            mySendMail.UseSSL = false;
            mySendMail.Port = 587;
            mySendMail.Subject = Asunto;
            mySendMail.Body = "Servicio proporcionado por ADESOFT SA de CV" + " www.adesoft.com.mx";

            mySendMail.AttachmentPath1 = xmlPath;
            mySendMail.AttachmentPath2 = pdfPath;
            mySendMail.From = "ADESOFT CFDi - Comprobante Fiscal Digital por Internet";

            if (mailCliente != String.Empty) mySendMail.To = mailCliente; else mySendMail.To = String.Empty;
            if (mailContador != String.Empty) mySendMail.ccContador = mailContador; else mySendMail.ccContador = String.Empty;
            if (mailRespaldo != String.Empty) mySendMail.ccRespaldo = mailRespaldo; else mySendMail.ccRespaldo = string.Empty;


            mySendMail.ccAdesoft = "adesoft@live.com.mx";

            try
            {
                mySendMail.SendEmail();
                MessageBox.Show("Message send successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
            */

            return 0;
        }




        private string getdv(string barCode)
        {
            int dv = 0, secuence = 0, dups = 0;
            char[] carray = new char[barCode.Length];
            carray = barCode.ToCharArray();
            secuence = 2;
            for (int i = barCode.Length - 1; i >= 0; i--)
            {

                dups = secuence * int.Parse(carray[i].ToString());
                if (dups > 10)
                {

                    char[] digits = new char[2];
                    digits = dups.ToString().ToCharArray();
                    dv += int.Parse(digits[0].ToString()) + int.Parse(digits[1].ToString());
                }
                else
                    dv += dups;
                if (secuence == 1)
                    secuence = 2;
                else
                    secuence--;
            }
            dv = dv % 10;
            if (!(dv == 0))
                dv = 10 - dv;
            //            lblshowcode.Text = "Text to Encode: \n\t" + barCode + "\nand the dv is: " + dv;
            return dv.ToString();
        }


        private static void ExtraerImpuestos(List<dlleFac.Traslado> t, List<dlleFac.Retencion> ret, dlleFac.ComprobanteFiscalDigital cfd)
        {
            cfd.Impuestos = new dlleFac.Impuestos()
            {
                TotalTraslados = t.First().TotalImpuestosTraslados,
                Traslados = t,
                TotalRetenido = ret.First().TotalImpuestoRetenido,
                Retenciones = ret
            };

        }

        private static void LlenarReceptor(Factura f, Direcciones_Fiscales direccionReceptor, dlleFac.ComprobanteFiscalDigital cfd)
        {
            cfd.Receptor = new dlleFac.Receptor()
            {
                RFCReceptor = f.Clientes.strRFC,
                NombreReceptor = f.Clientes.strRazonSocial
            };

            cfd.DomicilioFiscalReceptor = new dlleFac.DomicilioFiscal()
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



        private string getQRCode(String strQrCode)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            String encoding = "Byte";
            int intSize = 4;
            int intVersion = 9;
            string strErrorCorrect = "M";

            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            try
            {
                int scale = intSize;
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid size!");

            }
            try
            {
                int version = intVersion;
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid version !");
            }

            string errorCorrect = strErrorCorrect;
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            System.Drawing.Image image;
            String data = strQrCode;

            image = qrCodeEncoder.Encode(data);

            System.Windows.Forms.PictureBox picEncode = new System.Windows.Forms.PictureBox();
            picEncode.Image = image;

            /**************/

            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.FileName = "cbb.jpg";

            System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

            picEncode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
            fs.Close();

            return "";
        }





        private void FillCFD30(Factura f, Direcciones_Fiscales direccionEmisor, Direcciones_Fiscales direccionEmision, Direcciones_Fiscales direccionReceptor, List<dlleFac.ComprobanteConcepto> myConceptos, dlleFac.Comprobante MyCFD20)
        {

            decimal sumaRetIeps = 0;
            decimal sumaRetIva = 0;
            decimal sumaRetIsr = 0;
            decimal sumaSubtotal = 0;
            decimal sumaIva = 0;
            decimal totalRet = 0;
            decimal sumaBaseIva16 = 0;
            decimal sumaBaseIva0 = 0;
            Boolean isIva0 = false;
            Boolean isRet = false;
            Boolean isIva16 = false;


            //Folios myFolio = f.Certificates.Folios.Where(p=>p.chrStatus == "A" && p.intIDtipoCFD == f.intID_Tipo_CFD).First();
            Folios myFolio = f.CFD.Folios; //.Where(p=>p.chrStatus == "A").First();
            DateTime fechaAprobacion = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss"));



            MyCFD20.Fecha = fechaAprobacion;

            //MyCFD20.noAprobacion = myFolio.intNumero_Aprovacion.ToString();
            //MyCFD20.anoAprobacion = myFolio.strAño_Aprovacion;

            MyCFD20.LugarExpedicion = direccionEmision.strCodigoPostal; ; //dlleFac.c_CodigoPostal.Item01276;
            //xxx MyCFD20.numCuentaPago = string.IsNullOrEmpty(f.Clientes.strWebSite) ? "NO IDENTIFICADO" : f.Clientes.strWebSite;

            if ((f.intID_Tipo_CFD) == 1 || (f.intID_Tipo_CFD == 3) || ((f.intID_Tipo_CFD == 4) || ((f.intID_Tipo_CFD == 5))))
                MyCFD20.TipoDeComprobante = dlleFac.c_TipoDeComprobante.I;

            if ((f.intID_Tipo_CFD) == 2)
                MyCFD20.TipoDeComprobante = dlleFac.c_TipoDeComprobante.E;


           


            MyCFD20.Serie = f.strSerie;
            MyCFD20.Folio = f.strFolio;
            MyCFD20.FormaPago = f.strForma_Pago;
            MyCFD20.FormaPagoSpecified = true;
            MyCFD20.CondicionesDePago = f.CondPago;
            MyCFD20.MetodoPago = f.MetodoPago;
            MyCFD20.MetodoPagoSpecified = true;
            //xxx MyCFD20.motivodescuento = f.MotivoDesc;
           
            MyCFD20.Moneda = f.Divisa;
            if (f.Divisa != "MXN")
            {
                MyCFD20.TipoCambio = f.TipoCambio.Value;
                MyCFD20.TipoCambioSpecified = true;
            }



            MyCFD20.Descuento = decimal.Parse(f.dcmDescuento.Value.ToString("#0.00"));
            if (f.dcmDescuento > 0)
            {

                MyCFD20.DescuentoSpecified = true;
            }

            

            if (!string.IsNullOrEmpty(f.strNumeroContrato))
            {
                List <dlleFac.ComprobanteCfdiRelacionados> myRelacion = new List<dlleFac.ComprobanteCfdiRelacionados>();
                {
                    List<dlleFac.ComprobanteCfdiRelacionadosCfdiRelacionado> myUUID = new List<dlleFac.ComprobanteCfdiRelacionadosCfdiRelacionado>();
                    List<string> uuids2 = new List<string>();
                    var lstuuid = f.strNumeroContrato;

                    string[] uuids = lstuuid.Split('|');

                    foreach (var word in uuids)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            int intId_fac = int.Parse(word);

                            eFacDBEntities db = new eFacDBEntities();
                            Factura factura = db.Factura.Where(fac => fac.intID == intId_fac).First();


                            uuids2.Add(factura.strSelloDigital);
                        }

                    }

                    foreach (string item in uuids2)
                    {

                        myUUID.Add(new dlleFac.ComprobanteCfdiRelacionadosCfdiRelacionado
                        {


                            UUID = item

                        });

                    }



                    myRelacion.Add(new dlleFac.ComprobanteCfdiRelacionados
                    {

                        TipoRelacion = f.intEstimacion,
                        CfdiRelacionado = myUUID.ToArray()



                    });
                    



                    

                    MyCFD20.CfdiRelacionados = myRelacion.ToArray();
                }
            }


            MyCFD20.Emisor = new dlleFac.ComprobanteEmisor
            {
                RegimenFiscal = f.Empresa.strWebSite,
                Rfc = f.Empresa.strRFC,
                Nombre = f.Empresa.strRazonSocial

                /*
               DomicilioFiscal = new dlleFac.t_UbicacionFiscal
               {
                   calle = direccionEmisor.strCalle,
                   noInterior = direccionEmisor.strNoInterior,
                   noExterior = String.IsNullOrEmpty(direccionEmisor.strNoExterior.Trim()) ? "" : direccionEmisor.strNoExterior.Trim(),
                   pais = direccionEmisor.Paises.strNombrePais,
                   municipio = direccionEmisor.strMunicipio,
                   localidad = direccionEmisor.strPoblacionLocalidad,
                   estado = direccionEmisor.Estado.strNombreEstado,
                   codigoPostal = direccionEmisor.strCodigoPostal,
                   colonia = direccionEmisor.strColonia
               },


               ExpedidoEn = new dlleFac.t_Ubicacion
               {
                   calle = direccionEmision.strCalle,
                   noInterior = direccionEmision.strNoInterior,
                   noExterior = direccionEmision.strNoExterior,
                   pais = direccionEmision.Paises.strNombrePais,
                   municipio = direccionEmision.strMunicipio,
                   localidad = direccionEmision.strPoblacionLocalidad,
                   estado = direccionEmision.Estado.strNombreEstado,
                   codigoPostal = direccionEmision.strCodigoPostal,
                   colonia = direccionEmision.strColonia
               }*/
            };


            MyCFD20.Receptor = new dlleFac.ComprobanteReceptor
            {
                Rfc = f.Clientes.strRFC,
                Nombre = f.Clientes.strRazonSocial,
                UsoCFDI = f.MotivoDesc,
                RegimenFiscalReceptor = f.Clientes.strGiro,
                DomicilioFiscalReceptor = direccionReceptor.strCodigoPostal


                //ResidenciaFiscal = dlleFac.c_Pais.MEX,
                //ResidenciaFiscalSpecified = true

                /* 
                Domicilio = new dlleFac.t_Ubicacion
               {
                   calle = direccionReceptor.strCalle,
                   noInterior = direccionReceptor.strNoInterior,
                   noExterior = direccionReceptor.strNoExterior,
                   pais = direccionReceptor.Paises.strNombrePais,
                   municipio = direccionReceptor.strMunicipio,
                   localidad = direccionReceptor.strPoblacionLocalidad,
                   estado = direccionReceptor.Estado.strNombreEstado,
                   codigoPostal = direccionReceptor.strCodigoPostal,
                   colonia = direccionReceptor.strColonia

               }*/
            };





            foreach (var item in f.Detalle_Factura)
            {
                
                if(item.strPatida.Equals("0")){
                dlleFac.ComprobanteConceptoImpuestos
                       comImpuestos = new dlleFac.ComprobanteConceptoImpuestos();


                dlleFac.ComprobanteConceptoImpuestosTraslado
                   myConceptoTranslado = new dlleFac.ComprobanteConceptoImpuestosTraslado();

                myConceptoTranslado.Impuesto = dlleFac.c_Impuesto.Item002;

                myConceptoTranslado.TasaOCuota = decimal.Parse(item.dcmIVA.Value.ToString("#0.000000")); //item.dcmIVA.Value;
                myConceptoTranslado.TasaOCuotaSpecified = true;



                decimal BaseTraslado = decimal.Parse(item.dcmPrecioUnitario.ToString("#0.000000")) * item.dcmCantidad - item.dcmDescuento;

                myConceptoTranslado.Base = decimal.Parse(BaseTraslado.ToString("#0.000000"));

                if (item.dcmIVA > 0)
                {
                    isIva16 = true;
                    sumaBaseIva16 += myConceptoTranslado.Base;
                }
                if (item.dcmIVA == 0)
                {
                    isIva0 = true;
                    sumaBaseIva0 += myConceptoTranslado.Base;
                }

                decimal impuesto = myConceptoTranslado.Base * item.dcmIVA.Value;

                myConceptoTranslado.Importe = decimal.Parse(impuesto.ToString("#0.000000")); ;
                myConceptoTranslado.ImporteSpecified = true;


                sumaIva += impuesto;
                sumaSubtotal += BaseTraslado;

                List<dlleFac.ComprobanteConceptoImpuestosTraslado> myListTrans =
                 new List<dlleFac.ComprobanteConceptoImpuestosTraslado>();



                dlleFac.ComprobanteConceptoImpuestosRetencion
                   myConceptoRetencionIva = new dlleFac.ComprobanteConceptoImpuestosRetencion();
                dlleFac.ComprobanteConceptoImpuestosRetencion
                  myConceptoRetencionIrs = new dlleFac.ComprobanteConceptoImpuestosRetencion();


                dlleFac.ComprobanteConceptoImpuestosRetencion
                  myConceptoRetencionIeps = new dlleFac.ComprobanteConceptoImpuestosRetencion();

                List<dlleFac.ComprobanteConceptoImpuestosRetencion> myListReten =
                 new List<dlleFac.ComprobanteConceptoImpuestosRetencion>();



                if (item.retIVA > 0)
                {
                    myConceptoRetencionIva.Impuesto = dlleFac.c_Impuesto.Item002;
                    myConceptoRetencionIva.TasaOCuota = decimal.Parse(item.retIVA.Value.ToString("#0.000000")); //dlleFac.c_TasaOCuota.Item0160000;

                    decimal BasRetIva = item.dcmPrecioUnitario * item.dcmCantidad;
                    myConceptoRetencionIva.Base = decimal.Parse(BasRetIva.ToString("#0.0000")) ;

                    decimal ImportRet = BasRetIva * item.retIVA.Value;
                    sumaRetIva += ImportRet;
                    myConceptoRetencionIva.Importe = decimal.Parse(ImportRet.ToString("#0.0000"));

                     myListReten.Add(myConceptoRetencionIva);
                }

                if (item.retISR > 0)
                {
                    myConceptoRetencionIrs.Impuesto = dlleFac.c_Impuesto.Item001;
                    myConceptoRetencionIrs.TasaOCuota = decimal.Parse(item.retISR.Value.ToString("#0.000000"));  //dlleFac.c_TasaOCuota.Item0160000;

                    decimal BaseRetIsr = item.dcmPrecioUnitario * item.dcmCantidad;
                    myConceptoRetencionIrs.Base = decimal.Parse(BaseRetIsr.ToString("#0.0000"));

                    decimal impuestoRetIsr = myConceptoTranslado.Base * item.retISR.Value;

                    sumaRetIsr += impuestoRetIsr;

                    myConceptoRetencionIrs.Importe = decimal.Parse(impuestoRetIsr.ToString("#0.0000"));

                    if (impuestoRetIsr > 0)
                        myListReten.Add(myConceptoRetencionIrs);
                }


                if (item.retIEPS > 0)
                {
                    myConceptoRetencionIeps.Impuesto = dlleFac.c_Impuesto.Item003;
                    myConceptoRetencionIeps.TasaOCuota = decimal.Parse(item.retIEPS.Value.ToString("#0.000000")); //dlleFac.c_TasaOCuota.Item0160000;

                    decimal BaseRetIeps = item.dcmPrecioUnitario * item.dcmCantidad;
                    myConceptoRetencionIeps.Base = decimal.Parse(BaseRetIeps.ToString("#0.0000"));

                    decimal impuestoRetIeps = myConceptoTranslado.Base * item.retIEPS.Value;
                    myConceptoRetencionIeps.Importe = decimal.Parse(impuestoRetIeps.ToString("#0.0000")); 


                    if (impuestoRetIeps > 0)
                        myListReten.Add(myConceptoRetencionIeps);



                    sumaRetIeps += impuestoRetIeps;




                }





                myListTrans.Add(myConceptoTranslado);

                comImpuestos.Traslados = myListTrans.ToArray();

                if (myListReten.Count > 0)
                {
                    comImpuestos.Retenciones = myListReten.ToArray();
                }

                string[] arrunidad = item.strUnidad.Split('-');
                string claveunidad = arrunidad[0];
                string myuni = arrunidad[1];

                Boolean valueDesc = false;
                if (item.dcmDescuento > 0)
                {
                    valueDesc = true;
                }


                string[] arrayInfoGlobal = f.Destino.Split('/');
                string strPeriocidad = arrayInfoGlobal[0].Split('-')[0];
                string strMes = String.IsNullOrEmpty(arrayInfoGlobal[1]) ? "0" : arrayInfoGlobal[1];
                string strAno = String.IsNullOrEmpty(arrayInfoGlobal[1]) ? "0" : arrayInfoGlobal[2];


                try
                {
                    if ((decimal.Parse(strMes) > 0)||decimal.Parse(strAno)>0)
                    {

                        /* PARA GLOBAL*/
                        dlleFac.ComprobanteInformacionGlobal myGlobal = new dlleFac.ComprobanteInformacionGlobal();


                        myGlobal.Periodicidad = strPeriocidad;
                        myGlobal.Meses = strMes;
                        myGlobal.Año = short.Parse(strAno);

                        MyCFD20.InformacionGlobal = myGlobal;


                    }
                }
                catch (Exception ex) {

                    MessageBox.Show(ex + "Global");
                }




                decimal ImporteTotal = BaseTraslado + item.dcmDescuento;

                myConceptos.Add(new dlleFac.ComprobanteConcepto
                {

                    ObjetoImp = dlleFac.c_ObjetoImp.Item02,
                    Cantidad = decimal.Parse(item.dcmCantidad.ToString("#0.0000")),
                    ClaveProdServ = item.Productos.strCodigoBarras,
                    Unidad = myuni,
                    ClaveUnidad = claveunidad,
                    NoIdentificacion = item.strPatida,
                    Descripcion = item.strConcepto,
                    ValorUnitario = decimal.Parse(item.dcmPrecioUnitario.ToString("#0.000000")),
                    Importe = decimal.Parse(ImporteTotal.ToString("#0.000000")),
                    Descuento = item.dcmDescuento,
                    DescuentoSpecified = valueDesc,
                    Impuestos = comImpuestos


                });
            }
            }
            MyCFD20.Conceptos = myConceptos.ToArray();



            try
            {
                if (f.strCadenaOriginal.Contains("TranspInternac"))
                {

                    dataCartaPorte.Root dataJson = JsonConvert.DeserializeObject<dataCartaPorte.Root>(f.strCadenaOriginal);



                    dllCartaPorte.CartaPorte CartaPorte = new dllCartaPorte.CartaPorte();


                    //Nodo Ubicaciones
                    List<dllCartaPorte.CartaPorteUbicacion> myUbicacion = new List<dllCartaPorte.CartaPorteUbicacion>();

                    //   dllCartaPorte.CartaPorteFiguraTransporteOperadoresOperadorDomicilio myDomOper = new dllCartaPorte.CartaPorteFiguraTransporteOperadoresOperadorDomicilio();
                    //Nodo Mercancias
                    dllCartaPorte.CartaPorteMercancias myMercancia = new dllCartaPorte.CartaPorteMercancias();
                    List<dllCartaPorte.CartaPorteMercanciasMercancia> myMercancias = new List<dllCartaPorte.CartaPorteMercanciasMercancia>();


                    // dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta myCantTranpo = new dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta();

                    //Nodo Autotransporte
                    dllCartaPorte.CartaPorteMercanciasAutotransporte myAutoTransp = new dllCartaPorte.CartaPorteMercanciasAutotransporte();

                    //Nodo Figura Transpote
                    //dllCartaPorte.CartaPorteFiguraTransporte myFigTrans = new dllCartaPorte.CartaPorteFiguraTransporte();
                    dllCartaPorte.CartaPorteMercanciasAutotransporteIdentificacionVehicular myIndet = new dllCartaPorte.CartaPorteMercanciasAutotransporteIdentificacionVehicular();

                    dllCartaPorte.CartaPorteMercanciasAutotransporteSeguros mySeguro = new dllCartaPorte.CartaPorteMercanciasAutotransporteSeguros();
                    List<dllCartaPorte.CartaPorteMercanciasAutotransporteRemolque> myRemolque = new List<dllCartaPorte.CartaPorteMercanciasAutotransporteRemolque>();

                    //Nodo Operadores

                    //dllCartaPorte.CartaPorteO = new dllCartaPorte.CartaPorteFiguraTransporteOperadoresOperador();
                    List<dllCartaPorte.CartaPorteTiposFigura> myFiguraTransporte = new List<dllCartaPorte.CartaPorteTiposFigura>();



                    decimal dcmTotalDist = 0;

                    string idOrigen = "";
                    string idDestino = "";
                    decimal dcmPesoBrutoTotal = 0;





                    foreach (var i in dataJson.UbicacionOr)
                    {
                        try
                        {
                            //dllCartaPorte.CartaPorteUbicacionOrigen myOrigen = new dllCartaPorte.CartaPorteUbicacionOrigen();
                            dllCartaPorte.CartaPorteUbicacionDomicilio myDomOr = new dllCartaPorte.CartaPorteUbicacionDomicilio();


                            //  myOrigen.RFCRemitente = i.Origen.RFCRemitente;// "REFA860424NP1";
                            //  myOrigen.NombreRemitente = i.Origen.NombreRemitente;// "ALEJANDRO REYES";

                            //   myOrigen.FechaHoraSalida = DateTime.Parse(i.Origen.FechaHoraSalida.ToString());
                            myDomOr.Pais = dllCartaPorte.c_Pais.MEX;//"MEX";
                            myDomOr.NumeroExterior = i.DomicilioOri.NumeroExterior;// "728";
                            myDomOr.Municipio = i.DomicilioOri.Municipio; //"004";
                            myDomOr.Localidad = i.DomicilioOri.Localidad;
                            myDomOr.Estado = i.DomicilioOri.Estado;
                            myDomOr.Colonia = i.DomicilioOri.Colonia; //"0347";
                            myDomOr.CodigoPostal = i.DomicilioOri.CodigoPostal; //"25350";
                            myDomOr.Calle = i.DomicilioOri.Calle; //"JESUS VALDES SANCHEZ";



                            myUbicacion.Add(new dllCartaPorte.CartaPorteUbicacion
                            {
                                IDUbicacion = i.Origen.IDUbicacion,
                                TipoUbicacion = i.Origen.TipoUbicacion,
                                RFCRemitenteDestinatario = i.Origen.RFCRemitenteDestinatario,
                                FechaHoraSalidaLlegada = i.Origen.FechaHoraSalidaLlegada,
                                NombreRemitenteDestinatario = i.Origen.NombreRemitenteDestinatario,


                                //TipoEstacion = i.TipoEstacionOrigen.ToString(), //"01",
                                //TipoEstacionSpecified = false,
                                //DistanciaRecorrida = i.DistanciaRecorridaOrigen,
                                //DistanciaRecorridaSpecified = true,
                                //Origen = myOrigen,
                                Domicilio = myDomOr

                            });

                            //dcmTotalDist += i.DistanciaRecorridaOrigen;


                        }
                        catch (Exception ex) { }
                    }

                    foreach (var d in dataJson.UbicacionDes)
                    {
                        try
                        {

                            // dllCartaPorte.CartaPorteUbicacionDestino myDestino = new dllCartaPorte.CartaPorteUbicacionDestino();

                            dllCartaPorte.CartaPorteUbicacionDomicilio myDomDest = new dllCartaPorte.CartaPorteUbicacionDomicilio();



                            myDomDest.Pais = dllCartaPorte.c_Pais.MEX;// "MEX";
                            myDomDest.NumeroExterior = d.DomicilioDest.NumeroExterior; //"211";
                            myDomDest.Municipio = d.DomicilioDest.Municipio;
                            myDomDest.Localidad = d.DomicilioDest.Localidad;
                            myDomDest.Estado = d.DomicilioDest.Estado;
                            myDomDest.Colonia = d.DomicilioDest.Colonia;
                            myDomDest.CodigoPostal = d.DomicilioDest.CodigoPostal;
                            myDomDest.Calle = d.DomicilioDest.Calle;
                            myDomDest.Referencia = d.DomicilioDest.Referencia;







                            myUbicacion.Add(new dllCartaPorte.CartaPorteUbicacion
                            {

                                IDUbicacion = d.Destino.IDUbicacion,
                                TipoUbicacion = d.Destino.TipoUbicacion,
                                RFCRemitenteDestinatario = d.Destino.RFCRemitenteDestinatario,
                                NombreRemitenteDestinatario= d.Destino.NombreRemitenteDestinatario,
                                
                                FechaHoraSalidaLlegada = d.Destino.FechaHoraSalidaLlegada,
                                DistanciaRecorrida = d.Destino.DistanciaRecorrida,
                                DistanciaRecorridaSpecified = true,

                                //TipoEstacion = d.TipoEstacionDestino,
                                //TipoEstacionSpecified = false,
                                //DistanciaRecorrida = d.DistanciaRecorridaDestino,
                                //DistanciaRecorridaSpecified = true,
                                //Destino = myDestino,
                                Domicilio = myDomDest,

                            });

                            dcmTotalDist += d.Destino.DistanciaRecorrida;

                        }
                        catch (Exception ex) { }









                    }


                    CartaPorte.Ubicaciones = myUbicacion.ToArray();





                    CartaPorte.TotalDistRec = dcmTotalDist;
                    CartaPorte.TotalDistRecSpecified = true;
                    CartaPorte.TranspInternac = dllCartaPorte.CartaPorteTranspInternac.No;//"No";

                    foreach (var item in dataJson.Mecancancias.Mercancia)
                    {
                        List<dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta> lstCanTraspo = new List<dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta>();
                        string Claveunidad = item.ClaveUnidad.Split('-')[0];
                        string unidad = item.ClaveUnidad.Split('-')[1];
                        Boolean isValor = true;
                        Boolean isCantidad = true;
                        Boolean isBienesTransp = true;
                        string DescrpMerca = "";
                        Boolean isClaveUni = true;

                        DescrpMerca = item.Descripcion;

                        //if (string.IsNullOrEmpty(item.ValorMercancia.ToString()) || item.ValorMercancia == 0)
                        //{
                        //    isValor = false;
                        //}
                        //if (string.IsNullOrEmpty(item.Cantidad.ToString()) || item.Cantidad == 0)
                        //{
                        //    isCantidad = false;
                        //}



                        lstCanTraspo.Add(new dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta
                        {
                            IDOrigen = item.CantidadTransporta.IDOrigen,
                            IDDestino = item.CantidadTransporta.IDDestino,
                            Cantidad = item.Cantidad //item.CantidadTransporta.Cantidad


                        });

                        decimal dcmPesoKg = 0;
                        

                        if (item.PesoEnKg == 0)
                        { dcmPesoKg = decimal.Parse("0.001");

                        dcmPesoBrutoTotal += dcmPesoKg;
                        
                        }
                        else { 
                            
                            dcmPesoKg = item.PesoEnKg;
                            dcmPesoBrutoTotal += dcmPesoKg;
                        
                        }

                        Boolean isMatPel = false;
                        Boolean isMatPelCl = false;
                        Boolean isEmbalaje = false;
                        if (!string.IsNullOrEmpty(item.MaterialPeligroso))
                        {

                            if (item.MaterialPeligroso == "Si")
                            {

                                isMatPel = true;
                                isMatPelCl = true;
                                isEmbalaje = true;

                            }

                            if (item.MaterialPeligroso == "No")
                            {

                                isMatPel = true;
                               

                            }

                            

                        }
                       


                        myMercancias.Add(new dllCartaPorte.CartaPorteMercanciasMercancia

                        {

                            Descripcion = DescrpMerca,
                            ClaveUnidad = Claveunidad,// "XNG",
                            Unidad = unidad,
                            Cantidad = item.Cantidad,
                            PesoEnKg = dcmPesoKg,
                            BienesTransp = item.BienesTransp,
                            CantidadTransporta = lstCanTraspo.ToArray(),
                            MaterialPeligroso = item.MaterialPeligroso,
                            MaterialPeligrosoSpecified = isMatPel,
                            CveMaterialPeligroso = item.ClaveMatPel,
                            CveMaterialPeligrosoSpecified = isMatPelCl,
                            Embalaje = item.Embalaje,
                            EmbalajeSpecified = isEmbalaje
                            //Moneda = dllCartaPorte.c_Moneda.MXN,//"MXN",
                            //MonedaSpecified = true,




                            //  ClaveUnidadSpecified = isClaveUni,

                            //  CantidadSpecified = isCantidad,
                            //  ValorMercancia = item.ValorMercancia,
                            //  ValorMercanciaSpecified = isValor,
                            //item.PesoEnKg,

                            // BienesTranspSpecified = isBienesTransp,





                        });

                      
                    }

                    /*nodo AutoTransporte*/
                    myAutoTransp.PermSCT = dataJson.Autotransporte.PermSCT.Split('-')[0];//dllCartaPorte.c_TipoPermiso.TPAF01;  //"TPAF01";
                    myAutoTransp.NumPermisoSCT = dataJson.Autotransporte.NumPermisoSCT;//"NUMPERM2021";


                    /*nodo identificacion*/

                    myIndet.PlacaVM = dataJson.Autotransporte.IdentificacionVehicular.PlacaVM;
                    myIndet.ConfigVehicular = dataJson.Autotransporte.IdentificacionVehicular.ConfigVehicular; //dllCartaPorte.c_ConfigAutotransporte.C2; //"C2";
                    myIndet.AnioModeloVM = dataJson.Autotransporte.IdentificacionVehicular.AnioModeloVM;// 2020;

                    /*nodo seguros*/


                    mySeguro.AseguraRespCivil = dataJson.Autotransporte.Seguros.AseguraRespCivil; //"POLIZA2021";
                    mySeguro.PolizaRespCivil = dataJson.Autotransporte.Seguros.PolizaRespCivil; //"QUALITAS";
                    mySeguro.AseguraCarga = dataJson.Autotransporte.Seguros.AseguraCarga;


                    foreach (var rem in dataJson.Autotransporte.Remolques)
                    {

                        myRemolque.Add(new dllCartaPorte.CartaPorteMercanciasAutotransporteRemolque
                        {
                            Placa = rem.Placa,
                            SubTipoRem = rem.SubTipoRem
                        });

                    }


                    myAutoTransp.IdentificacionVehicular = myIndet;
                    myAutoTransp.Seguros = mySeguro;
                    myAutoTransp.Remolques = myRemolque.ToArray();



                    myMercancia.NumTotalMercancias = dataJson.Mecancancias.NumTotalMercancias;
                    //if (dataJson.Mecancancias.PesoBrutoTotal == 0)
                    //{
                         myMercancia.PesoBrutoTotal = dcmPesoBrutoTotal;

                        //      myMercancia.PesoNetoTotalSpecified = true;

                    //}
                    //else {

                    //    myMercancia.PesoBrutoTotal = dcmPesoBrutoTotal;
                    //}



                    myMercancia.UnidadPeso = dataJson.Mecancancias.UnidadPeso.Split('-')[0];
                    myMercancia.Mercancia = myMercancias.ToArray();




                    myMercancia.Autotransporte = myAutoTransp;







                    //     myFigTrans.CveTransporte = dataJson.FiguraTransporte.CveTransporte;//dllCartaPorte.c_CveTransporte.Item01;

                    //foreach (var o in dataJson.FiguraTransporte)
                    //{

                    //}

                    foreach (var rem in dataJson.FiguraTransporte.TiposFigura)
                    {
                        myFiguraTransporte.Add(new dllCartaPorte.CartaPorteTiposFigura
                        {
                            NumLicencia = rem.NumLicencia,
                            RFCFigura = rem.RFCFigura,
                            NombreFigura = rem.NombreFigura


                        });


                    }




                    CartaPorte.Mercancias = myMercancia;
                    CartaPorte.FiguraTransporte = myFiguraTransporte.ToArray();


                    MyCFD20.Complemento = new dlleFac.ComprobanteComplemento();

                    XmlDocument docPago = new XmlDocument();
                    XmlSerializerNamespaces xmlNameSpcePago = new XmlSerializerNamespaces();
                    xmlNameSpcePago.Add("cartaporte20", "http://www.sat.gob.mx/CartaPorte20");
                    using (XmlWriter writer = docPago.CreateNavigator().AppendChild())
                    {
                        new XmlSerializer(CartaPorte.GetType()).Serialize(writer, CartaPorte, xmlNameSpcePago);

                    }


                    MyCFD20.Complemento.Any = new XmlElement[1];
                    MyCFD20.Complemento.Any[0] = docPago.DocumentElement;
                }

                if (f.strCadenaOriginal.Contains("NumPermisoSPC"))
                {

                    try
                    {

                        dataServParCons.FillData dataSPC = JsonConvert.DeserializeObject<dataServParCons.FillData>(f.strCadenaOriginal);

                        dllSPC.parcialesconstruccion ServParCons = new dllSPC.parcialesconstruccion();
                        dllSPC.parcialesconstruccionInmueble pci = new dllSPC.parcialesconstruccionInmueble();

                        pci.Calle = dataSPC.Calle;
                        pci.NoInterior = dataSPC.NoInterior;
                        pci.NoExterior = dataSPC.NoExterior;
                        pci.Colonia = dataSPC.Colonia;
                        pci.Localidad = dataSPC.Localidad;
                        pci.Municipio = dataSPC.Municipio;
                        pci.Estado = dataSPC.Estado;
                        pci.CodigoPostal = dataSPC.CodigoPostal;


                        ServParCons.Version = "1.0";
                        ServParCons.NumPerLicoAut = dataSPC.NumPermisoSPC;
                        ServParCons.Inmueble = pci;
                        
                        MyCFD20.Complemento = new dlleFac.ComprobanteComplemento();

                    XmlDocument docSPC = new XmlDocument();
                    XmlSerializerNamespaces xmlNameSpceSPC = new XmlSerializerNamespaces();
                    xmlNameSpceSPC.Add("servicioparcial", "http://www.sat.gob.mx/servicioparcialconstruccion");
                    using (XmlWriter writer = docSPC.CreateNavigator().AppendChild())
                    {
                        new XmlSerializer(ServParCons.GetType()).Serialize(writer, ServParCons, xmlNameSpceSPC);

                    }


                    MyCFD20.Complemento.Any = new XmlElement[1];
                    MyCFD20.Complemento.Any[0] = docSPC.DocumentElement;

                    }
                    catch (Exception ex) {

                        MessageBox.Show("complemento SPC error " + ex);
                    
                    }
                
                
                
                
                }



            }
            catch (Exception e) { 



            
            }

            List<dlleFac.ComprobanteImpuestosTraslado> Mytraslado = new List<dlleFac.ComprobanteImpuestosTraslado>();

            List<dlleFac.ComprobanteImpuestosRetencion> MyRet = new List<dlleFac.ComprobanteImpuestosRetencion>();
            
            if (isIva16)
            {
                dlleFac.ComprobanteImpuestosTraslado MyIvaT = new dlleFac.ComprobanteImpuestosTraslado()
                    {
                        Base = decimal.Parse(sumaBaseIva16.ToString("#0.00")),
                        Importe = decimal.Parse(sumaIva.ToString("#0.00")),
                        ImporteSpecified = true,
                        Impuesto = dlleFac.c_Impuesto.Item002,
                        TipoFactor = dlleFac.c_TipoFactor.Tasa,
                        TasaOCuota = decimal.Parse("0.160000"),
                        TasaOCuotaSpecified = true



                    };


                Mytraslado.Add(MyIvaT);
            }

            if (isIva0)
            {

                dlleFac.ComprobanteImpuestosTraslado MyIvaT0 = new dlleFac.ComprobanteImpuestosTraslado()
                {
                    Base = decimal.Parse(sumaBaseIva0.ToString("#0.00")),
                    Importe = decimal.Parse("0.00"),
                    ImporteSpecified = true,
                    Impuesto = dlleFac.c_Impuesto.Item002,
                    TipoFactor = dlleFac.c_TipoFactor.Tasa,
                    TasaOCuota = decimal.Parse("0.000000"),//dlleFac.c_TasaOCuota.Item0160000    
                    TasaOCuotaSpecified = true
                };
                Mytraslado.Add(MyIvaT0);

            }


            if ((f.intID_Tipo_CFD == 1) || (f.intID_Tipo_CFD == 2) || (f.intID_Tipo_CFD == 5))
            {
               


                if (f.dcmRetIVA > 0)
                {


                   
                    dlleFac.ComprobanteImpuestosRetencion MyIvaRet = new dlleFac.ComprobanteImpuestosRetencion()
                    {
                        Importe = decimal.Parse(sumaRetIva.ToString("#0.00")),
                        Impuesto = dlleFac.c_Impuesto.Item002


                    };
                    MyRet.Add(MyIvaRet);
                }


                if (f.dcmRetISR >0)
                {

                    dlleFac.ComprobanteImpuestosRetencion MyIsrRet = new dlleFac.ComprobanteImpuestosRetencion()
                    {
                        Importe = decimal.Parse(sumaRetIsr.ToString("#0.00")),
                        Impuesto = dlleFac.c_Impuesto.Item001


                    };
                    MyRet.Add(MyIsrRet);

                }

                if (f.dcmRetIEPS > 0)
                {

                    dlleFac.ComprobanteImpuestosRetencion MyIsrRet = new dlleFac.ComprobanteImpuestosRetencion()
                    {
                        Importe = decimal.Parse(sumaRetIeps.ToString("#0.00")),
                        Impuesto = dlleFac.c_Impuesto.Item001


                    };
                    MyRet.Add(MyIsrRet);

                }




                
                Decimal rIsr = f.dcmRetISR.Value;
                totalRet = sumaRetIva + sumaRetIsr + sumaRetIeps;

                  if (radOption1.IsChecked == true)
                {

                    totalRet = decimal.Parse(totalRet.ToString("#0.00"));

                }
                else {

                    totalRet = Math.Truncate(100 * totalRet) / 100;
                
                }



               
                if (totalRet > 0) {
                    isRet = true;
                
                }



            }





            dlleFac.ComprobanteImpuestos Impuestos = new dlleFac.ComprobanteImpuestos()
            {
                TotalImpuestosTrasladados = decimal.Parse(sumaIva.ToString("#0.00")),
                Traslados = Mytraslado.ToArray(),
                TotalImpuestosTrasladadosSpecified = true,


              
                TotalImpuestosRetenidos = decimal.Parse(totalRet.ToString("#0.00")),
                Retenciones = MyRet.ToArray(),
                TotalImpuestosRetenidosSpecified = isRet,
                


            };






            MyCFD20.Impuestos = Impuestos;

            decimal granSubtotal = sumaSubtotal + f.dcmDescuento.Value; //f.dcmSubTotal + f.dcmDescuento.Value;
            MyCFD20.SubTotal = decimal.Parse(granSubtotal.ToString("#0.00"));
            decimal dcmTotal = MyCFD20.SubTotal + sumaIva - totalRet;
            


            if (radOption1.IsChecked == true)
            {

                dcmTotal = decimal.Parse(dcmTotal.ToString("#0.00"));

            }
            else
            {

                dcmTotal = Math.Truncate(100 * dcmTotal) / 100;

            }



            MyCFD20.Total = dcmTotal;

         


        }



        private void FillCFDPago30(Factura f, Direcciones_Fiscales direccionEmisor, Direcciones_Fiscales direccionEmision, Direcciones_Fiscales direccionReceptor, List<dlleFac.ComprobanteConcepto> myConceptos, dlleFac.Comprobante MyCFD20)
        {



            //Folios myFolio = f.Certificates.Folios.Where(p=>p.chrStatus == "A" && p.intIDtipoCFD == f.intID_Tipo_CFD).First();
            Folios myFolio = f.CFD.Folios; //.Where(p=>p.chrStatus == "A").First();
            DateTime fechaAprobacion = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss"));



            MyCFD20.Fecha = fechaAprobacion;

            //MyCFD20.noAprobacion = myFolio.intNumero_Aprovacion.ToString();
            //MyCFD20.anoAprobacion = myFolio.strAño_Aprovacion;

            MyCFD20.LugarExpedicion = direccionEmision.strCodigoPostal; ; //dlleFac.c_CodigoPostal.Item01276;
            //xxx MyCFD20.numCuentaPago = string.IsNullOrEmpty(f.Clientes.strWebSite) ? "NO IDENTIFICADO" : f.Clientes.strWebSite;


            MyCFD20.TipoDeComprobante = dlleFac.c_TipoDeComprobante.P;




            MyCFD20.Serie = f.strSerie;
            MyCFD20.Folio = f.strFolio;

            MyCFD20.Moneda = "XXX";





            MyCFD20.SubTotal = decimal.Parse("0");
            MyCFD20.Total = decimal.Parse("0");



            MyCFD20.Emisor = new dlleFac.ComprobanteEmisor
            {
                RegimenFiscal = f.Empresa.strWebSite.Split('-')[0],
                Rfc = f.Empresa.strRFC,
                Nombre = f.Empresa.strRazonSocial


            };


            MyCFD20.Receptor = new dlleFac.ComprobanteReceptor
            {
                Rfc = f.Clientes.strRFC,
                Nombre = f.Clientes.strRazonSocial,
                UsoCFDI = f.MotivoDesc,
                RegimenFiscalReceptor = f.Clientes.strGiro,
                DomicilioFiscalReceptor = direccionReceptor.strCodigoPostal



            };
            eFacDBEntities db = new eFacDBEntities();
            //  var producPago = db.Productos.Where(p => p.strCodigo.Equals("84111506")&& p.strNombre.Contains("Pago")).FirstOrDefault();




            myConceptos.Add(new dlleFac.ComprobanteConcepto
            {


                Cantidad = decimal.Parse(("1")),
                ClaveProdServ = "84111506", //producPago.strCodigoBarras,
                ClaveUnidad = "ACT",
                Descripcion = "Pago",//producPago.strNombre,
                ValorUnitario = decimal.Parse("0"),
                Importe = decimal.Parse("0"),
                ObjetoImp = dlleFac.c_ObjetoImp.Item01


            });
            MyCFD20.Conceptos = myConceptos.ToArray();



            dllPag.Pagos Pago = new dllPag.Pagos();
            dllPag.PagosTotales totalesPago = new dllPag.PagosTotales();
            dllPag.PagosPagoDoctoRelacionadoImpuestosDR myPagoImp = null;//new dllPag.PagosPagoDoctoRelacionadoImpuestosDR();
            //dllPag.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR myPagoImpTras = new dllPag.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR();
            //dllPag.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR myPagoImpRet = new dllPag.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR();
            List<dllPag.PagosPago> myPagos = new List<dllPag.PagosPago>();





            List<dllPag.PagosPagoDoctoRelacionado> myPagoRel = null;
            dllPag.PagosPagoImpuestosP myImpuestosP = null;
            List<dllPag.PagosPagoImpuestosPTrasladoP> myPagoImpTrasP = new List<dllPag.PagosPagoImpuestosPTrasladoP>();
            List<dllPag.PagosPagoImpuestosPRetencionP> myPagoImpRetP = new List<dllPag.PagosPagoImpuestosPRetencionP>();
            List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR> myPagoListImpRet = new List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR>();
            List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR> myPagoListTrasl = new List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR>();
            string myFormaPago = "";
            decimal NodototalIva = 0;
            decimal NodototalBaseIva = 0;
            Boolean isIva = false;
            decimal NodototalRetIva = 0;
            Boolean isRetIva = false;
            decimal NodototalRetIsr = 0;
            Boolean isRetIsr = false;
            decimal NodototaRetIeps = 0;
            Boolean isRetIeps = false;

            DataCompPago.DataComplementoPago dataJson = JsonConvert.DeserializeObject<DataCompPago.DataComplementoPago>(f.strCadenaOriginal);

            if (f.strObervaciones == "FACTORAJE")
            {


                /***metodo 1***/








                foreach (var item in dataJson.fillData)  //f.Detalle_Factura)
                {
                    myPagoRel = new List<dllPag.PagosPagoDoctoRelacionado>();
                    //List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR> myPagoListImpRet = new List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR>();
                    // List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR> myPagoListTrasl = new List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR>();

                    //   string[] words = item.strPatida.Split('|');
                    //    string myUUID = words[0];
                    //    string myFol = words[1];
                    //    string mySer = words[2];
                    myFormaPago = item.strFormaPago.Split('-')[0];

                    myPagoRel.Add(new dllPag.PagosPagoDoctoRelacionado
                    {



                        IdDocumento = item.strUUID,// myUUID,
                        Serie = item.strSerie,//mySer,
                        Folio = item.strFolio,// myFol,
                        MonedaDR = item.strMoneda,//"MXN",
                        //MetodoDePagoDR = "PPD",
                        NumParcialidad = f.CondPago,
                        ImpSaldoAnt = decimal.Parse(item.dcmImporte.ToString("#0.00")),
                        // ImpSaldoAntSpecified = true,
                        ImpPagado = decimal.Parse(item.dcmPagado.ToString("#0.00")),
                        //ImpPagadoSpecified = true,
                        ImpSaldoInsoluto = decimal.Parse(item.dcmPendiente.ToString("#0.00")),
                        //ImpSaldoInsolutoSpecified = true,



                    });


                    myPagos.Add(new dllPag.PagosPago
                    {

                        CtaBeneficiario = f.Empresa.strCedula,

                        CtaOrdenante = f.Clientes.strWebSite,
                        RfcEmisorCtaBen = f.Destino,
                        RfcEmisorCtaOrd = f.Origen,
                        NumOperacion = f.RecogerEn,
                        Monto = decimal.Parse(dataJson.dcmTotal.ToString("#0.00")),
                        MonedaP = dataJson.strMoneda,//"MXN",
                        FormaDePagoP = myFormaPago, //myFormaPago.Substring(0, 2),
                        FechaPago = f.dtmFecha,
                        DoctoRelacionado = myPagoRel.ToArray()


                    });


                }

                totalesPago.MontoTotalPagos = decimal.Parse(f.dcmTotal.ToString("#0.00"));

            }


            /*** Metodo 2 ***/
            // List<dllPag.PagosPagoDoctoRelacionado> myPagoRel = new List<dllPag.PagosPagoDoctoRelacionado>();



            else
            {

                myPagoRel = new List<dllPag.PagosPagoDoctoRelacionado>();
                myImpuestosP = new dllPag.PagosPagoImpuestosP();



                string strMoneda = "";
                string tipoCambio = "";
                Boolean valueTC = true;


                foreach (var item in dataJson.fillData)  //f.Detalle_Factura)
                {
                    myPagoImp = new dllPag.PagosPagoDoctoRelacionadoImpuestosDR();
                    myPagoListTrasl = new List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR>();
                    myPagoListImpRet = new List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR>();
                    myFormaPago = item.strFormaPago.Split('-')[0];



                    strMoneda = item.strMoneda;
                    tipoCambio = dataJson.strTipoCambio;

                    //if (dataJson.strMoneda == "USD") {
                    //     valueTC = true;             
                    //}




                    foreach (var i in item.fillDataImpuestosDR)
                    {


                        if (i.boolTraslado == true)
                        {
                            // myPagoListTrasl = new List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR>();

                            myPagoListTrasl.Add(new dllPag.PagosPagoDoctoRelacionadoImpuestosDRTrasladoDR
                            {
                                TipoFactorDR = dllPag.c_TipoFactor.Tasa,
                                TasaOCuotaDR = decimal.Parse(i.dcmTasaOCuotaDR.ToString("#0.000000")),
                                TasaOCuotaDRSpecified = true,
                                ImpuestoDR = i.strImpuestoDR,//dllPag.c_Impuesto.Item002,
                                ImporteDR = i.dcmImporteDR,
                                ImporteDRSpecified = true,
                                BaseDR = i.dcmBaseDR




                            });


                        }

                        if (i.boolRetencion == true)
                        {
                            //  myPagoListImpRet = new List<dllPag.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR>();

                            myPagoListImpRet.Add(new dllPag.PagosPagoDoctoRelacionadoImpuestosDRRetencionDR
                            {
                                TipoFactorDR = dllPag.c_TipoFactor.Tasa,
                                TasaOCuotaDR = decimal.Parse(i.dcmTasaOCuotaDR.ToString("#0.000000")),
                                //TasaOCuotaDRSpecified = true,
                                ImpuestoDR = i.strImpuestoDR,//dllPag.c_Impuesto.Item002,
                                ImporteDR = i.dcmImporteDR,
                                //  ImporteDRSpecified = true,
                                BaseDR = i.dcmBaseDR




                            });




                        }

                    }



                    myPagoImp.TrasladosDR = myPagoListTrasl.ToArray();
                    myPagoImp.RetencionesDR = myPagoListImpRet.ToArray();





                    myPagoRel.Add(new dllPag.PagosPagoDoctoRelacionado
                    {



                        IdDocumento = item.strUUID,//myUUID,
                        Serie = item.strSerie,
                        Folio = item.strFolio,
                        MonedaDR = strMoneda,
                        // MetodoDePagoDR = "PPD",
                        NumParcialidad = f.CondPago,
                        ImpSaldoAnt = decimal.Parse(item.dcmImporte.ToString("#0.00")),
                        //  ImpSaldoAntSpecified = true,
                        ImpPagado = decimal.Parse(item.dcmPagado.ToString("#0.00")),
                        //   ImpPagadoSpecified = true,
                        ImpSaldoInsoluto = decimal.Parse(item.dcmPendiente.ToString("#0.00")),
                        //   ImpSaldoInsolutoSpecified = true,
                        ObjetoImpDR = dllPag.c_ObjetoImp.Item02,

                        EquivalenciaDR = 1,
                        EquivalenciaDRSpecified = true,
                        ImpuestosDR = myPagoImp


                    });
                }


                string CtaBeneficiario = f.Empresa.strCedula;
                string CtaOrdenante = f.Clientes.strWebSite;

                if (myFormaPago == "01")
                {
                    CtaBeneficiario = "";
                    CtaOrdenante = "";

                }


                foreach (var i in dataJson.fillDataImpuestosP)
                {

                    if (i.boolTraslado)
                    {

                        myPagoImpTrasP.Add(new dllPag.PagosPagoImpuestosPTrasladoP
                        {
                            BaseP = i.dcmBaseP,
                            ImporteP = i.dcmImporteP,
                            ImportePSpecified = true,
                            ImpuestoP = dllPag.c_Impuesto.Item002,//"002",
                            TipoFactorP = dllPag.c_TipoFactor.Tasa,
                            TasaOCuotaP = decimal.Parse("0.160000"),
                            TasaOCuotaPSpecified = true
                        });

                        NodototalIva = i.dcmImporteP;
                        NodototalBaseIva = i.dcmBaseP;
                        isIva = true;



                    }

                    if (i.boolRetencion)
                    {
                        if (i.strImpuestoP == "001")
                        {
                            myPagoImpRetP.Add(new dllPag.PagosPagoImpuestosPRetencionP
                            {

                                ImporteP = i.dcmImporteP,
                                ImpuestoP = dllPag.c_Impuesto.Item001,

                            });

                            NodototalRetIsr = i.dcmImporteP;
                            isRetIsr = true;
                        }

                        if (i.strImpuestoP == "002")
                        {
                            myPagoImpRetP.Add(new dllPag.PagosPagoImpuestosPRetencionP
                            {

                                ImporteP = i.dcmImporteP,
                                ImpuestoP = dllPag.c_Impuesto.Item002,

                            });

                            NodototalRetIva = i.dcmImporteP;
                            isRetIva = true;
                        }
                        if (i.strImpuestoP == "003")
                        {
                            myPagoImpRetP.Add(new dllPag.PagosPagoImpuestosPRetencionP
                            {

                                ImporteP = i.dcmImporteP,
                                ImpuestoP = dllPag.c_Impuesto.Item003,

                            });

                            NodototaRetIeps = i.dcmImporteP;
                            isRetIeps = true;
                        }

                    }




                }




                myImpuestosP.TrasladosP = myPagoImpTrasP.ToArray();
                myImpuestosP.RetencionesP = myPagoImpRetP.ToArray();

                myPagos.Add(new dllPag.PagosPago
                {

                    CtaBeneficiario = CtaBeneficiario,
                    CtaOrdenante = CtaOrdenante,
                    RfcEmisorCtaBen = f.Destino,
                    RfcEmisorCtaOrd = f.Origen,
                    NumOperacion = f.RecogerEn,
                    Monto = decimal.Parse(f.dcmTotal.ToString("#0.00")),
                    MonedaP = strMoneda,
                    FormaDePagoP = myFormaPago,
                    FechaPago = f.dtmFecha,
                    DoctoRelacionado = myPagoRel.ToArray(),
                    TipoCambioP = decimal.Parse(tipoCambio),
                    TipoCambioPSpecified = valueTC,
                    ImpuestosP = myImpuestosP

                });


                totalesPago.MontoTotalPagos = decimal.Parse(f.dcmTotal.ToString("#0.00"));

                if (isIva)
                {
                    totalesPago.TotalTrasladosImpuestoIVA16 = NodototalIva;
                    totalesPago.TotalTrasladosImpuestoIVA16Specified = isIva;
                    totalesPago.TotalTrasladosBaseIVA16 = NodototalBaseIva;
                    totalesPago.TotalTrasladosBaseIVA16Specified = isIva;
                }
                //totalesPago.TotalTrasladosBaseIVA16 = 0;
                //totalesPago.TotalTrasladosBaseIVA16Specified = true;
                if (isRetIva)
                {
                    totalesPago.TotalRetencionesIVA = NodototalRetIva;
                    totalesPago.TotalRetencionesIVASpecified = isRetIva;
                }
                if (isRetIsr)
                {
                    totalesPago.TotalRetencionesISR = NodototalRetIsr;
                    totalesPago.TotalRetencionesISRSpecified = isRetIsr;
                }
                if (isRetIeps)
                {
                    totalesPago.TotalRetencionesIEPS = NodototaRetIeps;
                    totalesPago.TotalRetencionesIEPSSpecified = isRetIeps;
                }


            }

            Pago.Version = "2.0";
            Pago.Pago = myPagos.ToArray();
            Pago.Totales = totalesPago;



            // MyCFD20.Complemento = new dlleFac.ComprobanteComplemento[1];
            MyCFD20.Complemento = new dlleFac.ComprobanteComplemento();

            XmlDocument docPago = new XmlDocument();
            XmlSerializerNamespaces xmlNameSpcePago = new XmlSerializerNamespaces();
            xmlNameSpcePago.Add("pago20", "http://www.sat.gob.mx/Pagos20");
            using (XmlWriter writer = docPago.CreateNavigator().AppendChild())
            {
                new XmlSerializer(Pago.GetType()).Serialize(writer, Pago, xmlNameSpcePago);

            }


            MyCFD20.Complemento.Any = new XmlElement[1];
            MyCFD20.Complemento.Any[0] = docPago.DocumentElement;




            //  myComplemento.Pago = Pago;
            // MyCFD20.Complemento = new dlleFac.ComprobanteComplemento();
            //  MyCFD20.Complemento.Pago = Pago;



            //     dlleFac.ComprobanteComplemento myComplemento = new dlleFac.ComprobanteComplemento();
            //     myComplemento.Pago = Pago;
            //     MyCFD20.Complemento = myComplemento;




        }

       

        private void FillCFDCartaPorte30(Factura f, Direcciones_Fiscales direccionEmisor, Direcciones_Fiscales direccionEmision, Direcciones_Fiscales direccionReceptor, List<dlleFac.ComprobanteConcepto> myConceptos, dlleFac.Comprobante MyCFD20)
        {

            decimal sumaRetIeps = 0;
            decimal sumaRetIva = 0;
            decimal sumaRetIsr = 0;
            decimal sumaSubtotal = 0;
            decimal sumaIva = 0;
            decimal totalRet = 0;
            decimal sumaBaseIva16 = 0;
            decimal sumaBaseIva0 = 0;
            Boolean isIva0 = false;
            Boolean isRet = false;
            Boolean isIva16 = false;


            //Folios myFolio = f.Certificates.Folios.Where(p=>p.chrStatus == "A" && p.intIDtipoCFD == f.intID_Tipo_CFD).First();
            Folios myFolio = f.CFD.Folios; //.Where(p=>p.chrStatus == "A").First();
            DateTime fechaAprobacion = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss"));



            MyCFD20.Fecha = fechaAprobacion;

            //MyCFD20.noAprobacion = myFolio.intNumero_Aprovacion.ToString();
            //MyCFD20.anoAprobacion = myFolio.strAño_Aprovacion;

            MyCFD20.LugarExpedicion = direccionEmision.strCodigoPostal; ; //dlleFac.c_CodigoPostal.Item01276;
            //xxx MyCFD20.numCuentaPago = string.IsNullOrEmpty(f.Clientes.strWebSite) ? "NO IDENTIFICADO" : f.Clientes.strWebSite;

         
                MyCFD20.TipoDeComprobante = dlleFac.c_TipoDeComprobante.T;





            MyCFD20.Serie = f.strSerie;
            MyCFD20.Folio = f.strFolio;
          //  MyCFD20.FormaPago = f.strForma_Pago;
         //   MyCFD20.FormaPagoSpecified = true;
            MyCFD20.CondicionesDePago = f.CondPago;
         //   MyCFD20.MetodoPago = f.MetodoPago;
         //   MyCFD20.MetodoPagoSpecified = true;
            //xxx MyCFD20.motivodescuento = f.MotivoDesc;

            MyCFD20.Moneda = f.Divisa;
            if (f.Divisa != "MXN")
            {
                MyCFD20.TipoCambio = f.TipoCambio.Value;
                MyCFD20.TipoCambioSpecified = true;
            }



            MyCFD20.Descuento = decimal.Parse(f.dcmDescuento.Value.ToString("#0.00"));
            if (f.dcmDescuento > 0)
            {

                MyCFD20.DescuentoSpecified = true;
            }

            decimal granSubtotal = f.dcmSubTotal + f.dcmDescuento.Value;

            MyCFD20.SubTotal = decimal.Parse("0");
            MyCFD20.Total = decimal.Parse("0");
            if (!string.IsNullOrEmpty(f.strNumeroContrato))
            {
                List<dlleFac.ComprobanteCfdiRelacionados> myRelacion = new List<dlleFac.ComprobanteCfdiRelacionados>();
                {
                    List<dlleFac.ComprobanteCfdiRelacionadosCfdiRelacionado> myUUID = new List<dlleFac.ComprobanteCfdiRelacionadosCfdiRelacionado>();
                    List<string> uuids2 = new List<string>();
                    var lstuuid = f.strNumeroContrato;

                    string[] uuids = lstuuid.Split('|');

                    foreach (var word in uuids)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            int intId_fac = int.Parse(word);

                            eFacDBEntities db = new eFacDBEntities();
                            Factura factura = db.Factura.Where(fac => fac.intID == intId_fac).First();


                            uuids2.Add(factura.strSelloDigital);
                        }

                    }

                    foreach (string item in uuids2)
                    {

                        myUUID.Add(new dlleFac.ComprobanteCfdiRelacionadosCfdiRelacionado
                        {


                            UUID = item

                        });

                    }



                    myRelacion.Add(new dlleFac.ComprobanteCfdiRelacionados
                    {

                        TipoRelacion = f.intEstimacion,
                        CfdiRelacionado = myUUID.ToArray()



                    });






                    MyCFD20.CfdiRelacionados = myRelacion.ToArray();
                }
            }


            MyCFD20.Emisor = new dlleFac.ComprobanteEmisor
            {
                RegimenFiscal = f.Empresa.strWebSite,
                Rfc = f.Empresa.strRFC,
                Nombre = f.Empresa.strRazonSocial

                /*
               DomicilioFiscal = new dlleFac.t_UbicacionFiscal
               {
                   calle = direccionEmisor.strCalle,
                   noInterior = direccionEmisor.strNoInterior,
                   noExterior = String.IsNullOrEmpty(direccionEmisor.strNoExterior.Trim()) ? "" : direccionEmisor.strNoExterior.Trim(),
                   pais = direccionEmisor.Paises.strNombrePais,
                   municipio = direccionEmisor.strMunicipio,
                   localidad = direccionEmisor.strPoblacionLocalidad,
                   estado = direccionEmisor.Estado.strNombreEstado,
                   codigoPostal = direccionEmisor.strCodigoPostal,
                   colonia = direccionEmisor.strColonia
               },


               ExpedidoEn = new dlleFac.t_Ubicacion
               {
                   calle = direccionEmision.strCalle,
                   noInterior = direccionEmision.strNoInterior,
                   noExterior = direccionEmision.strNoExterior,
                   pais = direccionEmision.Paises.strNombrePais,
                   municipio = direccionEmision.strMunicipio,
                   localidad = direccionEmision.strPoblacionLocalidad,
                   estado = direccionEmision.Estado.strNombreEstado,
                   codigoPostal = direccionEmision.strCodigoPostal,
                   colonia = direccionEmision.strColonia
               }*/
            };


            MyCFD20.Receptor = new dlleFac.ComprobanteReceptor
            {
                Rfc = f.Clientes.strRFC,
                Nombre = f.Clientes.strRazonSocial,
                RegimenFiscalReceptor = f.Clientes.strGiro,
                DomicilioFiscalReceptor = direccionReceptor.strCodigoPostal,
                UsoCFDI = "S01"


                //ResidenciaFiscal = dlleFac.c_Pais.MEX,
                //ResidenciaFiscalSpecified = true

                /* 
                Domicilio = new dlleFac.t_Ubicacion
               {
                   calle = direccionReceptor.strCalle,
                   noInterior = direccionReceptor.strNoInterior,
                   noExterior = direccionReceptor.strNoExterior,
                   pais = direccionReceptor.Paises.strNombrePais,
                   municipio = direccionReceptor.strMunicipio,
                   localidad = direccionReceptor.strPoblacionLocalidad,
                   estado = direccionReceptor.Estado.strNombreEstado,
                   codigoPostal = direccionReceptor.strCodigoPostal,
                   colonia = direccionReceptor.strColonia

               }*/
            };





            foreach (var item in f.Detalle_Factura)
            {

                if (item.strPatida.Equals("0"))
                {
                    dlleFac.ComprobanteConceptoImpuestos
                           comImpuestos = new dlleFac.ComprobanteConceptoImpuestos();


                    dlleFac.ComprobanteConceptoImpuestosTraslado
                       myConceptoTranslado = new dlleFac.ComprobanteConceptoImpuestosTraslado();

                    myConceptoTranslado.Impuesto = dlleFac.c_Impuesto.Item002;

                    myConceptoTranslado.TasaOCuota = decimal.Parse(item.dcmIVA.Value.ToString("#0.000000")); //item.dcmIVA.Value;
                    myConceptoTranslado.TasaOCuotaSpecified = true;

                    if (item.dcmIVA > 0)
                    {
                        isIva16 = true;
                        sumaBaseIva16 += item.dcmImporte;
                    }
                    if (item.dcmIVA == 0)
                    {
                        isIva0 = true;
                        sumaBaseIva0 += item.dcmImporte;
                    }

                    decimal BaseTraslado = item.dcmPrecioUnitario * item.dcmCantidad - item.dcmDescuento;

                    myConceptoTranslado.Base = decimal.Parse(BaseTraslado.ToString("#0.000"));

                    decimal impuesto = myConceptoTranslado.Base * item.dcmIVA.Value;

                    myConceptoTranslado.Importe = decimal.Parse(impuesto.ToString("#0.0000")); ;
                    myConceptoTranslado.ImporteSpecified = true;


                    sumaIva += impuesto;
                    sumaSubtotal += myConceptoTranslado.Base;

                    List<dlleFac.ComprobanteConceptoImpuestosTraslado> myListTrans =
                     new List<dlleFac.ComprobanteConceptoImpuestosTraslado>();



                    dlleFac.ComprobanteConceptoImpuestosRetencion
                       myConceptoRetencionIva = new dlleFac.ComprobanteConceptoImpuestosRetencion();
                    dlleFac.ComprobanteConceptoImpuestosRetencion
                      myConceptoRetencionIrs = new dlleFac.ComprobanteConceptoImpuestosRetencion();


                    dlleFac.ComprobanteConceptoImpuestosRetencion
                      myConceptoRetencionIeps = new dlleFac.ComprobanteConceptoImpuestosRetencion();

                    List<dlleFac.ComprobanteConceptoImpuestosRetencion> myListReten =
                     new List<dlleFac.ComprobanteConceptoImpuestosRetencion>();



                    if (item.retIVA > 0)
                    {
                        myConceptoRetencionIva.Impuesto = dlleFac.c_Impuesto.Item002;
                        myConceptoRetencionIva.TasaOCuota = decimal.Parse(item.retIVA.Value.ToString("#0.000000")); //dlleFac.c_TasaOCuota.Item0160000;

                        decimal BasRetIva = item.dcmPrecioUnitario * item.dcmCantidad;
                        myConceptoRetencionIva.Base = decimal.Parse(BasRetIva.ToString("#0.0000"));

                        decimal ImportRet = BasRetIva * item.retIVA.Value;
                        sumaRetIva += ImportRet;
                        myConceptoRetencionIva.Importe = decimal.Parse(ImportRet.ToString("#0.00"));

                        myListReten.Add(myConceptoRetencionIva);
                    }

                    if (item.retISR > 0)
                    {
                        myConceptoRetencionIrs.Impuesto = dlleFac.c_Impuesto.Item001;
                        myConceptoRetencionIrs.TasaOCuota = item.retISR.Value; //dlleFac.c_TasaOCuota.Item0160000;


                        myConceptoRetencionIrs.Base = item.dcmPrecioUnitario * item.dcmCantidad;

                        decimal impuestoRetIsr = myConceptoTranslado.Base * item.retISR.Value;

                        sumaRetIsr += impuestoRetIsr;

                        myConceptoRetencionIrs.Importe = impuestoRetIsr;

                        if (impuestoRetIsr > 0)
                            myListReten.Add(myConceptoRetencionIrs);
                    }


                    if (item.retIEPS > 0)
                    {
                        myConceptoRetencionIeps.Impuesto = dlleFac.c_Impuesto.Item003;
                        myConceptoRetencionIeps.TasaOCuota = item.retIEPS.Value; //dlleFac.c_TasaOCuota.Item0160000;


                        myConceptoRetencionIeps.Base = item.dcmPrecioUnitario * item.dcmCantidad;

                        decimal impuestoRetIeps = myConceptoTranslado.Base * item.retIEPS.Value;
                        
                        sumaRetIeps += impuestoRetIeps;
                        myConceptoRetencionIeps.Importe = impuestoRetIeps;


                        if (impuestoRetIeps > 0)
                            myListReten.Add(myConceptoRetencionIeps);



                        sumaRetIeps += impuestoRetIeps;




                    }





                    myListTrans.Add(myConceptoTranslado);

                    comImpuestos.Traslados = myListTrans.ToArray();

                    if (myListReten.Count > 0)
                    {
                        comImpuestos.Retenciones = myListReten.ToArray();
                    }

                    string[] arrunidad = item.strUnidad.Split('-');
                    string claveunidad = arrunidad[0];
                    string myuni = arrunidad[1];

                    Boolean valueDesc = false;
                    if (item.dcmDescuento > 0)
                    {
                        valueDesc = true;
                    }


                    decimal ImporteTotal = item.dcmImporte + item.dcmDescuento;

                    myConceptos.Add(new dlleFac.ComprobanteConcepto
                    {


                        Cantidad = decimal.Parse(item.dcmCantidad.ToString("#0.00")),
                        ClaveProdServ = item.Productos.strCodigoBarras,
                        Unidad = myuni,
                        ClaveUnidad = claveunidad,
                        NoIdentificacion = item.strPatida,
                        Descripcion = item.strConcepto,
                        ValorUnitario = decimal.Parse("0.00"),
                        Importe = decimal.Parse("0.00"),
                       // Descuento = item.dcmDescuento,
                       // DescuentoSpecified = valueDesc,
                       // Impuestos = comImpuestos


                    });
                }
            }
            MyCFD20.Conceptos = myConceptos.ToArray();



            try
            {
                if (f.strCadenaOriginal.Contains("TranspInternac"))
                {

                    dataCartaPorte.Root dataJson = JsonConvert.DeserializeObject<dataCartaPorte.Root>(f.strCadenaOriginal);



                    dllCartaPorte.CartaPorte CartaPorte = new dllCartaPorte.CartaPorte();


                    //Nodo Ubicaciones
                    List<dllCartaPorte.CartaPorteUbicacion> myUbicacion = new List<dllCartaPorte.CartaPorteUbicacion>();

                    //   dllCartaPorte.CartaPorteFiguraTransporteOperadoresOperadorDomicilio myDomOper = new dllCartaPorte.CartaPorteFiguraTransporteOperadoresOperadorDomicilio();
                    //Nodo Mercancias
                    dllCartaPorte.CartaPorteMercancias myMercancia = new dllCartaPorte.CartaPorteMercancias();
                    List<dllCartaPorte.CartaPorteMercanciasMercancia> myMercancias = new List<dllCartaPorte.CartaPorteMercanciasMercancia>();


                    // dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta myCantTranpo = new dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta();

                    //Nodo Autotransporte
                    dllCartaPorte.CartaPorteMercanciasAutotransporte myAutoTransp = new dllCartaPorte.CartaPorteMercanciasAutotransporte();

                    //Nodo Figura Transpote
                    //dllCartaPorte.CartaPorteFiguraTransporte myFigTrans = new dllCartaPorte.CartaPorteFiguraTransporte();
                    dllCartaPorte.CartaPorteMercanciasAutotransporteIdentificacionVehicular myIndet = new dllCartaPorte.CartaPorteMercanciasAutotransporteIdentificacionVehicular();

                    dllCartaPorte.CartaPorteMercanciasAutotransporteSeguros mySeguro = new dllCartaPorte.CartaPorteMercanciasAutotransporteSeguros();
                    List<dllCartaPorte.CartaPorteMercanciasAutotransporteRemolque> myRemolque = new List<dllCartaPorte.CartaPorteMercanciasAutotransporteRemolque>();

                    //Nodo Operadores

                    //dllCartaPorte.CartaPorteO = new dllCartaPorte.CartaPorteFiguraTransporteOperadoresOperador();
                    List<dllCartaPorte.CartaPorteTiposFigura> myFiguraTransporte = new List<dllCartaPorte.CartaPorteTiposFigura>();



                    decimal dcmTotalDist = 0;

                    string idOrigen = "";
                    string idDestino = "";






                    foreach (var i in dataJson.UbicacionOr)
                    {
                        try
                        {
                            //dllCartaPorte.CartaPorteUbicacionOrigen myOrigen = new dllCartaPorte.CartaPorteUbicacionOrigen();
                            dllCartaPorte.CartaPorteUbicacionDomicilio myDomOr = new dllCartaPorte.CartaPorteUbicacionDomicilio();


                            //  myOrigen.RFCRemitente = i.Origen.RFCRemitente;// "REFA860424NP1";
                            //  myOrigen.NombreRemitente = i.Origen.NombreRemitente;// "ALEJANDRO REYES";

                            //   myOrigen.FechaHoraSalida = DateTime.Parse(i.Origen.FechaHoraSalida.ToString());
                            myDomOr.Pais = dllCartaPorte.c_Pais.MEX;//"MEX";
                            myDomOr.NumeroExterior = i.DomicilioOri.NumeroExterior;// "728";
                            myDomOr.Municipio = i.DomicilioOri.Municipio; //"004";
                            myDomOr.Localidad = i.DomicilioOri.Localidad;
                            myDomOr.Estado = i.DomicilioOri.Estado;
                            myDomOr.Colonia = i.DomicilioOri.Colonia; //"0347";
                            myDomOr.CodigoPostal = i.DomicilioOri.CodigoPostal; //"25350";
                            myDomOr.Calle = i.DomicilioOri.Calle; //"JESUS VALDES SANCHEZ";



                            myUbicacion.Add(new dllCartaPorte.CartaPorteUbicacion
                            {
                                IDUbicacion = i.Origen.IDUbicacion,
                                TipoUbicacion = i.Origen.TipoUbicacion,
                                RFCRemitenteDestinatario = i.Origen.RFCRemitenteDestinatario,
                                FechaHoraSalidaLlegada = i.Origen.FechaHoraSalidaLlegada,


                                //TipoEstacion = i.TipoEstacionOrigen.ToString(), //"01",
                                //TipoEstacionSpecified = false,
                                //DistanciaRecorrida = i.DistanciaRecorridaOrigen,
                                //DistanciaRecorridaSpecified = true,
                                //Origen = myOrigen,
                                Domicilio = myDomOr

                            });

                            //dcmTotalDist += i.DistanciaRecorridaOrigen;


                        }
                        catch (Exception ex) { }
                    }

                    foreach (var d in dataJson.UbicacionDes)
                    {
                        try
                        {

                            // dllCartaPorte.CartaPorteUbicacionDestino myDestino = new dllCartaPorte.CartaPorteUbicacionDestino();

                            dllCartaPorte.CartaPorteUbicacionDomicilio myDomDest = new dllCartaPorte.CartaPorteUbicacionDomicilio();



                            myDomDest.Pais = dllCartaPorte.c_Pais.MEX;// "MEX";
                            myDomDest.NumeroExterior = d.DomicilioDest.NumeroExterior; //"211";
                            myDomDest.Municipio = d.DomicilioDest.Municipio;
                            myDomDest.Localidad = d.DomicilioDest.Localidad;
                            myDomDest.Estado = d.DomicilioDest.Estado;
                            myDomDest.Colonia = d.DomicilioDest.Colonia;
                            myDomDest.CodigoPostal = d.DomicilioDest.CodigoPostal;
                            myDomDest.Calle = d.DomicilioDest.Calle;
                            myDomDest.Referencia = d.DomicilioDest.Referencia;







                            myUbicacion.Add(new dllCartaPorte.CartaPorteUbicacion
                            {

                                IDUbicacion = d.Destino.IDUbicacion,
                                TipoUbicacion = d.Destino.TipoUbicacion,
                                RFCRemitenteDestinatario = d.Destino.RFCRemitenteDestinatario,
                                FechaHoraSalidaLlegada = d.Destino.FechaHoraSalidaLlegada,
                                DistanciaRecorrida = d.Destino.DistanciaRecorrida,
                                DistanciaRecorridaSpecified = true,

                                //TipoEstacion = d.TipoEstacionDestino,
                                //TipoEstacionSpecified = false,
                                //DistanciaRecorrida = d.DistanciaRecorridaDestino,
                                //DistanciaRecorridaSpecified = true,
                                //Destino = myDestino,
                                Domicilio = myDomDest,

                            });

                            dcmTotalDist += d.Destino.DistanciaRecorrida;

                        }
                        catch (Exception ex) { }









                    }


                    CartaPorte.Ubicaciones = myUbicacion.ToArray();





                    CartaPorte.TotalDistRec = dcmTotalDist;
                    CartaPorte.TotalDistRecSpecified = true;
                    CartaPorte.TranspInternac = dllCartaPorte.CartaPorteTranspInternac.No;//"No";

                    foreach (var item in dataJson.Mecancancias.Mercancia)
                    {
                        List<dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta> lstCanTraspo = new List<dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta>();
                        string Claveunidad = item.ClaveUnidad.Split('-')[0];
                        string unidad = item.ClaveUnidad.Split('-')[1];
                        Boolean isValor = true;
                        Boolean isCantidad = true;
                        Boolean isBienesTransp = true;
                        string DescrpMerca = "";
                        Boolean isClaveUni = true;

                        DescrpMerca = item.Descripcion;

                        //if (string.IsNullOrEmpty(item.ValorMercancia.ToString()) || item.ValorMercancia == 0)
                        //{
                        //    isValor = false;
                        //}
                        //if (string.IsNullOrEmpty(item.Cantidad.ToString()) || item.Cantidad == 0)
                        //{
                        //    isCantidad = false;
                        //}



                        lstCanTraspo.Add(new dllCartaPorte.CartaPorteMercanciasMercanciaCantidadTransporta
                        {
                            IDOrigen = item.CantidadTransporta.IDOrigen,
                            IDDestino = item.CantidadTransporta.IDDestino,
                            Cantidad = item.Cantidad //item.CantidadTransporta.Cantidad


                        });

                        decimal dcmPesoKg = 0;
                        if (item.PesoEnKg == 0)
                        { dcmPesoKg = decimal.Parse("0.001"); }
                        else { dcmPesoKg = item.PesoEnKg; }

                        Boolean isMatPel = false;
                        Boolean isMatPelCl = false;
                        Boolean isEmbalaje = false;
                        if (!string.IsNullOrEmpty(item.MaterialPeligroso))
                        {

                            if (item.MaterialPeligroso == "Si")
                            {

                                isMatPel = true;
                                isMatPelCl = true;
                                isEmbalaje = true;

                            }

                            if (item.MaterialPeligroso == "No")
                            {

                                isMatPel = true;


                            }



                        }



                        myMercancias.Add(new dllCartaPorte.CartaPorteMercanciasMercancia

                        {

                            Descripcion = DescrpMerca,
                            ClaveUnidad = Claveunidad,// "XNG",
                            Unidad = unidad,
                            Cantidad = item.Cantidad,
                            PesoEnKg = dcmPesoKg,
                            BienesTransp = item.BienesTransp,
                            CantidadTransporta = lstCanTraspo.ToArray(),
                            MaterialPeligroso = item.MaterialPeligroso,
                            MaterialPeligrosoSpecified = isMatPel,
                            CveMaterialPeligroso = item.ClaveMatPel,
                            CveMaterialPeligrosoSpecified = isMatPelCl,
                            Embalaje = item.Embalaje,
                            EmbalajeSpecified = isEmbalaje
                            //Moneda = dllCartaPorte.c_Moneda.MXN,//"MXN",
                            //MonedaSpecified = true,




                            //  ClaveUnidadSpecified = isClaveUni,

                            //  CantidadSpecified = isCantidad,
                            //  ValorMercancia = item.ValorMercancia,
                            //  ValorMercanciaSpecified = isValor,
                            //item.PesoEnKg,

                            // BienesTranspSpecified = isBienesTransp,





                        });
                    }

                    /*nodo AutoTransporte*/
                    myAutoTransp.PermSCT = dataJson.Autotransporte.PermSCT.Split('-')[0];//dllCartaPorte.c_TipoPermiso.TPAF01;  //"TPAF01";
                    myAutoTransp.NumPermisoSCT = dataJson.Autotransporte.NumPermisoSCT;//"NUMPERM2021";


                    /*nodo identificacion*/

                    myIndet.PlacaVM = dataJson.Autotransporte.IdentificacionVehicular.PlacaVM;
                    myIndet.ConfigVehicular = dataJson.Autotransporte.IdentificacionVehicular.ConfigVehicular; //dllCartaPorte.c_ConfigAutotransporte.C2; //"C2";
                    myIndet.AnioModeloVM = dataJson.Autotransporte.IdentificacionVehicular.AnioModeloVM;// 2020;

                    /*nodo seguros*/


                    mySeguro.AseguraRespCivil = dataJson.Autotransporte.Seguros.AseguraRespCivil; //"POLIZA2021";
                    mySeguro.PolizaRespCivil = dataJson.Autotransporte.Seguros.PolizaRespCivil; //"QUALITAS";
                    mySeguro.AseguraCarga = dataJson.Autotransporte.Seguros.AseguraCarga;


                    foreach (var rem in dataJson.Autotransporte.Remolques)
                    {

                        myRemolque.Add(new dllCartaPorte.CartaPorteMercanciasAutotransporteRemolque
                        {
                            Placa = rem.Placa,
                            SubTipoRem = rem.SubTipoRem
                        });

                    }


                    myAutoTransp.IdentificacionVehicular = myIndet;
                    myAutoTransp.Seguros = mySeguro;
                    myAutoTransp.Remolques = myRemolque.ToArray();



                    myMercancia.NumTotalMercancias = dataJson.Mecancancias.NumTotalMercancias;
                    //  if(dataJson.Mecancancias.PesoBrutoTotal>0)
                    //  {
                    myMercancia.PesoBrutoTotal = dataJson.Mecancancias.PesoBrutoTotal;

                    //      myMercancia.PesoNetoTotalSpecified = true;

                    //  }



                    myMercancia.UnidadPeso = dataJson.Mecancancias.UnidadPeso.Split('-')[0];
                    myMercancia.Mercancia = myMercancias.ToArray();




                    myMercancia.Autotransporte = myAutoTransp;







                    //     myFigTrans.CveTransporte = dataJson.FiguraTransporte.CveTransporte;//dllCartaPorte.c_CveTransporte.Item01;

                    //foreach (var o in dataJson.FiguraTransporte)
                    //{

                    //}

                    foreach (var rem in dataJson.FiguraTransporte.TiposFigura)
                    {
                        myFiguraTransporte.Add(new dllCartaPorte.CartaPorteTiposFigura
                        {
                            NumLicencia = rem.NumLicencia,
                            RFCFigura = rem.RFCFigura,
                            NombreFigura = rem.NombreFigura


                        });


                    }




                    CartaPorte.Mercancias = myMercancia;
                    CartaPorte.FiguraTransporte = myFiguraTransporte.ToArray();


                    MyCFD20.Complemento = new dlleFac.ComprobanteComplemento();

                    XmlDocument docPago = new XmlDocument();
                    XmlSerializerNamespaces xmlNameSpcePago = new XmlSerializerNamespaces();
                    xmlNameSpcePago.Add("cartaporte20", "http://www.sat.gob.mx/CartaPorte20");
                    using (XmlWriter writer = docPago.CreateNavigator().AppendChild())
                    {
                        new XmlSerializer(CartaPorte.GetType()).Serialize(writer, CartaPorte, xmlNameSpcePago);

                    }


                    MyCFD20.Complemento.Any = new XmlElement[1];
                    MyCFD20.Complemento.Any[0] = docPago.DocumentElement;
                }

                if (f.strCadenaOriginal.Contains("NumPermisoSPC"))
                {

                    try
                    {

                        dataServParCons.FillData dataSPC = JsonConvert.DeserializeObject<dataServParCons.FillData>(f.strCadenaOriginal);

                        dllSPC.parcialesconstruccion ServParCons = new dllSPC.parcialesconstruccion();
                        dllSPC.parcialesconstruccionInmueble pci = new dllSPC.parcialesconstruccionInmueble();

                        pci.Calle = dataSPC.Calle;
                        pci.NoInterior = dataSPC.NoInterior;
                        pci.NoExterior = dataSPC.NoExterior;
                        pci.Colonia = dataSPC.Colonia;
                        pci.Localidad = dataSPC.Localidad;
                        pci.Municipio = dataSPC.Municipio;
                        pci.Estado = dataSPC.Estado;
                        pci.CodigoPostal = dataSPC.CodigoPostal;


                        ServParCons.Version = "1.0";
                        ServParCons.NumPerLicoAut = dataSPC.NumPermisoSPC;
                        ServParCons.Inmueble = pci;

                        MyCFD20.Complemento = new dlleFac.ComprobanteComplemento();

                        XmlDocument docSPC = new XmlDocument();
                        XmlSerializerNamespaces xmlNameSpceSPC = new XmlSerializerNamespaces();
                        xmlNameSpceSPC.Add("servicioparcial", "http://www.sat.gob.mx/servicioparcialconstruccion");
                        using (XmlWriter writer = docSPC.CreateNavigator().AppendChild())
                        {
                            new XmlSerializer(ServParCons.GetType()).Serialize(writer, ServParCons, xmlNameSpceSPC);

                        }


                        MyCFD20.Complemento.Any = new XmlElement[1];
                        MyCFD20.Complemento.Any[0] = docSPC.DocumentElement;

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("complemento SPC error " + ex);

                    }




                }



            }
            catch (Exception e)
            {




            }

            List<dlleFac.ComprobanteImpuestosTraslado> Mytraslado = new List<dlleFac.ComprobanteImpuestosTraslado>();

            List<dlleFac.ComprobanteImpuestosRetencion> MyRet = new List<dlleFac.ComprobanteImpuestosRetencion>();

            if (isIva16)
            {
                dlleFac.ComprobanteImpuestosTraslado MyIvaT = new dlleFac.ComprobanteImpuestosTraslado()
                {
                    Base = sumaBaseIva16,
                    Importe = decimal.Parse(sumaIva.ToString("#0.00")),
                    ImporteSpecified = true,
                    Impuesto = dlleFac.c_Impuesto.Item002,
                    TipoFactor = dlleFac.c_TipoFactor.Tasa,
                    TasaOCuota = decimal.Parse("0.160000"),
                    TasaOCuotaSpecified = true




                };


                Mytraslado.Add(MyIvaT);
            }

            if (isIva0)
            {

                dlleFac.ComprobanteImpuestosTraslado MyIvaT0 = new dlleFac.ComprobanteImpuestosTraslado()
                {
                    Base = sumaBaseIva0,
                    Importe = decimal.Parse("0.00"),
                    ImporteSpecified = true,
                    Impuesto = dlleFac.c_Impuesto.Item002,
                    TipoFactor = dlleFac.c_TipoFactor.Tasa,
                    TasaOCuota = decimal.Parse("0.000000"),//dlleFac.c_TasaOCuota.Item0160000    
                    TasaOCuotaSpecified = true

                };
                Mytraslado.Add(MyIvaT0);

            }


            if ((f.intID_Tipo_CFD == 1) || (f.intID_Tipo_CFD == 2) || (f.intID_Tipo_CFD == 5))
            {



                if (f.dcmRetIVA > 0)
                {



                    dlleFac.ComprobanteImpuestosRetencion MyIvaRet = new dlleFac.ComprobanteImpuestosRetencion()
                    {
                        Importe = decimal.Parse(sumaRetIva.ToString("#0.00")),
                        Impuesto = dlleFac.c_Impuesto.Item002


                    };
                    MyRet.Add(MyIvaRet);
                }


                if (f.dcmRetISR > 0)
                {

                    dlleFac.ComprobanteImpuestosRetencion MyIsrRet = new dlleFac.ComprobanteImpuestosRetencion()
                    {
                        Importe = decimal.Parse(sumaRetIsr.ToString("#0.00")),
                        Impuesto = dlleFac.c_Impuesto.Item002


                    };
                    MyRet.Add(MyIsrRet);

                }


                if (f.dcmRetIEPS > 0)
                {

                    dlleFac.ComprobanteImpuestosRetencion MyIsrRet = new dlleFac.ComprobanteImpuestosRetencion()
                    {
                        Importe = decimal.Parse(sumaRetIeps.ToString("#0.00")),
                        Impuesto = dlleFac.c_Impuesto.Item003


                    };
                    MyRet.Add(MyIsrRet);

                }






                Decimal rIsr = f.dcmRetISR.Value;
                totalRet = sumaRetIva + sumaRetIsr + sumaRetIeps;

                if (radOption1.IsChecked == true)
                {

                    totalRet = decimal.Parse(totalRet.ToString("#0.00"));

                }
                else
                {

                    totalRet = Math.Truncate(100 * totalRet) / 100;

                }




                if (totalRet > 0)
                {
                    isRet = true;

                }



            }





            dlleFac.ComprobanteImpuestos Impuestos = new dlleFac.ComprobanteImpuestos()
            {
                TotalImpuestosTrasladados = decimal.Parse(sumaIva.ToString("#0.00")),
                Traslados = Mytraslado.ToArray(),
                TotalImpuestosTrasladadosSpecified = false,



                TotalImpuestosRetenidos = decimal.Parse(totalRet.ToString("#0.00")),
                Retenciones = MyRet.ToArray(),
                TotalImpuestosRetenidosSpecified = isRet,



            };






            //MyCFD20.Impuestos = Impuestos;




        }

        private static void LlenarEmisor(Factura f, Direcciones_Fiscales direccionEmisor, dlleFac.ComprobanteFiscalDigital cfd)
        {

            cfd.Emisor = new dlleFac.Emisor()
            {
                RFCEmisor = f.Empresa.strRFC,
                NombreEmisor = f.Empresa.strRazonSocial
            };

            cfd.DomicilioFiscalEmisor = new dlleFac.DomicilioFiscal()
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

            cfd.ExpedidoEn = new dlleFac.DomicilioFiscal()
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
        }

        private static void LlenarEncabezado(Factura f, dlleFac.ComprobanteFiscalDigital cfd, dlleFac.Comprobante MyCFD20)
        {
            cfd.Serie = f.strSerie;
            cfd.Folio = f.strFolio;
            cfd.Fecha = f.dtmFecha.ToString("yyyy-MM-dd") + "T" + f.dtmFecha.ToString("HH:mm:ss");
            cfd.NoAprobacion = f.Empresa.Folios.First(fol => fol.chrStatus == "A").intNumero_Aprovacion.ToString();
            cfd.AñoAprobacion = f.Certificates.dtmValidoDesde.Year.ToString();
            cfd.TipoComprobante = f.CFD.strDescripcion;
            cfd.FormaPago = f.strForma_Pago;
            cfd.Descuento = f.dcmDescuento.HasValue ? f.dcmDescuento.Value.ToString() : "0.00";
            cfd.SubTotal = f.dcmSubTotal.ToString();
            cfd.Total = f.dcmTotal.ToString();


        }

        private static void GetTraslados(Factura f, List<dlleFac.Traslado> t, decimal tasa, decimal importeTotalTraslado)
        {
            t.Add(new dlleFac.Traslado()
            {
                TipoImpuesto = "IVA",
                Tasa = tasa.ToString("F"),
                //Importe = f.Traslados.First().TotalImpuestoTrasladado.ToString(),
                TotalImpuestosTraslados = f.dcmIVA.ToString()
            });
        }

        private static void GetRetenciones(Factura f, List<dlleFac.Retencion> t, decimal importeTotalRetenido)
        {
            t.Add(new dlleFac.Retencion()
            {

                TipoImpuesto = "IVA",
                //Importe = f.Traslados.First().dcmImporte.ToString()

            });

            t.Add(new dlleFac.Retencion()
            {

                TipoImpuesto = "ISR",
                //Importe = f.Traslados.Last().dcmImporte.ToString()

            });
        }
        private static void GetConceptos(System.Data.Objects.DataClasses.EntityCollection<Detalle_Factura> conceptos, List<dlleFac.Concepto> c, out decimal tasa, out decimal importeTotalTraslado)
        {
            tasa = 0;
            importeTotalTraslado = 0;

            foreach (var item in conceptos)
            {
                c.Add(new dlleFac.Concepto()
                {
                    Cantidad = item.dcmCantidad.ToString(),
                    UnidadMedida = item.Productos.UnidadMedida.strDescripcion,
                    NoIdentificacion = item.Productos.strCodigo,
                    Descripcion = item.Productos.strNombre,
                    ValorUnitario = item.dcmPrecioUnitario.ToString(),
                    Importe = item.dcmImporte.ToString()
                });

                decimal importeTraslado = (item.dcmImporte * (item.dcmIVA.Value / 100));

                tasa = item.dcmIVA.Value;
                importeTotalTraslado += importeTraslado;
            }
        }

        private void dtgFacturasHistorico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckCFDStatus(dtgFacturasHistorico);
        }

        private void mniCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (this.dtgFacturasHistorico.SelectedItem != null)
            {
                Factura f = (Factura)dtgFacturasHistorico.SelectedItem;

                if (f.chrStatus == "A")
                {
                    CancelaCFDi myCancelar = new CancelaCFDi(f);


                    //MessageBoxResult result = MessageBox.Show("Desea cancelar el comprobante", "Cancelar",
                    //   MessageBoxButton.OKCancel, MessageBoxImage.Information);

                    if (myCancelar.ShowDialog().Value == true)
                    {
                        try
                        {
                            //MessageBox.Show("Cancelación Test");

                            string urlSalida = PdfStampInExistingFile(f.strPDFpath);
                            eFacDBEntities db = new eFacDBEntities();

                            Factura factura = db.Factura.SingleOrDefault(fac => fac.intID == f.intID);// busca y edita status de comprobante

                            factura.chrStatus = "C";
                            factura.dtmFechaCancelacion = DateTime.Now;
                            factura.strNumero = "CANCELADA";
                            factura.dtmFechaEnvio = DateTime.Now;
                            factura.strPDFpath = urlSalida;

                            if (f.intID_Tipo_CFD == 6)
                            {

                                List<Detalle_Factura> detfact = db.Detalle_Factura.Where(df => df.intID_Factura == f.intID).ToList();// busca detalle de complemento


                                int idtem = 0;
                                foreach (var item in detfact)
                                {
                                    idtem = int.Parse(item.strConcepto);

                                    List<Detalle_Factura> getPag = db.Detalle_Factura.Where(df => df.strConcepto == item.strConcepto).ToList(); // obtiene pagos de factura para sumar monto

                                    decimal count = 0;

                                    foreach (var i in getPag)
                                    {

                                        count += i.dcmImporte;

                                    }


                                    Factura updateStatus = db.Factura.FirstOrDefault(fd => fd.intID == idtem);  // busca facturas de complemento
                                    updateStatus.strNumero = "";                                                // edita PAGADO O PARCIAL
                                    updateStatus.dtmFechaEnvio = null;                                          // elimina fecha de pago
                                    updateStatus.dcmDescuento = updateStatus.dcmTotal - count + item.dcmImporte;         // actualiza saldo pendiente
                                    item.strConcepto = "0";                                                     // cambia campo de id factura 

                                }

                              
                            }

                             db.SaveChanges();
                           // radMes.IsChecked = true;
                           // cmbMes.SelectedIndex = DateTime.Now.Month - 1;
                            //txtAaaa.Text = DateTime.Now.Year.ToString();
                            buscar();


                        }
                        catch (Exception error)
                        {
                            MessageBox.Show(error.Message);
                        }

                        this.DataContext = null;

                        this.DataContext = new PuntoVentaViewModel();
                    }
                }
            }
        }

        private void mniImprimir_Click(object sender, RoutedEventArgs e)
        {

            if (this.dtgFacturasHistorico.SelectedItem != null)
            {
                Factura f = (Factura)dtgFacturasHistorico.SelectedItem;
                if ((f.chrStatus == "A") || (f.chrStatus == "C"))
                    App.Current.Properties["liga"] = f.strPDFpath;
                else
                {

                    eFacDBEntities db = new eFacDBEntities();

                    db.Connection.Open();
                    if ((f.intID_Tipo_CFD == 1) || (f.intID_Tipo_CFD == 2) || (f.intID_Tipo_CFD == 3) || (f.intID_Tipo_CFD == 4) || (f.intID_Tipo_CFD == 5))
                    {
                       
                        AprobarFactura(f, db, false);
                    }
                    if  (f.intID_Tipo_CFD == 6) {

                        AprobarPago(f, db, false);
                    }

                    if (f.intID_Tipo_CFD == 7)
                    {

                        AprobarCartaPorte(f, db, false);
                    }

                    this.DataContext = null;

                    this.DataContext = new PuntoVentaViewModel();


                    Factura myfactura = db.Factura.Where(fac => fac.intID == f.intID).First();

                    App.Current.Properties["liga"] = myfactura.strPDFdemoPath;
                    //App.Current.Properties["liga"] = Directory.GetCurrentDirectory() + "c:\\adsoftOK.pdf";
                    //App.Current.Properties["liga"] = "c:\\adsoftOK.pdf";
                    //App.Current.Properties["liga"] = Directory.GetCurrentDirectory() + "\\factura.html";
                }

                Views.VinculosInteres.PdfViewer myW = new Views.VinculosInteres.PdfViewer();
                myW.WindowState = WindowState.Maximized;
                myW.ShowDialog();
                myW.Close();
            }

        }




        private List<Detalle_PreFactura> GetConceptosReporte(System.Data.Objects.DataClasses.EntityCollection<Detalle_Factura> entityCollection)
        {
            List<Detalle_PreFactura> result = new List<Detalle_PreFactura>();

            foreach (var item in entityCollection)
            {
                //Thest this should change to the data base

                result.Add(new Detalle_PreFactura()
                {
                    intCantidad = item.dcmCantidad,
                    dcmDescento = item.dcmDescuento,
                    dcmImporte = item.dcmImporte.ToString("N"),
                    strUnidad = item.Productos.UnidadMedida.strDescripcion,
                    strConcepto = item.strConcepto,
                    dcmPrecioUnitario = item.dcmPrecioUnitario.ToString("N"),
                    strPartida = item.strPatida, //Thest
                    dcmIIVA = item.dcmIVA.Value.ToString("F")
                });
            }//0106438820

            return result;
        }

        private void ShowReport(wpfEFac.Helpers.CFDReporte reporte)
        {
           // wpfEFac.Views.Reports.CFD ventanaReporte = new Reports.CFD(reporte);
           // ventanaReporte.Show();
        }

        private void mniXML_Click(object sender, RoutedEventArgs e)
        {
            if (this.dtgFacturasHistorico.SelectedItem != null)
            {
                Factura f = (Factura)dtgFacturasHistorico.SelectedItem;

                if (f.chrStatus == "A")
                {
                    try
                    {

                        App.Current.Properties["liga"] = f.strXMLpath;
                        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                        dlg.FileName = "XML_" + f.strFolio;
                        dlg.DefaultExt = ".xml"; // Default file extension
                        dlg.Filter = "Documentos XML (.xml)|*.xml"; // Filter files by extension
                        Nullable<bool> result = dlg.ShowDialog();
                        // Process save file dialog box results
                        if (result == true)
                        {




                            byte[] rawData = File.ReadAllBytes(f.strXMLpath);


                            System.IO.File.WriteAllBytes(dlg.FileName, rawData);
                        }



                        else
                        {


                        }
                       
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message);
                    }

                }
            }


        }

        private void mniPDF_Click(object sender, RoutedEventArgs e)
        {

            if (this.dtgFacturasHistorico.SelectedItem != null)
            {
                try {
                    Factura f = (Factura)dtgFacturasHistorico.SelectedItem;
                    if (f.chrStatus == "A")
                    {
                        App.Current.Properties["liga"] = f.strPDFpath;
                        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                        dlg.FileName = "Factura_" + f.strFolio;
                        dlg.DefaultExt = ".pdf"; // Default file extension
                        dlg.Filter = "Documentos PDF (.pdf)|*.pdf"; // Filter files by extension
                        Nullable<bool> result = dlg.ShowDialog();
                        // Process save file dialog box results
                        if (result == true)
                        {




                            byte[] rawData = File.ReadAllBytes(f.strPDFpath);


                            System.IO.File.WriteAllBytes(dlg.FileName, rawData);
                        }



                        else
                        {

                        }
                    }
                
                
                }
                catch(Exception ex) { }
                
            }
        }

        private void DoPDFCFD(Factura f, Direcciones_Fiscales direccionEmisor, Direcciones_Fiscales direccionReceptor)
        {
            Helpers.CFDReporte infoReporte = new wpfEFac.Helpers.CFDReporte()
            {
                Folio = "Folio: " + f.strSerie + " " + f.strFolio,
                Fecha = "Fecha: " + f.dtmFecha.ToString("yyyy-MM-dd") + "T" + f.dtmFecha.ToString("HH:mm:ss"),
                NumeroCertificado = "Certificado: " + f.Certificates.strNumeroCertificadoSelloDigital,
                NumeroAprobacion = "No. Aprob. " + f.Empresa.Folios.First(fol => fol.chrStatus == "A").intNumero_Aprovacion,
                AnoArobacion = "Año aprobacion" + f.Certificates.dtmValidoDesde.Year,

                Logo = "file:///" + f.Empresa.strLogo,
                NombreEmisor = f.Empresa.strRazonSocial,
                DomicilioEmisor = direccionEmisor.strCalle + " " +
                direccionEmisor.strNoExterior + " " + direccionEmisor.strColonia + " " +
                direccionEmisor.strCodigoPostal + " " + direccionEmisor.strMunicipio + " " +
                direccionEmisor.Estado.strNombreEstado + " " + direccionEmisor.Paises.strNombrePais,
                RFCEmisor = f.Empresa.strRFC,
                Telefono = f.Empresa.strTelefono,

                NombreCliente = f.Clientes.strRazonSocial,
                DomicilioCliente = direccionReceptor.strCalle + " " +
                direccionReceptor.strNoExterior + " " + direccionReceptor.strColonia + " " +
                direccionReceptor.strCodigoPostal + " " + direccionReceptor.strMunicipio + " " +
                direccionReceptor.Estado.strNombreEstado + " " + direccionReceptor.Paises.strNombrePais,
                RFCCliente = f.Clientes.strRFC,

                Proveedor = "Proveedor " + f.strProveedor, // prueva
                NoPedido = "No. Pedido " + f.strNumero,
                NoContrato = "No. Contrato " + f.strNumeroContrato,
                Usuario = f.Usuarios.strNombre,
                Estimacion = "Estimación: " + f.intEstimacion,

                ImporteLetra = ConvertidorNumeroLetra.NumeroALetras(f.dcmTotal.ToString("F"), "PESOS"),
                SubTotal = f.dcmSubTotal.ToString("F"),

                IVA = f.dcmIVA.ToString("F"),
                Total = f.dcmTotal.ToString("F"),

                Observaciones = f.strObervaciones,

                Conceptos = GetConceptosReporte(f.Detalle_Factura),

                SelloDigital = f.strSelloDigital,

                CadenaOriginal = f.strCadenaOriginal,
                Cedula = "file:///" + f.Empresa.strCedula,
            };

           // Reports.CFD pdfExporter = new Reports.CFD(infoReporte);

           // wpfEFac.Views.Reports.CFD ventanaReporte = new Reports.CFD(infoReporte);

            //ventanaReporte.DoPDF(infoReporte);

        }

        private void mniEmail_Click(object sender, RoutedEventArgs e)
        {
            if (this.dtgFacturasHistorico.SelectedItem != null)
            {
                Factura f = (Factura)dtgFacturasHistorico.SelectedItem;

                if (f.chrStatus == "A")
                {
                    EMail.Email emailwindow = new EMail.Email();

                    emailwindow.Show();

                    Messenger.Default.Send(f);

                    //if (MailSender.SendMail(f.Empresa.strEmail,
                    //    f.Empresa.ConfiguracionEmail.First().strPasswordEmailEmpresa,
                    //    //GetRecivers(f.Empresa.ConfiguracionEmail.First(), f.Clientes.strEmail),
                    //    new List<string>() { "adolfo.centeno@itesm.mx" },
                    //    "||CFD|" + f.Empresa.strRFC + "|" + f.dtmFecha + "|" + f.strFolio + "||",
                    //    "<html><body><b><font size='+1'>Este mensaje es un envio automatico de la " +
                    //    "aplicacion MyFacturaE</font></b><br /><br />" +
                    //    "Contribuyente: " + f.Empresa.strRazonSocial + "<br /><br />" +
                    //    "Fecha: " + f.dtmFecha.ToString() + "<br /><br />" +
                    //    "Receptor: " + f.Clientes.strNombreComercial + "<br /><br />" +
                    //    "Powered by Adesoft <A HREF =\"http://www.adesoft.com.mx/" +"\"" + ">http://www.adesoft.com.mx/</A>" + "<br /><br />" +
                    //    "</body></html>", 
                    //    GetAttach()))
                    //{
                    //    MessageBox.Show("Comprobante fiscal enviado a: " + f.Clientes.strEmail, "Exito", MessageBoxButton.OK, MessageBoxImage.Information);


                    //}
                }
            }

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

        private List<string> GetAttach()
        {
            return new List<string>()
            {
                "Muestra.pdf",//This has to change to the files generated by the app. 
                "Muestra.xml"
            };
        }

        private void bttEditarPreFactura_Click(object sender, RoutedEventArgs e)
        {
            if (this.dtgFacturasHistorico.SelectedItem != null)
            {
                Factura f = (Factura)dtgFacturasHistorico.SelectedItem;

                if (f.chrStatus == "P")
                {
                    if (f.intID_Tipo_CFD == 6)
                    {

                        MessageBox.Show("Lo sentimos complementos de pago no se pueden editar, elimine el registro y vuelva a capturar.");


                    }
                    else
                    {

                        PreFactura pf = new PreFactura(f.intID, f.strSerie,
                            f.strFolio, f.Empresa, f.Clientes, f.strProveedor,
                            f.strNumero, f.strNumeroContrato, f.intEstimacion,
                            f.strObervaciones, f.Detalle_Factura, f);

                        this.NavigationService.Navigate(pf);
                    }
                }
                else
                {
                    MessageBox.Show("El comprobante no puede ser editado.");
                }


               
               
            }
        }



        private void cmdReciboArr_Click(object sender, RoutedEventArgs e)
        {
            PreFactura newPreFactura = new PreFactura(3,null);

            this.NavigationService.Navigate(newPreFactura);
        }

        private void hplNuevaNotaCredito_Clik(object sender, RoutedEventArgs e)
        {
            if (this.dtgFacturasHistorico.SelectedItem != null)
            {

                IList items = dtgFacturasHistorico.SelectedItems;

                MessageBoxResult result = MessageBox.Show("El comprobante lleva " + items.Count + " documentos Relacionados", "CFDi Relacionado",
                            MessageBoxButton.OKCancel, MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    List<Relacionados> itemsId = new List<Relacionados>();



                    foreach (Factura item in items)
                    {
                        itemsId.Add(new Relacionados
                        {

                            idFact = item.intID
                        });
                    }


                    PreFactura newPreFactura = new PreFactura(2, itemsId);

                    this.NavigationService.Navigate(newPreFactura);

                }
                else
                {

                    PreFactura newPreFactura = new PreFactura(2, null);

                    this.NavigationService.Navigate(newPreFactura);

                }

            }
            else
            {

                PreFactura newPreFactura = new PreFactura(2, null);

                this.NavigationService.Navigate(newPreFactura);

            }
        }

        private void hplNuevoReciboHonorarios_Click(object sender, RoutedEventArgs e)
        {
            PreFactura newPreFactura = new PreFactura(4, null);
            this.NavigationService.Navigate(newPreFactura);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            buscar();
        }

        private void hplCotizacion_Click(object sender, RoutedEventArgs e)
        {
            PreFactura newPreFactura = new PreFactura(6,null);
            this.NavigationService.Navigate(newPreFactura);
        }

        private void hplRemision_Click(object sender, RoutedEventArgs e)
        {
            buscar();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            buscar();
        }

        private void hplNuevaCartaPorte_Click(object sender, RoutedEventArgs e)
        {
            if (this.dtgFacturasHistorico.SelectedItem != null)
            {

                IList items = dtgFacturasHistorico.SelectedItems;

                MessageBoxResult result = MessageBox.Show("El comprobante lleva " + items.Count + " documentos Relacionados", "CFDi Relacionado",
                            MessageBoxButton.OKCancel, MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    List<Relacionados> itemsId = new List<Relacionados>();



                    foreach (Factura item in items)
                    {
                        itemsId.Add(new Relacionados
                        {

                            idFact = item.intID
                        });
                    }


                    PreFactura newPreFactura = new PreFactura(5, itemsId);

                    this.NavigationService.Navigate(newPreFactura);

                }
                else
                {

                    PreFactura newPreFactura = new PreFactura(5, null);

                    this.NavigationService.Navigate(newPreFactura);

                }

            }
            else
            {

                PreFactura newPreFactura = new PreFactura(5, null);

                this.NavigationService.Navigate(newPreFactura);

            }
        }

        private void cmbMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buscar();
        }

        private void cmbCfd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buscar();
        }

        private void radMes_Click(object sender, RoutedEventArgs e)
        {
            //cmbMes.SelectedIndex = DateTime.Now.Month - 1;
            //txtAaaa.Text = DateTime.Now.Year.ToString();
            //buscar();

        }


        private void buscar()
        {

            eFacDBEntities db = new eFacDBEntities();
            List<Factura> fact = null;

            DateTime fechaInicio = DateTime.Parse(dtpFechaFGInicio.SelectedDate.Value.ToString("yyyy-MM-dd") + "T" + dtpFechaFGInicio.SelectedDate.Value.ToString("00:00:00"));
            DateTime fechaFin = DateTime.Parse(dtpFechaFGFin.SelectedDate.Value.ToString("yyyy-MM-dd") + "T" + dtpFechaFGFin.SelectedDate.Value.ToString("23:59:59"));






            /**Verificar status de cancelacion**/

            //foreach (var i in fact) {
            //    try
            //    {
            //        if (i.chrStatus == "C")
            //        {




            //            RespuestaEstatusSAT response = myService.consultarEstadoSAT(UserPac.idEquipo, i.strSelloDigital, UserPac.rfc, i.Clientes.strRFC, i.dcmTotal.ToString());

            //            string estatus = response.Estado+"/" +response.EstatusCancelacion;

            //            if (!string.IsNullOrEmpty(estatus))
            //            {
            //                i.strNumero = estatus;
            //                db.SaveChanges();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //  }




            if (radRango.IsChecked == true)
            {

                int intCFD = int.Parse(cmbTipoComprobante.SelectedValue.ToString());
                if (string.IsNullOrEmpty(txtFolio.Text) && string.IsNullOrEmpty(txtCliente.Text))
                {


                    fact = db.Factura.Where(x => x.dtmFecha >= fechaInicio && x.dtmFecha <= fechaFin && x.chrStatus != "E" && x.intID_Tipo_CFD == intCFD).OrderByDescending(p => p.dtmFecha).ToList();
                }

                if (!string.IsNullOrEmpty(txtFolio.Text))
                {
                    //fact = db.Factura.Where(x => x.chrStatus != "E" && x.strFolio == strFolio.Trim()).OrderByDescending(p => p.dtmFecha).ToList();
                    fact = db.Factura.Where(x => x.dtmFecha >= fechaInicio && x.dtmFecha <= fechaFin && x.chrStatus != "E" && x.intID_Tipo_CFD == intCFD && x.strFolio == txtFolio.Text.Trim()).OrderByDescending(p => p.dtmFecha).ToList();
                }


                if (!string.IsNullOrEmpty(txtCliente.Text))
                {
                    //fact = db.Factura.Where(x => x.chrStatus != "E" && x.Clientes.strNombreComercial.Contains(strNombre)).OrderByDescending(p => p.dtmFecha).ToList();
                    fact = db.Factura.Where(x => x.dtmFecha >= fechaInicio && x.dtmFecha <= fechaFin && x.chrStatus != "E" && x.intID_Tipo_CFD == intCFD && x.Clientes.strNombreComercial.Contains(txtCliente.Text.Trim())).OrderByDescending(p => p.dtmFecha).ToList();
                }
            }

            if (radTodos.IsChecked == true)
            {

                fact = db.Factura.Where(x => x.dtmFecha >= fechaInicio && x.dtmFecha <= fechaFin && x.chrStatus != "E").OrderByDescending(p => p.dtmFecha).ToList();


            }



            ICollectionView facturas = CollectionViewSource.GetDefaultView(fact);
            dtgFacturasHistorico.ItemsSource = facturas;
            //facturas.Filter = TextFilter;

        }
       

        /*
        public bool TextFilter(object o)
        {
            bool retValue = false;

            Factura p = (o as Factura);            

            if (p == null)
                retValue = false;

            if (opCfd == 0)
            {
                if (radFecha.IsChecked == true)
                {
                    if ((opCfd == 0) && p.strFolio.Contains(txtFolio.Text) && p.Clientes.strNombreComercial.ToUpper().Contains(txtCliente.Text.ToUpper()))
                        retValue = true;
                }

                if (radMes.IsChecked == true)
                {
                    if ((opCfd == 0) && (p.dtmFecha.Month == cmbMes.SelectedIndex + 1 && p.dtmFecha.Year.ToString() == txtAaaa.Text && p.strFolio.Contains(txtFolio.Text) && p.Clientes.strNombreComercial.ToUpper().Contains(txtCliente.Text.ToUpper())))
                        retValue = true;
                }


            }
            else
            {

                if (radFecha.IsChecked == true)
                {

                    // sin filtro de fecha

                    if ((p.intID_Tipo_CFD == opCfd) && (p.strFolio.Contains(txtFolio.Text) && p.Clientes.strNombreComercial.ToUpper().Contains(txtCliente.Text.ToUpper())))
                        retValue = true;
                    else
                        retValue = false;

                }
                else
                {
                    // si es por mes y año
                    if ((p.intID_Tipo_CFD == opCfd) && (p.dtmFecha.Month == cmbMes.SelectedIndex + 1 && p.dtmFecha.Year.ToString() == txtAaaa.Text && p.strFolio.Contains(txtFolio.Text) && p.Clientes.strNombreComercial.ToUpper().Contains(txtCliente.Text.ToUpper())))
                        retValue = true;
                    else
                        retValue = false;

                }
            }

            return retValue;
        }*/

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            buscar();
        }

        private void txtCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            buscar();
        }

        private void radFecha_Click(object sender, RoutedEventArgs e)
        {
            buscar();
        }

        private void bttBuscar_Click(object sender, RoutedEventArgs e)
        {
            buscar();
        }

        private void radFacturas_Click(object sender, RoutedEventArgs e)
        {
            buscar();    
        }

        private void radTodos_Click(object sender, RoutedEventArgs e)
        {
            buscar();
        }

        private void dtgFacturasHistorico_Loaded(object sender, RoutedEventArgs e)
        {
          //  buscar();
        }

        private void bttEliminarPreFactura_Click(object sender, RoutedEventArgs e)
        {

            //MessageBox.Show("eliminando...");

            if (this.dtgFacturasHistorico.SelectedItem != null)
            {
                Factura f = (Factura)dtgFacturasHistorico.SelectedItem;

                if (f.chrStatus == "P")
                {

                    eFacDBEntities db = new eFacDBEntities();

                    Factura MyFacturaDeleted = db.Factura.FirstOrDefault(fd => fd.intID == f.intID);// busca y edita status complemento
                    MyFacturaDeleted.chrStatus = "E";
                    if (f.intID_Tipo_CFD == 6)
                    {
                        List<Detalle_Factura> detfact = db.Detalle_Factura.Where(df => df.intID_Factura == f.intID).ToList();// busca detalle de complemento 
                      




                        int idtem = 0;
                        foreach (var item in detfact)
                        {
                             idtem = int.Parse(item.strConcepto);

                             List<Detalle_Factura> getPag = db.Detalle_Factura.Where(df => df.strConcepto == item.strConcepto).ToList();// obtiene pagos de factura para sumar monto

                             decimal count = 0;

                            foreach(var i in getPag){

                                count += i.dcmImporte;
                            
                            }


                            Factura updateStatus = db.Factura.FirstOrDefault(fd => fd.intID == idtem);  // busca facturas de complemento
                            
                            updateStatus.strNumero = "";                                                // edita PAGADO O PARCIAL
                            updateStatus.dtmFechaEnvio = null;                                          // elimina fecha de pago
                            updateStatus.dcmDescuento = updateStatus.dcmTotal - count + item.dcmImporte;         // actualiza saldo pendiente

                             


                            item.strConcepto = "0";                                                     // cambia campo de id factura 

                        }
                    }


               

                    db.SaveChanges();


                    // dtgFacturasHistorico.ItemsSource = db.Factura.Where(mf => mf.chrStatus != "E").OrderByDescending(d => d.dtmFecha);
                    //cmbMes.SelectedIndex = DateTime.Now.Month - 1;
                    //txtAaaa.Text = DateTime.Now.Year.ToString();
                    buscar();


                }

                else
                {
                    MessageBox.Show("No se pueden eliminar Facturas Aprobadas");

                }

            }

        }

        private void mniPayment_Click(object sender, RoutedEventArgs e)
        {
            PaymentFactura myPago = new PaymentFactura(null);


            if (myPago.ShowDialog().Value == true)
            {
                try
                {

                    cmbTipoComprobante.Text = "PAGO";
                    buscar();
                    

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }

                this.DataContext = null;

                this.DataContext = new PuntoVentaViewModel();
            }
            //myPago.ShowDialog();



        }

      

        private void hplComplementoPago_Click(object sender, RoutedEventArgs e)
        {
            if (this.dtgFacturasHistorico.SelectedItem != null)
            {

                IList items = dtgFacturasHistorico.SelectedItems;

                MessageBoxResult result = MessageBox.Show("El comprobante lleva " + items.Count + " documentos Relacionados", "CFDi Relacionado",
                            MessageBoxButton.OKCancel, MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    List<Relacionados> itemsId = new List<Relacionados>();



                    foreach (Factura item in items)
                    {
                        itemsId.Add(new Relacionados
                        {

                            idFact = item.intID
                        });
                    }


                    PreFactura newPreFactura = new PreFactura(7, itemsId);

                    this.NavigationService.Navigate(newPreFactura);

                }
                else
                {

                    PreFactura newPreFactura = new PreFactura(7, null);

                    this.NavigationService.Navigate(newPreFactura);

                }

            }
            else
            {

                PreFactura newPreFactura = new PreFactura(7, null);

                this.NavigationService.Navigate(newPreFactura);

            }


            //PreFactura newPreFactura = new PreFactura(7, null);

            //this.NavigationService.Navigate(newPreFactura);
        }

        private void radOption1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void radOption2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mniComntario_Click(object sender, RoutedEventArgs e)
        {
            if (this.dtgFacturasHistorico.SelectedItem != null)
            {
                Factura f = (Factura)dtgFacturasHistorico.SelectedItem;
                AddComentario myAddComentario = new AddComentario(f);
                if (myAddComentario.ShowDialog().Value == true)
                {

                    //radTodos.IsChecked = true;
                   
                    buscar();
                
                }
            }
        }

        public string PdfStampInExistingFile(string dirPdfEntrada)
        {

            string urlSalida = dirPdfEntrada.Replace(".pdf", "");

            urlSalida = urlSalida + "_cancelado.pdf";

            using (Stream inputPdfStream = new FileStream(dirPdfEntrada.Replace("_cancelado.pdf", ""), FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new FileStream(Directory.GetCurrentDirectory()+"\\cancelado.png", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream(urlSalida, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                //image.SetAbsolutePosition(70f, 70f);
                image.SetAbsolutePosition((PageSize.A4.Width - image.ScaledWidth) / 2, (PageSize.A4.Height - image.ScaledHeight) / 2);

                pdfContentByte.AddImage(image);
                stamper.Close();
            }




            return urlSalida;


        }

        private void radRango_Click(object sender, RoutedEventArgs e)
        {
            buscar();
        }

        private void mniSolicitudesCancelacion_Click(object sender, RoutedEventArgs e)
        {
            SolicitudesCancelacion mySolicitudes = new SolicitudesCancelacion();
            mySolicitudes.Show();
        }
    }
}
