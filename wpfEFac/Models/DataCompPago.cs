using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    class DataCompPago
    {

        public class fillDataCompPago
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

        }

        public class DataComplementoPago
        {



            public List<fillDataCompPago> fillData { get; set; }
            public decimal dcmTotal { get; set; }
            public string dtmFechaPago { get; set; }
            public string rfcCtaBeneficiario { get; set; }
            public string rfcCtaOrdenante { get; set; }
            public string strNumOperacion { get; set; }
            public string strMoneda { get; set; }
            

        }
    }
}
