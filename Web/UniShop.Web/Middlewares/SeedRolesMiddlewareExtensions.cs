﻿using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniShop.Web.Middlewares
{
   
        public static class SeedRolesMiddlewareExtensions
        {
            public static IApplicationBuilder UseSeedRolesMiddleware(
               this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<SeedRolesMiddleware>();
            }
        }
    
}
