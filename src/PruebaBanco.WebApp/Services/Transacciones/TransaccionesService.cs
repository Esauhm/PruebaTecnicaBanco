using PruebaBanco.WebApp.ViewModels;
using System.Net.Http;

namespace PruebaBanco.WebApp.Services.MovimientosTarjeta
{
    public class TransaccionesService : ITransaccionesService
    {

        private readonly HttpClient _httpClient;

        public TransaccionesService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7258/api");
        }
        public async Task<IEnumerable<TransaccionesViewModel>> GetTransactionesAsync(int idTarjeta, int Mes, int Anio)
        {
            

            var url = $"https://localhost:7258/api/Transacciones/GetTransacciones?IdTarjeta={idTarjeta}&Mes={Mes:00}&Anio={Anio}";

            var transactiones = await _httpClient.GetFromJsonAsync<IEnumerable<TransaccionesViewModel>>(url);
            return transactiones;
        }
    }
}
