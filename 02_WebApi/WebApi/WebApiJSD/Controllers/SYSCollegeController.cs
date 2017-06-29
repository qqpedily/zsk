using Com.Weehong.Elearning.MasterData;
using Com.Weehong.Elearning.MasterData.Common;
using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysManage;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using WebApiZSK.Filter;
using WebApiZSK.Models.Output;
using YinGu.Operation.Framework.Domain.Comm;
using System.Web.Caching;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;
using Com.Weehong.Elearning.Domain.WebModel;

namespace WebApiZSK.Controllers
{


    /// <summary>
    /// 院系管理
    /// </summary>
    [AllowAnonymous]

    public class SYSCollegeController : ApiController
    {
        SYS_CollegeAdapter adapter = new SYS_CollegeAdapter();

        /// <summary>
        ///获取所有 院系类型是 1 的院系数量  首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetIndexList()
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT t.CID,t.CollegeName,COUNT(t.ProductionID) AS SysCollegeCount FROM 
(SELECT U.UnitID AS CID,CollegeName,RU.ProductionID FROM dbo.SYS_College sysCollege LEFT JOIN  dbo.[User] U ON  U.UnitID=sysCollege.CID LEFT JOIN dbo.Relation_UserClaimWorks RU ON RU.SysUserID=U.UUID 
WHERE sysCollege.CollegeType=1
GROUP BY U.UnitID,CollegeName,RU.ProductionID) t
GROUP BY t.CID,t.CollegeName  ORDER BY t.CollegeName";

                List<CollegeIndexList> list = await db.Database.SqlQuery<CollegeIndexList>(sql).ToListAsync();

                return Json(list);
            }
        }
        /// <summary>
        ///获取所有 院系类型是 1 的院系
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSYSCollegeList()
        {

            // var list = await adapter.GetSYSCollegeList();
            // return Json(list);

            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT c.CID,CollegeName,COUNT(oneP.ProductionID) AS OneCount,COUNT(twoP.ProductionID) AS TwoCount
FROM SYS_College c LEFT JOIN dbo.StaticProductions oneP ON c.CID=oneP.jigouyuanxiCode 
 LEFT JOIN dbo.StaticProductions twoP ON twoP.jigouyuanxiCode IN (SELECT CID FROM dbo.SYS_College cc WHERE cc.ParentID=c.CID)
WHERE CollegeType=1 
GROUP BY c.CID,CollegeName   ORDER BY c.CollegeName";

                List<CollegeList> list = await db.Database.SqlQuery<CollegeList>(sql).ToListAsync();

                return Json(list);
            }
        }


        /// <summary>
        ///获取 院系 根据 cid，
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSYSCollegeByID(Guid cid)
        {
            var list = await adapter.GetSYSCollegeByID(cid);

            //return js(list);
            return Json(list);
        }

      

        /// <summary>
        ///根据父级id获取所有 院系类，
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> getSYSCollegeListByParentID(string parentid)
        {
            var list = await adapter.GetListByParentIDAsync(parentid);

            return Json(list);
        }

        /// <summary>
        ///获取所有 院系类型是 1 的院系  注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSYSCollegeListIndex()
        {
            var list = await adapter.GetSYSCollegeListIndex();
            return Json(list);
        }

        /// <summary>
        ///添加或者修改院系
        /// </summary>
        /// <returns></returns>
        [HttpPost]

        public IHttpActionResult AddOrUpdateSYSCollege([FromBody]SYS_College syscollege) {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //用户            
                SYS_College college = adapter.GetAll().Where(w => w.CID == syscollege.CID).FirstOrDefault();
                if (college == null)//添加
                {
                    college.CID= Guid.NewGuid();
                }
            
                int i = adapter.AddOrUpdate(syscollege);
                if (i > 0)
                {
                    isSucceed.IsSucceed = true;
                }
                else
                {
                    isSucceed.IsSucceed = false;
                    isSucceed.ErrorMessage = "无操作记录!";
                }
                return Json(isSucceed);
            }
            catch (Exception ex)
            {
                isSucceed.IsSucceed = false;
                isSucceed.ErrorMessage = ex.Message + "\r\n" + ex.StackTrace;
                return Json(isSucceed);
            }

        }
    }
}