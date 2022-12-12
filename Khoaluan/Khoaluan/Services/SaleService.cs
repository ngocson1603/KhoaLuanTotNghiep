using Khoaluan.InterfacesRepository;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using System.Collections.Generic;
using System.Linq;

namespace Khoaluan.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleProductRepository _SaleProductRepository;
        public SaleService(ISaleProductRepository saleProductRepository)
        {
            _SaleProductRepository = saleProductRepository;
        }
        public Sale CreateSale(SaleRequest request,out string message)
        {
            var currentSaleProduct = _SaleProductRepository.currentSaleProduct();
            var productIsSale=request.SaleProductRequests.Select(t=>t.ProductID).Intersect(currentSaleProduct);
            if(productIsSale.Count()>0)
            {
                message = "Product ";
                foreach(var p in productIsSale)
                {
                    message = message + " " + p.ToString() + " ";
                }
                message = message + "is sale";
                return null;
            }
            List<SaleProduct> saleProducts = new List<SaleProduct>();
            foreach(var x in request.SaleProductRequests)
            {
                SaleProduct sp = new SaleProduct()
                {
                    ProductID=x.ProductID,
                    Discount=x.Discount
                };
            }
            try
            {
                Sale Sale = new Sale()
                {
                    Name=request.Name,
                    StartDate=request.StartDate,
                    EndDate=request.EndDate,
                    SaleProducts=saleProducts
                };
                message = "Sale create success";
                return Sale;
            }
            catch
            {
                message = "Fail to create Sale";
                return null;
            }
        }
    }
}
