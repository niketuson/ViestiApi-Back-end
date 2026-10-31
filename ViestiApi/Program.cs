
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ViestiApi.Middleware;
using ViestiApi.Models;
using ViestiApi.Repositories;
using ViestiApi.Services;

namespace ViestiApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MessageContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MessageDb")));
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                ("BasicAuthentication", null);
            //string test = builder.Configuration.GetConnectionString("MessageDb");
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ApiKeyMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            
        }
    }
}
