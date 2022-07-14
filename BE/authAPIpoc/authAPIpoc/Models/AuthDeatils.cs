using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace authAPIpoc.Models
{
    [BsonIgnoreExtraElements]
    public class UserAuthData : AuditData
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        public bool isActivated { get; set; } = false;
        public bool is2Fa { get; set; } = false;
        public bool isLocked { get; set; } = true;
        public LOGINTYPE loginType { get; set; } = LOGINTYPE.NORMAL;
    }

    public class Secret : AuditData
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        public string password { get; set; }

    }

    public class LoginData
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class UserRegistrationData
    {
        public RegistrationDetails registrationDetails { get; set; }
        public string password { get; set; }

    }

    public class RegistrationDetails{
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
    }

    public class LoginReturnData
    {
        public string Identity { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        //public bool is2Fa { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
        public int expiry { get; set; }
        public List<AccessRoles> roleList { get; set; }

        public LoginReturnData(string id, string fName, string lName, string email, string userName, string tok, string refTok, int exp, List<AccessRoles> rlList)
        //string email, string userName, bool isFa, string tok, string refTok, int exp, List<AccessRoles> rlList)
        {
            this.Identity = id;
            this.firstName = fName;
            this.lastName = lName;
            this.email = email;
            this.userName = userName;
            this.token = tok;
            this.expiry = exp;
            //this.is2Fa = isFa;
            roleList = rlList;
            this.refreshToken = refTok;
        }
    }




    [JsonConverter(typeof(StringEnumConverter))]
    public enum LOGINTYPE
    {
        NORMAL,
        SOCIAL,
        PHONE
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AccessRoles
    {
        BASIC,
        SELLER,
        ADMIN,
        ACCOUNTS,
        DATAENTRY
    }
}
