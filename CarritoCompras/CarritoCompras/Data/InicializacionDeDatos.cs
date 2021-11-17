using CarritoCompras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.Data
{
	public static class InicializacionDeDatos
	{
		public static void Inicializar(CarritoComprasContext context)
		{
			context.Database.EnsureCreated();

			using (var transaccion = context.Database.BeginTransaction())
			{
				try
				{
					if (context.Empleado.Any())
					{
						return;
					}

					var nuevoEmpleado = new Empleado();
					nuevoEmpleado.Id = Guid.NewGuid();					
					nuevoEmpleado.UserName = "admin";
					nuevoEmpleado.Nombre = "Pedro";
					nuevoEmpleado.Apellido = "Picapiedra";					
					nuevoEmpleado.Password = "admin";
					nuevoEmpleado.Telefono = "12345678";
					nuevoEmpleado.Direccion = "balbal 1212";
					nuevoEmpleado.FechaAlta = DateTime.Now;
					context.Empleado.Add(nuevoEmpleado);

					var nuevoCliente = new Cliente();
					nuevoCliente.Id = Guid.NewGuid();
					nuevoCliente.UserName = "susan";
					nuevoCliente.Nombre = "Susana";
					nuevoCliente.Apellido = "Gimenez";
					nuevoCliente.Password = "susan";
					nuevoCliente.DNI = "20555666";
					nuevoCliente.Telefono = "12345678";
					nuevoCliente.Direccion = "balbal 1212";
					nuevoCliente.FechaAlta = DateTime.Now;
					context.Cliente.Add(nuevoCliente);

					var nuevoCliente1 = new Cliente();
					nuevoCliente1.Id = Guid.NewGuid();
					nuevoCliente1.UserName = "pepe";
					nuevoCliente1.Nombre = "Pepe";
					nuevoCliente1.Apellido = "Cantinflas";
					nuevoCliente1.Password = "1234";
					nuevoCliente1.DNI = "10632226";
					nuevoCliente1.Telefono = "12345678";
					nuevoCliente1.Direccion = "balbal 1212";
					nuevoCliente1.FechaAlta = DateTime.Now;
					context.Cliente.Add(nuevoCliente1);
					context.SaveChanges();
					
					if (context.Categoria.Any())
					{
						return;
					}
					
					var nuevaCategoria = new Categoria();
					nuevaCategoria.Nombre = "Desodorantes";
					nuevaCategoria.CategoriaId = Guid.NewGuid();
					context.Categoria.Add(nuevaCategoria);

					var nuevaCategoria1 = new Categoria();
					nuevaCategoria1.Nombre = "Televisores";
					nuevaCategoria1.CategoriaId = Guid.NewGuid();
					context.Categoria.Add(nuevaCategoria1);

					context.SaveChangesAsync();
				
					if (context.Producto.Any())
					{
						// Si ya hay datos aqui, significa que ya los hemos creado previamente
						return;
					}

					var categoria = context.Categoria.FirstOrDefault(n => n.Nombre == "Desodorantes");
					var categoria1 = context.Categoria.FirstOrDefault(n => n.Nombre == "Televisores");

					var nuevoProducto = new Producto();
					nuevoProducto.ProductoId = Guid.NewGuid();
					nuevoProducto.Nombre = "Desodorante Rexona";
					nuevoProducto.Descripcion = "Rico Desororante";
					nuevoProducto.Activo = true;
					nuevoProducto.PrecioVigente = 254;
					nuevoProducto.CategoriaId = categoria.CategoriaId;

					var nuevoProducto1 = new Producto
					{
						ProductoId = Guid.NewGuid(),
						Nombre = "Desodorante Axe",
						Activo = true,
						Descripcion = "Axe lomejor",
						PrecioVigente = 123,
						CategoriaId = categoria.CategoriaId
					};

					var nuevoProducto2 = new Producto();
					nuevoProducto2.ProductoId = Guid.NewGuid();
					nuevoProducto2.Nombre = "Televisor Phillips";
					nuevoProducto2.Descripcion = "Televisión de 12'";
					nuevoProducto2.Activo = true;
					nuevoProducto2.PrecioVigente = 10548;
					nuevoProducto2.CategoriaId = categoria1.CategoriaId;

					context.Producto.Add(nuevoProducto);
					context.Producto.Add(nuevoProducto1);
					context.Producto.Add(nuevoProducto2);					

					context.SaveChanges();
					
					var carrito = new Carrito();
					carrito.ClienteId = nuevoCliente.Id;
					carrito.CarritoId = Guid.NewGuid();					
					carrito.Activo = true;
					carrito.Subtotal = 0.00;
					context.Carrito.Add(carrito);

					context.SaveChanges();
				
					
					var carrito1 = new Carrito();
					carrito1.ClienteId = nuevoCliente1.Id;
					carrito1.CarritoId = Guid.NewGuid();					
					carrito1.Activo = true;
					carrito1.Subtotal = 0.00;
					context.Carrito.Add(carrito1);

					context.SaveChanges();

					
					var carritoItem1 = new CarritoItem();
					carritoItem1.CarritoItemId = Guid.NewGuid();
					carritoItem1.CarritoId = carrito.CarritoId;
					carritoItem1.ProductoId = nuevoProducto.ProductoId;
					carritoItem1.Cantidad = 2;
					context.CarritoItem.Add(carritoItem1);

					context.SaveChanges();
					
					
					var carritoItem2 = new CarritoItem();
					carritoItem2.CarritoItemId = Guid.NewGuid();
					carritoItem2.CarritoId = carrito.CarritoId;
					carritoItem2.ProductoId = nuevoProducto1.ProductoId;
					carritoItem2.Cantidad = 10;
					context.CarritoItem.Add(carritoItem2);
					context.SaveChanges();

					
					var carritoItem3 = new CarritoItem();
					carritoItem3.CarritoItemId = Guid.NewGuid();
					carritoItem3.CarritoId = carrito.CarritoId;
					carritoItem3.ProductoId = nuevoProducto2.ProductoId;
					carritoItem3.Cantidad = 200;
					context.CarritoItem.Add(carritoItem3);
					context.SaveChanges();

					var suc1 = new Sucursal();
					suc1.SucursalId = Guid.NewGuid();
					suc1.Nombre = "Fravega";
					suc1.Direccion = "Corrientes Av. 3444";
					suc1.Telefono = "12345678";
					suc1.Email = "Fraveg@Gmail.com";
					context.Sucursal.Add(suc1);
					context.SaveChanges();
					var suc2 = new Sucursal();
					suc2.SucursalId = Guid.NewGuid();
					suc2.Nombre = "Musimundo";
					suc2.Direccion = "Santa Fe Av. 8882";
					suc2.Telefono = "98765432";
					suc2.Email = "Musimundo@Gmail.com";
					context.Sucursal.Add(suc2);
					context.SaveChanges();

					var stkit1 = new StockItem();
					stkit1.StockItemId = Guid.NewGuid();
					stkit1.SucursalId = suc1.SucursalId;
					stkit1.ProductoId = nuevoProducto.ProductoId;
					stkit1.Cantidad = 100;
					context.StockItem.Add(stkit1);
					var stkit1a = new StockItem();
					stkit1a.StockItemId = Guid.NewGuid();
					stkit1a.SucursalId = suc1.SucursalId;
					stkit1a.ProductoId = nuevoProducto1.ProductoId;
					stkit1a.Cantidad = 100;
					context.StockItem.Add(stkit1a);
					var stkit1b = new StockItem();
					stkit1b.StockItemId = Guid.NewGuid();
					stkit1b.SucursalId = suc1.SucursalId;
					stkit1b.ProductoId = nuevoProducto2.ProductoId;
					stkit1b.Cantidad = 100;
					context.StockItem.Add(stkit1b);
					context.SaveChanges();
					suc1.StockItems.Add(stkit1);
					suc1.StockItems.Add(stkit1a);
					suc1.StockItems.Add(stkit1b);
					context.Sucursal.Update(suc1);
					context.SaveChanges();

					transaccion.Commit();
				}
				catch(Exception Ex)
				{					
					transaccion.Rollback();
				}

			}

		}
	}
}

