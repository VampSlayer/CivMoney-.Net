namespace CivMoney.AccessAndBusinessLayer.Contracts
{
    public interface IUpdateUsersService
    {
        bool UpdateUserCurrency(int userId, string currency);
    }
}
