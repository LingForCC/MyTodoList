namespace API.Models
{
    public class StandardApiResponse<T>
    {
        public string Message { get; set; }

        public T Data { get; set; }

        public StandardApiResponse(T data, string message = null)
        {
            this.Data = data;
            this.Message = message;
        }

        public StandardApiResponse(string message)
        {
            Message = message;
        }

        public StandardApiResponse()
            : this(string.Empty)
        {

        }
    }
}
