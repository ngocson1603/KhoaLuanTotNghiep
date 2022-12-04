using Khoaluan.Models;
using Khoaluan.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.InterfacesRepository
{
    public interface IItemRepository : IGameStoreRepository<Item>
    {
        List<ItemModelView> getItem(int id);
        ItemModelView getItemById(int id);
    }
}
