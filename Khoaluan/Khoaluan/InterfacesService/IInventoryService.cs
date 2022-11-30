using Khoaluan.Enums;
using Khoaluan.Models;

namespace Khoaluan.InterfacesService
{
    public interface IInventoryService
    {
        Inventory updateInventory(int userID, int ItemID,int marketype,int quantity);
    }
}
