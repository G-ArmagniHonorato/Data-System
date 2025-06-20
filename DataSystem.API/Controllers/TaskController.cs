﻿using DataSystem.Application.Interfaces;
using DataSystem.Domain.Entities;
using DataSystem.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _service.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("getId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _service.GetByIdAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TaskEntity task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _service.Create(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskEntity task)
        {
            if (id != task.Id) return BadRequest("IDs não coincidem.");
            if(task.Status == TaskEnumStatus.Concluida) return BadRequest("status Concluido não podem ser deletados.");
            await _service.UpdateAsync(task);
            return NoContent();
        }
        [HttpGet("filterStatus/{status}")]
        public async Task<IActionResult> GetByStatus(TaskEnumStatus status)
        {
            var tasks = await _service.GetByStatusAsync(status);
            return Ok(tasks);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
