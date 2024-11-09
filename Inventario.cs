using GesInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GesInventario
{
    public class Inventario
    {
        private List<Producto> productos;

        public Inventario()
        {
            productos = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            productos.Add(producto);
        }

        public IEnumerable<Producto> FiltrarYOrdenarProductos(decimal precioMinimo)
        {
            //Filtrar y ordenar productos con LinQ y expresiones lambda
            return productos
                .Where(p => p.Precio > precioMinimo) //Filtra productos con precio mayor al minimo especificado
                .OrderBy(p => p.Precio); //Ordena los productos de menor a mayor precio 
        }

        //Metodo que permite actualizar el precio de un producto por nombre

        public bool ActualizarPrecio(string nombre, decimal nuevoPrecio)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                producto.Precio = nuevoPrecio;
                return true;

            }
            return false;
        }

        //Metodo que permite eliminar un producto por nombre 

        public bool EliminarProducto(string nombre)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                productos.Remove(producto);
                return true;
            }
            return false;
        }

        //Metodo que permite contabilizar el total de productos
        public int ContarProductos()
        {
            return productos.Count;
        }

        //Metodo que permite calcular el precio promedio de los productos
        public decimal CalcularPrecioPromedio()
        {
            return productos.Any() ? productos.Average(p => p.Precio) : 0;
        }

        //Metodo que permite obtener el producto con el precio mas alto
        public Producto ObtenerProductoMasCaro()
        {
            return productos.OrderByDescending(p => p.Precio).FirstOrDefault();
        }

        //Metodo que permite obtener el producto con el precio mas bajo

        public Producto ObtenerProductoMasBarato()
        {
            return productos.OrderBy(p => p.Precio).FirstOrDefault();
        }

        //Metodo para agrupar productos en rangos de precio usando LinQ

        public IEnumerable<IGrouping<string, Producto>> AgruparProductosPorRangoDePrecio()
        {
            return productos.GroupBy(p =>
            {
                if (p.Precio < 100) return "Menores a 100";
                if (p.Precio <= 500) return "Entre 100 y 500";
                return "Mayores a 500";

            });


        }

    }
}

