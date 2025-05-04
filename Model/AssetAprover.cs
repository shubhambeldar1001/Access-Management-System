using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Access_Management_System.Model
{
    public class AssetAprover
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Asset")]
        public int AssetId { get; set; }

        [ForeignKey("Approver")]
        public int ApproverId { get; set; }
        public Asset Asset { get; set; }
        public User Approver { get; set; }
    }
}
