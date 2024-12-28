using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;
using System.Runtime.CompilerServices;

namespace NewZealandWalks.API.Models.Mapping
{
    public static class DtoToDomainMapper
    {
        #region Region
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

        public static Region ToRegion(this UpdateRegionRequestDto updateRegionRequestDto)
        {
            Region region =
                new Region()
                {
                    //Id = updateRegionRequestDto.Id,
                    Name = updateRegionRequestDto.Name,
                    Code = updateRegionRequestDto.Code,
                    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
                };

            return region;
        }
        #region

        #region Walk

        public static Walk ToWalk(this AddWalkRequestDto addWalkRequestDto)
        {
            Walk walk = new Walk()
            {
                Name = addWalkRequestDto.Name,
                Description = addWalkRequestDto.Description,
                LengthInKm = addWalkRequestDto.LengthInKm,
                WalkImageUrl = addWalkRequestDto.WalkImageUrl,
                DifficultyId = addWalkRequestDto.DifficultyId,
                RegionId = addWalkRequestDto.RegionId
            };

            return walk;
        }

        #endregion
    }
}