namespace PruebaBanco.WebApp.Models
{
    public class TarjetaEntity
    {
        public int id { get; set; }

        public string? nombreTitular { get; set; }

        public string? numeroTarjeta { get; set; }

        public double? montoOtorgado { get; set; }

        public double? porcentajeInteres { get; set; }

        public double? porcentajeInteresMinimo { get; set; }
    }
}
