using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wpfEFac.Models;

namespace wpfEFac.Views.Usuarios
{
    /// <summary>
    /// Interaction logic for EditarUsuario.xaml
    /// </summary>
    public partial class EditarUsuario : Window
    {

        private eFacDBEntities entidad;
        private EditarUsuarioViewModel euvm;
        private int id;
        public EditarUsuario(int id)
        {
            InitializeComponent();
            this.id = id;
            entidad = new eFacDBEntities();
            euvm = new EditarUsuarioViewModel();
            this.Loaded += EditarUsuario_Loaded;
        }

        protected void EditarUsuario_Loaded(object sender, RoutedEventArgs e)
        {
            var usuario = euvm.GetUsuario(id);
            var Nombre = euvm.GetNombre(usuario.strNombre);
            var IDGrupo = euvm.GetGrupo(usuario.intID_Grupo);
            var IDEmpresa = euvm.GetIDEmpresa(usuario.intID_Empresa);
            var Email = euvm.GetEmail(usuario.strEmail);

            //****************************************************************************

            txtNombre.Text = Nombre.strNombre;
            PwbPassword.Password = usuario.strPassword;
            txtIDGrupo.Text = IDGrupo.intID_Grupo.ToString();            
            txtEmail.Text = Email.strEmail;
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            int ID = id;
            string Nombre = txtNombre.Text;
            int IDGrupo = int.Parse(txtIDGrupo.Text);        
            string Email = txtEmail.Text;
            string password = PwbPassword.Password;

            BusClientes bus = new BusClientes();
            if (bus.EditarUsuario(ID, Nombre, IDGrupo, Convert.ToInt32(App.Current.Properties["idEmpresa"]), Email, password))
            {
                MessageBox.Show("El Usuario se ha Editado Correctamente", "Editado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ocurrio un Error durante la Edicion, por favor vuelva a intentarlo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            DialogResult = true;
        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
