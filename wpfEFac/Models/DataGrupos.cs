using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class DataGrupos
    {
        private eFacDBEntities db;
        public DataGrupos()
        {
            db = new eFacDBEntities();
        }

        public Grupos getGrupos(int id)
        {
            try
            {
                return db.Grupos.First(i => i.intID == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Grupos getNombre(string Nombre)
        {
            try
            {
                return db.Grupos.First(n => n.strDescripcion == Nombre);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Grupos getFechaCreacion(string Creacion)
        {
            try
            {
                return db.Grupos.First(c => c.dtmFechaCreacion == DateTime.Parse(Creacion));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteGrupo(int ID)
        {
            try
            {
                Grupos deletegrupo = db.Grupos.First(dg => dg.intID == ID);

                db.Grupos.DeleteObject(deletegrupo);

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
