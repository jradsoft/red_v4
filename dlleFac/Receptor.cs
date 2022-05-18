using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dlleFac
{
    public class Receptor
    {
        public string RFCReceptor { get; set; }
        public string NombreReceptor { get; set; }

        public override string ToString()
        {
            return RFCReceptor + ComprobanteFiscalDigital.pipe + NombreReceptor + ComprobanteFiscalDigital.pipe;
        }
    }
}
