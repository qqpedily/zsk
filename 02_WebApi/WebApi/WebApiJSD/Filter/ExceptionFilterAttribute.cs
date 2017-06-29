using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;

namespace WebApiZSK.Filter
{
   /// <summary>
   ///处理异常
   /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is TokenException)
            {
                Dictionary<String, Object> apiResult = new Dictionary<string, object>();
                apiResult.Add("currentDate", DateTime.Now);
                apiResult.Add("errMsg", actionExecutedContext.Exception.Message);
                apiResult.Add("errCode", -1);
                actionExecutedContext.Response = GetResponseMessage(apiResult); //new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else if (actionExecutedContext.Exception is DataException)
            {
                Dictionary<String, Object> apiResult = new Dictionary<string, object>();
                apiResult.Add("currentDate", DateTime.Now);
                apiResult.Add("errMsg", actionExecutedContext.Exception.Message);
                apiResult.Add("errCode", -1);
                actionExecutedContext.Response = GetResponseMessage(apiResult); //new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else
            {
                Dictionary<String, Object> apiResult = new Dictionary<string, object>();
                apiResult.Add("currentDate", DateTime.Now);
                apiResult.Add("errMsg", actionExecutedContext.Exception.Message);
                apiResult.Add("errCode", -1);
                actionExecutedContext.Response = GetResponseMessage(apiResult);
            }
            base.OnException(actionExecutedContext);
        }

        private HttpResponseMessage GetResponseMessage(Dictionary<String, Object> message)
        {
            JsonMediaTypeFormatter jmtf = new JsonMediaTypeFormatter();
            jmtf.SerializerSettings.Converters.Insert(
                0, new JsonDateTimeConverter());
            return new HttpResponseMessage()
            {
                Content = new ObjectContent<Dictionary<String, Object>>(
                    message,
                    jmtf,
                    "application/json"
                    )
            };
        }

    }

    class ApiModelsBase
    {
        public int Code { set; get; }
        public Object Message { set; get; }
    }

    public class TokenException : Exception
    {
        private TokenException() { }

        public TokenException(string message = "用户验证失败")
            : base(message) { }

        //public TokenException(string message = "用户验证失败", Exception innerException)
        //    : base(message, innerException) { }
    }
    public class DataException : Exception
    {
        private DataException() { }

        public DataException(string message = "信息查询失败")
            : base(message) { }

    }
    public class JsonDateTimeConverter : IsoDateTimeConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime dataTime;
            if (DateTime.TryParse(reader.Value.ToString(), out dataTime))
            {
                return dataTime;
            }
            else
            {
                return existingValue;
            }
        }

        public JsonDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }

}