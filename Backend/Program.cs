﻿using API.Utilities;
using Application.Configurations;
using Application.Mapping;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Utilities;
using Infrastructure;
using Infrastructure.Configurations;
using Infrastructure.Repositories;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<MiniCourseraContext>(options =>
                options.UseSqlServer(connectionString)
                       .EnableSensitiveDataLogging());

            // Course
            builder.Services.AddScoped<CourseService>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IImageStorageService, LocalImageStorageService>();
            builder.Services.AddScoped<IVideoService, CloudinaryVideoService>();

            // User
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Enrollment
            builder.Services.AddScoped<EnrollmentService>();
            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            // EnrollmentProgress
            builder.Services.AddScoped<EnrollmentProgressService>();
            builder.Services.AddScoped<IEnrollmentProgressRepository, EnrollmentProgressRepository>();

            // CourseModule
            builder.Services.AddScoped<CourseModuleService>();
            builder.Services.AddScoped<ICourseModuleRepository, CourseModuleRepository>();

            // ModuleContent
            builder.Services.AddScoped<ModuleContentService>();
            builder.Services.AddScoped<IModuleContentRepository, ModuleContentRepository>();

            //RefreshToken
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            // UnitOfWork
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Register TokenService
            builder.Services.AddScoped<TokenService>();


            // Bind and register JwtSettings
            builder.Services
                .Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


            // AutoMapper
            builder.Services.AddSingleton<IMapper>(sp =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();
                });
                return config.CreateMapper();
            });

            // Add Controllers and Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ✅ CORS for Production
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://mini-coursera-frontend.vercel.app")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Add this if using cookies/auth
                });

                // Add a separate policy for development
                options.AddPolicy("DevelopmentCors", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // Your dev frontend
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Add this if using cookies/auth
                });
            });

            // ✅ Read the JWT Key explicitly for debugging
            string jwtKey = builder.Configuration["JwtSettings:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new Exception("❌ JWT Key is missing from configuration. Check appsettings.json or environment variables.");
            }
            Console.WriteLine($"✅ JWT Key loaded: {jwtKey.Substring(0, 5)}...");

            // 🔐 Add Authentication + Authorization
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero, // 🔥 No delay allowed after expiration

                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            builder.Services.AddAuthorization();



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Use the development CORS policy
                app.UseCors("DevelopmentCors");
            }
            else
            {
                // Use production CORS policy
                app.UseCors("AllowFrontend");
            }





            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers();
            app.MapGet("/", () => "Mini Coursera Backend is live!");

            app.Run();
        }
    }
}
