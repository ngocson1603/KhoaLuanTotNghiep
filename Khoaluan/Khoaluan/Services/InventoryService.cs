using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using System.Linq;
using System.Reflection.Metadata;

namespace Khoaluan.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public Inventory updateInventory(int userID, int ItemID,int marketype,int quantity)
        {
            var inventory = _inventoryRepository.GetAll().Where(t => (t.UserID == userID) && (t.ItemID == ItemID)).Single();
            if(marketype==(int)marketType.buy)
            {
                if (inventory == null)
                {
                    return null;
                }
                else
                {
                    inventory.Quantity = inventory.Quantity + quantity;
                }
            }
            else if(marketype==(int)marketType.sell)
            {
                inventory.Quantity=inventory.Quantity - quantity;
            }
            return inventory;
        }
    }
}
