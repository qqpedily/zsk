using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.DataModels;
using Com.Weehong.Elearning.MasterData.Repositories;
using System.Data.Entity;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 用户认领作品
    /// </summary>
    public class RelationUserClaimWorksAdapter : EffectedAdapterBase<RelationUserClaimWorksModel, List<RelationUserClaimWorksModel>>
    {
        public static readonly RelationUserClaimWorksAdapter Instance = new RelationUserClaimWorksAdapter();

        /// <summary>
        /// 获取未提交全文数
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>
        public async Task<int> LoadNotAttachmentCount(Guid sysuserID)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT COUNT(1) AS NotAttachmentCount from dbo.StaticProductions sp LEFT JOIN dbo.Comm_Attachment a ON sp.ProductionID=a.BusinessID
 WHERE a.UUID IS NULL AND sp.UserID='" + sysuserID + "'";

                return await db.Database.SqlQuery<int>(sql).FirstOrDefaultAsync();
            }
        }

       /// <summary>
       /// 获取待认领数
       /// </summary>
       /// <param name="sysuserID"></param>
       /// <param name="chineseName"></param>
       /// <param name="englishName"></param>
       /// <returns></returns>
        public async Task<int> LoadSheltersCount(Guid sysuserID, string chineseName, string englishName)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT COUNT(DISTINCT sp.ProductionID) AS SheltersCount FROM dbo.StaticProductions sp WHERE (author LIKE '%" + chineseName + "%' OR author LIKE '%" + englishName + "%' OR sp.UserID= '" + sysuserID + "') AND sp.ProductionID NOT IN (select ProductionID from Relation_UserClaimWorks WHERE  cast(SysUserID as varchar(36)) ='"+ sysuserID + "' AND UserClaimWorksStatus <> '0')";
                
                return await db.Database.SqlQuery<int>(sql).FirstOrDefaultAsync(); ;
            }
        }

        public async Task<int> LoadBySysUserIDAndStatusCount(Guid sysuserID, int UserClaimWorksStatus)
        {
            using (var db = new OperationManagerDbContext())
            {

                return db.RelationUserClaimWorks.AsNoTracking().OrderBy(c => c.SysUserID).Where(w => w.SysUserID == sysuserID && w.UserClaimWorksStatus == UserClaimWorksStatus).ToListAsync().Result.Count;
            }
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
        public async Task<DicData> LoadClaimWorksCombineBySysUserID(Guid sysuserID, string chineseName, string englishName, int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {
                DicData dic = new DicData();
                //sp.UserID IN (SELECT UUID FROM dbo.[User] WHERE REPLACE(SurnameChinese,' ','') + REPLACE(NameChinese,' ', '')='" + chineseName.Trim() + "' OR REPLACE(SurnamePhoneticize,' ', '')+REPLACE(NamePhoneticize, ' ', '')='" + englishName.Trim() + "')
                //author LIKE '%zhujiu%' OR author LIKE '%朱九%'
                string sql = @"SELECT  DISTINCT sp.ProductionID, * FROM dbo.StaticProductions sp WHERE (author LIKE '%" + chineseName.Trim() + "%' OR author LIKE '%" + englishName.Trim() + "%' OR sp.UserID='" + sysuserID + "') AND sp.ProductionID NOT IN (select ProductionID from Relation_UserClaimWorks WHERE  cast(SysUserID as varchar(36)) ='" + sysuserID + "' AND UserClaimWorksStatus <> '0')";

                List<StaticProductions> lis = await db.Database.SqlQuery<StaticProductions>(sql).ToListAsync();

                List<StaticProductions> list = lis.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                int count = lis.Count;
                dic.TotalCount = count;
                dic.Data = list;

                return dic;
            }
        }


        /// <summary>
        /// 根据用户ID查询用户认领作品
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="userClaimWorksStatus">认领作品状态：待认领为0；未认领为1；已认领为2；拒绝认领为3；文件为完整上传4</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns></returns>
        public async Task<DicData> LoadBySysUserIDAndStatus(Guid sysuserID, int userClaimWorksStatus, int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {
                DicData dic = new DicData();

                string sql = @" select ru.* from Relation_UserClaimWorks ru inner join      dbo.StaticProductions p
                            on cast(p.ProductionID as varchar(36)) =ru.ProductionID WHERE 1=1";

                sql += "  and cast(ru.SysUserID as varchar(36)) ='" + sysuserID + "' and ru.UserClaimWorksStatus='" + userClaimWorksStatus + "'";

                List<RelationUserClaimWorksModel> lis = await db.Database.SqlQuery<RelationUserClaimWorksModel>(sql).ToListAsync();

                List<RelationUserClaimWorksModel> list = lis.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                int count = lis.Count;
                dic.TotalCount = count;
                dic.Data = list;

                return dic;
            }
        }


        /// <summary>
        /// 根据用户ID查询用户认领作品,未提交全文
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns></returns>
        public async Task<DicData> LoadBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {
                DicData dic = new DicData();

                string sql = @" select * from dbo.StaticProductions sp LEFT JOIN dbo.Comm_Attachment a ON sp.ProductionID=a.BusinessID
 WHERE a.UUID IS NULL AND sp.UserID='" + sysuserID + "'";

                List<StaticProductions> lis = await db.Database.SqlQuery<StaticProductions>(sql).ToListAsync();

                List<StaticProductions> list = lis.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                int count = lis.Count;

                dic.TotalCount = count;
                dic.Data = list;

                return dic;
            }
        }
    }
}
