using authAPIpoc.Models;

namespace authAPIpoc.Services
{
    public interface IUserService
    {
        bool IsUserExists(string email);
        User getUser(string email);
        Response loginUser(LoginData data);
        bool insertUser(User userData, string password);
        string generateRefreshToken();

    }
}
