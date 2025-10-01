namespace Employee_Portal.Response
{
    public class ResponseData
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public string JwtToken { get; set; }
    }
}
