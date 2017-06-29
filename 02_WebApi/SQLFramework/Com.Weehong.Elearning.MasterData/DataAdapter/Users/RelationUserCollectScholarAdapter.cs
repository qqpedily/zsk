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
    /// 学者
    /// </summary>
    public class RelationUserCollectScholarAdapter : EffectedAdapterBase<RelationUserCollectScholarModel, List<RelationUserCollectScholarModel>>
    {

        /// <summary>
        /// 根据用户ID查询收藏学者集合
        /// </summary>
        /// <param name="sysuserID"></param>
        /// <returns></returns>
        public async Task<DicData> LoadBySysUserID(Guid sysuserID, int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {
                DicData dic = new DicData();
                int lis = db.RelationUserCollectScholar.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).Count();
                List<RelationUserCollectScholarModel> list = await db.RelationUserCollectScholar.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToListAsync();
                //int count = lis;
                dic.Data = list;
                dic.TotalCount = lis;
                return dic;
            }
        }
        public int LoadBySysUserIDCount(Guid sysuserID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.RelationUserCollectScholar.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID).Count();
            }
        }

        //查询学者的作品数
        public int LoadProductsCountCountByXuezheID(Guid sysuserID, string zuozhe)
        {
            using (var db = new OperationManagerDbContext())
            {
                //return db.ProductionsField.AsNoTracking().OrderBy(c => c.FieldSequence).Where(w => w.DefaultText.Contains(zuozhe) && w.MetaDataID.ToString() == "50883877-E367-4D5B-85FD-5F15A5B2E789").Count();

                string sql = @"  SELECT COUNT(*) FROM dbo.StaticProductions WHERE author LIKE '%"+ zuozhe + "%'";
                int count = 0;
                count=db.Database.SqlQuery<int>(sql).FirstOrDefault();
                return count;
            }
        }

        public async Task<RelationUserCollectScholarModel> LoadBySysUserIDAndScholarID(Guid sysuserID, Guid scholarID)
        {
            using (var db = new OperationManagerDbContext())
            {

                return await db.RelationUserCollectScholar.AsNoTracking().OrderBy(c => c.CreateTime).Where(w => w.SysUserID == sysuserID && w.ScholarID == scholarID).FirstOrDefaultAsync();
            }
        }
        /// <summary>
        /// 删除 取消用户收藏学者
        /// </summary>
        /// <param name="SysUserID">用户ID</param>
        /// <param name="ScholarID">被收藏学者的ID</param>
        /// <returns></returns>
        public bool DelRelationUserCollectScholar(Guid SysUserID,Guid ScholarID)
        {
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                try
                {
                    int i = db.Database.ExecuteSqlCommand("DELETE FROM Relation_UserCollectScholar WHERE SysUserID='"+ SysUserID + "' AND ScholarID='"+ ScholarID + "' ");
                    if (i > 0)
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return false;
        }





    }
}
