using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Helpers
{
    public class Detalle_PreFactura
    {
        public decimal intCantidad { get; set; }
        public decimal dcmDescento { get; set; }
        public string strUnidad { get; set; }
        public string dcmImporte { get; set; }
        public string dcmPrecioUnitario { get; set; }
        public string strPartida { get; set; }
        public string dcmIIVA { get; set; }
        public string strConcepto { get; set; }
    }
}
