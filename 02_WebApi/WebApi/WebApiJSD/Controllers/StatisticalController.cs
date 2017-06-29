using Com.Weehong.Elearning.MasterData.DataAdapter.SysManage;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiZSK.Controllers
{
    /// <summary>
    /// 统计
    /// </summary>
    public class StatisticalController : ApiController
    {
        private readonly StatisticalAdapter statisticaladapter = new StatisticalAdapter();
        private readonly UserCitationProductionsAdapter usercitationproductionsadapter = new UserCitationProductionsAdapter();


        /// <summary>
        /// 年度成果院所排名---//--年度成果科系排名
        /// </summary>
        /// <param name="collegetype">1 院系，2 科系</param>
        /// <param name="top">前top条</param>
        /// <returns></returns>
        public IHttpActionResult GetStatisticalByStarYearAndEndYear(string collegetype, string top, int pageSize, int curPage)
        {
            var dicList = statisticaladapter.GetStatisticalByStarYearAndEndYear(collegetype, top, pageSize, curPage);

            return Json(dicList);
        }







        /// <summary>
        /// 年度论文院所收录情况排名---/年度论文科系收录情况排名
        /// </summary>
        /// <param name="collegetype">院系类型 1 院系，2科系</param>
        /// <param name="top">前top条</param>
        /// <returns></returns>
        /// <returns></returns>
        public IHttpActionResult GetTongJiByNDLunWenYuanSuoShouLuPaiMing(string collegetype, string top, int pageSize, int curPage)
        {

            return Json(statisticaladapter.GetTongJiByNDChengGuoYuanSuoPaiMing(collegetype, top, pageSize, curPage));

        }


        ///// <summary>
        ///// 年度论文院所收录情况排名---/年度论文科系收录情况排名
        ///// </summary>
        ///// <param name="collegetype">院系类型 1 院系，2科系</param>
        ///// <returns></returns>
        //public IHttpActionResult GetTongJiByNDChengGuoYuanSuoPaiMing(string collegetype, string staryear, string endyear)
        //{
        //    return Json(statisticaladapter.GetTongJiByNDChengGuoYuanSuoPaiMing(collegetype, staryear, endyear));
        //}

        /// <summary>
        /// 年度论文收录情况统计
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetTongJiByNDLunWenShoulu(string departid, string staryear, string endyear, string indexed)
        {
            return Json(statisticaladapter.GetTongJiByNDLunWenShoulu(departid, staryear, endyear, indexed));


        }

        /// <summary>
        /// 成果收录情况年度比较
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetTongJiByNDChengGuoBiJiao()
        {
            SYS_TemplateFieldAdapter sys_TemplateFieldModelAdapter = new SYS_TemplateFieldAdapter();
            return Json(sys_TemplateFieldModelAdapter.GetSYS_IndexedTypeListAndCount());
        }

        /// <summary>
        /// 年度成果类型分布
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetStatisticalByNianDuChengGuoLeiXingFenBu(string staryear, string endyear)
        {
            var dicList = statisticaladapter.GetStatisticalByNianDuChengGuoLeiXingFenBu(staryear, endyear);
            return Json(dicList);

        }



        /// <summary>
        /// 作者发文量TOP100
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetStatisticalByZuoZheTop100(string staryear, string endyear)
        {
            var dicList = statisticaladapter.GetStatisticalByZuoZheTop100(staryear, endyear);
            return Json(dicList);
        }
        /// <summary>
        /// 作者SCI发文量TOP100/作者CSCD发文量TOP100
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetStatisticalByZuoZheSCIOrCSCDTop100(string staryear, string endyear, string sciorcscd)
        {
            var dicList = statisticaladapter.GetStatisticalByZuoZheSCIOrCSCDTop100
                (staryear, endyear, sciorcscd);
            return Json(dicList);
        }


        /// <summary>
        /// SCI引用TOP100作者--/--CSCD引用TOP100作者
        /// </summary>
        /// <param name="staryear">开始年份</param>
        /// <param name="endyear">结束年份</param>
        /// <param name="sciorcscd">sci 或者 cscd类型</param>
        /// <param name="departid">机构id</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadUserCitationProductionsByContain(string staryear, string endyear, string sciorcscd, string departid, int pageSize, int curPage)
        {
            var list = await usercitationproductionsadapter.LoadUserCitationProductionsByContain(staryear, endyear, sciorcscd, departid, pageSize, curPage);

            return Json(list);

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
        [HttpGet]

        public async Task<IHttpActionResult> LoadCitationProductionsTop100(string staryear, string endyear, string sciorcscd, string departid, int pageSize, int curPage)
        {
            var list = await usercitationproductionsadapter.LoadCitationProductionsTop100(staryear, endyear, sciorcscd, departid, pageSize, curPage);

            return Json(list);
        }


        /// <summary>
        /// 职工职称 统计
        /// 统计每个职称有多少职工
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTongJiZhiGongZhiCheng()
        {
            var list = statisticaladapter.GetTongJiZhiGongZhiCheng();

            return Json(list);
        }



        #region 自选统计
        /// <summary>
        /// 自选统计--收录类别
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetIndexedList()
        {
            return Json(statisticaladapter.GetIndexedList());
        }

        /// <summary>
        /// 自选统计--单位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDepartList()
        {
            return Json(statisticaladapter.GetDepartList());
        }

        /// <summary>
        /// 自选统计--部门，可多选部门，部门ID用逗号分开，例如 id1,id2,id3
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDepartChilderList(string parentidlist)
        {
            return Json(statisticaladapter.GetDepartChilderList(parentidlist));
        }

        /// <summary>
        /// 自选统计--职称
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSYSPositionalTitleTypeList()
        {
            return Json(SYS_PositionalTitleTypeAdapter.Instance.GetAll().ToList());
        }
        /// <summary>
        /// 自选统计--身份信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> LoadByIdentityTypeData()
        {
            SYS_IdentityTypeAdapter adapter = new SYS_IdentityTypeAdapter();
            List<SYS_IdentityType> list = await adapter.LoadByIdentityTypeData();

            return Json(list);
        }

        /// <summary>
        /// 自选统计--内容类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTemplateList()
        {
            return Json(statisticaladapter.GetTemplateList());
        }





        /// <summary>
        /// 中科院分区，树类型一级菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSYSPeriodicalCasPartitionList()
        {
            return Json(statisticaladapter.GetSYSPeriodicalCasPartitionList());
        }


        /// <summary>
        /// 中科院分区，树类型二级菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSYSPeriodicalCasPartitionByParentNameList(string parentname)

        {
            return Json(statisticaladapter.GetSYSPeriodicalCasPartitionByParentNameList(parentname));
        }

        ///<summary>
        /// 查询sql
        ///</summary>
        ///<param name="input">输入查询条件</param>
        ///<param name="diclist">多条件集合</param>
        ///<param name="gender">性别 0为男，1为女</param>
        ///<param name="authorname">作者</param>
        ///<param name="tjauthorname">提交作者</param>
        ///<param name="startime">开始时间</param>
        ///<param name="endtime">结束时间</param>
        ///<param name="xgrouptype"></param>
        ///<param name="ytype">UnitID 机构的一级 ,DeptID 机构二级</param>
        ///<param name="pttid ">职称id,'id1','id2'</param>
        ///<param name="itid ">身份id,'id1','id2'</param>
        ///<returns></returns>
        [HttpPost]
        public IHttpActionResult GetProductionByCondition([FromBody]ConditionInput input)
        {
          
           
            return Json("");


        }

        #endregion









    }

    /// <summary>
    /// 输入查询条件
    /// </summary>
   
    public class ConditionInput {
        /// <summary>
        /// 
        /// </summary>
        public List<Dictionary<string, string>> Diclist { get; set; }
        /// <summary>
        /// 性别 0为男，1为女
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Authorname { get; set; }
        /// <summary>
        /// 提交作者
        /// </summary>
        public string Tjauthorname { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string Startime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string Endtime { get; set; }
        /// <summary>
        /// 职称id,'id1','id2'
        /// </summary>
        public string Pttid { get; set; }
        /// <summary>
        /// 身份id,'id1','id2'
        /// </summary>
        public string Itid { get; set; }
        /// <summary>
        /// 收录类别 indexed，
        /// </summary>
        public string Xgrouptype { get; set; }
        /// <summary>
        /// 单位，或者部门
        /// </summary>
        public string Ytype { get; set; }
        /// <summary>
        /// 引用次数开始
        /// </summary>
        public string Starcitations { get; set; }
        /// <summary>
        /// 引用次数结束
        /// </summary>
        public string Endcitations { get; set; }

        /// <summary>
        /// 一级机构id--'id1','id2'
        /// </summary>
        public string UnitID { get; set; }

        /// <summary>
        /// 二级机构id--'id1','id2'
        /// </summary>
        public string DeptID { get; set; }


    }

}
