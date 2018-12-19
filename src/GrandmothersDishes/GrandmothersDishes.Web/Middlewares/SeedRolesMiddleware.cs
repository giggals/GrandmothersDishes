using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandmothersDishes.Data;
using GrandmothersDishes.Models;
using Microsoft.AspNetCore.Identity;

namespace GrandmothersDishes.Web.Middlewares
{
    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            GrandmothersDishesDbContext dbContext,
            UserManager<GrandMothersUser> usermanager,
            RoleManager<IdentityRole> roleManager)
        {
            if (!dbContext.Roles.Any())
            {
                await this.SeedRoles(usermanager, roleManager);
            }

            if (dbContext.Users.Count() == 1)
            {
                var firstUser = usermanager.Users.FirstOrDefault();
                await usermanager.AddToRoleAsync(firstUser, "Administrator");
            }
            else
            {
                var users = usermanager.Users.ToList();

                foreach (var user in users)
                {
                    if (!usermanager.IsInRoleAsync(user, "Administrator").Result && !usermanager.IsInRoleAsync(user , "User").Result)
                    {
                        await usermanager.AddToRoleAsync(user, "User");
                    }
                }
            }

            await this.next(context);
        }
        
        private async Task SeedRoles(
            UserManager<GrandMothersUser> usermanager,
            RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.CreateAsync(new IdentityRole("Administrator"));
            var userRole = await roleManager.CreateAsync(new IdentityRole("User"));

        }
        
    }
}

