using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    public class Login
    {
        private eFacDBEntities db;

        public Login()
        {
            db = new eFacDBEntities();
        }

        public int DoLogin(string nombre, string password, int idEmpresa) 
        {
            Usuarios usuarioActual;

            try
            {
                usuarioActual = GetUsuario(nombre, password, idEmpresa);
            }
            catch (Exception)
            {
                return -1;
            }
            
            return usuarioActual.intID;
        }

        public Usuarios GetUsuario(string nombre, string password, int idEmpresa)
        {
            Usuarios usuarioActual;
            usuarioActual = db.Usuarios.First(usr => usr.strNombre == nombre && usr.strPassword == password && usr.Empresa.intID == idEmpresa);

            GetEmpresa(idEmpresa);
            return usuarioActual;

            
        }

        public Usuarios GetUsuario(int id)
        {
            Usuarios usuarioActual;
            usuarioActual = db.Usuarios.First(usr => usr.intID == id);
            return usuarioActual;
        }

        public Empresa GetEmpresa(int idUserLogged) 
        {
            
            Usuarios usuarioActual;
            Empresa empresa;
            

            try
            {
                usuarioActual= GetUsuario(idUserLogged);
                empresa = db.Empresa.First(emp => emp.intID == usuarioActual.intID_Empresa);
                UserPac.rfc = empresa.strRFC;
            }
            catch (Exception)
            {
                empresa = null;
            }

            return empresa;
        }

        public List<Empresa> GetEmpresas() 
        {
            return db.Empresa.ToList();
            
        }

    }
}
