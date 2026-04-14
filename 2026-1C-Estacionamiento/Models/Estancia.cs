namespace _2026_1C_Estacionamiento.Models
{
    public class Estancia
    {
        public int Id { get; set; }

        public decimal Monto { get; private set; }


        public DateTime Inicio { get; set; }

        public DateTime Fin { get; set; }

        public int ClienteId { get; set; } // Propiedad Relacional

       public int VehiculoId { get; set; } // Propiedad Relacional

        public Cliente Cliente { get; set; } // Propiedad Navegacional
        public Vehiculo Vehiculo { get; set; } // Propiedad Navegacional

        public Pago Pago { get; set; } // Propiedad Navegacional
    }
}
