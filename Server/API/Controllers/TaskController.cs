using Microsoft.AspNetCore.Mvc;
using Core;
using System.ComponentModel.DataAnnotations;
using API.Models;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskController(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<string> GetTask()
        {
            return "Your First Task";
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetTaskId(string id)
        {
            var task = _taskRepository.FindById(id);
            return task?.Name;
        }

        [HttpPost]
        public ActionResult AddTask([FromBody][Required] PostNewTaskRequestModel request)
        {
            try
            {
                var task = _mapper.Map<Task>(request);
                _taskRepository.Add(task);
            }
            catch
            {
                return BadRequest("unexpected error. retry later.");
            }

            return Ok("task is added successfully");
        }

    }
}
