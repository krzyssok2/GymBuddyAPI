using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymBuddyAPI
{
    public class GymBuddyContext : IdentityDbContext<IdentityUser>
    {
        public GymBuddyContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ApplyConfiguration(new CitiesConfiguration());
        }
    }
}
