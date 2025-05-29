using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICuidArte.Data;
using WebAPICuidArte.Models;

namespace WebAPICuidArte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BDContexto _context;

        public AuthController(BDContexto context)
        {
            _context = context;
        }

        // Registro de Adulto mayor
        [HttpPost("registro/adulto")]
        public IActionResult RegistrarAdulto(AdultoMayor adulto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar que el correo no esté registrado ni en adultos ni cuidadores
            bool correoExistente = _context.AdultosMayores.Any(a => a.Correo == adulto.Correo) ||
                                   _context.Cuidadores.Any(c => c.Correo == adulto.Correo);

            if (correoExistente)
                return Conflict("El correo ya está registrado.");

            _context.AdultosMayores.Add(adulto);
            _context.SaveChanges();

            return Ok("Registro exitoso.");
        }

        // Registro Cuidador
        [HttpPost("registro/cuidador")]
        public IActionResult RegistrarCuidador(Cuidador cuidador)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar que el correo no esté registrado ni en cuidadores ni adultos
            bool correoExistente = _context.Cuidadores.Any(c => c.Correo == cuidador.Correo) ||
                                   _context.AdultosMayores.Any(a => a.Correo == cuidador.Correo);

            if (correoExistente)
                return Conflict("El correo ya está registrado.");

            _context.Cuidadores.Add(cuidador);
            _context.SaveChanges();

            return Ok("Registro exitoso.");
        }

        // Inicio de sesión
        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLogin usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Contrasenia))
                return BadRequest("Debe ingresar correo y contraseña.");

            var adulto = _context.AdultosMayores
                .FirstOrDefault(a => a.Correo == usuario.Correo && a.Contrasenia == usuario.Contrasenia);

            if (adulto != null)
                return Ok(new
                {
                    TipoUsuario = "AdultoMayor",
                    Usuario = adulto
                });

            var cuidador = _context.Cuidadores
                .FirstOrDefault(c => c.Correo == usuario.Correo && c.Contrasenia == usuario.Contrasenia);

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
