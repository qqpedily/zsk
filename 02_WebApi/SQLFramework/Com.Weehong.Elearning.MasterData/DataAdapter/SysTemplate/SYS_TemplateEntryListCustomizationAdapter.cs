using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate
{
    /// <summary>
    /// 模板文献列表定制
    /// </summary>
    public class SYS_TemplateEntryListCustomizationAdapter : EffectedAdapterBase<SYS_TemplateEntryListCustomization, List<SYS_TemplateEntryListCustomization>>
    {
        public static readonly SYS_TemplateEntryListCustomizationAdapter Instance = new SYS_TemplateEntryListCustomizationAdapter();
    }
}
