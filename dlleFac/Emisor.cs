using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlleFac
{
    public class Emisor
    {
        public string RFCEmisor { get; set; }
        public string NombreEmisor { get; set; }

        public override string ToString()
        {
            return RFCEmisor + ComprobanteFiscalDigital.pipe + NombreEmisor + ComprobanteFiscalDigital.pipe;
        }
    }
}
