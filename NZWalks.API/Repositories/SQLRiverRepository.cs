using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public class SQLRiverRepository : IRiverRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRiverRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<River>> GetAllAsync()
        {
            return await dbContext.Rivers.ToListAsync();
        }

        public async Task<River?> GetById(Guid id)
        {
            return await dbContext.Rivers.FindAsync(id);
        }

        public async Task<River> Create(River river)
        {
            await dbContext.Rivers.AddAsync(river);
            await dbContext.SaveChangesAsync();
            return river;
        }

        public async Task<River?> Update(Guid id, River river)
        {
            var existingRiver = await dbContext.Rivers.FindAsync(id);
            if (existingRiver == null)
            {
                return null;
            }

            existingRiver.Name = river.Name;
            existingRiver.Description = river.Description;

            dbContext.Rivers.Update(existingRiver);
            await dbContext.SaveChangesAsync();

            return existingRiver;
        }

        public async Task<River?> Delete(Guid id)
        {
            var existingRiver = await dbContext.Rivers.FindAsync(id);
            if (existingRiver == null)
            {
                return null;
            }

            dbContext.Rivers.Remove(existingRiver);
            await dbContext.SaveChangesAsync();

            return existingRiver;
        }
    }
}
