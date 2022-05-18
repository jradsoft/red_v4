using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using wpfEFac.Helpers;

namespace wpfEFac.Models
{
    public class CFDModels
    {
        private eFacDBEntities db;
        
        public CFDModels()
        {
            db = new eFacDBEntities();
        }

        public dlleFac.ComprobanteFiscalDigital FillComprobanteFiscal(dlleFac.ComprobanteFiscalDigital cf,
            string version, string serie, string folio, string fecha, string noAprobacion, string anoAprovacion,
                string tipoComprobante, string formaPago, string subTotal, string descuento, string total, dlleFac.Emisor emisor,
            dlleFac.DomicilioFiscal domicilioFiscalEmisor, dlleFac.DomicilioFiscal expedidoEn, dlleFac.Receptor receptor,
            dlleFac.DomicilioFiscal domicilioFiscalReceptor, List<dlleFac.Concepto> conceptos) 
        {
            //Inicio de datos del comprovante
            cf.Version = version; // Version por default debe ser "2.0"
            cf.Serie = serie;
            cf.Folio = folio;
            cf.Fecha = fecha;
            cf.NoAprobacion = noAprobacion;
            cf.AñoAprobacion = anoAprovacion; // No HardCode
            cf.TipoComprobante = tipoComprobante;
            cf.FormaPago = formaPago;
            cf.SubTotal = subTotal;
            cf.Descuento = descuento;
            cf.Total = total;
            // Fin de datos del comprovante

            // Datos de emisor
            cf.Emisor = emisor;
            // Fin de Datos del emisor

            // Domicilio de Emisor
            cf.DomicilioFiscalEmisor = domicilioFiscalEmisor;
            // Fin de domicilio fiscal de emisor

            // Datos de Expedido En -> (Es un domicilio fical donde se emite el CFD)

            cf.ExpedidoEn = expedidoEn;

            // Fin de domicilio de Expedido En

            //Datos de receptor
            cf.Receptor = receptor;

            //Datos de receptor

            cf.DomicilioFiscalReceptor = domicilioFiscalReceptor;

            cf.Conceptos = conceptos;

            cf.Descuento = GetDescuento(conceptos);

            //cf.Impuestos = impuestos;

            return cf;

        }

        private string GetDescuento(List<dlleFac.Concepto> conceptos)
        {
            decimal descuento = 0;

            foreach (var item in conceptos)
            {
                decimal porcentaje = decimal.Parse(item.Descuento) / 100;

                decimal importe = decimal.Parse(item.Importe);

                descuento += importe * porcentaje;
            }

            return descuento.ToString("F");
        }


        public bool UpdateFactura(int idFactura,int idTipoCFD,
            int idEmpresa, string serie, string folio, DateTime fecha,
            int idUsuario, int idCliente, string formaPago,
            string observaciones, decimal subTotal, decimal descuento, decimal IVA,
            decimal ToTal, string proveedor,
            string numero, string numeroContrato, string estimacion,
            int idCertificado, string cadenaOriginal,
            List<Helpers.ConceptoPreFactura> conceptos, decimal retIVA, decimal retISR, decimal retIEPS,
            string strCondicionesPago,
                   string         strMetodoPago,
                   string         strMotivoDescuento,
                   string         strDivisa,
                   decimal         dcmTipoCambio,
            string origen,
         string recogerEn,

         string destino,
         string destinatario,
         string rfcDestinatario,
         string domicilioDestinatario,
         string entregarEn,
            List<AddendaEntry> Addenda
            
            )
        {
            if (!(db.Connection.State == System.Data.ConnectionState.Open))
            {
                db.Connection.Open();
            }

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    Factura cfd = db.Factura.First(f => f.intID == idFactura);

                    FillFactura(idTipoCFD, idEmpresa, serie, folio, fecha, idUsuario, idCliente, 
                        formaPago, observaciones, subTotal, descuento, IVA, ToTal, proveedor, numero, 
                        numeroContrato, estimacion, idCertificado, cadenaOriginal, cfd, retIVA, retISR, retIEPS,
                          strCondicionesPago,  
                        strMetodoPago,
                            strMotivoDescuento,
                            strDivisa,
                            dcmTipoCambio,
                             origen,
          recogerEn,

          destino,
          destinatario,
          rfcDestinatario,
          domicilioDestinatario,
          entregarEn
                        );

                    db.SaveChanges();

                    RemoveDetalleAndTrasladado(cfd);

                    db.SaveChanges();

                    foreach (var item in conceptos)
                    {

                        Detalle_Factura dt = new Detalle_Factura();

                        CreateDetalleFactura(idEmpresa, cfd, item, dt);

                        db.Detalle_Factura.AddObject(dt);
                    }

                    /*foreach (AddendaEntry item in Addenda)
                    {
                        Addendas myItemUpdateAddenda = db.Addendas.FirstOrDefault(a => a.idFactura == cfd.intID && a.idPos == item.idPos);

                        myItemUpdateAddenda.Default = item.Default;
                        db.SaveChanges();


                    }*/

                    /*
                    foreach (AddendaEntry item in Addenda)
                    {

                        Addendas myItemAddenda = new Addendas()
                        {
                            idFactura = cfd.intID,
                            idAddenda = item.idAddenda,
                            idPos = item.idPos,
                            Descripcion = item.Descripcion,
                            Default = item.Default
                        };

                    

                        db.Addendas.AddObject(myItemAddenda);
                    }*/
                    
                    db.SaveChanges();

                    transactionScope.Complete();

                    db.AcceptAllChanges();

                    db.Connection.Close();

                    Identifier.IsUsing = false;

                    return true;
                }
                catch(Exception)
                {
                    return false;
                }   
            }
            
            

        }

        private void RemoveDetalleAndTrasladado(Factura cfd)
        {
            try
            {
                foreach (var item in cfd.Detalle_Factura.ToList())
                {
                    db.Detalle_Factura.DeleteObject(item);
                }
            }
            catch (Exception)
            {

            }

            try
            {
               /*
                foreach (var item in cfd.Traslados.ToList())
                {
                    db.Traslados.DeleteObject(item);
                }
                */
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void FillFactura(int idTipoCFD, int idEmpresa, string serie, string folio, DateTime fecha, 
            int idUsuario, int idCliente, string formaPago, string observaciones, decimal subTotal, decimal descuento, 
            decimal IVA, decimal ToTal, string proveedor, string numero, string numeroContrato, string estimacion, 
            int idCertificado, string cadenaOriginal, Factura cfd, decimal retIVA, decimal retISR, decimal retIEPS,
           string strCondicionesPago,
            string strMetodoPago,
                   string strMotivoDescuento,
                   string strDivisa,
                   decimal dcmTipoCambio,
            string origen,
         string recogerEn,

         string destino,
         string destinatario,
         string rfcDestinatario,
         string domicilioDestinatario,
         string entregarEn
            )
        {
            cfd.intID_Tipo_CFD = idTipoCFD;
            cfd.intID_Empresa = idEmpresa;
            cfd.strSerie = serie;
            cfd.strFolio = folio;
            cfd.dtmFecha = fecha;
            cfd.intID_Usuario = idUsuario;
            cfd.intID_Cliente = idCliente;
            cfd.strForma_Pago = formaPago;
            cfd.strObervaciones = observaciones;
            cfd.dcmSubTotal = subTotal;
            cfd.dcmDescuento = descuento;
            cfd.dcmIVA = IVA;
            cfd.dcmTotal = ToTal;
            cfd.chrStatus = "P";
            cfd.strProveedor = proveedor;
            cfd.strNumero = numero;
            cfd.strNumeroContrato = numeroContrato;
            cfd.intEstimacion = estimacion;
            cfd.intID_Certificate = idCertificado;
            cfd.strCadenaOriginal = cadenaOriginal;
            cfd.dcmRetIVA = retIVA;
            cfd.dcmRetISR = retISR;
            cfd.dcmRetIEPS = retIEPS;
            cfd.CondPago = strCondicionesPago;
            cfd.MetodoPago = strMetodoPago;
            cfd.MotivoDesc = strMotivoDescuento;
            cfd.Divisa = strDivisa;
            cfd.TipoCambio = dcmTipoCambio;


            cfd.Origen = origen;
            cfd.RecogerEn = recogerEn;
            cfd.Destino = destino;
            cfd.Destinatario = destinatario;
            cfd.rfcDestinatario = rfcDestinatario;
            cfd.domicilioDestinatario = domicilioDestinatario;
            cfd.EntregarEn = entregarEn;
        }

        public bool SaveFactura(int idTipoCFD,
            int idEmpresa, string serie, string folio, DateTime fecha,
            int idUsuario, int idCliente, string formaPago,
            string observaciones, decimal subTotal, decimal descuento, decimal IVA,
            decimal ToTal, string proveedor,
            string numero, string numeroContrato, string estimacion,
            int idCertificado, 
            string cadenaOriginal,
            List<Helpers.ConceptoPreFactura> conceptos,
            decimal retIVA, decimal retISR, decimal retIEPS,
            
                   string strCondicionesPago,
                   string         strMetodoPago,
                   string         strMotivoDescuento,
                   string         strDivisa,
                   decimal         dcmTipoCambio,
             string origen,
             string recogerEn,
        
            string destino,
            string destinatario,
            string rfcDestinatario,
            string domicilioDestinatario,
            string entregarEn,
            List<AddendaEntry> Addenda,
            string urlXML

            
                          
            )
        {

            db.Connection.Open();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    
                    Factura cfd = new Factura();

                    CreateNewFactura(idTipoCFD, idEmpresa, serie, folio, fecha, idUsuario, idCliente, formaPago, observaciones, 
                        subTotal, descuento, IVA, ToTal, proveedor, numero, 
                        numeroContrato, estimacion, idCertificado, cadenaOriginal, cfd, retIVA, retISR, retIEPS,
                         strCondicionesPago,
                            strMetodoPago,
                           strMotivoDescuento,
                            strDivisa,
                            dcmTipoCambio, 

                        origen,
         recogerEn,
        
         destino,
         destinatario,
         rfcDestinatario,
         domicilioDestinatario,
         entregarEn);

                    db.Factura.AddObject(cfd);

                    foreach (var item in conceptos)
                    {
                        
                        Detalle_Factura dt = new Detalle_Factura() ;

                        CreateDetalleFactura(idEmpresa, cfd, item, dt);

                        db.Detalle_Factura.AddObject(dt);
                    }




                    /*
                    foreach (AddendaEntry item in Addenda)
                    {

                        Addendas myItemAddenda = new Addendas()
                        {
                            idFactura = cfd.intID,
                            idAddenda = item.idAddenda,
                            idPos = item.idPos,
                            Descripcion = item.Descripcion,
                            Default = item.Default
                        };



                        db.Addendas.AddObject(myItemAddenda);
                    }*/
                    
                    db.SaveChanges();

                    transactionScope.Complete();

                    db.AcceptAllChanges();

                    db.Connection.Close();

                    Identifier.IsUsing = false;

                    return true;
                }
                catch (Exception e)
                {
                    db.Connection.Close();
                    return false;
                }
            }
        }

        /*
        private void CreateRetencion(int idEmpresa, Factura cfd, dlleFac.Retencion item, Retenciones tr)
        {
            tr.intId = Identifier.GetID(typeof(Retenciones).Name, idEmpresa, cfd, EntityEnum.Retenciones, db);
            tr.IdFactura = cfd.intID;
            tr.strTipoIva = item.TipoImpuesto;
            
            tr.decImporte = decimal.Parse(item.Importe);
            
        }
        */
        /*
        private void CreateTraslado(int idEmpresa, Factura cfd, dlleFac.Traslado item, Traslados tr)
        {
            tr.intID = Identifier.GetID(typeof(Traslados).Name, idEmpresa, cfd, EntityEnum.Traslados, db);
            tr.intID_Factura = cfd.intID;
            tr.strTipoImpuesto = item.TipoImpuesto;
            tr.dcmTasa = decimal.Parse(item.Tasa);
            tr.dcmImporte = decimal.Parse(item.Importe);
            tr.TotalImpuestoTrasladado = decimal.Parse(item.TotalImpuestosTraslados);
        }*/

        private void CreateDetalleFactura(int idEmpresa, Factura cfd, Helpers.ConceptoPreFactura item, Detalle_Factura dt)
        {
            dt.intID = Identifier.GetID(typeof(Detalle_Factura).Name, idEmpresa, cfd, EntityEnum.Detalle_Factura, db); ;

            
            dt.intID_Factura = cfd.intID;

            dt.intID_Producto = item.intIdProducto;

            dt.dcmCantidad = item.intCantidad;

            dt.dcmDescuento = item.dcmDescuento;

            dt.dcmImporte = item.dcmImporte;

            dt.strUnidad = item.strUnidad;

            dt.strConcepto = item.strConcepto;

            dt.dcmPrecioUnitario = item.dcmPrecioUnitario;

            dt.strPatida = item.strPartida.ToString();

            dt.dcmDescuento = item.dcmDescuento;

            dt.dcmIVA = item.dcmIVA;
            dt.retIVA = item.dcmRetIVA;
            dt.retISR = item.dcmRetISR;
            dt.retIEPS = item.dcmRetIEPS;
        }

        private void CreateNewFactura(int idTipoCFD,
            int idEmpresa, string serie, string folio,
            DateTime fecha, int idUsuario, int idCliente,
            string formaPago, string observaciones, decimal subTotal,
            decimal descueto, decimal IVA, decimal ToTal,
            string proveedor, string numero, string numeroContrato,
            string estimacion, int idCertificado, string cadenaOriginal,
            Factura cfd, decimal retIVA, decimal retISR, decimal retIEPS,
            string strCondicionesPago,
            string                 strMetodoPago,
            string               strMotivoDescuento,
            string                strDivisa,
            decimal                dcmTipoCambio,
             string origen,
         string recogerEn,
        
         string destino,
         string destinatario,
         string rfcDestinatario,
         string domicilioDestinatario,
         string entregarEn

                        
            )
        {
            cfd.intID = Identifier.GetID(typeof(Factura).Name, idEmpresa, cfd, EntityEnum.Factura, db);
            cfd.intID_Tipo_CFD = idTipoCFD;
            cfd.intID_Empresa = idEmpresa;
            cfd.strSerie = serie;
            cfd.strFolio = folio;
            cfd.dtmFecha = fecha;
            cfd.intID_Usuario = idUsuario;
            cfd.intID_Cliente = idCliente;
            cfd.strForma_Pago = formaPago;
            cfd.strObervaciones = observaciones;
            cfd.dcmSubTotal = subTotal;
            cfd.dcmDescuento = descueto;
            cfd.dcmIVA = IVA;
            cfd.dcmTotal = ToTal;
            cfd.chrStatus = "P";
            cfd.strProveedor = proveedor;
            cfd.strNumero = numero;
            cfd.strNumeroContrato = numeroContrato;
            cfd.intEstimacion = estimacion;
            cfd.intID_Certificate = idCertificado;
            cfd.strCadenaOriginal = cadenaOriginal;
            cfd.dcmRetIVA = retIVA;
            cfd.dcmRetISR = retISR;
            cfd.dcmRetIEPS = retIEPS;
            cfd.CondPago = strCondicionesPago;
            cfd.MetodoPago = strMetodoPago;
            cfd.MotivoDesc = strMotivoDescuento;
            cfd.Divisa = strDivisa;
            cfd.TipoCambio = dcmTipoCambio;
            cfd.Origen = origen;
            cfd.RecogerEn = recogerEn;
            cfd.Destino = destino;
            cfd.Destinatario = destinatario;
            cfd.rfcDestinatario = rfcDestinatario;
            cfd.domicilioDestinatario = domicilioDestinatario;
            cfd.EntregarEn = entregarEn;

            if (cadenaOriginal.Contains("Factoraje"))
            {

                string[] urls = cadenaOriginal.Split('|');

                cfd.strXMLpath = urls[1];
                cfd.strPDFpath = urls[2];
                cfd.chrStatus = "A";

                string strXML = System.IO.File.ReadAllText(cfd.strXMLpath, UTF8Encoding.UTF8);
                int pos = strXML.IndexOf("UUID=");
                string UUID = strXML.Substring(pos + 6, 36);
                cfd.strSelloDigital = UUID;

            }
        }
    }
}
