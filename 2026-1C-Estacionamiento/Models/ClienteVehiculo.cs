namespace _2026_1C_Estacionamiento.Models
{
    public class ClienteVehiculo
    {
        public int Id { get; set; }
        public int ClienteId { get; set; } // Propiedad Relacional
        public int VehiculoId { get; set; } // Propiedad Relacional

        public string ResponsablePrincipal { get; set; }

        public bool Activo { get; set; }

        //propiedad de Navegacion
        public Cliente Cliente { get; set; }
        public Vehiculo Vehiculo { get; set; }

    }
}
