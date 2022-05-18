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
using Newtonsoft.Json;

namespace wpfEFac.Views.PuntoVenta
{
    /// <summary>
    /// Lógica de interacción para AddDataSPC.xaml
    /// </summary>
    public partial class AddDataSPC : Window
    {
        private string jsonString = "";

        public AddDataSPC()
        {
            InitializeComponent();
            eFacDBEntities mydb = new eFacDBEntities();
            wpfEFac.Models.Estado c = new Models.Estado();
            cmbEstado.ItemsSource = mydb.Estado.OrderBy(x => x.strNombreEstado);
            cmbEstado.DisplayMemberPath = "strNombreEstado";
            cmbEstado.SelectedValuePath = "intID";
            cmbEstado.SelectedValue = c.intID;

        }

        private void okButton_lick(object sender, RoutedEventArgs e)
        {
            try
            {
                dataServParCons.FillData myData = new dataServParCons.FillData();

                var idEstado = cmbEstado.SelectedValue;

                myData.NumPermisoSPC = txtNumPermiso.Text;
                myData.Calle = txtCalle.Text;
                myData.NoInterior = txtNumInt.Text;
                myData.NoExterior = txtNumExt.Text;
                myData.Colonia = txtColonia.Text;
                myData.Localidad = txtColonia.Text;
                myData.Referencia = txtReferencia.Text;
                myData.Municipio = txtMunicipio.Text;
                myData.Estado = idEstado.ToString().PadLeft(2, '0');
                myData.CodigoPostal = txtCP.Text;

                DialogResult = true;

                jsonString = JsonConvert.SerializeObject(myData);

                Console.WriteLine("jsonSPC " + jsonString);
            }
            catch (Exception ex) { }
           
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

        }


        public string TheValue
        {
            get { return jsonString; }
        }
    }
}
