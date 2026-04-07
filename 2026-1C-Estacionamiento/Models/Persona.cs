namespace _2026_1C_Estacionamiento.Models
{
    public class Persona
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
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
