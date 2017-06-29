using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApiZSK.Controllers
{
    /// <summary>
    /// 个人工作室-认领业务
    /// </summary>
    public class RelationUserClaimWorksController : ApiController
    {
        private readonly RelationUserClaimWorksAdapter relationUserClaimWorksAdapter = new RelationUserClaimWorksAdapter();
        /// <summary>
        /// 获取待认领数
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="chineseName"></param>
        /// <param name="englishName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadSheltersCount(Guid sysuserID, string chineseName, string englishName)
        {
            return Json(await relationUserClaimWorksAdapter.LoadSheltersCount(sysuserID, chineseName, englishName));
        }


        /// <summary>
        /// 获取未提交全文数
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadNotAttachmentCount(Guid sysuserID)
        {
            return Json(await relationUserClaimWorksAdapter.LoadNotAttachmentCount(sysuserID));
        }
    }
}