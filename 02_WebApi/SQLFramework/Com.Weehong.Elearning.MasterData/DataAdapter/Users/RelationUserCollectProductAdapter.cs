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

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 产品
    /// </summary>
    public class RelationUserCollectProductAdapter : EffectedAdapterBase<RelationUserCollectProductModel, List<RelationUserCollectProductModel>>
    {

        /// <summary>
        /// 根据用户ID查询产品集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        public async Task<DicData> LoadBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {
                DicData dic = new DicData();
               // int lis = db.RelationUserCollectProduct.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).Count();
               int lis = db.RelationUserCollectProduct.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).Count();

                List<RelationUserCollectProductModel> list = db.RelationUserCollectProduct.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                //  int count = lis;
                dic.Data = list;
                dic.TotalCount = lis;
                return dic;
            }
        }
        public int LoadBySysUserIDCount(Guid sysuserID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.RelationUserCollectProduct.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).Count();
            }
        }

        public async Task<RelationUserCollectProductModel> LoadBySysUserIDAndProductionID(Guid sysuserID, Guid productionID)
        {
            using (var db = new OperationManagerDbContext())
            {

                return await db.RelationUserCollectProduct.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID && w.ProductionID == productionID).FirstOrDefaultAsync();
            }
        }

    }
}
