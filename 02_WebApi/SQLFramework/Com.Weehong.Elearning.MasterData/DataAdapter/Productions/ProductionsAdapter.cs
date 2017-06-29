using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Productions
{
    /// <summary>
    /// 作品文献，通过查询获取文献 例如：作者、主题等
    /// </summary>
    public class ProductionsAdapter : EffectedAdapterBase<Production, List<Production>>
    {
        public async Task<DicData> LoadProductionByMetaData(string columname, string[] metaDataValues, int pageSize, int curPage, bool isExact = true)
        {
            using (var db = new OperationManagerDbContext())
            {
                // db.User.GetScalar
                //根据元数据KEY获取CODE
                /*  SYS_MetaData sys_MetaData = db.SYS_MetaData.AsNoTracking().Where(w => w.MetaDataCode == metaDataCode).FirstOrDefault();
                 */

                //string sql1 = @"SELECT distinct ps.* FROM Productions ps left join 
                //           dbo.ProductionsField pf on ps.ProductionID = pf.ProductionID
                //          WHERE pf.ProductionID IN
                //           (SELECT DISTINCT ProductionID FROM dbo.ProductionsField WHERE MetaDataID = '" + sys_MetaData.MetaDataID + "' ";

                string sql = @"  SELECT * FROM StaticProductions WHERE 1=1 ";




                if (metaDataValues.Length > 1)
                {

                    foreach (var item in metaDataValues)
                    {
                        // inValues += "'" + item.ToString() + "',";
                        sql += " and " + columname + " like '%" + item + "%' ";
                    }

                }
                else
                {
                    if (!isExact)
                        sql += " and " + columname + " = '" + metaDataValues[0].ToString() + "' ";
                    else
                        sql += " and " + columname + " like '%" + metaDataValues[0].ToString() + "%' ";
                }

                //通过查询获取文献 例如：作者、主题等
                List<StaticProductions> list = db.Database.SqlQuery<StaticProductions>(sql).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                DicData dic = new DicData();
                dic.TotalCount = db.Database.SqlQuery<StaticProductions>(sql).Count();
                dic.Data = list;

                return dic;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="templeteid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Production> LoadProductionByTempleteIDAndUserID(Guid templeteid, Guid userid)
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.Production.AsNoTracking().OrderBy(o => o.CreateTime).Where(o => o.TemplateID == templeteid).ToList(); ;
            }
        }
        /// <summary>
        /// 通过查询获取文献 例如：作者、主题等
        /// </summary>
        /// <param name="templeteid">模板ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<List<StaticProductions>> LoadProductionByTempleteIDAndUserIDAsync(Guid templeteid)
        {
            using (var db = new OperationManagerDbContext())
            {
                //
                return await db.StaticProductions.AsNoTracking().Where(o => o.TemplateID == templeteid).ToListAsync();
            }
        }
        /// <summary>
        /// 研究单元-发表日期
        /// 获取所有年份下的所有文献数量
        /// </summary>
        /// <param name="userid">userid,登陆用户ID</param>
        /// <returns>所有年份，所有年份下的文献and数量</returns>
        public async Task<List<TypeNameCount>> LoadProductionYearListAndCountByUserID()
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"select year(issued) as year ,count(*) as count from   StaticProductions where issued is not null and issued <>'' group by year(issued) 	 order by year desc";

                List<TypeNameCount> dicList = new List<TypeNameCount>();
                var list = await db.Database.SqlQuery<YearAndCount>(sql).ToListAsync();
                foreach (var sale in list)
                {
                    TypeNameCount dic = new TypeNameCount();
                    dic.TypeName = sale.Year.ToString();
                    dic.TypeCount = sale.Count;
                    // dic.TypeID = "6A94CC2F-545D-4F80-8843-CA037B84C24F";
                    dic.TypeID = "issued";
                    if (sale.Year.ToString() == "1900")
                    {
                        dic.TypeName = "其它";
                    }
                    dicList.Add(dic);
                }
                return dicList;
            }
        }
        /// <summary>
        /// 研究单元-发表日期
        /// 获取所有年份下的所有文献数量
        /// </summary>
        /// <param name="userID">userID,用户ID</param>
        /// <returns>所有年份，所有年份下的文献and数量</returns>
        public async Task<List<TypeNameCount>> LoadProductionYearListAndCountByUserID(Guid userID)
        {
            using (var db = new OperationManagerDbContext())
            {
                //string sqlUser = @"SELECT UUID,REPLACE(SurnameChinese,' ','')+REPLACE(NameChinese,' ', '') AS UserName,
                //  REPLACE(SurnamePhoneticize,' ', '')+REPLACE(NamePhoneticize, ' ', '') AS EnglishName FROM dbo.[User] WHERE UUID='" + userID + "' ";
                //UserInfo userInfo = new UserInfo();
                //userInfo = await db.Database.SqlQuery<UserInfo>(sqlUser).FirstOrDefaultAsync();

                string sql = @"SELECT DISTINCT YEAR(sp.issued) AS year,count(*) as count FROM dbo.StaticProductions sp
WHERE CAST(YEAR(sp.issued)AS NVARCHAR) IS NOT NULL  ";
                //if(userInfo != null)//查询用户的 根据用户名称   英文名称 查询作者
                //{
                //    if(!string.IsNullOrEmpty(userInfo.EnglishName))
                //        sql += "WHERE  s.author LIKE '%"+ userInfo.UserName + "%' OR author LIKE '%"+ userInfo.EnglishName + "%' ";
                //    else
                //        sql += "WHERE  s.author LIKE '%"+ userInfo.UserName + "%' ";
                //}
               
                sql += " group by year(issued) 	 order by year desc";
                List<TypeNameCount> dicList = new List<TypeNameCount>();
                var list = await db.Database.SqlQuery<YearAndCount>(sql).ToListAsync();
                foreach (var sale in list)
                {
                    TypeNameCount dic = new TypeNameCount();
                    dic.TypeName = sale.Year.ToString();
                    dic.TypeCount = sale.Count;
                    // dic.TypeID = "6A94CC2F-545D-4F80-8843-CA037B84C24F";
                    dic.TypeID = "issued";
                    if (sale.Year.ToString() == "1900")
                    {
                        dic.TypeName = "其它";
                    }
                    dicList.Add(dic);
                }
                return dicList;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<List<TypeNameCount>> LoadProductionYearListAndCountByUserIDAsync(Guid userid)
        {
            using (var db = new OperationManagerDbContext())
            {

                string sql = @" select year(tt.issued) as year ,count(*) as count from StaticProductions tt
  group by year(tt.issued) ORDER BY year DESC";

                List<TypeNameCount> dicList = new List<TypeNameCount>();
                var list = await db.Database.SqlQuery<YearAndCount>(sql).ToListAsync();

                foreach (var sale in list)
                {
                    TypeNameCount dic = new TypeNameCount();
                    dic.TypeName = sale.Year.ToString();
                    dic.TypeCount = sale.Count;
                    // dic.TypeID = "6A94CC2F-545D-4F80-8843-CA037B84C24F";
                    dic.TypeID = "issued";
                    dicList.Add(dic);
                }
                return dicList;
            }
        }

        /// <summary>
        /// 获取最新的文献
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>=
        public List<ProductionOutPut> LoadProductionOrderByCraeattime(int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {
                //List<Production> list = await db.Production.AsNoTracking().OrderByDescending(o => o.CreateTime).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToListAsync();



                string sql = @"select sp.ProductionID,sp.TemplateID,p.DownloadNum,sp.CreateTime,p.BrowseNum,
p.CitationNum,st.ContentType as ContentType,sp.Title from Productions p inner join
 StaticProductions sp on p.ProductionID = sp.ProductionID 
 INNER JOIN dbo.SYS_Template st ON st.TemplateID=p.TemplateID
	  ORDER BY  sp.CreateTime  DESC ";

                List<ProductionOutPut> list = db.Database.SqlQuery<ProductionOutPut>(sql).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                //list = list.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();


                return list;
            }
        }


        /// <summary>
        /// 获取引用量最多的文献
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        public List<ProductionOutPut> LoadProductionOrderByCitationNum(int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {

                //List<Production> list = db.Production.AsNoTracking().OrderByDescending(o => o.CitationNum).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                //return list;

                string sql = @"select sp.ProductionID,sp.TemplateID,p.DownloadNum,sp.CreateTime,p.BrowseNum,p.CitationNum,st.ContentType as ContentType,sp.Title from Productions p inner join
                  StaticProductions sp on p.ProductionID = sp.ProductionID  
  INNER JOIN dbo.SYS_Template st ON st.TemplateID=p.TemplateID
  order by p.CitationNum desc";

                List<ProductionOutPut> list = db.Database.SqlQuery<ProductionOutPut>(sql).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();


                return list;
            }
        }

        /// <summary>
        /// 获取重要文献引用量总数
        /// 所有文献数量
        /// </summary>
        /// <returns></returns>
        public int LoadProductionCitationNumAll()
        {

            using (var db = new OperationManagerDbContext())
            {
                int num = 0;
                try
                {
                    // num = db.Production.AsNoTracking().Sum(o => (int)o.CitationNum);

                    string sql = @"select count(*) from StaticProductions ";
                    num = db.Database.SqlQuery<int>(sql).FirstOrDefault();

                }
                catch
                {


                }

                return num;
            }
        }



        /// <summary>
        /// 获取下载量最多的文献,根据下载量排序
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        public List<ProductionOutPut> LoadProductionOrderByDownloadNum(int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {

                //List<Production> list = db.Production.AsNoTracking().OrderByDescending(o => o.DownloadNum).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                //return list;

                //string sql = @"select p.ProductionID,p.TemplateID,p.DownloadNum,p.CreateTime,p.BrowseNum,p.CitationNum,p.DataType as ContentType,sp.Title from Productions p inner join
                //  StaticProductions sp on p.ProductionID=sp.ProductionID  order by p.DownloadNum desc";


                string sql = @" select sp.ProductionID,sp.TemplateID,p.DownloadNum,sp.CreateTime,p.BrowseNum,p.CitationNum,st.ContentType as ContentType,sp.Title from Productions p inner join
                  StaticProductions sp on p.ProductionID = sp.ProductionID  
    INNER JOIN dbo.SYS_Template st ON st.TemplateID=p.TemplateID
   order by p.DownloadNum desc";

                List<ProductionOutPut> list = db.Database.SqlQuery<ProductionOutPut>(sql).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();


                return list;
            }
        }


        /// <summary>
        /// 多条件查询文献
        /// </summary>
        /// <param name="diclist"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <param name="order"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public async Task<DicData> LoadProductionByCondition(List<Dictionary<string, string>> diclist,string hasattachment, int pageSize, int curPage, string order, string orderby)
        {

            DicData dicdata = new DicData();
            using (var db = new OperationManagerDbContext())
            {



                var q = diclist.GroupBy(x => x.Keys.FirstOrDefault()).Where(x => x.Count() >= 1).ToList();
                List<Dictionary<string, string>> liststr = new List<Dictionary<string, string>>();


                for (int i = 0; i < q.Count; i++)
                {
                    string key = q[i].Key;
                    string var = "";
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    for (int j = 0; j < diclist.Count; j++)
                    {

                        string value = diclist[j].FirstOrDefault().Value;
                        if (key == diclist[j].FirstOrDefault().Key)
                        {
                            var += value + ",";
                        }
                    }
                    dic.Add(key, var);
                    liststr.Add(dic);


                }


                string sql = @"SELECT sp.* FROM  StaticProductions  sp WHERE 1=1 ";

                string sqlcount = "SELECT count(-1) FROM  StaticProductions  sp WHERE 1=1 ";
                string where = "";
                string strorder = "";
                for (int i = 0; i < liststr.Count; i++)  //外循环是循环的次数
                {
                    string key = liststr[i].FirstOrDefault().Key;
                    string value = liststr[i].FirstOrDefault().Value;
                    string[] arrayvalue = value.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray();

                    if (arrayvalue.Length > 1)
                    {
                        where += " and";
                        for (int j = 0; j < arrayvalue.Length; j++)
                        {
                            if (j > 0)
                            {
                                where += " OR " + key + "  like '%" + arrayvalue[j] + "%'";
                            }
                            else
                            {
                                where += " ( " + key + "  like '%" + arrayvalue[j] + "%'";
                            }
                        }
                        where += " ) ";
                    }
                    else
                    {
                        where += " and " + key + " like '%" + arrayvalue[0] + "%'";
                    }
                    // string stror = "";

                    // where += stror;
                   

                }
                

                    if (hasattachment== "有")
                    {

                        where += " and ( sp.ProductionID   in( SELECT DISTINCT BusinessID FROM dbo.Comm_Attachment ))";
                    }
                    if (hasattachment == "无")
                    {
                        where += " and ( sp.ProductionID  not  in( SELECT DISTINCT BusinessID FROM dbo.Comm_Attachment ))";
                    }

                




                if (order.ToLower() == "citationnum")
                {
                    strorder = "  order by sp.citations " + orderby;

                }
                if (order.ToLower() == "createtime")
                {
                    strorder = "  order by sp.CreateTime " + orderby;

                }

                if (order.ToLower() == "title")
                {
                    strorder = "  order by sp.title " + orderby;

                }

                int count = db.Database.SqlQuery<int>(sqlcount + where).FirstOrDefault();

                List<StaticProductions> list = db.Database.SqlQuery<StaticProductions>(sql + where+ strorder).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();



                dicdata.Data = list;
                dicdata.TotalCount = count;
                return dicdata;
            }
        }


        /// <summary>
        ///获取所有文献附件数量
        /// </summary>
        /// <returns></returns>
        public int LoadProductionAllCount()
        {
            using (var db = new OperationManagerDbContext())
            {

                string sql = @"select count (distinct(p.ProductionID)) as count from Productions p inner join Comm_Attachment c on p.ProductionID=c.BusinessID ";

                var list = 0;
                list = db.Database.SqlQuery<int>(sql).ToList().FirstOrDefault();

                return list;
            }
        }

        /// <summary>
        /// 根据条件查询报表
        /// </summary>
        public void LoadReport(ReportCondition reportCondition)
        {

        }

        /// <summary>
        /// 获取作者的合作者
        /// </summary>
        /// <param name="author">作者名称</param>
        /// <returns></returns>
        public List<string> LoadAuthorList(string author, int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {

                string sql = @"SELECT DISTINCT(author) FROM dbo.StaticProductions WHERE author LIKE '%" + author + "%'";


                var list = db.Database.SqlQuery<string>(sql).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();


                return list;
            }
        }


        /// <summary>
        /// 按找院系一级菜单查询文献
        /// </summary>
        /// <param name="departid"></param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
       
        /// <returns></returns>
        public async Task<DicData> LoadProductionByDepartID(string departid, int pageSize, int curPage)
        {

            DicData dicdata = new DicData();
            using (var db = new OperationManagerDbContext())
            {
                string sql = @" SELECT * from dbo.StaticProductions sp WHERE sp.ProductionID IN(
SELECT RU.ProductionID FROM dbo.SYS_College sysCollege 
LEFT JOIN  dbo.[User] U ON  U.UnitID=sysCollege.CID 
LEFT JOIN dbo.Relation_UserClaimWorks RU ON RU.SysUserID=U.UUID 
WHERE  U.UnitID='"+ departid + "'AND RU.ProductionID IS NOT NULL AND RU.ProductionID<>''GROUP BY RU.ProductionID) ";

               
            

                int count = db.Database.SqlQuery<StaticProductions>(sql).Count();

                List<StaticProductions> list = db.Database.SqlQuery<StaticProductions>(sql).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();



                dicdata.Data = list;
                dicdata.TotalCount = count;
                return dicdata;
            }
        }


        /// <summary>
        /// 更新下载量
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public int AddProductsDownloadNum(Guid productid)
        {

            using (var db = new OperationManagerDbContext())
            {
                string sql = @"UPDATE dbo.Productions SET DownloadNum=DownloadNum+1 WHERE ProductionID='"+ productid + "'";

                int list = db.Database.ExecuteSqlCommand(sql);
               

                return list;
            }
        }

        /// <summary>
        /// 更新访问量
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public int AddProductsBrowseNum(Guid productid)
        {

            using (var db = new OperationManagerDbContext())
            {
                string sql = @"UPDATE dbo.Productions SET BrowseNum=BrowseNum+1 WHERE ProductionID='" + productid + "'";

                int list = db.Database.ExecuteSqlCommand(sql);


                return list;
            }
        }

    }

    /// <summary>
    /// 报表条件
    /// </summary>
    public class ReportCondition
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 收录类别
        /// </summary>
        public string[] IncludeType { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string[] Unit { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string[] Department { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public string[] Title { get; set; }

        /// <summary>
        /// 身份
        /// </summary>
        public string[] Identity { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string[] ContextType { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string[] Author { get; set; }

        /// <summary>
        /// 影响因子开始
        /// </summary>
        public string FactorInfluenceBegin { get; set; }

        /// <summary>
        /// 影响因子结束
        /// </summary>
        public string FactorInfluenceEnd { get; set; }

        /// <summary>
        /// 引用次数开始
        /// </summary>
        public int CitationsBegin { get; set; }

        /// <summary>
        /// 引用次数结束
        /// </summary>
        public int CitationsEnd { get; set; }

        /// <summary>
        /// SCI分区
        /// </summary>
        public string SCIPartition { get; set; }

        /// <summary>
        /// 中科院分区
        /// </summary>
        public string ZKYPartition { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 作者姓名
        /// </summary>
        public string AuthorName { get; set; }

    }

    public class YearAndCount
    {
        public int Year { get; set; }

        public int Count { get; set; }

    }
}