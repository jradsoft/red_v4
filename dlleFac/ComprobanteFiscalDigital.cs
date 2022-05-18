using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlleFac
{
    public class ComprobanteFiscalDigital
    {

        #region Name Spaces
        public List<string> nameSpaces = new List<string>()
                                        {
                                             "http://www.sat.gob.mx/cfd/2"
                                        };

        public List<string> schemaLocation = new List<string>()
                                        {
                                            "http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd"
                                        };

        #endregion

        #region Public Constants

        public const string pipe = "|";
        public const string inicio = pipe + pipe;
        public const string fin = inicio;

        #endregion

        #region Private Members

        private string version = "2.0";

        #endregion

        #region Comprobante

        //Datos del comprovante
        public string Version { get { return version; } set { version = value; } }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public string NoAprobacion { get; set; }
        public string AñoAprobacion { get; set; }
        public string FormaPago { get; set; }
        public string CondicionesPago { get; set; }
        public string SubTotal { get; set; }
        public string Descuento { get; set; }
        public string Total { get; set; }
        public string metodoDePago { get; set; } // cheque, tarjeta de crédito o debito, depósito en cuenta, etc.
        public string TipoComprobante { get; set; } // ingreso, esgreso o traslado

        #endregion

        #region Emisor

        //Datos del emisor
        public Emisor Emisor { get; set; }

        //Domicilio fiscal del emisor
        public DomicilioFiscal DomicilioFiscalEmisor { get; set; }

        //Datos de domiclio de expedicion del comprobante
        public DomicilioFiscal ExpedidoEn { get; set; }

        #endregion

        #region Receptor

        //Datos del receptor
        public Receptor Receptor { get; set; }

        //Domicilio Fiscal del Receptor
        public DomicilioFiscal DomicilioFiscalReceptor { get; set; }

        #endregion

        public List<Concepto> Conceptos { get; set; }

        public Impuestos Impuestos { get; set; }

        #region Normatividad Generación de la Cadena Original

        //        1)      Datos del comprobante
        //a)      Versión
        //b)      Serie
        //c)      Folio
        //d)     Fecha
        //e)      Número de Aprobación
        //f)       Año de Aprobación
        //g)      Tipo de Comprobante
        //h)      Forma de Pago
        //i)        Condiciones de Pago
        //j)        Subtotal
        //k)      Descuento
        //l)        Total

        //2)      Datos del emisor
        //a.-RFC del Emisor
        //b.-Nombre o denominación social del emisor

        //3)      Datos del domicilio fiscal del emisor
        //a.-Calle del domicilio fiscal del emisor
        //b.-Número exterior del Domicilio Fiscal del Emisor
        //c.-Número Interior del Domicilio Fiscal del Emisor
        //d.-Colonia del Domicilio Fiscal del Emisor
        //e.-Localidad del Domicilio Fiscal del Emisor
        //f.-Referencia del Domicilio Fiscal del Emisor
        //g.-Municipio del Domicilio Fiscal del Emisor
        //h.-Estado del Domicilio Fiscal del Emisor
        //i.-País del Domicilio Fiscal del Emisor
        //j.-Código Postal del Domicilio Fiscal del Emisor

        //4)      Datos del Domicilio de Expedición del Comprobante
        //a.-Calle del Domicilio De Expedición del Comprobante
        //b.-Número Exterior del Domicilio De Expedición del Comprobante
        //c.-Número Interior del Domicilio De Expedición del Comprobante
        //d.-Colonia del Domicilio De Expedición del Comprobante
        //e.-Localidad del Domicilio De Expedición del Comprobante
        //f.-Referencia del Domicilio De Expedición del Comprobante
        //g.-Municipio del Domicilio De Expedición del Comprobante
        //h.-Estado del Domicilio De Expedición del Comprobante
        //i.-País del Domicilio De Expedición del Comprobante
        //j.-Código Postal del Domicilio De Expedición del Comprobante

        //5)      Datos del Receptor
        //a.-RFC del Receptor
        //b.-Nombre o Denominación Social del Receptor

        //6)      Datos del Domicilio Fiscal del Receptor
        //a.-Calle del Domicilio Fiscal del Receptor
        //b.-Número Exterior del Domicilio Fiscal del Receptor
        //c.-Número Interior del Domicilio Fiscal del Receptor
        //d.-Colonia del Domicilio Fiscal del Receptor
        //e.-Localidad del Domicilio Fiscal del Receptor
        //f.-Referencia del Domicilio Fiscal del Receptor
        //g.-Municipio del Domicilio Fiscal del Receptor
        //h.-Estado del Domicilio Fiscal del Receptor
        //i.-País del Domicilio Fiscal del Receptor
        //j.-Código Postal del Domicilio Fiscal del Receptor

        //7)      Datos de Cada Concepto Relacionado en el Comprobante
        //Nota: Esta secuencia deberá ser repetida por cada concepto relacionado:

        //a)      Cantidad
        //b)      Unidad de Medida
        //c)      No Identificación
        //d)     Descripción
        //e)      Valor Unitario
        //f)       Importe
        //g)      Número del Documento Aduanero
        //h)      Fecha de Expedición del Documento Aduanero
        //i)        Aduana que Expide el Documento Aduanero
        //j)        Número de la Cuenta Predial 

   
        //8)      Datos de Cada Retención de Impuestos
        //Nota: Esta secuencia deberá ser repetida por cada impuesto retenido relacionado en el comprobante

        //a)      Tipo de Impuesto
        //b)      Importe
        //c)      Total Impuestos Retenidos

        //9)      Datos de Cada Traslado de Impuestos
        //Nota: Esta secuencia deberá ser repetida por cada impuesto trasladado relacionado en el comprobante

        //a)      Tipo de Impuesto
        //b)      Tasa
        //c)      Importe
        //d)     Total Impuestos Trasladados 

        #endregion

        public ComprobanteFiscalDigital ()
	    {

	    }

        public override string ToString()
        {
            string result = inicio;

            result += Version + pipe; // 0
            result += Serie != null ? Serie + pipe: string.Empty; // 1
            result += Folio + pipe; // 2
            result += Fecha + pipe; // 3
            result += NoAprobacion + pipe; // 4
            result += AñoAprobacion + pipe; // 5
            result += TipoComprobante + pipe; // 6
            result += FormaPago + pipe; // 7
            result += CondicionesPago != null ? CondicionesPago + pipe : string.Empty;
            result += SubTotal + pipe; // 8
            result += Descuento != null ? Descuento + pipe : "0" + pipe; // 9
            result += Total + pipe; // 10
            result += Emisor.ToString();
            result += DomicilioFiscalEmisor.ToString() ;
            result += ExpedidoEn.ToString();
            result += Receptor.ToString();
            result += DomicilioFiscalReceptor.ToString();
            result += GetConceptos(); // Conceptos
            result += GetRetenciones(); // Retenciones
            result += GetTraslados(); // Traslados
            result += pipe; // Fin

            return result;

            // Version                    
            //,Serie
            //,Folio
            //,Fecha
            //,NúmeroAprobacion
            //,AñoAprobacion,
            //TipoComprobante
            //,FormaPago
            //,CondicionesPago
            //,SubTotal,
            //Descuento,
            //Total,
            //RFCEmisor,
            //NombreEmisor,
            //CalleEmisor,
            //NumeroExteriorEmisor,
            //NumeroInteriorEmisor,
            //ColoniaEmisor,
            //LocalidadEmisor,
            //ReferenciaDomicilioEmisor, 
            //MunicipioDomicilioEmisor,
            //EstadoDomicilioEmisor,
            //PaisDomicilioEmisor,
            //CodigoPostalDomicilioEmisor,
            //CalleDomicilioExpedicion,
            //NumeroExteriorDomicilioExpedicion,
            //NumeroInteriorDomicilioExpedicion,
            //ColoniaDomicilioExpedicion,
            //LocalidadDomicilioExpedicion,
            //ReferenciaDomicilioExpedicion,
            //MunicipioDomicilioExpedicion,
            //EstadoDomicilioExpedicion, 
            //PaisDomicilioExpedicion,
            //CodigoDomicilioExpedicion,
            //RFCReceptor,
            //NombreReceptor,
            //CalleReceptor, 
            //NumeroExteriorReceptor,
            //NumeroInteriorReceptor, 
            //ColoniaReceptor,
            //LocalidadReceptor,
            //ReferenciaReceptor,
            //MunicipioReceptor,
            //EstadoReceptor,
            //PaisReceptor, 
            //CodigoPostalReceptor
        }

        private string GetTraslados()
        {
            string result = string.Empty;

            string total=string.Empty;
            
            if (Impuestos.Traslados != null)
            {
                foreach (var item in Impuestos.Traslados)
                {
                    result += item.TipoImpuesto + pipe + item.Tasa + pipe
                        + item.Importe + pipe;

                    total = item.TotalImpuestosTraslados + pipe;
                }

                result += total;
            }

            return result;
        }

        private string GetRetenciones()
        {
            string result = string.Empty;

            if (Impuestos.Retenciones != null)
            {
                foreach (var item in Impuestos.Retenciones)
                {
                    result += item.TipoImpuesto + pipe +
                        item.Importe + pipe + item.TotalImpuestoRetenido;
                }
                result += pipe;
            }
            
            return result;
        }

        private string GetConceptos()
        {
            string result = string.Empty;

            foreach (var item in Conceptos)
            {
                result += item.Cantidad + pipe + item.UnidadMedida
                    + pipe + item.Descripcion + pipe + item.ValorUnitario +
                    pipe + item.Importe + pipe;

                if (item.InformacionAduanera!= null)
                {
                    foreach (var infoAduanera in item.InformacionAduanera)
                    {
                        result += infoAduanera.Numero + pipe +
                            Fecha + pipe + infoAduanera.Aduana + pipe;
                    }
                }

            }

            return result;
        }

        public Adenda Adenda { get; set; }

    }
}

