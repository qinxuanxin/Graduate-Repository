using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Model.ResultBase;
using lyw.blueunion.backstagesystem.Bll.Authority;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace lyw.blueunion.backstagesystem.Filters
{
    public class CheckPermissionFilter : IAuthorizationFilter
    {
        AuthorityBll authorbll = new AuthorityBll();
        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
              string rooturl= "http://egov.jinyuc.com/gxdzbackstage/system/";
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.HttpContext.Request.Url == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            if (controllerName.Equals("Login"))
            {
                return;
            }
            string pageUrl = filterContext.HttpContext.Request.Url.AbsolutePath; //OperateContext.GetThisPageUrl(false);
            // 是否是Ajax请求
            var bAjax = filterContext.HttpContext.Request.IsAjaxRequest();
            // 1、允许匿名访问 用于标记在授权期间要跳过 AuthorizeAttribute 的控制器和操作的特性 
            var actionAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true) as IEnumerable<AllowAnonymousAttribute>;
            var controllerAnonymous = filterContext.Controller.GetType().GetCustomAttributes(typeof(AllowAnonymousAttribute), true) as IEnumerable<AllowAnonymousAttribute>;
            if ((actionAnonymous != null && actionAnonymous.Any()) || (controllerAnonymous != null && controllerAnonymous.Any()))
            {
                return;
            }
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["userName"];
            // 判断是否登录或登录已超时 需要重新登录
            if (cookie == null || cookie.Value == "")
            {
                if (bAjax)
                {
                    BusinessResultBase result = new BusinessResultBase();
                    result.Title = "未登录或登录已超时";
                    result.Status = false;
                    result.StatusCode = "234";
                    result.StatusMessage = "请重新登录系统。";

                    var jsonResult = new JsonResult();
                    jsonResult.Data = result;
                    filterContext.Result = jsonResult;
                }
                else
                {
                  //  filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", action = "LoginIndex" }));
                ContentResult Content1 = new ContentResult();
                       Content1.Content = "<script type=\"text/javascript\">window.parent.location.href=\"" + rooturl + "login/loginindex\"</script>";
                       //执行结果为权限不通过  
                       filterContext.Result = Content1;
                 
                }
            }
            else
            {
                // 2、拒绝某个账号登录当前系统
                if (cookie.Name=="123")
                {
                    if (bAjax)
                    {
                        BusinessResultBase result = new BusinessResultBase();
                        result.Title = "拒绝访问当前系统";
                        result.Status = false;
                        result.StatusCode = "404";
                        result.StatusMessage = "您的账号不允许访问当前系统。";
                        var jsonResult = new JsonResult();
                        jsonResult.Data = result;
                        filterContext.Result = jsonResult;
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Home", action = "Error", bAjaxReq = false, message = "没有获取您拥有的权限菜单，请尝试重新登录。" }));
                    }
                }
                else
                {
                    // 3、判断登录状态 Controller  Action 标签 某些功能只需判断是否登录 用户没登录 调到登录页面  
                    // 判断Controller上是否有CheckLoginAttribute标签 只需要登录就可以访问
                    var checkLoginControllerAttr = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(CheckLoginFilter), true) as IEnumerable<CheckLoginFilter>;
                    var attNames= filterContext.ActionDescriptor.GetCustomAttributes(typeof(CustomerResourceAttribute), true) as IEnumerable<CustomerResourceAttribute>;
                    var customeractionname = "";
                    var customeractionid = "";
                    foreach (CustomerResourceAttribute cus in attNames)
                    {
                        customeractionname = cus.ResourceName;
                        customeractionid = cus.ResourceId;
                    }
                    if ((checkLoginControllerAttr != null && checkLoginControllerAttr.Any()) && (customeractionname==""))
                    {
                        return;
                    }
                    // 判断action上是否有CheckLoginAttribute标签 只需要登录就可以访问
                    var checkLoginActionAttr = filterContext.ActionDescriptor.GetCustomAttributes(typeof(CheckLoginFilter), true) as IEnumerable<CheckLoginFilter>;
                    if (checkLoginActionAttr != null && checkLoginActionAttr.Any())
                    {
                        return;
                    }
                    // 4、有些要判断是否有某个菜单 action的权限 具体判断某个用户是否有某个权限
                    // 用于标记在授权期间需要CustomerResourceAttribute 的操作的特性
                    //var attNames = filterContext.ActionDescriptor.GetCustomAttributes(typeof(CustomerResourceAttribute), true) as IEnumerable<CustomerResourceAttribute>;
                    // 用户具有的菜单
                    HttpCookie cookieadminid = System.Web.HttpContext.Current.Request.Cookies["ADMIN_ID"];
                    var moduleList = authorbll.VerificationAuthority(cookieadminid.Value.ToString());
                    if (moduleList == null || !moduleList.Any())
                    {
                        if (bAjax)
                        {
                            BusinessResultBase result = new BusinessResultBase();
                            result.msg = "fail";
                            result.status = "没有访问权限";
                            result.Title = "没有访问权限";
                            result.Status = false;
                            result.StatusCode = "206";
                            result.StatusMessage = "没有获取您拥有的权限菜单，请尝试重新登录。";
                            var jsonResult = new JsonResult();
                            jsonResult.Data = result;
                            filterContext.Result = jsonResult;
                          
                        }
                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Home", action = "Error", bAjaxReq = false, message = "没有获取您拥有的权限菜单，请尝试重新登录。" }));
                        }
                    }
                    else
                    {
                        // 判断用户的权限菜单中的code是否与控制器上标示的资源的code一致
                        //var joinResult = (from aclEntity in moduleList
                        //                  join attName in attNames on aclEntity.Code equals attName.ResourceName
                        //                  select attName).Any();
                    

                        JObject jo = (JObject)JsonConvert.DeserializeObject(moduleList);
                        string roleid = jo["AUTHORITY"].ToString();
                        string[] role = roleid.Split(',');
                        bool hasflag = false;
                        foreach (string i in role)
                        {
                            if (i.Equals(customeractionid))
                            {
                                hasflag = true;
                                break;
                            }
                           
                        }
                        if (!hasflag)
                        {

                            if (bAjax)
                            {
                                BusinessResultBase result = new BusinessResultBase();
                                result.msg = "fail";
                                result.status = "没有访问权限";
                                result.Title = "没有访问权限";
                                result.Status = false;
                                result.StatusCode = "206";
                                result.StatusMessage = "您没有访问权限：" + pageUrl;
                                var jsonResult = new JsonResult();
                                jsonResult.Data = result;
                                filterContext.Result = jsonResult;
                            }
                            else
                            {
                                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Home", action = "Error", bAjaxReq = false, message = "您没有访问权限：" + pageUrl }));
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}