using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Khoaluan.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public DbContext Context { get; }
        public ProductService(IProductRepository productRepository,GameStoreDbContext context)
        {
            _productRepository = productRepository;
            Context = context;
        }

        public void ReleaseProduct()
        {
            var result=_productRepository.listProductRelease();
            result.ForEach(t => t.Status = (int)productType.release);
            Context.BulkUpdate(result);
        }

        public bool IsGameCanBuy(int UserId, int ProductId)
        {
            var result=_productRepository.IsRefundGame(UserId, ProductId);
            if(result==null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
