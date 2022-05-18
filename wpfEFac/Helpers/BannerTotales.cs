using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Helpers
{
    public class BannerTotales
    {
        public string Descripcion { get; set; }
        public int Hoy { get; set; }
        public int Mes { get; set; }
        public int Aut { get; set; }
        public int Pend { get; set; }
        public int Env { get; set; }
        public decimal Total { get; set; }
    }
}
