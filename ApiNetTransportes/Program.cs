using ApiNetTransportes.Data;
using ApiNetTransportes.Helpers;
using ApiNetTransportes.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

// Add services to the container..
string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryCoches>();
builder.Services.AddTransient<HelperUploadFiles>();
builder.Services.AddTransient<HelperPathProvider>();
builder.Services.AddTransient<HelperToken>();
builder.Services.AddDbContext<TransportesContext>(options=> options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title="Api Transportes",
        Description="Api con token"
    });
}
    );

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(
    options=>
    {
        options.SwaggerEndpoint(url: "/swagger/v1/swagger.json"
        , name: "Api OAuth Transportes");
        options.RoutePrefix = "";
    });
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
