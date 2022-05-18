using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class ConnectorTypes
    {
        /*
         * Datos de productos
         */
        protected int intIdProducto = 0;
        protected int intIdCategoriaProducto = 0;
        protected String strNombreProducto = "";
        protected String strCodigoProducto = "";
        protected String dcmPrecioProducto = "";
        protected String dcmActos_IVAProducto = "";
        protected String dcmDescuentoProducto = "";
        protected int intUnidadProducto = 0;
        protected String strDescripcionProducto = "";


        /*
         * Datos de Clientes
         */

        protected String strIdCliente = "";
        protected String strRfcCliente = "";
        protected String strRazonSocialCliente = "";
        protected String strNombreComercialCliente = "";
        protected String strGiroCliente = "";
        protected String strTipoInscripcionCliente = "";
        protected String strTelefonoCliente = "";
        protected String strMobilCliente = "";
        protected String strEmailCliente = "";
        protected String strRetieneIvaCliente = "";
        protected String strContactoCliente = "";
        protected String strWebsiteCliente = "";
        

        /*
         * Conector a la base de datos 
         */
        protected ConnectorSave conSave;
    }
}
