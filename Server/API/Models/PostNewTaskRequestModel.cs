using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PostNewTaskRequestModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "invalid task name")]
        [MaxLength(300, ErrorMessage = "task name should be less than 300 length.")]
        [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "invalid task name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
