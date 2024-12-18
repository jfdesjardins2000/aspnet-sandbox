using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;

namespace NewZealandWalks.API.Models.Mapping
{
    public static class DtoToDomainMapper
    {
        public static Region ToRegion(this RegionDto regionDto)
        {
            Region region =
                new Region()
                {
                    Id = regionDto.Id,
                    Name = regionDto.Name,
                    Code = regionDto.Code,
                    RegionImageUrl = regionDto.RegionImageUrl
                };

            return region;
        }


        public static Region ToRegion(this AddRegionRequestDto addRegionRequestDto)
        {
            Region region =
                new Region()
                {
                    //Id = regionCreateContract.Id,
                    Name = addRegionRequestDto.Name,
                    Code = addRegionRequestDto.Code,
                    RegionImageUrl = addRegionRequestDto.RegionImageUrl
                };

            return region;
        }

    }
}
