using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_SistemaGestionVentas.Models;
using ProyectoFinal_SistemaGestionVentas.Repositories;

namespace ProyectoFinal_SistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class VentaController : Controller
    {
        private VentaRepository repository = new VentaRepository();

        [HttpGet]
        public ActionResult<List<Venta>> GetVenta()
        {
            try
            {
                List<Venta> lista = repository.ListarVentas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<Venta> Get(int Id)
        {
            try
            {
                Venta? venta = repository.ObtenerVenta(Id);
                if (venta != null)
                {
                    return Ok(venta);
                }
                else
                {
                    return NotFound("La venta no fue encontrada.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetVenta/{Id}")]
        public ActionResult<Venta> GetVenta(int Id)
        {
            try
            {
                Venta? venta = repository.ObtenerVenta(Id);
                if (venta != null)
                {
                    return Ok(venta);
                }
                else
                {
                    return NotFound("La venta no fue encontrada.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult InsertarVenta([FromBody] Venta venta)
        {
            try
            {
                Venta? ventaCreada = repository.CrearVenta(venta);
                return StatusCode(StatusCodes.Status201Created, ventaCreada);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public ActionResult<Venta> ActualizarVenta(int Id, [FromBody] Venta ventaParaActualizar)
        {
            try
            {
                Venta? ventaActualizado = repository.ModificarVenta(Id, ventaParaActualizar);
                if (ventaActualizado != null)
                {
                    return Ok(ventaActualizado);
                }
                else
                {
                    return NotFound("La venta no fue encontrada.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult EliminaVenta([FromBody] int Id)
        {
            try
            {
                bool verificaEliminacion = repository.EliminarVenta(Id);
                if (verificaEliminacion)
                {
                    return Ok();
                }
                else
                {
                    return NotFound("La venta no fue encontrada.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}