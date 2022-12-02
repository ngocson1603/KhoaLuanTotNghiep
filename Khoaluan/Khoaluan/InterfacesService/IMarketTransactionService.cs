using Khoaluan.Models;
using Khoaluan.OtpModels;
using System.Transactions;

namespace Khoaluan.InterfacesService
{
    public interface IMarketTransactionService
    {
        MarketTransaction Transaction(TransactionRequest request);
    }
}
