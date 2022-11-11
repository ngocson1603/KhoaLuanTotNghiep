using Khoaluan.Models;
using Khoaluan.OtpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Interfaces
{
    public interface ILibraryRepository:IGameStoreRepository<Library>
    {
        List<LibraryDetail> getLibrary(int id);
        void remove(int userID, int productID);
        void updateLibrary(int userID, List<Cart> cart);
    }
}
