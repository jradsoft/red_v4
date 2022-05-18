using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    public class ProductoData
    {
        private eFacDBEntities db;

        public ProductoData()
        {
            db = new eFacDBEntities();
        }

        public ObservableCollection<Categorias> GetCategorias()
        {
            return new ObservableCollection<Categorias>(db.Categorias);
        }

        public ObservableCollection<Productos> GetProductos()
        {
            return new ObservableCollection<Productos>(db.Productos);
        }
    }
}
