using Dapper;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class LibraryRepository:GameStoreRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(GameStoreDbContext context) : base(context)
        {

        }
        public List<LibraryModelDetail> getLibrary()
        {
            var query = @"select Library.UserID,Product.Id,Product.Name as Name,Overview,price,image as Image,Description,ReleaseDate
                        from Product, Library
                        where Library.ProductId = Product.Id";

            var data = Context.Database.GetDbConnection().Query<LibraryModelDetail>(query);
            var result = from p in data.ToList()
                         group p by new
                         {
                             p.UserID,
                             p.Id,
                             p.Name,
                             p.Overview,
                             p.Price,
                             p.Image,
                             p.Description,
                             p.ReleaseDate,
                         } into product
                         select new LibraryModelDetail
                         {
                             UserID = product.Key.UserID,
                             Id = product.Key.Id,
                             Name = product.Key.Name,
                             Overview = product.Key.Overview,
                             Price = product.Key.Price,
                             Image = product.Key.Image,
                             Description = product.Key.Description,
                             ReleaseDate = product.Key.ReleaseDate,
                         };
            return result.ToList();
        }
    }
}
