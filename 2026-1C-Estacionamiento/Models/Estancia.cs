namespace _2026_1C_Estacionamiento.Models
{
    public class Estancia
    {
        public int Id { get; set; }



        public decimal Monto { get; private set; }


        public DateTime Inicio { get; set; }

        public DateTime Fin { get; set; }
    }
}
