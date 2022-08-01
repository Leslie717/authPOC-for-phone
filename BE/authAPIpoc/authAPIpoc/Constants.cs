namespace authAPIpoc
{

    public static class CONSTANTS
    {
        //public const string AUTH_SCHEME = "ByrebarnAuth";
        public const string API_VERSION_1 = "1";

        public const string MONGO_DB = "Mongo Database";
        //public const string AWS = "Amazon Web Services";
        public const string CONTENT_TYPE = "application/json";
        //public const string AWS_PROFILE = "AWSSettings:Profile";
        //public const string AESKEY = @"4g352hACL3Q2u1Ohap/9J6xTnP8h6GMdpmbO9CoJjCw=";
        //public const string AESIV = @"aOLXSCvr9Q1nVekb/anlRw==";


        public static class MESSAGES
        {
            public const string ERROR = "Error while processing the request";
            public const string STATUS_SUCCESS = "SUCCESS";
            public const string STATUS_FAILED = "FAILED";

            public static class REGISTRATION
            {
                public const string REGISTRATION_EXISTS = "Registration Already exist";
                public const string REGISTRATION_FAILED = "User registration failed";
                public const string REGISTRATION_SUCCESS = "User registration successful";
            }
            public static class ACTIVATION
            {
                public const string ACTIVATION_ID_MISSING = "Activation id missing";
                public const string ACTIVATION_COMPLETED = "Activation already completed";
                public const string ACTIVATION_FAILED = "Profile activation failed";
                public const string ACTIVATION_SUCCESS = "Profile activation successful";
            }
            public static class USER_DETAILS
            {
                public const string FAILED_POST = "Your details did not get uploaded. Please try again.";
                public const string FAILED_GET = "Failed to retrive user details";
                public const string SUCCESS_POST = "You details have been saved successfully!";
                public const string SUCCESS_GET = "User details retrieved successfully";
                public const string ROLE_UPDATE_SUCCESS = "Successfully updated the user role";
                public const string ROLE_UPDATE_FAILED = "Failed to update the user role";
            }
            public static class LOGIN
            {
                public const string LOGIN_FAILED = @"Incorrect Email or Password";
                public const string LOGIN_SUCCESS = @"Login successful";
                public const string INCORRECT_CRED = @"INCORRECT CREDENTIALS";
                public const string TRYANOTHERMETHOD = @"You cannot perform this action, please try another login methods";
                public const string CANNOTPERFORM = @"You cannot perform this action";
                public const string UNREGISTERED_USER_LOGIN = @"This account is not registered.";
            }

 
        }
        public static class ROUTES
        {
            public static class USER
            {
                public const string AUTHENTICATION_POST = @"api/[Controller]/authenticate";
                //public const string AUTHENTICATION_POST = @"api/authenticate";
                public const string AUTHENTICATION_RESET_POST = @"api/v{version:apiVersion}/[Controller]/reset";
                public const string REGISTRATION_POST = @"api/[Controller]/register";
                //public const string REGISTRATION_POST = @"api/register";
            }

        }
        public static class SETTINGS
        {
            public static class DB
            {
                public const string CONNECTION = "DBSettings:ConnectionString";
                public const string NAME = "DBSettings:DbName";
            }
            public static class CORS
            {
                public const string ORIGIN = "Cors:Origin";
                //public const string CORS_POLICY = "ByrebarnCorsPolicy";
                //public const string DEV_POLICY = "CorsPolicyDev";
                //public const string PROD_POLICY = "CorsPolicyProd";
            }
            public static class TOKEN
            {
                public const string TOKEN_SETTINGS = "TokenSettings";
                public const string TOKEN_SECRET = "Secret";
                public const string TOKEN_ISSUER = "Issuer";
                public const string TOKEN_AUDIENCE = "Audience";
            }
        }
    }


}
