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

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 关注的期刊
    /// </summary>
    public class RelationUserAttentionPeriodicalAdapter : EffectedAdapterBase<RelationUserAttentionPeriodicalModel, List<RelationUserAttentionPeriodicalModel>>
    {
        public static readonly RelationUserAttentionPeriodicalAdapter Instance = new RelationUserAttentionPeriodicalAdapter();

        /// <summary>
        /// 根据用户ID查询期刊集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>
        //public async Task<DicData> LoadBySysUserID(Guid sysuserID, int pageSize, int curPage)
        //{
        //    using (var db = new OperationManagerDbContext())
        //    {
        //        DicData dic = new DicData();
        //        List<RelationUserAttentionPeriodicalModel> lis = await db.RelationUserAttentionPeriodical.OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).ToListAsync();
        //        int count = lis.Count;
        //        List<RelationUserAttentionPeriodicalModel> list = lis.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
        //        dic.Data = list;
        //        dic.TotalCount = count;
        //        return dic;


        //    }
        //}

        //根据用户id 和文献id获取 关注信息
        public async Task<RelationUserAttentionPeriodicalModel> LoadBySysUserIDAndProdctionID(Guid sysuserID,Guid productionid)
        {
            using (var db = new OperationManagerDbContext())
            {
              
                return   db.RelationUserAttentionPeriodical.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID && w.PeriodicalID==productionid).ToList().FirstOrDefault();
            }
        }


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
                List<RelationUserAttentionPeriodicalModel> lis = await db.RelationUserAttentionPeriodical.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).ToListAsync();
                int count = lis.Count;
                List<RelationUserAttentionPeriodicalModel> list = lis.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                dic.Data = list;
                dic.TotalCount = count;
                return dic;


            }
        }
    }
}
