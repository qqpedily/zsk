using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Productions
{
    /// <summary>
    /// 作品文献字段
    /// </summary>
    public class ProductionsFieldAdapter : EffectedAdapterBase<ProductionsField, List<ProductionsField>>
    {
        private readonly SYS_TemplateFieldAdapter sys_TemplateFieldAdapter = new SYS_TemplateFieldAdapter();
        public static readonly ProductionsFieldAdapter Instance = new ProductionsFieldAdapter();
        /// <summary>
        /// 查询所有文献
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductionsField>> LoadAllProductions()
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.ProductionsField.AsNoTracking().OrderBy(p => p.FieldSequence).ToListAsync();
            }
        }

        /// <summary>
        /// 查询所有文献
        /// </summary>
        /// <returns></returns>
        public async Task<ProductionsFieldTotal> LoadAllProductions(int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {
                ProductionsFieldTotal result = new ProductionsFieldTotal();
                result.TotalCount = db.ProductionsField.Count();
                result.ProductionsField = await db.ProductionsField.AsNoTracking().OrderBy(c => c.MetaDataID).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToListAsync();
                return result;
            }
        }

        /// <summary>
        /// 根据ProductionID，TemplateID查询文献信息
        /// </summary>
        /// <param name="productionID"></param>
        /// <returns></returns>
        public List<ProductionsField> LoadByProductionIDAndTemplateID(Guid productionID)
        {
            using (var db = new OperationManagerDbContext())
            {
                var list = db.ProductionsField.AsNoTracking().OrderBy(p => p.FieldSequence).Where(p => p.ProductionID == productionID).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据ProductionID，TemplateID查询文献信息
        /// </summary>
        /// <param name="productionID"></param>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public List<ProductionsField> LoadByProductionIDAndTemplateID(Guid productionID, Guid? templateID)
        {
            using (var db = new OperationManagerDbContext())
            {
                var list = db.ProductionsField.AsNoTracking().OrderBy(p => p.FieldSequence).Where(p => p.ProductionID == productionID && p.TemplateID == templateID).ToList();
                return list;
            }
        }

        /// <summary>
        /// 查询该语言有多少文献old
        /// </summary>
        /// <param name="defaulttext"></param>
        /// <returns></returns>

        public int LoadByDefaultText(string metadataid, string defaulttext)
        {
            using (var db = new OperationManagerDbContext())
            {
                //var list = db.ProductionsField.AsNoTracking().OrderBy(p => p.FieldSequence).Where(p => p.DefaultText.Contains(defaulttext) && p.MetaDataID.ToString() == metadataid).ToList();

                string sql = @" SELECT count (*) FROM  StaticProductions WHERE "+ metadataid + " LIKE'%"+ defaulttext + "%'";
                return db.Database.SqlQuery<int>(sql).FirstOrDefault();
            }
        }
       
        /// <summary>
        /// 查询该语言有多少文献
        /// </summary>
        /// <param name="defaulttext"></param>
        /// <returns></returns>
        public async Task<List<StaticProductions>> LoadByDefaultTextAsync(string metadataid, string defaulttext)
        {
            using (var db = new OperationManagerDbContext())
            {
                //return await db.ProductionsField.AsNoTracking().OrderBy(p => p.FieldSequence).Where(p => p.DefaultText.Contains(defaulttext) && p.MetaDataID.ToString() == metadataid).ToListAsync();

                string sql = @" SELECT * FROM  StaticProductions WHERE " + metadataid + " LIKE'%" + defaulttext + "%'";
                var list = db.Database.SqlQuery<StaticProductions>(sql).ToList();
                return list;
            }
        }
        /// <summary>
        /// 按照时间，userid，查询该语言有多少文献
        /// </summary>
        /// <param name="metadataid"></param>
        /// <param name="defaulttext"></param>
        /// <param name="startime"></param>
        /// <param name="endtime"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<StaticProductions>> LoadContentListByTimeAndUserID(Guid metadataid, string startime, string endtime)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"select  p.* from StaticProductions p 
	                    where p.TemplateID like '%" + metadataid + "%' ";
                sql += " and (p.CreateTime between '" + startime + "' and '" + endtime + "') ";
                //if (!string.IsNullOrEmpty(userid))
                //{
                //    sql += " and p.UserID= '" + userid + "'";

                //}

                return await db.Database.SqlQuery<StaticProductions>(sql).ToListAsync();
            }
        }

        public async Task<List<Production>> LoadByDefaultTextByTimeAndUserID(Guid metadataid, string defaulttext, string startime, string endtime)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"select  p.* from StaticProductions p 
	                    where p.iso like '%" + defaulttext + "%' ";
                sql += " and (p.CreateTime between '" + startime + "' and '" + endtime + "') ";
                //if (!string.IsNullOrEmpty(userid))
                //{
                //    sql += " and p.UserID= '" + userid + "'";

                //}

                return await db.Database.SqlQuery<Production>(sql).ToListAsync();
            }
        }


        /// <summary>
        /// 根据MetaDataID 查询 SYS_MetaData信息
        /// </summary>
        /// <param name="metadataid"></param>
        /// <returns></returns>
        public SYS_MetaData LoadSYS_MetaDataByID(Guid? metadataid)
        {
            using (var db = new OperationManagerDbContext())
            {
                var list = db.SYS_MetaData.AsNoTracking().OrderBy(p => p.Number).Where(p => p.MetaDataID == metadataid).ToList().FirstOrDefault();
                return list;
            }
        }
    }
}
