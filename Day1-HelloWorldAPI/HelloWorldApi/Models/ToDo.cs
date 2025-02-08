using System.ComponentModel.DataAnnotations;

namespace HelloWorldApi.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title can't exceed 100 characters.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "IsCompleted is required.")]
        public bool IsCompleted { get; set; } = false;
    }
}
