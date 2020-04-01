using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entity;

using Logica;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using pulsacionesdotnet.Models;

namespace pulsacionesdotnet.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase {
        private readonly PersonaService _personaService;
        public IConfiguration Configuration { get; }
        public PersonaController (IConfiguration configuration) {
            Configuration = configuration;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _personaService = new PersonaService (connectionString);
        }

        // GET: api/Persona
        [HttpGet]
        public IEnumerable<PersonaViewModel> Get ( ) {
            var personas = _personaService.ConsultarTodos ( ).Personas.Select (p => new PersonaViewModel (p));
            return personas;
        }

        // GET: api/Persona/5
        [HttpGet ("{identificacion}")]
        public ActionResult<PersonaViewModel> Get (string identificacion) {
            var persona = _personaService.BuscarPorIdentificacion (identificacion).PersonaEncontrada;
            if (persona == null) return NotFound ( );
            var personaViewModel = new PersonaViewModel (persona);
            return personaViewModel;
        }
        // POST: api/Persona
        [HttpPost]
        public ActionResult<PersonaViewModel> Post (PersonaInputModel personaInput) {
            Persona persona = MapearPersona (personaInput);
            var response = _personaService.Guardar (persona);
            if (response.Error) {
                return BadRequest (response.Mensaje);
            }
            return Ok (response.Persona);
        }

        private Persona MapearPersona (PersonaInputModel personaInput) {
            var persona = new Persona {
                Identificacion = personaInput.Identificacion,
                Nombre = personaInput.Nombre,
                Edad = personaInput.Edad,
                Sexo = personaInput.Sexo
            };
            return persona;
        }
    }
}