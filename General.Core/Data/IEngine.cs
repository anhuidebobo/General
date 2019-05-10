using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Core
{
    public interface IEngine
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///实例
        /// </returns>
        /// 创建者：libb
        /// 创建日期：2019/5/10 10:31
        /// 修改者：
        /// 修改时间：
        /// -----------------------------------------------------------
        T Resolve<T>() where T : class;
    }
}
