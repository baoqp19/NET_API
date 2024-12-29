using Microsoft.EntityFrameworkCore;
using NET_API.DbConnect;
using NET_API.Models.Domain;

namespace NET_API.Repositorys
{
    public class IMPRegionRepository : IRegionRepository
    {

        private readonly DbConnectApp dbconnect;

        public IMPRegionRepository(DbConnectApp dbconnect)
        {
            this.dbconnect = dbconnect;
        }
        public async Task<List<Region>> GetAllAsync()
        {
           return await dbconnect.Regions.ToListAsync();
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbconnect.Regions.AddAsync(region);
            await dbconnect.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbconnect.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbconnect.Regions.Remove(existingRegion);
            await dbconnect.SaveChangesAsync();
            return existingRegion;
        }
        
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbconnect.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbconnect.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dbconnect.SaveChangesAsync();
            return existingRegion;
        }



    }
}
