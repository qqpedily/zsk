using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate
{
    /// <summary>
    /// 元素数据管理
    /// </summary>
    public class SYS_TemplateFieldAdapter : EffectedAdapterBase<SYS_TemplateField, List<SYS_TemplateField>>
    {
        /// <summary>
        /// 获取模板数据集合
        /// </summary>
        /// <returns></returns>
        public List<SYS_TemplateField> GetSYS_TemplateFieldList()
        {
            using (var db = new OperationManagerDbContext())
            {
                return base.GetAll().ToList();
            }
        }

        /// <summary>
        /// 获取单个模板
        /// </summary>
        /// <param name="templateID">模板ID</param>
        /// <returns></returns>
        public List<SYS_TemplateField> GetSYS_TemplateField(Guid? templateID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return base.GetAll().Where(w => w.TemplateID == templateID).ToList();
            }
        }

        /// <summary>
        /// 获取单个模板内的属性
        /// </summary>
        /// <param name="metaDataID">属性ID</param>
        /// <returns></returns>
        public SYS_TemplateField GetSYS_TemplateFieldByMetaDataID(string metaDataID)
        {
            using (var db = new OperationManagerDbContext())
            {
                var info = db.SYS_TemplateField.AsNoTracking().Where(w => w.MetaDataID.ToString() == metaDataID).FirstOrDefault();
                return info;
            }
        }

        /// <summary>
        /// 获取单个模板内的属性
        /// </summary>
        /// <param name="metaDataID">属性ID</param>
        /// <returns></returns>
        public async Task<SYS_TemplateField> GetByMetaDataIDAsync(string metaDataID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.SYS_TemplateField.AsNoTracking().Where(w => w.MetaDataID.ToString() == metaDataID).FirstOrDefaultAsync();
            }
        }


        /// <summary>
        /// 研究单元-内容类型
        ///获取所有模板的内容类型,该内容类型下的所有文献数量
        /// </summary>
        /// <param name="userID">userID,登陆用户ID</param>
        /// <returns>所有模板内容类型和该内容类型下的所有文献and数量</returns>

        public async Task<List<TypeNameCount>> GetSYS_TemplateContentTypeListAndCount()
        {
            using (var db = new OperationManagerDbContext())
            {
                SYS_TemplateAdapter sys_TemplateAdapter = new SYS_TemplateAdapter();
                List<TypeNameCount> dicList = new List<TypeNameCount>();
                ProductionsAdapter prad = new ProductionsAdapter();
                List<SYS_Template> list = await db.SYS_Template.ToListAsync();
                int lic;
                foreach (var sale in list)
                {
                    if (sale.TemplateID.ToString().Length > 0)
                    {
                        TypeNameCount dic = new TypeNameCount();

                        lic = db.StaticProductions.Where(w => w.TemplateID == sale.TemplateID).Count();
                        // dic.TypeID = sale.TemplateID.ToString();
                        dic.TypeID = "TemplateID";
                        dic.TypeValue = sale.TemplateID.ToString();
                        dic.TypeName = sale.ContentType;
                        dic.TypeCount = lic;
                        //  dic.Add("ContentTypeList", lic);
                        dicList.Add(dic);
                    }
                }

                return dicList;
            }
        }


        /// <summary>
        /// 研究单元-内容类型
        ///获取所有模板的内容类型,该内容类型下的所有文献数量
        /// </summary>
        /// <param name="userID">userID,登陆用户ID</param>
        /// <returns>所有模板内容类型和该内容类型下的所有文献and数量</returns>

        public async Task<List<TypeNameCount>> GetSYS_TemplateContentTypeListAndCountAsync(Guid userID)
        {

            using (var db = new OperationManagerDbContext())
            {
                SYS_TemplateAdapter sys_TemplateAdapter = new SYS_TemplateAdapter();
                List<SYS_Template> list = await db.SYS_Template.AsNoTracking().ToListAsync();
                List<TypeNameCount> dicList = new List<TypeNameCount>();
                ProductionsAdapter prad = new ProductionsAdapter();
                int lic = 0;
                foreach (var sale in list)
                {
                    TypeNameCount dic = new TypeNameCount();

                    lic = prad.LoadProductionByTempleteIDAndUserID(sale.TemplateID, userID).Count;

                    //  dic.TypeID = sale.TemplateID.ToString();

                    dic.TypeID = "TemplateID";
                    dic.TypeName = sale.ContentType;
                    dic.TypeCount = lic;

                    dicList.Add(dic);
                }

                return dicList;

            }



        }

        /// <summary>
        /// 研究单元-发表日期
        ///获取所有年份下的所有文献数量
        /// </summary>
        /// <returns>所有年份，所有年份下的文献and数量</returns>

        public async Task<List<TypeNameCount>> GetSYS_YearListAndCount()
        {
            ProductionsAdapter productionsAdapter = new ProductionsAdapter();


            return await productionsAdapter.LoadProductionYearListAndCountByUserID();
        }
        /// <summary>
        /// 研究单元-发表日期
        /// 获取所有年份下的所有文献数量
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public async Task<List<TypeNameCount>> GetSYS_YearListAndCount(Guid userID)
        {
            ProductionsAdapter productionsAdapter = new ProductionsAdapter();

            return await productionsAdapter.LoadProductionYearListAndCountByUserID(userID);
        }
        /// <summary>
        /// 研究单元-发表日期
        ///获取所有年份下的所有文献数量
        /// </summary>
        /// <param name="userID">userID,登陆用户ID</param>
        /// <returns>所有年份，所有年份下的文献and数量</returns>

        public async Task<List<TypeNameCount>> GetSYS_YearListAndCountAsync(Guid userID)
        {
            ProductionsAdapter productionsAdapter = new ProductionsAdapter();

            return await productionsAdapter.LoadProductionYearListAndCountByUserID();
        }
        /// <summary>
        /// 研究单元-语言
        ///获取所有语言下的所有文献数量
        /// </summary>
        /// <returns>所有语言，所有语言下的文献数量</returns>

        public async Task<List<TypeNameCount>> GetSYS_LanguageListAndCount()
        {
            SYS_TemplateFieldAdapter sys_TemplateFieldModelAdapter = new SYS_TemplateFieldAdapter();
            ProductionsFieldAdapter productionsFieldAdapter = new ProductionsFieldAdapter();
           

            string sql = @"SELECT DISTINCT Iso FROM dbo.StaticProductions WHERE iso IS NOT NULL AND iso<>''";
            using (var db = new OperationManagerDbContext())
            {
                List<string> strlist = db.Database.SqlQuery<string>(sql).ToList();

                
                List<TypeNameCount> dicList = new List<TypeNameCount>();
              

                int lif = 0;
                for (int i = 0; i < strlist.Count; i++)
                {
                    lif = productionsFieldAdapter.LoadByDefaultText("iso", strlist[i]);
                    TypeNameCount dic = new TypeNameCount();
                    // dic.TypeID = "8BC5C3A3-6565-45F8-B77A-891077D4E2A5";
                    dic.TypeID = "iso";
                    dic.TypeName = strlist[i];
                    dic.TypeCount = lif;

                    dicList.Add(dic);
                }
                return dicList;
            }
        }
        /// <summary>
        /// 研究单元-收录类别
        ///获取所有收录类别下的所有文献数量
        /// </summary>
        /// <returns>所有收录类别，收录类别下的文献数量</returns>

        public  List<TypeNameCount> GetSYS_IndexedTypeListAndCount()
        {
          
            ProductionsFieldAdapter productionsFieldAdapter = new ProductionsFieldAdapter();
           

            string sql = @"SELECT DISTINCT indexed FROM dbo.StaticProductions WHERE indexed IS NOT NULL AND indexed<>''";
            List<TypeNameCount> dicList = new List<TypeNameCount>();
            using (var db = new OperationManagerDbContext())
            {
                List<string> strlist = db.Database.SqlQuery<string>(sql).ToList();

                int lif = 0;
                for (int i = 0; i < strlist.Count; i++)
                {
                    string indexsql = @"SELECT COUNT(*) FROM dbo.StaticProductions WHERE indexed='"+ strlist[i] + "'";
                    lif = db.Database.SqlQuery<int>(indexsql).FirstOrDefault();
                    TypeNameCount typeNameCount = new TypeNameCount();
                    

                    typeNameCount.TypeID = "indexed";//收录类别
                    typeNameCount.TypeName = strlist[i];
                    typeNameCount.TypeCount = lif;

                    dicList.Add(typeNameCount);
                }
            }

            return dicList;
        }



        /// <summary>
        /// 研究单元-是否有全文
        ///获取所有收录类别下的是否有全文
        /// </summary>
        /// <returns>所有收录类别，是否有全文</returns>

        public List<TypeNameCount> GetSYS_AttachmentListAndCount()
        {

          
            var hasnum = 0;
            var nonum = 0;
            string sqlhave = @"SELECT COUNT(*)  FROM  dbo.StaticProductions WHERE ProductionID IN (SELECT BusinessID FROM dbo.Comm_Attachment) ";

            string sqlnot = @"SELECT COUNT(*)  FROM  dbo.StaticProductions WHERE ProductionID not IN (SELECT BusinessID FROM dbo.Comm_Attachment) ";
            using (var db = new OperationManagerDbContext())
            {
                hasnum = db.Database.SqlQuery<int>(sqlhave).FirstOrDefault();
                nonum = db.Database.SqlQuery<int>(sqlnot).FirstOrDefault();
            }


          

            List<TypeNameCount> dicList = new List<TypeNameCount>();

            TypeNameCount dic = new TypeNameCount();
            dic.TypeID = "CommAttachmentTypeID";
            dic.TypeName = "有";
            dic.TypeCount = hasnum;
            TypeNameCount dic1 = new TypeNameCount();
            dic1.TypeID = "CommAttachmentTypeID";
            dic1.TypeName = "无";
            dic1.TypeCount = nonum;
            dicList.Add(dic);
            dicList.Add(dic1);
            return dicList;
        }
    }
}
