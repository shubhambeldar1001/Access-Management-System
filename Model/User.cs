using System.ComponentModel.DataAnnotations;

namespace Access_Management_System.Model
{
    public class User
    {
        [Key]
        public int id {  get; set; }
        public string name { get; set; }
        public ICollection<AccessRequest> Requests { get; set; }
        public ICollection<Asset> AssetsWithAccess { get; set; } = new List<Asset>();
    }
}
