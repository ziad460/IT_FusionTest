using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITFusionTask.API.ApiServicesClasses
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userName);
    }
}
