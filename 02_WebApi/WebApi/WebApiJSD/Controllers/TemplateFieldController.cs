using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysManage;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using Com.Weehong.Elearning.MasterData.Repositories;
using Com.Weehong.Elearning.MasterData.Common;

namespace WebApiZSK.Controllers
{
    /// <summary>
    /// 模板字段
    /// </summary>
    [AllowAnonymous]
    public class TemplateFieldController : ApiController
    {

        private readonly SYS_TemplateFieldAdapter sys_TemplateFieldModelAdapter = new SYS_TemplateFieldAdapter();

        private readonly SYS_TemplateAdapter sys_TemplateAdapter = new SYS_TemplateAdapter();

        private readonly ProductionsAdapter productionsAdapter = new ProductionsAdapter();

        private readonly ProductionsFieldAdapter productionsFieldAdapter = new ProductionsFieldAdapter();


        /// <summary>
        ///获取模板ID或者模板所有元素
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllTemplate()
        {
            return Json(sys_TemplateAdapter.GetAll().ToList());
        }

        /// <summary>
        ///获取模板ID或者模板所有元素
        /// </summary>
        /// <param name="templateID">模板ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMateDataByTemplateID(Guid templateID)
        {
            return Json(sys_TemplateFieldModelAdapter.GetAll().Where(w => w.TemplateID == templateID).ToList());
        }
        /// <summary>
        ///获取所有模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTemplateList()
        {
            return Json(sys_TemplateFieldModelAdapter.GetAll());
        }




        /// <summary>
        /// 个人作品统计-个人作品统计 
        /// 个人作品统计所有模板内容类型和该内容类型下的所有文献and数量
        /// </summary>
        /// <param name="startime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns>个人作品统计所有模板内容类型和该内容类型下的所有文献and数量</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSYS_TemplateContentTypeListAndCountByTime(string startime, string endtime)
        {
            using (var db = new OperationManagerDbContext())
            {
                List<SYS_Template> list = await db.SYS_Template.ToListAsync();
                List<Dictionary<String, Object>> dicList = new List<Dictionary<string, object>>();
                foreach (var sale in list)
                {
                    Dictionary<String, Object> dic = new Dictionary<string, object>();
                    List<StaticProductions> lic = await productionsFieldAdapter.LoadContentListByTimeAndUserID(sale.TemplateID, startime, endtime);
                  
                    dic.Add("ContentTypeID", "TemplateID");
                    dic.Add("ContentTypeValue", sale.TemplateID);
                    dic.Add("ContentType", sale.ContentType);
                    dic.Add("ContentTypeCount", lic.Count);
                  

                    dicList.Add(dic);
                }



                return Json(dicList);
            }
           
        }

        /// <summary>
        /// 研究单元-内容类型
        ///获取所有模板的内容类型,该内容类型下的所有文献数量
        /// </summary>
        /// <param name="userID">userID,登陆用户ID</param>
        /// <returns>所有模板内容类型和该内容类型下的所有文献and数量</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSYS_TemplateContentTypeListAndCount()
        {

            //var info = CacheClass.GetCache("contentType");
            //if (info == null)
            //{
            //    CacheClass.SetCache("contentType", sys_TemplateFieldModelAdapter.GetSYS_TemplateContentTypeListAndCount(userID));
            //    info = CacheClass.GetCache("contentType");
            //}

            //return Json(info);

            return Json(await sys_TemplateFieldModelAdapter.GetSYS_TemplateContentTypeListAndCount());
        }


        /// <summary>
        /// 研究单元-发表日期
        /// 获取所有年份下的所有文献数量
        /// </summary>
        /// <param name="userID">userID,登陆用户ID</param>
        /// <returns>所有年份，所有年份下的文献and数量</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSYS_YearListAndUserCount(Guid userID)
        {
           // var info = CacheClass.GetCache("ContentYear");
            //if (info == null)
            //{
            //    CacheClass.SetCache("ContentYear", sys_TemplateFieldModelAdapter.GetSYS_YearListAndCount(userID));
            //    info = CacheClass.GetCache("ContentYear");
            //}
           
            //return Json(info);
            return Json(await sys_TemplateFieldModelAdapter.GetSYS_YearListAndCount(userID));
        }
        /// <summary>
        ///  研究单元-发表日期
        ///  获取所有年份下的所有文献数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSYS_YearListAndCount()
        {
            return Json(await sys_TemplateFieldModelAdapter.GetSYS_YearListAndCount());
        }

        /// <summary>
        /// 研究单元-语言
        ///获取所有语言下的所有文献数量
        /// </summary>
        /// <returns>所有语言，所有语言下的文献数量</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSYS_LanguageListAndCount()
        {
            //var info = CacheClass.GetCache("languageConut");
            //if (info == null)
            //{
            //    CacheClass.SetCache("languageConut", await sys_TemplateFieldModelAdapter.GetSYS_LanguageListAndCount());
            //    info = CacheClass.GetCache("languageConut");
            //}

            //return Json(info);
            return Json(await sys_TemplateFieldModelAdapter.GetSYS_LanguageListAndCount());
        }

        /// <summary>
        /// 研究单元-收录类别
        ///获取所有收录类别下的所有文献数量
        /// </summary>
        /// <returns>所有收录类别，收录类别下的文献数量</returns>
        [HttpGet]
        public IHttpActionResult GetSYS_IndexedTypeListAndCount()
        {
           return Json( sys_TemplateFieldModelAdapter.GetSYS_IndexedTypeListAndCount());


            //var info = CacheClass.GetCache("collectType");
            //if (info == null)
            //{
            //    CacheClass.SetCache("collectType", await sys_TemplateFieldModelAdapter.GetSYS_IndexedTypeListAndCount());
            //    info = CacheClass.GetCache("collectType");
            //}

            //return Json(info);
        }


        /// <summary>
        /// 研究单元-是否有全文
        ///获取所有收录类别下的是否有全文
        /// </summary>
        /// <returns>所有收录类别，是否有全文</returns>
        [HttpGet]
        public IHttpActionResult GetSYS_AttachmentListAndCount()
        {
            return Json(sys_TemplateFieldModelAdapter.GetSYS_AttachmentListAndCount());
        }
    }
}
