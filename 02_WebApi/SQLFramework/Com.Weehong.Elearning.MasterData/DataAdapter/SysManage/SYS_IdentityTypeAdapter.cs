using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.SysManage
{
    /// <summary>
    /// 身份类型表
    /// </summary>
    public class SYS_IdentityTypeAdapter : EffectedAdapterBase<SYS_IdentityType, List<SYS_IdentityType>>
    {
        public static readonly SYS_IdentityTypeAdapter Instance = new SYS_IdentityTypeAdapter();


        /// <summary>
        /// 获取身份信息列表
        /// 【by ZHL】
        /// </summary>
        /// <returns></returns>
        public Task<List<SYS_IdentityType>> LoadByIdentityTypeDataList()
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.SYS_IdentityType.OrderBy(c => c.Sort).Where(w => w.DelFlag == 1).ToListAsync();
            }
        }
        /// <summary>
        /// 获取身份信息列表
        /// 【by ZHL】
        /// </summary>
        /// <returns></returns>
        public async Task<List<SYS_IdentityType>> LoadByIdentityTypeData()
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.SYS_IdentityType.OrderBy(c => c.Sort).Where(w => w.DelFlag == 1).ToListAsync();
            }
        }

    }
}
