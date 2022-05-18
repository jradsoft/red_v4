using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class EditarPaisViewModels
    {

        private eFacDBEntities entidad;
        private DataPaises data;
        public EditarPaisViewModels()
        {
            entidad = new eFacDBEntities();
            data = new DataPaises();
        }

        public Paises GetPais(int id)
        {
            Paises pais = data.getPais(id);

            if (pais != null)
            {
                return pais;
            }

            return null;
        }

        public Paises GetNombre(string Nombre)
        {
            Paises pais = data.getNombre(Nombre);

            if (pais != null)
            {
                return pais;
            }

            return null;
        }
    }
}
