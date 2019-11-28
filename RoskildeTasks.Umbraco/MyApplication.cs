using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Umbraco
{
    public class MyApplication : Umbraco.Web.UmbracoApplication
    {
        protected override void OnApplicationStarting(object sender, EventArgs e)
        {
            base.OnApplicationStarting(sender, e);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var corsAttr = new EnableCorsAttribute("https://roskildetasks.netlify.com/", "*", "*");
            config.EnableCors(corsAttr);
        }
    }
}