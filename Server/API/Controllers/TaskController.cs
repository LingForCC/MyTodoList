using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetTask()
        {
            return "Your First Task";
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetTaskId(int id)
        {
            return id.ToString();
        }

        // PUT api/values/5
        [HttpPost("{id}")]
        public void AddTask(int id, [FromBody] Core.Task task)
        {
            //do nothing for now
        }

    }
}
