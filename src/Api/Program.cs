using System.Text.Json.Serialization;
using HeuteApp.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });

builder.AddServices();
builder.AddCors();

if(builder.Environment.IsDevelopment())
{
    builder.AddSwagger();
}

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseAppMiddlewares();

if(app.Environment.IsDevelopment())
{
    app.UseAppSwagger();
}

app.MapControllers();

app.Run();