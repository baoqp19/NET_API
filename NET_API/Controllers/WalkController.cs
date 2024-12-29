using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_API.Models.Domain;
using NET_API.Models.DTO.Walk;
using NET_API.Repositorys;
using NET_API.ValidationCustom;


namespace NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository iwalkRepository;
        public WalkController(IMapper mapper, IWalkRepository iwalkRepository)
        {
            this.mapper = mapper;
            this.iwalkRepository = iwalkRepository;
        }
    
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO addWalkRequestDto)
        {
           
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await iwalkRepository.CreateAsync(walkDomainModel);
           
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await iwalkRepository.GetAllAsync();
           
            return Ok(mapper.Map<List<WalkDTO>>(walksDomainModel));
        }
     
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await iwalkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
          
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
     
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkDTO updateWalkDTO)
        {
            
            var walkDomainModel = mapper.Map<Walk>(updateWalkDTO);
            walkDomainModel = await iwalkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
         
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
 
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await iwalkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }
           
            return Ok(mapper.Map<WalkDTO>(deletedWalkDomainModel));
        }
    }
}
