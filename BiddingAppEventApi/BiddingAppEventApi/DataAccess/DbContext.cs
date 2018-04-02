using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebAppEventApi.Models.Event;

namespace WebAppEventApi.DataAccess
{
    // DbContext for Entity Framework - The class name should map to the connection string in the web.config.
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext() : base("DbContext")
        {
            Database.SetInitializer<DbContext>(null);
        }

        public DbSet<EventEntity> Event { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}