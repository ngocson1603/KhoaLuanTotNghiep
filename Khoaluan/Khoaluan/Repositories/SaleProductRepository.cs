using Dapper;
using Khoaluan.InterfacesRepository;
using Khoaluan.Models;
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
    }
}
