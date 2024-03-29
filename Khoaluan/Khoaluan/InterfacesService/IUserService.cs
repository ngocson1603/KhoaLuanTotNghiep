﻿using Khoaluan.Models;
using Khoaluan.ModelViews;

namespace Khoaluan.InterfacesService
{
    public interface IUserService
    {
        Users updateBalance(int userID, decimal price, int type);
        int SignIn(LoginViewModel model);
        int SignUp(RegisterViewModel model);
        int ChangePassword(ChangePasswordViewModel model, int accountId);
        bool SendVerification(int userId, string verifyCode);
    }
}
