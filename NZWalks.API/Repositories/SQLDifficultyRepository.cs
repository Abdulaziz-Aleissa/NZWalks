using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Repositories { 

    public class SQLDifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLDifficultyRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

 
        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await dbContext.Difficulties.ToListAsync();
        }


        public async Task<Difficulty?> GetById(Guid id)
        {
            return await dbContext.Difficulties.FindAsync(id);
        }


        public async Task<Difficulty> Create(Difficulty difficulty)
        {
            await dbContext.Difficulties.AddAsync(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }


        public async Task<Difficulty?> Update(Guid id, Difficulty difficulty)
        {
            var existingDifficulty = await dbContext.Difficulties.FindAsync(id);
            if (existingDifficulty == null)
            {
                return null;
            }

            existingDifficulty.Name = difficulty.Name;

            dbContext.Difficulties.Update(existingDifficulty);
            await dbContext.SaveChangesAsync();

            return existingDifficulty;
        }


        public async Task<Difficulty?> Delete(Guid id)
        {
            var existingDifficulty = await dbContext.Difficulties.FindAsync(id);
            if (existingDifficulty == null)
            {
                return null;
            }

            dbContext.Difficulties.Remove(existingDifficulty);
            await dbContext.SaveChangesAsync();

            return existingDifficulty;
        }
    }
}
