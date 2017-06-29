using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.UseGroup;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.UseGroup
{
    /// <summary>
    /// 用户组合用户关联表
    /// </summary>
    public class Relation_UseGroup_UserAdapter : EffectedAdapterBase<Relation_UseGroup_User, List<Relation_UseGroup_User>>
    {

        public static readonly Relation_UseGroup_UserAdapter Instance = new Relation_UseGroup_UserAdapter();
        /// <summary>
        /// 获取已加入本组的人员信息 自己除外 【By ZHL】
        ///【by ZHL】
        /// </summary>
        /// <param name="UserGroupID"></param>
        /// <param name="SysUserID"></param>
        /// <returns></returns>
        public async Task<List<UserList>> LoadByUserDataAsync(Guid? UserGroupID, Guid? SysUserID)
        {
            List<UserList> list = new List<UserList>();
            if (!UserGroupID.HasValue && UserGroupID.HasValue)
                using (var db = new OperationManagerDbContext())
                {
                    string sql = @" SELECT (u.SurnameChinese+ISNULL(u.NameChinese,'')) AS UserName FROM Relation_UseGroup_User AS r LEFT JOIN dbo.[User] AS u ON
                            u.UUID = r.SysUserID WHERE 1=1 ";// "
                                                             //本组   抛出本人  已同意加入的人
                    sql += " AND r.UseGroupID='" + UserGroupID + "' AND  u.UUID <>'" + SysUserID + "' AND [Join]=1 ";
                    list = await db.Database.SqlQuery<UserList>(sql + "").ToListAsync();
                    return list;
                }
            return list;
        }

        /// <summary>
        /// 获取加入本组的人员信息 自己除外 【By ZHL】
        ///【by ZHL】
        /// </summary>
        /// <param name="UserGroupID"></param>
        /// <param name="SysUserID"></param>
        /// <returns></returns>
        public List<UserList> LoadByUserData(Guid? UserGroupID, Guid? SysUserID)
        {
            List<UserList> list = new List<UserList>();
            if (UserGroupID.HasValue)//&& SysUserID.HasValue
                using (var db = new OperationManagerDbContext())
                {
                    string sql = @" SELECT (u.SurnameChinese+ISNULL(u.NameChinese,'')) AS UserName FROM Relation_UseGroup_User AS r
 LEFT JOIN dbo.Relation_UseGroup AS ru ON r.UseGroupID =ru.UseGroupID 
 LEFT JOIN   dbo.[User] AS u ON u.UUID = r.SysUserID  WHERE 1=1  ";// "
                                                             //本组   抛出本人  已同意加入的人
                    sql += " AND r.UseGroupID='" + UserGroupID + "' AND [Join]=1 ";
                    //sql += " AND r.UseGroupID='" + UserGroupID + "' AND  u.UUID <>'" + SysUserID + "' AND [Join]=1 ";
                    list =  db.Database.SqlQuery<UserList>(sql + "").ToList();
                    return list;
                }
            return list;
        }


        /// <summary>
        /// 获取已加入本组的人员信息 自己除外 【By ZHL】
        ///【by ZHL】
        /// </summary>
        /// <param name="UserGroupID"></param>
        /// <param name="SysUserID"></param>
        /// <returns></returns>
        public List<UserList> LoadByUser(Guid? UserGroupID, Guid? SysUserID)
        {
            List<UserList> list = new List<UserList>();
            if (UserGroupID.HasValue && SysUserID.HasValue)
                using (var db = new OperationManagerDbContext())
                {
                    string sql = @" SELECT (u.SurnameChinese+ISNULL(u.NameChinese,'')) AS UserName FROM Relation_UseGroup_User AS r LEFT JOIN dbo.[User] AS u ON
                            u.UUID = r.SysUserID WHERE 1=1 ";// "
                                                             //本组   抛出本人  已同意加入的人
                    sql += " AND r.UseGroupID='" + UserGroupID + "' AND  u.UUID <>'" + SysUserID + "' AND [Join]=1 ";
                    list = db.Database.SqlQuery<UserList>(sql + "").ToList();
                    return list;
                }
            return list;
        }

    }
}
