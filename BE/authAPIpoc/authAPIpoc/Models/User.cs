using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace authAPIpoc.Models
{
    [BsonIgnoreExtraElements]
    public class User : AuditData
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string mobileNumber { get; set; }
        public UserDetails userDetail { get; set; }
        public List<AccessRoles> roleList { get; set; }

        public User()
        {
            userDetail = new UserDetails();
            roleList = new List<AccessRoles>();
        }
    }

    [BsonIgnoreExtraElements]
    public class UserDetails
    {
        public string imageUrl { get; set; }
        public string businessName { get; set; }
        public List<BUSINESSTYPES> businessType { get; set; }
        public string gender { get; set; }
        public string addressPreference { get; set; }
        public List<Address> addressList { get; set; }

        public UserDetails()
        {
            imageUrl = string.Empty;
            businessName = string.Empty;
            businessType = new List<BUSINESSTYPES>();
            gender = string.Empty;
            addressPreference = string.Empty;
            addressList = new List<Address>();
        }
    }

    [BsonIgnoreExtraElements]
    public class Address
    {
        public string addressType { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string pinCode { get; set; }
        public string state { get; set; }
        public string country { get; set; }

        public Address()
        {
            addressType = string.Empty;
            addressLine1 = string.Empty;
            addressLine2 = string.Empty;
            city = string.Empty;
            pinCode = string.Empty;
            state = string.Empty;
            country = string.Empty;
        }
    }





    [JsonConverter(typeof(StringEnumConverter))]
    public enum BUSINESSTYPES
    {
        NONE,
        PRODUCT,
        SERVICE,
        WHOLESALE,
        RETAIL
    }




}
