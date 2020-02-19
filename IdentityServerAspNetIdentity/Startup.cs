using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerAspNetIdentity.Contexts;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServerAspNetIdentity
{
    public class Startup
    {
        IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<IdentityDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserModel, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDBContext>()
                .AddDefaultTokenProviders();

            var serverBuilder = services.AddIdentityServer()
                .AddConfigurationStore(storeOptions => storeOptions.ConfigureDbContext = builder =>
                    builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddOperationalStore(storeOptions => storeOptions.ConfigureDbContext = builder =>
                    builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddAspNetIdentity<UserModel>();

            serverBuilder.AddDeveloperSigningCredential();
            
                services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            FillDB(app);

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }

        public void FillDB(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                //if (!context.Clients.Any())
                //{
                //    foreach (var client in Config.Clients)
                //    {
                //        context.Clients.Add(client.ToEntity());
                //    }

                //    context.SaveChanges();
                //}

                //if (!context.IdentityResources.Any())
                //{
                //    foreach (var identityResource in Config.IdentityResources)
                //    {
                //        context.IdentityResources.Add(identityResource.ToEntity());
                //    }

                //    context.SaveChanges();
                //}

                //if (!context.ApiResources.Any())
                //{
                //    foreach (var apiResource in Config.APIs)
                //    {
                //        context.ApiResources.Add(apiResource.ToEntity());
                //    }

                //    context.SaveChanges();
                //}

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<UserModel>>();

                foreach (var testUser in Config.GetUsers)
                {
                    var user = userManager.FindByNameAsync(testUser.Username).Result;
                    if (user == null)
                    {
                        user = new UserModel
                        {
                            UserName = testUser.Username
                        };
                        var result = userManager.CreateAsync(user, testUser.Password).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }
                }

            }
        }
    }
}
