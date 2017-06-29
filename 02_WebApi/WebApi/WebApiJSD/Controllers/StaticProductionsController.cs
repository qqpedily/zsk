using Com.Weehong.Elearning.DataHelper;
using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiZSK.Models.Input;
using WebApiZSK.Models.Output;
using YinGu.Operation.Framework.Domain.Comm;
using YinGu.Operation.Framework.Domain.Productions;

namespace WebApiZSK.Controllers
{
    /// <summary>
    /// 作品文献静态表
    /// </summary>
    public class StaticProductionsController : ApiController
    {
        private readonly StaticProductionsAdapter staticProductionsAdapter = new StaticProductionsAdapter();


        /// <summary>
        /// 个人工作室-查询已提交文献
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> GetByUserID(Guid userID)
        {
            return Json(await staticProductionsAdapter.GetByUserID(userID));

        }
    }
}
       