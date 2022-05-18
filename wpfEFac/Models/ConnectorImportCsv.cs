using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace wpfEFac.Models
{
    public class ConnectorImportCsv : ConnectorTypes
    {



        public int importCsv(String strFilePath, String type)
        {
            int n = 0;

            try
            {
                StreamReader re = File.OpenText(strFilePath);
                string input = null;

                conSave = new ConnectorSave();

                while ((input = re.ReadLine()) != null)
                {
                    string[] words = input.Split(',');
                    if (type == "Producto") importProducto(words);
                    if (type == "Cliente") importCliente(words);
                }
                re.Close();
            }
            catch (IOException e)
            {

            }


            return n;
        }

        private void importProducto(string[] words)
        {
            //intIdProducto = Convert.ToInt32(words[0];
            //intIdCategoriaProducto = Convert.ToInt32(words[1]);
            //strNombreProducto = words[2];
            //strCodigoProducto = words[3];
            //dcmPrecioProducto = words[4];
            //dcmActos_IVAProducto = words[5];
            //dcmDescuentoProducto = words[6];
            //intUnidadProducto = words[7];
            //strDescripcionProducto = words[8];

            //conSave.saveProducto(intIdProducto, intIdCategoriaProducto,
            //    strNombreProducto, strCodigoProducto,
            //    dcmPrecioProducto, dcmActos_IVAProducto,
            //    dcmDescuentoProducto, intUnidadProducto,
            //    strDescripcionProducto);        
        }

        private void importCliente(string[] words)
        {
            strIdCliente = words[0];
            strRfcCliente = words[1];
            strRazonSocialCliente = words[2];
            strNombreComercialCliente = words[3];
            strGiroCliente = words[4];
            strTipoInscripcionCliente = words[5];
            strTelefonoCliente = words[6];
            strMobilCliente = words[7];
            strEmailCliente = words[8];
            strRetieneIvaCliente = words[9];
            strContactoCliente = words[10];
            strWebsiteCliente = words[11];

            conSave.saveCliente(
                                strIdCliente,
                                strRfcCliente,
                                strRazonSocialCliente,
                                strNombreComercialCliente,
                                strGiroCliente,
                                strTipoInscripcionCliente,
                                strTelefonoCliente,
                                strMobilCliente,
                                strEmailCliente,
                                strRetieneIvaCliente,
                                strContactoCliente,
                                strWebsiteCliente                
                );
        }


    }


    
}
