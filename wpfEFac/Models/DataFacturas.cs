using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    /// <summary>
    /// Class that retrives data from the data base.
    /// </summary>
    public class DataFacturas
    {
        /// <summary>
        /// The model of the data base.
        /// </summary>
        private static eFacDBEntities db = new eFacDBEntities();

        /// <summary>
        /// Gets the facturas.
        /// </summary>
        /// <param name="start">Zero-based index that determines the start of the products to be returned.</param>
        /// <param name="itemCount">Number of facturas that is requested to be returned.</param>
        /// <param name="sortColumn">Name of column or member that is the basis for sorting.</param>
        /// <param name="ascending">Indicates the sort direction to be used.</param>
        /// <param name="totalItems">Total number of products.</param>
        /// <returns>List of products.</returns>
        public static ObservableCollection<Factura> GetFacturas(int start, int itemCount, string sortColumn, bool ascending, out int totalItems)
        {
            db = new eFacDBEntities();

            totalItems = db.Factura.Count();

            ObservableCollection<Factura> sortedFacturas = new ObservableCollection<Factura>();

            // Sort the products. In reality, the items should be stored in a database and
            // use SQL statements for sorting and querying items.
            switch (sortColumn)
            {
                case ("strID"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.intID
                        select f
                    );
                    break;
                case ("Clientes.strNombreComercial"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.Clientes.strNombreComercial
                        select f
                    );
                    break;
                case ("strTipoDocumento"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.CFD.strTipoCFD
                        select f
                    );
                    break;
                case ("intID_Empresa"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.intID_Empresa
                        select f
                    );
                    break;
                case ("strSerie"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.strSerie
                        select f
                    );
                    break;
                case ("strFolio"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.strFolio
                        select f
                    );
                    break;
                case ("dtmFecha"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.dtmFecha
                        select f
                    );
                    break;
                case ("dtmFechaEnvio"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.dtmFechaEnvio
                        select f
                    );
                    break;
                case ("strNumero"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.strNumero
                        select f
                    );
                    break;

                case ("dtmFechaAprovacion"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.dtmFechaAprovacion
                        select f
                    );
                    break;
                case ("intID_Usuario"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.intID_Usuario
                        select f
                    );
                    break;
                case ("intID_Cliente"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.intID_Cliente
                        select f
                    );
                    break;
                case ("strForma_Pago"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.strForma_Pago
                        select f
                    );
                    break;
                case ("strObervaciones"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.strObervaciones
                        select f
                    );
                    break;
                case ("dcmSubTotal"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.dcmSubTotal
                        select f
                    );
                    break;
                case ("dcmIVA"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.dcmIVA
                        select f
                    );
                    break;
                case ("dcmTotal"):
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.dcmTotal
                        select f
                    );
                    break;
                default:
                    sortedFacturas = new ObservableCollection<Factura>
                    (
                        from f in db.Factura
                        orderby f.dtmFecha
                        select f
                    );
                    break;
            }

            sortedFacturas = ascending ? sortedFacturas : new ObservableCollection<Factura>(sortedFacturas.Reverse());

            ObservableCollection<Factura> filteredFacturas = new ObservableCollection<Factura>();

            for (int i = start; i < start + itemCount && i < totalItems; i++)
            {
                filteredFacturas.Add(sortedFacturas[i]);
            }

            return filteredFacturas;
        }

    }
}
