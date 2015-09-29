using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(big.tourzj.gov.cn.Startup))]

namespace big.tourzj.gov.cn
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //// 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/index"),
            });

        }
    }
}
