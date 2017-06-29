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
    /// 用户
    /// </summary>
    public class UserAdapter : EffectedAdapterBase<UserModel, List<UserModel>>
    {
        public static readonly UserAdapter Instance = new UserAdapter();
        /// <summary>
        /// 读取数据库用户是否激活
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserModel> LoadByLoginUserAsync(string strUserName, string password)
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.User.Where(w => (w.UserEmail == strUserName || w.UserID == strUserName || w.Telphone == strUserName) && w.IsLogin == 0 &&
                w.PassWord == password).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// 读取数据库用户是否激活
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        public async Task<UserModel> LoadByLoginUserAsync(string strUserName)
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.User.Where(w => (w.UserEmail == strUserName || w.UserID == strUserName || w.Telphone == strUserName)).FirstOrDefaultAsync();
            }
        }


        /// <summary>
        /// 根据用户姓名,职称获取详情
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="positional">职称</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns></returns>
        public async Task<DicData> LoadUserByNameAndPositional(string name, string positional, int pageSize, int curPage)
        {
            DicData dicdata = new DicData();
            using (var db = new OperationManagerDbContext())
            {


               // string sql = @"select u.* from [dbo].[User] u left join SYS_PositionalTitleType p on p.PttID=u.PttID where 1=1 ";

                string sql = "SELECT  u.UUID,u.SurnameChinese+u.NameChinese AS username,spt.PostTitleTypeName,sc.CollegeName ,uh.UploadIMG FROM dbo.[User] u LEFT JOIN dbo.SYS_PositionalTitleType spt ON u.PttID= spt.PttID LEFT JOIN dbo.SYS_College sc ON sc.CID= u.DeptID   LEFT JOIN dbo.UserPersonalHomepage uh ON uh.UserID= u.UUID WHERE 1=1 ";


                if (!string.IsNullOrEmpty(name))
                {
                    sql += "  and  (u.SurnameChinese+u.NameChinese) like '%" + name + "%'";
                }
                if (!string.IsNullOrEmpty(positional))
                {
                    sql += " and spt.PostTitleTypeName like '%" + positional + "%'";
                }

                List<UserOutput> list = db.Database.SqlQuery<UserOutput>(sql).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                int count = db.Database.SqlQuery<UserOutput>(sql).Count();

                dicdata.Data = list;
                dicdata.TotalCount = count;

                return dicdata;
            }
        }

        /// <summary>
        /// 获取用户信息 【by ZHL】
        /// </summary>
        /// <param name="Name">用户名称</param>
        /// <returns></returns>
        public async Task<List<UserList>> LoadByUserData(string Name)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @" SELECT * FROM (
                SELECT(u.SurnameChinese + ISNULL(u.NameChinese, '')) as UserName, (ISNULL(u.SurnamePhoneticize, '') + ISNULL(u.NamePhoneticize, ''))
                AS PinYinUserName, * FROM[USER] AS u)t WHERE 1=1";// "

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    sql += " AND t.UserName LIKE '%" + Name + "%' OR PinYinUserName LIKE '%" + Name + "%' OR t.UserEmail LIKE '%" + Name + "%' ";
                }

                List<UserList> list = await db.Database.SqlQuery<UserList>(sql + "").ToListAsync();
                return list;
            }
        }

        /// <summary>
        /// 根据用户姓,拼音首字母获取用户详情
        /// </summary>
        /// <param name="phoneticize">姓的首字母A，B，C，D</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns></returns>
        public async Task<DicData> LoadUserBySurnamePhoneticize(string phoneticize, int pageSize, int curPage)
        {
            DicData dicdata = new DicData();
            using (var db = new OperationManagerDbContext())
            {
                string sql = "SELECT  u.UUID,u.SurnameChinese+u.NameChinese AS username,spt.PostTitleTypeName,sc.CollegeName ,uh.UploadIMG FROM dbo.[User] u LEFT JOIN dbo.SYS_PositionalTitleType spt ON u.PttID= spt.PttID LEFT JOIN dbo.SYS_College sc ON sc.CID= u.DeptID  LEFT JOIN dbo.UserPersonalHomepage uh ON uh.UserID= u.UUID WHERE 1=1 ";
                sql += @" and  u.SurnamePhoneticize like '" + phoneticize + "%'";
                var list = db.Database.SqlQuery<UserOutput>(sql).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                int count = db.Database.SqlQuery<UserOutput>(sql).Count();

                dicdata.Data = list;
                dicdata.TotalCount = count;
                return dicdata;
            }
        }


        /// <summary>
        /// 根据院系获取学者信息
        /// </summary>
        /// <param name="departid">院系结构id</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns>学者列表</returns>
        public DicData LoadUserByDepartID(string departid, int pageSize, int curPage)
        {
            DicData dicdata = new DicData();

            using (var db = new OperationManagerDbContext())
            {
                string sql = "SELECT  u.UUID,u.SurnameChinese+u.NameChinese AS username,spt.PostTitleTypeName,sc.CollegeName ,uh.UploadIMG FROM dbo.[User] u LEFT JOIN dbo.SYS_PositionalTitleType spt ON u.PttID= spt.PttID LEFT JOIN dbo.SYS_College sc ON sc.CID= u.DeptID   LEFT JOIN dbo.UserPersonalHomepage uh ON uh.UserID= u.UUID WHERE 1=1 ";
                sql += @" and ( u.UnitID='" + departid + "' OR u.UnitID='" + departid + "')";


                var list = db.Database.SqlQuery<UserOutput>(sql).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                int count = db.Database.SqlQuery<UserOutput>(sql).Count();

                dicdata.Data = list;
                dicdata.TotalCount = count;
                return dicdata;




            }
        }
        /// <summary>
        /// 根据作者名称获取作者文献数量
        /// </summary>
        /// <param name="username">作者</param>
        /// <returns>数量</returns>
        public int GetProductionCountByAuthor(string username)
        {
         
            using (var db = new OperationManagerDbContext())
            {
                string sql = " SELECT COUNT(*) FROM dbo.StaticProductions WHERE author LIKE '%" + username + "%'";
                
                return db.Database.SqlQuery<int>(sql).Count();
                
            }
        }

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户</returns>
        public UserModel LoadUserByUserName(string username)
        {

            using (var db = new OperationManagerDbContext())
            {

                string sql = @"select u.* from [dbo].[User] u where (u.SurnameChinese+u.NameChinese) like '%" + username + "%'";


                var list = db.Database.SqlQuery<UserModel>(sql).ToList().FirstOrDefault();

                return list;



            }
        }

        /// <summary>
        /// 根据邮箱获取用户信息
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public async Task<UserModel> LoadUserByUserEmail(string userEmail)
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.User.Where(w => w.UserEmail == userEmail).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// 根据邮箱获取用户信息
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public async Task<UserModel> LoadUserByUserPhone(string userphone)
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.User.Where(w => w.Telphone == userphone).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// 根据姓名、部门获取用户信息是否激活
        /// </summary>
        /// <param name="surnameChinese"></param>
        /// <param name="nameChinese"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public async Task<UserModel> LoadByUserNameAndUnit(string surnameChinese, string nameChinese, Guid unitID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return await db.User.AsNoTracking().Where(w => w.SurnameChinese == surnameChinese && w.NameChinese == nameChinese && w.UnitID == unitID).FirstOrDefaultAsync();
            }
        }






    }

    public class UserOutput
    {
        public Guid UUID { get; set; }
        public string Username { get; set; }
        public string PostTitleTypeName { get; set; }
        public string CollegeName { get; set; }
        public string UploadIMG { get; set; }
       // public int? Productioncount { get; set; }

    }
}
