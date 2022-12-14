using Dapper;
using Khoaluan.Interfaces;
using Khoaluan.Models;
using Khoaluan.ModelViews;
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
                             Discount=0,
                             Categories = productdetail.ToList()
                         };
            return result.ToList();
        }
        public List<Product> GetProductByName(string name)
        {
            return this.GetAll().Where(t => t.Name.ToLower().Contains(name)).ToList();
        }
        public List<gameRefund> listGameRefund(int Userid)
        {
            var query = @"select distinct productid,p.Name,Image from 
                        [order] o,orderdetail od,Product p
                        where o.id=od.id and od.ProductID=p.Id and datediff(day,datepurchase,getdate())<=7 and UserID=@id";
            var parameter = new DynamicParameters();
            parameter.Add("id", Userid);
            var result = Context.Database.GetDbConnection().Query<gameRefund>(query, parameter);
            return result.ToList();
        }

        public List<Product> listProductDev(int id)
        {
            var query = @"select * from Product where Product.DevId = @id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var result = Context.Database.GetDbConnection().Query<Product>(query, parameter);
            return result.ToList();
        }

        public ActiveGame listProdevActive(int id)
        {
            var query = @"select Product.Id as Id,Product.Name as NamePro, UserName, Developer.Name as NameDev, Price, Status 
from Developer,Product where Product.DevId = Developer.Id  and Product.Id = @id";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var result = Context.Database.GetDbConnection().QuerySingle<ActiveGame>(query, parameter);
            return result;
        }
        public List<ActiveGame> listProdevNotif()
        {
            var query = @"select Product.Id as Id,Product.Name as NamePro, UserName, Developer.Name as NameDev, Price, Status 
from Developer,Product where Product.DevId = Developer.Id order by Product.Id desc ";
            var result = Context.Database.GetDbConnection().Query<ActiveGame>(query);
            return result.ToList();
        }
        public List<ActiveGame> listProforum()
        {
            var query = @"select Product.Id as Id,Product.Name as NamePro,Image, UserName, Developer.Name as NameDev, Price, Status, ReleaseDate 
from Developer,Product where Product.DevId = Developer.Id";
            var result = Context.Database.GetDbConnection().Query<ActiveGame>(query);
            return result.ToList();
        }
        public List<Product> listProductItem()
        {
            var query = @"select Product.* from Product, Item where Product.Id = Item.ProductId";
            var result = Context.Database.GetDbConnection().Query<Product>(query);
            return result.ToList();
        }
        public List<Product> listProductItem(int id)
        {
            var query = @"select Product.* from Product, Item,Inventory,Library where Product.Id = Item.ProductId and Inventory.ItemID = Item.Id and Library.ProductId = Product.Id and Inventory.UserID =@id
group by Product.Id,Product.Name,Product.Overview,Product.Description,Product.ReleaseDate,Product.Price,Product.Image,Product.DevId,Product.Status";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var result = Context.Database.GetDbConnection().Query<Product>(query, parameter);
            return result.ToList();
        }
    }
}
