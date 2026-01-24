using mero_movie_api.Dto;
using mero_movie_api.Dto.Response;
using mero_movie_api.Services;
using mero_movie_api.Services.Interfaces;
using mero_movie_api.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mero_movie_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public readonly IMediaService MediaService;

        public HomeController(IMediaService mediaService)
        {
            MediaService = mediaService;
        }
        
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PaginatedResponse<MediaListResponse>>>> Trending(
            [FromQuery] string type = "all",
            [FromQuery] string time = "week")
        {
            var result = await MediaService.TrendingList(type, time);

            return Ok(ApiResponse<PaginatedResponse<MediaListResponse>>.SuccessResponse(
                result,
                "Trending movies fetched successfully"
            ));
        }
        
        
    }
}