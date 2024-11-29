using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using cine_web_app.back_end.Models;

namespace cine_web_app.back_end.Services
{
    public class ReservaService
    {
        private readonly HttpClient _httpClient;

        public ReservaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int[,]> ObtenerButacasReservadasAsync(string url)
        {
            int[,] butacasArray = new int[17, 30];

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var reservas = JsonSerializer.Deserialize<List<Reserva>>(jsonResponse);

                if (reservas != null)
                {
                    foreach (var reserva in reservas)
                    {
                        foreach (var butaca in reserva.ButacasReservadas)
                        {
                            var posiciones = butaca.Split('-');
                            if (posiciones.Length == 2 &&
                                int.TryParse(posiciones[0], out int fila) &&
                                int.TryParse(posiciones[1], out int columna))
                            {
                                if (fila >= 1 && fila <= 17 && columna >= 1 && columna <= 30)
                                {
                                    butacasArray[fila - 1, columna - 1] = 1;
                                }
                            }
                        }
                    }
                }
            }

            return butacasArray;
        }
    }
}
