using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlleFac
{
    public class Parte
    {
        public string Cantidad { get; set; }
        public string Unidad { get; set; }
        public string NoIdentificacion { get; set; }
        public string Descripcion { get; set; }
        public string ValorUnitario { get; set; }
        public string Importe { get; set; }

        public List<InformacionAduanera> InformacionAduanera { get; set; }
    }
}


