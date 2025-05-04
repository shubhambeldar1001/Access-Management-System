//using Access_Management_System.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Access_Management_System.Model
{
    public class AccessRequest
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Asset")]
        public int AssetId { get; set; }
        public AccessRequestSatus Status { get; set; }
        public ICollection<AccessApproval> Approvals { get; set; } = new List<AccessApproval>();
        public User User { get; set; }
        public Asset Asset { get; set; }
    }
}
