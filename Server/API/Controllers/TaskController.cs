using API.Models;
using AutoMapper;
using Core;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try 
            { 
                return Ok(_mapper.Map<IEnumerable<GetTaskDetailsModel>>(_taskService.GetTasks()));
            } 
            catch (Exception e) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
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
                _taskService.CreateTask(request.Name);

                // should we response the task details?
                return Ok("task is added successfully");
            }
            catch(ServiceException e)
            {
                if(e.InnerException is TaskException)
                {
                    return BadRequest(e.Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        [HttpDelete("{id}")]
        public ActionResult<GetTaskDetailsModel> DeleteTask(string id)
        {
            _taskService.DeleteTask(id);
            return Ok("task is deleted");
        }
    }
}
