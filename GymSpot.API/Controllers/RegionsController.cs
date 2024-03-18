using AutoMapper;
using GymSpot.API.Models.Domain;
using GymSpot.API.Models.DTOs;
using GymSpot.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regionsDomain = await _unitOfWork.RegionRepository.GetAllAsync();

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

            regionDomain = await _unitOfWork.RegionRepository.CreateAsync(regionDomain);

            await _unitOfWork.Commit();

            var regionDto = _mapper.Map<RegionDTO>(regionDomain);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);

        }
        [HttpPut]
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
