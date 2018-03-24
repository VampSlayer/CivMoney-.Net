namespace CivMoney.AccessAndBusinessLayer.Contracts
{
    public interface IReadUsersService
    {
        string GetUserName(int userId);
        int GetUserIdFromUserName(string userName);
        string GetUserCurrency(int userId);
    }
}
