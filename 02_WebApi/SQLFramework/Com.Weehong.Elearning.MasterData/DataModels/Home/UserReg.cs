using Com.Weehong.Elearning.MasterData.DataAdapter.SysManage;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.Home
{
    /// <summary>
    /// 用户注册
    /// </summary>
    public class UserReg
    {

        /// <summary>
        /// 身份列表
        /// </summary>
        public Task<List<SYS_IdentityType>> IdentityTypeList
        {
            get
            {
                return  SYS_IdentityTypeAdapter.Instance.LoadByIdentityTypeDataList();
            }
        }





    }
}
