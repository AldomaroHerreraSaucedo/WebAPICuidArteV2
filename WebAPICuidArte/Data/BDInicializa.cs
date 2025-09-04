using WebAPICuidArte.Models;

namespace WebAPICuidArte.Data
{
    public class BDInicializa
    {
        public static void Inicializar(BDContexto contexto)
        {
            // Registros de Adultos mayores
            if (contexto.AdultosMayores.Any())
            {
                return;
            }
            var adultoMayor = new AdultoMayor[]
            {
                new AdultoMayor { Apellidos = "Torres Sánchez", Nombres = "Carlos Eduardo", FechaNacimiento = "20-5-1950", Sexo = "Masculino", Peso = 75.5f, Talla = 170, Correo = "carlos.torres@gmail.com", Contrasenia = "12345678" },
                new AdultoMayor { Apellidos = "Ramirez Castillo", Nombres = "Marta Isabel", FechaNacimiento = "10-5-1960", Sexo = "Femenino", Peso = 60.0f, Talla = 160, Correo = "marta.ramirez@gmail.com", Contrasenia = "12345678" }
            };
            contexto.AdultosMayores.AddRange(adultoMayor);
            contexto.SaveChanges();


            // Registros de Cuidadores
            if (contexto.Cuidadores.Any())
            {
                return;
            }
            var cuidador = new Cuidador[]
            {
                new Cuidador { Apellidos = "Perez", Nombres = "Juan", Correo = "juan.perez@gmail.com", Contrasenia = "12345678" },
                new Cuidador { Apellidos = "Lopez Vargas", Nombres = "Ana Beatriz", Correo = "ana.lopez@gmail.com", Contrasenia = "12345678" }
            };
            contexto.Cuidadores.AddRange(cuidador);
            contexto.SaveChanges();


            // Registros de Detalle AdultoMayorCuidador
            if (contexto.AdultoMayorCuidadores.Any())
            {
                return;
            }
            var detalleAMC = new AdultoMayorCuidador[]
            {
                new AdultoMayorCuidador { AdultoMayorId = 1, CuidadorId = 1 },
                new AdultoMayorCuidador { AdultoMayorId = 2, CuidadorId = 1 }
            };
            contexto.AdultoMayorCuidadores.AddRange(detalleAMC);
            contexto.SaveChanges();
        }
    }
}
