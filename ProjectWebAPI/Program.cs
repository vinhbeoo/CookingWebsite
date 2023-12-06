using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;

//if (environment.IsDevelopment())
//{
//    builder.WebHost.UseUrls("https://www.jamesthew.com.vn");
//}
//else
//{
//    var url = builder.Configuration["Urls:" + environment.EnvironmentName];
//    if (!string.IsNullOrEmpty(url))
//    {
//        builder.WebHost.UseUrls(url);
//    }
//}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "https://localhost:7269/swagger/index.html"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
