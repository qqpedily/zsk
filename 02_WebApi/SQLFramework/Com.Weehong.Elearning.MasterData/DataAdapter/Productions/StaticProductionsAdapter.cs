using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter
{
    /// <summary>
    /// 作品文献静态表
    /// </summary>
    public class StaticProductionsAdapter : EffectedAdapterBase<StaticProductions, List<StaticProductions>>
    {
        /// <summary>
        /// 根据文献ID查询大表数据
        /// </summary>
        /// <param name="ProductionID"></param>
        /// <returns></returns>
        public StaticProductions GetProductionsByID(Guid ProductionID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.StaticProductions.Where(w => w.ProductionID == ProductionID).FirstOrDefault();
            }
        }

        /// <summary>
        /// 根据UserID查询大表数据
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<StaticProductions>> GetByUserID(Guid userID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.StaticProductions.Where(w => w.UserID == userID).ToListAsync();
            }
        }

    }
}