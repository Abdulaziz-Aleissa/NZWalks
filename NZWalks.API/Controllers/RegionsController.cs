using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using AutoMapper;
using NZWalks.API.custom_action_filters;
using Microsoft.AspNetCore.Authorization;
using Azure.Core.Serialization;
using System.Text.Json;


namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Add the Authorize attribute to the controller to secure all endpoints

    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles ="Reader")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                throw new Exception("This is a custom exception");
                var regionModels = await regionRepository.GetAllAsync();
                var regionDtos = mapper.Map<List<RegionDto>>(regionModels);

                logger.LogInformation($"Finished get all regions data {JsonSerializer.Serialize(regionModels)}");
                logger.LogWarning("This is a warning log");
                return Ok(regionDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }



        }

        [HttpGet("{id:guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var regionModel = await regionRepository.GetById(id);
            if (regionModel == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(regionModel);
            return Ok(regionDto);
        }


        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create(RegionDto regionDto)
        {
            var regionModel = mapper.Map<Region>(regionDto);
            regionModel = await regionRepository.Create(regionModel);
            var createdRegionDto = mapper.Map<RegionDto>(regionModel);
            return CreatedAtAction(nameof(GetById), new { id = createdRegionDto.Id }, createdRegionDto);
        }


        [HttpPut("{id:guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid id, RegionDto regionDto)
        {
            var regionModel = mapper.Map<Region>(regionDto);
            var updatedRegion = await regionRepository.Update(id, regionModel);
            if (updatedRegion == null)
            {
                return NotFound();
            }
            var updatedRegionDto = mapper.Map<RegionDto>(updatedRegion);
            return Ok(updatedRegionDto);
        }

        [HttpDelete("{id:guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedRegion = await regionRepository.Delete(id);
            if (deletedRegion == null)
            {
                return NotFound();
            }
            var deletedRegionDto = mapper.Map<RegionDto>(deletedRegion);
            return Ok(deletedRegionDto);
        }
    }
}
