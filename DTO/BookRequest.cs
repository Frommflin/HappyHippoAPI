namespace HappyHippoAPI.DTO
{
    public class BookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
