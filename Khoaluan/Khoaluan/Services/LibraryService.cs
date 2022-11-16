using Khoaluan.Interfaces;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using System.Collections.Generic;
using System.Linq;

namespace Khoaluan.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IProductRepository _productRepository;
        public LibraryService(ILibraryRepository libraryRepository,IProductRepository productRepository)
        {
            _libraryRepository = libraryRepository;
            _productRepository = productRepository;
        }

        public List<LibraryDetail> getLibrary(int id)
        {
            var products = _productRepository.GetAll();
            var libraries = _libraryRepository.GetAll();
            var detail = from p in products
                         join lib in libraries
                         on p.Id equals lib.ProductId
                         where lib.UserId == id
                         select new LibraryDetail
                         {
                             UserID=lib.UserId,
                             Id=p.Id,
                             Name=p.Name,
                             Image=p.Image
                         };
            return detail.ToList();
        }

        public Library remove(int userID, int productID)
        {
            var getLibrary = _libraryRepository.GetAll().Where(t => (t.ProductId == productID) && (t.UserId == userID)).Single();
            return getLibrary;
        }

        public List<Library> updateLibrary(int userID, List<Cart> cart)
        {
            List<Library> libraries = new List<Library>();
            foreach (var p in cart)
            {
                Library library = new Library()
                {
                    UserId = userID,
                    ProductId = p.product.Id
                };
                libraries.Add(library);
            }
            return libraries;
        }
    }
}
