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
    /// 职称类型表
    /// </summary>
    public class SYS_PositionalTitleTypeAdapter: EffectedAdapterBase<SYS_PositionalTitleType,List<SYS_PositionalTitleType>>
    {
        public static readonly SYS_PositionalTitleTypeAdapter Instance = new SYS_PositionalTitleTypeAdapter();

        //获取所有的职称
        public async Task< List<SYS_PositionalTitleType>> getAllList() {
            using (var db = new OperationManagerDbContext())
            {
                List<SYS_PositionalTitleType> list = await db.SYSPositionalTitleType.Where(w => w.DelFlag == 1).ToListAsync();

                return list;
            }
               

        }
    }
}
