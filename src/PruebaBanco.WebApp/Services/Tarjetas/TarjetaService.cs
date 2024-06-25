using PruebaBanco.WebApp.Models;
using PruebaBanco.WebApp.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PruebaBanco.WebApp.Services.Tarjetas
{
    public class TarjetaService : ITarjetaService
    {
        private readonly HttpClient _httpClient;

        public TarjetaService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7258/api");
        }

        public async Task<List<TarjetaEntity>> GetAll()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Tarjeta/AllTarjetas");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                List<TarjetaEntity> tarjetas = JsonSerializer.Deserialize<List<TarjetaEntity>>(jsonString);
                return tarjetas;
            }
            else
            {
                // Manejar el error de alguna manera apropiada
                throw new Exception("Error al obtener las tarjetas de crédito desde la API.");
            }
        }

        public async Task<TarjetaViewModel> GetByNumeroTarjeta(string numeroTarjeta)
        {
            var requestUri = $"api/Tarjeta/TarjetasByNumero?numeroTarjeta={Uri.EscapeDataString(numeroTarjeta)}";

            // Crear el contenido de la solicitud con un cuerpo vacío
            var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var tarjetaVM = JsonSerializer.Deserialize<TarjetaViewModel>(jsonString);
                return tarjetaVM;
            }
            else
            {
                throw new Exception($"Error al obtener la información de la tarjeta. Código de estado: {response.StatusCode}");
            }
        }

        public async Task<PresentacionViewModel> GetTarjetaByIdAsync(int idTarjeta)
        {
            var url = $"https://localhost:7258/api/Tarjeta/GetDataById?idTarjeta={idTarjeta}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se pudo obtener la data");
            }

            var tarjeta = await response.Content.ReadFromJsonAsync<PresentacionViewModel>();
            return tarjeta;
        }

        public async Task RealizarTransaccion(TransaccionesViewModel model)
        {
            var jsonData = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/Transacciones/RealizarTransaccion", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al realizar la compra. Código de estado: {response.StatusCode}");
            }
        }

    }
}
