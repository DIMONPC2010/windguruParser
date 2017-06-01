using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WindguruParser
{
    class SharedRes
    {
        public static DateTime updateTime;
        public static Mutex mtx = new Mutex();
    }
}
