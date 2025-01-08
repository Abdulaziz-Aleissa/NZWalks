using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Repositories
{

    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

    
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber=1,int pageSize=1000)
        {
            var walks = dbContext.Walks.AsQueryable();

            //filtering

            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase)){
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                
            }

            //Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending)
                    {
                        walks = walks.OrderBy(x => x.Name);
                    }
                    else
                    {
                        walks = walks.OrderByDescending(x => x.Name);
                    }
                }
            }


            //Pagination
            // if page number is 1, then skip 0 results, if page number is 2, then skip 1000 results
            var skipResaults = (pageNumber - 1) * pageSize;





            return await walks.Skip(skipResaults).Take(pageSize).ToListAsync();
            //return await dbContext.Walks.ToListAsync();
        }

        public async Task<Walk?> GetById(Guid id)
        {
            return await dbContext.Walks.FindAsync(id);
        }


        public async Task<Walk> Create(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

   
        public async Task<Walk?> Update(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyID = walk.DifficultyID;
            existingWalk.RegionId = walk.RegionId;

            dbContext.Walks.Update(existingWalk);
            await dbContext.SaveChangesAsync();

            return existingWalk;
        }


        public async Task<Walk?> Delete(Guid id)
        {
            var existingWalk = await dbContext.Walks.FindAsync(id);
            if (existingWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
