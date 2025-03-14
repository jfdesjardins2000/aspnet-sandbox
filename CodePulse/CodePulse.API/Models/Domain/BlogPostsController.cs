using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Models.Domain
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;



    }
}
