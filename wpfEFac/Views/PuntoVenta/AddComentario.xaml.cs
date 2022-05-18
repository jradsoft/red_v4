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

namespace wpfEFac.Views.PuntoVenta
{
    /// <summary>
    /// Lógica de interacción para AddComentario.xaml
    /// </summary>
    public partial class AddComentario : Window
    {
        Factura myF;


        public AddComentario(Factura f)
        {

            myF = f;
            InitializeComponent();


            txtComentario.Text = f.strNumero;
            
        }

     

        private void cmdAddComentario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                eFacDBEntities db = new eFacDBEntities();

                Factura factura = db.Factura.SingleOrDefault(fac => fac.intID == myF.intID);// edita comentario

                factura.strNumero = txtComentario.Text;
                db.SaveChanges();

                DialogResult = true;
            }
            catch (Exception ex) {

                MessageBox.Show("Lo sentimos no se pudo guardar el comentario..." + ex);
            
            }


        }

        private void cmdCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
