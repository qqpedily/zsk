using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataAdapter
    {
        /// <summary>
        /// 异步添加或更新
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        Task<int> AddOrUpdateAsync(object o);

        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        int AddOrUpdate(object o);

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        Task<int> RemoveAsync(object o);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        int Remove(object o);
    }
}
