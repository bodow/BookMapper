using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcel.DataClasses
{
    interface ILogger
    {
        bool HasErrors { get;  }
        string ErrorMessage { get; }
    }
}
