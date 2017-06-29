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
using Com.Weehong.Elearning.MasterData.DataAdapter.SysManage;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;
using YinGu.Operation.Framework.Domain.Comm;
using System.Data.Entity;

namespace WebApiZSK.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [AllowAnonymous]

    public class UserController : ApiController
    {

        private readonly UserAdapter user = new UserAdapter();
        private readonly TransUser transUser = new TransUser();
        private readonly UserHonorAdapter userHonorAdpter = new UserHonorAdapter();//用户荣誉头衔
        private readonly UserAliasAdapter userAliasAdpter = new UserAliasAdapter();//用户别名
        private readonly UserContactInformationAdapter userContactInformationAdpter = new UserContactInformationAdapter();//用户_联系信息
        private readonly UserEnglishLevelAdpter userEnglishLevelAdpter = new UserEnglishLevelAdpter();//用户外语能力
        private readonly UserHonorAwardAdapter userHonorAwardAdapter = new UserHonorAwardAdapter();//
        private readonly UserInsidePrincipalOfficeAdapter userInsidePrincipalOfficeAdapter = new UserInsidePrincipalOfficeAdapter();
        private readonly UserLearningExperienceAdapter userLearningExperienceAdapter = new UserLearningExperienceAdapter();
        private readonly UserOtherPrincipalOfficeAdapter userOtherPrincipalOfficeAdapter = new UserOtherPrincipalOfficeAdapter();
        private readonly UserPersonalHomepageAdapter userPersonalHomepageAdapter = new UserPersonalHomepageAdapter();
        private readonly UserPracticeQualificationAdapter userPracticeQualificationAdapter = new UserPracticeQualificationAdapter();
        private readonly UserResearchProjectAdapter userResearchProjectAdapter = new UserResearchProjectAdapter();
        private readonly UserSearchDirectionAdapter userSearchDirectionAdapter = new UserSearchDirectionAdapter();
        private readonly UserWorkExperienceAdapter userWorkExperienceAdapter = new UserWorkExperienceAdapter();

        private readonly RelationUserCollectPeriodicalAdapter relationUserCollectPeriodicalAdapter = new RelationUserCollectPeriodicalAdapter();
        private readonly RelationUserCollectProductAdapter relationUserCollectProductAdapter = new RelationUserCollectProductAdapter();
        private readonly RelationUserCollectScholarAdapter relationUserCollectScholarAdapter = new RelationUserCollectScholarAdapter();
        private readonly RelationUserCollectThemeAdapter relationUserCollectThemeAdapter = new RelationUserCollectThemeAdapter();
        private readonly RelationUserClaimWorksAdapter relationUserClaimWorksAdapter = new RelationUserClaimWorksAdapter();

        private readonly TransUserAlias transUserAlias = new TransUserAlias();//用户别名

        private readonly UserCommentAdapter usercommentadapter = new UserCommentAdapter();
        private readonly UserPrivateLetterAdapter userprivateletteradapter = new UserPrivateLetterAdapter();

        private readonly UserCitationProductionsAdapter usercitationproductionsadapter = new UserCitationProductionsAdapter();


        #region 用户 User

        /// <summary>
        /// 获取用户信息
        /// 【by ZHL】
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserList()
        {
            return Json(user.GetAll().ToList());
        }
        /// <summary>
        ///获取用户信息
        ///【by ZHL】
        /// </summary>
        /// <param name="UUID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUser(Guid UUID)
        {
            return Json(user.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }

        /// <summary>
        /// 获取用户信息列表  分页
        /// 【by ZHL】
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="curPage">当前页</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserPageList(int pageSize, int curPage)
        {
            UserTotal model = new UserTotal();
            if (curPage == 0)
            {
                //不分页
                model.UserList = user.GetAll().ToList();
            }
            else
            {
                model = transUser.GetUserTotal(pageSize, curPage);
            }
            return Json(model);
        }

        /// <summary>
        /// 根据用户ID 获取用户信息
        /// 【by ZHL】
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserInfoByID(Guid ID)
        {

            return Json(user.GetAll().Where(w => w.UUID == ID).FirstOrDefault());
        }

        /// <summary>
        ///添加编辑用户信息
        ///【by ZHL】
        /// </summary>
        /// <param name="users">User 实体</param>
        /// <returns>WebModelIsSucceed</returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUser([FromBody]UserModel users)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //用户            
                UserModel userModel = user.GetAll().Where(w => w.UUID == users.UUID).FirstOrDefault();
                if (userModel == null)//添加
                {
                    users.UUID = Guid.NewGuid();
                    users.PassWord = MD5Method.Instance.MD5Encrypt(users.PassWord);
                }
                else
                {
                    users.PassWord = userModel.PassWord;
                }
                int i = user.AddOrUpdate(users);
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
        ///更改用户密码
        ///【by ZHL】
        /// </summary>
        /// <param name="users">User 实体 原密码Pwd  现PassWord</param>
        /// <returns>WebModelIsSucceed</returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserPwd([FromBody]UserModel users)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                string Pwd = MD5Method.Instance.MD5Encrypt(users.Pwd);
                string PassWord = MD5Method.Instance.MD5Encrypt(users.PassWord);
                //用户            
                UserModel userModel = user.GetAll().Where(w => w.UUID == users.UUID && w.PassWord == Pwd).FirstOrDefault();
                if (userModel == null)//添加
                {
                    isSucceed.IsSucceed = false;
                    isSucceed.ErrorMessage = "原始密码有误,请重试!";
                    return Json(isSucceed);
                }
                if (PassWord == Pwd)
                {
                    isSucceed.IsSucceed = false;
                    isSucceed.ErrorMessage = "原始密码和更改密码相同,请重试!";
                    return Json(isSucceed);
                }
                userModel.UUID = users.UUID;
                userModel.PassWord = PassWord;

                int i = user.AddOrUpdate(userModel);
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

        #region 用户别名 UserAlias
        /// <summary>
        /// 获取用户别名列表  
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserAliasList(Guid UserID)
        {
            return Json(userAliasAdpter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取用户别名详细信息  
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户别名ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserAlias(Guid UUID)
        {
            return Json(userAliasAdpter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        ///添加 编辑用户信息
        ///【by ZHL】
        /// </summary>
        /// <param name="model">UserAlias 实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserAlias([FromBody]UserAliasModel model)
        {
            bool boolFlag = true;
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //用户            
                UserAliasModel userAliasModel = userAliasAdpter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userAliasModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }

                if (!model.UserID.HasValue)
                {
                    isSucceed.IsSucceed = false;
                    isSucceed.ErrorMessage = "获取用户信息有误:用户编号不能能空！";
                    return Json(isSucceed);
                }
                if (model.FirstChineseShow == 0)//是
                {
                    boolFlag = transUserAlias.UpdateUserAlias(model.UserID, "FirstChineseShow");
                }
                else if (model.FirstEnglishShow == 0)
                {
                    boolFlag = transUserAlias.UpdateUserAlias(model.UserID, "FirstEnglishShow");
                }
                if (boolFlag)
                {
                    int i = userAliasAdpter.AddOrUpdate(model);
                    if (i > 0)
                    {
                        isSucceed.IsSucceed = true;
                    }
                    else
                    {
                        isSucceed.IsSucceed = false;
                        isSucceed.ErrorMessage = "无操作记录!";
                    }
                }
            }
            catch (Exception ex)
            {
                isSucceed.IsSucceed = false;
                isSucceed.ErrorMessage = ex.Message + "\r\n" + ex.StackTrace;
                return Json(isSucceed);
            }
            return Json(isSucceed);
        }
        /// <summary>
        /// 删除用户别名
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户别名ID UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserAlias(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserAliasModel userAliasModel = new UserAliasModel();
                userAliasModel.UUID = UUID;
                int i = userAliasAdpter.Remove(userAliasModel);
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

        /// <summary>
        /// 添加用户别名
        /// 获取所有用户，添加所有用户别名
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SetAllUserAlias()
        {
            userAliasAdpter.SetAllUserAlias();
            return Json("1");
        }

        /// <summary>
        /// 添加用户别名
        /// 根据用户ID获取用户，添加该用户别名
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>

        [HttpPost]
        public IHttpActionResult SetUserAliasByUserID(string userid)
        {
            userAliasAdpter.SetUserAliasByUserID(userid);
            return Json("1");

        }

        #endregion

        #region 用户_联系信息 

        /// <summary>
        /// 获取 用户_联系信息 列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetUserContactInformationList(Guid UserID)
        {
            return Json(await userContactInformationAdpter.GetUserContactInformationAsync(UserID));
            //return Json(userContactInformationAdpter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取 用户_联系信息ID  详细信息
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserContactInformation(Guid UUID)
        {
            return Json(userContactInformationAdpter.GetUserContactInformationInfoAsync(UUID));
            //return Json(userContactInformationAdpter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        ///添加、编辑 用户_联系信息 
        ///【by ZHL】
        /// </summary>
        /// <param name="model">UserContactInformation 实体</param>
        /// <param>UUID传值为编辑 传值空位新增</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserContactInformation([FromBody]UserContactInformationModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserContactInformationModel userContactInformationModel = userContactInformationAdpter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userContactInformationModel == null)//添加
                {
                    //userContactInformationModel = new UserContactInformationModel();
                    model.UUID = Guid.NewGuid();
                }
                int i = userContactInformationAdpter.AddOrUpdate(model);
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
        /// 删除用户_联系信息 
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户_联系信息ID:UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserContactInformationMo(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserContactInformationModel userContactInformationModel = new UserContactInformationModel();
                userContactInformationModel.UUID = UUID;
                int i = userContactInformationAdpter.Remove(userContactInformationModel);
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

        #region 用户外语能力 
        /// <summary>
        ///获取外语能力列表
        ///【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserEnglishLevelList(Guid UserID)
        {
            return Json(userEnglishLevelAdpter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        ///获取外语能力 详细信息
        ///【by ZHL】
        /// </summary>
        /// <param name="UUID">外语能力ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserEnglishLevel(Guid UUID)
        {
            return Json(userEnglishLevelAdpter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        ///新增、编辑 用户外语能力信息
        ///【by ZHL】
        /// </summary>
        /// <param name="model">UserEnglishLevel 表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserEnglishLevel([FromBody]UserEnglishLevelModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                UserEnglishLevelModel userEnglishLevelModel = userEnglishLevelAdpter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userEnglishLevelModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }

                int i = userEnglishLevelAdpter.AddOrUpdate(model);
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
        /// 删除 用户外语能力信息 
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户外语能力信息ID:UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserEnglishLevel(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserEnglishLevelModel userEnglishLevelModel = new UserEnglishLevelModel();
                userEnglishLevelModel.UUID = UUID;
                int i = userEnglishLevelAdpter.Remove(userEnglishLevelModel);
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

        #region 荣誉头衔

        /// <summary>
        ///获取荣誉头衔列表
        ///【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserHonorList(Guid UserID)
        {
            return Json(userHonorAdpter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        ///获取荣誉头衔 [详细信息]
        ///【by ZHL】
        /// </summary>
        /// <param name="UUID">荣誉头衔ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserHonor(Guid UUID)
        {
            return Json(userHonorAdpter.GetAll().Where(w => w.UserID == UUID).FirstOrDefault());
        }
        /// <summary>
        ///新增、编辑 荣誉头衔
        ///【by ZHL】
        /// </summary>
        /// <param name="model">UserHonor 表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserHonor([FromBody]UserHonorModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //用户            
                UserHonorModel userHonorModel = userHonorAdpter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userHonorModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }

                int i = userHonorAdpter.AddOrUpdate(model);
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
        /// 删除 用户荣誉信息
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户荣誉ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserHonor(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserHonorModel userHonorModel = new UserHonorModel();
                userHonorModel.UUID = UUID;
                int i = userHonorAdpter.Remove(userHonorModel);
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

        #region 用户学术履历中的荣誉奖励 
        /// <summary>
        /// 获取【用户学术履历中的荣誉奖励】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserHonorAwardList(Guid UserID)
        {
            return Json(userHonorAwardAdapter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取【用户学术履历中的荣誉奖励】[详细信息]
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户学术履历中的荣誉奖励ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserHonorAward(Guid UUID)
        {
            return Json(userHonorAwardAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        /// 新增 编辑 用户学术履历中的荣誉奖励
        /// 【by ZHL】
        /// </summary>
        /// <param name="model">UserHonorAward 表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserHonorAward([FromBody]UserHonorAwardModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserHonorAwardModel userHonorAwardModel = userHonorAwardAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userHonorAwardModel == null)//添加
                {

                    model.UUID = Guid.NewGuid();
                }


                int i = userHonorAwardAdapter.AddOrUpdate(model);
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
        /// 删除 用户学术履历中的荣誉奖励
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户学术履历中的荣誉奖励ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserHonorAward(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserHonorAwardModel userHonorAwardModel = new UserHonorAwardModel();
                userHonorAwardModel.UUID = UUID;
                int i = userHonorAwardAdapter.Remove(userHonorAwardModel);
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

        #region  用户内部任职信息
        /// <summary>
        /// 获取【用户内部任职信息】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserInsidePrincipalOfficeList(Guid UserID)
        {
            return Json(userInsidePrincipalOfficeAdapter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取【用户内部任职信息】[详细信息]
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户内部任职信息ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserInsidePrincipalOffice(Guid UUID)
        {
            return Json(userInsidePrincipalOfficeAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        ///新增 编辑 用户内部任职信息
        ///【by ZHL】
        /// </summary>
        /// <param name="model">UserInsidePrincipalOffice 表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserInsidePrincipalOffice([FromBody]UserInsidePrincipalOfficeModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                UserInsidePrincipalOfficeModel userInsidePrincipalOfficeModel = userInsidePrincipalOfficeAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userInsidePrincipalOfficeModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }
                int i = userInsidePrincipalOfficeAdapter.AddOrUpdate(model);
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
        /// 删除 用户内部任职信息
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户内部任职信息ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserInsidePrincipalOffice(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserInsidePrincipalOfficeModel userInsidePrincipalOfficeModel = new UserInsidePrincipalOfficeModel();
                userInsidePrincipalOfficeModel.UUID = UUID;
                int i = userInsidePrincipalOfficeAdapter.Remove(userInsidePrincipalOfficeModel);
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

        #region  用户学习经历
        /// <summary>
        /// 获取【用户学习经历】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserLearningExperienceList(Guid UserID)
        {
            return Json(userLearningExperienceAdapter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取【用户学习经历】[详细信息]
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserLearningExperience(Guid UUID)
        {
            return Json(userLearningExperienceAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }

        /// <summary>
        ///新增 编辑 用户学习经历
        ///【by ZHL】
        /// </summary>
        /// <param name="model">UserLearningExperience表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserLearningExperience([FromBody]UserLearningExperienceModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                UserLearningExperienceModel userLearningExperienceModel = userLearningExperienceAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userLearningExperienceModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }
                int i = userLearningExperienceAdapter.AddOrUpdate(model);
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
        /// 删除 用户学习经历
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户学习经历ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserLearningExperience(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserLearningExperienceModel userLearningExperienceModel = new UserLearningExperienceModel();
                userLearningExperienceModel.UUID = UUID;
                int i = userLearningExperienceAdapter.Remove(userLearningExperienceModel);
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

        #region  用户其他任职信息

        /// <summary>
        /// 获取【用户其他任职信息】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserOtherPrincipalOfficeList(Guid UserID)
        {
            return Json(userOtherPrincipalOfficeAdapter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取【用户其他任职信息】[详细信息]
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户其他任职信息ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserOtherPrincipalOffice(Guid UUID)
        {
            return Json(userOtherPrincipalOfficeAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        /// 新增、编辑  用户其他任职信息
        /// 【by ZHL】
        /// </summary>
        /// <param name="model">UserOtherPrincipalOffice</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserOtherPrincipalOffice([FromBody]UserOtherPrincipalOfficeModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                UserOtherPrincipalOfficeModel userOtherPrincipalOfficeModel = userOtherPrincipalOfficeAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userOtherPrincipalOfficeModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }

                int i = userOtherPrincipalOfficeAdapter.AddOrUpdate(model);
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
        /// 删除 用户其他任职信息
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户其他任职信息ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserOtherPrincipalOffice(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserOtherPrincipalOfficeModel userOtherPrincipalOfficeModel = new UserOtherPrincipalOfficeModel();
                userOtherPrincipalOfficeModel.UUID = UUID;
                int i = userOtherPrincipalOfficeAdapter.Remove(userOtherPrincipalOfficeModel);
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

        #region  用户_联系信息
        /// <summary>
        /// 获取【用户_联系信息】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserPersonalHomepageList(Guid UserID)
        {
            return Json(userPersonalHomepageAdapter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取【用户_联系信息】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户_联系信息ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserPersonalHomepage(Guid UUID)
        {
            return Json(userPersonalHomepageAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        ///新增/编辑  用户_联系信息
        ///【by ZHL】
        /// </summary>
        /// <param name="model">UserOtherPrincipalOffice 表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserPersonalHomepage([FromBody]UserPersonalHomepageModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserPersonalHomepageModel userPersonalHomepageModel = userPersonalHomepageAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();

                if (userPersonalHomepageModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }



                int i = userPersonalHomepageAdapter.AddOrUpdate(model);
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
        /// 删除 用户_联系信息
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户_联系信息ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserPersonalHomepage(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserPersonalHomepageModel userPersonalHomepageModel = new UserPersonalHomepageModel();
                userPersonalHomepageModel.UUID = UUID;
                int i = userPersonalHomepageAdapter.Remove(userPersonalHomepageModel);
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

        /// <summary>
        /// 上传或者修改用户头像
        /// </summary>
        /// <param name="useruuid">用户ID: 用户的UUID</param>
        /// <param name="localpath">图片路径 可不传</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SaveUserFile(Guid useruuid, string localpath)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            OperationFiles operationFiles = new OperationFiles();
            isSucceed = await operationFiles.SaveUserFile(useruuid, "C:\\Files\\user_img\\");//上传文档信息

            return Json(isSucceed);

        }






        #endregion

        #region  用户职业资格

        /// <summary>
        /// 获取【用户职业资格】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserPracticeQualificationList(Guid UserID)
        {
            return Json(userPracticeQualificationAdapter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取【用户职业资格】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户职业资格ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserPracticeQualification(Guid UUID)
        {
            return Json(userPracticeQualificationAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        /// 新增/编辑  用户职业资格
        /// 【by ZHL】
        /// </summary>
        /// <param name="model">UserPracticeQualification 表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserPracticeQualification([FromBody]UserPracticeQualificationModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                UserPracticeQualificationModel userPracticeQualificationModel = userPracticeQualificationAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userPracticeQualificationModel == null)//添加
                {

                    model.UUID = Guid.NewGuid();
                }

                int i = userPracticeQualificationAdapter.AddOrUpdate(model);
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
        /// 删除 用户职业资格
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户职业资格ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserPracticeQualification(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserPracticeQualificationModel userPracticeQualificationModel = new UserPracticeQualificationModel();
                userPracticeQualificationModel.UUID = UUID;
                int i = userPracticeQualificationAdapter.Remove(userPracticeQualificationModel);
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

        #region  用户学术履历中的_研究项目

        /// <summary>
        /// 获取【用户学术履历中的_研究项目】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserResearchProjectList(Guid UserID)
        {
            return Json(userResearchProjectAdapter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取【用户学术履历中的_研究项目】[详细信息]
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户学术履历中的_研究项目ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserResearchProject(Guid UUID)
        {
            return Json(userResearchProjectAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        ///新增/编辑 用户学术履历中的_研究项目
        ///【by ZHL】
        /// </summary>
        /// <param name="model">UserResearchProject 表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserResearchProject([FromBody]UserResearchProjectModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                UserResearchProjectModel userResearchProjectModel = userResearchProjectAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userResearchProjectModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }

                int i = userResearchProjectAdapter.AddOrUpdate(model);
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
        /// 删除 用户学术履历中的_研究项目
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户学术履历中的_研究项目ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserResearchProject(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserResearchProjectModel userResearchProjectModel = new UserResearchProjectModel();
                userResearchProjectModel.UUID = UUID;
                int i = userResearchProjectAdapter.Remove(userResearchProjectModel);
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

        #region   用户学术履历中的用户研究方向

        /// <summary>
        /// 获取【用户学术履历中的用户研究方向】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetUserSearchDirectionList(Guid UserID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return Json(await db.UserSearchDirection.Where(w => w.UserID == UserID).ToListAsync());
            }
        }
        /// <summary>
        /// 获取【用户学术履历中的用户研究方向】[详细信息]
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserSearchDirection(Guid UUID)
        {
            return Json(userSearchDirectionAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        ///新增/编辑 用户学术履历中的用户研究方向
        ///【by ZHL】
        /// </summary>
        /// <param name="model">UserSearchDirection表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserSearchDirection([FromBody]UserSearchDirectionModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserSearchDirectionModel userSearchDirectionModel = userSearchDirectionAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (userSearchDirectionModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }

                int i = userSearchDirectionAdapter.AddOrUpdate(model);
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
        /// 删除 用户学术履历中的用户研究方向
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户学术履历中的用户研究方向ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserSearchDirection(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserSearchDirectionModel userSearchDirectionModel = new UserSearchDirectionModel();
                userSearchDirectionModel.UUID = UUID;
                int i = userSearchDirectionAdapter.Remove(userSearchDirectionModel);
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

        #region   用户工作经验

        /// <summary>
        /// 获取【用户学术履历中的用户研究方向】列表
        /// 【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserWorkExperienceList(Guid UserID)
        {
            using (var db = new OperationManagerDbContext())
            {

            }

            return Json(userWorkExperienceAdapter.GetAll().Where(w => w.UserID == UserID).ToList());
        }
        /// <summary>
        /// 获取【用户学术履历中的用户研究方向】[详细信息]
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户学术履历中的用户研究方向ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserWorkExperience(Guid UUID)
        {
            return Json(userWorkExperienceAdapter.GetAll().Where(w => w.UUID == UUID).FirstOrDefault());
        }
        /// <summary>
        /// 添加/编辑  用户工作经验
        /// 【by ZHL】
        /// </summary>
        /// <param name="model">UserWorkExperience表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserWorkExperience([FromBody]UserWorkExperienceModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                UserWorkExperienceModel UserWorkExperienceModel = userWorkExperienceAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (UserWorkExperienceModel == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }

                int i = userWorkExperienceAdapter.AddOrUpdate(model);
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
        /// 删除 用户学术履历中的用户研究方向 
        /// 【by ZHL】
        /// </summary>
        /// <param name="UUID">用户学术履历中的用户研究方向ID: UUID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult DelUserWorkExperience(Guid UUID)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserWorkExperienceModel userWorkExperienceModel = new UserWorkExperienceModel();
                userWorkExperienceModel.UUID = UUID;
                int i = userWorkExperienceAdapter.Remove(userWorkExperienceModel);
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
        #region    获取身份信息列表



        /// <summary>
        /// 获取身份信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadByIdentityTypeData()
        {
            SYS_IdentityTypeAdapter adapter = new SYS_IdentityTypeAdapter();
            List<SYS_IdentityType> list = await adapter.LoadByIdentityTypeData();

            return Json(list);
        }
        #endregion


        #region 按照职称和姓名查询用户

        /// <summary>
        /// 根据用户姓名,职称获取详情
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="positional">职称</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns>用户列表</returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadUserByNameAndPositional(string name, string positional, int pageSize, int curPage)
        {
            return Json(await user.LoadUserByNameAndPositional(name, positional, pageSize, curPage));
        }

        #endregion


        #region 按照姓的首字母查询用户

        /// <summary>
        /// 按照姓的首字母查询用户
        /// </summary>
        /// <param name="phoneticize">姓的首字母A，B，C，D</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>

        /// <returns>用户列表</returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadUserBySurnamePhoneticize(string phoneticize, int pageSize, int curPage)
        {
            return Json(await user.LoadUserBySurnamePhoneticize(phoneticize, pageSize, curPage));
        }

        #endregion

        /// <summary>
        /// 获取所有职称
        /// </summary>
        /// <returns>用户列表</returns>
        [HttpGet]
        public IHttpActionResult GetSYSPositionalTitleTypeList()
        {
            return Json(SYS_PositionalTitleTypeAdapter.Instance.GetAll().ToList());
        }


        /// <summary>
        /// 根据院系获取学者信息
        /// </summary>
        /// <param name="departid">院系结构id</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns>学者列表</returns>
        [HttpGet]
        public IHttpActionResult LoadUserByDepartID(string departid, int pageSize, int curPage)
        {
            return Json(user.LoadUserByDepartID(departid, pageSize, curPage));
        }

        /// <summary>
        /// 根据作者名称获取作者文献数量
        /// </summary>
        /// <param name="username">作者</param>
        /// <returns>数量</returns>
        public IHttpActionResult GetProductionCountByAuthor(string username)
        {
            return Json(user.GetProductionCountByAuthor(username));
        }

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户</returns>
        [HttpGet]
        public IHttpActionResult LoadUserByUserName(string username)
        {
            return Json(user.LoadUserByUserName(username));
        }

        /// <summary>
        /// 根据职称id获取职称
        /// </summary>
        /// <returns>职称</returns>
        [HttpGet]
        public IHttpActionResult GetSYSPositionalTitleTypeByID(Guid ppid)
        {
            return Json(SYS_PositionalTitleTypeAdapter.Instance.GetAll().Where(s => s.PttID == ppid).FirstOrDefault());
        }

        /// <summary>
        /// 根据部门id获取部门
        /// </summary>
        /// <returns>单位部门</returns>
        [HttpGet]
        public IHttpActionResult GetSYSCollegeByID(Guid deptID)
        {
            return Json(SYS_CollegeAdapter.Instance.GetAll().Where(s => s.CID == deptID).FirstOrDefault());

        }

        #region  用户收藏期刊

        /// <summary>
        /// 根据用户ID查询期刊集合
        /// </summary>
        ///<param name="sysuserID"></param>
        ///<param name="pageSize"></param>
        ///<param name="curPage"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserCollectPeriodicalBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {

            return Json(await relationUserCollectPeriodicalAdapter.LoadBySysUserID(sysuserID, pageSize, curPage));
        }

        /// <summary>
        /// 根据用户ID查询期刊集合数目
        /// </summary>
        ///<param name="sysuserID"></param>

        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserCollectPeriodicalBySysUserIDCount(Guid sysuserID)
        {

            return Json(await relationUserCollectPeriodicalAdapter.LoadBySysUserIDCount(sysuserID));
        }


        /// <summary>
        /// 添加/编辑  用户收藏期刊
        /// </summary>
        /// <param name="model">RelationUserCollectPeriodical表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRelationUserCollectPeriodical([FromBody]RelationUserCollectPeriodicalModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                RelationUserCollectPeriodicalModel Model = relationUserCollectPeriodicalAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (Model == null)//添加
                {
                    Model = new RelationUserCollectPeriodicalModel();
                    Model.UUID = Guid.NewGuid();
                }
                Model.CreateTime = model.CreateTime;
                Model.PeriodicalID = model.PeriodicalID;
                Model.PeriodicalName = model.PeriodicalName;
                Model.SysUserID = model.SysUserID;
                Model.ValidStatus = model.ValidStatus;


                int i = relationUserCollectPeriodicalAdapter.AddOrUpdate(Model);
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
        /// 删除期刊  用户收藏期刊
        /// </summary>
        /// <param name="model">RelationUserCollectPeriodical表</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserCollectPeriodical([FromBody]RelationUserCollectPeriodicalModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {

                int i = relationUserCollectPeriodicalAdapter.Remove(model);
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
        /// 删除期刊  用户收藏期刊
        /// </summary>
        /// <param name="idlist">id集合。id1,id2</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserCollectPeriodicalByList(string idlist)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            string[] ids = idlist.Split(',');
            try
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    RelationUserCollectPeriodicalModel model = new RelationUserCollectPeriodicalModel();
                    model.UUID = Guid.Parse(ids[j]);

                    int i = relationUserCollectPeriodicalAdapter.Remove(model);
                    if (i > 0)
                    {
                        isSucceed.IsSucceed = true;
                    }
                    else
                    {
                        isSucceed.IsSucceed = false;
                        isSucceed.ErrorMessage = "无操作记录!";
                    }
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
        /// 根据用户id，文献id获取用户收藏期刊
        /// </summary>
        /// <param name="sysuserID">用户id</param>
        /// <param name="productionID">文献id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadUserCollectPeriodicalBySysUserIDAndProductionID(Guid sysuserID, Guid productionID)
        {


            return Json(await relationUserCollectPeriodicalAdapter.LoadBySysUserIDAndProductionID(sysuserID, productionID));


        }

        #endregion

        #region  用户收藏作品


        /// <summary>
        /// 根据用户id，文献id获取用户收藏作品
        /// </summary>
        /// <param name="sysuserID">用户id</param>
        /// <param name="productionID">文献id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadUserCollectProductBySysUserIDAndProductionID(Guid sysuserID, Guid productionID)
        {


            return Json(await relationUserCollectProductAdapter.LoadBySysUserIDAndProductionID(sysuserID, productionID));


        }

        /// <summary>
        /// 根据用户ID查询作品集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="curPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserCollectProductBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            return Json(await relationUserCollectProductAdapter.LoadBySysUserID(sysuserID, pageSize, curPage));
        }

        /// <summary>
        /// 根据用户ID查询作品集合数目
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRelationUserCollectProductBySysUserIDCount(Guid sysuserID)
        {
            return Json(relationUserCollectProductAdapter.LoadBySysUserIDCount(sysuserID));
        }


        /// <summary>
        /// 添加/编辑  用户收藏作品
        /// </summary>
        /// <param name="userCollect">RelationUserCollectProduct表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRelationUserCollectProduct([FromBody]UserCollect userCollect)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                RelationUserCollectProductModel Model = new RelationUserCollectProductModel();

                Model.UUID = Guid.NewGuid();
                Model.CreateTime = DateTime.Now;
                Model.ProductionID = userCollect.CollectInfoID;
                Model.ProductionName = userCollect.CollectInfoName;
                Model.SysUserID = userCollect.UserID;
                Model.ValidStatus = true;

                int i = relationUserCollectProductAdapter.AddOrUpdate(Model);
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
        /// 取消 用户收藏作品
        /// </summary>
        /// <param name="userCollect">RelationUserCollectProduct表</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserCollectProduct([FromBody]UserCollect userCollect)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                RelationUserCollectProductModel Model = relationUserCollectProductAdapter.GetAll().Where(w => w.SysUserID == userCollect.UserID && w.ProductionID == userCollect.CollectInfoID).FirstOrDefault();
                int i = relationUserCollectProductAdapter.Remove(Model);
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
        /// 删除 用户收藏作品
        /// </summary>
        /// <param name="idlist">id集合。id1,id2</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserCollectProductByList(string idlist)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            string[] ids = idlist.Split(',');
            try
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    RelationUserCollectProductModel model = new RelationUserCollectProductModel();
                    model.UUID = Guid.Parse(ids[j]);
                    int i = relationUserCollectProductAdapter.Remove(model);

                    if (i > 0)
                    {
                        isSucceed.IsSucceed = true;
                    }
                    else
                    {
                        isSucceed.IsSucceed = false;
                        isSucceed.ErrorMessage = "无操作记录!";
                    }
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

        #region  用户收藏学者

        /// <summary>
        /// 根据用户id，学者id获取用户收藏学者
        /// </summary>
        /// <param name="sysuserID">用户id</param>
        /// <param name="scholarID">学者id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadUserCollectScholarBySysUserIDAndScholarID(Guid sysuserID, Guid scholarID)
        {
            return Json(await relationUserCollectScholarAdapter.LoadBySysUserIDAndScholarID(sysuserID, scholarID));
        }


        /// <summary>
        /// 根据用户ID查询收藏学者集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserCollectScholarBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            return Json(await relationUserCollectScholarAdapter.LoadBySysUserID(sysuserID, pageSize, curPage));
        }

        /// <summary>
        /// 根据用户ID查询学者集合数目
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRelationUserCollectScholarBySysUserIDCount(Guid sysuserID)
        {
            return Json(relationUserCollectScholarAdapter.LoadBySysUserIDCount(sysuserID));
        }


        /// <summary>
        /// 添加/编辑  用户收藏学者
        /// </summary>
        /// <param name="userCollect">RelationUserCollectScholar表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRelationUserCollectScholar([FromBody]UserCollect userCollect)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                RelationUserCollectScholarModel model = new RelationUserCollectScholarModel();

                model.UUID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.ScholarID = userCollect.CollectInfoID;
                model.ScholarName = userCollect.CollectInfoName;
                model.SysUserID = userCollect.UserID;
                model.ValidStatus = true;
                int i = relationUserCollectScholarAdapter.AddOrUpdate(model);
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
        /// 取消 用户收藏学者 
        /// </summary>
        /// <param name="userCollect">RelationUserCollectScholar表</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserCollectScholar([FromBody]UserCollect userCollect)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //    Relation_UserCollectScholar
                RelationUserCollectScholarModel model = relationUserCollectScholarAdapter.GetAll().Where(w => w.SysUserID == userCollect.UserID && w.ScholarID == userCollect.CollectInfoID).FirstOrDefault();
                if (model != null)
                {
                    bool boolFlag = relationUserCollectScholarAdapter.DelRelationUserCollectScholar(userCollect.UserID, userCollect.CollectInfoID);
                    if (boolFlag)
                    {
                        isSucceed.IsSucceed = true;
                    }else
                    {
                        isSucceed.IsSucceed = false;
                        isSucceed.ErrorMessage = "无操作记录!";
                    }
                    return Json(isSucceed);
                }
                isSucceed.IsSucceed = false;
                isSucceed.ErrorMessage = "无操作记录!";
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

        #region  用户收藏主题

        /// <summary>
        /// 根据用户id，文献id获取用户收藏主题
        /// </summary>
        /// <param name="sysuserID">用户id</param>
        /// <param name="themeID">主题id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadUserCollectThemeBySysUserIDAndThemeID(Guid sysuserID, Guid themeID)
        {


            return Json(await relationUserCollectThemeAdapter.LoadBySysUserIDAndThemeID(sysuserID, themeID));


        }


        /// <summary>
        /// 根据用户ID查询主题集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserCollectThemeBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            return Json(await relationUserCollectThemeAdapter.LoadBySysUserID(sysuserID, pageSize, curPage));
        }

        /// <summary>
        /// 根据用户ID查询主题集合数目
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>


        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserCollectThemeBySysUserIDCount(Guid sysuserID)
        {
            return Json(await relationUserCollectThemeAdapter.LoadBySysUserIDCount(sysuserID));
        }


        /// <summary>
        /// 添加/编辑  用户收藏主题
        /// </summary>
        /// <param name="model">RelationUserCollectTheme表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRelationUserCollectTheme([FromBody]RelationUserCollectThemeModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                RelationUserCollectThemeModel Model = relationUserCollectThemeAdapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (Model == null)//添加
                {
                    Model = new RelationUserCollectThemeModel();
                    Model.UUID = Guid.NewGuid();
                }
                Model.CreateTime = model.CreateTime;
                Model.ThemeID = model.ThemeID;
                Model.ThemeName = model.ThemeName;
                Model.SysUserID = model.SysUserID;
                Model.ValidStatus = model.ValidStatus;


                int i = relationUserCollectThemeAdapter.AddOrUpdate(Model);
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
        /// 删除 用户收藏主题
        /// </summary>
        /// <param name="model">RelationUserCollectTheme表</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserCollectTheme([FromBody]RelationUserCollectThemeModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {

                int i = relationUserCollectThemeAdapter.Remove(model);
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

        #region  用户作品认领与全文提交

        /// <summary>
        /// 根据用户ID，认领作品状态 查询用户作品认领与全文提交
        /// </summary>
        /// <param name="sysuserID">用户ID</param>
        /// <param name="UserClaimWorksStatus">认领作品状态：待认领为0；未认领为1；已认领为2；拒绝认领为3；文件为完整上传4</param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserClaimWorksBySysUserIDAndStatus(Guid sysuserID, int UserClaimWorksStatus, int pageSize, int curPage)
        {
            return Json(await relationUserClaimWorksAdapter.LoadBySysUserIDAndStatus(sysuserID, UserClaimWorksStatus, pageSize, curPage));
        }

        /// <summary>
        /// 根据用户ID/名称/英文名称 查询用户作品认领
        /// </summary>
        /// <param name="sysuserID">用户ID</param>
        /// <param name="chineseName">用户名称</param>
        /// <param name="englishName">用户英文名称</param>
        /// <param name="pageSize">页条数</param>
        /// <param name="curPage">页数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetClaimWorksCombineBySysUserIDAndStatus(Guid sysuserID, string chineseName, string englishName, int pageSize, int curPage)
        {
            return Json(await relationUserClaimWorksAdapter.LoadClaimWorksCombineBySysUserID(sysuserID, chineseName, englishName, pageSize, curPage));
        }

        /// <summary>
        /// 未提交全文
        /// </summary>
        /// <param name="sysuserID">用户ID</param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserClaimWorksBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            return Json(await relationUserClaimWorksAdapter.LoadBySysUserID(sysuserID, pageSize, curPage));
        }


        /// <summary>
        /// 添加/编辑   用户作品认领与全文提交
        /// </summary>
        /// <param name="model">RelationUserClaimWorks表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRelationUserClaimWorks([FromBody]RelationUserClaimWorksModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                RelationUserClaimWorksModel Model = relationUserClaimWorksAdapter.GetAll().Where(w => w.UserClaimWorksID == model.UserClaimWorksID).FirstOrDefault();
                if (Model == null)//添加
                {
                    Model = new RelationUserClaimWorksModel();
                    Model.UserClaimWorksID = Guid.NewGuid();
                }
                Model.ProductionID = model.ProductionID;
                Model.SysUserID = model.SysUserID;
                Model.UserClaimWorksStatus = model.UserClaimWorksStatus;
                Model.IsHave = model.IsHave;
                Model.AuthorOrder = model.AuthorOrder;
                Model.CorrespondenceAuthor = model.CorrespondenceAuthor;
                Model.ParticipatingAuthor = model.ParticipatingAuthor;

                int i = relationUserClaimWorksAdapter.AddOrUpdate(Model);
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
        /// 添加/编辑   用户作品认领与全文提交
        /// </summary>
        /// <param name="model">RelationUserClaimWorks表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateRelationUserClaimWorksByUserID([FromBody]RelationUserClaimWorksModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                RelationUserClaimWorksModel Model = relationUserClaimWorksAdapter.GetAll().Where(w => w.ProductionID == model.ProductionID && w.SysUserID == model.SysUserID).FirstOrDefault();
                if (Model == null)//添加
                {
                    Model = new RelationUserClaimWorksModel();
                    Model.UserClaimWorksID = Guid.NewGuid();
                }
                Model.ProductionID = model.ProductionID;
                Model.SysUserID = model.SysUserID;
                Model.UserClaimWorksStatus = model.UserClaimWorksStatus;
                Model.IsHave = Convert.ToInt32(model.IsHave);
                Model.AuthorOrder = Convert.ToInt32(model.AuthorOrder);
                Model.CorrespondenceAuthor = Convert.ToInt32(model.CorrespondenceAuthor);
                Model.ParticipatingAuthor = Convert.ToInt32(model.ParticipatingAuthor);

                int i = relationUserClaimWorksAdapter.AddOrUpdate(Model);
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
        /// 删除 用户作品认领与全文提交
        /// </summary>
        /// <param name="model">relationUserClaimWorks表</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteRelationUserClaimWorks([FromBody]RelationUserClaimWorksModel model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                int i = relationUserClaimWorksAdapter.Remove(model);
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
        /// 根据用户ID，认领作品状态 查询用户作品认领与全文提交数目
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="UserClaimWorksStatus">认领作品状态：待认领为0；未认领为1；已认领为2；拒绝认领为3；文件为完整上传4</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetRelationUserClaimWorksBySysUserIDAndStatusCount(Guid sysuserID, int UserClaimWorksStatus)
        {
            return Json(await relationUserClaimWorksAdapter.LoadBySysUserIDAndStatusCount(sysuserID, UserClaimWorksStatus));
        }



        #endregion


        #region  用户评论与私信

        /// <summary>
        /// 获取文献用户评论信息 
        /// </summary>
        /// <param name="productionid">文献id</param>
        /// <param name="type">评论类型0普通、1专家、2报错</param> 
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadUserCommentListByProductionIDAndType(string productionid, string type, int pageSize, int curPage)
        {
            return Json(await usercommentadapter.LoadUserCommentListByProductionIDAndType(productionid, type, pageSize, curPage));
        }

        /// <summary>
        /// 获取文献用户评论信息 
        /// </summary>
        /// <param name="userID">用户</param>
        /// <param name="type">评论类型0普通、1专家、2报错</param> 
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IHttpActionResult> LoadUserCommentListByUserIDAndType(string userID, string type, int pageSize, int curPage)
        {
            return Json(await usercommentadapter.LoadUserCommentListByUserIDAndType(userID, type, pageSize, curPage));
        }

        /// <summary>
        /// 添加/编辑   文献用户评论信息
        /// </summary>
        /// <param name="model">UserComment表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserComment([FromBody]UserCommentModels model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserCommentModels Model = usercommentadapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (Model == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }
                int i = usercommentadapter.AddOrUpdate(model);
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
        /// 删除  文献用户评论信息
        /// </summary>
        /// <param name="model">UserComment表</param>
        /// <returns></returns>

        [HttpPost]
        public IHttpActionResult DeleteUserComment([FromBody]UserCommentModels model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                int i = usercommentadapter.Remove(model);
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
        /// 获取文献用户私信信息
        /// </summary>
        /// <param name="userid">接受人id</param>
        /// <param name="senduserid">发送人id</param> 
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IHttpActionResult> GetUserPrivateLetterListByUserIDOrSendUserid(string userid, string senduserid, int pageSize, int curPage)
        {
            return Json(await userprivateletteradapter.GetUserPrivateLetterListByUserIDOrSendUserid(userid, senduserid, pageSize, curPage));
        }

        /// <summary>
        /// 添加/编辑   文献用户 私信信息
        /// </summary>
        /// <param name="model">UserComment表实体</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserPrivateLetter([FromBody]UserPrivateLetterModels model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                UserPrivateLetterModels Model = userprivateletteradapter.GetAll().Where(w => w.UUID == model.UUID).FirstOrDefault();
                if (Model == null)//添加
                {
                    model.UUID = Guid.NewGuid();
                }
                int i = userprivateletteradapter.AddOrUpdate(model);
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
        /// 删除  文献用户私信信息
        /// </summary>
        /// <param name="model">UserPrivateLetter</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteUserPrivateLetter([FromBody]UserPrivateLetterModels model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                int i = userprivateletteradapter.Remove(model);
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

        #region  用户引用文献

        /// <summary>
        /// 根据用户id，获取用户引用文献
        /// </summary>
        /// <param name="sysuserID">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult LoadUserCitationProductionsByUserid(Guid sysuserID)
        {
            return Json(usercitationproductionsadapter.GetAll().Where(s => s.UserID == sysuserID).ToList());
        }


        /// <summary>
        /// 根据文献ID查询用户引用文献信息
        /// </summary>
        /// <param name="productionid"></param>
        /// <returns></returns>


        [HttpGet]
        public IHttpActionResult GetUserCitationProductionsByProductioid(Guid productionid)
        {
            return Json(usercitationproductionsadapter.GetAll().Where(s => s.ProductionID == productionid).ToList());
        }




        /// <summary>
        /// 添加/编辑  用户引用的文献信息
        /// </summary>
        /// <param name="model">UserCitationProductions</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOrUpdateUserCitationProductions([FromBody]UserCitationProductions model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {
                //            
                UserCitationProductions Model = usercitationproductionsadapter.GetAll().Where(w => w.CitationID == model.CitationID).FirstOrDefault();
                if (Model == null)//添加
                {
                    Model = new UserCitationProductions();
                    Model.CitationID = Guid.NewGuid();
                }



                int i = usercitationproductionsadapter.AddOrUpdate(Model);
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
        /// 删除 用户引用得文献
        /// </summary>
        /// <param name="model">UserCitationProductions</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DeleteUserCitationProductions([FromBody]UserCitationProductions model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            try
            {

                int i = usercitationproductionsadapter.Remove(model);
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

        /// <summary>
        ///保存附件
        /// </summary>
        /// <returns></returns>

        public async Task<IHttpActionResult> SavaFiles()
        {
            OperationFiles operationFiles = new OperationFiles();
            // WebModelIsSucceed WebModelIsSucceed = 
            return Json(await operationFiles.SaveFile(new Guid(HttpContext.Current.Request["ProductionID"])));
        }

        /// <summary>
        ///保存附件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> SavaFilesTest(Guid guid)
        {
            OperationFiles operationFiles = new OperationFiles();
            // WebModelIsSucceed WebModelIsSucceed = 
            return Json(await operationFiles.SaveFile(guid));
        }
    }
}
