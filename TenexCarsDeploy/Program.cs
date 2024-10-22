using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TenexCars.DataAccess;
using TenexCars.Services;
using TenexCarsDeploy.Data.Models;
using TenexCarsDeploy.Helper;
using TenexCarsDeploy.Interfaces;
using TenexCarsDeploy.Services;

    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog
    var configuration = builder.Configuration;
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("serilog_logs\\Serilog.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Host.UseSerilog(); // Use Serilog as the logging provider

try
    {
        Log.Information("Starting web host");

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true; // changed from false to true
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8; // changed from 6 to 8
            options.Password.RequiredUniqueChars = 1;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IPhotoService, PhotoService>();
        builder.Services.AddScoped<IEmailService, EmailService>();

        // Configure CloudinarySettings
        builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

        // Register JWT configuration
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
        var jwtSettings = builder.Configuration.GetSection("JWT").Get<JwtSettings>();

        // Configure JWT authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        });

    var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    catch (Exception)
    {
        //Log.Fatal(ex, "Host terminated unexpectedly");
    }
    finally
    {
        //Log.CloseAndFlush();
    }
