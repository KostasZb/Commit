using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication3.Models
{
    public class CommitDB : DbContext
    {
        public CommitDB() : base("CommitDB")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CommitDB>());
        }

        public DbSet<CauseModel> Causes { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //preventing the pluralizing of table names 
            //source:https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}