﻿// This file is part of Hangfire.
// Copyright © 2016 Sergey Odinokov.
// 
// Hangfire is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as 
// published by the Free Software Foundation, either version 3 
// of the License, or any later version.
// 
// Hangfire is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public 
// License along with Hangfire. If not, see <http://www.gnu.org/licenses/>.

using System;
using Hangfire.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.Dashboard
{
    public sealed class AspNetCoreDashboardContext : DashboardContext
    {
        public AspNetCoreDashboardContext(
            [NotNull] JobStorage storage,
            [NotNull] DashboardOptions options,
            [NotNull] HttpContext httpContext) 
            : base(storage, options)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            HttpContext = httpContext;
            Request = new AspNetCoreDashboardRequest(httpContext);
            Response = new AspNetCoreDashboardResponse(httpContext);
        }

        public HttpContext HttpContext { get; }

        public override IBackgroundJobClient GetBackgroundJobClient()
        {
            return HttpContext.RequestServices.GetService<IBackgroundJobClient>() ?? base.GetBackgroundJobClient();
        }

        public override IRecurringJobManager GetRecurringJobManager()
        {
            return HttpContext.RequestServices.GetService<IRecurringJobManager>() ?? base.GetRecurringJobManager();
        }
    }
}
