using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entity;

namespace pulsacionesdotnet.Models {
    public class PersonaInputModel {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public decimal Edad { get; set; }
        public string Sexo { get; set; }
    }

    public class PersonaViewModel : PersonaInputModel {
        public decimal Pulsacion { get; set; }
        public PersonaViewModel ( ) {

        }
        public PersonaViewModel (Persona persona) {
            Identificacion = persona.Identificacion;
            Nombre = persona.Nombre;
            Edad = persona.Edad;
            Sexo = persona.Sexo;
            Pulsacion = persona.Pulsacion;
        }
    }
}