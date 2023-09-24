using System.ComponentModel.DataAnnotations.Schema;

namespace isragram_csharp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
