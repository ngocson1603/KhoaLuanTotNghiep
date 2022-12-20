using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.InterfacesRepository;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Khoaluan.Services
{
    public class RefundService : IRefundService
    {
        private readonly IRefundRepository _refundRepository;
        private readonly IUserService _userService;
        private readonly IUsersRepository _usersRepository;
        public DbContext Context { get; }
        public RefundService(IRefundRepository refundRepository, 
            IUserService userService, 
            IUsersRepository usersRepository,
            GameStoreDbContext context)
        {
            _refundRepository = refundRepository;
            _userService = userService;
            _usersRepository = usersRepository;
            Context = context;
        }

        public Refund refund(int userID, int productID)
        {
            RefundRequest request=_refundRepository.refundRequest(productID,userID);
            Refund rf=new Refund()
            {
                UserID=userID,
                OrderID=request.OrderID,
                ProductID=productID,
                Price=request.Price,
                Status=(int)RefundType.pending,
                DatePurchase=request.DatePurchase,
                DateCreate=DateTime.Now
            };
            return rf;
        }
        public void refundtoallUser()
        {
            var userRefunds=_refundRepository.GetRefundUsers();
            foreach(var user in userRefunds)
            {
                var userRefund = _userService.updateBalance(user.UserID, user.Price, (int)marketType.buy);
                var rf = _refundRepository.GetById(user.Id);
                rf.Status = (int)RefundType.accept;
                _usersRepository.Update(userRefund);
                _refundRepository.Update(rf);
            }
            Context.SaveChanges();
        }
    }
}
