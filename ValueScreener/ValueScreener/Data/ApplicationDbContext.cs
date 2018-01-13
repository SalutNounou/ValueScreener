using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ValueScreener.Models;
using ValueScreener.Models.Domain;

namespace ValueScreener.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PricingResult> PricingResults { get; set; }
        public DbSet<MarketData> MarketDatas { get; set; }
        public DbSet<FinancialStatement> FinancialStatements { get; set; }
        public DbSet<BalanceSheet> BalanceSheets { get; set; }
        public DbSet<IncomeStatement> IncomeStatements { get; set; }
        public DbSet<CashFlowStatement> CashFlowStatements { get; set; }
        public DbSet<AnnualResult> AnnualResults { get; set; }
        public DbSet<PiotroskiResult> PiotroskiResults { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FinancialStatement>().HasOne(f => f.BalanceSheet).WithOne(b => b.FinancialStatement)
                .HasForeignKey<BalanceSheet>(b => b.FinancialStatementId);
            builder.Entity<FinancialStatement>().HasOne(f => f.IncomeStatement).WithOne(b => b.FinancialStatement)
                .HasForeignKey<IncomeStatement>(b => b.FinancialStatementId);
            builder.Entity<FinancialStatement>().HasOne(f => f.CashFlowStatement).WithOne(b => b.FinancialStatement)
                .HasForeignKey<CashFlowStatement>(b => b.FinancialStatementId);

            builder.Entity<Stock>().HasOne(s => s.MarketData).WithOne(m => m.Stock)
                .HasForeignKey<MarketData>(m => m.StockId);
            builder.Entity<Stock>().HasOne(s => s.PricingResult).WithOne(p => p.Stock)
                .HasForeignKey<PricingResult>(p => p.StockId);

            builder.Entity<FinancialStatement>().HasOne(f => f.Stock).WithMany(s => s.FinancialStatements);

            builder.Entity<AnnualResult>().HasOne(r => r.PricingResult).WithMany(p => p.AnnualResults);

            builder.Entity<PiotroskiResult>().HasOne(f => f.PricingResult).WithMany(s => s.PiotroskiResults);

        }
    }
}
