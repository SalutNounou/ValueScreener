using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
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

        }
    }
}
