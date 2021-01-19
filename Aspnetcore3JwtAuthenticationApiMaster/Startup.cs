using BLL;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
         
            services.AddDistributedMemoryCache();
            services.AddSession( );
            services.AddMvc();
            //services.AddDbContextPool<EmployeeDBContext>(opt =>
            //{
            //   opt.UseLazyLoadingProxies()
            //    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //});

            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //var appSettingsSection = Configuration.GetSection("AppSettings");
 
            //var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //     //   ValidateLifetime = true,
            //        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});

            // configure DI for application services
            services.AddDbContext<EmployeeDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // services.AddSingleton<ITokenRefresher, TokenRefresher>();
            services.AddHttpContextAccessor();

            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddScoped<IJWTAuthenticationManager, JWTAuthenticationManager>();
            services.AddScoped<ITokenRefresher, TokenRefresher>();

            //services.AddScoped<IEmployeeSerivce, EmployeeSerivce>();
            services.AddScoped<IUserBs, UserBs>();
            services.AddScoped<IEmployeeBs, EmployeeBs>();
            services.AddScoped<IEmployeeDb, EmployeeDb>();
            services.AddScoped<IUserDb, UserDb>();
            
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            //services.AddMvc(opt =>
            //{

            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    opt.Filters.Add(new AuthorizeFilter(policy));
            //});

        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
           
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSession();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            // test comment

            app.UseEndpoints(x => x.MapControllers());
          

        }
    }
}
