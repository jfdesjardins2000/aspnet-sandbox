using NewZealandWalks.API.Models.Contracts;
using NewZealandWalks.API.Models.Domain;
using NewZealandWalks.API.Models.DTO;

namespace NewZealandWalks.API.Models.Mapping
{
    public static class ApiContractToDomainMapper
    {
        // Map or Convert Contract to Domain Model
        //Region regionDomainModel = new()
        //{
        //    Code = regionCreateContract.Code,
        //    Name = regionCreateContract.Name,
        //    RegionImageUrl = regionCreateContract.RegionImageUrl
        //};

        public static Region ToRegion(this RegionCreateContract regionCreateContract)
        {
            Region region =
                new Region()
                {
                    //Id = regionCreateContract.Id,
                    Name = regionCreateContract.Name,
                    Code = regionCreateContract.Code,
                    RegionImageUrl = regionCreateContract.RegionImageUrl
                };

            return region;
        }

    }
}
