using Com.Weehong.Elearning.DataModels;
using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter
{
    /// <summary>
    /// 附件
    /// </summary>
    public class AttachmentAdapter : EffectedAdapterBase<AttachmentModel, List<AttachmentModel>>
    {

        public static readonly AttachmentAdapter Instance = new AttachmentAdapter();
        /// <summary>
        /// 根据业务ID查询附件集合
        /// </summary>
        /// <param name="businessID">文献ID</param>
        /// <returns></returns>
        public async Task<List<AttachmentModel>> LoadListByOrderNumAsync(Guid businessID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.Attachment.AsNoTracking().Where(w => w.BusinessID == businessID).ToListAsync();
            }
        }

        /// <summary>
        /// 根据业务ID查询附件集合
        /// </summary>
        /// <param name="businessID">文献ID</param>
        /// <returns></returns>
        public async Task<AttachmentModel> LoadByOrderNumAsync(Guid businessID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.Attachment.AsNoTracking().Where(w => w.BusinessID == businessID).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// 根据业务ID查询附件集合
        /// </summary>
        /// <param name="productionID">文献ID</param>
        /// <returns></returns>
        public List<AttachmentModel> LoadByProductionsAsync(string productionID)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"select ca.* from  Comm_Attachment   ca
                                where ca.BusinessID='" + productionID + "'";

                var list =  db.Database.SqlQuery<AttachmentModel>(sql).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据用户ID查询用户联系信息
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <returns></returns>
        public UserPersonalHomepageModel LoadUserPersonalHomepageByUseriD(string userID)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"select * from UserPersonalHomepage where UserID='"+userID+"'";

                var list = db.Database.SqlQuery<UserPersonalHomepageModel>(sql).ToList().FirstOrDefault();
                return list;
            }
        }
    }
}
