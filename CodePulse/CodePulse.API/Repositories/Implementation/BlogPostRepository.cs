using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        public Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
