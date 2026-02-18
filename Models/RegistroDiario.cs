using System;

namespace TaxiApp.Models
{
    public class RegistroDiario
    {
        public int Id { get; set; }

        public decimal TotalDia { get; set; }

        public decimal Combustible { get; set; }

        public decimal PagoBase { get; set; }

        public decimal PagoConductor { get; set; }

        public decimal PagoDueno { get; set; }

        public decimal Gastos { get; set; }

        public string Observacion { get; set; }

        public bool Eliminado { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime Fecha { get; set; }

        // Foreign Keys
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public int TaxiId { get; set; }

        public Taxis Taxi { get; set; }
    }
}
