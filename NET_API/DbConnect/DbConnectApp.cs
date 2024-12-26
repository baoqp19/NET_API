namespace NET_API.DbConnect;
using Microsoft.EntityFrameworkCore;
using NET_API.Models.Domain;

public class DbConnectApp : DbContext
{
        public DbConnectApp(DbContextOptions<DbConnectApp> options) : base(options) { }
    

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

}
