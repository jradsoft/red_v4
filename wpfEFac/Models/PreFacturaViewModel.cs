using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    public class PreFacturaViewModel
    {
        private eFacDBEntities db;

        public PreFacturaViewModel()
        {
            db = new eFacDBEntities();
        }

        public Empresa GetEmpresa(int idUsuario) 
        {
            return new Login().GetEmpresa(idUsuario);
        }

        public Usuarios GetUsuario(int idUsuario)
        {
            return new Login().GetUsuario(idUsuario);
        }

        public ObservableCollection<Clientes> GetClientes() 
        {
            DataClientes clientesDb = new DataClientes();

            return clientesDb.GetClientes();
        }

        public Direcciones_Fiscales GetDireccionCliente(int idCliente) 
        {
            return new DataPreFactura().GetDireccionesFiscalesClientes(idCliente);
        }

        public Direcciones_Fiscales GetDireccionEmpresa(int idEmpresa) 
        {
            return new DataPreFactura().GetDireccionesFiscalesEmpresa(idEmpresa);
        }


        public Direcciones_Fiscales GetDireccionEmpresaEmision(int idEmpresa)
        {
            return new DataPreFactura().GetDireccionesEmisionEmpresa(idEmpresa);
        }

        public System.Collections.IEnumerable GetTipoInscripcion()
        {
            var tiposInscripcion = from ti in GetClientes()
                                   select ti.strTipodeInscripcion;

            List<string> result = tiposInscripcion.ToList();

            result.Add("Todos");

            result.Reverse();

            return result.Distinct();
        }

        public ObservableCollection<Clientes> buscar(string rfc, string nombreComercial, string razonSocial, string tipoInscripcion) 
        {
            var encontrados = from c in GetClientes()
                              where c.strRFC.ToUpper().Contains(rfc != null ? rfc.ToUpper() : string.Empty)
                              && c.strNombreComercial.ToUpper().Contains(nombreComercial != null ? nombreComercial.ToUpper() : string.Empty)
                              && c.strRazonSocial.ToUpper().Contains(razonSocial != null ? razonSocial.ToUpper() : string.Empty)
                              && c.strTipodeInscripcion.ToUpper().Equals(tipoInscripcion != "Todos" ? tipoInscripcion.ToUpper() : c.strTipodeInscripcion.ToUpper())
                              select c;
            
            return new ObservableCollection<Clientes>(encontrados);
        }
        /*
         * 
         *PENDIENTES DE CAMBIOS 
         * 
         * 
         */

        public bool UpdateFolioActual(int idCertificado, int intTipoCFD) 
        {
            try
            {
                //var myCFD = db.CFD.Where(c => c.intID == intTipoCFD).First();

                //db.Productos.First(pro => pro.intID == id);

                Folios folio = db.CFD.FirstOrDefault(c=>c.intID == intTipoCFD).Folios;
                
                folio.intFolioActual = folio.intFolioActual + 1;

                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
    }
}
