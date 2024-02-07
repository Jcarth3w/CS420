using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebRazor.Middleware
{
    public class MyAuth
    {
        private readonly RequestDelegate _next;

        public MyAuth(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Query["username"] == "user1" && context.Request.Query["password"] == "password1")
            {
                context.Request.HttpContext.Items.Add("userdetails", "Authorized");
                await _next(context);
            }
            else{
                await context.Response.WriteAsync("Not authorized");
            }
        }
    }
}