using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 用户_联系信息
    /// </summary>
    public class UserContactInformationAdapter :EffectedAdapterBase<UserContactInformationModel, List<UserContactInformationModel>>
    {
        /// <summary>
        /// 根据用户ID  获取用户联系人信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<List<UserContactInformationModel>> GetUserContactInformationAsync(Guid UserID)
        {

            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT * FROM UserContactInformation as  uc WHERE uc.UserID='" + UserID + "'";

                return await db.Database.SqlQuery<UserContactInformationModel>(sql).ToListAsync();
            }
        }
        /// <summary>
        ///根据用户联系人信息信息ID    获取用户联系人信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<UserContactInformationModel> GetUserContactInformationInfoAsync(Guid UserID)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT * FROM UserContactInformation as  uc WHERE uc.UserID='" + UserID + "'";
                return await db.Database.SqlQuery<UserContactInformationModel>(sql).FirstOrDefaultAsync();
            }
        }
        





    }
}
