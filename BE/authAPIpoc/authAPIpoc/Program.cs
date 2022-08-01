using authAPIpoc;
using authAPIpoc.Database;
using authAPIpoc.Services;
using System.Configuration;

var MyAllowSpecificOrigins = "MyPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://10.0.2.2:8081","http://localhost:4200","http://localhost:3000","http://192.168.0.113:8081").AllowAnyHeader().AllowAnyMethod();
                      }
                      );
});

//Enable CORS
//builder.Services.AddCors(c =>
//{
//    c.AddPolicy(name: MyAllowSpecificOrigins, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var dbName = builder.Configuration.GetValue<string>(CONSTANTS.SETTINGS.DB.NAME);
var dbConnection = builder.Configuration.GetValue<string>(CONSTANTS.SETTINGS.DB.CONNECTION);
builder.Services.AddSingleton<IDbManager>(o => new DbManager(dbConnection, dbName));
builder.Services.AddSingleton<IUserService, UserService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
