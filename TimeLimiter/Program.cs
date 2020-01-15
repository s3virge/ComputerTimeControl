using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimeLimiter
{
    class Program
    {
        static void Main(string[] args)
        {            
            new TimeLimiter().Start(args);           
        }
    }
}
