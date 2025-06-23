using MenShop_Assignment.Datas;
using Microsoft.AspNetCore.Identity;

namespace MenShop_Assignment.Models.Account
{
    public static class DbInitializer
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@menshop.com";
            string adminPassword = "Admin@123";
            string roleName = "Admin";

            try
            {
                // Tạo role nếu chưa có
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (roleResult.Succeeded)
                        Console.WriteLine("Role 'Admin' created.");
                    else
                        Console.WriteLine("Failed to create role 'Admin'.");
                }

                // Kiểm tra xem user đã tồn tại chưa
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    var newAdmin = new User
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true,
                        FullName = "Administrator"
                    };

                    var createUserResult = await userManager.CreateAsync(newAdmin, adminPassword);

                    if (createUserResult.Succeeded)
                    {
                        Console.WriteLine("Admin user created successfully.");

                        var addToRoleResult = await userManager.AddToRoleAsync(newAdmin, roleName);
                        if (addToRoleResult.Succeeded)
                            Console.WriteLine("Admin user assigned to 'Admin' role.");
                        else
                        {
                            Console.WriteLine("Failed to assign admin user to 'Admin' role:");
                            foreach (var err in addToRoleResult.Errors)
                                Console.WriteLine($"- {err.Description}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to create admin user:");
                        foreach (var err in createUserResult.Errors)
                            Console.WriteLine($"- {err.Description}");
                    }
                }
                else
                {
                    Console.WriteLine("⚠Admin user already exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while seeding admin user:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
