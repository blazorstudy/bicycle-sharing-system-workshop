using BicycleSharingSystem.WebApi.Contexts;

namespace BicycleSharingSystem.WebApi;

/// <summary>
/// Program Entrypoint Support.
/// </summary>
public static class Program
{
    /// <summary>
    /// Program Entrypoint.
    /// </summary>
    /// <param name="args">args</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.AddServiceDefaults();
        builder.AddMySqlDbContext<BicycleSharingContext>("workshopdb");

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}