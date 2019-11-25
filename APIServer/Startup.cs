using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Batch;
using Microsoft.EntityFrameworkCore;

using IdxSistemas.Models;
using IdxSistemas.AppRepository.Context;
using IdxSistemas.AppServer.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Claims;
using Newtonsoft.Json.Serialization;

using IdxSistemas.AppServer.OData.Edm;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace IdxSistemas.AppServer
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
            services.Configure<KestrelServerOptions>(
                Configuration.GetSection("Kestrel")
            );

            services.AddDbContext<DataContext>(
                options => 
                    options.UseSqlServer( 
                        Configuration.GetConnectionString("sage_db"), 
                        b => b.MigrationsAssembly("APIServer") 
                    ) 
            );
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = "Cookies";
                x.DefaultChallengeScheme = "Cookies";
            })
            .AddCookie("Cookies", options => {
                options.Cookie.Name = "SIAGRO_SSO";
                options.Cookie.SameSite = SameSiteMode.None;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext => 
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });
            
            services.AddCors();
            services.AddOData();

            services.AddAntiforgery();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
               
                config.Filters.Add(new AuthorizeFilter(policy));
                

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());            


            services.AddAuthorization(options =>
            {
                options.AddPolicy("ALL", policy => policy.RequireClaim(ClaimTypes.Role, "ALL"));
            });
                  
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.Use(async (context, next) =>
            {
                context.Request.Headers["CookieBackup"] = context.Request.Headers["Cookie"];
                context.Request.Headers.Remove("Cookie");
                await next();
            });   // Exception when handle odata batch request when is has cookie #1620

            app.UseODataBatching();

            app.Use(async (context, next) =>
            {
                context.Request.Headers["Cookie"] = context.Request.Headers["CookieBackup"];
                context.Request.Headers.Remove("CookieBackup");
                await next();
            });   // Exception when handle odata batch request when is has cookie #1620

            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(
                b=>{
                    b.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
                    b.MapODataServiceRoute("default","service.svc", DefaultEdmModel.GetEdmModel(), new DefaultODataBatchHandler());
                    //b.MapODataServiceRoute("pedidoVenda","pedido_venda.svc", PedidoVendaEdmModel.GetEdmModel(), new DefaultODataBatchHandler());
                    b.EnableDependencyInjection();    
                }
            );
        }

        
    }
}
