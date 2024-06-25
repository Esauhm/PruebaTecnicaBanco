namespace PruebaBanco.WebApi.ViewModels
{
    public class TarjetaInfoViewModel
    {
        public int Id { get; set; }

        public string? NombreTitular { get; set; }

        public string? NumeroTarjeta { get; set; }

        public double MontoOtorgado { get; set; }

        public double PorcentajeInteres { get; set; }

        public double PorcentajeInteresMinimo { get; set; }

        public double SaldoTotal { get; set; }

        public double CuotaMinima { get; set; }

        public double InteresBonificable { get; set; }

        public double TotalContadoConInteres { get; set; }

        public double SaldoDisponible { get; set; }

        public double ComprasTotal { get; set; }

        public double ComprasMesAnterior { get; set; }
    }
}
