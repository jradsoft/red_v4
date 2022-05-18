using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class EditarVehiculoViewModels
    {
        private eFacDBEntities entidad;
        private DataVehiculos data;

        public EditarVehiculoViewModels()
        {
            entidad = new eFacDBEntities();
            data = new DataVehiculos();
        }

        public Vehiculos GetVehiculo(string placa)
        {
            Vehiculos vehiculo = data.getVehiculos(placa);

            if (vehiculo != null)
            {
                return vehiculo;
            }

            return null;
        }
    }
}
