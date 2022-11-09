﻿using Khoaluan.Models;
using System.Collections.Generic;

namespace Khoaluan.Interfaces
{
    public interface IOrderRepository:IGameStoreRepository<Order>
    {
        Order createOrder(int userID,List<Product> productPurcahse);
    }
}
