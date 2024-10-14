using Customer.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> dbContextoptions) : base(dbContextoptions) { }

        public DbSet<CustomerDomain> Customers { get; set; }
    }
}
