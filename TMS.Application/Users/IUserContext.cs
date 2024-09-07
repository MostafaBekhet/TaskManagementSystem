namespace TMS.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}