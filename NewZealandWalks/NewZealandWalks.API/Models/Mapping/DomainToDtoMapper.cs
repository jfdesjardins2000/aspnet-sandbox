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
        public static DifficultyDto ToDifficultyDto(this Difficulty difficulty)
        {
            DifficultyDto difficultyDto =
                new DifficultyDto()
                {
                    Id = difficulty.Id,
                    Name = difficulty.Name,
                };
            return difficultyDto;
        }

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

        public static WalkDto ToWalkDto(this Walk walk)
        {
            WalkDto walkDto =
                new WalkDto()
                {
                    Id = walk.Id,
                    Name = walk.Name,
                    Description = walk.Description,
                    LengthInKm = walk.LengthInKm,
                    WalkImageUrl = walk.WalkImageUrl,
                    RegionID = walk.RegionId,
                    DifficultyId = walk.DifficultyId,
                    Region = walk.Region,
                    Difficulty = walk.Difficulty
                };
            return walkDto;
        }
    }
}