using ApiBijou.Model.Panier;
using ApiBijou.Model.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlX.XDevAPI;
using Stripe;
using System;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services à l'application
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache(); // Ajoute un cache en mémoire distribué


var app = builder.Build();


// Configurer le pipeline de requêtes
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseAuthorization();

app.MapControllers();

app.Run();
