using Basic_Authentication.Data_Access_Layer;
using Basic_Authentication.Models;

namespace Basic_Authentication.Authentication
{
    public class Validate
    {
        public static UserModel validateUser(string username, string password)
        {
            BasicAccess info = new BasicAccess();
            UserModel user = new UserModel();
            user = info.GetData().Find(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
            return user;
        }
    }
}