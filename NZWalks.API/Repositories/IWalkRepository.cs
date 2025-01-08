using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    // Interface for Walk repository
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetById(Guid id);
        Task<Walk> Create(Walk walk);
        Task<Walk?> Update(Guid id, Walk walk);
        Task<Walk?> Delete(Guid id);
    }

}
