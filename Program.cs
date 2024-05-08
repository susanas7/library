using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Library.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDBContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        // Aqu� puedes manejar cualquier error que pueda surgir, por ejemplo, utilizando un logger
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Un error ocurri� al aplicar las migraciones.");
    }
}


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseHttpsRedirection();



app.Run();

