
namespace MyGraphqlApp.config
{
    public static class SecurityConfig
    {
        public static void AddCorsPolicy(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
        }
    }
}
