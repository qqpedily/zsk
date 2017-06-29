using Com.Weehong.Elearning.DataModels;
using Com.Weehong.Elearning.Domain.WebModel;
using Com.Weehong.Elearning.MasterData.Common;
using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApiZSK.Models.Input;
using WebApiZSK.Models.Output;
using YinGu.Operation.Framework.Domain.Comm;

namespace WebApiZSK.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [AllowAnonymous]

    public class AccountController : ApiController
    {
        Account accountModel = new Account();
        private UserAliasAdapter userAliasAdapter = new UserAliasAdapter();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginUser">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Logon([FromBody]LoginUser loginUser)
        {
            WebModelUser isSucceed = new WebModelUser();
            string strUserName = loginUser.UserName;
            string strPassword = loginUser.Password;
            try
            {
                //验证用户是否激活
                if (accountModel.EnableUser(strUserName))
                {
                    UserModel user = await accountModel.ValidateUserLogin(strUserName, strPassword);
                    // 验证用户是否是正确
                    if (user != null && user.UUID != null)
                    {
                        //创建用户ticket信息  
                        isSucceed.Ticket = accountModel.CreateLoginUserTicket(strUserName, strPassword);
                        isSucceed.IsSucceed = true;
                        isSucceed.User = await GetLoginUser(user);
                    }
                    else
                    {
                        isSucceed.IsSucceed = false;
                        isSucceed.ErrorMessage = "密码错误！";
                    }
                }
                else
                {
                    isSucceed.IsSucceed = false;
                    isSucceed.ErrorMessage = "用户名错误或用户未激活！";
                }
            }
            catch (Exception ex)
            {
                isSucceed.IsSucceed = false;
                isSucceed.ErrorMessage = ex.Message + "\r\n" + ex.StackTrace;
            }

            return Json(isSucceed);
        }

        /// <summary>
        /// 验证票据返回user数据
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> VerifyTicket(string ticket)
        {


            WebModelUser isSucceed = new WebModelUser();

            if (accountModel.VerifyTicket(ticket))
            {
                UserModel user = await UserAdapter.Instance.LoadByLoginUserAsync(HttpContext.Current.User.Identity.Name);

                isSucceed.User = await GetLoginUser(user);
                isSucceed.IsSucceed = true;
            }
            else
            {
                Logout();
                isSucceed.IsSucceed = false;
                isSucceed.ErrorMessage = "票据验证失败,请重新登陆!";
            }
            return Json(isSucceed);
        }

        private async Task<LoginUserTicket> GetLoginUser(UserModel user)
        {
            LoginUserTicket usertiket = new LoginUserTicket();
            AttachmentModel attachment = await AttachmentAdapter.Instance.LoadByOrderNumAsync(user.UUID);
            usertiket.ImgUserHead = attachment != null ? attachment.FileFullPath : null;

            if (userAliasAdapter.LoadByUserID(user.UUID) != null)
            {
                usertiket.AliasName = userAliasAdapter.LoadByUserID(user.UUID).AliasName;
            }



            usertiket.UserID = user.UUID;
            usertiket.NameChinese = user.NameChinese;
            usertiket.SurnameChinese = user.SurnameChinese;
            usertiket.SurnamePhoneticize = user.SurnamePhoneticize;
            usertiket.NamePhoneticize = user.NamePhoneticize;


            return usertiket;
        }

        /// <summary>  
        /// 用户注销，注销之前，清除用户ticket  
        /// </summary>  
        /// <returns></returns>  
        [HttpPost]
        public IHttpActionResult Logout()
        {
            WebModelUser isSucceed = new WebModelUser();
            accountModel.Logout();
            isSucceed.IsSucceed = true;
            return Json(isSucceed);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody]RegisterUser registerUser)
        {
            WebModelIsSucceed model = new WebModelIsSucceed();

            UserAdapter userAdapter = new UserAdapter();
            string userEmail = registerUser.UserEmail;
            UserModel user = await userAdapter.LoadUserByUserEmail(userEmail);

            string telphone = registerUser.Telphone;

            UserModel userph = await userAdapter.LoadUserByUserPhone(telphone);

            int result = 0;


            //验证电话号
            if (userph != null && !string.IsNullOrEmpty(userph.Telphone))
            {

                if (userph.IsLogin == 1)
                {
                    //未激活
                    userph.IsLogin = 0;
                    result = await userAdapter.AddOrUpdateAsync(userph);
                    if (result > 0)
                    {
                        model.IsSucceed = true;
                    }
                    else
                    {
                        model.IsSucceed = false;
                        model.ErrorMessage = "用户激活失败!";
                    }
                }
                else
                {
                    //已激活
                    model.IsSucceed = false;
                    model.ErrorMessage = "此电话号码已激活,无需注册!";
                }
            }

            //验证邮箱
            else if (user != null && !string.IsNullOrEmpty(user.UserEmail))
            {
                if (user.IsLogin == 1)
                {
                    //未激活
                    user.IsLogin = 0;
                    result = await userAdapter.AddOrUpdateAsync(user);
                    if (result > 0)
                    {
                        model.IsSucceed = true;
                    }
                    else
                    {
                        model.IsSucceed = false;
                        model.ErrorMessage = "用户激活失败!";
                    }
                }
                else
                {
                    //已激活
                    model.IsSucceed = false;
                    model.ErrorMessage = "此邮箱已激活,无需注册!";
                }
            }
            else
            {
                //邮箱未存在,在判断姓名和部门
                UserModel userUnit = await userAdapter.LoadByUserNameAndUnit(registerUser.SurnameChinese, registerUser.NameChinese, registerUser.UnitID);
                if (userUnit != null && !string.IsNullOrEmpty(Convert.ToString(userUnit.UnitID)))
                {
                    //姓名和部门符合要求
                    if (userUnit.IsLogin == 1)
                    {
                        //未激活
                        userUnit.IsLogin = 0;
                        result = await userAdapter.AddOrUpdateAsync(userUnit);
                        return Json(userUnit);
                    }
                    else
                    {
                        //已激活
                        model.IsSucceed = false;
                        model.ErrorMessage = "此姓名和部门已被注册过，无法注册！";
                    }
                }
                else
                {
                    //添加数据
                    user = new UserModel();
                    user.UUID = Guid.NewGuid();
                    user.UserEmail = registerUser.UserEmail;
                    user.Telphone = registerUser.Telphone;
                    user.SurnameChinese = registerUser.SurnameChinese;
                    user.SurnamePhoneticize = registerUser.SurnamePhoneticize;
                    user.NameChinese = registerUser.NameChinese;
                    user.NamePhoneticize = registerUser.NamePhoneticize;
                    user.PassWord = MD5Method.Instance.MD5Encrypt(registerUser.Password);
                    user.UnitID = registerUser.UnitID;
                    user.DeptID = registerUser.DeptID;
                    user.PttID = registerUser.PttID;
                    user.ItID = registerUser.ItID;

                    result = await userAdapter.AddOrUpdateAsync(user);
                    if (result > 0)
                    {
                        model.IsSucceed = true;
                        return Json(user);
                    }
                    else
                    {
                        model.IsSucceed = false;
                        model.ErrorMessage = "插入数据有误!";
                    }
                }

            }
            return Json(model);
        }


        [HttpPost]
        public IHttpActionResult Get([FromBody]LoginUser loginUser)
        {
            //LoginUser LoginUser = new LoginUser();
            //LoginUser.UserName = "aaaa";
            //LoginUser.Password = "123123";
            return Json(loginUser.UserName + loginUser.Password);
        }

    }
}