namespace HappyHippoAPI.DTO
{
    public class QuoteRequest
    {
        public string QuoteText { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
