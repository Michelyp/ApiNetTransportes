using ApiNetTransportes.Data;
using ApiNetTransportes.Helpers;
using ApiNetTransportes.Repositories;
using Microsoft.EntityFrameworkCore;
using NSwag.Generation.Processors.Security;
using NSwag;
using Swashbuckle.AspNetCore.SwaggerUI;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddProblemDetails();

// Add services to the container..
string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryCoches>();
builder.Services.AddTransient<HelperUploadFiles>();
builder.Services.AddTransient<HelperPathProvider>();
builder.Services.AddDbContext<TransportesContext>(options=> options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// REGISTRAMOS SWAGGER COMO SERVICIO


builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api Transportes";
    document.Description = "Api Proyecto transportes";
    // CONFIGURAMOS LA SEGURIDAD JWT PARA SWAGGER,
    // PERMITE AÑADIR EL TOKEN JWT A LA CABECERA.
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
        }
    );
    document.OperationProcessors.Add(
    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});


HelperToken helper =   new HelperToken(builder.Configuration);
builder.Services.AddAuthentication(helper.GetAuthenticationOptions()).AddJwtBearer(helper.GetJwtOptions());
builder.Services.AddSingleton<HelperToken>(helper);
builder.Services.AddControllers();
var app = builder.Build();
//app.UseProblemDetails();
app.UseOpenApi();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwaggerUI(
    options=>
    {
        options.InjectStylesheet("/css/material3x.css");
        options.SwaggerEndpoint(url: "/swagger/v1/swagger.json"
        , name: "Api OAuth Transportes");
        options.RoutePrefix = "";
        options.DocExpansion(DocExpansion.None);
    });

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
