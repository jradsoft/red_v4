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

namespace wpfEFac.Views.Paises
{
    /// <summary>
    /// Interaction logic for EditarPais.xaml
    /// </summary>
    public partial class EditarPais : Window
    {

        private eFacDBEntities entidad;
        private EditarPaisViewModels epvm;
        private int id;
        public EditarPais(int id)
        {
            InitializeComponent();
            this.id = id;
            entidad = new eFacDBEntities();
            epvm = new EditarPaisViewModels();
            this.Loaded += EditarPais_Loaded;
        }

        protected void EditarPais_Loaded(object sender, RoutedEventArgs e)
        {
            var pais = epvm.GetPais(id);
            var Nombre = epvm.GetNombre(pais.strNombrePais);

            //************************************************************

            txtNombre.Text = Nombre.strNombrePais;
        }

        private void bttGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bttCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
