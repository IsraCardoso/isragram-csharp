using isragram_csharp.Models;

namespace isragram_csharp.Repository
{
    public interface IFollowerRepository
    {
        public bool Follow(Follower follower);
        public bool Unfollow(Follower follower);
        public Follower GetFollower(int followerUserId, int followedUserId);
    }
}
