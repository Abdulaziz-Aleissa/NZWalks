using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using AutoMapper;

using NZWalks.API.custom_action_filters;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DifficultiesController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

 
        public DifficultiesController(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var difficultyModels = await difficultyRepository.GetAllAsync();
            var difficultyDtos = mapper.Map<List<DifficultyDto>>(difficultyModels);
            return Ok(difficultyDtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var difficultyModel = await difficultyRepository.GetById(id);
            if (difficultyModel == null)
            {
                return NotFound();
            }
            var difficultyDto = mapper.Map<DifficultyDto>(difficultyModel);
            return Ok(difficultyDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create(DifficultyDto difficultyDto)
        {
            var difficultyModel = mapper.Map<Difficulty>(difficultyDto);
            difficultyModel = await difficultyRepository.Create(difficultyModel);
            var createdDifficultyDto = mapper.Map<DifficultyDto>(difficultyModel);
            return CreatedAtAction(nameof(GetById), new { id = createdDifficultyDto.Id }, createdDifficultyDto);
        }


        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, DifficultyDto difficultyDto)
        {
            var difficultyModel = mapper.Map<Difficulty>(difficultyDto);
            var updatedDifficulty = await difficultyRepository.Update(id, difficultyModel);
            if (updatedDifficulty == null)
            {
                return NotFound();
            }
            var updatedDifficultyDto = mapper.Map<DifficultyDto>(updatedDifficulty);
            return Ok(updatedDifficultyDto);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedDifficulty = await difficultyRepository.Delete(id);
            if (deletedDifficulty == null)
            {
                return NotFound();
            }
            var deletedDifficultyDto = mapper.Map<DifficultyDto>(deletedDifficulty);
            return Ok(deletedDifficultyDto);
        }
    }
}
