using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace NVelocityTest
{
    public class CommonHelper
    {
        /// <summary>
        /// 用data数据填充templateName模板，渲染生成html返回
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string RenderHtml(string templateName, object data)
        {
            //第一步：Creating a VelocityEngine也就是创建一个VelocityEngine的实例
            VelocityEngine vltEngine = new VelocityEngine();   //也可以使用带参构造函数直接实例
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, System.Web.Hosting.HostingEnvironment.MapPath("~/templates"));//模板文件所在的文件夹
            vltEngine.Init();
            //vltEngine.AddProperty(RuntimeConstants.INPUT_ENCODING, "gb2312");
            //vltEngine.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "gb2312");

            //第二步：Creating the Template加载模板文件
            //这时通过的是Template类，并使用VelocityEngine的GetTemplate方法加载模板
            Template vltTemplate = vltEngine.GetTemplate(templateName);

            //第三步：Merging the template整合模板
            VelocityContext vltContext = new VelocityContext();
            vltContext.Put("Data", data);//设置参数，在模板中可以通过$data来引用

            //第四步：创建一个IO流来输出模板内容推荐使用StringWriter(因为template中以string形式存放)
            System.IO.StringWriter vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);

            string html = vltWriter.GetStringBuilder().ToString();
            return html;
        }
    }
}