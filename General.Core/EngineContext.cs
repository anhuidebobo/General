using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace General.Core
{
    public class EngineContext
    {
        private static IEngine _engine;

        //https://www.cnblogs.com/acles/p/6556479.html
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initial(IEngine engine)
        {
            if (_engine == null)
                _engine = engine;
            return _engine;
        }

        public static IEngine Current
        {
            get
            {
                return _engine;
            }
        }
    }
}
