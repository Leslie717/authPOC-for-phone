
namespace authAPIpoc.Models
{
    public class Response
    {
        public string status { get; set; }
        public string message { get; set; }
        public Object data { get; set; }

        public Response()
        {
            status = "FAILED";
        }

        public Response(string msg)
        { status = msg; }
    }
}
