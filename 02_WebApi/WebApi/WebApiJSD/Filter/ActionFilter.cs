using Com.Weehong.Elearning.MasterData.DataModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiZSK.Filter
{
    public class ActionFilter  : ActionFilterAttribute
    {
      
   
        public ActionFilter()
        {
          
        }
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
           var attrs = actionContext.ActionDescriptor.GetCustomAttributes<IgnoreAttribute>().ToList() ;
            if (attrs.Count == 1)//有IgnoreAttribute属性
            {
                //逻辑处理
            }

           


            var dic = actionContext.ActionArguments;
            //直接判断参数名 一定为token
            if (dic.Keys.Count(n => n == "token") > 0)
            {
                //判断用户名后 自己就会抛出异常
              //  var user = loginBll.getUserByToken(dic["token"].ToString());
               // actionContext.Request.Properties.Add("user", user);
            }
            base.OnActionExecuting(actionContext);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public UserModel UserLogin(string UserID,string PassWord)
        {
            UserModel userModel = new UserModel();

            //userModel =

            return userModel;
        }


    }
}