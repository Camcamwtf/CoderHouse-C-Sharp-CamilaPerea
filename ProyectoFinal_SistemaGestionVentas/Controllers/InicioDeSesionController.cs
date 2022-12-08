using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_SistemaGestionVentas.Models;
using ProyectoFinal_SistemaGestionVentas.Repositories;

namespace ProyectoFinal_SistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class InicioDeSesionController : Controller
    {
        InicioDeSesionRepository repository = new InicioDeSesionRepository();

        [HttpPost]
        public ActionResult<Usuario> InicioDeSesion(Usuario usuario)
        {
            try
            {
                return Ok();
                // Pendiente de hacer para entrega final.
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}