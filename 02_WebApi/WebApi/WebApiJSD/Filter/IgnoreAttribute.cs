using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiZSK.Filter
{
    /// <summary>
    /// 忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class IgnoreAttribute : Attribute
    {



    }
}