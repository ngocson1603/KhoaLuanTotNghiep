using Khoaluan.Enums;
using Khoaluan.Extension;
using Khoaluan.Helpper;
using Khoaluan.Interfaces;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using System;

namespace Khoaluan.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Users updateBalance(int userID, int price, int type)
        {
            Users user = _usersRepository.GetById(userID);
            if (type == (int)marketType.buy)
            {
                user.Balance = user.Balance - price;
            }
            else if (type == (int)marketType.sell)
            {
                user.Balance = user.Balance + price;
            }
            return user;
        }

        /// <summary>
        /// Đăng nhập tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// -1: Không tìm thấy tài khoản được đăng ký với địa chỉ email này
        ///  2: Đăng nhập thất bại
        ///  Các trường hợp còn lại: Đăng nhập thành công
        /// </returns>
        public int SignIn(LoginViewModel model)
        {
            Users user = _usersRepository.FindByEmail(model.Gmail);

            if (user == null)
                return -1;

            if (user.Password == (model.Password + user.Salt.Trim()).ToMD5())
                return user.Id;

            return 2;
        }

        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// -1: Địa chỉ email này đã được đăng ký tài khoản
        ///  0: Có lỗi trong quá trình đăng ký
        ///  1: Đăng ký thành công
        /// </returns>
        public int SignUp(RegisterViewModel model)
        {
            if (_usersRepository.FindByEmail(model.Email) != null)
                return -1;

            string salt = Utilities.GetRandomKey();

            Users account = new Users()
            {
                Gmail = model.Email,
                HoTen = model.FullName,
                Password = (model.Password + salt.Trim()).ToMD5(),
                Salt = salt
            };

            try
            {
                _usersRepository.Create(account);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Đổi mật khẩu tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// -2: Mật khẩu hiện tại không trùng khớp
        /// -1: Không tìm thấy tài khoản được đăng ký với địa chỉ email này
        ///  0: Có lỗi trong quá trình đổi mật khẩu
        ///  1: Đổi mật khẩu thành công
        /// </returns>
        public int ChangePassword(ChangePasswordViewModel model, int userId)
        {
            Users user = _usersRepository.GetById(userId);

            if (user == null)
                return -1;

            string oldPassword = (model.PasswordNow.Trim() + user.Salt.Trim()).ToMD5();
            string newPassword = (model.Password.Trim() + user.Salt.Trim()).ToMD5();

            if (!oldPassword.Equals(user.Password))
                return -2;

            user.Password = newPassword;

            try
            {
                _usersRepository.Update(user);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
