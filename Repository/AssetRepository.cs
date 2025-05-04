using Access_Management_System.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Access_Management_System.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AccessDbContext _context;

        public AssetRepository(AccessDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ApproveRequestAsync(int requestId, int approverId)
        {
            try
            {
                var request = await _context.AccessRequests.FirstOrDefaultAsync(r => r.id == requestId);
                if (request == null || request.UserId == approverId || request.Approvals.Any(a => a.ApproverId == approverId))
                    return false;

                request.Approvals.Add(
                    new AccessApproval { AccessRequestId = requestId, ApproverId = approverId });

                if (request.Approvals.Count >= 2)
                {
                    request.Status = AccessRequestSatus.Approved;
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.id == request.UserId);
                    var asset = await _context.Assets.FirstOrDefaultAsync(a => a.id == request.AssetId);
                    if (asset != null && user != null)
                    {
                        asset.AssetUsers.Add(user);
                        await _context.SaveChangesAsync();
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while  Approving  Request: {ex.Message}");
                return false;
            }
        }

        public async Task<Asset> CreateAssetAsync(Asset asset, List<int> approverIds)
        {
            try
            {
                _context.Assets.Add(asset);
                foreach (var approverId in approverIds)
                {
                    _context.AssetApprovers.Add(new AssetAprover
                    {
                        AssetId = asset.id,
                        ApproverId = approverId
                    });
                }
                await _context.SaveChangesAsync();
                return asset;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating assets: {ex.Message}");
            }
            return asset;
        }

        public async Task<AccessRequest> CreateRequestAsync(AccessRequest request)
        {
            _context.AccessRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<User> createUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<Asset>> GetAllAssetsAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset> GetAssetById(int id)
        {
            return await _context.Assets.FirstOrDefaultAsync(a => a.id == id);
        }

        public async Task<List<Asset>> GetAssetsForUserAsync(int userId)
        {
            return await _context.Assets
                .Where(a => a.AssetUsers.Any(u => u.id == userId))
                .ToListAsync();
        }

        public async Task<List<AccessRequest>> GetRequestsForApproverAsync(int approverId)
        {
            return await _context.AccessRequests
                .Where(r => !r.Approvals.Any(a => a.ApproverId == approverId) &&
                            r.UserId != approverId &&
                            r.Status != AccessRequestSatus.Approved)
                .ToListAsync();
        }

        public async Task<List<User>> GetUsersWithAccessToAssetAsync(int assetId)
        {
            return await _context.Assets
                .Where(a => a.id == assetId)
                .SelectMany(s => s.AssetUsers)
                .ToListAsync();
        }
    }
}
