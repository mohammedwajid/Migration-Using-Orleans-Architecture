using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Orleans.Configuration;
using Orleans.Serialization;
using Orleans.Serialization;
using Orleans.Serialization.Cloning;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var corsBuilder = new CorsPolicyBuilder();

// Orleans silo (in-memory, no clustering)
//builder.Host.UseOrleans(siloBuilder =>
//{
//    siloBuilder
//        .UseLocalhostClustering() // No ADO.NET clustering
//        .Configure<ClusterOptions>(options =>
//        {
//            options.ClusterId = "dev";
//            options.ServiceId = "NotificationAPI";
//        })
//        // .AddMemoryGrainStorage("PubSubStore"); // Required for streams

//        .AddAdoNetGrainStorage(
//                        "Default", // storage provider name
//                        options =>
//                        {
//                            options.Invariant = "Microsoft.Data.SqlClient"; // or Microsoft.Data.SqlClient
//                            options.ConnectionString = "Server=AZJ-L-3N4NTT3\\LOCALHOST;Database=Orleans;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=true";

//                        });
//        });

builder.UseOrleans(siloBuilder =>
{
    // Your existing Orleans setup
    siloBuilder.UseLocalhostClustering();
    siloBuilder.AddMemoryGrainStorage("Default");

    // Register custom copier for JsonElement here
    siloBuilder.Services.AddSingleton<IDeepCopier<JsonElement>, JsonElementCopier>();
});
corsBuilder.AllowAnyOrigin(); // For anyone access.
                              //corsBuilder.WithOrigins("http://localhost:4200");
corsBuilder.AllowAnyHeader();
//corsBuilder.AllowCredentials();
corsBuilder.AllowAnyMethod();
builder.Services.AddCors(options =>
{
    options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
});


// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Notification API",
        Version = "v1",
        Description = "Swagger UI for Orleans Notification API",
    });
});



var app = builder.Build();

// Enable Swagger in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification API v1");
        c.RoutePrefix = "swagger"; // URL -> /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("SiteCorsPolicy");
app.MapControllers();
app.Run();
