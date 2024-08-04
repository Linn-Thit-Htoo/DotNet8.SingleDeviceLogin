using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Enums
{
    public enum EnumStatusCode
    {
        Success = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        UnAuthorized = 401,
        NotFound = 404,
        MethodNotAllowed = 405,
        Conflict = 409,
        Locked = 423,
        InternalServerError = 500
    }
}
