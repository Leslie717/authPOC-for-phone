using authAPIpoc.Database;
using authAPIpoc.DbService;
using authAPIpoc.Models;
using authAPIpoc.Utilities.claims;
using authAPIpoc.Utilities.token;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace authAPIpoc.Services
{
    public class UserService : ServiceBase, IUserService
    {

        #region "Private Properties"
        private readonly IConfiguration configuration = null;
        private readonly IOptions<TokenModel> TokenSettings;

        #endregion

        #region "Constructor"
        public UserService(IConfiguration config, IDbManager dbM, IOptions<TokenModel> tknSettings) //IEmailService emailSrv
          : base(dbM)
        {
            configuration = config;
            TokenSettings = tknSettings;
        }
        #endregion

        #region "Public Functions"

        public bool IsUserExists(string email)
        {
            FilterDefinition<User> query = null;
            User usr = null;
            bool rtnVal = false;

            try
            {
                if (email != string.Empty)
                {
                    //Defining filter defintion specific to this email
                    query = Builders<User>.Filter.Where(an => an.email == email);
                    usr = base.GetEntity<User>(query);
                }
                if (usr != null)
                    rtnVal = true;

            }
            catch
            {
                throw;
            }
            return rtnVal;
        }

        public User getUser(string email)
        {
            bool rtnVal = false;
            User user = null;

            FilterDefinition<User> query = null;

            try
            {
                if (email != string.Empty)
                {
                    query = Builders<User>.Filter.Where(o => o.email == email);
                    user = base.GetEntity<User>(query);
                }
            }
            catch
            {
                throw;
            }

            return user;
        }

        public Response loginUser(LoginData data)
        {
            //TODO
            //1) decrypt password.(get encreypted password)
            //2) add token generator service and cookies to send to client

            LoginReturnData rtnData = null;
            Response res = null;
            bool rtnVal = false;

            try
            {
                
                res = new Response();
                res.data = rtnData;

                User usrLookUp = getUser(data.userName);

                if (usrLookUp == null)
                    res.message = CONSTANTS.MESSAGES.LOGIN.LOGIN_FAILED;
                else
                {

                    if (checkPassword(data.password, usrLookUp.Identity.ToString())) //check password corect?   
                    {
                        //var tokenString = "dummyToken";
                        //var refTokenString = "refDummyToken";
                        var claimData = ClaimsGenerator.generateClaims(usrLookUp);
                        var tokenString = TokenGenerator.generateToken(claimData, TokenSettings);
                        var refTokenString = generateRefreshToken();
                        res.message = CONSTANTS.MESSAGES.LOGIN.LOGIN_SUCCESS;
                        res.data = new LoginReturnData(usrLookUp.Identity.ToString(), usrLookUp.firstName, usrLookUp.lastName, usrLookUp.email,
                            usrLookUp.userName, tokenString, refTokenString, (int)TimeSpan.FromMinutes(20).TotalSeconds, usrLookUp.roleList);

                        res.status = CONSTANTS.MESSAGES.STATUS_SUCCESS;
                        rtnVal = true;
                    }
                    else
                    {
                        res.status = CONSTANTS.MESSAGES.STATUS_FAILED;
                        res.message = "Password is worng.";
                    }

                }
            }

            catch
            {
                throw;
            }
            return res;
        }


        public bool insertUser(User userData, string password)
        {
            //TODO
            //1) encrypt password.(store encreypted password)
            //2) add activation flow
            //3) activate user according to login type
            //4) add email service, to send activation email.

            Response res = null;
            bool rtnVal = false;
            Secret mySecret = null;
            User user = null;
            UserAuthData authData = null;

            try
            {
                res = new Response();

                //insert user
                mySecret = new Secret();
                mySecret.password = password;
                userData.roleList.Add(AccessRoles.BASIC);
                userData.userDetail.businessType.Add(BUSINESSTYPES.NONE);

                if (base.InsertEntity(userData)) //DB call 1
                {
                    mySecret.userId = userData.Identity;
                    mySecret.createdBy = userData.Identity;

                    if (base.InsertEntity(mySecret)) //DB call 2
                    {
                        authData = new UserAuthData();
                        authData.loginType = LOGINTYPE.NORMAL;
                        authData.userId = userData.Identity;
                        authData.createdBy = userData.Identity;

                        if (base.InsertEntity(authData)) //DB call 3
                        {
                            rtnVal = true;
                        }
                    }
                }

            }
            catch
            {
                throw;
            }


            return rtnVal;
        }

        public string generateRefreshToken()
        {
            string refTok = string.Empty;
            try
            {
                var randomNumber = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    refTok = Convert.ToBase64String(randomNumber);
                    //get user detais and user ip then save this ref tok to db instance collection for user
                    //
                }
            }
            catch { throw; }
            return refTok;
        }

        #endregion

        #region "Private Functions"
        private bool checkPassword(string password, string userId)
        {
            Response res = null;
            bool rtnVal = false;
            Secret mySecret = null;
            FilterDefinition<Secret> query = null;
            try
            {
                if (password != string.Empty)
                {
                    query = Builders<Secret>.Filter.Where(s => s.userId == userId && s.password == password);
                    mySecret = base.GetEntity<Secret>(query);
                    if (mySecret != null)
                        rtnVal = true;
                }
            }
            catch
            {
                throw;
            }
            return rtnVal;
        }

        #endregion
    }
}
