using Application.Mapping;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Utilities;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

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
           .EnableSensitiveDataLogging()); // Optional: shows parameters

            //Course
            builder.Services.AddScoped<CourseService>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<CourseService>();
            builder.Services.AddScoped<IImageStorageService, LocalImageStorageService>();
            builder.Services.AddScoped<IVideoService, CloudinaryVideoService>();

            //User
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            //Enrollment
            builder.Services.AddScoped<EnrollmentService>();
            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            //EnrollmentProgress
            builder.Services.AddScoped<EnrollmentProgressService>();
            builder.Services.AddScoped<IEnrollmentProgressRepository, EnrollmentProgressRepository>();

            //CourseModle
            builder.Services.AddScoped<CourseModuleService>();
            builder.Services.AddScoped<ICourseModuleRepository, CourseModuleRepository>();

            //ModuleContent
            builder.Services.AddScoped<ModuleContentService>();
            builder.Services.AddScoped<IModuleContentRepository, ModuleContentRepository>();


            //UnitOfWork
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IMapper>(sp =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutoMapperProfile>();  // Your profile class
                });

                return config.CreateMapper();
            });


            // No named CORS policy needed
            var app = builder.Build();

            // Enable Swagger only in development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // ✅ Allow all origins in development
                app.UseCors(policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
