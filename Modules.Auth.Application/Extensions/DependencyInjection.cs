﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        }
    }
}
