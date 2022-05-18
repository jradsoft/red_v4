using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class DataVehiculos
    {

         private eFacDBEntities db;
         public DataVehiculos()
        {
            db = new eFacDBEntities();
        }

        public Vehiculos getVehiculos(string placa)
        {
            try
            {
                return db.Vehiculos.First(i => i.strPlaca == placa);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
