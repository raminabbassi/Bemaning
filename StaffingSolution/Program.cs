using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using StaffingSolution.Models;
using StaffingSolution.Data;
using StaffingSolution.Repositories;
using StaffingSolution.Services;
using StaffingSolution.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using StaffingSolution.Controllers;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<JobController>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=RAMIN\\SQLEXPRESS;Database=Bemaning;Trusted_Connection=True;TrustServerCertificate=True;"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ContactRepository>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<StatisticsService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddHttpClient<JobTechService>();
builder.Services.Configure<CircuitOptions>(options => options.DetailedErrors = true);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(24);

    options.SlidingExpiration = false;
});

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/Imports/_Host");

app.Run();
