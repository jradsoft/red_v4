using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace wpfEFac.Models
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Fields

        private Usuarios usuario;

        private int idUsuario;

        private string nombreUsuario;

        private string password;

        #endregion

        public LoginViewModel()
        {
            //RefeshLogin();
        }

        private void RefeshLogin()
        {
            Usuarios usr = new Login().GetUsuario(nombreUsuario, password, idEmpresa);
            NotifyPropertyChanged("Usuario");
            idUsuario = usr.intID;
            NotifyPropertyChanged("IdUsuario");
        }

        public Usuarios Usuario 
        { 
            get 
            {
                return usuario;
            }
            set 
            {
                if (object.ReferenceEquals(usuario, value) != true)
                {
                    usuario = value;
                    NotifyPropertyChanged("Usuario");
                }
            }
        }

        public int DoLogin(string nombreUsuario, string password, int idEmpresa) 
        {
            this.nombreUsuario = nombreUsuario;
            NotifyPropertyChanged("NombreUsuario");
            this.password = password;
            NotifyPropertyChanged("Password");
            this.idEmpresa = idEmpresa;
            NotifyPropertyChanged("idEmpresa");
            RefeshLogin();
            return idUsuario;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public const string NombreUsuarioErrorMessage = "Nombre es un valor requerido.";

        public int IdUsuario { get { return idUsuario; } }
        
        public string NombreUsuario 
        { 
            get 
            { 
                return nombreUsuario; 
            }
            set 
            {
                nombreUsuario = value;
                if (String.IsNullOrEmpty(value))
                {
                    throw new ApplicationException(NombreUsuarioErrorMessage);
                }
            }
        }

        public const string PasswordErrorMessage = "Password es un valor requerido.";
        
        private int idEmpresa;

        public string Password 
        { 
            get 
            {
                return password;
            }
            set 
            {
                password = value;
                if (String.IsNullOrEmpty(value))
                {
                    throw new ApplicationException(PasswordErrorMessage);
                }
            }
        }

        public List<Empresa> GetEmpresas()
        {
            return new Login().GetEmpresas();
        }
    }
}
