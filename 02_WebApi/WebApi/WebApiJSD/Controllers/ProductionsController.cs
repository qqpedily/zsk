using Com.Weehong.Elearning.DataHelper;
using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiZSK.Models.Input;
using WebApiZSK.Models.Output;
using YinGu.Operation.Framework.Domain.Comm;
using YinGu.Operation.Framework.Domain.Productions;

namespace WebApiZSK.Controllers
{
    /// <summary>
    /// 作品文献
    /// </summary>
    public class ProductionsController : ApiController
    {
        /// <summary>
        /// 作品文献
        /// </summary>
        ProductionsAdapter productionsAdapter = new ProductionsAdapter();
        ProductionsUploadFileAdapter productionsUploadFileAdapter = new ProductionsUploadFileAdapter();
        SYS_TemplateFieldAdapter sYS_TemplateFieldModelAdapter = new SYS_TemplateFieldAdapter();
        ProductionsFieldAdapter productionsFieldAdapter = new ProductionsFieldAdapter();
        TransProductions transProductions = new TransProductions();
        StaticProductionsAdapter staticproductionsadapter = new StaticProductionsAdapter();
        #region  作品文献
        /// <summary>
        ///获取用户作品文献列表
        ///【by ZHL】
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="curPage">页数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetProductionsList(Guid UserID, int pageSize, int curPage)
        {
            return Json(await transProductions.GetProductionsList(UserID, pageSize, curPage));
        }

        /// <summary>
        /// 新增/编辑 作品文献
        /// 【by ZHL】
        /// </summary>
        /// <param name="model">实体 json串</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> AddOrUpdateProductionsField([FromBody]ProductionsModel model)
        {
            //System.Collections.Specialized.NameValueCollection

            return Json(await transProductions.AddOrUpdateProductionsField(model));
        }
        /// <summary>
        /// 独立上传接口  
        /// </summary>
        /// <param name="ProductionID">文献ID</param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<IHttpActionResult> SavaFiles(Guid ProductionID)
        //{
        //    OperationFiles operationFiles = new OperationFiles();
        //    return Json(await operationFiles.SaveFile(ProductionID));
        //}


        /// <summary>
        ///根据作品id 获取作品信息
        /// </summary>
        /// <param name="ProductionID">作品ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProductionsByID(Guid ProductionID)
        {
            return Json(staticproductionsadapter.GetProductionsByID(ProductionID));
        }

        /// <summary>
        ///根据作品文献id 获取作品文献信息
        /// </summary>
        /// <param name="ProductionID">作品文献ID</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProductionsFieldByID(Guid ProductionID)
        {
            return Json(productionsFieldAdapter.GetAll().Where(w => w.ProductionID == ProductionID).ToList());
        }




        /// <summary>
        ///根据作品文献 属性 获取作品文献信息
        ///通过查询获取文献 例如：作者、主题等
        /// columname='doctype' 文章类型
        /// columname='author' 作者
        /// columname='title' 题名
        /// </summary>
        /// <param name="columname">列明</param>
        /// <param name="metaDataValue">作品文献属性值 。'','',''数组形式参数</param>
        /// <param name="isExact">true模糊查询，false精确查询</param>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页数</param>
        ///  <returns></returns>
        /// 
        [HttpGet]
        public async Task<IHttpActionResult> GetLoadProductionByMetaData(string columname, string metaDataValue, int pageSize, int curPage, bool isExact = true)
        {
            string[] metaDataValues = metaDataValue.Split(',');

            return Json(await productionsAdapter.LoadProductionByMetaData(columname, metaDataValues, pageSize, curPage, isExact));
        }

        #endregion

        /// <summary>
        ///首页-获取最新文献信息
        /// </summary>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        ///  <returns></returns>
        [HttpGet]
        public IHttpActionResult LoadProductionOrderByCraeattime(int pageSize, int curPage)
        {
            return Json(productionsAdapter.LoadProductionOrderByCraeattime(pageSize, curPage));
        }

        /// <summary>
        ///首页-文献信息，按照下载量排序
        /// </summary>
        /// <param name="pageSize">条数</param>
        /// <param name="curPage">页码</param>
        ///  <returns></returns>
        [HttpGet]
        public IHttpActionResult LoadProductionOrderByDownloadNum(int pageSize, int curPage)
        {
            return Json(productionsAdapter.LoadProductionOrderByDownloadNum(pageSize, curPage));

        }

        /// <summary>
        ///首页-获取所有文献数量
        /// </summary>
        ///  <returns></returns>
        [HttpGet]
        public IHttpActionResult LoadProductionAllCount()
        {

            return Json(productionsAdapter.LoadProductionAllCount());
        }

        /// <summary>
        ///首页-获取重要成果文献信息
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="curPage">页码</param>
        ///  <returns></returns>
        [HttpGet]
        public IHttpActionResult LoadProductionOrderByCitationNum(int pageSize, int curPage)
        {
            return Json(productionsAdapter.LoadProductionOrderByCitationNum(pageSize, curPage));
        }




        /// <summary>
        ///首页-获取重要文献引用量总数
        /// </summary>
        ///  <returns></returns>
        [HttpGet]
        public IHttpActionResult LoadProductionCitationNumAll()
        {
            return Json(productionsAdapter.LoadProductionCitationNumAll());
        }

        /// <summary>
        ///条件查询文献
        /// 输入参数Diclist查询条件，PageSize每页条数,页数CurPage，
        /// Order排序字段名称(引用次数CitationNum)，(createtime 创建时间)(title 标题)
        /// OrderBy排序方式（正序asc，倒序desc），
        /// </summary>
        ///  <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> LoadProductionByCondition([FromBody]RequestInput input)
        {

            return Json(await productionsAdapter.LoadProductionByCondition(input.Diclist,input.Hasattachment, input.PageSize, input.CurPage, input.Order, input.OrderBy));
        }
        /// <summary>
        /// 获取作者的合作者
        /// </summary>
        /// <param name="author">作者名称</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult LoadAuthorList(string author, int pageSize, int curPage)
        {
            return Json(productionsAdapter.LoadAuthorList(author, pageSize, curPage));
        }


        /// <summary>
        /// 按找院系一级菜单查询文献
        /// </summary>
        /// <param name="departid">院系id</param>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult LoadProductionByDepartID(string departid, int pageSize, int curPage)
        {
            return Json(productionsAdapter.LoadProductionByDepartID(departid, pageSize, curPage));
        }


        /// <summary>
        /// 更新下载量
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult AddProductsDownloadNum(Guid productid)
        {
            return Json(productionsAdapter.AddProductsDownloadNum(productid));


        }





        /// <summary>
        /// 更新访问量
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult AddProductsBrowseNum(Guid productid)
        {
            return Json(productionsAdapter.AddProductsBrowseNum(productid));


        }



    }
        /// <summary>
        /// 输入参数
        /// </summary>
        public class RequestInput
    {


        /// <summary>
        /// 查询条件
        /// </summary>
        public List<Dictionary<string, string>> Diclist { get; set; }



        /// <summary>
        /// 有无附件，有，无
        /// </summary>
        public string Hasattachment { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int CurPage { get; set; }

        /// <summary>
        /// 排序字段名称
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 正序asc，倒序desc
        /// </summary>
        public string OrderBy { get; set; }



    }
}
