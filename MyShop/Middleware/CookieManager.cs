using Domain.Repository;
using Newtonsoft.Json;

namespace MCSteklo.Middleware
{
    public class CookieManage
    {
        private readonly RequestDelegate _next;


        public CookieManage(RequestDelegate next)
        {
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            var uidcookievalue = context.Request.Cookies["UId"] ?? Guid.NewGuid().ToString();
            CookieOptions uidoption = new CookieOptions();
            uidoption.Expires = DateTime.Now.AddDays(3);
            uidoption.IsEssential = true;
            uidoption.SameSite = SameSiteMode.Lax;
            context.Response.Cookies.Append("UId", uidcookievalue, uidoption);
            await _next.Invoke(context);
        }
    }
}