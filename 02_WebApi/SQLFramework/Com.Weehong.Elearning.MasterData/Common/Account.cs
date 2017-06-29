using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Data.Entity;
using Com.Weehong.Elearning.MasterData.DataModels.Users;

namespace Com.Weehong.Elearning.MasterData.Common
{
    /// <summary>
    /// 用户账户
    /// </summary>
    public class Account
    {
        UserAdapter userAdapter = new UserAdapter();

        /// <summary>
        /// 验证票据
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public bool VerifyTicket(string ticket)
        {
            FormsAuthenticationTicket ticketUser = FormsAuthentication.Decrypt(ticket);
            //重写HttpContext中的用户身份，可以封装自定义角色数据；  
            //判断是否合法用户，可以检查：HttpContext.User.Identity.IsAuthenticated的属性值  
            string[] roles = ticketUser.UserData.Split(',');
            IIdentity identity = new FormsIdentity(ticketUser);
            IPrincipal principal = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = principal;
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        /// <summary>  
        /// 创建登录用户的票据信息  
        /// </summary>  
        /// <param name="strUserName"></param>  
        /// <param name="strPassword"></param>  
        public string CreateLoginUserTicket(string strUserName, string strPassword)
        {
            //构造Form验证的票据信息  
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, strUserName, DateTime.Now, DateTime.Now.AddMinutes(1440),
                true, string.Format("{0}:{1}", strUserName, strPassword));

            string ticString = FormsAuthentication.Encrypt(ticket);

            //FormsAuthenticationTicket a = FormsAuthentication.Decrypt(ticString);

            //把票据信息写入Cookie和Session  
            //SetAuthCookie方法用于标识用户的Identity状态为true  
           // HttpContext.Current.Response.Cookies.Add(new HttpCookie("ZSKUsesr", ticString));

            //FormsAuthentication.SetAuthCookie(strUserName, true);
            //HttpContext.Current.Session["USER_LOGON_TICKET"] = ticString;



            //重写HttpContext中的用户身份，可以封装自定义角色数据；  
            //判断是否合法用户，可以检查：HttpContext.User.Identity.IsAuthenticated的属性值  
            string[] roles = ticket.UserData.Split(',');
            IIdentity identity = new FormsIdentity(ticket);
            IPrincipal principal = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = principal;
            return ticString;
        }

        /// <summary>  
        /// 获取用户权限列表数据  
        /// </summary>  
        /// <param name="userName"></param>  
        /// <returns></returns>  
        //internal string GetUserAuthorities(string userName)
        //{
        //从WebApi 访问用户权限数据，然后写入Session  
        //string jsonAuth = "[{\"Controller\": \"SampleController\", \"Actions\":\"Apply,Process,Complete\"}, {\"Controller\": \"Product\", \"Actions\": \"List,Get,Detail\"}]";
        //var userAuthList = ServiceStack.Text.JsonSerializer.DeserializeFromString(jsonAuth, typeof(UserAuthModel[]));
        //HttpContext.Current.Session["USER_AUTHORITIES"] = userAuthList;

        //return jsonAuth;
        // }

        /// <summary>  
        /// 读取数据库用户表数据，判断用户密码是否匹配  
        /// </summary>  
        /// <param name="name"></param>  
        /// <param name="password"></param>  
        /// <returns></returns>  
        public async Task<UserModel> ValidateUserLogin(string name, string password)
        {
            string newPassword = MD5Method.Instance.MD5Encrypt(password);
            UserModel user = await userAdapter.LoadByLoginUserAsync(name, newPassword);
            return user;
        }

        /// <summary>
        /// 读取数据库用户是否激活
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool EnableUser(string name)
        {
            int result = userAdapter.GetAll().Where(w => (w.UserEmail == name || w.UserID == name || w.Telphone == name) && w.IsLogin == 0).Count();
            return result > 0 ? true : false;
        }


        /// <summary>  
        /// 用户注销执行的操作  
        /// </summary>  
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
