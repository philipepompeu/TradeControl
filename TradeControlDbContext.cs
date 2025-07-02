using Microsoft.EntityFrameworkCore;

public class TradeControlDbContext : DbContext
{
    public TradeControlDbContext(DbContextOptions<TradeControlDbContext> options) : base(options) { }
}