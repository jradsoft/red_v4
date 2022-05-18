using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace wpfEFac.Models
{
    public class ConnectorImportSAE40 : ConnectorTypes
    {

        private void importCliente(DataRow r)
        {
            strIdCliente = r["cclie"].ToString();
            strRfcCliente = r["cclie"].ToString();
            strRazonSocialCliente = r["cclie"].ToString();
            strNombreComercialCliente = r["cclie"].ToString();
            strGiroCliente = r["cclie"].ToString();
            strTipoInscripcionCliente = r["cclie"].ToString();
            strTelefonoCliente = r["cclie"].ToString();
            strMobilCliente = r["cclie"].ToString();
            strEmailCliente = r["cclie"].ToString();
            strRetieneIvaCliente = r["cclie"].ToString();
            strContactoCliente = r["cclie"].ToString();
            strWebsiteCliente = r["cclie"].ToString(); 

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

        private void importProducto(DataRow r)
        {
            intIdProducto = 0; // Checar
            intIdCategoriaProducto = 0; // Cambio
            strNombreProducto = r[""].ToString();
            strCodigoProducto = r[""].ToString();
            dcmPrecioProducto = r[""].ToString();
            dcmActos_IVAProducto = r[""].ToString();
            dcmDescuentoProducto = r[""].ToString();
            intUnidadProducto = 0; // Cambio
            strDescripcionProducto = r[""].ToString();

            conSave.saveProducto(intIdProducto, intIdCategoriaProducto,
                strNombreProducto, strCodigoProducto,
                dcmPrecioProducto, dcmActos_IVAProducto,
                dcmDescuentoProducto, intUnidadProducto,
                strDescripcionProducto);
        }


        public int importData(String strPath, String type)
        {
            int n = 0;

            try
            {
                ConnectorOleDb myConnData = new ConnectorOleDb(strPath);
                DataTable myDataTable = myConnData.select("Select * from " + type);
                conSave = new ConnectorSave();

                foreach (DataRow r in myDataTable.Rows)
                {
                    
                    if (type == "Producto") importProducto(r);
                    if (type == "Cliente") importCliente(r);
                }
                
            }
            catch (IOException e)
            {

            }

            return 0;
        }
    }
}
