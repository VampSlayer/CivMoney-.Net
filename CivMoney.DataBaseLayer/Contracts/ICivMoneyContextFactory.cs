namespace CivMoney.DataBaseLayer.Contracts
{
    public interface ICivMoneyContextFactory
    {
        CivMoneyContext GetContext();
    }
}
