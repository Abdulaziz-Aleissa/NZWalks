using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext

    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<River> Rivers { get; set; }

        public DbSet<Image> Images { get; set; }  





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed the data for the difficulties table
            // easy, medium, hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("b64726e2-8dbd-4946-98fb-5f35a8457642"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("d73ff9ab-c5b5-448d-bbff-4cc4aa87d371"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("0db49dba-19b2-429d-838b-a1f2b11035df"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for regions

            var regions = new List<Region>()
            {

                new Region
                {
                    Id = Guid.Parse("a03fd20e-8c43-4bae-be98-0c13b9dc4804"),
                    Code = "N",
                    Name = "Northland"
                },
                new Region
                {
                    Id = Guid.Parse("6b03355f-0734-4ea9-a690-919ac931f934"),
                    Code = "A",
                    Name = "Auckland"
                },
                new Region
                {
                    Id = Guid.Parse("10334b53-ba3c-4cba-8346-9caaee60fd20"),
                    Code = "W",
                    Name = "Waikato"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);



        }
    }
}
