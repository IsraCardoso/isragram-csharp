using System.ComponentModel.DataAnnotations.Schema;

namespace isragram_csharp.Models
{
    public class Follower
    {
        public int Id { get; set; }
        public int? FollowerUserId { get; set; }
        public int? FollowedUserId { get; set; }

        [ForeignKey("FollowerUserId")]
        public virtual User FollowerUser { get; private set; }

        [ForeignKey("FollowedUserId")]
        public virtual User FollowedUser { get; private set; }


    }
}
