using System;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ValueScreener.Data;
using ValueScreener.Models;
using ValueScreener.Models.Domain;
using ValueScreener.Services;
using ValueScreener.Services.Batch;
using ValueScreener.Services.FinancialStatements;
using ValueScreener.Services.MarketData;
using ValueScreener.Services.Valuation;

namespace ValueScreener
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddHangfire(config =>
                config.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IMarketDataService, MarketDataService>();
            services.AddTransient<IStockMarketDataUpdater, StockMarketDataUpdater>();
            services.AddTransient<IFinancialStatementService>(s => new EdgarFinancialStatementService(Configuration["Services:EdgarApiKey"]));
            services.AddTransient<IStockEvaluator, StockEvaluator>();
            services.AddTransient<IValuationHintAnalyzer, ValuationHintAnalyzer>();
            services.AddTransient<IFinancialStatementUpdater, FinancialStatementUpdater>();
            services.AddTransient<IApplicationBatchService, ApplicationBatchService>();
            services.AddMvc();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();



            app.UseAuthentication();

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            RecurringJob.AddOrUpdate<IApplicationBatchService>("MarketDataRetrieval",service => service.RetrieveAllMArketData() , "0 */4 * * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval1", service => service.RetrieveAllFinancialStatements(1,StatementFrequency.Annual), "30 11 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval2", service => service.RetrieveAllFinancialStatements(2, StatementFrequency.Annual), "35 11 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval3", service => service.RetrieveAllFinancialStatements(3, StatementFrequency.Annual), "40 11 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval4", service => service.RetrieveAllFinancialStatements(4, StatementFrequency.Annual), "45 11 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval5", service => service.RetrieveAllFinancialStatements(5, StatementFrequency.Annual), "50 11 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval6", service => service.RetrieveAllFinancialStatements(6, StatementFrequency.Annual), "55 11 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval7", service => service.RetrieveAllFinancialStatements(7, StatementFrequency.Annual), "0 12 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval8", service => service.RetrieveAllFinancialStatements(8, StatementFrequency.Annual), "05 12 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval9", service => service.RetrieveAllFinancialStatements(9, StatementFrequency.Annual), "10 12 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("AnnualStatementRetrieval10", service => service.RetrieveAllFinancialStatements(10, StatementFrequency.Annual), "15 12 */1 * *");

            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrStatementRetrieval1", service => service.RetrieveAllFinancialStatements(1, StatementFrequency.Quarterly), "30 13 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrStatementRetrieval2", service => service.RetrieveAllFinancialStatements(2, StatementFrequency.Quarterly), "35 13 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrStatementRetrieval3", service => service.RetrieveAllFinancialStatements(3, StatementFrequency.Quarterly), "40 13 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrStatementRetrieval4", service => service.RetrieveAllFinancialStatements(4, StatementFrequency.Quarterly), "45 13 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrStatementRetrieval5", service => service.RetrieveAllFinancialStatements(5, StatementFrequency.Quarterly), "50 13 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrStatementRetrieval6", service => service.RetrieveAllFinancialStatements(6, StatementFrequency.Quarterly), "55 13 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrlStatementRetrieval7", service => service.RetrieveAllFinancialStatements(7, StatementFrequency.Quarterly), "0 14 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrStatementRetrieval8", service => service.RetrieveAllFinancialStatements(8, StatementFrequency.Quarterly), "05 14 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrStatementRetrieval9", service => service.RetrieveAllFinancialStatements(9, StatementFrequency.Quarterly), "10 14 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("QtrStatementRetrieval10", service => service.RetrieveAllFinancialStatements(10, StatementFrequency.Quarterly), "15 14 */1 * *");
            RecurringJob.AddOrUpdate<IApplicationBatchService>("EvaluateAllStocks", service => service.ReevaluateAllStocks(), "15 17 */1 * *");

        }
    }
}
