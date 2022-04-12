using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Security.Principal;
using System.Web.Security;

using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using System.Data.Entity;
using Nop.Data;

namespace Nop.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Khởi tạo ứng dụng: Đăng ký các dịch vụ sẽ sử dụng trên hệ thống
        /// </summary>
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            // Database.SetInitializer<NopObjectContext>(new DropCreateDatabaseAlways<NopObjectContext>());
            //initialize engine context
            EngineContext.Initialize(false);
        }



        /// <summary>
        /// Khởi tạo phiên làm việc mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_Start(object sender, EventArgs e)
        {
            Session[SessionConst.SESSION_START] = Guid.NewGuid().ToString();
        }


        /// <summary>
        /// Tắt phiên làm việc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_End(object sender, EventArgs e)
        {
            Session.Remove(SessionConst.SESSION_START);
        }
    }
}
