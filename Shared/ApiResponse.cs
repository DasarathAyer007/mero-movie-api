namespace mero_movie_api.Shared;


public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public object? Errors { get; set; }

    public ApiResponse(
        bool success,
        int statusCode,
        string message,
        T data ,
        object? errors = null)
    {
        Success = success;
        StatusCode = statusCode;
        Message = message;
        Data = data;
        Errors = errors;
    }

    public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
    {
        return new ApiResponse<T>(true, 200, message, data);
    }

    public static ApiResponse<T> ErrorResponse(
        string message,
        int statusCode = 500,
        object? errors = null)
    {
        return new ApiResponse<T>(false, statusCode, message, default!, errors);
    }
}