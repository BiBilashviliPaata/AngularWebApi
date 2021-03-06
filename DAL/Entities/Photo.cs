using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public UserModel AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}