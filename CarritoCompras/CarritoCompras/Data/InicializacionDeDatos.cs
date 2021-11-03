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
					nuevoEmpleado.Password = "1234";
					nuevoEmpleado.Telefono = "12345678";
					nuevoEmpleado.Direccion = "balbal 1212";
					nuevoEmpleado.FechaAlta = DateTime.Now;
					context.Empleado.Add(nuevoEmpleado);

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
					
					transaccion.Commit();
				}
				catch
				{					
					transaccion.Rollback();
				}

			}

		}
	}
}

