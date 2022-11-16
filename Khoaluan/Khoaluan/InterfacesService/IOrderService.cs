using Khoaluan.Models;
using System.Collections.Generic;

namespace Khoaluan.InterfacesService
{
    public interface IOrderService
    {
        Order createOrder(int userID, List<Cart> productPurchase);
    }
}
