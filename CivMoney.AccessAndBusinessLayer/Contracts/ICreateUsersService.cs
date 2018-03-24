namespace CivMoney.AccessAndBusinessLayer.Contracts
{
    public interface ICreateUsersService
    {
        int AddUser(string userName, string password, string currency);
    }
}
