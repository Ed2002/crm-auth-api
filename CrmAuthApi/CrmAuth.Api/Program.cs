using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<System.Reflection.MethodBase>(() => new OpenApiSchema { Type = "string" });
});

var ApiPolicy = "apiauthpolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ApiPolicy,
        policy => {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(ApiPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
