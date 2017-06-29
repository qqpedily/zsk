using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Com.Weehong.Elearning.DataObjects.Interface;
using System.Linq.Expressions;

namespace Com.Weehong.Elearning.DataObjects
{
    public class OperationManagerDbContextBase<T> : DbContext
        where T : class
    {
        public OperationManagerDbContextBase()
            : base("OperationManager") { }

        public OperationManagerDbContextBase(string nameConnectionString)
           : base(nameConnectionString)
        {

        }
        public virtual DbSet<T> DbContextBase { get; set; }

    }

    /// <summary>
    /// 带修改、添加、删除的Adapter基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EffectedAdapterBase<T, TCollection> : IDataAdapter<T, TCollection>
        where T : class
        where TCollection : IEnumerable<T>
    {
        OperationManagerDbContextBase<T> db = null;
        public EffectedAdapterBase()
        {
            db = new OperationManagerDbContextBase<T>();
        }

        public EffectedAdapterBase(string nameConnectionString)
        {
            db = new OperationManagerDbContextBase<T>(nameConnectionString);
        }


        //public List<T> load(T tc)
        //{
        //    Type t = tc.GetType();
        //    string orderNum = t.GetProperty("OrderNum").Name;
        //    //  PropertyInfo pi = t.GetProperties().Contains(("OderNum");

        //    //foreach (PropertyInfo pi in t.GetProperties())
        //    //{
        //    //    orderNum = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作
        //    //                       //获得属性的类型,进行判断然后进行以后的操作,例如判断获得的属性是整数
        //    //                       //if (value1.GetType() == typeof(int))
        //    //                       //{
        //    //                       //    //进行你想要的操作
        //    //}

        //    // protected readonly string EsDataField = ORMapping.GetMappingInfo(typeof(T)).Single(p => p.PropertyName == "EffectStartTime").DataFieldName;
        //    List<T> list = db.DbContextBase.Where(s => orderNum == "asdf").ToList();
        //    return list;
        //}


        #region 添加、修改、删除
        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddOrUpdate(T entity)
        {

            db.DbContextBase.AddOrUpdate(entity);
            int result = db.SaveChanges();
            db.Dispose();
            return result;


        }

        /// <summary>
        /// 添加或更新集合
        /// </summary>
        /// <param name="entityCollection">集合</param>
        /// <returns></returns>
        public int AddOrUpdateCollection(TCollection entityCollection)
        {
            foreach (T item in entityCollection)
            {
                db.DbContextBase.AddOrUpdate(item);
            }
            int result = db.SaveChanges();
            db.Dispose();
            return result;
        }

        /// <summary>
        /// 异步添加或更新
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public async Task<int> AddOrUpdateAsync(T entity)
        {
            db.DbContextBase.AddOrUpdate(entity);
            int result = await db.SaveChangesAsync();
            db.Dispose();
            return result;
        }

        /// <summary>
        /// 异步添加或更新集合
        /// </summary>
        /// <param name="entityCollection">集合</param>
        /// <returns></returns>
        public async Task<int> AddOrUpdateAsyncCollection(TCollection entityCollection)
        {
            foreach (T item in entityCollection)
            {
                db.DbContextBase.AddOrUpdate(item);
            }
            int result = await db.SaveChangesAsync();
            db.Dispose();
            return result;
        }

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="entityCollection">集合</param>
        /// <returns></returns>
        public int RemoveCollection(TCollection entityCollection)
        {
            foreach (var item in entityCollection)
            {
                db.DbContextBase.Attach(item);
                db.DbContextBase.Remove(item);
            }
            int result = db.SaveChanges();
            db.Dispose();
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int Remove(T entity)
        {
            db.DbContextBase.Attach(entity);
            db.DbContextBase.Remove(entity);
            int result = db.SaveChanges();
            db.Dispose();
            return result;
        }

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(T entity)
        {
            db.DbContextBase.Attach(entity);
            db.DbContextBase.Remove(entity);
            int result = await db.SaveChangesAsync();
            db.Dispose();
            return result;
        }

        /// <summary>
        /// 异步删除集合
        /// </summary>
        /// <param name="entityCollection">集合</param>
        /// <returns></returns>
        public async Task<int> RemoveAsyncCollection(TCollection entityCollection)
        {
            foreach (T item in entityCollection)
            {
                db.DbContextBase.Attach(item);
                db.DbContextBase.Remove(item);
            }
            int result = await db.SaveChangesAsync();
            db.Dispose();
            return result;
        }




        //public virtual IEnumerable<T> GetLst(string orderNum)
        //{
        //    if (orderNum != string.Empty)
        //        return GetSet()(orderNum);
        //    else
        //        return null;
        //}
        #endregion

        public virtual T GetCode(int code)
        {
            if (code != 0)
                return GetSet().Find(code);
            else
                return null;
        }

        public virtual T GetCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
                return GetSet().Find(code);
            else
                return null;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GetSet();
        }

        IDbSet<T> GetSet()
        {

            return db.Set<T>();
        }


    }
}
