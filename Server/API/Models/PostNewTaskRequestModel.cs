using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PostNewTaskRequestModel
    {
        [Required(ErrorMessage = "invalid task name")]
        [MaxLength(300, ErrorMessage = "task name should be less than 300 length.")]
        [RegularExpression(Core.Ultils.Regessions.TASK_NAME_REG, ErrorMessage = "invalid task name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
