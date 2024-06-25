namespace PruebaBanco.WebApi.ViewModels
{
    public class TarjetaViewModel
    {
        public int Id { get; set; }

        public string? NombreTitular { get; set; }

        public string? NumeroTarjeta { get; set; }

        public double MontoOtorgado { get; set; }

        public double PorcentajeInteres { get; set; }

        public double PorcentajeInteresMinimo { get; set; }

    }
}
