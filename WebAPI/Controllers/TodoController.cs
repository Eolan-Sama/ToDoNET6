﻿using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTO;
using NLayer.Core.Models;
using NLayer.Core.Services;
using System.Linq.Expressions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Todo> _service;


        public TodoController(IService<Todo> service,IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var response = await _service.GetAllAsync();
            var todoDto = _mapper.Map<List<TodoDto>>(response.ToList());
            return CreateActionResult<List<TodoDto>>(CustomResponseDto<List<TodoDto>>.Success(200, todoDto));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _service.GetByIdAsync(id);
            var todoDto = _mapper.Map<TodoDto>(todo);
            return Ok(todoDto);
        }

        [HttpGet("status/{status}")]

        public Task<IActionResult> Where(string status)
        {
            var todo =  _service.Where(x => x.Status == {status});
            var todoDto = _mapper.Map<List<TodoDto>>(todo.ToList());
            return todoDto;
        }
        [HttpPost]

        public async Task<IActionResult> Save(TodoDto todoDto)
        {
            var todo = await _service.AddAsync(_mapper.Map<Todo>(todoDto));
            var todosDto = _mapper.Map<TodoDto>(todo);
            return Ok(todosDto);
        }
        [HttpPut]

        public async Task<IActionResult> Update(TodoDto todoDto)
        {
            await _service.UpdateAsync(_mapper.Map<Todo>(todoDto));
            return Ok(204);
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> Remove(int id)
        {
            var todo = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(todo);
            return Ok(204);
        }
    }

}
