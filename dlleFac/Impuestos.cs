using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlleFac
{
    public class Impuestos
    {
        public string TotalTraslados { get; set; }
        public string TotalRetenido { get; set; }
        public List<Retencion> Retenciones { get; set; }

        public List<Traslado> Traslados { get; set; }
    }
}
