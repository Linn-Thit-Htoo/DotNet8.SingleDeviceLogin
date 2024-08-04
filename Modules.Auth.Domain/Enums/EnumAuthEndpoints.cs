using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Domain.Enums
{
    public enum EnumAuthEndpoints
    {
        [Description("/api/account/login")]
        Login,

        [Description("/api/account/register")]
        Register
    }
}
