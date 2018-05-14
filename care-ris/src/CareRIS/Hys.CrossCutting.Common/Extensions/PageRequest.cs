using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CrossCutting.Common
{
    public class PageRequest<T>
    {
        public DataSourceRequest Request { get; set; }
        public T Specify { get; set; }
    }
}
