using System.ComponentModel.DataAnnotations;

namespace Access_Management_System.Model
{
    public class Asset
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> AssetUsers { get; set; } = new List<User>();

    }
}
