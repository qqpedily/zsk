using Com.Weehong.Elearning.MasterData;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using WebApiZSK.Filter;
using WebApiZSK.Models.Input;
using YinGu.Operation.Framework.Domain.Comm;

namespace WebApiZSK.Controllers
{
    [AllowAnonymous]

    public class TestController : ApiController
    {

        [HttpPost]
        public async Task<IHttpActionResult> SavaFiles()
        {
            OperationFiles operationFiles = new OperationFiles();

            // WebModelIsSucceed WebModelIsSucceed = 
            return Json(await operationFiles.SaveFile(Guid.NewGuid()));
        }
        /// <summary>
        /// 用户上传图片
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="localpath"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SavaUserFiles(Guid userid,string localpath)
        {
            OperationFiles operationFiles = new OperationFiles();

            // WebModelIsSucceed WebModelIsSucceed = 
            return Json(await operationFiles.SaveUserFile(userid, "C:\\Files\\user_img\\"));
        }

        //[HttpGet]
        //public IHttpActionResult Get()
        //{
        //    DataTable a = OperationData.Instance.RCC_DataTableByProduction(ProductionsFieldAdapter.Instance.GetAll().ToList());
        //    return Json(a);
        //}


        [HttpPost]
        public async Task<IHttpActionResult> GetAA([FromBody]LoginUser loginUser)
        {
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                string username = loginUser.UserName;
                string pwd = loginUser.Password;
                List<SYS_MetaData> a = await db.SYS_MetaData.ToListAsync();
                return Json(a);
            }

        }



    }
}