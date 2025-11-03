using Microsoft.AspNetCore.Mvc;

namespace HttpClientDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly PostService _postService;

    public TestController(
        PostService postService)
    {
        _postService = postService;
    }

    [HttpGet("posts")]
    public async Task<IActionResult> GetPostsAsync([FromQuery] int userId)
    {
        var posts = await _postService.GetPostsAsync(userId);

        return Ok(posts);
    }
}
