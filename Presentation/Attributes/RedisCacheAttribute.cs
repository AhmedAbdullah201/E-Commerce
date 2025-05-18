using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    internal class RedisCacheAttribute(int durationInSeconds = 90)
        : ActionFilterAttribute
    {
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var service = context.HttpContext.RequestServices.GetRequiredService<ICashService>();

            var key = GetCacheKey(context.HttpContext.Request);

            var cashValue = await service.GetCashAsync(key);

            if (cashValue != null)
            {
                context.Result = new ContentResult()
                {
                    Content = cashValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var excutedContext = await next.Invoke(); //excute action

            if (excutedContext.Result is ObjectResult objectResult)
                await service.SetCashAsync(key, objectResult.Value, TimeSpan.FromSeconds(durationInSeconds));

        }
        private static string GetCacheKey(HttpRequest request)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(request.Path + '?');

            foreach (var item in request.Query.OrderBy(x => x.Key))
                sb.Append($"{item.Key}={item.Value}&");

            return sb.ToString().TrimEnd('&');
        }
    }
}
