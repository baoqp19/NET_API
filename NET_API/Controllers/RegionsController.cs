using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_API.DbConnect;
using NET_API.Models.Domain;
using NET_API.Models.DTO;

namespace NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly DbConnectApp dbContext;
        public RegionsController(DbConnectApp dbContext)
        { 
            this.dbContext = dbContext;
        }


        [HttpGet]   
        public IActionResult getAll()
        {
            var rigions = dbContext.Regions.ToList();


            var rigionDto = new List<RegionDTO>();

            foreach (var rigion in rigions)
            {
                rigionDto.Add(new RegionDTO()
                {
                    Id = rigion.Id,
                    Code = rigion.Code,
                    Name = rigion.Name,
                    RegionImageUrl = rigion.RegionImageUrl
                }); 
            }
            return Ok(rigionDto);
        }



        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var rigions = dbContext.Regions.Find(id);

            var rigions = dbContext.Regions.FirstOrDefault(x => x.Id == id);


            if (rigions == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDTO
            {
                Id = rigions.Id,
                Code = rigions.Code,
                Name = rigions.Name,
                RegionImageUrl = rigions.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionDTO addRegionDTO)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionDTO.Code,
                Name = addRegionDTO.Name,
                RegionImageUrl = addRegionDTO.RegionImageUrl
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegion updateRegion)
        {
            // Check if region exists
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map DTO to Domain model
            regionDomainModel.Code = updateRegion.Code;
            regionDomainModel.Name = updateRegion.Name;
            regionDomainModel.RegionImageUrl = updateRegion.RegionImageUrl;
            dbContext.SaveChanges();
            // Convert Domain Model to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
           

            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();
         
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }





    }
}
