using _2026_1C_Estacionamiento.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2026_1C_Estacionamiento.Models
{
    public class Persona
    {
        public int Id { get; set; }


        [Required (ErrorMessage = ErrorMsg.Requerido)]
        [StringLength(50, MinimumLength = 2 , ErrorMessage = ErrorMsg.LargoMinMax)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = ErrorMsg.SoloLetras)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMsg.Requerido)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMsg.LargoMinMax)]

        public string Apellido { get; set; }

        [Required(ErrorMessage = ErrorMsg.Requerido)]
        [RegularExpression (@"^\d{8}$", ErrorMessage = "El campo {0} debe contener exactamente 8 dígitos.")]
        public string Dni { get; set; }

        [Required (ErrorMessage = ErrorMsg.Requerido)]
        [DataType (DataType.PhoneNumber, ErrorMessage = "El campo {0} debe ser un número de teléfono válido.")]
        public string Telefono { get; set; }

        [DataType ( DataType.EmailAddress, ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        public string Email { get; set; }
     
        public string Foto { get; set; }

        public string NombreCompleto {
            get
            {
                return $"{Nombre} {Apellido}";
            }
        }

        
    }
}
