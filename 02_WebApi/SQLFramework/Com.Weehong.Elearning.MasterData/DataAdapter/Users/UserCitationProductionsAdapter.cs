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
using Com.Weehong.Elearning.MasterData.DataModels;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 用户关注文献
    /// </summary>
    public class UserCitationProductionsAdapter : EffectedAdapterBase<UserCitationProductions, List<UserCitationProductions>>
    {
        /// <summary>
        /// SCI引用TOP100作者
        /// </summary>
        /// <param name="staryear">开始年份</param>
        /// <param name="endyear">结束年份</param>
        /// <param name="sciorcscd">sci 或者 cscd类型</param>
        /// <param name="departid">机构id</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns></returns>
        public async Task<DicData> LoadUserCitationProductionsByContain(string staryear, string endyear, string sciorcscd, string departid, int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {
                string sql = @"select top 100 sp.dataset as type,u.UUID as userid, u.SurnameChinese+u.NameChinese as username,COUNT(*)as count from UserCitationProductions uc left join [User] u
	on uc.UserID=u.UUID left join StaticProductions sp on uc.ProductionID=sp.ProductionID
	where 1=1 ";
                sql += " and year(sp.issued) between '" + staryear + "' and '" + endyear + "' ";
                if (!string.IsNullOrEmpty(sciorcscd))
                {
                    sql += " and sp.dataset like '%" + sciorcscd + "%'";
                }


                sql += "  group by u.UUID, u.SurnameChinese+u.NameChinese,sp.dataset ";


                DicData dicdata = new DicData();

                List<UserCitationProductionsModel> list = await db.Database.SqlQuery<UserCitationProductionsModel>(sql).ToListAsync();

                int count = list.Count;
                List<UserCitationProductionsModel> lis = list.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                dicdata.Data = lis;
                dicdata.TotalCount = count;
                return dicdata;
            }
        }




        /// <summary>
        /// SCI引用TOP100论文
        /// </summary>
        /// <param name="staryear">开始年份</param>
        /// <param name="endyear">结束年份</param>
        /// <param name="sciorcscd">sci 或者 cscd类型</param>
        /// <param name="departid">机构id</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns></returns>
        public async Task<DicData> LoadCitationProductionsTop100(string staryear, string endyear, string sciorcscd, string departid, int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {
                string sql = @"select top 100 sp.* from UserCitationProductions uc  left join StaticProductions sp on uc.ProductionID=sp.ProductionID  where 1=1  ";
                sql += " and year(sp.issued) between '" + staryear + "' and '" + endyear + "' ";
                if (!string.IsNullOrEmpty(sciorcscd))
                {
                    sql += " and sp.dataset like '%" + sciorcscd + "%'";
                }

                
                DicData dicdata = new DicData();

                List<StaticProductions> list = await db.Database.SqlQuery<StaticProductions>(sql).ToListAsync();

                int count = list.Count;
                List<StaticProductions> lis = list.Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();

                dicdata.Data = lis;
                dicdata.TotalCount = count;
                return dicdata;
            }
        }
    }

    public class UserCitationProductionsModel
    {
        public string UsernID;
        public string UserName;
        public string Count;
        public string Type;

    }
}
