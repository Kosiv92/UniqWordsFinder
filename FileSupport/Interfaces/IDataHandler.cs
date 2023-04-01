using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSupport.Interfaces
{
    public interface IDataHandler
    {
        public Dictionary<string, int> HandleData();
    }
}
