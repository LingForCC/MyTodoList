using API.ErrorCode;
using API.Models;
using AutoMapper;
using Core;
using Core.Repositories;
using Core.Services;
using Core.Services.Exceptions;
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
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly IErrorCodeGeneratorManager _errorCodeGeneratorManager;

        public TaskController(IRepository<Task> taskRepository, ITaskService taskService, 
            IMapper mapper, IErrorCodeGeneratorManager errorCodeGeneratorManager)
        {
            _mapper = mapper;
            _taskService = taskService;
            _errorCodeGeneratorManager = errorCodeGeneratorManager;
        }

        [HttpGet]
        public ActionResult GetTasks()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<GetTaskDetailsModel>>(_taskService.GetTasks()));
            }
            catch(Exception e)
            {
                return GetGenericError(e);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<GetTaskDetailsModel> GetTaskId(string id)
        {
            var task = _taskService.GetTask(id);

            return _mapper.Map<GetTaskDetailsModel>(task);
        }

        [HttpPost]
        public ActionResult AddTask([FromBody][Required] PostNewTaskRequestModel request)
        {
            try 
            {
                var taskDetails = _mapper.Map<GetTaskDetailsModel>(_taskService.CreateTask(request.Name));
                return Ok(taskDetails);
            }
            catch (TaskServiceCreationException e)
            {
                return BadRequest(new
                {
                    ErrorCode = _errorCodeGeneratorManager.GetErrorCode(e),
                    e.Message
                });
            }
            catch(Exception e)
            {
                return GetGenericError(e);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(string id)
        {
            _taskService.DeleteTask(id);
            return Ok(new StandardApiResponse<object>("task is deleted."));
        }

        private ActionResult GetGenericError(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                ErrorCode = _errorCodeGeneratorManager.GetErrorCode(e),
                Message = "Service Not Available"
            });
        }
    }

}
