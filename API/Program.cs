using APITrassBank.Context;
using APITrassBank.Models;
using APITrassBank.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace APITrassBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            // Add services to the container.

            builder.Services.AddControllers(opciones =>
            {
                opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddDbContext<ContextDB>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            })
            .AddIdentity<IdentityUser, IdentityRole>(opciones =>
            {
                opciones.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<ContextDB>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>();
            builder.Services
                            .AddHttpContextAccessor()
                            .AddAuthorization()
                            .AddAuthentication(options =>
                            {
                                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                            })
                            .AddJwtBearer(options =>
                            {
                                options.SaveToken = true;
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = builder.Configuration.GetSection("Jwt")["Issuer"],
                                    ValidAudience = builder.Configuration.GetSection("Jwt")["Audience"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt")["Key"]))
                                };
                            });
            builder.Services.AddAutoMapper(typeof(Program));
            //builder.Services.AddDbContext<ContextDB>(opciones =>
            //opciones.UseMySql(
            //    builder.Configuration.GetConnectionString("MariaDbConnectionString"),
            //    new MariaDbServerVersion(new Version(10, 3, 27))
            //    ));
            builder.Services
                .AddTransient<IUserService, UserService>()
                .AddTransient<IContextDB, ContextDB>()
                .AddTransient<IAuthUsersService, AuthUsersService>()
                .AddTransient<IWorkerService, WorkerService>()
                .AddTransient<ICustomerService, CustomersService>()
                .AddTransient<IMessagesService, MessagesService>()
                .AddTransient<ILoansService, LoansService>()
                .AddTransient<IEnumsService, EnumsService>()
                .AddTransient<IAccountsService, AccountsService>()
                .AddTransient<IScoringsService,ScoringsService>()
                .AddTransient<IResourcesService, ResourcesService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.AddSecurityDefinition(JwtAuthenticationDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Name = JwtAuthenticationDefaults.HeaderName, // Authorization
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtAuthenticationDefaults.AuthenticationScheme
                                }
                            },
                            new List<string>()
                        }
                    });
                });

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


            app.MapControllers();

            app.Run();
        }
    }
}