namespace Business
{
    public interface IUserContext
    {
        bool IsValid(User user);
    }

    public class UserContext : BaseContext, IUserContext
    {
        public bool IsValid(User user)
        {
            if (user == null)
                return false;

            bool isValidLogin = false;

            //TODO validate user, for now just let them pass
            isValidLogin = true;

            return isValidLogin;
        }
    }
}
