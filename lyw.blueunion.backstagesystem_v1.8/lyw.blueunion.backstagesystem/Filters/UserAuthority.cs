using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Bll.Authority;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace lyw.blueunion.backstagesystem.Filters
{

    public class UserAuthority : ActionFilterAttribute
    {
        AuthorityBll authorbll = new AuthorityBll();
          public bool IsCheck = false;
          public UserAuthority(bool ischeck)
        {
        
          this.IsCheck = ischeck;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string rooturl1 = "http://egov.jinyuc.com/gxdzbackstage/system/";
            try
            {
                // 获取url请求里的 controller 和 action
                string controllerName = filterContext.RouteData.Values["controller"].ToString();
               string actionName = filterContext.RouteData.Values["action"].ToString();
               // string actionName = "SaveSecret";

               if (IsCheck)
               {
                   // 获取用户信息
                   HttpCookie cookieName = System.Web.HttpContext.Current.Request.Cookies["ADMIN_ID"];

                   string info = authorbll.VerificationAuthority(actionName);
                   JObject jo = (JObject)JsonConvert.DeserializeObject(info);
                   string roleid = jo["ADMIN_ID"].ToString();
                   string[] role = roleid.Split(',');
                   bool hasflag = false;
                   foreach (string i in role)
                   {
                       if (i.Equals(cookieName.Value))
                       {
                           hasflag = true;
                           break;
                       }
                       else
                       {
                           hasflag = false;
                       }
                   }
                   if (hasflag)
                   {
                       ContentResult Content1 = new ContentResult();
                       Content1.Content = "<script type=\"text/javascript\">window.parent.location.href=\"" + rooturl1 + "home/error\"</script>";
                       //执行结果为权限不通过  
                       filterContext.Result = Content1;

                   }


               }
              
          
            }
            catch
            {
                //filterContext.Result = new RedirectResult("/Lyw.Hz.Release/home/error");
                System.Web.HttpContext.Current.Response.Write("<script language=javascript>window.location.href='" + rooturl1 + "home/error'</script>");
                //        System.Web.HttpContext.Current.Response.Redirect("http://egov.jinyuc.com/gxdzwx/gxdzwxlogin");
            }

        }
    }
}