using System;
using System.Collections.Generic;

using Datos;

using Entity;

namespace Logica {
    public class PersonaService {
        private readonly ConnectionManager _conexion;
        private readonly PersonaRepository _repositorio;
        public PersonaService (string lineaDeConexion) {
            _conexion = new ConnectionManager (lineaDeConexion);
            _repositorio = new PersonaRepository (_conexion);
        }

        public GuardarPersonaResponse Guardar (Persona persona) {
            try {
                persona.CalcularPulsaciones ( );
                _conexion.Open ( );
                _repositorio.Guardar (persona);
                _conexion.Close ( );
                return new GuardarPersonaResponse (persona);
            }
            catch (Exception e) {
                return new GuardarPersonaResponse ($"Error de la Aplicacion: {e.Message}");
            } finally { _conexion.Close ( ); }
        }
        public ConsultarTodosResponse ConsultarTodos ( ) {
            try {
                _conexion.Open ( );
                List<Persona> personas = _repositorio.ConsultarTodos ( );
                _conexion.Close ( );
                return new ConsultarTodosResponse(personas);
            }
            catch (Exception e) {
                return new ConsultarTodosResponse($"Error de la Aplicacion: {e.Message}");
            } finally {
                _conexion.Close ( );
            }

        }
        public BuscarPorIdentificacionResponse BuscarPorIdentificacion (string IdABuscar) {
            try {
                _conexion.Open ( );
                Persona persona = _repositorio.ConsultarPorIdentificacion (IdABuscar);
                _conexion.Close ( );
                return new BuscarPorIdentificacionResponse (persona);
            }
            catch (Exception e) {
                return new BuscarPorIdentificacionResponse ($"Ha ocurrido un error inesperado {e.Message}");
            } finally {
                _conexion.Close ( );
            }
        }
        public class BuscarPorIdentificacionResponse {
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public Persona PersonaEncontrada { get; set; }

            public BuscarPorIdentificacionResponse (Persona persona) {
                Error = false;
                PersonaEncontrada = persona;
            }
            public BuscarPorIdentificacionResponse (string mensaje) {
                Mensaje = mensaje;
                Error = true;
            }
        }
        public class GuardarPersonaResponse {
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public Persona Persona { get; set; }
            public GuardarPersonaResponse (Persona persona) {
                Error = false;
                Persona = persona;
            }
            public GuardarPersonaResponse (string mensaje) {
                Error = true;
                Mensaje = mensaje;
            }
        }
        public class ConsultarTodosResponse {
            public bool Error { get; set; }
            public string Mensaje { get; set; }
            public List<Persona> Personas { get; set; }
            public ConsultarTodosResponse (List<Persona> personas) {
                Error = false;
                Personas = personas;
            }
            public ConsultarTodosResponse (string mensaje) {
                Error = true;
                Mensaje = mensaje;
            }
        }
    }
}