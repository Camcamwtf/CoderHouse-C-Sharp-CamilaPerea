using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_SistemaGestionVentas.Models;
using ProyectoFinal_SistemaGestionVentas.Repositories;

namespace ProyectoFinal_SistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class ProductoVendidoController : Controller
    {
        private ProductoVendidoRepository repository = new ProductoVendidoRepository();

        [HttpGet]
        public ActionResult<List<ProductoVendido>> Get()
        {
            try
            {
                List<ProductoVendido> lista = repository.ListarProductosVendidos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<ProductoVendido> Get(int Id)
        {
            try
            {
                ProductoVendido? productoVendido = repository.ObtenerProductoVendido(Id);
                if (productoVendido != null)
                {
                    return Ok(productoVendido);
                }
                else
                {
                    return NotFound("El producto vendido no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetProductoVendido/{Id}")]
        public ActionResult<ProductoVendido> GetProductoVendido(int Id)
        {
            try
            {
                ProductoVendido? productoVendido = repository.ObtenerProductoVendido(Id);
                if (productoVendido != null)
                {
                    return Ok(productoVendido);
                }
                else
                {
                    return NotFound("El producto vendido no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult InsertarProductoVendido([FromBody] ProductoVendido productoVendido)
        {
            try
            {
                ProductoVendido? productoVendidoCreado = repository.CrearProductoVendido(productoVendido);
                return StatusCode(StatusCodes.Status201Created, productoVendidoCreado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public ActionResult<ProductoVendido> ActualizarProducto(int Id, [FromBody] ProductoVendido productoVendidoParaActualizar)
        {
            try
            {
                ProductoVendido? productoVendidoActualizado = repository.ModificarProductoVendido(Id, productoVendidoParaActualizar);
                if (productoVendidoActualizado != null)
                {
                    return Ok(productoVendidoActualizado);
                }
                else
                {
                    return NotFound("El producto vendido no fue encontrado.");
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
                bool verificaEliminacion = repository.EliminarProductoVendido(Id);
                if (verificaEliminacion)
                {
                    return Ok();
                }
                else
                {
                    return NotFound("El producto vendido no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}