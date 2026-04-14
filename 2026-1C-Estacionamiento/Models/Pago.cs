namespace _2026_1C_Estacionamiento.Models
{
    public class Pago
    {
        public int Id { get; set; }


        public decimal Monto { get; set; }

        public int EstanciaId { get; set; } // Propiedad Relacional

        public Estancia Estancia { get; set; } // Propiedad Navegacional

    }
}
