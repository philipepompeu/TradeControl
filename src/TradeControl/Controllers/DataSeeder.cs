using System;
using TradeControl.Domain.Model;

namespace TradeControl.Controllers
{
    public class DataSeeder
    {

        public static void Generate(TradeControlDbContext context)
        {
            GenerateUsers(context);
            GenerateAssets(context);
            GenerateTradeOperations(context);
            context.SaveChanges();
        }

        private static void GenerateTradeOperations(TradeControlDbContext context)
        {
            var random = new Random();

            foreach (var user in context.Users.ToList())
            {
                foreach (var asset in context.Assets.ToList())
                {
                    var trade = new TradeOperation { 
                        Id = Guid.NewGuid(),
                        Asset = asset,
                        AssetId = asset.Id,
                        User = user,
                        UserId = user.Id,
                        UnitPrice = Math.Round((decimal)(random.NextDouble() * (50 - 5) + 5), 2),
                        Quantity = random.Next(1, 100),
                        BrokerageFee = Math.Round((decimal)(random.NextDouble() * (5 - 1) + 1), 2),
                        Date = DateTime.UtcNow.AddDays(-random.Next(1, 90))
                    };
                    context.TradeOperations.Add(trade);
                }
            }
        }

        private static void GenerateAssets(TradeControlDbContext context)
        {
            if (!context.Assets.Any())
            {
                context.Assets.AddRange(new List<Asset> {
                    new Asset { Id = Guid.NewGuid(), Ticker = "PETR4", Name = "Petrobras PN" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "VALE3", Name = "Vale ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "ITUB4", Name = "Itaú Unibanco PN" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "BBDC4", Name = "Bradesco PN" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "B3SA3", Name = "B3 ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "ABEV3", Name = "Ambev ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "WEGE3", Name = "Weg ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "BBAS3", Name = "Banco do Brasil ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "RENT3", Name = "Localiza ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "BRFS3", Name = "BRF ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "ITSA4", Name = "Itaúsa PN" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "SUZB3", Name = "Suzano ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "RADL3", Name = "Raia Drogasil ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "LREN3", Name = "Lojas Renner ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "HAPV3", Name = "Hapvida ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "CSNA3", Name = "CSN ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "EGIE3", Name = "Engie Brasil ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "CPLE6", Name = "Copel PN" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "CYRE3", Name = "Cyrela ON" },
                    new Asset { Id = Guid.NewGuid(), Ticker = "ELET3", Name = "Eletrobras ON" }
                });
            }            
        }

        private static void GenerateUsers(TradeControlDbContext context) 
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User { Id = Guid.NewGuid(), Name = "Alice" });
                context.Users.Add(new User { Id = Guid.NewGuid(), Name = "Bob" });
            }
        }
    }
}
