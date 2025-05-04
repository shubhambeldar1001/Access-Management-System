using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Access_Management_System.Model
{
    public class AccessApproval
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AccessRequest")]
        public int AccessRequestId { get; set; }
        [ForeignKey("Approver")]
        public int ApproverId { get; set; }

        public AccessRequest AccessRequest { get; set; }
        public User Approver { get; set; }
    }
}
