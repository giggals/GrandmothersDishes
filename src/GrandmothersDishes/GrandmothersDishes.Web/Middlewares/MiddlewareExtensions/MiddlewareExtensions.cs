using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandmothersDishes.Web.Middlewares.MiddlewareExtensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedRolesMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedRolesMiddleware>();
        }
        
    }
}
