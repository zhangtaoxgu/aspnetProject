using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NVelocityTest.Handlers
{
    /// <summary>
    /// CookieHandler 的摘要说明
    /// </summary>
    public class CookieHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string read = context.Request["Read"];
            if (!string.IsNullOrEmpty(read))
            {
                HttpCookie cookie = context.Request.Cookies["Age"];
                string value = cookie.Value;
                var data = new { Value = value };
                string html = CommonHelper.RenderHtml("CookieTest.html", data);
                context.Response.Write(html);
            }
            else if (!string.IsNullOrEmpty(context.Request["Write"]))
            {
                context.Response.SetCookie(new HttpCookie("Age", "22"));
                string html = CommonHelper.RenderHtml("CookieTest.html", null);
                context.Response.Write(html);
            }
            else
            {
                string html = CommonHelper.RenderHtml("CookieTest.html", null);
                context.Response.Write(html);
            }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}