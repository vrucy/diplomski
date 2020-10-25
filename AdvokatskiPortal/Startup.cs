using CraftmanPortal.Data;
using CraftmanPortal.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CraftmanPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtFirstName. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod().AllowAnyHeader()
                .Build();
            }));
            services.AddDbContext<PortalOfCraftsmanDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("PortalOfCraftsmanDbContext")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<PortalOfCraftsmanDbContext>();
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RegularCraftman", policy => policy.RequireClaim("RegularCraftman"));
                options.AddPolicy("AdminCraftman", policy => policy.RequireClaim("AdminCraftman","RegularCraftman"));
                options.AddPolicy("AdminCraftman", policy => policy.RequireAssertion(context => (
                    context.User.HasClaim(c => (
                        c.Type == "AdminCraftman" || c.Type == "RegularCraftman"
                        )))));
                

                options.AddPolicy("RegularUser", policy => policy.RequireClaim("RegularUser"));
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtFirstName. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseCors("Cors");
           // app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSpaStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
