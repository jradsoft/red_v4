using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    public class DataClientes
    {
        eFacDBEntities db;

        public DataClientes()
        {
            db = new eFacDBEntities();
        }

        public ObservableCollection<Clientes> GetClientes() 
        {
            return new ObservableCollection<Clientes>(db.Clientes.Where(x => x.strTipodeInscripcion.Equals("Persona Física") || x.strTipodeInscripcion.Equals("Persona Moral")));
        }

        public Clientes getCliente(int id)
        {
            try
            {
                return db.Clientes.First(p => p.intID == id );
            }
            catch (Exception)
            {
                return null;              
            }
        }

        public Clientes getRFC(string RFC)
        {
            try
            {
                return db.Clientes.First(p => p.strRFC == RFC);
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public Clientes getRazonSocial(string Nombre)
        {
            try
            {
                return db.Clientes.First(p => p.strRazonSocial == Nombre);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public Clientes getNombreComercial(string Nombre)
        {
            try
            {
                return db.Clientes.First(p => p.strNombreComercial == Nombre);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public Clientes getGiro(string Giro)
        {
            try
            {
                return db.Clientes.First(p => p.strGiro == Giro);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public Clientes getTipoInscripcion(string Tipo)
        {
            try
            {
                return db.Clientes.First(p => p.strTipodeInscripcion == Tipo);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public Direcciones_Fiscales getCalle(int idCliente)
        {
            var calle = db.Direcciones_Fiscales.FirstOrDefault(dire => dire.strIDCliente == idCliente && dire.strTipoEntidad == "C");

            return calle;         
        }

        public Clientes getTelefono(string Telefono)
        {
            try
            {
                return db.Clientes.First(p => p.strTelefono == Telefono);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public Clientes getTelefonoMovil(string Telefono)
        {
            try
            {
                return db.Clientes.First(p => p.strTelefonoMovil == Telefono);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public Clientes getEmail(string Email)
        {
            try
            {
                return db.Clientes.First(p => p.strEmail == Email);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public Clientes getContacto(string Contacto)
        {
            try
            {
                return db.Clientes.First(p => p.strContacto == Contacto);
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public Clientes getWeb(string Web)
        {
            try
            {
                return db.Clientes.First(p => p.strWebSite == Web);
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public ObservableCollection<Paises>getPaises()
        {
            return new ObservableCollection<Paises>(db.Paises);
        }

        public ObservableCollection<Estado> getEstado()
        {
            return new ObservableCollection<Estado>(db.Estado);
        }
    }
}
