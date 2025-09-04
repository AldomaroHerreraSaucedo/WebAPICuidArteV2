using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICuidArte.Data;
using WebAPICuidArte.Models;

namespace WebAPICuidArte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {
        private readonly BDContexto _context;

        public InicioSesionController(BDContexto context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Contrasenia))
                return BadRequest("Debe ingresar correo y contraseña.");

            var adultoMayor = _context.AdultosMayores.FirstOrDefault(a => a.Correo == usuario.Correo && a.Contrasenia == usuario.Contrasenia);

            if (adultoMayor != null)
                return Ok(new
                {
                    TipoUsuario = "AdultoMayor",
                    Usuario = adultoMayor
                });

            var cuidador = _context.Cuidadores.FirstOrDefault(c => c.Correo == usuario.Correo && c.Contrasenia == usuario.Contrasenia);

            if (cuidador != null)
                return Ok(new
                {
                    TipoUsuario = "Cuidador",
                    Usuario = cuidador
                });

            return Unauthorized("Correo o contraseña incorrectos.");
        }
    }
}
