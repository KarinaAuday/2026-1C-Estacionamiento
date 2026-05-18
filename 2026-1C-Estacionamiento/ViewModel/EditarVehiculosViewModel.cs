using Microsoft.AspNetCore.Mvc.Rendering;

namespace _2026_1C_Estacionamiento.ViewModel
{
    public class EditarVehiculosViewModel
    {
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public List<VehiculoAsociadoViewModel> VehiculosAsociados { get; set; }
        public SelectList VehiculosDisponibles { get; set; }
        public int VehiculoIdSeleccionado { get; set; }
        public string ResponsablePrincipal { get; set; }
    }
}
