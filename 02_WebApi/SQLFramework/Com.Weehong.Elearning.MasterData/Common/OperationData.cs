using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YinGu.Operation.Framework.Domain.Comm
{
    /// <summary>
    /// 行转列获取元素数据
    /// </summary>
    public class OperationData
    {
        /// <summary>
        /// 静态变量
        /// </summary>
        public static readonly OperationData Instance = new OperationData();
        /// <summary>
        /// 异步行转列
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<DataTable> RCC_DataTableByProductionAsync(List<ProductionsField> list)
        {
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                DataTable newTable = new DataTable();
                var distinct = list.Select(s => s.ProductionID).Distinct();
                //获取模板类
                SYS_Template sys_Template = await db.SYS_Template.AsNoTracking().Where(w => w.TemplateID == new Guid("11111111-2222-3333-4444-00049D56A9CD")).FirstOrDefaultAsync();
                DataColumn new_TemplateName = new DataColumn("ContentType");
                DataColumn new_TemplateID = new DataColumn("TemplateID");
                DataColumn new_ProductionID = new DataColumn("ProductionID");
                newTable.Columns.Add(new_ProductionID);
                newTable.Columns.Add(new_TemplateID);
                newTable.Columns.Add(new_TemplateName);

                DataColumn new_EntryListCustomization = new DataColumn("EntryListCustomization");
                newTable.Columns.Add(new_EntryListCustomization);
                DataColumn new_CustomFormatting = new DataColumn("CustomFormatting");
                newTable.Columns.Add(new_CustomFormatting);
                // Production
                //获取模板所有元数据
                List<SYS_TemplateField> list_Temp = await db.SYS_TemplateField.AsNoTracking().Where(w => w.TemplateID == new Guid("11111111-2222-3333-4444-00049D56A9CD")).ToListAsync();

                //获取实体所有属性
                //获取所有文献ID
                foreach (var dis in distinct)
                {
                    DataRow new_dr = newTable.NewRow();
                    Guid? a = dis.Value;
                    new_dr["ProductionID"] = a;
                    new_dr["ContentType"] = sys_Template.ContentType;
                    new_dr["TemplateID"] = sys_Template.TemplateID;

                    string str_EntryListCustomization = null;
                    //模板文献列表定制
                    foreach (var listCustom in sys_Template.SYS_TemplateEntryListCustomization)
                    {
                        if (listCustom.type == 0)
                        {
                            str_EntryListCustomization += listCustom.value;
                        }
                        else if (listCustom.type == 1)
                        {
                            //存元数据多值数

                            str_EntryListCustomization += list.Where(w => w.MetaDataID == new Guid(listCustom.value) && w.ProductionID == a).FirstOrDefault().DefaultText;
                        }
                        else
                        {
                            str_EntryListCustomization += listCustom.value;
                        }
                    }

                    new_dr["EntryListCustomization"] = str_EntryListCustomization;
                    Dictionary<string, string> PropertyInfoDict = new Dictionary<string, string>();

                    //获取所有元数据
                    foreach (var item in list_Temp)
                    {
                        string text = item.SYS_MetaData.Qualifier;
                        //拼列
                        DataColumn new_colc = new DataColumn(text);
                        if (!newTable.Columns.Contains(text))
                        {
                            newTable.Columns.Add(new_colc);
                        }
                        //存元数据多值数据
                        List<ProductionsField> list_zi = list.Where(w => w.MetaDataID == item.MetaDataID && w.ProductionID == a).ToList();
                        string value = null;
                        foreach (var zi in list_zi)
                        {
                            value += zi.DefaultText + ",";

                        }
                        PropertyInfoDict[text] = value != null && value.Length > 0 ? value.Substring(0, value.Length - 1) : value;
                    }

                    //赋值

                    foreach (var item in PropertyInfoDict)
                    {
                        new_dr[item.Key] = item.Value;
                    }
                    newTable.Rows.Add(new_dr);
                }
                return newTable;
            }
        }

        /// <summary>
        /// 行转列
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataTable RCC_DataTableByProduction(List<ProductionsField> list)
        {
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                DataTable newTable = new DataTable();
                var distinct = list.Select(s => s.ProductionID).Distinct();
                //获取模板类
                SYS_Template sys_Template = db.SYS_Template.AsNoTracking().Where(w => w.TemplateID == new Guid("11111111-2222-3333-4444-00049D56A9CD")).FirstOrDefault();
                DataColumn new_TemplateName = new DataColumn("ContentType");
                DataColumn new_TemplateID = new DataColumn("TemplateID");
                DataColumn new_ProductionID = new DataColumn("ProductionID");
                newTable.Columns.Add(new_ProductionID);
                newTable.Columns.Add(new_TemplateID);
                newTable.Columns.Add(new_TemplateName);

                DataColumn new_EntryListCustomization = new DataColumn("EntryListCustomization");
                newTable.Columns.Add(new_EntryListCustomization);
                DataColumn new_CustomFormatting = new DataColumn("CustomFormatting");
                newTable.Columns.Add(new_CustomFormatting);
                // Production
                //获取模板所有元数据
                List<SYS_TemplateField> list_Temp = db.SYS_TemplateField.AsNoTracking().Where(w => w.TemplateID == new Guid("11111111-2222-3333-4444-00049D56A9CD")).ToList();

                //获取实体所有属性
                //获取所有文献ID
                foreach (var dis in distinct)
                {
                    DataRow new_dr = newTable.NewRow();
                    Guid? a = dis.Value;
                    new_dr["ProductionID"] = a;
                    new_dr["ContentType"] = sys_Template.ContentType;
                    new_dr["TemplateID"] = sys_Template.TemplateID;

                    string str_EntryListCustomization = null;
                    //模板文献列表定制
                    foreach (var listCustom in sys_Template.SYS_TemplateEntryListCustomization)
                    {
                        if (listCustom.type == 0)
                        {
                            str_EntryListCustomization += listCustom.value;
                        }
                        else if (listCustom.type == 1)
                        {
                            //存元数据多值数

                            //str_EntryListCustomization += list.Where(w => w.MetaDataID == new Guid(listCustom.value) && w.ProductionID == a).FirstOrDefault().DefaultText;
                            var strvalue = list.Where(w => w.MetaDataID == new Guid(listCustom.value) && w.ProductionID == a).FirstOrDefault();
                            if (strvalue != null)
                            {
                                str_EntryListCustomization += strvalue.DefaultText;
                            }
                        }
                        else
                        {
                            str_EntryListCustomization += listCustom.value;
                        }
                    }

                    new_dr["EntryListCustomization"] = str_EntryListCustomization;
                    Dictionary<string, string> PropertyInfoDict = new Dictionary<string, string>();

                    //获取所有元数据
                    foreach (var item in list_Temp)
                    {
                        string text = item.SYS_MetaData.Qualifier;
                        //拼列
                        DataColumn new_colc = new DataColumn(text);
                        if (!newTable.Columns.Contains(text))
                        {
                            newTable.Columns.Add(new_colc);
                        }
                        //存元数据多值数据
                        List<ProductionsField> list_zi = list.Where(w => w.MetaDataID == item.MetaDataID && w.ProductionID == a).ToList();
                        string value = null;
                        foreach (var zi in list_zi)
                        {
                            value += zi.DefaultText + ",";

                        }
                        PropertyInfoDict[text] = value != null && value.Length > 0 ? value.Substring(0, value.Length - 1) : value;
                    }

                    //赋值

                    foreach (var item in PropertyInfoDict)
                    {
                        new_dr[item.Key] = item.Value;
                    }
                    newTable.Rows.Add(new_dr);
                }
                return newTable;
            }
        }
    }
}
