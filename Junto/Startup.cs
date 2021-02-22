namespace Junto
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Junto.Core.Context;
    using Junto.Core.Repository;
    using Junto.Core.Service;
    using Junto.Domain;
    using Junto.Domain.Service;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    public class Startup
    {
        internal static byte[] JwtSecret => Encoding.ASCII.GetBytes("juntosjwtsecretabc");
        internal static DateTime JwtExpires => DateTime.UtcNow.AddDays(7);
        internal static string Issuer => "Junto";
        internal static string Audience => "https://localhost:44370";
        internal static SigningCredentials SigningCredentials => new SigningCredentials(new SymmetricSecurityKey(JwtSecret), SecurityAlgorithms.HmacSha256Signature);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<JuntoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Junto")));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            var symmetricSecurityKey = new SymmetricSecurityKey(JwtSecret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = Issuer,

                ValidateAudience = false,
                ValidAudience = Audience,

                ValidateIssuerSigningKey = false,
                IssuerSigningKey = symmetricSecurityKey,

                RequireExpirationTime = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtConfig =>
            {
                jwtConfig.RequireHttpsMetadata = false;
                jwtConfig.SaveToken = true;
                jwtConfig.ClaimsIssuer = Issuer;
                jwtConfig.TokenValidationParameters = tokenValidationParameters;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
