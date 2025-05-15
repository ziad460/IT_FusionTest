using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITFusionTask.Data.Dtos.API_Dtos
{
    public class GetAllEmployeesRequestRoot
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string? search { get; set; }
    }

}
