﻿using System;
using System.Security.Claims;
using AdjutantApi.Data;
using AdjutantApi.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdjutantApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentity<AdjutantUser, IdentityRole>()
                .AddEntityFrameworkStores<AdjutantContext>()
                .AddDefaultTokenProviders();
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddDiscord(discordOptions =>
                {
                    discordOptions.ClientId = Configuration["Authentication:Discord:AppId"];
                    discordOptions.ClientSecret = Configuration["Authentication:Discord:AppSecret"];
                    discordOptions.Scope.Add("guilds identify");
                    discordOptions.SaveTokens = true;
                    discordOptions.ClaimActions.MapJsonKey(ClaimTypes.Surname, "discriminator");
                    discordOptions.ClaimActions.MapJsonKey(ClaimTypes.UserData, "avatar");
                });
            
            // TODO: Put whole connection string into environment into configs.
            // reason is that connection strings can differ from prod-settings
            var connection = $"User ID={Environment.GetEnvironmentVariable("DB_USER")};Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};Host=localhost;Port=5432;Database=adjutant;";

            services.AddDbContext<AdjutantContext>
                (options =>
            {
                options.UseNpgsql(connection, b => { b.MigrationsAssembly("AdjutantApi"); });
            });
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

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}