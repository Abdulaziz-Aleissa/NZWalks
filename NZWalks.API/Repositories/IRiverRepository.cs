using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRiverRepository
    {
        Task<List<River>> GetAllAsync();
        Task<River?> GetById(Guid id);
        Task<River> Create(River river);
        Task<River?> Update(Guid id, River river);
        Task<River?> Delete(Guid id);
    }
}

