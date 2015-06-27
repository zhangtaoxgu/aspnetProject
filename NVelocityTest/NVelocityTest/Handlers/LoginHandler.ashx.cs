using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NVelocityTest.Handlers
{
    /// <summary>
    /// LoginHandler 的摘要说明
    /// </summary>
    public class LoginHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string login = context.Request["Login"];
            if (string.IsNullOrEmpty(login))
            {
                HttpCookie cookie = context.Request.Cookies["name"];
                string userName;
                if (cookie == null)
                {
                    userName = "";
                }
                else
                {
                    userName = cookie.Value;
                }
                var data = new { UserName = userName };
                context.Response.Write(CommonHelper.RenderHtml("Login.html", data));
            }
            else
            {
                string userName = context.Request["UserName"];
                HttpCookie cookie = new HttpCookie("name", userName);
                cookie.Expires = DateTime.Now.AddDays(7);
                context.Response.SetCookie(cookie);
                context.Response.Write("登录成功！");
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