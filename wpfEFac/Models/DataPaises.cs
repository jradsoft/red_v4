using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class DataPaises
    {
        private eFacDBEntities db;
        public DataPaises()
        {
            db = new eFacDBEntities();
        }

        public Paises getPais(int id)
        {
            try
            {
                return db.Paises.First(i => i.intID == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Paises getNombre(string Nombre)
        {
            try
            {
                return db.Paises.First(n => n.strNombrePais == Nombre);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
