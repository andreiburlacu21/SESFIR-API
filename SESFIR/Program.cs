using Microsoft.Extensions.FileProviders;
using SESFIR.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorConfiguration();
builder.Services.AddServiceConfiguration(builder.Configuration);
builder.Services.AddRepositoryConfiguration(builder.Configuration);
builder.Services.AddJWTConfiguration(builder.Configuration);
builder.Services.AddMapperConfiguration();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
   .WithOrigins(builder.Configuration.GetConnectionString("CorsLocation"), "https://localhost:4200", "exp://192.168.0.187:19000", "https://192.168.0.187:19000", "http://192.168.0.187:19000")
   .AllowAnyMethod()
   .AllowAnyHeader());

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

var imagesPath = Path.Combine(builder.Environment.ContentRootPath, "Images");
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

app.UseStaticFiles
    (
        new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images")),
            RequestPath = "/Images"
        }
    );

app.MapControllers();

app.Run();
