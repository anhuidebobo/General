using General.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace General.Mvc
{
    public class GeneralEngine : IEngine
    {
        private IServiceProvider _serviceProvider;
        public GeneralEngine(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// 实例
        /// </returns>
        /// 创建者：libb
        /// 创建日期：2019/5/10 11:04
        /// 修改者：
        /// 修改时间：
        /// -----------------------------------------------------------
        /// 创建者：libb
        /// 创建日期：2019/5/10 10:31
        /// 修改者：
        /// 修改时间：
        /// -----------------------------------------------------------
        public T Resolve<T>() where T : class
        {
            //GetService是扩展方法
            return _serviceProvider.GetService<T>();
        }
    }
}
