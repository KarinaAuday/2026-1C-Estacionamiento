namespace _2026_1C_Estacionamiento.Models
{
    public class Cliente : Persona
    {
        public string Cuit  { get; set; }

        public List<Telefono> Telefonos { get; set; } 

        public Direccion Direccion { get; set; }
    }
}
