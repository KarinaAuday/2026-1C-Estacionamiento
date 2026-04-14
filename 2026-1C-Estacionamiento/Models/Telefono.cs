using System.ComponentModel.DataAnnotations;

namespace _2026_1C_Estacionamiento.Models
{
    public class Telefono
    {
        public int Id { get; set; }
        public int CodArea { get; set; }

        public string Numero { get; set; }


        public bool Principal { get; set; }

        public int ClienteId { get; set; } // Propiedad Relacional

        public Cliente Cliente { get; set; } // Propiedad Navegacional

        public TipoTelefono Tipo { get; set; }
    }
}
