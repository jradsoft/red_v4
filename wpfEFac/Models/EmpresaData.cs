using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    public class EmpresaData
    {
        eFacDBEntities db;

        public EmpresaData()
        {
            db = new eFacDBEntities();
        }

        public Empresa getEmpresa(int id)
        {
            try
            {
                return db.Empresa.First(p => p.intID == id);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public Direcciones_Fiscales GetDireccionFiscal(int idCliente) 
        {
            try
            {
                var idDireccion = from d in db.Direcciones_Fiscales
                                  join e in db.Empresa
                                  on d.strIDCliente equals e.intID
                                  where d.strTipoEntidad == "E"
                                  && d.chrStatus == "1"
                                  && d.strIDCliente == idCliente
                                  select new
                                  {
                                      d.intID
                                  };

                int idDireccionFiscal = idDireccion.First().intID;

                return db.Direcciones_Fiscales.FirstOrDefault(dir => dir.intID == idDireccionFiscal && dir.strTipoEntidad == "E");
            }
            catch (Exception)
            {
                return null;
            }                                
        }

        public Direcciones_Fiscales GetDireccionFiscal(int idEmpresa, eFacDBEntities db) 
        {
            Direcciones_Fiscales direccion = db.Direcciones_Fiscales.First(d => d.intID_Empresa == idEmpresa && d.strIDCliente == idEmpresa && d.chrStatus == "1" && d.strTipoEntidad == "E");

            return direccion;
        }

        public ObservableCollection<Paises> GetPais()
        {
            return new ObservableCollection<Paises>(db.Paises);
        }

        public ObservableCollection<Estado> GetEstado()
        {
            return new ObservableCollection<Estado>(db.Estado);
        }

        public ObservableCollection<CFD> GetCFD()
        {
            return new ObservableCollection<CFD>(db.CFD);
        }

        
    }
}
