using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Helpers
{
    public class ConceptoPreFactura
    {
        public int intIdProducto { get; set; }
        public decimal intCantidad { get; set; }
        public decimal dcmDescuento { get; set; }
        public decimal dcmImporte { get; set; }
        public string strUnidad { get; set; }
        public string strConcepto { get; set; }
        public decimal dcmPrecioUnitario { get; set; }
        public string strPartida { get; set; }

        //conceptos.Add(new Detalle_Factura()
        //{
        //    strID_Producto = concepto.strID,
        //    intCantidad = concepto.Cantidad,
        //    dcmDescuento = decimal.Parse(concepto.Descuento),
        //    dcmImporte = concepto.Importe,
        //    strUnidad = concepto.Unidad,
        //    strConcepto = concepto.Nombre,
        //    dcmPrecioUnitario = concepto.PrecioUnitario,
        //    strPartida = int.Parse(txtPartida.Text)
        //});

        public decimal dcmIVA { get; set; }
        public decimal dcmRetIVA { get; set; }
        public decimal dcmRetISR { get; set; }
        public decimal dcmRetIEPS { get; set; }
    }
}
