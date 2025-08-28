using Microsoft.EntityFrameworkCore;
using WebApiProject.Data.Entities;

namespace WebApiProject.Data;

public class YahooFinanceDbContext : DbContext
{
    public YahooFinanceDbContext(DbContextOptions<YahooFinanceDbContext> options) : base(options)
    {
    }

    public DbSet<StockQuote> StockQuotes { get; set; }
    public DbSet<TrendingStock> TrendingStocks { get; set; }
    public DbSet<MarketIndex> MarketIndices { get; set; }
    public DbSet<MarketSector> MarketSectors { get; set; }
    public DbSet<CurrencyExchangeRate> CurrencyExchangeRates { get; set; }
    public DbSet<CryptocurrencyData> CryptocurrencyData { get; set; }
    public DbSet<BulkQuoteOperation> BulkQuoteOperations { get; set; }
    public DbSet<BulkQuoteGroup> BulkQuoteGroups { get; set; }
    public DbSet<ApiHealthMetric> ApiHealthMetrics { get; set; }
    public DbSet<ApiRequest> ApiRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure table names and schemas
        modelBuilder.Entity<StockQuote>(entity =>
        {
            entity.ToTable("stock_quotes");
            entity.HasIndex(e => new { e.Symbol, e.Region, e.QuoteTimestamp })
                  .IsUnique()
                  .HasDatabaseName("uk_symbol_region_timestamp");
        });

        modelBuilder.Entity<TrendingStock>(entity =>
        {
            entity.ToTable("trending_stocks");
            entity.HasIndex(e => new { e.Region, e.Symbol, e.JobTimestamp })
                  .IsUnique()
                  .HasDatabaseName("uk_region_symbol_timestamp");
        });

        modelBuilder.Entity<MarketIndex>(entity =>
        {
            entity.ToTable("market_indices");
            entity.HasIndex(e => new { e.Region, e.Symbol, e.MarketTime })
                  .IsUnique()
                  .HasDatabaseName("uk_region_symbol_timestamp");
        });

        modelBuilder.Entity<MarketSector>(entity =>
        {
            entity.ToTable("market_sectors");
            entity.HasIndex(e => new { e.Region, e.Name, e.CreatedAt })
                  .IsUnique()
                  .HasDatabaseName("uk_region_name_timestamp");
        });

        modelBuilder.Entity<CurrencyExchangeRate>(entity =>
        {
            entity.ToTable("currency_exchange_rates");
            entity.HasIndex(e => new { e.FromCurrency, e.ToCurrency, e.LastUpdate })
                  .IsUnique()
                  .HasDatabaseName("uk_from_to_currency_timestamp");
        });

        modelBuilder.Entity<CryptocurrencyData>(entity =>
        {
            entity.ToTable("cryptocurrency_data");
            entity.HasIndex(e => new { e.Symbol, e.Currency, e.LastUpdate })
                  .IsUnique()
                  .HasDatabaseName("uk_symbol_currency_timestamp");
        });

        modelBuilder.Entity<BulkQuoteOperation>(entity =>
        {
            entity.ToTable("bulk_quote_operations");
            entity.HasKey(e => e.OperationId);
            entity.Property(e => e.OperationId).HasColumnName("operation_id");
        });

        modelBuilder.Entity<BulkQuoteGroup>(entity =>
        {
            entity.ToTable("bulk_quote_groups");
            entity.HasOne<BulkQuoteOperation>()
                  .WithMany()
                  .HasForeignKey(e => e.OperationId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ApiHealthMetric>(entity =>
        {
            entity.ToTable("api_health_metrics");
        });

        modelBuilder.Entity<ApiRequest>(entity =>
        {
            entity.ToTable("api_requests");
            entity.HasKey(e => e.RequestId);
            entity.Property(e => e.RequestId).HasColumnName("request_id");
        });

        base.OnModelCreating(modelBuilder);
    }
}
