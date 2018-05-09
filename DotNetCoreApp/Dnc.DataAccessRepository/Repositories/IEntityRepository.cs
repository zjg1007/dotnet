using Dnc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dnc.DataAccessRepository.Repositories
{
    public interface IEntityRepository
    {
        /// <summary>
        /// 持久化数据
        /// </summary>
        void Save();

        /// <summary>
        /// 无限制提取所有业务对象
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll<T>() where T : class, IEntity, new();

        /// <summary>
        /// 除了提取本身的对象数据集合外，还提取包含根据表达式提取关联的的对象的集合
        /// </summary>
        /// <param name="includeProperties">
        /// 需要直接提取关联类集合数据的表达式集合，通过逗号隔开，例如：
        /// var boCollection = _DbService.GetAll<Article>(x=>x.ArticleType);
        /// </param>
        /// <returns></returns>
        IQueryable<T> GetAll<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IEntity, new();

        /// <summary>
        /// 根据对象的ID提取具体的对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetSingle<T>(Guid id) where T : class, IEntity, new();
        T GetSingle<T>(Guid id, params Expression<Func<T, object>>[] includeProperties) where T : class, IEntity, new();

        /// <summary>
        /// 根据 Lambda 表达式提取具体的对象，实际上是提取满足表达式限制的集合的第一个对象集合
        /// </summary>
        /// <param name="predicate">布尔条件的 Lambda 表达式</param>
        /// <returns></returns>
        T GetSingleBy<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new();
        T GetSingleBy<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, IEntity, new();

        /// <summary>
        /// 根据 Lambda 表达式提取对象集合
        /// </summary>
        /// <param name="predicate">布尔条件的 Lambda 表达式</param>
        /// <returns></returns>
        IQueryable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity, new();

        /// <summary>
        /// 添加对象到内存中的数据集中
        /// </summary>
        /// <param name="entity"></param>
        void Add<T>(T entity) where T : class, IEntity, new();

        /// <summary>
        /// 添加对象到内存中的数据集中，并直接持久化。
        /// </summary>
        /// <param name="entity"></param>
        void AddAndSave<T>(T entity) where T : class, IEntity, new();

        /// <summary>
        /// 编辑内存中对应的数据集的对象
        /// </summary>
        /// <param name="entity"></param>
        void Edit<T>(T entity) where T : class, IEntity, new();

        /// <summary>
        /// 编辑内存中对应的数据集的对象，并直接持久化。
        /// </summary>
        /// <param name="entity"></param>
        void EditAndSave<T>(T entity) where T : class, IEntity, new();

        /// <summary>
        /// 根据内存中对应的数据集是否存在，自动决定采取添加或者编辑方法处理传入的对象
        /// </summary>
        /// <param name="entity"></param>
        void AddOrEdit<T>(T entity) where T : class, IEntity, new();

        /// <summary>
        /// 根据内存中对应的数据集是否存在，自动决定采取添加或者编辑方法处理传入的对象，并直接持久化。
        /// </summary>
        /// <param name="entity"></param>
        void AddOrEditAndSave<T>(T entity) where T : class, IEntity, new();

        /// <summary>
        /// 删除内存中对应的数据集的对象。
        /// </summary>
        /// <param name="entity"></param>
        void Delete<T>(T entity) where T : class, IEntity, new();

        /// <summary>
        /// 删除内存中对应的数据集的对象，并直接持久化。
        /// </summary>
        /// <param name="entity"></param>
        void DeleteAndSave<T>(T entity) where T : class, IEntity, new();
    }
}
