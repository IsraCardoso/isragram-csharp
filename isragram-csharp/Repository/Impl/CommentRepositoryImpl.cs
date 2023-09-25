using DevagramCSharp.Models;
using isragram_csharp.Models;
using System.Drawing.Text;

namespace isragram_csharp.Repository.Impl
{
    public class CommentRepositoryImpl : ICommentRepository
    {
        private readonly IsragramContext _context;
        public CommentRepositoryImpl (IsragramContext context)
        {
            _context = context;
        }
        public void Comment(Comment comment)
        {
            _context.Add(comment);
            _context.SaveChanges();
        }
    }
}
