﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using ValueScreener.Data;
using ValueScreener.Models.Domain;

namespace ValueScreener.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ValueScreener.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.AnnualResult", b =>
                {
                    b.Property<int>("AnnualResultId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AssetTurnover");

                    b.Property<decimal>("CurrentRatio");

                    b.Property<decimal>("FreeCashFlow");

                    b.Property<decimal>("GrossMargin");

                    b.Property<decimal>("Leverage");

                    b.Property<decimal>("NetMargin");

                    b.Property<int>("PricingResultId");

                    b.Property<decimal>("QuickRatio");

                    b.Property<decimal>("ReturnOnAssets");

                    b.Property<decimal>("ReturnOnEquity");

                    b.Property<decimal>("ReturnOnInvestedCapital");

                    b.Property<int>("Year");

                    b.HasKey("AnnualResultId");

                    b.HasIndex("PricingResultId");

                    b.ToTable("AnnualResults");
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.BalanceSheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("CashAndCashEquivalent");

                    b.Property<decimal>("CashCashEquivalentAndShortTermInvestments");

                    b.Property<decimal>("CommonStock");

                    b.Property<decimal>("DeferredCharges");

                    b.Property<int>("FinancialStatementId");

                    b.Property<decimal>("Goodwill");

                    b.Property<decimal>("IntangibleAssets");

                    b.Property<decimal>("InventoriesNet");

                    b.Property<decimal>("MinorityInterest");

                    b.Property<decimal>("OtherAssets");

                    b.Property<decimal>("OtherCurrentAssets");

                    b.Property<decimal>("OtherCurrentLiabilities");

                    b.Property<decimal>("OtherEquity");

                    b.Property<decimal>("OtherLiabilities");

                    b.Property<decimal>("PreferredStock");

                    b.Property<decimal>("PropertyPlantEquipmentNet");

                    b.Property<decimal>("RetainedEarnings");

                    b.Property<decimal>("TotalAssets");

                    b.Property<decimal>("TotalCurrentAssets");

                    b.Property<decimal>("TotalCurrentLiabilities");

                    b.Property<decimal>("TotalEquity");

                    b.Property<decimal>("TotalLiabilities");

                    b.Property<decimal>("TotalLongTermDebt");

                    b.Property<decimal>("TotalReceivableNet");

                    b.Property<decimal>("TotalShortTermDebt");

                    b.Property<decimal>("TotalStockHolderEquity");

                    b.Property<decimal>("TreasuryStock");

                    b.HasKey("Id");

                    b.HasIndex("FinancialStatementId")
                        .IsUnique();

                    b.ToTable("BalanceSheets");
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.CashFlowStatement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AccountingChange");

                    b.Property<decimal>("CapitalExpanditure");

                    b.Property<decimal>("CashFromFinancingActivities");

                    b.Property<decimal>("CashFromInvestingActivities");

                    b.Property<decimal>("CashFromOperatingActivities");

                    b.Property<decimal>("CfDepreciationAmortization");

                    b.Property<decimal>("ChangeInAccountReceivable");

                    b.Property<decimal>("ChangeInCurrentAsset");

                    b.Property<decimal>("ChangeInCurrentLiabilities");

                    b.Property<decimal>("ChangeInInventories");

                    b.Property<decimal>("DividendsPaid");

                    b.Property<decimal>("EffectOfExchangeRateOnCash");

                    b.Property<int>("FinancialStatementId");

                    b.Property<decimal>("InvestmentChangesNet");

                    b.Property<decimal>("NetChangeInCash");

                    b.Property<decimal>("TotalAdjustments");

                    b.HasKey("Id");

                    b.HasIndex("FinancialStatementId")
                        .IsUnique();

                    b.ToTable("CashFlowStatements");
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.FinancialStatement", b =>
                {
                    b.Property<int>("FinancialStatementId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrencyCode");

                    b.Property<int>("FiscalQuarter");

                    b.Property<int>("FiscalYear");

                    b.Property<string>("FormType")
                        .IsRequired();

                    b.Property<DateTime>("PeriodEnd");

                    b.Property<decimal>("PeriodLength");

                    b.Property<string>("PeriodLengthCode");

                    b.Property<string>("PrimarySymbol")
                        .IsRequired();

                    b.Property<DateTime>("ReceivedDate");

                    b.Property<string>("Source");

                    b.Property<int>("StockId");

                    b.Property<decimal>("UsdConversionRate");

                    b.HasKey("FinancialStatementId");

                    b.HasIndex("StockId");

                    b.ToTable("FinancialStatements");
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.IncomeStatement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("CostOfRevenue");

                    b.Property<decimal>("DiscontinuedOperation");

                    b.Property<decimal>("Ebit");

                    b.Property<decimal>("EquityEarnings");

                    b.Property<decimal>("ExtraordinaryItems");

                    b.Property<int>("FinancialStatementId");

                    b.Property<decimal>("GrossProfit");

                    b.Property<decimal>("IncomeBeforeTaxes");

                    b.Property<decimal>("InterestExpense");

                    b.Property<decimal>("NetIncome");

                    b.Property<decimal>("NetIncomeApplicableToCommon");

                    b.Property<decimal>("ResearchDevelopementExpense");

                    b.Property<decimal>("SellingGeneralAdministrativeExpense");

                    b.Property<decimal>("TotalRevenue");

                    b.HasKey("Id");

                    b.HasIndex("FinancialStatementId")
                        .IsUnique();

                    b.ToTable("IncomeStatements");
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.MarketData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("LastPrice");

                    b.Property<decimal?>("MarketCapitalization");

                    b.Property<long?>("OutstandingShares");

                    b.Property<int>("QuotationUnit");

                    b.Property<int>("StockId");

                    b.HasKey("Id");

                    b.HasIndex("StockId")
                        .IsUnique();

                    b.ToTable("MarketDatas");
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.PiotroskiResult", b =>
                {
                    b.Property<int>("PiotroskiResultId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GlobalFScore");

                    b.Property<bool>("GoodAccrual");

                    b.Property<bool>("HigherAssetTurnover");

                    b.Property<bool>("HigherCurrentRatio");

                    b.Property<bool>("HigherGrossMargin");

                    b.Property<bool>("HigherReturnOnAssets");

                    b.Property<bool>("LowerLeverage");

                    b.Property<bool>("NoDilutionInShares");

                    b.Property<bool>("PositiveOperatingCashFlow");

                    b.Property<bool>("PositiveReturns");

                    b.Property<int>("PricingResultId");

                    b.Property<decimal>("SalesGrowth");

                    b.Property<int>("Year");

                    b.HasKey("PiotroskiResultId");

                    b.HasIndex("PricingResultId");

                    b.ToTable("PiotroskiResults");
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.PricingResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AverageAssetTurnover");

                    b.Property<decimal>("AverageCurrentRatio");

                    b.Property<decimal>("AverageFreeCashFlow");

                    b.Property<decimal>("AverageGrossMargin");

                    b.Property<decimal>("AverageLeverage");

                    b.Property<decimal>("AverageNetMargin");

                    b.Property<decimal>("AveragePiotroskiScore");

                    b.Property<decimal>("AveragePriceToFcfRatio");

                    b.Property<decimal>("AverageQuickRatio");

                    b.Property<decimal>("AverageRoa");

                    b.Property<decimal>("AverageRoe");

                    b.Property<decimal>("AverageRoic");

                    b.Property<decimal>("AverageSalesGrowth");

                    b.Property<decimal>("CurrentAssetTurnover");

                    b.Property<decimal>("CurrentCurrentRatio");

                    b.Property<decimal>("CurrentFreeCashFlow");

                    b.Property<decimal>("CurrentGrossMargin");

                    b.Property<decimal>("CurrentLeverage");

                    b.Property<decimal>("CurrentNetMargin");

                    b.Property<int>("CurrentPiotroskiScore");

                    b.Property<decimal>("CurrentPriceToFcfRatio");

                    b.Property<decimal>("CurrentQuickRatio");

                    b.Property<decimal>("CurrentRoa");

                    b.Property<decimal>("CurrentRoe");

                    b.Property<decimal>("CurrentRoic");

                    b.Property<decimal>("CurrentSalesGrowth");

                    b.Property<decimal>("DiscountOnNcav");

                    b.Property<decimal>("EnterpriseMultiple");

                    b.Property<decimal>("EnterpriseValue");

                    b.Property<decimal>("LeverageRatio");

                    b.Property<decimal>("NetCurrentAssetValue");

                    b.Property<decimal>("PriceEarningRatio");

                    b.Property<decimal>("PriceToBookRatio");

                    b.Property<decimal>("PriceToSalesRatio");

                    b.Property<int>("StockId");

                    b.Property<decimal>("TimesInterestCovered");

                    b.HasKey("Id");

                    b.HasIndex("StockId")
                        .IsUnique();

                    b.ToTable("PricingResults");
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AnnualStatementsReceivedDate");

                    b.Property<bool>("AnnualStatementsSuccess");

                    b.Property<string>("Country");

                    b.Property<string>("Currency")
                        .IsRequired();

                    b.Property<string>("Industry");

                    b.Property<DateTime>("MarketDataReceivedDate");

                    b.Property<bool>("MarketDataSuccess");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("QuarterlyStatementsReceivedDate");

                    b.Property<bool>("QuarterlyStatementsSuccess");

                    b.Property<string>("QuotationPlace");

                    b.Property<string>("Sector");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ValueScreener.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ValueScreener.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ValueScreener.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ValueScreener.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.AnnualResult", b =>
                {
                    b.HasOne("ValueScreener.Models.Domain.PricingResult", "PricingResult")
                        .WithMany("AnnualResults")
                        .HasForeignKey("PricingResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.BalanceSheet", b =>
                {
                    b.HasOne("ValueScreener.Models.Domain.FinancialStatement", "FinancialStatement")
                        .WithOne("BalanceSheet")
                        .HasForeignKey("ValueScreener.Models.Domain.BalanceSheet", "FinancialStatementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.CashFlowStatement", b =>
                {
                    b.HasOne("ValueScreener.Models.Domain.FinancialStatement", "FinancialStatement")
                        .WithOne("CashFlowStatement")
                        .HasForeignKey("ValueScreener.Models.Domain.CashFlowStatement", "FinancialStatementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.FinancialStatement", b =>
                {
                    b.HasOne("ValueScreener.Models.Domain.Stock", "Stock")
                        .WithMany("FinancialStatements")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.IncomeStatement", b =>
                {
                    b.HasOne("ValueScreener.Models.Domain.FinancialStatement", "FinancialStatement")
                        .WithOne("IncomeStatement")
                        .HasForeignKey("ValueScreener.Models.Domain.IncomeStatement", "FinancialStatementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.MarketData", b =>
                {
                    b.HasOne("ValueScreener.Models.Domain.Stock", "Stock")
                        .WithOne("MarketData")
                        .HasForeignKey("ValueScreener.Models.Domain.MarketData", "StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.PiotroskiResult", b =>
                {
                    b.HasOne("ValueScreener.Models.Domain.PricingResult", "PricingResult")
                        .WithMany("PiotroskiResults")
                        .HasForeignKey("PricingResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ValueScreener.Models.Domain.PricingResult", b =>
                {
                    b.HasOne("ValueScreener.Models.Domain.Stock", "Stock")
                        .WithOne("PricingResult")
                        .HasForeignKey("ValueScreener.Models.Domain.PricingResult", "StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
