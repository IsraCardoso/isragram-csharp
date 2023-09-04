namespace isragram_csharp.Dtos
{
    public class ErrorResponseDto
    {
        public int Status { get; set; }
        public string Description { get; set; }

        public List<string>? Errors { get; set; }

        public ErrorResponseDto(int status, string description, List<string> errors)
        {
            (Status, Description, Errors) = (status, description, errors);
        }
        public ErrorResponseDto(int status, string description)
        {
            (Status, Description) = (status, description);
        }

    }
}
