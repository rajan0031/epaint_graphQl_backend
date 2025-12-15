

var builder = WebApplication.CreateBuilder(args);


Env.Load();




// datanbas econnection 
DbConnections.AddDatabase(builder.Services, builder.Configuration);


// dependencies injections 
DependencyInjection.addDependencies(builder.Services);





SecurityConfig.AddCorsPolicy(builder.Services);

string? secretKey = Environment.GetEnvironmentVariable("JWT_KEY");
if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("Please make sure your env file have JWT_KEY  as a variable");
}
builder.Services.AddJwtAuthentication(secretKey!);

var app = builder.Build();

app.UseCors("FrontendPolicy");

// authorization and authentication goes here 
app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

// rest api map controller here 
app.MapControllers();
app.UseMiddleware<GlobalExceptionMiddleware>();


app.MapGet("/", () =>
{
    return "Hello Rajan ! ";
});

app.Run();