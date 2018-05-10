using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Utilities;
using lyw.blueunion.backstagesystem.Bll.Authority;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace lyw.blueunion.backstagesystem.Filters
{
    /// <summary>
    /// CheckLoginFilter
    /// 用于检测用户是否处于登录状态的标签
    /// 某些功能只需要用户登录就可以使用
    /// 
    /// 修改纪录
    /// 
    /// 2015-10-11 版本：1.0 创建文件。   
    /// 
    public class CheckLoginFilter : AuthorizeAttribute 
    {
        AuthorityBll authorbll = new AuthorityBll();
        string rooturl = "http://egov.jinyuc.com/gxdzbackstage/system/";
    
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool pass = false;
          
         
            try
            {
                
                // 获取url请求里的 controller 和 action
 
                    // 获取用户信息
                    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["userName"];
                    //     System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
                    //  filterContext.RequestContext.HttpContext.Session["userName"];
                    //  string rooturl = HttpContext.Current.Server.MapPath("~/");


                    if (cookie == null || cookie.Value == "")
                    {
                        httpContext.Response.StatusCode = 401;//无权限状态码
                        pass = false;

                    }
                    else
                    {
                        pass = true;
                    }
                    return pass;
  
            
            }
            catch
            {
                return pass;
            }

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 401)
            {
                ContentResult Content = new ContentResult();
                Content.Content = "<script type=\"text/javascript\">window.parent.location.href=\"" + rooturl + "login/loginindex\"</script>";
                //执行结果为权限不通过  
                filterContext.Result = Content;
             //   filterContext.Result = new RedirectResult("/Login/LoginIndex");
            }
        }

    }
}