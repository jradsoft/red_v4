using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace dlleFac
{
    public class Factura
    {
        public string sello { get; set; }
        public string certificado { get; set; }
        public string noCertificado { get; set; }
        public XmlTextWriter xml { get; set; }
        public string filePath { get; set; }
        public string cadenaOriginal { get; set; }
        public string fileXMLpath { get; set; }
        public string filePDFpath { get; set; }
        public string strValError { get; set; }
        public string serie { get; set; }
        public string folio { get; set; }
        public string UUID { get; set; }
        public DateTime fechaAprobacion { get; set; }
    }
}
