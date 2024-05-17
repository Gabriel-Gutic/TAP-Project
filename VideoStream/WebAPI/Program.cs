
using DataAccessLayer.Repository;
using DataAccessLayer;
using BusinessLayer.Contracts;
using BusinessLayer.Services;
using WebAPI.File;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using BusinessLayer.Notifications.Manager;
using BusinessLayer.Logger;
using BusinessLayer.Notifications.Factory;
using BusinessLayer.RandomGenerator;
using BusinessLayer.VideoSelector;


namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("Default", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            //Repository
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Random
            builder.Services.AddScoped<IRandomGenerator, RandomGenerator>();
            // Logger
            builder.Services.AddScoped<IAppLogger, AppLogger>();

            //Services
            builder.Services.AddScoped<IVideoSelectorFactory, VideoSelectorFactory>();
            builder.Services.AddScoped<IVideoSelectorService, VideoSelectorService>();

            builder.Services.AddScoped<IVideoCategoryService, VideoCategoryService>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IVideoService, VideoService>();
			builder.Services.AddScoped<ICommentService, CommentService>();
			builder.Services.AddScoped<IViewService, ViewService>();
			builder.Services.AddScoped<IFeedbackService, FeedbackService>();
			builder.Services.AddScoped<ISubscriberService, SubscriberService>();
			
            builder.Services.AddScoped<INotificationService, NotificationService>();
			builder.Services.AddScoped<INotificationFactory, NotificationFactory>();
            builder.Services.AddScoped<INotificationManager, NotificationManager>();

            // Scopes
            builder.Services.AddScoped<IFileManager, FileManager>();
			builder.Services.AddScoped<IPasswordHandler, PasswordHandler>();

			//Database
			builder.Services.AddDbContext<MyDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("Default");
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
