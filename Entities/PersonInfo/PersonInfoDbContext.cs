using Microsoft.EntityFrameworkCore;

namespace Adaca_Challenge.Entities.PersonInfo
{
    public class PersonInfoDbContext : DbContext
    {
        public PersonInfoDbContext(DbContextOptions<PersonInfoDbContext> options)
            : base(options)
        {

        }

        public DbSet<PersonInfoModel> PersonInfo { get; set; }
    }
}