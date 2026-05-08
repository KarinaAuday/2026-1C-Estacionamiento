using _2026_1C_Estacionamiento.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2026_1C_Estacionamiento.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = ErrorMsg.Requerido)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMsg.LargoMinMax)]
        
        public string Calle { get; set; }

        [Required(ErrorMessage = ErrorMsg.Requerido)]
        
        public int Altura { get; set; }
        public int CodigoPostal { get; set; }

        public string Localidad { get; set; }

        public string Provincia { get; set; }

        public int ClienteId { get; set; }  //Propiedad Relacional

        public Cliente Cliente { get; set; } // Propiedad Navegacional

    }
}
