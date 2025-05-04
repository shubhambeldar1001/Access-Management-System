using Access_Management_System.Model;
using System.Threading.Tasks;

namespace Access_Management_System.Repository
{
    public interface IAssetRepository
    {
        Task<Asset> CreateAssetAsync(Asset asset, List<int> approverIds);
        Task<List<Asset>> GetAllAssetsAsync();
        Task<Asset> GetAssetById(int id);
        Task<AccessRequest> CreateRequestAsync(AccessRequest request);
        Task<List<AccessRequest>> GetRequestsForApproverAsync(int approverId);
        Task<bool> ApproveRequestAsync(int requestId, int approverId);
        Task<List<User>> GetUsersWithAccessToAssetAsync(int assetId);
        Task<List<Asset>> GetAssetsForUserAsync(int userId);

        Task<User> createUser(User user);
    }
}
