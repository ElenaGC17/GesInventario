using GesInventario;
using System;

namespace GesInventario
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventario inventario = new Inventario();
            Console.WriteLine("Bienvenid@s al sistema de gestión de inventario.");


            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\nPor favor, seleccione una opción:");
                Console.WriteLine("1. Agregar productos");
                Console.WriteLine("2. Filtrar y mostrar productos por precio mínimo");
                Console.WriteLine("3. Actualizar precio de un producto");
                Console.WriteLine("4. Eliminar un producto");
                Console.WriteLine("5. Generar reporte");
                Console.WriteLine("6. Salir");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarProductos(inventario);
                        break;
                    case "2":
                        FiltrarYMostrarProductos(inventario);
                        break;
                    case "3":
                        ActualizarPrecioProducto(inventario);
                        break;
                    case "4":
                        EliminarProducto(inventario);
                        break;
                    case "5":
                        GenerarReporteResumido(inventario);
                        break;
                    case "6":
                        continuar = false;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intente nuevamente.");
                        break;

                }
            }

            static void AgregarProductos(Inventario inventario)
            {
                Console.WriteLine("¿Cuántos productos desea ingresar?");
                if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
                {
                    for (int i = 0; i < cantidad; i++)
                    {
                        Console.WriteLine($"\nProducto {i + 1}:");
                        Console.Write("Nombre: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Precio: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal precio) && precio > 0)
                        {
                            Producto producto = new Producto(nombre, precio);
                            inventario.AgregarProducto(producto);
                            Console.WriteLine("Producto agregado exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("Precio inválido. Intente nuevamente.");
                            i--;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Cantidad inválida.");
                }
            }

            static void FiltrarYMostrarProductos(Inventario inventario)
            {
                Console.WriteLine("\nIngrese el precio mínimo para filtrar los productos: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal precioMinimo) && precioMinimo >= 0)
                {
                    var productosFiltrados = inventario.FiltrarYOrdenarProductos(precioMinimo);
                    Console.WriteLine("\nProductos filtrados y ordenados: ");
                    foreach (var producto in productosFiltrados)
                    {
                        Console.WriteLine(producto);
                    }
                }
                else
                {
                    Console.WriteLine("Precio mínimo inválido.");
                }
            }

            static void ActualizarPrecioProducto(Inventario inventario)
            {
                Console.Write("\nIngrese el nombre del producto a actualizar: ");
                string nombre = Console.ReadLine();
                Console.Write("Nuevo precio: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal nuevoPrecio) && nuevoPrecio > 0)
                {
                    if (inventario.ActualizarPrecio(nombre, nuevoPrecio))
                    {
                        Console.WriteLine("Precio actualizado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Producto no encontrado.");
                    }
                }
                else
                {
                    Console.WriteLine("Precio inválido.");
                }
            }

            static void EliminarProducto(Inventario inventario)
            {
                Console.Write("\nIngrese el nombre del producto a eliminar: ");
                string nombre = Console.ReadLine();
                if (inventario.EliminarProducto(nombre))
                {
                    Console.WriteLine("Producto eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }

            static void GenerarReporteResumido(Inventario inventario)
            {
                Console.WriteLine("\n--- Reporte Resumido del Inventario ---");
                Console.WriteLine($"Total de productos: {inventario.ContarProductos()}");
                Console.WriteLine($"Precio promedio: {inventario.CalcularPrecioPromedio():C}");
                Console.WriteLine($"Producto más caro: {inventario.ObtenerProductoMasCaro()?.Nombre}");
                Console.WriteLine($"Producto más barato: {inventario.ObtenerProductoMasBarato()?.Nombre}");

                var agrupaciones = inventario.AgruparProductosPorRangoDePrecio();
                Console.WriteLine("\nProductos agrupados por rango de precio:");
                foreach (var grupo in agrupaciones)
                {
                    Console.WriteLine($"{grupo.Key}: {grupo.Count()} productos");
                }
            }

        }
    }
}
