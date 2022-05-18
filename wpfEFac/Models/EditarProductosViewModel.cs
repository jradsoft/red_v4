using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace wpfEFac.Models
{
    public class EditarProductosViewModel
    {

        private eFacDBEntities db;
        private ProductoData prodb;
        private DataProductos data;

        public EditarProductosViewModel()
        {
            db = new eFacDBEntities();
            prodb = new ProductoData();
            data = new DataProductos();
        }

        public ObservableCollection<Categorias> GetCategoria()
        {
            return prodb.GetCategorias();
        }

        public ObservableCollection<Productos> GetProductos()
        {
            return prodb.GetProductos();
        }

        //************************** EDITAR PRODUCTOS **************************************************

        public Productos GetProductos(int id)
        {
            Productos producto = data.getProducto(id);

            if (producto != null)
            {
                return producto;
            }

            return null;
        }

        public Productos GetNombre(string Nombre)
        {
            Productos producto = data.getNombre(Nombre);

            if (producto != null)
            {
                return producto;
            }

            return null;
        }

        public Productos GetCodigo(string Codigo)
        {
            Productos producto = data.getCodigo(Codigo);

            if (producto != null)
            {
                return producto;
            }

            return null;
        }

        public Productos GetPrecio(decimal Precio)
        {
            Productos producto = data.getPrecio(Precio);

            if (producto != null)
            {
                return producto;
            }

            return producto;
        }

        public Productos GetActos(decimal Actos)
        {
            Productos productos = data.getActos(Actos);

            if (productos != null)
            {
                return productos;
            }

            return null;
        }

        public Productos GetDescuento(decimal Descuento)
        {
            Productos productos = data.getDescuento(Descuento);

            if (productos != null)
            {
                return productos;
            }

            return productos;
        }

        public ObservableCollection<Categorias> GetCategorias()
        {
            return data.getCategorias();
        }

        public Productos GetCategoria(int Categoria)
        {
            Productos productos = data.getCategoria(Categoria);

            if (productos != null)
            {
                return productos;
            }

            return null;
        }

        public Productos GetUnidad(int Unidad)
        {
            Productos productos = data.getUnidad(Unidad);

            if (productos != null)
            {
                return productos;
            }

            return null;
        }

        public Productos GetDescripcion(string Descripcion)
        {
            Productos productos = data.getDescripcion(Descripcion);

            if (productos != null)
            {
                return productos;
            }

            return null;
        }

        public List<UnidadMedida> GetUnidad()
        {
            return db.UnidadMedida.ToList();
        }
    }
}

