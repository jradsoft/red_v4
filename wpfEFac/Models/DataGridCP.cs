using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    class dataGridCP
    {


        public class FillDataGrid
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

        public class DataCP {

            public FillDataGrid fillData { get; set; }
        

        
        }


    }

}
