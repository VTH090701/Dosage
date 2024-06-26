using Blazored.SessionStorage;
using Dosage.Data;
using Dosage.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System_EMS_1._0.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddRadzenComponents();
builder.Services.AddHttpClient();
builder.Services.AddTransient<ICatalogService, CatalogService>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<IApi, Api>();



//Config
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<ApiSettingsLogin>(builder.Configuration.GetSection("ApiSettingsLogin"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
