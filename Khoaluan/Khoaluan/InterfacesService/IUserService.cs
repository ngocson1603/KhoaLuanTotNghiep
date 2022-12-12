using Khoaluan.Models;
using Khoaluan.ModelViews;

namespace Khoaluan.InterfacesService
{
    public interface IUserService
    {
        Users updateBalance(int userID, int price, int type);
        int SignIn(LoginViewModel model);
        int SignUp(RegisterViewModel model);
        int ChangePassword(ChangePasswordViewModel model, int accountId);
        bool SendVerification(int userId);
    }
}
