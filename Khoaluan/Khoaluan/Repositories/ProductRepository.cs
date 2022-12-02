using Dapper;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Khoaluan.Repositories
{
    public class ProductRepository:GameStoreRepository<Product>,IProductRepository
    {

        public ProductRepository(GameStoreDbContext context):base(context)
        {

        }
        public List<Productdetail>getallProductwithCategory()
        {
            var query = @"select Product.Id,Product.Name as Name,Overview,price,image as Image,Description,ReleaseDate,Category.Name Category,Developer.Name as DevName
                        from Product,ProductCategory,Category,Developer
                        where Product.DevId=Developer.Id and Product.Id=ProductCategory.ProductId
                        and ProductCategory.CategoryId=Category.Id";
            
            var data=Context.Database.GetDbConnection().Query<ProductDetailModel>(query);
            var result = from p in data.AsEnumerable()
                         group p.Category by new
                         {
                             p.Id,
                             p.Name,
                             p.Overview,
                             p.Price,
                             p.Image,
                             p.Description,
                             p.ReleaseDate,
                             p.DevName
                         } into productdetail
                         select new Productdetail
                         {
                             Id = productdetail.Key.Id,
                             Name = productdetail.Key.Name,
                             Overview = productdetail.Key.Overview,
                             Price = productdetail.Key.Price,
                             Image = productdetail.Key.Image,
                             Description = productdetail.Key.Description,
                             ReleaseDate = productdetail.Key.ReleaseDate,
                             DevName = productdetail.Key.DevName,
                             Categories = productdetail.ToList()
                         };
            return result.ToList();
        }
        public List<Product> GetProductByName(string name)
        {
            return this.GetAll().Where(t => t.Name.ToLower().Contains(name)).ToList();
        }
        public List<GameRefund> listGameRefund(int Userid)
        {
            var query = @"select distinct productid,p.Name,Image from 
                        [order] o,orderdetail od,Product p
                        where o.id=od.id and od.ProductID=p.Id and datediff(day,datepurchase,getdate())<=7 and UserID=@id";
            var parameter = new DynamicParameters();
            parameter.Add("id", Userid);
            var result = Context.Database.GetDbConnection().Query<GameRefund>(query, parameter);
            return result.ToList();
        }
    }
}
