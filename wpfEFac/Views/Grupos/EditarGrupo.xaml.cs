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

namespace wpfEFac.Views.Grupos
{
    /// <summary>
    /// Interaction logic for EditarGrupo.xaml
    /// </summary>
    public partial class EditarGrupo : Window
    {

        private eFacDBEntities entidad;
        private EditarGrupoViewModels egvm;
        private int id;
        public EditarGrupo(int id)
        {
            InitializeComponent();
            this.id = id;
            entidad = new eFacDBEntities();
            egvm = new EditarGrupoViewModels();
            this.Loaded += EditarGrupo_Loaded;
        }

        protected void EditarGrupo_Loaded(object sender, RoutedEventArgs e)
        {
            var grupo = egvm.GetGrupo(id);
            var Nombre = egvm.GetNombre(grupo.strDescripcion);
            
            //var FechaCreacion = egvm.GetFechaCreacion(grupo.dtmFechaCreacion.ToString());
            //var FechaModificacion = egvm.GetFechaModificacion(grupo.dtmFechaModificacion.ToString());

            //**************************************************************************************

            txtNombre.Text = Nombre.strDescripcion;
            
            txtFecha_Creacion.Text = grupo.dtmFechaCreacion.ToString();
         
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {
            int ID = id;
            string Nombre = txtNombre.Text;
            
            DateTime Creacion = DateTime.Parse(txtFecha_Creacion.Text);
            
            BusClientes bus = new BusClientes();
            if (bus.EditarGrupo(ID, Nombre, Creacion))
            {
                MessageBox.Show("El Grupo ha sido Editado Correctamente", "Editado", MessageBoxButton.OK, MessageBoxImage.Information);
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
