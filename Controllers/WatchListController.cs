using mero_movie_api.Dto.Request;
using mero_movie_api.Dto.Response;
using mero_movie_api.Model;
using mero_movie_api.Services.Interfaces;
using mero_movie_api.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mero_movie_api.Controllers;

[Route("api/watchlist")]
[ApiController]
[Authorize]
public class WatchListController(IWatchListService watchListService, ICurrentUser currentUser) : ControllerBase
{
    private readonly IWatchListService _watchListService = watchListService;
    private readonly ICurrentUser _currentUser = currentUser;

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ApiResponse<List<WatchListResponse>>>> WatchList([FromQuery] WatchStatus? status)
    {
        return Ok(ApiResponse<List<WatchListResponse>>.SuccessResponse(
            await _watchListService.MyWatchList(_currentUser.UserId, status),
            "User Watch List fetch ed successfully"
        ));
    }

    [HttpGet("ids")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<List<List<int>>>>> WatchListIds()
    {
        return Ok(ApiResponse<List<int>>.SuccessResponse(await _watchListService.WatchListIds(_currentUser.UserId)));
    }

    [HttpGet("mywatchlist")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<List<WatchListResponse>>>> WatchListMedia()
    {
        return Ok(
            ApiResponse<List<WatchListResponse>>.SuccessResponse(
                await _watchListService.WatchListsMedia(_currentUser.UserId)));
    }

    [HttpPost("add")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> Add([FromBody] CreateWatchListDto watchList)
    {
        return Ok(ApiResponse<bool>.SuccessResponse(
            await _watchListService.AddMyWatchList(watchList, _currentUser.UserId),
            "Watch list added successfully"
        ));
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<ActionResult<WatchListResponse>> Update([FromBody] UpdateWatchListDto watchList)
    {
        return Ok(ApiResponse<WatchListResponse>.SuccessResponse(
            await _watchListService.UpdateMyWatchList(watchList, _currentUser.UserId),
            "Watch List updated successfully"
        ));
    }

    [HttpDelete("remove/{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<bool>>> Remove(int id)
    {
        var result = await _watchListService.DeleteMyWatchList(id, _currentUser.UserId);

        if (!result)
        {
            return NotFound(ApiResponse<bool>.ErrorResponse("WatchList item not found"));
        }

        return Ok(ApiResponse<bool>.SuccessResponse(
            result,
            "WatchList item removed successfully"
        ));
    }
}