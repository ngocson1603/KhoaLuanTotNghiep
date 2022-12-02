using Khoaluan.Models;

namespace Khoaluan.Interfaces
{
    public interface IDeveloperRepository:IGameStoreRepository<Developer>
    {
        Developer getDev(string id);
    }
}
