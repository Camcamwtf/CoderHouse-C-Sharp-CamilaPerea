using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_SistemaGestionVentas.Models;
using ProyectoFinal_SistemaGestionVentas.Repositories;

namespace ProyectoFinal_SistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("API/[Controller]")]
    public class ProductoController : Controller
    {
        private ProductoRepository repository = new ProductoRepository();

        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<Producto> lista = repository.ListarProductos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<Producto> Get(int Id)
        {
            try
            {
                Producto? producto = repository.ObtenerProducto(Id);
                if (producto != null)
                {
                    return Ok(producto);
                }
                else
                {
                    return NotFound("El producto no fue encontrado.");
                }
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetProducto/{Id}")]
        public ActionResult<Producto> GetProducto(int Id)
        {
            try
            {
                Producto? producto = repository.ObtenerProducto(Id);
                if (producto != null)
                {
                    return Ok(producto);
                }
                else
                {
                    return NotFound("El producto no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult InsertarProducto([FromBody] Producto producto)
        {
            try
            {
                Producto? productoCreado = repository.CrearProducto(producto);
                return StatusCode(StatusCodes.Status201Created, productoCreado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public ActionResult<Producto> ActualizarProducto(int Id, [FromBody] Producto productoParaActualizar)
        {
            try
            {
                Producto? productoActualizado = repository.ModificarProducto(Id, productoParaActualizar);
                if (productoActualizado != null)
                {
                    return Ok(productoActualizado);
                }
                else
                {
                    return NotFound("El producto no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult EliminarProducto([FromBody] int Id)
        {
            try
            {
                bool verificaEliminacion = repository.EliminarProducto(Id);
                if (verificaEliminacion)
                {
                    return Ok();
                }
                else
                {
                    return NotFound("El producto no fue encontrado.");
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}