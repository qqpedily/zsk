using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.IO;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysManage;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;

namespace Com.Weehong.Elearning.MasterData.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheClass
    {


        public static readonly SYS_CollegeAdapter InstanceCollege = new SYS_CollegeAdapter();
        public static readonly ProductionsAdapter productionsAdapter = new ProductionsAdapter();
        public static readonly ProductionsUploadFileAdapter productionsUploadFileAdapter = new ProductionsUploadFileAdapter();
        public static readonly SYS_TemplateFieldAdapter sYS_TemplateFieldModelAdapter = new SYS_TemplateFieldAdapter();
        public static readonly ProductionsFieldAdapter productionsFieldAdapter = new ProductionsFieldAdapter();
        //public static readonly TransProductions transProductions = new TransProductions();
        //private readonly TransUser transUser = new TransUser();

        private static readonly SYS_TemplateFieldAdapter sys_TemplateFieldModelAdapter = new SYS_TemplateFieldAdapter();

        private static readonly SYS_TemplateAdapter sys_TemplateAdapter = new SYS_TemplateAdapter();

       




        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }
        /// <summary>
        /// 加载初始数据
        /// </summary>
        public static void Loading()
        {

            //首页相关cache 
            //object CollegeList = CacheClass.GetCache("CollegeList");

            CacheClass.SetCache("CollegeList", InstanceCollege.GetSYSCollegeList());
            //全部成果
            CacheClass.SetCache("chengguoCount", productionsAdapter.LoadProductionCitationNumAll());
            //全文数量
            CacheClass.SetCache("wenxianCount", productionsAdapter.LoadProductionAllCount());
            //最新文献
            CacheClass.SetCache("getNewPro", productionsAdapter.LoadProductionOrderByCraeattime(8, 1));
            //重要成果
            CacheClass.SetCache("ImportResult", productionsAdapter.LoadProductionOrderByCitationNum(8, 1));
            //下载最多
            CacheClass.SetCache("maxDown", productionsAdapter.LoadProductionOrderByDownloadNum(8, 1));

            ////学者推荐
            //CacheClass.SetCache("scholarRe", "");
            //内容类型
            CacheClass.SetCache("contentType", sys_TemplateFieldModelAdapter.GetSYS_TemplateContentTypeListAndCount());
            //发表日期
            CacheClass.SetCache("ContentYear", sys_TemplateFieldModelAdapter.GetSYS_YearListAndCount());
            //语言
            CacheClass.SetCache("languageConut", sys_TemplateFieldModelAdapter.GetSYS_LanguageListAndCount());
            //收录类型 collectType
            CacheClass.SetCache("collectType", sys_TemplateFieldModelAdapter.GetSYS_IndexedTypeListAndCount());
            //object CollegeList = CacheClass.GetCache("CollegeList");
            //object chengguoCount = CacheClass.GetCache("chengguoCount");
            //object wenxianCount = CacheClass.GetCache("wenxianCount");
            //object getNewPro = CacheClass.GetCache("getNewPro");

        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
           
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }
        /// <summary>
        /// 清除单一键缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        public static void RemoveOneCache(string CacheKey)
        {
            Cache objCache = HttpRuntime.Cache;
            objCache.Remove(CacheKey);
        }
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            if (_cache.Count > 0)
            {
                ArrayList al = new ArrayList();
                while (CacheEnum.MoveNext())
                {
                    al.Add(CacheEnum.Key);
                }
                foreach (string key in al)
                {
                    _cache.Remove(key);
                }
            }
        }


        /// <summary>
        /// 以列表形式返回已存在的所有缓存 
        /// </summary>
        /// <returns></returns> 
        public static ArrayList ShowAllCache()
        {
            ArrayList al = new ArrayList();
            Cache _cache = HttpRuntime.Cache;
            if (_cache.Count > 0)
            {
                IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
                while (CacheEnum.MoveNext())
                {
                    al.Add(CacheEnum.Key);
                }
            }
            return al;
        }



        //public DataSet GetDataSetByTypeid(string typeid)
        //{
        //    string CacheKey = "KeyName-" + typeid;
        //    DataSet objModel = (DataSet)GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = GetDataSetByTypeid(typeid);//取得对象操作,根据自已的需求随意改
        //            if (objModel != null)
        //            {
        //                int intCache = 30;//缓存时间,
        //                //绝对期限
        //                SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(intCache), TimeSpan.Zero);
        //                //可调整期限 最后一次访问所插入对象时与该对象到期时之间的时间间隔。
        //                //DataCache.SetCache(CacheKey, objModel, DateTime.MaxValue, TimeSpan.FromMinutes(3));
        //            }
        //        }
        //        catch
        //        { }
        //    }
        //    return objModel;
        //}





    }
}
