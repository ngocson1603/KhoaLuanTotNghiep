using Khoaluan.Models;
using Khoaluan.OtpModels;

namespace Khoaluan.InterfacesService
{
    public interface IMarketTransactionService
    {
        TransactionModel Transaction(TransactionRequest request);
    }
}
