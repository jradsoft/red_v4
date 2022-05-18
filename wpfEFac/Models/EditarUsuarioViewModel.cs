using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    class EditarUsuarioViewModel
    {

        private eFacDBEntities entidad;
        private DataUsuario data;
        public EditarUsuarioViewModel()
        {
            entidad = new eFacDBEntities();
            data = new DataUsuario();
        }

        public Usuarios GetUsuario(int ID)
        {
            Usuarios usuario = data.getUsuario(ID);

            if (usuario != null)
            {
                return usuario;
            }

            return null;
        }

        public Usuarios GetNombre(string Nombre)
        {
            Usuarios usuario = data.getNombre(Nombre);

            if (usuario != null)
            {
                return usuario;
            }
            
            return null;
        }

        public Usuarios GetGrupo(int IDGrupo)
        {
            Usuarios usuario = data.getIDGrupo(IDGrupo);

            if (usuario != null)
            {
                return usuario;
            }

            return null;
        }

        public Usuarios GetIDEmpresa(int IDEmpresa)
        {
            Usuarios usuario = data.getIDEmpresa(IDEmpresa);

            if (usuario != null)
            {
                return usuario;
            }

            return null;
        }

        public Usuarios GetEmail(string Email)
        {
            Usuarios usuario = data.getEmail(Email);

            if (usuario != null)
            {
                return usuario;
            }

            return null;
        }
    }
}
