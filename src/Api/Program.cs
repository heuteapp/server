using HeuteApp.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.AddServices();
builder.AddCors();

if(builder.Environment.IsDevelopment())
{
    builder.AddSwagger();
}

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAppCors();
app.UseAppMiddlewares();
app.UseAuthorization();

if(app.Environment.IsDevelopment())
{
    app.UseAppSwagger();
}

app.MapControllers();

app.Run();