﻿using System;
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
    /// 关注的主题
    /// </summary>
    public class RelationUserAttentionThemeAdapter : EffectedAdapterBase<RelationUserAttentionThemeModel, List<RelationUserAttentionThemeModel>>
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
                List<RelationUserAttentionThemeModel> lis = await db.RelationUserAttentionTheme.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).ToListAsync();
               
                List<RelationUserAttentionThemeModel> list =lis.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

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
                return db.RelationUserAttentionTheme.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).ToList().Count;
            }
        }

        //根据用户id 主题id 获取关注主题信息
        public async Task<RelationUserAttentionThemeModel> LoadBySysUserIDAndThemID(Guid sysuserID,Guid themid)
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.RelationUserAttentionTheme.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID&& w.ThemeID==themid).ToList().FirstOrDefault();
            }
        }
    }
}
