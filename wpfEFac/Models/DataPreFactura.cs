using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    public class DataPreFactura
    {
        private eFacDBEntities db = new eFacDBEntities();

        public Empresa GetEmpresas(int idEmprea) 
        {
            return db.Empresa.First(e => e.intID == idEmprea);
        }

        public Direcciones_Fiscales GetDireccionesFiscalesEmpresa(int id) 
        {
            try
            {
                var idDir = from de in db.Direcciones_Fiscales
                            join e in db.Empresa
                            on de.strIDCliente equals e.intID
                            where de.strIDCliente == id &&
                            de.chrStatus == "1"
                            && de.strTipoEntidad == "E"
                            select de.intID;

                int idDireccionFiscal = idDir.First();

                return db.Direcciones_Fiscales.First(df => df.intID == idDireccionFiscal);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public Direcciones_Fiscales GetDireccionesEmisionEmpresa(int id)
        {
            try
            {
                var idDir = from de in db.Direcciones_Fiscales
                            join e in db.Empresa
                            on de.strIDCliente equals e.intID
                            where de.strIDCliente == id &&
                            de.chrStatus == "1"
                            && de.strTipoEntidad == "D"
                            select de.intID;

                int idDireccionFiscal = idDir.First();

                return db.Direcciones_Fiscales.First(df => df.intID == idDireccionFiscal);
            }
            catch (Exception)
            {
                return null;
            }
        }



        public Direcciones_Fiscales GetDireccionesFiscalesClientes(int id) 
        {
            try
            {
                var direccion = db.Direcciones_Fiscales.First(df => df.strIDCliente == id && df.chrStatus == "1" && df.strTipoEntidad == "C");
                return direccion;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
