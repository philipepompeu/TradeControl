﻿using Microsoft.EntityFrameworkCore;
using TradeControl.Domain.Model;

public class TradeControlDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<TradeOperation> TradeOperations { get; set; }
    
    public TradeControlDbContext(DbContextOptions<TradeControlDbContext> options) : base(options) 
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TradeOperation>()
            .HasIndex(o => new { o.UserId, o.AssetId, o.Date })
            .HasDatabaseName("IX_TradeOperation_User_Asset_Date");
    }

}