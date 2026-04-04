namespace RoyalVillaAPI.Data.Models.DTO
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public Object? Errors { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        // Static Factory Methods

        public static ApiResponse<T> Create(bool success, int statusCode, string message, T? data = default, object? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = success,
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Errors = errors
            };
        }
        public static ApiResponse<T> Ok(T data, string message) => Create(true, 200, message, data);
        public static ApiResponse<T> CreatedAt(T data, string message) => Create(true, 201, message, data);

        public static ApiResponse<T> NotFound(string message = "Resource not found!") => Create(false, 404, message);
        public static ApiResponse<T> NoContent(string message = "Operation completed successfully!") => Create(true, 204, message);
        public static ApiResponse<T> BadMessage(string message, object? errors = null) => Create(false, 400, message, errors: errors);
        public static ApiResponse<T> Conflict(string message) => Create(false, 409, message);
        public static ApiResponse<T> Error(int statusCode, string message, object? errors = null) => Create(false, statusCode, message, errors: errors);

    }
}
