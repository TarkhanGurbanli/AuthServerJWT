using AuthServer.SharedLibrary.Configurations;
using AuthServer.SharedLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

//bunu yukleyirik : Microsoft.AspNetCore.Authentication.JwtBe

namespace AuthServer.SharedLibrary.Extensions
{
    public static class CustomTokenAuth
    {
        public static void AddCustomTokenAuth(this IServiceCollection services, CustomTokenOption tokeOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {

                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,  // Issuer'?n do?rulu?unu kontrol etmek istiyorsan?z true kullan?n
                    ValidIssuer = tokeOptions.Issuer,  // Geçerli Issuer'? belirtin

                    ValidateAudience = true,  // Audience'un do?rulu?unu kontrol etmek istiyorsan?z true kullan?n
                    ValidAudience = tokeOptions.Audience[0],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SignService.GetSymMetricSecurityKey(tokeOptions.SecurityKey),

                    ValidateLifetime = true,

                    //yazmasaqda olar
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
