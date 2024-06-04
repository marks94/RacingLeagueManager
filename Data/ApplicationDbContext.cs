using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RacingLeagueManager.Models;

namespace RacingLeagueManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<League> Leagues { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
