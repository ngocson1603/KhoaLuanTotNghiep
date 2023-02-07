using Khoaluan.Extension;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Khoaluan.Helpper
{
    public static class Utilities
    {
        public static string StripHTML(string input)
        {
            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    return Regex.Replace(input, "<.*?>", String.Empty);
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static int PAGE_SIZE = 20;
        public static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }

        public static string ToTitleCase(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join(" ", words);
            }
            return result;
        }

        public static bool IsInteger(string str)
        {
            Regex regex = new Regex(@"^[0-9]+$");

            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }

                return true;

            }
            catch
            {

            }
            return false;

        }

        public static string GetRandomKey(int length = 5)
        {
            //chuỗi mẫu (pattern)
            string pattern = @"0123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }

        public static string SEOUrl(string url)
        {
            url = url.ToLower();
            url = Regex.Replace(url, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẹẻẽêếềệểễ]", "e");
            url = Regex.Replace(url, @"[óòọỏõôốồộổỗơớờợởỡ]", "o");
            url = Regex.Replace(url, @"[íìịỉĩ]", "i");
            url = Regex.Replace(url, @"[ýỳỵỉỹ]", "y");
            url = Regex.Replace(url, @"[úùụủũưứừựửữ]", "u");
            url = Regex.Replace(url, @"[đ]", "d");

            //2. Chỉ cho phép nhận:[0-9a-z-\s]
            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();
            //xử lý nhiều hơn 1 khoảng trắng --> 1 kt
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            //thay khoảng trắng bằng -
            url = Regex.Replace(url, @"\s", "-");
            while (true)
            {
                if (url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;
        }

        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", newname);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower())) /// Khác các file định nghĩa
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch
            {
                return null;
            }
        }
        public static async Task<string> UploadFileBlog(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", sDirectory);
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", sDirectory, newname);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower())) /// Khác các file định nghĩa
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch
            {
                return null;
            }
        }
        public static void sendemaildev(string emailaddress, Developer dev)
        {
            if (emailaddress.Length == 0 || emailaddress == null)
            {
                return;
            }
            else
            {
                if (Khoaluan.Helpper.Utilities.IsValidEmail(emailaddress))
                {
                    SmtpClient client = new SmtpClient()
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential()
                        {
                            UserName = "sondovipro123@gmail.com",
                            Password = "caofqthenhkakkgl"
                        }
                    };
                    MailAddress fromemail = new MailAddress("sondovipro123@gmail.com", "Xin chao");
                    MailAddress toemail = new MailAddress(emailaddress, "someone");
                    MailMessage mess = new MailMessage()
                    {
                        From = fromemail,
                        Subject = "tài khoản của bạn",
                        IsBodyHtml = true,
                    };
                    mess.Body += "<h1>Xin chào:" + dev.Name + "</h1>";
                    mess.Body += "<h3>Tài khoản của bạn đã đăng kí thành công thành công<h3>";
                    mess.Body += "<h3>thông tin tài khoản</h3>";
                    mess.Body += "<table><thead>";
                    mess.Body += "<tr><th>Username</th><th>password</th></thead>";
                    mess.Body += "<tbody>";
                    mess.Body += "<tr><td>" + dev.UserName + "</td>" + "<td>" + dev.Passwork + "</td></tr>";
                    mess.Body += "</tbody></table>";
                    mess.To.Add(toemail);
                    client.Send(mess);
                }
                else
                {

                }
            }
        }

        public static void SendEmail(MailMessage message, string email)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "sondovipro123@gmail.com",
                    Password = "caofqthenhkakkgl"
                }
            };

            MailAddress fromEmail = new MailAddress("sondovipro123@gmail.com", "Admin Good Games");
            MailAddress toEmail = new MailAddress(email);

            message.From = fromEmail;
            message.To.Add(toEmail);
            client.Send(message);
        }

        public static void sendemailactivegame(ActiveGame pro)
        {
            if (pro.UserName.Length == 0)
            {

            }
            else
            {
                if (Khoaluan.Helpper.Utilities.IsValidEmail(pro.UserName))
                {
                    SmtpClient client = new SmtpClient()
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential()
                        {
                            UserName = "sondovipro123@gmail.com",
                            Password = "caofqthenhkakkgl"
                        }
                    };
                    MailAddress fromemail = new MailAddress("sondovipro123@gmail.com", "Xin chao");
                    MailAddress toemail = new MailAddress(pro.UserName, "someone");
                    MailMessage mess = new MailMessage()
                    {
                        From = fromemail,
                        Subject = "tài khoản của bạn",
                        IsBodyHtml = true,
                    };
                    string status;
                    if (pro.Status == 1)
                    {
                        status = "Approved game";
                    }
                    else
                    {
                        status = "The game is not approved";
                    }
                    mess.Body += "<h1>Xin chào:" + pro.NameDev + "</h1>";
                    mess.Body += "<h3>Thông tin duyệt game<h3>";
                    mess.Body += "<h3>thông tin game</h3>";
                    mess.Body += "<table><thead>";
                    mess.Body += "<tr><th>Name</th><th>Price</th><th>Status</th></thead>";
                    mess.Body += "<tbody>";
                    mess.Body += "<tr><td>" + pro.NamePro + "</td>" + "<td>" + pro.Price + "</td>" + "<td>" + status + "</td></tr>";
                    mess.Body += "</tbody></table>";
                    mess.To.Add(toemail);
                    client.Send(mess);
                }
                else
                {

                }
            }
        }

        public static void sendemailcontact(Contact contact)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "congty.gamests@gmail.com",
                    Password = "beshjrsdtncjimiv"
                }
            };
            MailAddress fromemail = new MailAddress("congty.gamests@gmail.com", "Xin chao");
            MailAddress toemail = new MailAddress("sondovipro123@gmail.com", "someone");
            MailMessage mess = new MailMessage()
            {
                From = fromemail,
                Subject = "tài khoản của bạn",
                IsBodyHtml = true,
            };

            mess.Body += "<h1>thông tin yêu cầu</h1>";
            mess.Body += "<h1>Name:" + contact.Name + "</h1>";
            mess.Body += "<h1>Email:" + contact.Email + "</h1>";
            mess.Body += "<h1>Message:" + contact.Message + "</h1>";

            mess.To.Add(toemail);
            client.Send(mess);
        }
        public static void sendemailuser(Contact contact)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "congty.gamests@gmail.com",
                    Password = "beshjrsdtncjimiv"
                }
            };
            MailAddress fromemail = new MailAddress("congty.gamests@gmail.com", "Thank you");
            MailAddress toemail = new MailAddress(contact.Email, "someone");
            MailMessage mess = new MailMessage()
            {
                From = fromemail,
                Subject = "Thank you",
                IsBodyHtml = true,
            };

            mess.Body += "<h1>Thank you for your feedback, we will get back to you soon</h1>";

            mess.To.Add(toemail);
            client.Send(mess);
        }

        public static string SetSizeImage(string source)
        {
            string final = source;
            IEnumerable<int> indexes = source.AllIndexesOf("width: ");
            foreach (var index in indexes)
            {
                int start, end;
                start = index + 6;
                end = source.IndexOf("px", start);
                int imgWidth = int.Parse(source.Substring(start, end - start));

                if (imgWidth > 840)
                    final = Regex.Replace(final, $"style=\"width: {imgWidth}px;\"", "style=\"width: 840px;\"");
            }

            return final;
        }
    }
}
