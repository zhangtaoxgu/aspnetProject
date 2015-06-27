using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NVelocityTest.Handlers
{
    /// <summary>
    /// HandlerEdit 的摘要说明
    /// </summary>
    public class HandlerEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string action = context.Request["action"];
            if (action == "AddNew")
            {
                //bool save = context.Request["name"];
                //int age = Convert.ToInt32(context.Request["Age"]);
                //string email = context.Request["Email"];
                //SqlHelper.ExecuteNonQuery("Insert into T_Persons(Name,Age,Email) values(@Name,@Age,@Email)", new SqlParameter("@Name", name)
                //        , new SqlParameter("@Age", age)
                //        , new SqlParameter("@Email", email));
                //context.Response.Redirect("PersonList.ashx");//保存成功返回列表页面

                bool save = Convert.ToBoolean(context.Request["save"]);
                if (save)
                {
                    string name = context.Request["name"];
                    string Addresses = context.Request["Addresses"];
                    string Sex = context.Request["Sex"];
                    string Tel = context.Request["Tel"];
                    SqlHelper.ExecuteNonQuery("insert into yonghu(name,addresses,sex,tel) values (@name,@addresses,@sex,@tel)", new SqlParameter("@name", name), new SqlParameter("@addresses", Addresses), new SqlParameter("@sex", Sex), new SqlParameter("@tel", Tel));
                    context.Response.Redirect("Handler1.ashx");
                }
                else
                {
                    var data = new { Action = "AddNew", Person = new { name = "", addresses = "武汉", sex = "男", tel = "4234242423" } };
                    string html = CommonHelper.RenderHtml("testEdit.html", data);
                    context.Response.Write(html);
                }

            }
            else if (action =="Edit")
            {
                bool save = Convert.ToBoolean(context.Request["save"]);
                if (save)
                {
                    int id = Convert.ToInt32(context.Request["Id"]);
                    //todo:检查id的合法性
                    string name = context.Request["name"];
                    string Addresses = context.Request["Addresses"];
                    string Sex = context.Request["Sex"];
                    string Tel = context.Request["Tel"];
                    SqlHelper.ExecuteNonQuery("update yonghu set name=@name,add=@add,sex=@sex,tel=@tel", new SqlParameter("@name", name), new SqlParameter("@add", Addresses), new SqlParameter("@sex", Sex), new SqlParameter("@tel", Tel));
                    context.Response.Redirect("Handler1.ashx");
                }
                else
                {
                    int id = Convert.ToInt32(context.Request["Id"]);
                    DataTable dt = SqlHelper.ExecuteDataTable("select * from yonghu where id = @id", new SqlParameter("@id", id));
                    if (dt.Rows.Count <= 0)
                    {
                        context.Response.Write("没有找到id=" + id + "的数据");
                    }
                    else if (dt.Rows.Count >1)
                    {
                        context.Response.Write("找到多条id=" + id + "的数据");
                    }
                    else
                    {
                        var data = new { Action = "Edit", Person = dt.Rows[0] };
                        string html = CommonHelper.RenderHtml("testEdit.html", data);
                        context.Response.Write(html);
                    }
                }
                
            }
            else
            {
                //int id = Convert.ToInt32(context.Request["Id"]);
                context.Response.Write("参数错误。");
            }

            //context.Response.Write("Hello World");
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