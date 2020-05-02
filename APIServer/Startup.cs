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
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace IdxSistemas.AppServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(
                Configuration.GetSection("Kestrel")
            );

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "IdxTec";
            });

            //services.AddDbContext<DataContext>(
            //    options => 
            //        options.UseSqlServer( 
            //            Configuration.GetConnectionString("sage_db"), 
            //            b => b.MigrationsAssembly("APIServer") 
            //        ) 
            //);

            services.AddDbContextPool<DataContext>(options => {
                options.UseMySql(Configuration.GetConnectionString("sage_db"),
                        b => b.MigrationsAssembly("APIServer"));
            });

            var SecretKey = Encoding.ASCII.GetBytes
                    ("IdxTecKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");


            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    ValidateIssuer = true,
                    //Usually, this is your application base URL
                    ValidIssuer = "http://localhost:5000/",
                    ValidateAudience = true,
                    //Here, we are creating and using JWT within the same application.
                    //In this case, base URL is fine.
                    //If the JWT is created using a web service, then this would be the consumer URL.
                    ValidAudience = "http://localhost:5000/",
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
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


            services.AddAuthorization();
        }


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

            app.UseSession();

            app.Use(async (context, next) =>
            {
                context.Request.Headers["CookieBackup"] = context.Request.Headers["Cookie"];
                context.Request.Headers.Remove("Cookie");

                await next();
            });

            app.UseODataBatching();

            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    if (!context.Request.Headers.ContainsKey("Authorization"))
                    {
                        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                    }
                }

                context.Request.Headers["Cookie"] = context.Request.Headers["CookieBackup"];
                context.Request.Headers.Remove("CookieBackup");
                
                await next();
            });

            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(
                b=>{
                    b.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
                    b.MapODataServiceRoute("default","service.svc", DefaultEdmModel.GetEdmModel(), new DefaultODataBatchHandler());
                    b.EnableDependencyInjection();    
                }
            );
        }

        
    }
}
