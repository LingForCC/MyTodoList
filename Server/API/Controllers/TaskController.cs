using API.Models;
using AutoMapper;
using Core;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IRepository<Task> _taskRepository;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;

        public TaskController(IRepository<Task> taskRepository, ITaskService taskService, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GetTaskDetailsModel>> GetTasks()
        {
            return Ok(_mapper.Map<IEnumerable<GetTaskDetailsModel>>(_taskRepository.FindAll()));
        }

        [HttpGet("{id}")]
        public ActionResult<GetTaskDetailsModel> GetTaskId(string id)
        {
            var task = _taskRepository.FindById(id);
            if (task == null)
            {
                return NotFound("task not found");
            }

            return _mapper.Map<GetTaskDetailsModel>(task);
        }

        [HttpPost]
        public ActionResult AddTask([FromBody][Required] PostNewTaskRequestModel request)
        {
            try
            {
                var task = _mapper.Map<Task>(request);
                task.Id = NUlid.Ulid.NewUlid().ToString();

                _taskService.CreateTask(task);

                // should we response the task details?
                return Ok("task is added successfully");
            }
            catch (TaskException taskEx)
            {
                return BadRequest(taskEx.Message);
            }
            catch
            {
                return BadRequest("unexpected error. retry later.");
            }
        }

    }
}
