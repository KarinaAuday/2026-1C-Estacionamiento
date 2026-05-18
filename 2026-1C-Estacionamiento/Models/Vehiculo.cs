namespace _2026_1C_Estacionamiento.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public int Patente { get; set; }
        public string Marca { get; set; }

        public string Color { get; set; }

        // Propiedad de navegación para relación muchos a muchos
        public List<ClienteVehiculo> ClienteVehiculos { get; set; }

        public int AnioFabricacion { get; set; } = DateTime.Now.Year;
    }
}
