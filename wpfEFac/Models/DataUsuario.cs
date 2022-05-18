using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class DataUsuario
    {

        private eFacDBEntities db;
        public DataUsuario()
        {
            db = new eFacDBEntities();
        }

        public Usuarios getUsuario(int ID)
        {
            try
            {
                return db.Usuarios.First(u => u.intID == ID);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Usuarios getNombre(string Nombre)
        {
            try
            {
                return db.Usuarios.First(n => n.strNombre == Nombre);
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public Usuarios getIDGrupo(int idGrupo)
        {
            try
            {
                return db.Usuarios.First(g => g.intID_Grupo == idGrupo);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Usuarios getIDEmpresa(int idEmpresa)
        {
            try
            {
                return db.Usuarios.First(e => e.intID_Empresa == idEmpresa);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Usuarios getEmail(string Email)
        {
            try
            {
                return db.Usuarios.First(e => e.strEmail == Email);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteUsuario(int ID)
        {
            try
            {
                Usuarios EliminarUsuario = db.Usuarios.First(eu => eu.intID == ID);

                db.Usuarios.DeleteObject(EliminarUsuario);

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
