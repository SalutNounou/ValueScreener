using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ValueScreener.Data;
using ValueScreener.Models;
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

            RecurringJob.AddOrUpdate<IApplicationBatchService>("Retrieve Everything", service=>service.RetrieveEverything(), "45 10 */1 * *" );
        }
    }
}
