﻿using API.Models;
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
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;

        public TaskController(IRepository<Task> taskRepository, ITaskService taskService, IMapper mapper)
        {
            _mapper = mapper;
            _taskService = taskService;
        }

        [HttpGet]
        public StandardApiResponse<IEnumerable<GetTaskDetailsModel>> GetTasks()
        {
            return new StandardApiResponse<IEnumerable<GetTaskDetailsModel>>(
                _mapper.Map<IEnumerable<GetTaskDetailsModel>>(_taskService.GetTasks())
            );
        }

        [HttpGet("{id}")]
        public ActionResult<GetTaskDetailsModel> GetTaskId(string id)
        {
            var task = _taskService.GetTask(id);

            return _mapper.Map<GetTaskDetailsModel>(task);
        }

        [HttpPost]
        public StandardApiResponse<GetTaskDetailsModel> AddTask([FromBody][Required] PostNewTaskRequestModel request)
        {
            var taskDetails = _mapper.Map<GetTaskDetailsModel>(_taskService.CreateTask(request.Name));

            return new StandardApiResponse<GetTaskDetailsModel>(taskDetails, "task is added successfully.");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(string id)
        {
            _taskService.DeleteTask(id);
            return Ok(new StandardApiResponse<object>("task is deleted."));
        }
    }

}
