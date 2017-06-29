using Com.Weehong.Elearning.DataHelper;
using Com.Weehong.Elearning.Files.Attachment;
using Com.Weehong.Elearning.MasterData;
using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataModels;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YinGu.Operation.Framework.Domain.Comm;

namespace WebApiZSK
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //string[] asa = { "张三", "李四", "王五" };
                //ProductionsFieldAdapter a = new ProductionsFieldAdapter();

                ////115 作者元数据的CODE
                //var list = a.LoadByMetaData(115, asa);

                //var list2 = a.LoadByMetaData(115, new string[] { "张三" }, false);
            }
        }
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        private DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            // Guid g = new Guid("6136F7AF-E99F-4534-B62F-AA0371C2CEC9");
            List<ProductionsField> list = ProductionsFieldAdapter.Instance.GetAll().Where(w => w.ProductionID == new Guid("6136F7AF-E99F-4534-B62F-AA0371C2CEC9")).ToList();
            //List<TBModel> list = new List<TBModel>();
            //TBAdapter adapter = new TBAdapter();
            //list = adapter.GetAll().ToList();

            List<string> lstring = new List<string>();
            lstring.Add("ProductionID");
            lstring.Add("TemplateID");
            //lstring.Add("DefaultText");
            //lstring.Add("FieldSequence");
            List<string> AllNumberField = new List<string>();
            AllNumberField.Add("DefaultText");
            //AllNumberField.Add("DefaultValue");

            List<string> loutstring;

            //Attachment attachment = new Attachment();
            List<dynamic> listcc = DataHelper.DynamicLinq(list, lstring, "FieldValue", AllNumberField, out loutstring);

            DataTable tdd = DataHelper.ListChangeDataTable(listcc);
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}