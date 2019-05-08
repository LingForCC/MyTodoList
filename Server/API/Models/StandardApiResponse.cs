namespace API.Models
{
    public class StandardApiResponse
    {
        public string Message { get; set; }

        public object Data { get; set; }

        public StandardApiResponse(object data, string message = null)
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
