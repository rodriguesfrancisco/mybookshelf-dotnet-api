using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyBookshelf.API.Extensions;
using MyBookshelf.Application.Commands.CreateUser;
using MyBookshelf.Application.Security;
using MyBookshelf.Application.ViewModels;
using MyBookshelf.Core.Entities;
using MyBookshelf.Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookshelf.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {          
                    builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Configuration["MyBookshelf:JwtTokenIssuer"],
                        ValidAudience = Configuration["MyBookshelf:JwtTokenAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["MyBookshelf:JwtTokenSecret"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["tid"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddControllers();

            services.AddRepositories()
                .AddMediatR(typeof(CreateUser))
                .AddScoped<IJwtProvider, JwtProvider>();
            
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Book, BookViewModel>();
                cfg.CreateMap<UserBook, UserBookViewModel>();
                cfg.CreateMap<UserBook, UserBookResumedViewModel>();
                cfg.CreateMap<Status, StatusViewModel>();
                cfg.CreateMap<StatusHistory, StatusHistoryViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Author, AuthorViewModel>();
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
