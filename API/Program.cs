using API.Data;
using API.Extensions;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        var builder = WebApplication.CreateBuilder(args);

        var config = builder.Configuration;
        var services = builder.Services;

        // Add services to the container.
        services.AddApplicationServices(config);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins,
            builder =>
            {
                builder
                    .WithOrigins(config.GetSection("Cors")["AppClient"])
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }
        ));
        services.AddIdentityServices(config);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}