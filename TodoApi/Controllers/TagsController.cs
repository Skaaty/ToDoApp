using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.DTOs;
using static TodoApi.DTOs.TagDto;
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

        public TagsController(TodoContext todoContext, Mapper mapper)
            => (_todoContext, _mapper) = (todoContext, mapper);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAll()
            => Ok(_mapper.Map<IEnumerable<TagDTO>>(await _todoContext.Tags.ToListAsync()));

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> Get(int id)
            => await _todoContext.Tags.FindAsync(id) is Tag tag ? Ok(_mapper.Map<TagDTO>(tag)) : NotFound();

        [HttpPost]
        public async Task<ActionResult<TagDTO>> Create(CreateTagDTO dto)
        {
            var tag = _mapper.Map<Tag>(dto);
            _todoContext.Tags.Add(tag);
            await _todoContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = tag.Id }, _mapper.Map<TagDTO>(tag));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TagDTO>> Update(int id, UpdateTagDTO dto)
        {
            var tag = await _todoContext.Tags.FindAsync(id);
            if (tag is null) return NotFound();
            _mapper.Map(dto, tag);
            await _todoContext.SaveChangesAsync();
            return Ok(_mapper.Map<TagDTO>(tag));
        }

        [HttpDelete("{id}")]
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
