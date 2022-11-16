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
builder.Services.AddMapperConfiguration();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
   .WithOrigins(builder.Configuration.GetConnectionString("CorsLocation"), "http://localhost:4200")
   .AllowAnyMethod()
   .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
