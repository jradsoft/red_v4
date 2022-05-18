using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wpfEFac.Models;

namespace wpfEFac.Helpers
{
    public class CFDReporte
    {
        // Header
        public string Logo { get; set; }
        public string NombreEmisor { get; set; }
        public string DomicilioEmisor { get; set; }
        public string RFCEmisor { get; set; }
        public string Telefono { get; set; }
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public string NumeroCertificado { get; set; }
        public string NumeroAprobacion { get; set; }
        public string AnoArobacion { get; set; }
        public string NombreCliente { get; set; }
        public string DomicilioCliente { get; set; }
        public string RFCCliente { get; set; }
        public string Proveedor { get; set; }
        public string NoPedido { get; set; }
        public string NoContrato { get; set; }
        public string Usuario { get; set; }
        public string Estimacion { get; set; }
        public string Observaciones { get; set; }
        
        //Conceptos
        public List<Detalle_PreFactura> Conceptos { get; set; }

        public List<Detalle_PreFactura> GetConceptos() 
        {
            return Conceptos;
        }

        public string Cedula { get; set; }
        public string CadenaOriginal { get; set; }
        public string SelloDigital { get; set; }
        public string SubTotal { get; set; }
        public string IVA { get; set; }
        public string Total { get; set; }
        public string retIVA { get; set; }
        public string retISR { get; set; }
        public string TotalNeto { get; set; }
        public string ImporteLetra { get; set; }

    }
}
