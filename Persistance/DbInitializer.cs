using Domain.Models.Identity;
using Domain.Models.Orders;
using Domain.Models.Product;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;
using System.Text.Json;

namespace Persistence
{
    public class DbInitializer
        (StoreDbContext _context ,
        StoreIdentityDbContext _identityContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager)
        : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            try
            {
                //dev
                if ((await _context.Database.GetPendingMigrationsAsync()).Any())
                    await _context.Database.MigrateAsync();

                if (!await _context.Set<ProductBrand>().AnyAsync())
                {
                    var data = await ReadFileAsync("brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                    if (brands is not null && brands.Any())
                    {
                        await _context.Set<ProductBrand>().AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }
                }

                if (!await _context.Set<ProductType>().AnyAsync())
                {
                    var data = await ReadFileAsync("types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(data);
                    if (types is not null && types.Any())
                    {
                        await _context.Set<ProductType>().AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }

                if (!await _context.Set<Product>().AnyAsync())
                {
                    var data = await ReadFileAsync("brands.json");
                    var types = JsonSerializer.Deserialize<List<Product>>(data);
                    if (types is not null && types.Any())
                    {
                        await _context.Set<Product>().AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }
                if (!await _context.Set<DeliveryMethod>().AnyAsync())
                {
                    var data = await ReadFileAsync("delivery.json");
                    var types = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);
                    if (types is not null && types.Any())
                    {
                        await _context.Set<DeliveryMethod>().AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Seeding :");
                Console.WriteLine(ex.ToString());
            }



        }
        private async static Task<string> ReadFileAsync(string relativePath)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Seeding");
            var fullPath = Path.Combine(basePath, relativePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Seed file not found: {fullPath}");

            return await File.ReadAllTextAsync(fullPath);
        }
        public async Task InitializeIdentityAsync()
        {
            //if ((await _identityContext.Database.GetPendingMigrationsAsync()).Any())
            //    await _identityContext.Database.MigrateAsync();

            if(! await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!await _userManager.Users.AnyAsync()) 
            {
                var superAdminUser = new ApplicationUser
                {
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "1234567890",
                };
                var adminUser = new ApplicationUser
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "1234567890",
                };

                await _userManager.CreateAsync(superAdminUser,"P@ssW0rd123");
                await _userManager.CreateAsync(adminUser, "P@ssW0rd123");

                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

    }
}

