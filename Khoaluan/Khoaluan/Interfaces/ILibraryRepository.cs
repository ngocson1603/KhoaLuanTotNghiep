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
        public List<LibraryModelDetail> getLibrary();
    }
}
