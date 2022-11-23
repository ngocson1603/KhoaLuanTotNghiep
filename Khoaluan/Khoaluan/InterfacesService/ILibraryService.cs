using Khoaluan.Models;
using Khoaluan.OtpModels;
using System.Collections.Generic;

namespace Khoaluan.InterfacesService
{
    public interface ILibraryService
    {
        Library remove(int userID, int productID);
        List<Library> updateLibrary(int userID, List<Cart> cart);
        List<LibraryDetail> getLibrary(int id);
    }
}
