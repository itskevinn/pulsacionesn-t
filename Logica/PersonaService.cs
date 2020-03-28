using System;

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
        
        public List<Persona> ConsultarTodos ( )
        {
            _conexion.Open ( );
            List<Persona> personas = _repositorio.ConsultarTodos ( );
            _conexion.Close ( );
            return personas;
        }
    }
}