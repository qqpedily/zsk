using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using Com.Weehong.Elearning.MasterData.DataModels;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 用户评论
    /// </summary>
    public class UserCommentAdapter : EffectedAdapterBase<UserCommentModels, List<UserCommentModels>>
    {
        /// <summary>
        /// 获取文献用户评论信息 
        /// </summary>
        /// <param name="productionid">文献id</param>
        /// <param name="type">评论类型0普通、1专家、2报错</param> 
        /// <returns></returns>
        public async Task<DicData> LoadUserCommentListByProductionIDAndType(string productionid,string type, int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {

                string sql = @" select uc.UUID,uc.Content,uc.CreateTime,uc.Level,uc.ParentID,uc.ProductionID
	,uc.Type,uc.UserID,uc.ValidStatus,u.SurnameChinese+u.NameChinese as UserName,uph.UploadIMG as UploadIMG 
	from UserComment uc left join [User] u
   on uc.UserID=u.UUID 
   left join UserPersonalHomepage uph on u.UUID=uph.UserID
   WHERE 1=1  ";
                

                if (!string.IsNullOrWhiteSpace(productionid))
                {
                    sql += "  and uc.ProductionID  ='"+ productionid + "' ";
                }
                if (!string.IsNullOrWhiteSpace(productionid))
                {
                    sql += "  and uc.Type='"+type+"' ";
                }
                sql += "    order by CreateTime desc  ";
                List<UserCommentModels> list = await db.Database.SqlQuery<UserCommentModels>(sql).ToListAsync();


                DicData dicdata = new DicData();
             

                int count = list.Count;
                List<UserCommentModels> lis = list.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                dicdata.Data = lis;
                dicdata.TotalCount = count;


                return dicdata;
            }
        }

        /// <summary>
        /// 获取文献用户评论信息 
        /// </summary>
        /// <param name="productionid">文献id</param>
        /// <param name="type">评论类型0普通、1专家、2报错</param> 
        /// <returns></returns>
        public async Task<DicData> LoadUserCommentListByUserIDAndType(string userid, string type, int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {

                string sql = @" select uc.UUID,uc.Content,uc.CreateTime,uc.Level,uc.ParentID,uc.ProductionID
	,uc.Type,uc.UserID,uc.ValidStatus,u.SurnameChinese+u.NameChinese as UserName,uph.UploadIMG as UploadIMG 
	from UserComment uc left join [User] u
   on uc.UserID=u.UUID 
   left join UserPersonalHomepage uph on u.UUID=uph.UserID
   WHERE 1=1  ";


                if (!string.IsNullOrWhiteSpace(userid))
                {
                    sql += "  and uc.UserID  ='" + userid + "' ";
                }
                if (!string.IsNullOrWhiteSpace(userid))
                {
                    sql += "  and uc.Type='" + type + "' ";
                }
                sql += "    order by CreateTime desc  ";
                List<UserCommentModels> list = await db.Database.SqlQuery<UserCommentModels>(sql).ToListAsync();


                DicData dicdata = new DicData();


                int count = list.Count;
                List<UserCommentModels> lis = list.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                dicdata.Data = lis;
                dicdata.TotalCount = count;


                return dicdata;
            }
        }
    }
}
