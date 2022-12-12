using Dapper;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.Repositories
{
    public class LibraryRepository:GameStoreRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(GameStoreDbContext context) : base(context)
        {
            
        }
        public List<LibraryDetail> getLibrary(int id)
        {
            var query = @"select Library.UserID,Product.Id,Product.Name as Name,image as Image
                        from Product, Library
                        where Library.ProductId = Product.Id and Library.UserID=@id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var data = Context.Database.GetDbConnection().Query<LibraryDetail>(query,parameter);
            return data.ToList();
        }
        public void remove(int userID, int productID)
        {
            var query = @"delete from Library
                        where Library.ProductId=@productid and Library.UserID=@id";
            var parameter = new DynamicParameters();
            parameter.Add("id", userID);
            parameter.Add("productid", productID);
            Context.Database.GetDbConnection().Execute(query, parameter);
        }
      
        public void updateLibrary(int userID, List<Cart> cart)
        {
            List<Library> libraries = new List<Library>();
            foreach(var p in cart)
            {
                Library library = new Library()
                {
                    UserId = userID,
                    ProductId = p.product.Id
                };
                libraries.Add(library);
            }
            this.BulkInsert(libraries);
        }
    }
}
