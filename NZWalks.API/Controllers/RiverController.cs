
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.custom_action_filters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RiverController : ControllerBase
    {
        private readonly IRiverRepository riverRepository;
        private readonly IMapper mapper;

        public RiverController(IRiverRepository riverRepository, IMapper mapper)
        {
            this.riverRepository = riverRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ValidateModel]
        public async Task<IActionResult> GetAll()
        {
            var riverModels = await riverRepository.GetAllAsync();
            return Ok(mapper.Map<List<RiverDto>>(riverModels));
        }
        // 
        [HttpGet]    
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var riverDomain = await riverRepository.GetById(id);
            if (riverDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RiverDto>(riverDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRiverRequestDto addRiverRequestDto)
        {
            var riverDomainModel = mapper.Map<River>(addRiverRequestDto);
            riverDomainModel = await riverRepository.Create(riverDomainModel);
            var riverDto = mapper.Map<RiverDto>(riverDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = riverDto.Id }, riverDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRiverRequestDto updateRiverRequestDto)
        {
            var riverModel = mapper.Map<River>(updateRiverRequestDto);
            var updatedRiver = await riverRepository.Update(id, riverModel);

            if (updatedRiver == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RiverDto>(updatedRiver));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedRiver = await riverRepository.Delete(id);

            if (deletedRiver == null)
            {
                return NotFound($"Entity with ID '{id}' was not found.");
            }

            return NoContent();
        }
    }
}
