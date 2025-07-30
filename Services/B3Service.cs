using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Text.Json;
using TradeControl.Dtos;

namespace TradeControl.Services{
    
    public class B3Service
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public B3Service(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }
        public async Task<AssetPriceView> ObterCotacaoAsync(string ticker)
        {
            var stopwatch = Stopwatch.StartNew();
            
            string cacheKey = $"cotacao:{ticker.ToUpper()}";

            try
            {
                if (_cache.TryGetValue(cacheKey, out AssetPriceView assetPriceView)) 
                {
                    Console.WriteLine("Obteve cotação do cache");
                    return assetPriceView;               
                }

                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

                HttpResponseMessage response = await _httpClient.GetAsync($"https://b3api.vercel.app/api/Assets/{ticker}", cts.Token);

                response.EnsureSuccessStatusCode();

                string conteudo = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Resposta da B3: {conteudo}");

                AssetPriceView dto = JsonSerializer.Deserialize<AssetPriceView>(conteudo, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _cache.Set(cacheKey, dto, new MemoryCacheEntryOptions
                {
                    Size = 1,
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(1)
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
