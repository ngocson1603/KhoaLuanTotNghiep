using Khoaluan.InterfacesRepository;
using Khoaluan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class ItemRepository : GameStoreRepository<Item>, IItemRepository
    {
        public ItemRepository(GameStoreDbContext context) : base(context)
        {

        }
    }
}
