using AdquisicionesAPI.Models;
using AdquisicionesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AdquisicionService>();
builder.Services.AddSingleton<AdquisicionLogService>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");


app.MapGet("/adquisicionesLog/{id}", (int id, AdquisicionLogService service) =>
{
	var adquisicionLog = service.Get(id);
	return adquisicionLog is not null ? Results.Ok(adquisicionLog) : Results.NotFound();
})
.WithName("GetAdquisicionLogById")
.WithOpenApi();

app.MapGet("/adquisiciones", (AdquisicionService service) =>
{
	return service.GetAll();
})
.WithName("GetAdquisiciones")
.WithOpenApi();

app.MapGet("/adquisiciones/{id}", (int id, AdquisicionService service) =>
{
	var adquisicion = service.Get(id);
	return adquisicion is not null ? Results.Ok(adquisicion) : Results.NotFound();
})
.WithName("GetAdquisicionById")
.WithOpenApi();

app.MapPost("/adquisiciones", (Adquisicion adquisicion, AdquisicionService service) =>
{
	service.Add(adquisicion);
	return Results.Created($"/adquisiciones/{adquisicion.Id}", adquisicion);
})
.WithName("CreateAdquisicion")
.WithOpenApi();

app.MapPut("/adquisiciones/{id}", (int id, Adquisicion adquisicion, AdquisicionService service) =>
{

	var existingAdquisicion = service.Get(id);
	if (existingAdquisicion is null)
	{
		return Results.NotFound();
	}
	adquisicion.Id = existingAdquisicion.Id;
	service.Update(adquisicion);
	return Results.Ok(adquisicion);
})
.WithName("UpdateAdquisicion")
.WithOpenApi();

app.MapDelete("/adquisiciones/{id}", (int id, AdquisicionService service) =>
{
	var existingAdquisicion = service.Get(id);
	if (existingAdquisicion is null)
	{
		return Results.NotFound();
	}

	service.Delete(id);
	return Results.Ok("Registro eliminado.");
})
.WithName("DeleteAdquisicion")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
