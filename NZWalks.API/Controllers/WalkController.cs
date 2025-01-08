using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NZWalks.API.custom_action_filters;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;


        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 1000)
        {
            var walkModels = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy, isAscending,pageNumber,pagesize);
            var walkDtos = mapper.Map<List<WalkDto>>(walkModels);
            return Ok(walkDtos);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkModel = await walkRepository.GetById(id);
            if (walkModel == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walkModel);
            return Ok(walkDto);
        }

      
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create(WalkDto walkDto)
        {
            var walkModel = mapper.Map<Walk>(walkDto);
            walkModel = await walkRepository.Create(walkModel);
            var createdWalkDto = mapper.Map<WalkDto>(walkModel);
            return CreatedAtAction(nameof(GetById), new { id = createdWalkDto.Id }, createdWalkDto);
        }


        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, WalkDto walkDto)
        {
            var walkModel = mapper.Map<Walk>(walkDto);
            var updatedWalk = await walkRepository.Update(id, walkModel);
            if (updatedWalk == null)
            {
                return NotFound();
            }
            var updatedWalkDto = mapper.Map<WalkDto>(updatedWalk);
            return Ok(updatedWalkDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedWalk = await walkRepository.Delete(id);
            if (deletedWalk == null)
            {
                return NotFound();
            }
            var deletedWalkDto = mapper.Map<WalkDto>(deletedWalk);
            return Ok(deletedWalkDto);
        }
    }
}
