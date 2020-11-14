using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tabby_Docker.Data;
using System.IO;


namespace Tabby_Docker.Models
{
    public static class SeedData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                PrepDB(serviceScope.ServiceProvider.GetService<Tabby_DockerContext>());
            }
        }

        public static void PrepDB(Tabby_DockerContext context)
        {
            System.Console.WriteLine("Checking if needing to Prepare the Database...");
            System.Console.WriteLine("Migrating Database...");

            context.Database.Migrate();

            if (!context.Bookmark.Any())
            {
                System.Console.WriteLine("Adding data - seeding...");

                context.Bookmark.AddRange(
                    new Bookmark
                    {
                        Title = "Welcome to Tabby!",
                        Description = "Here you can delete this BookMark. Then feel free to install the extension for you preffered browser to add new ones!",
                        URL = "https://www.github.com",
                        SiteName = "Github",
                        DateAdded = DateTime.Now,
                        FaviconLoc = "/favicon.png"
                    });
                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Already have data - no seeding.");
            }
        }
    }
}
