using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigFile.DLL;
using System.Linq.Expressions;

namespace BigFile.BLL
{
    public class BFBaseService<T> where T:class
    {
        BFBaseDBOption<T> _BFBaseDBOption;

        public BFBaseService()
        {
            _BFBaseDBOption = new BFBaseDBOption<T>();
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">增加的记录</param>
        /// <param name="IsSave">是否保存</param>
        /// <returns>返回增加的记录</returns>
        public T Add(T entity, bool IsSave = true)
        {

            return _BFBaseDBOption.Add(entity, IsSave);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">更新的记录</param>
        /// <param name="IsSave">是否保存</param>
        /// <returns>返回更新的记录</returns>
        public T UpDate(T entity, bool IsSave = true)
        {

            return _BFBaseDBOption.UpDate(entity, IsSave);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">需要删除的记录</param>
        /// <param name="IsSave">是否保存</param>
        public bool Delete(T entity, bool IsSave = true)
        {
            return _BFBaseDBOption.Delete(entity, IsSave);
        }

        /// <summary>
        /// 根据主键查找一条记录，请注意按照主键的顺序
        /// </summary>
        /// <param name="keyvalues">可以传入多个参数，不限制类型</param>
        /// <returns>T</returns>
        public T Find(params object[] keyvalues)
        {
            return _BFBaseDBOption.Find(keyvalues);
        }

        /// <summary>
        /// 根据条件查询一条记录
        /// </summary>
        /// <param name="lambda">Lambda表达式</param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> lambda)
        {
            return _BFBaseDBOption.Find(lambda);
        }

        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <param name="number">返回的记录数【0-返回所有】</param>
        /// <param name="whereLandba">查询条件</param>        
        /// <returns>数据集合</returns>
        public IQueryable<T> FindList(Expression<Func<T, bool>> whereLandba)
        {
            return _BFBaseDBOption.FindList(whereLandba);
        }

        public int Save()
        {
            return _BFBaseDBOption.Save();
        }
    }
}
