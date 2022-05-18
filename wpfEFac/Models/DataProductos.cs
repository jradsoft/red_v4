using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    public class DataProductos
    {
        private eFacDBEntities db;

        public DataProductos()
        {
            db = new eFacDBEntities();
        }

        public ObservableCollection<Productos> GetProductos() 
        {
            return new ObservableCollection<Productos>(db.Productos);
        }

        public ObservableCollection<Productos> BuscarProductoCodigo(string codigo) 
        {
            var query = db.Productos.Where(p => p.strCodigo.ToLower().Contains(codigo.ToLower()));

            return new ObservableCollection<Productos>(query);
        }

        public ObservableCollection<Productos> BuscarProductoNombre(string nombre) 
        {
            var query = db.Productos.Where(p => p.strNombre.ToLower().Contains(nombre.ToLower()));

            return new ObservableCollection<Productos>(query);
        }

        //******************************** EDITAR PRODUCTO ***********************************************

        public Productos getProducto(int id)
        {
            try
            {
                return db.Productos.First(pro => pro.intID == id);
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public Productos getNombre(string Nombre)
        {
            try
            {
                return db.Productos.First(p => p.strNombre == Nombre);
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public Productos getCodigo(string Codigo)
        {
            try
            {
                return db.Productos.First(c => c.strCodigo == Codigo);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Productos getPrecio(decimal Precio)
        {
            try
            {
                return db.Productos.First(p => p.dcmPrecio1 == Precio);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Productos getActos(decimal Actos)
        {
            try
            {
                return db.Productos.First(a => a.porcIva == Actos);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Productos getDescuento(decimal Descuento)
        {
            try
            {
                return db.Productos.First(d => d.dcmDescuent == Descuento);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ObservableCollection<Categorias> getCategorias()
        {
            return new ObservableCollection<Categorias>(db.Categorias);
        }

        public Productos getCategoria(int Categoria)
        {
            try
            {
                return db.Productos.First(c => c.intID_Categoria == Categoria);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Productos getUnidad(int Unidad)
        {
            try
            {
                return db.Productos.First(u => u.intUnidad == Unidad);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Productos getDescripcion(string Descripcion)
        {
            try
            {
                return db.Productos.First(d => d.strDescripcion == Descripcion);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool EliminarProducto(int ID) 
        {
            try
            {
                Productos productoEliminar = db.Productos.First(p => p.intID == ID);
                db.Productos.DeleteObject(productoEliminar);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminarVehiculo(string strPlaca)
        {
            try
            {
                Vehiculos vehiculoEliminar = db.Vehiculos.First(p => p.strPlaca == strPlaca);
                db.Vehiculos.DeleteObject(vehiculoEliminar);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
