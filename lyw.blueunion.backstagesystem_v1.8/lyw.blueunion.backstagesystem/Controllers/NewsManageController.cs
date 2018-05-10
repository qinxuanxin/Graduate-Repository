using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Bll.NewsManage;
using lyw.blueunion.backstagesystem.Filters;
namespace lyw.blueunion.backstagesystem.Controllers
{
    [CheckLoginFilter]
    public class NewsManageController : Controller
    {
        NewsManageBll newsbll = new NewsManageBll();
        string username = "";
        string ip = "";
        // GET: /NewsManage/

        /// <summary>
        /// 短信事件视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShortMessage()
        {
            return View();
        }
        /// <summary>
        /// 查询所有事件列表
        /// </summary>
        /// <returns></returns>
          [CustomerResource("SmsEventListData", "39")]
        public ActionResult SmsEventListData()
        {
            string responseText = newsbll.smsEventListData();
            return Content(responseText);
        }
        /// <summary>
        /// 设置事件状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
           [CustomerResource("SetEventStatus", "40")]
        public ActionResult SetEventStatus(string status,string eventid)
        {
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            string responseText = newsbll.setEventStatus(status,eventid,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 所有sms模板
        /// </summary>
        /// <returns></returns>
        public ActionResult AllSmsTemplate()
        {
            string responseText = newsbll.allSmsTemplate();
            return Content(responseText);
        }
        /// <summary>
        /// 新建事件
        /// </summary>
        /// <param name="eventname"></param>
        /// <param name="eventtemplateid"></param>
        /// <returns></returns>
         [CustomerResource("NewEvent", "47")]
        public ActionResult NewEvent(string eventname, string eventtemplateid)
        {
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            string responseText = newsbll.newEvent(eventname, eventtemplateid,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
         [CustomerResource("DelEvent", "41")]
        public ActionResult DelEvent(string eventid)
        {
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            string responseText = newsbll.delEvent(eventid,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 短信模板视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShortMessageTemplate()
        {
            return View();
        }
        /// <summary>
        /// 查询所有短信模板
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
         [CustomerResource("SmsTemplateListData", "42")]
        public ActionResult SmsTemplateListData(string keyword)
        {
            string responseText = newsbll.smsTemplateListData(keyword);
            return Content(responseText);
        }
        /// <summary>
        /// 新建模板
        /// </summary>
        /// <param name="templatetheme"></param>
        /// <param name="templatecontent"></param>
        /// <returns></returns>
          [CustomerResource("NewTemplate", "44")]
        public ActionResult NewTemplate(string templateid, string templatetheme, string templatecontent)
        {
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            string responseText = newsbll.newTemplate(templateid, templatetheme, templatecontent,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 编辑模板
        /// </summary>
        /// <param name="templateid"></param>
        /// <param name="templatetheme"></param>
        /// <param name="templatecontent"></param>
        /// <returns></returns>
         [CustomerResource("EditTemplate", "43")]
        public ActionResult EditTemplate(string templateid, string templatetheme, string templatecontent)
        {
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            string responseText = newsbll.editTemplate(templateid, templatetheme, templatecontent,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 删除当前模板
        /// </summary>
        /// <param name="templateid"></param>
        /// <returns></returns>
         [CustomerResource("DelTemplate", "45")]
        public ActionResult DelTemplate(string templateid)
        {
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            string responseText = newsbll.delTemplate(templateid,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 短信记录视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShortMessageRecord()
        {
            return View();
        }
        /// <summary>
        /// 请求短信记录
        /// </summary>
        /// <param name="phone_number"></param>
        /// <param name="regtime_start"></param>
        /// <param name="regtime_end"></param>
        /// <returns></returns>
         [CustomerResource("RequestSmsRecord", "46")]
        public ActionResult RequestSmsRecord(string phone_number, string regtime_start, string regtime_end, string Pagesize, string Nowpage)
        {
            string responseText = newsbll.requestSmsRecord(phone_number,regtime_start,regtime_end,Pagesize,Nowpage);
            return Content(responseText);
        }
        
    }
}
