using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class ConnectorSave
    {
        eFacDBEntities myDb = new eFacDBEntities();


        private bool existeCategoria(int idCategoria)
        {
            Categorias myCat = getCategoria(idCategoria);
            if (myCat == null)
            {
                myCat = new Categorias
                {
                    intID = idCategoria,
                    strNombre = idCategoria.ToString()
                };

                myDb.Categorias.AddObject(myCat);
                myDb.SaveChanges();
            }

            return true;
        }


        public Categorias getCategoria(int id)
        {
            try
            {
                return myDb.Categorias.First(p => p.intID == id);
            }
            catch (Exception)
            {
                return null;
            }
        }



        private bool existeUnidad(int intParUnidad)
        {
            UnidadMedida myUnidad = getUnidad(intParUnidad);
            if (myUnidad == null)
            {
                //myUnidad = new UnidadMedida
                //{
                //    intId = intParUnidad,
                //    strDescripcion = intParUnidad
                //};

                //myDb.UnidadMedida.AddObject(myUnidad);
                
                myDb.SaveChanges();
            }

            return true;
        }


        public UnidadMedida getUnidad(int id)
        {
            try
            {
                return myDb.UnidadMedida.First(p => p.intId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool existeunidad(String strParIdUnidad)
        {

            return true;
        }


        public void saveProducto(int intParID, 
                                int intParID_Categoria, 
                                String strParNombre,
                                String strParCodigo, 
                                String dcmParPrecio, 
                                String dcmParActos_IVA , 
                                String dcmParDescuent, 
                                int intParUnidad, 
                                String strParDescripcion)
        {

            existeCategoria(intParID_Categoria);
            existeUnidad(intParUnidad);

            Productos myProducto = new Productos
            {
                intID = intParID,
                intID_Categoria = intParID_Categoria,
                strNombre = strParNombre,
                strCodigo = strParCodigo,
                //dcmPrecio = decimal.Parse(dcmParPrecio),
                //dcmActos_IVA = decimal.Parse(dcmParActos_IVA),
                dcmDescuent = decimal.Parse(dcmParDescuent),
                intUnidad = intParUnidad,
                strDescripcion = strParDescripcion
            };

            myDb.Productos.AddObject(myProducto);
            myDb.SaveChanges();
                   
        }



        public void saveCliente(
                                String strParIdCliente,
                                String strParRfcCliente,
                                String strParRazonSocialCliente,
                                String strParNombreComercialCliente,
                                String strParGiroCliente,
                                String strParTipoInscripcionCliente,
                                String strParTelefonoCliente,
                                String strParMobilCliente,
                                String strParEmailCliente,
                                String strParRetieneIvaCliente,
                                String strParContactoCliente,
                                String strParWebsiteCliente
            
            )
        {
            //Clientes myCliente= new Clientes
            //{
            
            //                    intID = strParIdCliente,
            //                    strRFC = strParRfcCliente,
            //                    strRazonSocial = strParRazonSocialCliente,
            //                    strNombreComercial= strParNombreComercialCliente,
            //                    strGiro = strParGiroCliente,
            //                    strTipodeInscripcion = strParTipoInscripcionCliente,
            //                    strTelefono = strParTelefonoCliente,
            //                    strTelefonoMovil = strParMobilCliente,
            //                    strEmail = strParEmailCliente,
            //                    chrRetencionIVA = strParRetieneIvaCliente,
            //                    strContacto = strParContactoCliente,
            //                    strWebSite = strParWebsiteCliente
            

            //};

            //myDb.Clientes.AddObject(myCliente);
            //myDb.SaveChanges();

        }
    }
}
