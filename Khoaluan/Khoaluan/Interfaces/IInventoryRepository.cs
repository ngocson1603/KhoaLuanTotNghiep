﻿using Khoaluan.Models;

namespace Khoaluan.Interfaces
{
    public interface IInventoryRepository:IGameStoreRepository<Inventory>
    {
        void updateInventory(int userID, int ItemID);
    }
}
