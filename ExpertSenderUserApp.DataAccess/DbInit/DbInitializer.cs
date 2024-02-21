using Bogus;
using Bogus.Extensions;
using ExpertSenderUserApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpertSenderUserApp.DataAccess.DbInit
{
    public class DbInitializer
    {
        public static async Task SeedData(ApplicationDbContext _context)
        {
            if (await _context.Users.AnyAsync()) return;

            var testUsers = new Faker<User>()
                .CustomInstantiator(f => new User())
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName().ClampLength(max: 50))
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName().ClampLength(max: 50))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName).ClampLength(max: 100))
                .RuleFor(u => u.Description, (f,u) => $"Hello, I'm {u.FirstName}. I work in {f.Company.CompanyName().ClampLength(max: 300)}")
                .RuleFor(u => u.DateCreated, DateTime.Now)
                .RuleFor(u => u.DateModified, DateTime.Now)
                .FinishWith((f, u) =>
                {
                    Console.WriteLine("User Created! Id={0}", u.Id);
                });

            var users = testUsers.Generate(10);
            await _context.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
    }
}
