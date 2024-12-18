using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;

namespace NewZealandWalks.API.Models.Mapping
{
    /// <summary>
    /// Stop Using AutoMapper in .NET
    /// https://www.youtube.com/watch?v=RsnEZdc3MrE
    /// </summary>
    public static class DomainToDtoMapper
    {
        public static RegionDto ToRegionDto(this Region region)
        {
            RegionDto regionDto =
                new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                };

            return regionDto;
        }

    }
}
