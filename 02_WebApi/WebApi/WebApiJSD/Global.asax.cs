
using Com.Weehong.Elearning.MasterData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApiZSK
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 设置JSon格式
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //xml转json
            ConfigureApi(GlobalConfiguration.Configuration); 

            using (var dbcontext = new Com.Weehong.Elearning.MasterData.Repositories.OperationManagerDbContext())
            {
                var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (System.Data.Entity.Core.Mapping.StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(System.Data.Entity.Core.Metadata.Edm.DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<System.Data.Entity.Core.Metadata.Edm.EdmSchemaError>());
            }
        }
        void ConfigureApi(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

        /// <summary>
        /// 网络跨域设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod.Equals("OPTIONS"))
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }
    }
}
