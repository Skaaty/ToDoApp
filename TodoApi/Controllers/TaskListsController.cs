using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.DTOs;
using static TodoApi.DTOs.TaskListDto;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListsController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;

        public TaskListsController(TodoContext todoContext, IMapper mapper)
            => (_todoContext,  _mapper) = (todoContext, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListDTO>>> GetAll()
            => Ok(_mapper.Map<IEnumerable<TaskListDTO>>(await _todoContext.TaskLists.ToListAsync()));

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskListDTO>> Get(int id)
            => await _todoContext.TaskLists.FindAsync(id) is TaskList tl ? Ok(_mapper.Map<TaskListDTO>(tl)) : NotFound();

        [HttpPost]
        public async Task<ActionResult<TaskListDto>> Create(CreateTaskListDTO dto)
        {
            var entity = _mapper.Map<TaskList>(dto);
            entity.UserId = 1;
            _todoContext.TaskLists.Add(entity);
            await _todoContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, _mapper.Map<TaskListDTO>(entity));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskListDTO>> Update(int id, UpdateTaskListDTO dto)
        {
            var entity = await _todoContext.TaskLists.FindAsync(id);
            if (entity is null) return NotFound();

            _mapper.Map(dto, entity);

            await _todoContext.SaveChangesAsync();

            return Ok(_mapper.Map<TaskListDTO>(entity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _todoContext.TaskLists.FindAsync(id);
            if (entity is null) return NotFound();
            _todoContext.TaskLists.Remove(entity);
            await _todoContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
