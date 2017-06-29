using Com.Weehong.Elearning.Domain.WebModel;
using Com.Weehong.Elearning.MasterData.DataModels.UseGroup;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace YinGu.Operation.Framework.Domain.UseGroup
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class TransRelation_UseGroup
    {
        /// <summary>
        /// 添加用户组信息  邀请人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<WebModelIsSucceed> AddOrUpdateRelation_UseGroup(Relation_UseGroup model)
        {
            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    List<Relation_UseGroup_User> list = new List<Relation_UseGroup_User>();
                    try
                    {
                        Relation_UseGroup rrelation_UseGroupModel = null;// new Relation_UseGroup();// useGroupAdapter.GetAll().Where(w => w.UseGroupID == model.UseGroupID).FirstOrDefault();
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
                        if (model.Relation_UseGroup_User != null)//邀请人信息
                        {
                            foreach (var Relation_UseGroup_User in model.Relation_UseGroup_User)
                            {
                                Relation_UseGroup_User relation_UseGroup_User = new Relation_UseGroup_User();
                                relation_UseGroup_User.UUID = Guid.NewGuid();
                                relation_UseGroup_User.UseGroupID = rrelation_UseGroupModel.UseGroupID;
                                relation_UseGroup_User.SysUserID = Relation_UseGroup_User.SysUserID;
                                relation_UseGroup_User.InvitationTime = DateTime.Now;
                                relation_UseGroup_User.Join = 0;
                                relation_UseGroup_User.sort = Relation_UseGroup_User.sort ==null ? 0: Relation_UseGroup_User.sort;
                                list.Add(relation_UseGroup_User);
                            }
                            //加入创建组人信息 创建人自己
                            Relation_UseGroup_User relation_UseGroup_UserAdmin = new Relation_UseGroup_User();
                            relation_UseGroup_UserAdmin.UUID = Guid.NewGuid();
                            relation_UseGroup_UserAdmin.UseGroupID = rrelation_UseGroupModel.UseGroupID;
                            relation_UseGroup_UserAdmin.SysUserID = model.SponsorID;
                            relation_UseGroup_UserAdmin.InvitationTime = DateTime.Now;
                            relation_UseGroup_UserAdmin.Join = 1;
                            relation_UseGroup_UserAdmin.sort = 0;
                            list.Add(relation_UseGroup_UserAdmin);
                        }

                        db.Relation_UseGroup.AddOrUpdate(rrelation_UseGroupModel);
                        if(list.Count >0)
                        db.Relation_UseGroup_User.AddOrUpdate(list.ToArray());

                        isSucceed.IsSucceed = await db.SaveChangesAsync() > 0 ? true : false;
                       
                        dbContextTransaction.Commit();
                        return isSucceed;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        isSucceed.IsSucceed = false;
                        isSucceed.ErrorMessage = ex.Message + "\r\n" + ex.StackTrace;
                        return isSucceed;
                    }

                }
            }

        }

        /// <summary>
        /// 删除用户组成员   删除用户组的用
        /// </summary>
        /// <param name="UseGroupID">用户ID</param>
        /// <returns></returns>
        public bool DelUseGroup_User(Guid? UseGroupID)
        {
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                if (UseGroupID.HasValue)
                {
                    try
                    {
                        int i = db.Database.ExecuteSqlCommand("DELETE FROM Relation_UseGroup_User WHERE UseGroupID='"+ UseGroupID + "'");
                        if (i > -1)
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {

                        return false;
                    }
                }
            }

            return false;
        }




    }
}
