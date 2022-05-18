using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class EditarGrupoViewModels
    {
        private DataGrupos data;
        private eFacDBEntities entidad;
        public EditarGrupoViewModels()
        {
            data = new DataGrupos();
            entidad = new eFacDBEntities();
        }

        public Grupos GetGrupo(int id)
        {
            Grupos grupo = data.getGrupos(id);

            if (grupo != null)
            {
                return grupo;
            }

            return null;
        }

        public Grupos GetNombre(string Nombre)
        {
            Grupos grupo = data.getNombre(Nombre);

            if (grupo != null)
            {
                return grupo;
            }

            return null;
        }

        public Grupos GetFechaCreacion(string Creacion)
        {
            Grupos grupo = data.getFechaCreacion(Creacion);

            if (grupo != null)
            {
                return grupo;
            }

            return null;
        }

    }
}
