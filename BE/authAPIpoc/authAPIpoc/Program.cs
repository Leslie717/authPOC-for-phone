using authAPIpoc;
using authAPIpoc.Database;
using authAPIpoc.Services;
using System.Configuration;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200", "http://10.0.2.2:8081", "http://192.168.0.113:8081");
                      });
});

//Enable CORS
//builder.Services.AddCors(c =>
//{
//    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
//     .AllowAnyHeader());
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var dbName = builder.Configuration.GetValue<string>(CONSTANTS.SETTINGS.DB.NAME);
var dbConnection = builder.Configuration.GetValue<string>(CONSTANTS.SETTINGS.DB.CONNECTION);
builder.Services.AddSingleton<IDbManager>(o => new DbManager(dbConnection,dbName));
builder.Services.AddSingleton<IUserService, UserService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
