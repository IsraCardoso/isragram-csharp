using System.ComponentModel.DataAnnotations.Schema;

namespace isragram_csharp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
