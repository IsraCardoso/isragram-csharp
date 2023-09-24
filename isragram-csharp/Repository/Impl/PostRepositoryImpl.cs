using DevagramCSharp.Models;
using isragram_csharp.Models;

namespace isragram_csharp.Repository.Impl
{
    public class PostRepositoryImpl : IPostRepository
    {
        private readonly IsragramContext _context;
        public PostRepositoryImpl( IsragramContext context)
        {
            _context = context;
        }

        public void Post(Post post)
        {
            _context.Add(post);
            _context.SaveChanges();
        }
    }

}
