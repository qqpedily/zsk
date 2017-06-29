using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using YinGu.Operation.Framework.Domain.Users;
using Com.Weehong.Elearning.Domain.WebModel;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Text;
using Com.Weehong.Elearning.MasterData.Repositories;
using WebApiZSK.Models.Input;
using Com.Weehong.Elearning.MasterData.Common;

namespace WebApiZSK.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AttentionController : ApiController
    {
      
        private readonly RelationUserAttentionPeriodicalAdapter relationUserAttentionPeriodicalAdapter = new  RelationUserAttentionPeriodicalAdapter();
      
        private readonly RelationUserAttentionScholarAdapter relationUserAttentionScholarAdapter = new  RelationUserAttentionScholarAdapter();
        private readonly RelationUserAttentionThemeAdapter relationUserAttentionThemeAdapter = new  RelationUserAttentionThemeAdapter();
        

        #region  用户关注期刊的作品集合

        /// <summary>
        /// 根据用户ID查询用户关注期刊集合的作品集合
        /// </summary>
        ///<param name="sysuserID">用户ID</param>
        ///<param name="pageSize">每页条数</param>
        ///<param name="curPage">页数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task< IHttpActionResult> GetRelationUserAttentionPeriodicalBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {

            return Json(await relationUserAttentionPeriodicalAdapter.LoadBySysUserID(sysuserID, pageSize, curPage));
        }

        /// <summary>
        /// 根据用户ID查询关注期刊集合数目
        /// </summary>
        ///<param name="sysuserID">用户ID</param>
        /// <returns></returns>
        //[HttpGet]
        //public async Task<IHttpActionResult> GetRelationUserAttentionPeriodicalBySysUserIDCount(Guid sysuserID)
        //{

        //    return Json();
        //}
        /// <summary>
        /// /根据用户id 和文献id获取 关注信息
        /// </summary>
        ///<param name="sysuserID">用户ID</param>
        ///<param name="productionid">文献id</param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IHttpActionResult> LoadBySysUserIDAndProdctionID(Guid sysuserID,Guid productionid)
        {

            return Json(await relationUserAttentionPeriodicalAdapter.LoadBySysUserIDAndProdctionID(sysuserID,productionid));
        }

        /// <summary>
        /// 添加/编辑  用户关注期刊
        /// </summary>
        /// <param name="model">RelationUserAttentionPeriodical表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateAttentionUserCollectPeriodical([FromBody]RelationUserCollectPeriodicalModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                RelationUserAttentionPeriodicalModel Model = relationUserAttentionPeriodicalAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (Model == null)//添加
                {
                    Model = new  RelationUserAttentionPeriodicalModel();
                    Model.UUID = Guid.NewGuid();
                }
                Model.CreateTime = model.CreateTime;
                Model.PeriodicalID = model.PeriodicalID;
                Model.PeriodicalName = model.PeriodicalName;
                Model.SysUserID = model.SysUserID;
                Model.ValidStatus = model.ValidStatus;
                int i = relationUserAttentionPeriodicalAdapter.AddOrUpdate(Model);
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
        /// 删除关注期刊
        /// </summary>
        /// <param name="model">RelationUserCollectPeriodical表</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserAttentionPeriodical([FromBody]RelationUserAttentionPeriodicalModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {

                int i = relationUserAttentionPeriodicalAdapter.Remove(model);
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




        #endregion
        

        #region  用户关注学者的作品集合

        /// <summary>
        /// 根据用户ID查询关注学者集合的作品集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserAttentionScholarBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            return Json(await relationUserAttentionScholarAdapter.LoadBySysUserID(sysuserID, pageSize, curPage));
        }


        /// <summary>
        /// /根据用户id 和学者id获取 关注信息
        /// </summary>
        ///<param name="sysuserID">用户ID</param>
        ///<param name="scholarID">学者id</param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IHttpActionResult> LoadBySysUserIDAndScholarID(Guid sysuserID, Guid scholarID)
        {
            var info = await relationUserAttentionScholarAdapter.LoadBySysUserIDAndScholarID(sysuserID, scholarID);
            return Json(info);
        }
        /// <summary>
        /// 根据用户ID查询关注学者集合数目
        /// </summary>
        /// <param name="sysuserID"></param>

        /// <returns></returns>

        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserAttentionScholarBySysUserIDCount(Guid sysuserID)
        {
            return Json(await relationUserAttentionScholarAdapter.LoadBySysUserIDCount(sysuserID));
        }


        /// <summary>
        /// 添加/编辑  用户关注学者
        /// </summary>
        /// <param name="model">RelationUserAttentionScholar表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRelationUserAttentionScholar([FromBody]RelationUserAttentionScholarModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                RelationUserAttentionScholarModel Model = relationUserAttentionScholarAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (Model == null)//添加
                {
                    Model = new RelationUserAttentionScholarModel();
                    Model.UUID = Guid.NewGuid();
                }
                Model.CreateTime = model.CreateTime;
                Model.ScholarID = model.ScholarID;
                Model.ScholarName = model.ScholarName;
                Model.SysUserID = model.SysUserID;
                Model.ValidStatus = model.ValidStatus;


                int i = relationUserAttentionScholarAdapter.AddOrUpdate(Model);
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
        /// 删除 用户关注学者
        /// </summary>
        /// <param name="model">RelationUserAttentionScholar表</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserAttentionScholar([FromBody]RelationUserAttentionScholarModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {

                int i = relationUserAttentionScholarAdapter.Remove(model);
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

        #endregion


        #region  用户关注主题的作品集合

        /// <summary>
        /// 根据用户ID查询关注主题集合的作品集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserAttentionThemeBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            return Json(await relationUserAttentionThemeAdapter.LoadBySysUserID(sysuserID, pageSize, curPage));
        }

        /// <summary>
        /// /根据用户id 和主题id获取 关注信息的作品集合
        /// </summary>
        ///<param name="sysuserID">用户ID</param>
        ///<param name="themid">主题id</param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IHttpActionResult> LoadBySysUserIDAndThemID(Guid sysuserID, Guid themid)
        {

            return Json(await relationUserAttentionThemeAdapter.LoadBySysUserIDAndThemID(sysuserID, themid));
        }
        /// <summary>
        /// 根据用户ID查询关注主题集合数目
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserAttentionThemeBySysUserIDCount(Guid sysuserID)
        {
            return Json(await relationUserAttentionThemeAdapter.LoadBySysUserIDCount(sysuserID));
        }


        /// <summary>
        /// 添加/编辑  用户关注主题
        /// </summary>
        /// <param name="model">RelationUserAttentionTheme表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRelationUserAttentionTheme([FromBody]RelationUserAttentionThemeModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                RelationUserAttentionThemeModel Model = relationUserAttentionThemeAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (Model == null)//添加
                {
                    Model = new RelationUserAttentionThemeModel();
                    Model.UUID = Guid.NewGuid();
                }
                Model.CreateTime = model.CreateTime;
                Model.ThemeID = model.ThemeID;
                Model.ThemeName = model.ThemeName;
                Model.SysUserID = model.SysUserID;
                Model.ValidStatus = model.ValidStatus;


                int i = relationUserAttentionThemeAdapter.AddOrUpdate(Model);
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
        /// 删除 用户关注主题
        /// </summary>
        /// <param name="model">RelationUserAttentionTheme表</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserAttentionTheme([FromBody]RelationUserAttentionThemeModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {

                int i = relationUserAttentionThemeAdapter.Remove(model);
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
        #endregion
    }
}
