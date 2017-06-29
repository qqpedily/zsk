using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.DataModels;
using Com.Weehong.Elearning.MasterData.Repositories;
using System.Data.Entity;
using System.Collections.Generic;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 关注的学者
    /// </summary>
    public class RelationUserAttentionScholarAdapter : EffectedAdapterBase<RelationUserAttentionScholarModel, List<RelationUserAttentionScholarModel>>
    {

        /// <summary>
        /// 根据用户ID查询学者集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>
        public async Task<DicData> LoadBySysUserID(Guid sysuserID,int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {
               

                List <RelationUserAttentionScholarModel> lis = await db.RelationUserAttentionScholar.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).ToListAsync();
              
            

                string[] values = new string[lis.Count];

                for (int i = 0; i < lis.Count; i++)
                {
                   
                    values[i] = lis[i].ScholarName;

                }
               
           

                ProductionsAdapter proadapter = new ProductionsAdapter();
                Task<DicData> dic =  proadapter.LoadProductionByMetaData("author", values, pageSize, curPage, true);

                return await dic;
                
            }
        }
        public async Task<int> LoadBySysUserIDCount(Guid sysuserID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.RelationUserAttentionScholar.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).ToList().Count;
            }
        }

        //根据用户id 和学者id 获取关注学者信息
        public async Task<RelationUserAttentionScholarModel> LoadBySysUserIDAndScholarID(Guid sysuserID,Guid scholarID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.RelationUserAttentionScholar.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID && w.ScholarID==scholarID).ToList().FirstOrDefault();
            }
        }

    }
}
