using Access_Management_System.Model;
using Access_Management_System.Repository;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private readonly IAssetRepository _assetRepository;

    public AssetController(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }
    public class CreateAssetDto
    {
        public Asset Asset { get; set; }
        public List<int> ApproverIds { get; set; }
    }

    [HttpPost("request-access")]
    public async Task<IActionResult> RequestAccess([FromBody] AccessRequest request)
    {
        var created = await _assetRepository.CreateRequestAsync(request);
        return Ok(created);
    }

    [HttpPost("create-asset")]
    public async Task<IActionResult> CreateAsset([FromBody] CreateAssetDto request)
    {
        var created = await _assetRepository.CreateAssetAsync(request.Asset, request.ApproverIds);
        return Ok(created);
    }

    [HttpPost("{id}/approve")]
    public async Task<IActionResult> ApproveRequest(int id, [FromQuery] int approverId)
    {
        var result = await _assetRepository.ApproveRequestAsync(id, approverId);
        return result ? Ok() : BadRequest("Invalid approval");
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var created = await _assetRepository.createUser(user);
        return Ok(created);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingRequests([FromQuery] int approverId)
    {
        var requests = await _assetRepository.GetRequestsForApproverAsync(approverId);
        return Ok(requests);
    }

    [HttpGet("{assetId}/users")]
    public async Task<IActionResult> GetUsersWithAccess(int assetId)
    {
        var asset = await _assetRepository.GetUsersWithAccessToAssetAsync(assetId);
        return Ok(asset);
    }

    [HttpGet("user/{userId}/assets")]
    public async Task<IActionResult> GetUserAssets(int userId)
    {
        var assets = await _assetRepository.GetAssetsForUserAsync(userId);
        return Ok(assets);
    }

    [HttpGet("all-assets")]
    public IActionResult GetAllAssetsAsync()
    {
        var asset = _assetRepository.GetAllAssetsAsync();
        return Ok(asset);
    }

    [HttpGet("{id}")]
    public IActionResult GetAssetById(int id)
    {
        var asset = _assetRepository.GetAssetById(id);
        return Ok(asset);
    }
}
