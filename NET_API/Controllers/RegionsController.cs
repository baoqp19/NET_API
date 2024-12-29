using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NET_API.DbConnect;
using NET_API.Models.Domain;
using NET_API.Models.DTO.Region;
using NET_API.Repositorys;
using NET_API.ValidationCustom;

namespace NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    //[Authorize(Roles = "Reader")]
    public class RegionsController : ControllerBase
    {
        private readonly DbConnectApp dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(DbConnectApp dbContext, IRegionRepository regionRepository, IMapper mapper)
        { 
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]   
        public async Task<IActionResult> getAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();


            return Ok(mapper.Map<List<RegionDTO>>(regionsDomain));
        }



        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var rigions = dbContext.Regions.Find(id);

            var regionDomain = await regionRepository.GetByIdAsync(id);



            return Ok(mapper.Map<RegionDTO>(regionDomain));
        }

        [HttpPost]
        
        public async Task<IActionResult> Create([FromBody] AddRegionDTO addRegionDTO)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionDTO);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);


            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegion updateRegion)
        {
            // Check if region exists
            var regionDomainModel = mapper.Map<Region>(updateRegion);


            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }


            // Map DTO to Domain model
            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)

        {
                var regionDomainModel = await regionRepository.DeleteAsync(id);

                if (regionDomainModel == null)
            {
                return NotFound();
            }


                return Ok(mapper.Map<RegionDTO>(regionDomainModel));
            }

    }
}
