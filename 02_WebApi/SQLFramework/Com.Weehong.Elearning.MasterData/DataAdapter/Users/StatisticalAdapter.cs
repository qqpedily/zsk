using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using System.Globalization;
using Pinyin4net;
using Com.Weehong.Elearning.MasterData.Repositories;
using Com.Weehong.Elearning.MasterData.DataModels.ReportForm;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;
using Com.Weehong.Elearning.MasterData.DataModels;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 文献统计 
    /// </summary>
    public class StatisticalAdapter : EffectedAdapterBase<UserAliasModel, List<UserAliasModel>>
    {
        /// <summary>
        ///  年度成果院所排名---//--年度成果科系排名
        /// </summary>
        /// <param name="collegetype"></param>
        /// <returns></returns>
        public List<OutPutReport> GetStatisticalByStarYearAndEndYear(string collegetype, string top, int pageSize, int curPage)
        {
            using (var db = new OperationManagerDbContext())
            {

                string sqlstr = @"SELECT DISTINCT CAST(YEAR(issued)AS NVARCHAR)  AS year FROM dbo.StaticProductions  ORDER BY year desc";


                List<string> list = db.Database.SqlQuery<string>(sqlstr).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                List<OutPutReport> list_outputReport = new List<OutPutReport>();
                // Dictionary<string, List<SysCollegeResult>> diclist = new Dictionary<string, List<SysCollegeResult>>();
                for (int i = 0; i < list.Count; i++)
                {
                    OutPutReport outPutReport = new OutPutReport();
                    outPutReport.Year = Convert.ToInt32(list[i]);
                    string sql = @"SELECT top " + top + " sc.CollegeName as Year,cast(year(sp.issued) as varchar) as Resulttype,COUNT(-1) AS count FROM dbo.SYS_College sc LEFT join  dbo.StaticProductions sp ON sc.CID=sp.jigouyuanxiCode  WHERE sc.CollegeType=" + collegetype + " and  year(sp.issued) like '%" + list[i] + "%'  GROUP BY  year(sp.issued),sc.CollegeName  ORDER BY  count DESC";
                    List<SysCollegeResult> lis = db.Database.SqlQuery<SysCollegeResult>(sql).ToList();
                    if (lis.Count > 0)
                    {
                        outPutReport.CollegeResult = lis;
                        //  listAll.Add(lis);
                        //diclist.Add(list[i], lis);
                    }
                    list_outputReport.Add(outPutReport);
                }


                return list_outputReport;
            }
        }










        /// <summary>
        /// 年度论文院所收录情况排名---/年度论文科系收录情况排名
        /// </summary>
        /// <param name="collegetype">院系类型 1 院系，2科系</param>
        /// <returns></returns>
        public List<YearAndCountOutPut> GetTongJiByNDChengGuoYuanSuoPaiMing(string collegetype, string top, int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {
                string sqlstr = @"SELECT DISTINCT CAST(YEAR(issued)AS NVARCHAR)  AS year FROM dbo.StaticProductions  ORDER BY year desc";


                List<string> list = db.Database.SqlQuery<string>(sqlstr).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                List<YearAndCountOutPut> list_outputReport = new List<YearAndCountOutPut>();
               // Dictionary<string, List<SysCollegeResult>> diclist = new Dictionary<string, List<SysCollegeResult>>();
                for (int i = 0; i < list.Count; i++)
                {
                    YearAndCountOutPut report = new YearAndCountOutPut();
             
                    string sql = @" SELECT TOP " + top + " sc.CID,sc.CollegeName  as Year,COUNT(*) AS count FROM dbo.SYS_College sc   LEFT JOIN dbo.[User] u ON (u.UnitID=sc.CID OR u.DeptID=sc.CID) LEFT JOIN dbo.Relation_UserClaimWorks ruw ON ruw.SysUserID = u.UUID  LEFT join  dbo.StaticProductions sp ON ruw.ProductionID = sp.ProductionID  where sc.CollegeType=" + collegetype + " and year(sp.issued) like '%" + list[i] + "%' GROUP BY  sc.CID,sc.CollegeName  ORDER BY sc.CID ASC , count DESC";
                    List<YearAndCount> lis = db.Database.SqlQuery<YearAndCount>(sql).ToList();
                    report.Year = list[i];
                    if (lis.Count > 0)
                    {
                        report.CollegeResult = lis;
                         //diclist.Add(list[i], lis);
                    }
                    list_outputReport.Add(report);

                }


                return list_outputReport;
            }
        }


        /// <summary>
        /// 年度成果类型分布
        /// </summary>
        /// <returns></returns>
        public List<YearAndCountOutPut> GetStatisticalByNianDuChengGuoLeiXingFenBu(string staryear, string endyear)
        {
            using (var db = new OperationManagerDbContext())
            {
                string strsql = @" SELECT DISTINCT CAST(YEAR(issued)AS NVARCHAR) AS year FROM dbo.StaticProductions WHERE YEAR(issued) BETWEEN '" + staryear + "' AND '" + endyear + "'  ORDER BY year desc";

                List<string> list = db.Database.SqlQuery<string>(strsql).ToList();
                //Dictionary<string, List<YearAndCount>> diclist = new Dictionary<string, List<YearAndCount>>();

                List<YearAndCountOutPut> output = new List<YearAndCountOutPut>();
                for (int i = 0; i < list.Count; i++)
                {

                    YearAndCountOutPut op = new YearAndCountOutPut();
                    string sql = @" select resulttype,CAST(YEAR(issued)AS NVARCHAR) as year ,count(*) as count from StaticProductions sp
where  year(sp.issued) like '%" + list[i] + "%'  group by resulttype,year(sp.issued)";

                    List<YearAndCount> listm = db.Database.SqlQuery<YearAndCount>(sql).ToList();
                    op.Year = list[i];
                    op.CollegeResult = listm;
                    output.Add(op);
                   // diclist.Add(list[i], listm);

                }

                return output;
            }
        }



        /// <summary>
        /// 年度论文收录情况统计
        /// </summary>
        /// <returns></returns>
        public List<YearAndCountOutPut> GetTongJiByNDLunWenShoulu(string departid, string staryear, string endyear, string indexed)
        {


            using (var db = new OperationManagerDbContext())
            {
                string strsql = @" SELECT DISTINCT CAST(YEAR(issued)AS NVARCHAR) AS year FROM dbo.StaticProductions WHERE YEAR(issued) BETWEEN '" + staryear + "' AND '" + endyear + "'  ORDER BY year desc";

                List<string> list = db.Database.SqlQuery<string>(strsql).ToList();
               // Dictionary<string, List<YearAndCount>> diclist = new Dictionary<string, List<YearAndCount>>();

                List<YearAndCountOutPut> output = new List<YearAndCountOutPut>();
                for (int i = 0; i < list.Count; i++)
                {
                    YearAndCountOutPut op = new YearAndCountOutPut();
                    string sql = @" SELECT tt.indexed AS resulttype, CAST(YEAR(issued)AS NVARCHAR) AS year ,COUNT(-1) AS count FROM StaticProductions tt 
WHERE  YEAR(tt.issued) like '%" + list[i] + "%' AND tt.indexed LIKE'%" + indexed + "%' ";
                    if (!string.IsNullOrEmpty(departid))
                    {
                      //  sql += " AND tt.jigouyuanxiCode='" + departid + "'";
                    }

                    sql += " group by year(tt.issued),tt.indexed ORDER BY year DESC ";

                    var listm = db.Database.SqlQuery<YearAndCount>(sql).ToList(); ;
                  //  diclist.Add(list[i], listm);
                    op.Year = list[i];
                    op.CollegeResult = listm;
                    output.Add(op);
                }
                return output;
            }


        }






        /// <summary>
        /// 作者发文量TOP100
        /// </summary>
        /// <returns></returns>
        public List<YearAndCountOutPut> GetStatisticalByZuoZheTop100(string staryear, string endyear)
        {

            using (var db = new OperationManagerDbContext())
            {


                string sql = @" select top 100 us.SurnameChinese+us.NameChinese as year,count(*)as count from Relation_UserClaimWorks u 
 left join StaticProductions sp on u.ProductionID=sp.ProductionID
 left join [User] us on us.uuid=u.SysUserID
 where UserClaimWorksStatus=2
 and year(sp.issued) between '" + staryear + "' and '" + endyear + "'group by us.SurnameChinese+us.NameChinese order by count desc";


                List<YearAndCount> list = db.Database.SqlQuery<YearAndCount>(sql).ToList();
                List<YearAndCountOutPut> output = new List<YearAndCountOutPut>();
                for (int i = 0; i < list.Count; i++)
                {
                    YearAndCountOutPut op = new YearAndCountOutPut();
                    op.Year = list[i].Year;
                    YearAndCount ya = list[i];
                    op.CollegeResult = new List<YearAndCount>();

                    op.CollegeResult.Add(ya);

                    output.Add(op);
                }


                return output;
            }
        }

        /// <summary>
        /// 作者SCI发文量TOP100/作者CSCD发文量TOP100
        /// </summary>
        /// <returns></returns>
        public List<YearAndCountOutPut> GetStatisticalByZuoZheSCIOrCSCDTop100(string staryear, string endyear, string sciorcscd)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @" select top 100 us.SurnameChinese+us.NameChinese as year,count(*)as count from Relation_UserClaimWorks u 
 left join StaticProductions sp on u.ProductionID=sp.ProductionID
 left join [User] us on us.uuid=u.SysUserID
 where UserClaimWorksStatus=2
 and year(sp.issued)  between '" + staryear + "' and '" + endyear + "' and sp.dataset like '%" + sciorcscd + "%' group by us.SurnameChinese+us.NameChinese order by count desc";
                List<YearAndCount> list = db.Database.SqlQuery<YearAndCount>(sql).ToList();

                List<YearAndCountOutPut> output = new List<YearAndCountOutPut>();
                for (int i = 0; i < list.Count; i++)
                {
                    YearAndCountOutPut op = new YearAndCountOutPut();
                    op.Year = list[i].Year;
                    YearAndCount ya = list[i];
                    op.CollegeResult = new List<YearAndCount>();
                    op.CollegeResult.Add(ya);

                    output.Add(op);
                }
                return output;
            }
        }

        /// <summary>
        /// 职工职称 统计
        /// 统计每个职称有多少职工
        /// </summary>
        /// <returns></returns>
        public List<YearAndCount> GetTongJiZhiGongZhiCheng()
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @" select spt.PostTitleTypeName as Year,COUNT(*) as count from SYS_PositionalTitleType spt left join UserInsidePrincipalOffice uo
on spt.PttID=uo.PttID  group by uo.PttID,spt.PostTitleTypeName";
//                sql = @"SELECT t.PostTitleTypeName AS Year,t.Num AS count FROM (
//SELECT PostTitleTypeName,COUNT(-1) AS Num FROM dbo.SYS_PositionalTitleType AS spt LEFT JOIN dbo.[User] AS u ON
//u.PttID = spt.PttID GROUP BY spt.PostTitleTypeName 
//)t ORDER BY t.Num DESC  ";
                List<YearAndCount> list = db.Database.SqlQuery<YearAndCount>(sql).ToList();

                return list;
            }
        }

        /// <summary>
        /// 年度成果类型分布
        /// </summary>
        /// <returns></returns>
        public List<AchievementsType> GetAchievementsType()
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT count(-1) as value,(CASE when (resulttype is null or resulttype='') then '其它' else resulttype end) as name
                               from StaticProductions  group by resulttype ";
                List<AchievementsType> list = db.Database.SqlQuery<AchievementsType>(sql).ToList();
                return list;
            }
        }

        #region 自选统计


        /// <summary>
        /// 收录类别
        /// </summary>
        /// <returns></returns>
        public List<YearAndCount> GetIndexedList()
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT  DefaultText FROM dbo.SYS_TemplateField WHERE MetaDataID='E134A22A-4187-4318-BB70-BCC66711ED1D'";
                string list = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                string[] str = list.Split(';');
                List<YearAndCount> liststr = new List<YearAndCount>();
                for (int i = 0; i < str.Length; i++)
                {
                    YearAndCount ya = new YearAndCount();
                    ya.ResultType = "indexed";
                    ya.Year = str[i];
                    liststr.Add(ya);
                }
                return liststr;
            }
        }

        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        public List<SysCollegeResult> GetDepartList()
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT 'jigouyuanxiCode' AS Resulttype, CID,CollegeName  as Year FROM dbo.SYS_College  WHERE CollegeType=1 ";
                List<SysCollegeResult> list = db.Database.SqlQuery<SysCollegeResult>(sql).ToList();

                return list;
            }
        }

        /// <summary>
        /// 部门，可多选部门，部门ID用逗号分开，例如 id1,id2,id3
        /// </summary>
        /// <returns></returns>
        public List<SysCollegeResult> GetDepartChilderList(string parentidlist)
        {
            using (var db = new OperationManagerDbContext())
            {
                string[] str = parentidlist.Split(',');
                string instr = "'";
                for (int i = 0; i < str.Length; i++)
                {
                    instr += str[i] + "','";

                }
                instr += "'";
                instr = instr.Substring(0, instr.Length - 3);
                string sql = @"SELECT 'jigouyuanxiCode' AS Resulttype, CID,CollegeName  as Year FROM dbo.SYS_College   WHERE ParentID IN (" + instr + ")";
                List<SysCollegeResult> list = db.Database.SqlQuery<SysCollegeResult>(sql).ToList();

                return list;
            }
        }

        /// <summary>
        /// 内容类型
        /// </summary>
        /// <returns></returns>
        public List<SysCollegeResult> GetTemplateList()
        {
            using (var db = new OperationManagerDbContext())
            {

                string sql = @"SELECT 'TemplateID' AS Resulttype, TemplateID AS cid, ContentType AS year FROM dbo.SYS_Template";
                List<SysCollegeResult> list = db.Database.SqlQuery<SysCollegeResult>(sql).ToList();

                return list;
            }
        }


        /// <summary>
        /// SCI分区，树类型一级菜单
        /// </summary>
        /// <returns></returns>
        //        public List<string> ()
        //        {
        //            using (var db = new OperationManagerDbContext())
        //            {

        //                string sql = @"SELECT DISTINCT  PartitionSection FROM SYS_PeriodicalCasPartition
        //WHERE PartitionSection IS NOT NULL AND PartitionSection <> '' ORDER BY PartitionSection ";
        //                List<string> list = db.Database.SqlQuery<string>(sql).ToList();

        //                return list;
        //            }
        //        }

        //        /// <summary>
        //        /// SCI分区，树类型二级菜单
        //        /// </summary>
        //        /// <returns></returns>
        //        public List<string> (string parentname)
        //        {
        //            using (var db = new OperationManagerDbContext())
        //            {

        //                string sql = @"SELECT  DISTINCT PartitionName FROM SYS_PeriodicalCasPartition
        //WHERE PartitionSection='" + parentname + "'and PartitionName IS NOT NULL AND PartitionName <> '' ORDER BY PartitionName ";
        //                List<string> list = db.Database.SqlQuery<string>(sql).ToList();

        //                return list;
        //            }
        //        }









        /// <summary>
        /// 中科院分区，树类型一级菜单
        /// </summary>
        /// <returns></returns>
        public List<string> GetSYSPeriodicalCasPartitionList()
        {
            using (var db = new OperationManagerDbContext())
            {

                string sql = @"SELECT DISTINCT  PartitionSection FROM SYS_PeriodicalCasPartition
WHERE PartitionSection IS NOT NULL AND PartitionSection <> '' ORDER BY PartitionSection ";
                List<string> list = db.Database.SqlQuery<string>(sql).ToList();

                return list;
            }
        }

        /// <summary>
        /// 中科院分区，树类型二级菜单
        /// </summary>
        /// <returns></returns>
        public List<string> GetSYSPeriodicalCasPartitionByParentNameList(string parentname)
        {
            using (var db = new OperationManagerDbContext())
            {

                string sql = @"SELECT  DISTINCT PartitionName FROM SYS_PeriodicalCasPartition
WHERE PartitionSection='" + parentname + "'and PartitionName IS NOT NULL AND PartitionName <> '' ORDER BY PartitionName ";
                List<string> list = db.Database.SqlQuery<string>(sql).ToList();

                return list;
            }
        }

     













        #endregion




    }
    /// <summary>
    /// 报表输出
    /// </summary>
    public class OutPutReport
    {
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 值
        /// </summary>

        public List<SysCollegeResult> CollegeResult { get; set; }
    }

 

    public class SysCollegeResult
    {
        public Guid CID { get; set; }

        public string Year { get; set; }
        public int? Count { get; set; }
        public string Resulttype { get; set; }

    }
    public class SysCollegeResultOutPut
    {
        public string Resulttype { get; set; }
        public List<SysCollegeResult> List { get; set; }
    }
    public class YearAndCount
    {
        public string ResultType { get; set; }
        public string Year { get; set; }

        public int? Count { get; set; }

    }
    public class YearAndCountOutPut
    {

        public string Year { get; set; }

        public List<YearAndCount> CollegeResult { get; set; }

    }


}
