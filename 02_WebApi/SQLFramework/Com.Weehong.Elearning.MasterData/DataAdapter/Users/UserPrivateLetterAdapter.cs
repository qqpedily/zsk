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
    /// 用户评论私信
    /// </summary>
    public class UserPrivateLetterAdapter : EffectedAdapterBase<UserPrivateLetterModels, List<UserPrivateLetterModels>>
    {
        /// <summary>
        /// 获取文献用户私信信息 
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="senduserid">发送用户id</param> 
        /// <returns></returns>
        public async Task<DicData> GetUserPrivateLetterListByUserIDOrSendUserid(string userid,string senduserid, int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {

                string sql = @"    select up.UUID,up.UserID,up.SendUserID,up.Content,up.type,up.createtime,up.ValidStatus,
    (select u.SurnameChinese+u.NameChinese from  [User] u where u.UUID=up.UserID) as UserName,
	 (select u.UploadIMG from  UserPersonalHomepage u where u.UserID=up.UserID) as UserUploadIMG,
    (select u.SurnameChinese+u.NameChinese from  [User] u where u.UUID=up.SendUserID) as SendUserName,
	 (select u.UploadIMG from  UserPersonalHomepage u where u.UserID=up.SendUserID) as SendUserUploadIMG
    from UserPrivateLetter up where 1=1  ";
                

                if (!string.IsNullOrWhiteSpace(userid))
                {
                    sql += " and up.UserID ='" + userid + "' ";
                }
                if (!string.IsNullOrWhiteSpace(senduserid))
                {
                    sql += "  and up.SendUserID='" + senduserid + "' ";
                }

                sql += "    order by CreateTime desc  ";
                List<UserPrivateLetterModels> list = await db.Database.SqlQuery<UserPrivateLetterModels>(sql).ToListAsync();


                DicData dicdata = new DicData();
             

                int count = list.Count;
                List<UserPrivateLetterModels> lis = list.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                dicdata.Data = lis;
                dicdata.TotalCount = count;


                return dicdata;
            }
        }
    }
}
