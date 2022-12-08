using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_SistemaGestionVentas.Models;
using ProyectoFinal_SistemaGestionVentas.Repositories;

namespace ProyectoFinal_SistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepository repository = new UsuarioRepository();

        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            try
            {
                List<Usuario> lista = repository.ListarUsuarios();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public ActionResult<Usuario> Get(int Id)
        {
            try 
            {
                Usuario? usuario = repository.ObtenerUsuario(Id);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("El usuario no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetUsuario/{Id}")]
        public ActionResult<Usuario> GetUsuario(int Id)
        {
            try
            {
                Usuario? usuario = repository.ObtenerUsuario(Id);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("El usuario no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult InsertarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                Usuario? usuarioCreado = repository.CrearUsuario(usuario);
                return StatusCode(StatusCodes.Status201Created, usuarioCreado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public ActionResult<Usuario> ActualizarUsuario(int Id, [FromBody] Usuario usuarioParaActualizar)
        {
            try
            {
                Usuario? usuarioActualizado = repository.ModificarUsuario(Id, usuarioParaActualizar);
                if (usuarioActualizado != null)
                {
                    return Ok(usuarioActualizado);
                }
                else
                {
                    return NotFound("El usuario no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult EliminarUsuario([FromBody] int Id)
        {
            try
            {
                bool verificaEliminacion = repository.EliminarUsuario(Id);
                if (verificaEliminacion)
                {
                    return Ok();
                }
                else
                {
                    return NotFound("El usuario no fue encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}