using APITrassBank.Context;
using APITrassBank.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IContextDB, ContextDB>();
            builder.Services.AddTransient<IAuthUsersService, AuthUsersService>();
            builder.Services.AddTransient<IWorkerService, WorkerService>();
            builder.Services.AddTransient<ICustomerService, CustomersService>();
            builder.Services.AddTransient<IMessagesService, MessagesService>();
            builder.Services.AddTransient<ILoansService, LoansService>();
            builder.Services.AddTransient<IEnumsService, EnumsService>();
            builder.Services.AddTransient<IAccountsService, AccountsService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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