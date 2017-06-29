using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.DataObjects.Interface
{
    public interface IDataAdapter<T, TCollection>
    {
        /// <summary>
        /// 异步添加或更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AddOrUpdateAsync(T entity);

        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddOrUpdate(T entity);

        /// <summary>
        /// 异步添加或更新集合
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <returns></returns>
        Task<int> AddOrUpdateAsyncCollection(TCollection entityCollection);

        /// <summary>
        /// 添加或更新集合
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <returns></returns>
        int AddOrUpdateCollection(TCollection entityCollection);

        /// <summary>
        /// 异步添加或更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> RemoveAsync(T entity);

        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Remove(T entity);

        /// <summary>
        /// 异步添加或更新集合
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <returns></returns>
        Task<int> RemoveAsyncCollection(TCollection entityCollection);

        /// <summary>
        /// 添加或更新集合
        /// </summary>
        /// <param name="entityCollection"></param>
        /// <returns></returns>
        int RemoveCollection(TCollection entityCollection);
    }
}
