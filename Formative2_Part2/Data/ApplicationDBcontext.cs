using Formative2_Part2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Formative2_Part2.Data
{
    public class ApplicationDBcontext : DbContext
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options)
        {
        }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<EventEnrollment> EventEnrollments { get; set; }

        public DbSet<Result> Results { get; set; }


                     


    }
}
