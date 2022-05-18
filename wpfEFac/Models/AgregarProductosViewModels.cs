using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    public class AgregarProductosViewModels
    {
        private DataProductos db;

        public AgregarProductosViewModels()
        {
            db = new DataProductos();
        }

        public ObservableCollection<Productos> GetProductos()
        {
            return db.GetProductos();
        }

        public ObservableCollection<Productos> BuscarPorCodigo(string codigo) 
        {
            return db.BuscarProductoCodigo(codigo);
        }

        public ObservableCollection<Productos> BuscarPorNombre(string nombre) 
        {
            return db.BuscarProductoNombre(nombre);
        }
    }
}
