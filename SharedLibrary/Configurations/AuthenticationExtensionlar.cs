using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configurations
{
    public static class AuthenticationExtensionlar
    {
        public static void AuthenticationExtension(this IServiceCollection services,CustomTokenOption tokenOption)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                opts.TokenValidationParameters = new()
                {
                    ValidIssuer = tokenOption.Issuer,
                    //ValidAudiences=tokenOption.Audience,
                    ValidAudience = tokenOption.Audience[0],
                    IssuerSigningKey = SignService.GetSimetrikKey(tokenOption.SecurityKey),

                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew=TimeSpan.Zero
                };
            });
        }
    }
}
