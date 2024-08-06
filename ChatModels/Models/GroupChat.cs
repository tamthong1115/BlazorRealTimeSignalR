using System.ComponentModel.DataAnnotations;

namespace ChatModels.Models
{
    public class GroupChat
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? SenderId { get; set; }
        public string? Message { get; set; }
        [Required]
        public DateTime DateTime { get; set; }

    }
}
