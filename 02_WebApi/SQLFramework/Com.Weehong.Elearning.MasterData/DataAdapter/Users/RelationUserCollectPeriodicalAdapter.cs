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
    /// 期刊
    /// </summary>
    public class RelationUserCollectPeriodicalAdapter : EffectedAdapterBase<RelationUserCollectPeriodicalModel, List<RelationUserCollectPeriodicalModel>>
    {
        public static readonly RelationUserCollectPeriodicalAdapter Instance = new RelationUserCollectPeriodicalAdapter();

        /// <summary>
        /// 根据用户ID查询期刊集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>
        public async Task<DicData> LoadBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {
                DicData dic = new DicData();
                List<RelationUserCollectPeriodicalModel> lis =
                 await db.RelationUserCollectPeriodical.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToListAsync();

                int count = lis.Count;
                List<RelationUserCollectPeriodicalModel> list = lis.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                dic.Data = list;
                dic.TotalCount = count;
                return dic;

            }
        }
        public async Task<int> LoadBySysUserIDCount(Guid sysuserID)
        {
            using (var db = new OperationManagerDbContext())
            {

                return db.RelationUserCollectPeriodical.OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).ToList().Count;
            }
        }

        public async Task<RelationUserCollectPeriodicalModel> LoadBySysUserIDAndProductionID(Guid sysuserID,Guid productionID)
        {
            using (var db = new OperationManagerDbContext())
            {

                return db.RelationUserCollectPeriodical.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID && w.PeriodicalID==productionID).ToList().FirstOrDefault();
            }
        }
    }
}
