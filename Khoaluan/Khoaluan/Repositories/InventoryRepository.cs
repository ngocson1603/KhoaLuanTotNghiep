using Dapper;
using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Khoaluan.Repositories
{
    public class InventoryRepository:GameStoreRepository<Inventory>,IInventoryRepository
    {
        public InventoryRepository(GameStoreDbContext context):base(context)
        {

        }
    }
}
