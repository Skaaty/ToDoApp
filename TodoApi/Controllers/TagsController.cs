using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.DTOs;
using Microsoft.VisualBasic;
using System.Collections;

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;

        public TagsController(TodoContext todoContext, IMapper mapper)
            => (_todoContext, _mapper) = (todoContext, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetAll()
            => Ok(_mapper.Map<IEnumerable<TagDto>>(await _todoContext.Tags.ToListAsync()));

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TagDto>> Get(int id)
            => await _todoContext.Tags.FindAsync(id) is Tag tag ? Ok(_mapper.Map<TagDto>(tag)) : NotFound();

        [HttpPost]
        public async Task<ActionResult<TagDto>> Create(CreateTagDto dto)
        {
            var entity = _mapper.Map<Tag>(dto);
            _todoContext.Tags.Add(entity);
            await _todoContext.SaveChangesAsync();

            return Ok(_mapper.Map<TagDto>(entity));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TagDto>> Update(int id, UpdateTagDto dto)
        {
            var entity = await _todoContext.Tags.FindAsync(id);
            if (entity is null) return NotFound();
            _mapper.Map(dto, entity);
            await _todoContext.SaveChangesAsync();

            return Ok(_mapper.Map<TagDto>(entity));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tag = await _todoContext.Tags.FindAsync(id);
            if (tag is null) return NotFound();
            _todoContext.Remove(tag);
            await _todoContext.SaveChangesAsync();

            return NoContent();
        }


    }
}
