using Com.Weehong.Elearning.Domain.WebModel;
using Com.Weehong.Elearning.MasterData.Common;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysManage;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiZSK.Controllers
{
    /// <summary>
    /// 基础管理  基础数据管理
    /// </summary>
    [AllowAnonymous]
    public class SysManageController : ApiController
    {

        CacheClass cacheClass = new CacheClass();

        #region 身份类型

        /// <summary>
        /// 身份类型 获取所有身份类型列表
        /// 【by ZHL】
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSYSIdentityTypeList()
        {
            return Json(SYS_IdentityTypeAdapter.Instance.GetAll().ToList());
        }
       
        /// <summary>
        /// 获取身份类型详细信息  
        /// </summary>
        /// <param name="ItID">身份类型ItID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSYSIdentityTypeInfo(Guid ItID)
        {
            return Json(SYS_IdentityTypeAdapter.Instance.GetAll().Where(w => w.ItID == ItID).FirstOrDefault());
        }
        /// <summary>
        /// 新增/编辑 身份类型
        /// 【by ZHL】
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateSYSIdentityType([FromBody]SYS_IdentityType model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                SYS_IdentityType sYS_IdentityTypeModel = SYS_IdentityTypeAdapter.Instance.GetAll().Where(w => w.ItID == model.ItID).FirstOrDefault();
                if (sYS_IdentityTypeModel == null)//添加
                {
                    sYS_IdentityTypeModel = new SYS_IdentityType();
                    sYS_IdentityTypeModel.ItID = Guid.NewGuid();
                    sYS_IdentityTypeModel.DelFlag = 1;
                }
                sYS_IdentityTypeModel.IdentityTypeName = model.IdentityTypeName;
                sYS_IdentityTypeModel.IdentityTypeEnglishName = model.IdentityTypeEnglishName;
                sYS_IdentityTypeModel.Sort = model.Sort;

                int i = SYS_IdentityTypeAdapter.Instance.AddOrUpdate(sYS_IdentityTypeModel);
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

        /// <summary>
        /// 删除 身份类型
        /// 【by ZHL】
        /// </summary>
        /// <param name="ItID">身份类型ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelRelation_UseGroup(Guid ItID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                SYS_IdentityType sYS_IdentityType = new SYS_IdentityType();
                sYS_IdentityType.ItID = ItID;
                int i = SYS_IdentityTypeAdapter.Instance.Remove(sYS_IdentityType);
                if (i > 0)
                {
                    isSucceed.IsSucceed = true;
                    isSucceed.ErrorMessage = "删除成功";
                }
                else
                {
                    isSucceed.IsSucceed = false;
                    isSucceed.ErrorMessage = "删除失败";
                }
            }
            catch (Exception ex)
            {
                isSucceed.IsSucceed = false;
                isSucceed.ErrorMessage = "删除失败:" + ex.ToString();
            }
            return Json(isSucceed);
        }



        #endregion

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult SetCache()
        {
            List<SYS_IdentityType> IdentityTypeList = new List<SYS_IdentityType>();
            IdentityTypeList = SYS_IdentityTypeAdapter.Instance.GetAll().ToList();
            CacheClass.SetCache("IdentityTypeList", IdentityTypeList);
            return Json("");
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCache()
        {
            object IdentityTypeList = CacheClass.GetCache("IdentityTypeList");
            //object val= CacheClass.GetCache("ceshi");
            return Json(IdentityTypeList);
        }









    }
}
