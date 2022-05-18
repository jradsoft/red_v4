using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlleFac
{
    public class InformacionAduanera
    {
        public string Numero { get; set; }
        /// <summary>
        /// Format
        /// aaaa-mm-dd
        /// </summary>
        public string Fecha { get; set; } 
        public string Aduana { get; set; }
    }
}
