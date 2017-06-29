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
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 个人详细信息
    /// </summary>
    public class MainUserAdapter : EffectedAdapterBase<UserModel, List<UserModel>>
    {
        public static readonly MainUserAdapter Instance = new MainUserAdapter();



        public async Task<List<Dictionary<String, Object>>> LoadContentListCount(string username)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sqltem = @"SELECT * FROM dbo.SYS_Template ";
                List<Dictionary<String, Object>> listdic = new List<Dictionary<string, object>>();
                List<SYS_Template> list = db.Database.SqlQuery<SYS_Template>(sqltem).ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    string sql = @"select count(-1) from StaticProductions p 
	                    where  p.TemplateID='" + list[i].TemplateID + "' and p.author LIKE '%" + username + "%'";
                    var count = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                    Dictionary<String, Object> dic = new Dictionary<string, object>();
                    dic.Add("IndexedTypeID", "TemplateID");
                    dic.Add("IndexedTypeValue", list[i].TemplateID);
                    dic.Add("IndexedType", list[i].ContentType);
                    dic.Add("IndexedTypeCount", count);
                    listdic.Add(dic);
                }


                return listdic;
            }
        }



        /// <summary>
        /// 个人详细信息-发表日期
        ///获取所有年份下的所有文献数量
        /// </summary>
        /// <param name="username">作者</param>
        /// <returns>所有年份，所有年份下的文献and数量</returns>

        public List<Dictionary<String, Object>> GetSYS_YearListAndCountAsync(string username)
        {

            var dicList = LoadProductionYearListAndCountByUserID(username);

            return dicList;
        }
        /// <summary>
        /// 个人详细信息-语言
        ///获取所有语言下的所有文献数量
        /// </summary>
        /// <param name="username">作者的名字</param>
        /// <returns>所有语言，所有语言下的文献数量</returns>

        public List<Dictionary<String, Object>> GetSYS_LanguageListAndCount(string username)
        {
            SYS_TemplateFieldAdapter sys_TemplateFieldModelAdapter = new SYS_TemplateFieldAdapter();

            ProductionsAdapter productionsAdapter = new ProductionsAdapter();
            //var templateField = sys_TemplateFieldModelAdapter.GetSYS_TemplateFieldByMetaDataID("8BC5C3A3-6565-45F8-B77A-891077D4E2A5");
            string languagesql = @"SELECT DISTINCT Iso FROM dbo.StaticProductions WHERE iso IS NOT NULL AND iso<>''";
            using (var db = new OperationManagerDbContext())
            {
                List<string> language = db.Database.SqlQuery<string>(languagesql).ToList();



                // string[] language = templateField.DefaultText.Split(';');
                List<Dictionary<String, Object>> dicList = new List<Dictionary<string, object>>();

                for (int i = 0; i < language.Count; i++)
                {

                    //List<ProductionsField> lif = LoadByDefaultText("8BC5C3A3-6565-45F8-B77A-891077D4E2A5", language[i], "50883877-E367-4D5B-85FD-5F15A5B2E789", username);
                    int count = 0;
                    string sql = @" SELECT  count(*) FROM  StaticProductions WHERE iso LIKE '%" + language[i] + "%' and author LIKE'%" + username + "%'";

                    count = db.Database.SqlQuery<int>(sql).FirstOrDefault();


                    Dictionary<String, Object> dic = new Dictionary<string, object>();

                    dic.Add("LanguageTypeID", "iso");
                    dic.Add("LanguageType", language[i]);
                    dic.Add("LanguageTypeCount", count);

                    dicList.Add(dic);
                }



                return dicList;
            }
        }

        /// <summary>
        /// 个人详细信息-收录类别
        ///获取个人所有模板的收录类别,该收录类别的所有文献数量
        /// </summary>
        /// <param name="username">作者的名字</param>
        /// <returns>所有模板收录类别和该收录类别下的所有文献and数量</returns>

        //E134A22A-4187-4318-BB70-BCC66711ED1D 收录类型
        //50883877-E367-4D5B-85FD-5F15A5B2E789 作者
        public List<Dictionary<String, Object>> GetUserIndexedTypeListAndCount(string username)
        {
            SYS_TemplateFieldAdapter sys_TemplateFieldModelAdapter = new SYS_TemplateFieldAdapter();
            ProductionsFieldAdapter productionsFieldAdapter = new ProductionsFieldAdapter();
            ProductionsAdapter productionsAdapter = new ProductionsAdapter();
            string IndexedTypesql = @"SELECT DISTINCT indexed FROM dbo.StaticProductions WHERE indexed IS NOT NULL AND indexed<>''";

            using (var db = new OperationManagerDbContext())
            {
                List<string> IndexedType = db.Database.SqlQuery<string>(IndexedTypesql).ToList();

                List<Dictionary<String, Object>> dicList = new List<Dictionary<string, object>>();

                for (int i = 0; i < IndexedType.Count; i++)
                {
                    int count = 0;
                    string sql = @" SELECT count(*) FROM  StaticProductions WHERE indexed = '" + IndexedType[i] + "' and author LIKE'%" + username + "%'";

                    count = db.Database.SqlQuery<int>(sql).FirstOrDefault();



                    Dictionary<String, Object> dic = new Dictionary<string, object>();
                    dic.Add("IndexedTypeID", "indexed");
                    dic.Add("IndexedType", IndexedType[i]);
                    dic.Add("IndexedTypeCount", count);

                    dicList.Add(dic);
                }

                return dicList;
            }
        }



        public List<Dictionary<String, Object>> LoadProductionYearListAndCountByUserID(string username)
        {
            using (var db = new OperationManagerDbContext())
            {

                string sql = @" select cast( year(tt.issued)as NVARCHAR) as year ,count(*) as count from StaticProductions tt
  WHERE tt.author LIKE '%" + username + "%' group by year(tt.issued)   ORDER BY YEAR desc ";

                List<Dictionary<String, Object>> dicList = new List<Dictionary<string, object>>();
                var list = db.Database.SqlQuery<YearAndCount>(sql).ToList();

                foreach (var sale in list)
                {
                    Dictionary<String, Object> dic = new Dictionary<string, object>();
                    if (string.IsNullOrEmpty(sale.Year + ""))
                    {
                        dic.Add("ContentYear", "其它");
                        dic.Add("ContentYearCount", sale.Count);
                        dic.Add("ContentYearID", "issued");
                    }
                    else
                    {
                        dic.Add("ContentYear", sale.Year);
                        dic.Add("ContentYearCount", sale.Count);
                        dic.Add("ContentYearID", "issued");
                    }

                    dicList.Add(dic);
                }




                return dicList;
            }
        }


        /// <summary>
        /// 首页-根据院系ID 获取文献信息
        /// </summary>
        /// <param name="cid">院系ID</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns>所有模板收录类别和该收录类别下的所有文献and数量</returns>

        public async Task<DicData> GetProductionsByCID(string cid, int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {

                string sql = @"SELECT * FROM dbo.StaticProductions WHERE ProductionID IN(
	SELECT ProductionID FROM Relation_UserClaimWorks WHERE SysUserID IN
	(SELECT UUID FROM dbo.[User] WHERE UnitID='" + cid + "'))";

                List<StaticProductions> list = await db.Database.SqlQuery<StaticProductions>(sql).ToListAsync();
                int count = list.Count;
                List<StaticProductions> lis = list.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                DicData dicdata = new DicData();
                dicdata.Data = lis;
                dicdata.TotalCount = count;

                return dicdata;

            }
        }


        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="curPage">第几页</param>
        /// <returns></returns>
        public UserTotal GetUserList(int pageSize, int curPage)
        {
            UserTotal userTotal = new UserTotal();
            using (var db = new OperationManagerDbContext())
            {

                userTotal.TotalCount = db.User.Count();
                userTotal.UserList = db.User.OrderBy(c => c.OrderBy).Where(c => c.IsRecommend == 1).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                return userTotal;
            }
        }

        /// <summary>
        /// 研究单元-是否有全文
        ///获取所有收录类别下的是否有全文
        /// </summary>
        /// <returns>所有收录类别，是否有全文</returns>

        public List<TypeNameCount> GetSYS_AttachmentCountByUser(string username)
        {


            var hasnum = 0;
            var nonum = 0;
            string sqlhave = @"SELECT COUNT(*)  FROM  dbo.StaticProductions WHERE ProductionID IN (SELECT BusinessID FROM dbo.Comm_Attachment)  AND author LIKE '%" + username + "%'";

            string sqlnot = @"SELECT COUNT(*)  FROM  dbo.StaticProductions WHERE ProductionID not IN (SELECT BusinessID FROM dbo.Comm_Attachment)  AND author LIKE '%" + username + "%'";

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
