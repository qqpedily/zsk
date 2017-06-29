using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.DataModels;
using Com.Weehong.Elearning.MasterData.Repositories;
using System.Data.Entity;
using System.Collections.Generic;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.SysManage
{
    /// <summary>
    /// 院系科室配置
    /// </summary>
    public class SYS_CollegeAdapter : EffectedAdapterBase<SYS_College, List<SYS_College>>
    {
        public static readonly SYS_CollegeAdapter Instance = new SYS_CollegeAdapter();



        /// <summary>
        /// 根据父节点获取子类
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public List<SYS_College> getSYSCollegeListByParentID(string parentid)
        {

            using (var db = new OperationManagerDbContext())
            {
                return db.SYS_College.Where(s => s.ParentID.ToString() == parentid).ToList();

            }
        }


        /// <summary>
        /// 根据父节点获取子类
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public async Task<List<SYS_College>> GetListByParentIDAsync(string parentid)
        {

            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT c.*,(SELECT COUNT(1) FROM dbo.ProductionsField WHERE MetaDataID='D06AB22F-773B-4A2A-B557-DC8C6A224FEF' 
AND DefaultValue=CONVERT(nvarchar(36),c.CID)) AS ProcuctsNum,
(SELECT COUNT(1) from dbo.ProductionsField WHERE MetaDataID='D06AB22F-773B-4A2A-B557-DC8C6A224FEF' AND DefaultValue IN
(SELECT CONVERT(nvarchar(36),CID) FROM SYS_College WHERE ParentID=CONVERT(nvarchar(36),c.CID))) AS ProcuctsAllNum 
FROM SYS_College c WHERE c.ParentID='" + parentid + "'";

                return await db.Database.SqlQuery<SYS_College>(sql).ToListAsync();
            }
        }

        public async Task<List<SYS_College>> GetSYSCollegeListIndex()
        {
            using (var db = new OperationManagerDbContext())
            {
               // return await db.SYS_College.Where(s => s.CollegeType == 1).ToListAsync();

                string sql = @"  SELECT * ,0 AS 'ProcuctsNum',0 as 'ProcuctsAllNum' FROM SYS_College WHERE CollegeType=1 ";

                return await db.Database.SqlQuery<SYS_College>(sql).ToListAsync();

            }
        }

        public async Task<List<SYS_College>> GetSYSCollegeList()
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT c.*,(SELECT COUNT(1) FROM dbo.ProductionsField WHERE MetaDataID='D06AB22F-773B-4A2A-B557-DC8C6A224FEF' 
AND DefaultValue=CONVERT(nvarchar(36),c.CID)) AS ProcuctsNum,
(SELECT COUNT(1) from dbo.ProductionsField WHERE MetaDataID='D06AB22F-773B-4A2A-B557-DC8C6A224FEF' AND DefaultValue IN
(SELECT CONVERT(nvarchar(36),CID) FROM SYS_College WHERE ParentID=CONVERT(nvarchar(36),c.CID))) AS ProcuctsAllNum
FROM SYS_College c WHERE CollegeType=1";

                return await db.Database.SqlQuery<SYS_College>(sql).ToListAsync();


            }
        }
        public async Task<SYS_College> GetSYSCollegeByID(Guid cid)
        {
            using (var db = new OperationManagerDbContext())
            {
                string sql = @"SELECT c.*,(SELECT COUNT(1) FROM dbo.ProductionsField WHERE MetaDataID='D06AB22F-773B-4A2A-B557-DC8C6A224FEF' 
AND DefaultValue=CONVERT(nvarchar(36),c.CID)) AS ProcuctsNum,
(SELECT COUNT(1) from dbo.ProductionsField WHERE MetaDataID='D06AB22F-773B-4A2A-B557-DC8C6A224FEF' AND DefaultValue IN
(SELECT CONVERT(nvarchar(36),CID) FROM SYS_College WHERE ParentID=CONVERT(nvarchar(36),c.CID))) AS ProcuctsAllNum 
FROM SYS_College c WHERE c.CID='" + cid + "'";

                return await db.Database.SqlQuery<SYS_College>(sql).FirstOrDefaultAsync();


            }
        }
    }
}
