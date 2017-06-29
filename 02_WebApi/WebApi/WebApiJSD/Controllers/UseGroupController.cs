
using Com.Weehong.Elearning.Domain.WebModel;
using Com.Weehong.Elearning.MasterData.DataAdapter.UseGroup;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.UseGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using YinGu.Operation.Framework.Domain.UseGroup;

namespace WebApiZSK.Controllers
{
    /// <summary>
    ///用户组
    /// </summary>
    [AllowAnonymous]
    public class UseGroupController : ApiController
    {
        Relation_UseGroupAdapter useGroupAdapter = new Relation_UseGroupAdapter();
        Relation_UseGroup_UserAdapter relation_UseGroup_UserAdapter = new Relation_UseGroup_UserAdapter();
        Relation_UseGroup_UseGroupAdapter relationUseGroupUseGroupAdapter = new Relation_UseGroup_UseGroupAdapter();
        private readonly UserAdapter user = new UserAdapter();
        TransRelation_UseGroup transRelation_UseGroup = new TransRelation_UseGroup();

        #region 用户组管理 

        /// <summary>
        /// 获取用户信息列表   【by ZHL】
        /// </summary>
        /// <param name="UserName">用户姓名</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetUserList(string UserName)
        {
            return Json(await user.LoadByUserData(UserName));
        }
        //[HttpGet]
        //public IHttpActionResult GetUserList(string NameChinese)
        //{

        //    return Json(user.GetAll().Where(w => w.NameChinese.Contains(NameChinese)| w.NameChinese ==NameChinese).ToList());
        //}

        /// <summary>
        /// 获取用户组信息列表   【by ZHL】
        /// </summary>
        /// <param name="SponsorID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUseGroupList(Guid SponsorID)
        {
            return Json(useGroupAdapter.GetAll().Where(w => w.SponsorID == SponsorID).ToList());
        }
        /// <summary>
        /// 获取用户组详细信息  【by ZHL】
        /// </summary>
        /// <param name="UseGroupID">用户组UseGroupID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUseGroupInfo(Guid UseGroupID)
        {
            return Json(useGroupAdapter.GetAll().Where(w => w.UseGroupID == UseGroupID).FirstOrDefault());
        }
        /// <summary>
        /// 新增/编辑 用户组 不带邀请人 【by ZHL】
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRrelation_UseGroup([FromBody]Relation_UseGroup model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                Relation_UseGroup rrelation_UseGroupModel = useGroupAdapter.GetAll().Where(w => w.UseGroupID == model.UseGroupID).FirstOrDefault();
                if (rrelation_UseGroupModel == null)//添加
                {
                    rrelation_UseGroupModel = new Relation_UseGroup();
                    rrelation_UseGroupModel.UseGroupID = Guid.NewGuid();
                    rrelation_UseGroupModel.CreateTime = DateTime.Now;
                }
                rrelation_UseGroupModel.UseGroupName = model.UseGroupName;
                rrelation_UseGroupModel.SponsorID = model.SponsorID;
                rrelation_UseGroupModel.Describe = model.Describe;
                rrelation_UseGroupModel.GroupType = model.GroupType;
                rrelation_UseGroupModel.Sort = model.Sort;

                int i = useGroupAdapter.AddOrUpdate(rrelation_UseGroupModel);
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
        /// 新增用户组  加邀请人 【by ZHL】
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddRrelation_UseGroup([FromBody]Relation_UseGroup model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            if (!model.SponsorID.HasValue)
            {
                isSucceed.IsSucceed = false;
                isSucceed.ErrorMessage = "邀请人信息有误!";
                return Json(isSucceed);
            }
            else if (string.IsNullOrEmpty(model.UseGroupName))
            {
                isSucceed.IsSucceed = false;
                isSucceed.ErrorMessage = "组名称不能为空!";
                return Json(isSucceed);
            }
            return Json(await transRelation_UseGroup.AddOrUpdateRelation_UseGroup(model));
        }



        /// <summary>
        /// 删除 用户组  【by ZHL】
        /// </summary>
        /// <param name="UseGroupID">用户组ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelRelation_UseGroup(Guid UseGroupID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                Relation_UseGroup rrelation_UseGroupModel = new Relation_UseGroup();
                rrelation_UseGroupModel.UseGroupID = UseGroupID;
                if (transRelation_UseGroup.DelUseGroup_User(UseGroupID))//删除用户组用户信息
                {
                    int i = useGroupAdapter.Remove(rrelation_UseGroupModel);
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
                else {
                    isSucceed.IsSucceed = false;
                    isSucceed.ErrorMessage = "删除用户组用户失败";
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

        #region 用户组用户关系

        /// <summary>
        /// 用户组关系列表  根据用户组ID 获取用户组用户信息列表 【加入组】 可查询【加入组、已加入组】信息列表 
        ///【by ZHL】
        /// </summary>
        /// <param name="SysUserID">受邀人ID</param>
        /// <param name="Join">是否已加入组标识 【未加入为0;已加入为1;拒绝加入为2】</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUseGroup_UserList(Guid SysUserID,int? Join)
        {
            return Json(relation_UseGroup_UserAdapter.GetAll().Where(w => w.SysUserID == SysUserID && w.Join == Join).ToList());
        }

        /// <summary>
        /// 用户组关系信息  根据用户组ID 获取用户组用户详细 
        ///【by ZHL】
        /// </summary>
        /// <param name="UUID">用户组用户关系ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUseGroup_UserInfo(Guid UUID)
        {
            return Json(relation_UseGroup_UserAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }

        /// <summary>
        /// 加入组  【加入组/拒绝组】
        ///【by ZHL】
        /// </summary>
        /// <param name="useGroup_User">Relation_UseGroup_User 实体必传UUID 和 Jion(是否加入用户组，未加入为0；已加入为1；拒绝加入为2（代表用户推出该用户组并且删除该条数据） </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateRelationUseGroupUser([FromBody]Relation_UseGroup_User useGroup_User)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();

            try
            {
                //用户组邀请信息            
                Relation_UseGroup_User useGroup_UserModel = relation_UseGroup_UserAdapter.GetAll().Where(w => w.UUID == useGroup_User.UUID).FirstOrDefault();
                if (useGroup_UserModel == null)//
                {
                    isSucceed.IsSucceed = false;
                    isSucceed.ErrorMessage = "传过来的参数有误,请检查!";
                    return Json(isSucceed);
                }

                useGroup_UserModel.UUID = useGroup_User.UUID;
                useGroup_UserModel.Join = useGroup_User.Join;

                int i = relation_UseGroup_UserAdapter.AddOrUpdate(useGroup_UserModel);
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
        /// 新增/编辑  用户组用户关系信息
        ///【by ZHL】
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRrelation_UseGroup_User([FromBody]Relation_UseGroup_User model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                Relation_UseGroup_User rrelation_UseGroup_UserModel = relation_UseGroup_UserAdapter.GetAll().Where(w => w.UseGroupID == model.UseGroupID).FirstOrDefault();
                if (rrelation_UseGroup_UserModel == null)//添加
                {
                    rrelation_UseGroup_UserModel = new Relation_UseGroup_User();
                    rrelation_UseGroup_UserModel.UUID = Guid.NewGuid();
                    rrelation_UseGroup_UserModel.InvitationTime = DateTime.Now;
                }
                rrelation_UseGroup_UserModel.UseGroupID = model.UseGroupID;
                rrelation_UseGroup_UserModel.SysUserID = model.SysUserID;
                rrelation_UseGroup_UserModel.Join = model.Join;
                rrelation_UseGroup_UserModel.sort = model.sort;

                int i = relation_UseGroup_UserAdapter.AddOrUpdate(rrelation_UseGroup_UserModel);
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
