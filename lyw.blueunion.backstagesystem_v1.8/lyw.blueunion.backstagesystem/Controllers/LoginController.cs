using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Bll.Login;
using lyw.blueunion.backstagesystem.Model.Login;
using lyw.blueunion.backstagesystem.Utilities;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace lyw.blueunion.backstagesystem.Controllers
{
    public class LoginController : Controller
    {
        LoginBll lgBll = new LoginBll();
        //
        // GET: /Login/

        public ActionResult LoginIndex()
        {
            return View();
        }
        public ActionResult LoginCheckUser(string user_name, string user_password)
        {
            // string userName = Request["userName"];
            // string userPwd = Request["userPwd"];
          
          Login_AdminModel adminmodel=new Login_AdminModel();
          Login_Record record = new Login_Record();
           
          IpHelper iphel = new IpHelper();
          string ip = iphel.GetRealIP();
            string responseText = "";
            string address;
            address = iphel.GetPosition(ip);
            Response.Cookies["ip"].Value = ip;
            Response.Cookies["ip"].Expires = DateTime.Now.AddHours(2);
            Response.Cookies["address"].Value = address;
            Response.Cookies["address"].Expires = DateTime.Now.AddHours(2);
           
            adminmodel = lgBll.loginCheckUser(user_name, user_password);
            /*********记录当前登陆用户信息作日志*********/
             record.Ip = ip;
            record.Address = address;
            record.Guid = adminmodel.Guid;
            record.User_name = adminmodel.Admin_name;
            record.Admin_id = adminmodel.Admin_id;
            record.Sigin_time = DateTime.Now.ToString();
            
            /*********记录当前登陆用户信息作日志*********/
            if (adminmodel.bl)
            {
                record.Sigin_content = "登录成功";

                responseText = "[{\"msg\":\"success\",\"status\":\"" + adminmodel.backmessage + "\"}]";
                Response.Cookies["userName"].Value = System.Web.HttpContext.Current.Server.UrlEncode(adminmodel.Admin_name);
                Response.Cookies["userName"].Expires = DateTime.Now.AddHours(2);
                //  Response.Cookies["userName"].Expires = DateTime.Now.AddMinutes(1);

             
                Response.Cookies["ADMIN_ID"].Value = adminmodel.Admin_id;
                Response.Cookies["ADMIN_ID"].Expires = DateTime.Now.AddHours(2);
                Response.Cookies["GUID"].Value = adminmodel.Guid;
                Response.Cookies["GUID"].Expires = DateTime.Now.AddHours(2);





                //   Response.Cookies["Role_id"].Expires = DateTime.Now.AddMinutes(1);
                //ViewBag.welcome = police.Police_name;
                //ViewBag.author_id = police.Role_id;
            }
            else
            {
                responseText = "[{\"msg\":\"fail\",\"status\":\"" + adminmodel.backmessage + "\"}]";
                record.Sigin_content = "登录失败";
            }
            lgBll.loginRecord(record);//日志
            return Content(responseText);
        }
    }
}
