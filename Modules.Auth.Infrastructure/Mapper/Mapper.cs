using Modules.Auth.Domain.Entities;
using Shared.DTOs.Features.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Infrastructure.Mapper
{
    public static class Mapper
    {
        public static Tbl_User Map(this RegisterRequestModel requestModel)
        {
            return new Tbl_User
            {
                UserId = Ulid.NewUlid().ToString(),
                UserName = requestModel.UserName,
                Email = requestModel.Email,
                Password = requestModel.Password,
                UserRole = requestModel.UserRole,
                IsActive = true
            };
        }
    }
}
