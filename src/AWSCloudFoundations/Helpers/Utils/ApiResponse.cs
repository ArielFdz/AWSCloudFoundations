namespace AWSCloudFoundations.Helpers.Utils
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public ApiResponse(T data, int statusCode)
        {
            Data = data;
            StatusCode = statusCode;
            Headers = new Dictionary<string, string>();
        }

        public ApiResponse<T> AddHeader(string key, string value)
        {
            Headers.Add(key, value);
            return this;
        }
    }
}
