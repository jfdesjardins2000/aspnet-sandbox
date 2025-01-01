using NewZealandWalks.API.Models.Domain;

namespace NewZealandWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
