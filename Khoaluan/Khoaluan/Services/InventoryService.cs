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

        public Inventory updateInventory(int userID, int ItemID)
        {
            var inventory = _inventoryRepository.GetAll().Where(t => (t.UserID == userID) && (t.ItemID == ItemID)).Single();
            return inventory;
        }
    }
}
