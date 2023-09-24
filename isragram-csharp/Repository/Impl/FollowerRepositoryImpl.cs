using DevagramCSharp.Models;
using isragram_csharp.Models;

namespace isragram_csharp.Repository.Impl
{
    public class FollowerRepositoryImpl : IFollowerRepository
    {
        private readonly IsragramContext _context;
        public FollowerRepositoryImpl (IsragramContext context)
        {
            _context = context;
        }

        public bool Follow(Follower follower)
        {
            try 
            {
                _context.Add(follower);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Unfollow(Follower follower)
        {
            try
            {
                _context.Remove(follower);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Follower GetFollower(int followerUserId, int followedUserId) 
        {
            return _context.Followers.FirstOrDefault(s => s.FollowerUserId == followerUserId && s.FollowedUserId == followedUserId); //why??
        }
    }
}
