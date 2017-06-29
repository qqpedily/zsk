using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate
{
    /// <summary>
    /// 元素数据管理
    /// </summary>
    public class SYS_TemplateFieldModelAdapter : EffectedAdapterBase<SYS_TemplateField, List<SYS_TemplateField>>
    {


        /// <summary>
        /// 获取模板数据集合
        /// </summary>
        /// <returns></returns>
        public List<SYS_TemplateField> GetSYS_TemplateFieldList()
        {
            using (var db = new OperationManagerDbContext())
            {
                return this.GetAll().ToList();
            }
        }



    }
}
