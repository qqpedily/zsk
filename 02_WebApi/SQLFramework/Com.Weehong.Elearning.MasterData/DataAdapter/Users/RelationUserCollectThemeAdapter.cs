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
    /// 主题
    /// </summary>
    public class RelationUserCollectThemeAdapter : EffectedAdapterBase<RelationUserCollectThemeModel, List<RelationUserCollectThemeModel>>
    {
        /// <summary>
        /// 根据用户ID查询主题集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>
        public async Task<DicData> LoadBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {
                DicData dic = new DicData();
                List<RelationUserCollectThemeModel> lis= await  db.RelationUserCollectTheme.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToListAsync();

                List<RelationUserCollectThemeModel> list= lis.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                int count = lis.Count;
                dic.Data = list;
                dic.TotalCount = count;
                return dic;

            }
        }

        public async Task<int> LoadBySysUserIDCount(Guid sysuserID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.RelationUserCollectTheme.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).ToList().Count;
            }
        }

        public async Task<RelationUserCollectThemeModel> LoadBySysUserIDAndThemeID(Guid sysuserID, Guid themeID)
        {
            using (var db = new OperationManagerDbContext())
            {

                return db.RelationUserCollectTheme.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID && w.ThemeID == themeID).ToList().FirstOrDefault();
            }
        }

    }
}
