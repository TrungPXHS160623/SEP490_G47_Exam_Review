using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_Library.Common
{
    public class ResultResponse<T> : RequestResponse
    {
        public T Item { get; set; }

        public List<T> Items { get;set; }
    }
}
