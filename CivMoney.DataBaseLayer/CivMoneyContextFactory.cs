using CivMoney.DataBaseLayer.Contracts;

namespace CivMoney.DataBaseLayer
{
    public class CivMoneyContextFactory : ICivMoneyContextFactory
    {
        private readonly CivMoneyContext _context;

        public CivMoneyContextFactory()
        {
            _context = new CivMoneyContext();
        }

        public CivMoneyContext GetContext()
        {
            return _context;
        }
    }
}
