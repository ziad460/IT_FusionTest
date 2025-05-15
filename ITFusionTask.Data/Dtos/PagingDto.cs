using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITFusionTask.Data.Dtos
{
    public class PagingDto<T> where T : class
    {
        public List<T> ListItems { get; set; }

        public int TotalCountRows { get; set; }
    }
}
