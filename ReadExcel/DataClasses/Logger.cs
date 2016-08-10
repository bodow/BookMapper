using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcel
{
    public class Logger
    {
        private StringBuilder sb = new StringBuilder();
        private bool hasErrors = false;

        public string ErrorMessage
        {
            get
            {
                return sb.ToString();
            }
        }
        public bool HasErrors { get { return hasErrors;  } }

        public void MessageClear()
        {
            sb.Clear();
            hasErrors = false;
        }
        public void MessageAdd(string msg, bool isError)
        {
            sb.AppendLine(msg);
            hasErrors = isError;
        }
        public void MessageAdd(string msg)
        {
            MessageAdd(msg, true);
        }

    }
}
