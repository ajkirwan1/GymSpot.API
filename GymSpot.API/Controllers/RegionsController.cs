using AutoMapper;
using GymSpot.API.Models.Domain;
using GymSpot.API.Models.DTOs;
using GymSpot.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GymSpot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {


        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regionsDomain = await _regionRepository.GetAllAsync();
            throw new Exception("an error occured");

            if (regionsDomain.Count == 0)
            {
                return NotFound();
            }

            var regionsDtoList = _mapper.Map<List<RegionDTO>>(regionsDomain);

            return Ok(regionsDtoList);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            if (addRegionRequestDTO == null)
            {
                return BadRequest();
            }

            if (addRegionRequestDTO.Name.IsNullOrEmpty() || addRegionRequestDTO.Code.IsNullOrEmpty())
            {
                return BadRequest();
            }

            var regionDomain = _mapper.Map<Region>(addRegionRequestDTO);

            regionDomain = await _regionRepository.CreateAsync(regionDomain);

            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            var region = _mapper.Map<Region>(updateRegionDTO);

            var regionDomain = await _regionRepository.UpdateAsync(id, region);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(region);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDto);
        }

    }
}
