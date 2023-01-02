using Dapper;
using Khoaluan.InterfacesRepository;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Khoaluan.Repositories
{
    public class SaleProductRepository:GameStoreRepository<SaleProduct>,ISaleProductRepository
    {
        public SaleProductRepository(GameStoreDbContext context):base(context)
        {

        }
        public int getdiscount(int productid)
        {
            var query = @"select top 1.Discount
                        from Sale s,SaleProduct sp
                        where s.Id=sp.SaleID and ProductID=@id and GETDATE() between CAST(StartDate as date) 
                        and  CAST(EndDate as date)
                        order by sp.Id desc";
            var parameter = new DynamicParameters();
            parameter.Add("id", productid);          
            try
            {
                var Discount = Context.Database.GetDbConnection().QuerySingle<int>(query, parameter);
                return Discount;
            }
            catch
            {
                return -1;
            }         
        }
        public List<int> currentSaleProduct()
        {
            var query = @"select ProductID
                        from Sale s,SaleProduct sp
                        where s.Id=sp.SaleID and GETDATE() between 
                        CAST(StartDate as date) and  CAST(EndDate as date)";
            var data = Context.Database.GetDbConnection().Query<int>(query);
            return data.ToList();
        }

        public List<SaleModelView> ProductIsSale(int id)
        {
            var query = @"select Sale.Id as SaleId,Product.Name as ProductName,Product.Id as ProductId,Product.Image as Image,MAX(SaleProduct.Id) as Id,Discount from Sale,SaleProduct,Product where 
            Sale.Id=SaleProduct.SaleID and Product.Id=SaleProduct.ProductID and SaleId = @id
            group by Sale.Id,Product.Name,Product.Image,Product.Id,Discount";
            var parameter = new DynamicParameters();
            parameter.Add("id", id);
            var result = Context.Database.GetDbConnection().Query<SaleModelView>(query, parameter);
            return result.ToList();
        }

        public List<SaleModelView> ProductIsSaleInDate()
        {
            var query = @"select s.Name as SaleName,Product.Name as ProductName,Product.Image as Image,MAX(sp.Id) as Id
                        from Sale s,SaleProduct sp, Product
                        where s.Id=sp.SaleID and sp.ProductID = Product.Id and GETDATE() between 
                        CAST(StartDate as date) and  CAST(EndDate as date)
						group by s.Name,Product.Name,Product.Image";
            var result = Context.Database.GetDbConnection().Query<SaleModelView>(query);
            return result.ToList();
        }

        public List<SaleModelView> ProductNotSale()
        {
            var query = @"select Product.Id as ProductId,Product.Name as ProductName, Discount, Status,Image,Price,Sale.Name as NameSale,StartDate,EndDate,Developer.Name as DevName,Description from Product
                        left join SaleProduct on Product.Id = SaleProduct.ProductID
                        FULL OUTER JOIN Sale
                        ON SaleProduct.SaleID = Sale.Id
						FULL OUTER JOIN Developer
                        ON Developer.Id = Product.DevId";
            var result = Context.Database.GetDbConnection().Query<SaleModelView>(query);
            return result.ToList();
        }

        public List<SaleModelView> ProductSaleHomePage()
        {
            var query = @"select Product.Id as ProductId,Product.Name as ProductName, Discount, Status,Image,Price,Sale.Name as NameSale,StartDate,EndDate from Product
                        left join SaleProduct on Product.Id = SaleProduct.ProductID
                        FULL OUTER JOIN Sale
                        ON SaleProduct.SaleID = Sale.Id 
						where Sale.Name is not null and GETDATE() between 
                        CAST(StartDate as date) and  CAST(EndDate as date)";
            var result = Context.Database.GetDbConnection().Query<SaleModelView>(query);
            return result.ToList();
        }

        public List<SellitemModelView> ProductSellInMonth()
        {
            var query = @"SELECT Product.Id,Name as NameGame,Image,[Order].DatePurchase as DayCreate,OrderDetail.Price as PricePerItem
                FROM Product
                INNER JOIN OrderDetail ON OrderDetail.ProductID = Product.Id 
                FULL OUTER JOIN [Order] ON [Order].Id = OrderDetail.Id 
                group by Product.Id,Name,Image,[Order].DatePurchase,OrderDetail.Price";
            var result = Context.Database.GetDbConnection().Query<SellitemModelView>(query);
            return result.ToList();
        }
    }
}
