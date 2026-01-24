using mero_movie_api.Model;
using mero_movie_api.Services.Interfaces;
using mero_movie_api.Shared;
using Microsoft.AspNetCore.Mvc;


namespace mero_movie_api.Controllers;
[Route("api/watchlist")]
[ApiController]
public class WatchListController(IWatchListService watchListService):ControllerBase
{
    private  readonly IWatchListService _watchListService=watchListService;
    private int UserId=1;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<WatchList>>>> WatchList([FromQuery]WatchStatus? status)
    {
        return Ok(ApiResponse<List<WatchList>>.SuccessResponse(
            await _watchListService.MyWatchList(UserId, status),
            "User Watch List fetch ed successfully"
        ));

    }
    [HttpPost("add")]
    public async Task<ActionResult<ApiResponse<bool>>> Add([FromBody] WatchList watchList)
    {
        return Ok(ApiResponse<bool>.SuccessResponse(
            await _watchListService.AddMyWatchList(watchList,UserId),
            "Watch list added successfully"
        ));
    }

    [HttpPut("update")]
    public async Task<ActionResult<WatchList>> Update([FromBody] WatchList watchList)
    {
        return Ok(ApiResponse<WatchList>.SuccessResponse(
            await _watchListService.UpdateMyWatchList(watchList,UserId),
            "Watch List updated successfully"
        ));
    }

    [HttpDelete("remove/{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Remove(int id)
    {
        var result = await _watchListService.DeleteMyWatchList(id, UserId);

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