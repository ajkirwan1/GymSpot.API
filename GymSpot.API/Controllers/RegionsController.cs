using AutoMapper;
using GymSpot.API.Filters;
using GymSpot.API.Models.Domain;
using GymSpot.API.Models.DTOs;
using GymSpot.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSpot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync([FromQuery] string? filteredOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            var regionsDomain = await _unitOfWork.RegionRepository.GetAllAsync(filteredOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

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
            var regionDomain = await _unitOfWork.RegionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomain = _mapper.Map<Region>(addRegionRequestDTO);

            regionDomain = await _unitOfWork.RegionRepository.CreateAsync(regionDomain);

            await _unitOfWork.Commit();

            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            var region = _mapper.Map<Region>(updateRegionDTO);

            var regionDomain = await _unitOfWork.RegionRepository.UpdateAsync(id, region);

            await _unitOfWork.Commit();

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomain = await _unitOfWork.RegionRepository.DeleteAsync(id);

            await _unitOfWork.Commit();

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDto);
        }

    }
}
