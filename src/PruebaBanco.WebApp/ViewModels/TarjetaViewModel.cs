namespace PruebaBanco.WebApp.ViewModels
{
    public class TarjetaViewModel
    {
        public int id { get; set; }
        public string nombreTitular { get; set; }
        public string numeroTarjeta { get; set; }
        public double montoOtorgado { get; set; }
        public double porcentajeInteres { get; set; }
        public double porcentajeInteresMinimo { get; set; }
        public double saldoTotal { get; set; }
        public double cuotaMinima { get; set; }
        public double interesBonificable { get; set; }
        public double totalContadoConInteres { get; set; }
        public double saldoDisponible { get; set; }
        public double comprasTotal { get; set; }
        public double comprasMesAnterior { get; set; }

        
    }
}
