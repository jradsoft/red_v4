using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlleFac
{
    public class DomicilioFiscal
    {
        public string Calle { get; set; }
        
        public string NumeroExterior { get; set; }
        
        public string NumeroInterior { get; set; }
        
        public string Colonia { get; set; }
        
        public string Localidad { get; set; }
        
        public string Referencia { get; set; }
        
        public string Municipio { get; set; }
        
        public string Estado { get; set; }
        
        public string Pais { get; set; }
        
        public string CodigoPostal { get; set; }

        public override string ToString()
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(Pais))
            {
                throw new ApplicationException("Pais no puede estar vacio");
            }

            if (!string.IsNullOrWhiteSpace(Calle))
            {
                result += Calle + ComprobanteFiscalDigital.pipe;
            }

            if (!string.IsNullOrWhiteSpace(NumeroExterior))
            {
                result += NumeroExterior + ComprobanteFiscalDigital.pipe;
            }
            
            if (!string.IsNullOrWhiteSpace(NumeroInterior))
            {
                result +=  NumeroInterior + ComprobanteFiscalDigital.pipe;
            }

            if (!string.IsNullOrWhiteSpace(Colonia))
            {
                result += Colonia + ComprobanteFiscalDigital.pipe;
            }

            if (!string.IsNullOrWhiteSpace(Localidad))
            {
                result += Localidad + ComprobanteFiscalDigital.pipe;
            }

            if (!string.IsNullOrWhiteSpace(Municipio))
            {
                result += Municipio + ComprobanteFiscalDigital.pipe;
            }

            if (!string.IsNullOrWhiteSpace(Estado))
            {
                result += Estado + ComprobanteFiscalDigital.pipe;
            }
            
            result += Pais + ComprobanteFiscalDigital.pipe;

            if (!string.IsNullOrWhiteSpace(CodigoPostal))
            {
                result += CodigoPostal + ComprobanteFiscalDigital.pipe;
            }

            return result;
        }

    }
}
