using System;
using System.Collections.Generic;

namespace TaxiApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; }

        public string Contrasena { get; set; }

        public bool Eliminado { get; set; }

        public DateTime CreatedDate { get; set; }

        // Relaciones
        public ICollection<Taxis> Taxis { get; set; }

        public ICollection<RegistroDiario> RegistrosDiarios { get; set; }
    }
}
