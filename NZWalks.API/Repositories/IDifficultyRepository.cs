using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    // Interface for Difficulty repository
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();
        Task<Difficulty?> GetById(Guid id);
        Task<Difficulty> Create(Difficulty difficulty);
        Task<Difficulty?> Update(Guid id, Difficulty difficulty);
        Task<Difficulty?> Delete(Guid id);
    }

}
