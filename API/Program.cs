using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    builder.Services.AddDbContext<AppDbContext>(option => 
    {
        option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    builder.Services.AddCors(option =>{
        option.AddPolicy("CorsPolicy", policy => {
            policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:5173");
        });
    });

    builder.Services.AddMediatR(typeof(List.Handler));
    builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

}
var app = builder.Build();
{
    app.UseCors("CorsPolicy");
    app.UseHttpsRedirection();
    app.MapControllers();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedData(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error occurred while migrations are running");
    }
}

app.Run();
