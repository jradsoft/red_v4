using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    class dataServParCons
    {


        public class FillData
        {
            public string NumPermisoSPC { get; set; }
            public string Calle { get; set; }
            public string NoInterior { get; set; }
            public string NoExterior { get; set; }
            public string Colonia { get; set; }
            public string Localidad { get; set; }
            public string Referencia { get; set; }
            public string Municipio { get; set; }
            public string Estado { get; set; }
            public string CodigoPostal { get; set; }
        }

        public class DataSPC {

            public FillData fillData { get; set; }
        

        
        }


    }

}
