namespace _2026_1C_Estacionamiento.Models
{
    public class Direccion
    {
        public int Id { get; set; }

        public string Calle { get; set; }


        public int Altura { get; set; }
        public int CodigoPostal { get; set; }

        public string Localidad { get; set; }

        public string Provincia { get; set; }

        public int ClienteId { get; set; }  //Propiedad Relacional

        public Cliente Cliente { get; set; } // Propiedad Navegacional

    }
}
