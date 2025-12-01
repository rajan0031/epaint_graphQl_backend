using Microsoft.EntityFrameworkCore;
using MyGraphqlApp.Data;
using MyGraphqlApp.Interface;
using MyGraphqlApp.Schema;
using MyGraphqlApp.Service;



var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IUserService, UserService>();


builder.Services
    .AddGraphQLServer()
   .AddApplicationSchema();

   
var app = builder.Build();

app.MapGraphQL();

app.Run();