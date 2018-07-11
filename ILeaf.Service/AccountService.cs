using ILeaf.Core;
using ILeaf.Core.Config;
using ILeaf.Core.Enums;
using ILeaf.Core.Extensions;
using ILeaf.Core.Models;
using ILeaf.Core.Utilities;
using ILeaf.Repository;
using StructureMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace ILeaf.Service
{
    public interface IAccountService : IBaseService<Account>
    {
        /// <summary>
        /// 检验用户名是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool CheckUserNameExisted(long id, string userName);
        bool CheckEmailExisted(long id, string email);
        /// <summary>
        /// 根据用户名获取账号信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Account GetAccount(string userName);
        Account GetAccount(string userName, string password);
        Account GetAccountByWeixinOpenId(string openId);
        void Login(string userName, bool rememberMe, IEnumerable<string> roles, bool recordLoginInfo);
        Account TryLogin(string userNameOrEmailOrPhone, string password, bool rememberMe, bool recordLoginInfo);
        void Logout();
        bool CheckPassword(string userName, string password);
        Account CreateAccount(string userName, string email, string phone, string password, string weixinOpenId);

        /// <summary>
        /// 未验证邮箱的用户通过验证记录邮箱
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="accountId"></param>
        void EMailCheckPass(string email, int accountId);
        
        /// <summary>
        /// 获取头像路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetAvatarPath(long id);
    }

    public class AccountService : BaseService<Account>, IAccountService
    {
        public AccountService(IAccountRepository accountRepo)
            : base(accountRepo)
        {
        }

        public bool CheckUserNameExisted(long id, string userName)
        {
            return
                this.GetObject(
                    z => z.Id != id && z.UserName.Equals(userName.Trim(), StringComparison.CurrentCultureIgnoreCase)) !=
                null;
        }

        public bool CheckEmailExisted(long id, string email)
        {
            return
                this.GetObject(
                    z => z.Id != id && z.Email.Equals(email.Trim(), StringComparison.CurrentCultureIgnoreCase)) != null;
        }
        

        public Account GetAccount(string userName)
        {
            userName = userName.Trim();
            return this.GetObject(z => z.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)
                                       || z.Email.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
        }

        public Account GetAccount(string userName, string password)
        {
            userName = userName.Trim();
            Account account =
                this.GetObject(z => z.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)
                                    || z.Email.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            if (account == null)
            {
                return null;
            }
            return PasswordEncrypter.CheckPassword(password, account.PasswordSalt, account.EncryptedPassword) ? account : null;
        }

        public Account GetAccountByWeixinOpenId(string openId)
        {
            return GetObject(z => z.WeChatOpenIdPresent && z.WeChatOpenId == openId);
        }
        

        public virtual void Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Remove("UserId");
                HttpContext.Current.Session.Remove("UserType");
                HttpContext.Current.Session.Remove("IsAdmin");
            }
            catch (Exception ex)
            {
                Logger.Account.Error("Unable to log out: ", ex);
            }
        }

        public virtual void Login(string userName, bool rememberMe, IEnumerable<string> roles, bool recordLoginInfo)
        {
            FormsAuthentication.SetAuthCookie(userName, rememberMe);
            
            if (recordLoginInfo)
            {
                var account = this.GetAccount(userName);
                if (account != null)
                {
                    account.LastLoginTime = account.ThisLoginTime;
                    account.LastLoginIP = account.ThisLoginIP;
                    account.ThisLoginTime = DateTime.Now;
                    account.ThisLoginIP = HttpContext.Current != null ? HttpContext.Current.Request.UserHostName : "";
                    this.SaveObject(account); 
                }
            }
        }


        public bool CheckPassword(string userName, string password)
        {
            var account = this.GetObject(z => z.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (account == null)
            {
                return false;
            }
            return PasswordEncrypter.CheckPassword(password, account.PasswordSalt, account.EncryptedPassword);
        }

        public Account TryLogin(string userNameOrEmailOrPhone, string password, bool rememberMe, bool recordLoginInfo)
        {
            Account account = this.GetAccount(userNameOrEmailOrPhone, password);
            if (account != null)
            {
                this.Login(account.UserName, rememberMe, null, recordLoginInfo);
                HttpContext.Current.Session["UserId"] = account.Id;
                HttpContext.Current.Session["UserType"] = (UserType)account.UserType;
                HttpContext.Current.Session["IsAdmin"] = account.IsAdmin;
                return account;
            }
            else
            {
                return null;
            }
        }

        public Account CreateAccount(string userName, string email, string phone, string password, string weixinOpenId)
        {
            var passwordSalt = DateTime.Now.Ticks.ToString();
            var account = new Account()
            {
                UserName = userName,
                WeChatOpenIdPresent = weixinOpenId.IsNullOrEmpty(),
                WeChatOpenId = weixinOpenId,
                Email = email,
                EmailPresent = email.IsNullOrEmpty(),
                EncryptedPassword = PasswordEncrypter.EncryptPasswordForStorage(password, passwordSalt),
                PasswordSalt = passwordSalt,
                RegisterTime = DateTime.Now,
                Gender = (int)Gender.Undefined,
                ThisLoginTime = DateTime.Now,
                LastLoginTime = DateTime.Now,
                UserType = (int)UserType.Uncomplete,
            };

            this.SaveObject(account);
            return account;
        }

        /// <summary>
        /// 自动生成新的用户名
        /// </summary>
        /// <returns></returns>
        private string GetNewUserName()
        {
            string userName;
            Account account;

            do
            {
                userName = "iLeaf_{0}".With(Guid.NewGuid().ToString("n").Substring(0, 8));
                account = this.GetAccount(userName);
            } while (account != null);

            return userName;
        }
        
        /// <summary>
        /// 下载图片到指定文件
        /// </summary>
        /// <param name="picUrl"></param>
        /// <param name="fileName"></param>
        private void DownLoadPic(string picUrl, string fileName)
        {

            FileStream fs = new FileStream(Server.GetMapPath("~" + fileName), FileMode.CreateNew);
            HttpWebRequest request = WebRequest.Create(picUrl) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();

            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                fs.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            fs.Close();
            responseStream.Close();
        }

        public void EMailCheckPass(string email, int accountId)
        {
            var account = GetObject(accountId);

            account.Email = email;
            account.EmailPresent = true;

            SaveObject(account);
        }

        public string GetAvatarPath(long id)
        {
            if (id <= 0)
            {
                return SiteConfig.DEFAULT_AVATAR;
            }

            var account = this.GetObject(id);
            if (account != null)
            {
                return account.HeadImg;
            }
            else
            {
                return SiteConfig.DEFAULT_AVATAR;
            }
            
        }

        public override void SaveObject(Account obj)
        {
            var isInsert = base.IsInsert(obj);
            base.SaveObject(obj);
            Logger.WebLogger.InfoFormat("Account {2}：{0}（ID：{1}）", obj.UserName, obj.Id, isInsert ? "Added" : "Edited");
            
        }


        public override void DeleteObject(Account obj)
        {
            if (HttpContext.Current == null
                || !HttpContext.Current.User.Identity.IsAuthenticated
                || (bool)HttpContext.Current.Session["IsAdmin"])
            {
                Logger.SystemLogger.WarnFormat("Attemption to delete Account failed！IP：{0} / {1}", HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.UserHostName);
                

                throw new Exception("您的权限不够！此次操作所有信息已被记录！");
            }

            base.DeleteObject(obj);
            Logger.WebLogger.InfoFormat("Account deleted：{0}（ID：{1}）", obj.UserName, obj.Id);

            
        }
    }
}
