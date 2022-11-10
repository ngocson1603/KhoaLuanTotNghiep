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
        public List<LibraryDetail> getLibrary(int id);
        public void remove(int userID, int productID);
    }
}
