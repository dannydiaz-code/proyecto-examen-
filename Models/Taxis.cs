using System;
using System.Collections.Generic;

namespace TaxiApp.Models
{
    public class Taxis
    {
        public int Id { get; set; }

        public string Placa { get; set; }

        public string Observacion { get; set; }

        public bool Eliminado { get; set; }

        public DateTime CreatedDate { get; set; }

        // Foreign Key
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public ICollection<RegistroDiario> RegistrosDiarios { get; set; }
    }
}
