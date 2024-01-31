using System.ComponentModel.DataAnnotations;

namespace HappyHippoAPI.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string QuoteText { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
