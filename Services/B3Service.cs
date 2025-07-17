using System.Diagnostics;
using System.Text.Json;
using TradeControl.Dtos;

namespace TradeControl.Services{
    
    public class B3Service
    {
        private readonly HttpClient _httpClient;

        public B3Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<AssetPriceView> ObterCotacaoAsync(string ticker)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://b3api.vercel.app/api/Assets/{ticker}");

                response.EnsureSuccessStatusCode();

                string conteudo = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Resposta da B3: {conteudo}");

                AssetPriceView dto = JsonSerializer.Deserialize<AssetPriceView>(conteudo, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return dto;

            }
            catch (Exception ex)
            {
                // Loga o erro
                Console.WriteLine($"Erro ao consultar B3: {ex.Message}");

                return null; // Ou DTO indicando falha
            }finally
            {
                stopwatch.Stop();
                TradeMetrics.B3ApiAvgCallDuration.Record(stopwatch.Elapsed.TotalSeconds);
                TradeMetrics.B3TotalApiCalls.Add(1);
            }
        }
    }
}
