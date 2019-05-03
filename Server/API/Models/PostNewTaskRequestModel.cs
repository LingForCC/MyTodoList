using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PostNewTaskRequestModel
    {

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
