using Microsoft.AspNetCore.Mvc;
using Core;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public ActionResult<string> GetTask()
        {
            return "Your First Task";
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetTaskId(int id)
        {
            var task = _taskRepository.FindById(id);
            return task?.Name;
        }

        // PUT api/values/5
        [HttpPost]
        public ActionResult AddTask([FromBody][Required] Core.Task task)
        {
            if (task.Name == "!@#$%")
            {
                return BadRequest("invalid task name");
            }

            _taskRepository.Add(task);
            return Ok("task is added successfully");
        }

    }
}
