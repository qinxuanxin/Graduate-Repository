using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Bll.Home;
using lyw.blueunion.backstagesystem.Bll.Log;
using lyw.blueunion.backstagesystem.Filters;
using System.IO;
using lyw.blueunion.backstagesystem.Bll.Authority;
using lyw.blueunion.backstagesystem.Utilities;
namespace lyw.blueunion.backstagesystem.Controllers
{

     [CheckLoginFilter]
    public class HomeController : Controller
    {
        HomeBll hbll = new HomeBll();
        LogBll lgbll = new LogBll();
        AuthorityBll authorbll = new AuthorityBll();
        //
        // GET: /Home/
        /// <summary>
        /// 主页视图
        /// </summary>
        /// <returns></returns>
        
        public ActionResult HomeIndex()
        {
          
            if (Request.Cookies["ADMIN_ID"] != null)
                ViewBag.adminid = Request.Cookies["ADMIN_ID"].Value;
            if (Request.Cookies["userName"] != null)
                ViewBag.username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
           
            return View();
        }
        /// <summary>
        /// 初始显示视图
        /// </summary>
        /// <returns></returns>
      
        public ActionResult Start()
        {
            return View();
        }
        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
       
        public ActionResult DataStatistics()
        {
            string responseText = "";

            return Content(responseText);
        }
        /// <summary>
        /// 会员总数量
        /// </summary>
        /// <returns></returns>
       
        public ActionResult AllCount()
        {
            string responseText = "";
            responseText = hbll.allCount();
            return Content(responseText);
        }
        /// <summary>
        /// 今天的新增的会员/订单数量
        /// </summary>
        /// <returns></returns>
       
        public ActionResult TodayCount()
        {
            string responseText = "";
            responseText = hbll.todayCount();
            return Content(responseText);
        }
        /// <summary>
        /// 个人资料视图
        /// </summary>
        /// <returns></returns>
       
        public ActionResult PersonalData()
        {
            return View();
        }
        /// <summary>
        /// 保存密码
        /// </summary>
        /// <param name="initsecret"></param>
        /// <param name="newsecret"></param>
        /// <returns></returns>
    //  [CheckLoginFilter(true)]
       //[UserAuthority(true)]
      
       [CustomerResource("SaveSecret","1")]
       
        public ActionResult SaveSecret(string initsecret, string newsecret)
        {
         
            string responseText = "";
            string guid="";
            string ip = "";
            string username = "";
          //  string info = authorbll.VerificationAuthority("SaveSecret");
           // bool flag = AuthorityHelper.validateAuthority(info);
          //  if (flag)
         //   {
                if (Request.Cookies["GUID"] != null)
                    guid = Request.Cookies["GUID"].Value;
                if (Request.Cookies["ip"] != null)
                    ip = Request.Cookies["ip"].Value;
                if (Request.Cookies["userName"] != null)
                    username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
                responseText = hbll.saveSecret(initsecret, newsecret, guid, username, ip);

                return Content(responseText);
         //   }
         //   else
         //   {
         //       return Content(responseText);
            
       //     }

               
            
           
        }
        /// <summary>
        /// 查询用户登录记录
        /// </summary>
        /// <returns></returns>
       
        public ActionResult SearchUserRecord(string nowpage, string pagesize)
        {
            string responseText = "";
            string guid = "";
            if (Request.Cookies["GUID"] != null)
                guid = Request.Cookies["GUID"].Value;

            responseText = hbll.searchUserRecord(guid,nowpage,pagesize);
            return Content(responseText);
        }
        /// <summary>
        /// 系统日志视图
        /// </summary>
        /// <returns></returns>
      
        public ActionResult OperationLog()
        {
            return View();
        }
        /// <summary>
        /// 请求操作日志
        /// </summary>
        /// <returns></returns>
      
        public ActionResult RequestOperationLog()
        {
            string responseText = "";
            responseText = hbll.requestOperationLog();
            return Content(responseText);
        }
        /// <summary>
        /// 请求所有的用户
        /// </summary>
        /// <returns></returns>
        
        public ActionResult RequestAllAccount()
        {
            string responseText = "";
            responseText = hbll.requestAllAccount();
            return Content(responseText);
        }
        /// <summary>
        /// 请求带条件的操作日志
        /// </summary>
        /// <param name="admin_name"></param>
        /// <param name="oper_time_start"></param>
        /// <param name="oper_time_end"></param>
        /// <param name="nowpage"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ActionResult RequestMoreOperationLog(string admin_name, string oper_time_start, string oper_time_end, string nowpage, string pagesize)
        {
            string responseText = "";
            responseText = hbll.requestMoreOperationLog(admin_name, oper_time_start, oper_time_end, nowpage, pagesize);
            return Content(responseText);
        }
        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
     
        public ActionResult Error(string ip)
        {
            string ss = ip;
          
          return View();
        }
        public ActionResult test()
        {
            return View();
        }
        
        public ActionResult holdfile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string folderpath = "/UploadFile/";//上传图片的文件夹
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(Server.MapPath(folderpath));
                }
                string ext1 = Path.GetExtension(file.FileName);
                if (ext1 != ".mp4" && ext1 != ".rmvb" && ext1 != ".avi" && ext1 != ".flv")//笔者这儿修改了后缀的判断
                {
                    return Json(new { statu = 201, msg = "文件格式不正确！" });
                }
                else
                {
                    string name = DateTime.Now.ToString("yyyyMMddHHmmssff");
                    string ext = Path.GetExtension(file.FileName);
                    string downpath = folderpath + name + ext;
                    string filepath = Server.MapPath(folderpath) + name + ext;
                    file.SaveAs(filepath);
                    return Json(new { statu = 200, src = downpath, id = name });
                }
            }
            else
            {
                return Json(new { statu = 202, msg = "请上传文件！" });
            }
        }

    }
}
