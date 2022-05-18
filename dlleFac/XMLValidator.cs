using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace dlleFac
{
    public class XMLValidator
    {

        private static bool isValid = true;      // Si se produce un error de validación,
        // configure este indicador como false
        // en el controlador de eventos de validación.
        private string strError = string.Empty;
 
        public static void MyValidationEventHandler(object sender,
                                            ValidationEventArgs args)
        {
            isValid = false;
            Console.WriteLine("Evento de validación\n" + args.Message);
        }


        public string Validate(string strXML, string strXSD)
        {

            XmlTextReader r = new XmlTextReader(strXML);
            XmlValidatingReader v = new XmlValidatingReader(r);
            XmlSchemaCollection cache = new XmlSchemaCollection();
            cache.Add("urn:MyNamespace", "cfdv2.xsd");


            v.Schemas.Add(cache);

            v.ValidationType = ValidationType.Schema;
            v.ValidationEventHandler +=
                new ValidationEventHandler(MyValidationEventHandler);

            while (v.Read())
                {
                   // Puede agregar código aquí para procesar el contenido.
                }
                v.Close();

                            // Comprobar si el documento es válido o no.
                if (isValid)
                   Console.WriteLine("El documento es válido");
                else
                   Console.WriteLine("El documento no es válido");

                return strError;
        }
    }
}