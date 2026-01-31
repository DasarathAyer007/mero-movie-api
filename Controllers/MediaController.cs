using mero_movie_api.Dto.Response;
using mero_movie_api.Services.Interfaces;
using mero_movie_api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace mero_movie_api.Controllers;

[Route("api/media")]
[ApiController]
public class MediaController(IMediaService mediaService) : Controller
{
    private readonly IMediaService _mediaService = mediaService;

    [HttpGet("trending")]
    public async Task<ActionResult<ApiResponse<PaginatedResponse<MediaListResponse>>>> Trending(
        [FromQuery] string type = "all",
        [FromQuery] string time = "week")
    {
        var result = await _mediaService.TrendingList(type, time);

        return Ok(ApiResponse<PaginatedResponse<MediaListResponse>>.SuccessResponse(
            result,
            "Trending movies fetched successfully"
        ));
    }

    [HttpGet("popular/movie")]
    public async Task<ActionResult<ApiResponse<PaginatedResponse<MediaListResponse>>>> PopularMovie()
    {
        return Ok(ApiResponse<PaginatedResponse<MediaListResponse>>.SuccessResponse(
            await _mediaService.PopularMovieList(),
            "Popular movies fetched successfully"));
    }

    [HttpGet("popular/tv")]
    public async Task<ActionResult<ApiResponse<PaginatedResponse<MediaListResponse>>>> PopularTv()
    {
        return Ok(ApiResponse<PaginatedResponse<MediaListResponse>>.SuccessResponse(await _mediaService.PopularTvList(),
            "Popular movies fetched successfully"));
    }

    [HttpGet("new/movie")]
    public async Task<ActionResult<ApiResponse<PaginatedResponse<MediaListResponse>>>> NewMovie()
    {
        return Ok(ApiResponse<PaginatedResponse<MediaListResponse>>.SuccessResponse(await _mediaService.NewMovieList(),
            "Popular movies fetched successfully"));
    }

    [HttpGet("new/tv")]
    public async Task<ActionResult<ApiResponse<PaginatedResponse<MediaListResponse>>>> NewTv()
    {
        return Ok(ApiResponse<PaginatedResponse<MediaListResponse>>.SuccessResponse(await _mediaService.NewTvList(),
            "Popular movies fetched successfully"));
    }
}