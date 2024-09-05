namespace Bloggr.API.Models.API
{
    public class APIResponse
    {
        public object? Data { get; set; }
        public string? Error { get; set; }
        public bool Success => Error == null;
    }
}