using Microsoft.EntityFrameworkCore;
using MyGraphqlApp.Data;
using MyGraphqlApp.Interface;
using MyGraphqlApp.Schema;
using MyGraphqlApp.Service;
using MyGraphqlApp.Query;
using MyGraphqlApp.Mutation;
using MyGraphqlApp.config;
using MyGraphqlApp.Utils;
using MyGraphqlApp.Validators.UserValidator;
using MyGraphqlApp.Exception.GlobalException;




var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


// start of the adding the controller rest apis 
builder.Services.AddControllers();
// builder.Services.AddScoped<UsersController>();
// end of the adding rest apis here 


builder.Services.AddScoped<IUserService, UserService>();



// adding all the Query here 
builder.Services.AddScoped<RootQuery>();
builder.Services.AddScoped<UserQuery>();


// add all the mutations here 
builder.Services.AddScoped<RootMutation>();
builder.Services.AddScoped<UserMutation>();


// middlewares and jwtutils
builder.Services.AddScoped<JwtUtils>();
builder.Services.AddScoped<UserValidator>();








builder.Services
    .AddGraphQLServer()
   .AddApplicationSchema();

SecurityConfig.AddCorsPolicy(builder.Services);

var app = builder.Build();

app.UseCors("FrontendPolicy");

app.MapGraphQL();

// rest api map controller here 
app.MapControllers();
app.UseMiddleware<GlobalExceptionMiddleware>();


app.MapGet("/", () =>
{
    return "Hello Rajan ! ";
});

app.Run();