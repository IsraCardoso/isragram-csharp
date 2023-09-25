using Microsoft.AspNetCore.Mvc;

namespace isragram_csharp.Dtos
{
    public class CommentRequestDto 
    {
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}
