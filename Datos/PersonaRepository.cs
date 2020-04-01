using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Entity;

namespace Datos {
    public class PersonaRepository {
        private readonly SqlConnection _connection;
        private readonly List<Persona> _personas = new List<Persona> ( );
        public PersonaRepository (ConnectionManager connection) {
            _connection = connection._conexion;
        }
        public void Guardar (Persona persona) {
            using (var comando = _connection.CreateCommand ( )) {
                comando.CommandText = @"Insert Into Persona (Identificacion, Nombre, Edad, Sexo, Pulsacion) values (@id, @nombre,
@edad, @sexo, @pulsacion)";
                comando.Parameters.AddWithValue ("@id", persona.Identificacion);
                comando.Parameters.AddWithValue ("@nombre", persona.Nombre);
                comando.Parameters.AddWithValue ("@sexo", persona.Sexo);
                comando.Parameters.AddWithValue ("@edad", persona.Edad);
                comando.Parameters.AddWithValue ("@pulsacion", persona.Pulsacion);
                var filas = comando.ExecuteNonQuery ( );
            }
        }
        public List<Persona> ConsultarTodos ( ) {
            SqlDataReader lector;
            List<Persona> personas = new List<Persona> ( );
            using (var comando = _connection.CreateCommand ( )) {
                comando.CommandText = "Select * from Persona";
                lector = comando.ExecuteReader ( );
                if (lector.HasRows) {
                    while (lector.Read ( )) {
                        Persona persona = MapearPersonaEnLector (lector);
                        personas.Add (persona);
                    }
                }
            }
            return personas;
        }
        public Persona ConsultarPorIdentificacion (string identificacionABuscar) {
            SqlDataReader lector;
            using (var comando = _connection.CreateCommand ( )) {
                comando.CommandText = "SELECT * FROM persona WHERE Identificacion = @idPersona";
                comando.Parameters.AddWithValue ("@idPersona", identificacionABuscar);
                lector = comando.ExecuteReader ( );
                lector.Read ( );
                return MapearPersonaEnLector (lector);
            }
        }
        private Persona MapearPersonaEnLector (SqlDataReader dataReader)

        {
            if (!dataReader.HasRows) return null;
            Persona persona = new Persona ( );
            persona.Identificacion = (string) dataReader["Identificacion"];
            persona.Nombre = (string) dataReader["Nombre"];
            persona.Sexo = (string) dataReader["Sexo"];
            persona.Edad = (decimal) dataReader["Edad"];
            persona.Pulsacion = (decimal) dataReader["Pulsacion"];
            return persona;
        }
    }
}