using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiZSK.Models.Output;

namespace WebApiZSK.Controllers
{
    public class MainUserController : ApiController
    {

        private readonly MainUserAdapter mainuser = new MainUserAdapter();


        /// <summary>
        /// 个人详细信息-内容类型
        /// </summary>
        /// <param name="username">作者</param>
        /// <returns>内容类型文数量</returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadContentListCount(string username) {
            var dicList = mainuser.LoadContentListCount(username);

            return Json(dicList);

        }


        /// <summary>
        /// 个人详细信息-发表日期
        ///获取所有年份下的所有文献数量
        /// </summary>
        /// <param name="username">作者</param>
        /// <returns>所有年份，所有年份下的文献and数量</returns>
        [HttpGet]
        public IHttpActionResult GetSYS_YearListAndCountAsync(string username)
        {
            // SqlCacheDependency
            var dicList = mainuser.GetSYS_YearListAndCountAsync(username);

            return Json(dicList);
        }
        /// <summary>
        /// 个人详细信息-语言
        ///获取所有语言下的所有文献数量
        /// </summary>
        /// <param name="username">作者的名字</param>
        /// <returns>所有语言，所有语言下的文献数量</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSYS_LanguageListAndCount(string username)
        {

            return Json(mainuser.GetSYS_LanguageListAndCount(username));
        }

        /// <summary>
        /// 个人详细信息-收录类别
        ///获取个人所有模板的收录类别,该收录类别的所有文献数量
        /// </summary>
        /// <param name="username">作者的名字</param>
        /// <returns>所有模板收录类别和该收录类别下的所有文献and数量</returns>

        //E134A22A-4187-4318-BB70-BCC66711ED1D 收录类型
        //50883877-E367-4D5B-85FD-5F15A5B2E789 作者
        [HttpGet]
        public async Task<IHttpActionResult> GetUserIndexedTypeListAndCount(string username)
        {
            var list = mainuser.GetUserIndexedTypeListAndCount(username);

            return Json(list);
        }


        /// <summary>
        /// 个人详细信息-是否有全文
        ///获取是否有全文
        /// </summary>
        /// <returns>所有是否有全文</returns>

        public IHttpActionResult GetSYS_AttachmentCountByUser(string username) {
            var list = mainuser.GetSYS_AttachmentCountByUser(username);

            return Json(list);
        }


        /// <summary>
        /// 首页-根据院系ID 获取文献信息
        /// </summary>
        /// <param name="cid">院系ID</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns>所有文献and数量</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetProductionsByCID(string cid, int pageSize, int curPage)
        {

            var list = await mainuser.GetProductionsByCID(cid, pageSize, curPage);
            return Json(list);

        }

        /// <summary>
        /// 根据bussinessid 获取附件
        /// </summary>
        /// <param name="busssinessid">busssinessid</param>
        /// <returns>附件</returns>
        [HttpGet]
        public IHttpActionResult GetAttachmentByBusssinessid(string busssinessid)
        {
            AttachmentAdapter adapter = new AttachmentAdapter();
            var info = adapter.LoadByProductionsAsync(busssinessid);
            return Json(info);
        }


        /// <summary>
        /// 获取推荐学者分页数据
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="curPage">第几页</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserList(int pageSize, int curPage)
        {
            MainUserAdapter mainadapter = new MainUserAdapter();
            var info = mainadapter.GetUserList(pageSize, curPage);
            return Json(info);
        }


        /// <summary>
        /// 首页获取推荐学者分页数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetIndexUserList()
        {
            using (var db = new OperationManagerDbContext())
            {
                //                string sql = @"
                //SELECT TOP 8 u.UUID,SurnameChinese,NameChinese,
                //(SELECT COUNT(-1) FROM (
                //SELECT  s.ProductionID FROM dbo.StaticProductions s WHERE s.UserID=u.UUID
                //   UNION
                //    SELECT ruc.ProductionID FROM  Relation_UserClaimWorks AS ruc 
                //   WHERE ruc.SysUserID=u.UUID
                //   )t 
                //) totalcount,up.UploadIMG
                //FROM dbo.[User] u LEFT JOIN dbo.UserPersonalHomepage up ON up.UserID = u.UUID
                //WHERE IsRecommend =1 ORDER BY OrderBy";
                string sql = @"

SELECT TOP 8 u.UUID,SurnameChinese,NameChinese,
(SELECT COUNT(-1) FROM (
SELECT  s.ProductionID FROM dbo.StaticProductions s WHERE CHARINDEX(REPLACE(u.SurnameChinese+u.NameChinese,' ',''),s.author)>0
   )t 
) totalcount,up.UploadIMG
FROM dbo.[User] u LEFT JOIN dbo.UserPersonalHomepage up ON up.UserID = u.UUID
WHERE IsRecommend =1 ORDER BY OrderBy";
                List<IndexUser> list = await db.Database.SqlQuery<IndexUser>(sql).ToListAsync();

                return Json(list);
            }
        }


        /// <summary>
        /// 根据用户ID查询用户联系信息,获取头像信息等
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult LoadUserPersonalHomepageByUseriD(string userID)
        {
            AttachmentAdapter att = new AttachmentAdapter();
            return Json(att.LoadUserPersonalHomepageByUseriD(userID));
        }
    }
}
