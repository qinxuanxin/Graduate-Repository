using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Model.ImageManage;
using lyw.blueunion.backstagesystem.Bll.ImageManage;
using System.IO;
using lyw.blueunion.backstagesystem.Filters;
namespace lyw.blueunion.backstagesystem.Controllers
{
    [CheckLoginFilter]
    public class ImageManageController : Controller
    {
        ImageManageBll imagebll = new ImageManageBll();
        string username = "";
        string ip = "";
        #region 广告管理
        //
        // GET: /ImageManage/
        /// <summary>
        /// 广告管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AdverManage()
        {
         
            return View();
        }
        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns></returns>
        public ActionResult AdverClassData()
        {
            string responseText = "";
            responseText = imagebll.adverClassData();
            return Content(responseText);
        }
        /// <summary>
        /// 获取带条件的广告位信息
        /// </summary>
        /// <param name="adverterm"></param>
        /// <returns></returns>
        [CustomerResource("AdverListData", "32")]
        public ActionResult AdverListData(AdverSearchTerm adverterm)
        {
            string responseText = "";
            responseText = imagebll.adverListData(adverterm);
            return Content(responseText);
        }
        /// <summary>
        /// 上传新图片
        /// </summary>
        /// <param name="addimage"></param>
        /// <returns></returns>
        [CustomerResource("AdverAddImage", "33")]
        public ActionResult AdverAddImage(AdverAddImage addimage)
        {
           
            string responseText = "";
            string fileExt = "";
            List<string> filename = new List<string>();
            string filenames = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            /****************上传图片到服务器****************/
            int cnt = System.Web.HttpContext.Current.Request.Files.Count;
            if (cnt == 0)
            {
                responseText= "[{\"msg\":\"fail\",\"status\":\"未能获取到图片，请重新上传\"}]";
                return Content(responseText);
            }
            for (int i = 0; i < cnt; i++)
            {
                HttpPostedFile hpf = System.Web.HttpContext.Current.Request.Files[i];
                 filenames = Path.GetFileName(hpf.FileName);
                fileExt = Path.GetExtension(hpf.FileName).ToLower();//带.的后缀
                filename.Add(filenames);
                string fileFilt = ".jpg|.png|.JPG|.PNG|......";
                if ((fileFilt.IndexOf(fileExt) <= -1) || (fileExt == "") || (hpf.ContentLength > 4 * 1024 * 1024))
                    continue;
              
             //   hpf.SaveAs("D:\\MVCRoot\\blueunion\\uploadimages\\Carouselfigure\\" + filenames);
           
                hpf.SaveAs("D:\\MVCRoot\\gxdzbackstage\\image\\Carouselfigure\\" + filenames);
               
            }
            /****************上传图片到服务器****************/
            responseText = imagebll.adverAddImage(addimage, filenames,username,ip);
            return Content(responseText);

        }

        /// <summary>
        /// 删除图片记录
        /// </summary>
        /// <param name="image_id"></param>
        /// <returns></returns>
         [CustomerResource("AdverDelImage", "35")]
        public ActionResult AdverDelImage(string image_id)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = imagebll.adverDelImage(image_id,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 设置图片状态
        /// </summary>
        /// <param name="image_id"></param>
        /// <returns></returns>
         [CustomerResource("AdverSetUpStatus", "34")]
        public ActionResult AdverSetUpStatus(string image_id)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = imagebll.adverSetUpStatus(image_id,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 各类型广告图片数量
        /// </summary>
        /// <returns></returns>
        public ActionResult AllClassCount()
        {
            string responseText = "";
            responseText = imagebll.allClassCount();
            return Content(responseText);
        }
        /// <summary>
        /// 分类视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AdverClass()
        {
            return View();
        }
        /// <summary>
        /// 获取广告位分类数据
        /// </summary>
        /// <param name="class_name"></param>
        /// <param name="add_time"></param>
        /// <param name="pagesize"></param>
        /// <param name="nowpage"></param>
        /// <returns></returns>
         [CustomerResource("AdverClassListData", "36")]
        public ActionResult AdverClassListData(string class_name, string adver_addtime_start, string adver_addtime_end, string pagesize, string nowpage)
        {
            string responseText = "";
            responseText = imagebll.adverClassListData(class_name, adver_addtime_start, adver_addtime_end, pagesize, nowpage);
            return Content(responseText);
        }
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="class_name"></param>
        /// <param name="clas_explain"></param>
        /// <returns></returns>
        [CustomerResource("AdverAddClass", "37")]
        public ActionResult AdverAddClass(string class_name, string class_explain)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = imagebll.adverAddClass(class_name, class_explain,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 删除分类记录
        /// </summary>
        /// <param name="class_id"></param>
        /// <returns></returns>
          [CustomerResource("AdverDelClass", "38")]
        public ActionResult AdverDelClass(string class_id)
        {

            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = imagebll.adverDelClass(class_id,username,ip);
            return Content(responseText);
        }
        #endregion

    }
}
