using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace NZWalks.API.Repositories
{

    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

   
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }


        public async Task<Region?> GetById(Guid id)
        {
            return await dbContext.Regions.FindAsync(id);
        }


        public async Task<Region> Create(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }


        public async Task<Region?> Update(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FindAsync(id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            dbContext.Regions.Update(existingRegion);
            await dbContext.SaveChangesAsync();

            return existingRegion;
        }

    
        public async Task<Region?> Delete(Guid id)
        {
            var existingRegion = await dbContext.Regions.FindAsync(id);
            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
