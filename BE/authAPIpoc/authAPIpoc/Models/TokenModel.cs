namespace authAPIpoc.Models
{
    public class TokenModel
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience {get; set;}
    }
}
