using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class Seed
    {
        public static async Task SeedUsers(AppDbContext context)
        {
            if (await context.User.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("../DAL/Json/UserSeedData.Json");

            var users = JsonSerializer.Deserialize<List<UserModel>>(userData);

            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();

                user.Username = user.Username.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.User.Add(user);
                context.SaveChanges();
            }
        }
    }
}
