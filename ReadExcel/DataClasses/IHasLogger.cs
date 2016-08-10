using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcel.DataClasses
{
    interface IHasLogger
    {
        static bool HasErrors { get; }
        static string ErrorMessage { get; }
    }
}
