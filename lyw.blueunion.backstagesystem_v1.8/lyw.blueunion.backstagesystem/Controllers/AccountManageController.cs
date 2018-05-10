using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Model.Login;
using lyw.blueunion.backstagesystem.Bll.AccountManage;
using System.IO;
using lyw.blueunion.backstagesystem.Filters;
namespace lyw.blueunion.backstagesystem.Controllers
{
    [CheckLoginFilter]
    public class AccountManageController : Controller
    {

        AccountManageBll accountbll = new AccountManageBll();
        string username = "";
        string ip = "";


        //
        // GET: /AccountManage/

        /// <summary>
        /// 账号注册视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountRegister()
        {
            return View();
        }
        /// <summary>
        /// 账号注册请求行为
        /// </summary>
        /// <param name="adminmodel"></param>
        /// <returns></returns>

           [CustomerResource("AccountSaveRegister","2")]
        public ContentResult AccountSaveRegister(Login_AdminModel adminmodel)
        {
            string fileExt = "";
            List<string> filename = new List<string>();
            string filenames = "";
            bool bl;
            string response;
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            /****************上传图片到服务器****************/
            //int cnt = System.Web.HttpContext.Current.Request.Files.Count;
            //if (cnt == 0)
            //{
            //    filenames = "";
            //}
            //for (int i = 0; i < cnt; i++)
            //{
            //    HttpPostedFile hpf = System.Web.HttpContext.Current.Request.Files[i];
            //    filenames = Path.GetFileName(hpf.FileName);
            //    fileExt = Path.GetExtension(hpf.FileName).ToLower();//带.的后缀
            //    filename.Add(filenames);
            //    string fileFilt = ".jpg|.png|.JPG|.PNG|......";
            //    if ((fileFilt.IndexOf(fileExt) <= -1) || (fileExt == "") || (hpf.ContentLength > 4 * 1024 * 1024))
            //        continue; 
            ////    D:\MVCRoot\blueunion\uploadimages\Head
            //    hpf.SaveAs("D:\\MVCRoot\\gxdzbackstage\\image\\Head\\" + filenames);
            //    //hpf.SaveAs("D:\\MVCRoot\\blueunion\\uploadimages\\Head\\" + filenames);
            //}
            /****************上传图片到服务器****************/
            bl = accountbll.AccountRegister(adminmodel, filenames,username,ip);
            if (bl)
            {
                response = "{\"msg\":\"success\",\"status\":\"注册账号成功\"}";
            }
            else response = "{\"msg\":\"fail\",\"status\":\"注册账号失败\"}";

            return Content(response);
        }
        /// <summary>
        /// 账号编辑删除视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountEdit()
        {
            return View();
        }
        /// <summary>
        /// 账号修改视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountModify()
        {
            string guid = Request["guid"];

            Login_AdminModel usfo = new Login_AdminModel();
            usfo = accountbll.AccountEdit(guid);
           
            return View(usfo);
        }
        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="adminmodel"></param>
        /// <returns></returns>
          [CustomerResource("AccountSaveModify","3")]
        public ContentResult AccountSaveModify(Login_AdminModel adminmodel)
        {
            string response = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            bool bl = accountbll.AccountEdit(adminmodel,username,ip);
            if (bl)
            {
                response = "{\"msg\":\"success\",\"status\":\"修改账号成功\"}";
            }
            else response = "{\"msg\":\"fail\",\"status\":\"修改账号失败\"}";
            return Content(response);
        }
        /// <summary>
        /// 删除账号
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
          [CustomerResource("AccountDelete","4")]
        public ContentResult AccountDelete(string guid)
        {
            // string police_name = Request["UserName"];
            string response = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            bool bl = false;
            bl = accountbll.AccountDelete(guid,username,ip);
            if (bl)
                response = "{\"msg\":\"success\",\"status\":\"删除账号成功\"}";
            else
                response = "{\"msg\":\"fail\",\"status\":\"删除账号失败\"}";
            return Content(response);
            //  return Json(stus,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 带条件的账号信息查询
        /// </summary>
        /// <returns></returns>
         [CustomerResource("SearchAccountInfo","5")]
        public ActionResult SearchAccountInfo(string admin_name, string admin_id)
        {
            string responseText = "";
            responseText = accountbll.SearchAccountInfo(admin_name, admin_id);
            return Content(responseText);
        }
        /// <summary>
        /// 账号权限分配视图
        /// </summary>
        /// <returns></returns>
        [CustomerResource("AccountAuthority","16")]
        public ActionResult AccountAuthority()
        {
            return View();
        }
        /// <summary>
        /// 权限查看视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountBrowseAuthority()
        {
            return View();
        }
        /// <summary>
        /// 获取所有权限描述
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountAuthorityExplain()
        {
            string responseText = "";
            responseText = accountbll.accountAuthorityExplain();
            return Content(responseText);
        }
     /// <summary>
        /// 获取角色所拥有的权限
     /// </summary>
     /// <param name="user_id"></param>
     /// <returns></returns>
        public ActionResult AccountHasAuthority(string user_id)
        {
            string responseText = "";
            responseText = accountbll.accountHasAuthority(user_id);
            return Content(responseText);

        }
        /// <summary>
        /// 保存设置的权限
        /// </summary>
        /// <param name="authority_id"></param>
        /// <param name="role_id"></param>
        /// <returns></returns>
        public ActionResult AccountSaveAuthority(string authority_id, string role_id)
        {
            string responseText = "";
            responseText = accountbll.accountSaveAuthority(authority_id, role_id);
            return Content(responseText);
        }
    }
}
