using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Bll.MemberManage;
using lyw.blueunion.backstagesystem.Filters;
namespace lyw.blueunion.backstagesystem.Controllers
{
    [CheckLoginFilter]
    public class MemberManageController : Controller
    {
        MemberManageBll memberbll = new MemberManageBll();
        string username = "";
        string ip = "";
        //
        // GET: /MemberManage/
        /// <summary>
        /// 个人会员列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberList()
        {
            return View();
        }
        /// <summary>
        /// 企业会员列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberListEnterprise()
        {
            return View();
        }
        /// <summary>
        /// 会员单位列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberListUnit()
        {
            return View();
        }
        /// <summary>
        /// 自由职业列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberListProfessional()
        {
            return View();
        }
       /// <summary>
       /// 会员信息列表
       /// </summary>
       /// <param name="memberName"></param>
       /// <param name="registerTime"></param>
       /// <param name="nowpage"></param>
       /// <param name="pagesize"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       [CustomerResource("MemberListData","6")]
        public ActionResult MemberListData(string memberName, string registerTime, string nowpage, string pagesize,string type)
        {
            string responseText = memberbll.memberListData(memberName,registerTime,nowpage,pagesize,type);
            return Content(responseText);
        }
        /// <summary>
        /// 企业会员信息列表
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="registerTime"></param>
        /// <param name="nowpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="type"></param>
        /// <returns></returns>
         [CustomerResource("EnterMemberListData","8")]
        public ActionResult EnterMemberListData(string memberName, string registerTime, string nowpage, string pagesize, string type)
        {
            string responseText = memberbll.memberListData(memberName, registerTime, nowpage, pagesize, type);
            return Content(responseText);
        }
        /// <summary>
        /// 自由职业信息列表
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="registerTime"></param>
        /// <param name="nowpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="type"></param>
        /// <returns></returns>
          [CustomerResource("ProfessionMemberListData","10")]
        public ActionResult ProfessionMemberListData(string memberName, string registerTime, string nowpage, string pagesize, string type)
        {
            string responseText = memberbll.memberListData(memberName, registerTime, nowpage, pagesize, type);
            return Content(responseText);
        }
        /// <summary>
        /// 详细的个人会员信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public ActionResult MemberDetailPersonalInfo(string user_id)
        {
            string responseText = memberbll.detailpersonalInfo(user_id);
            return Content(responseText);
        }
        /// <summary>
        /// 详细的企业会员信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public ActionResult MemberDetailEnterpriseInfo(string user_id)
        {
            string responseText = memberbll.detailEnterpriseInfo(user_id);
            return Content(responseText);
        }
        /// <summary>
        /// 详细的自由职业信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public ActionResult MemberDetailProfessionalInfo(string user_id, string profession)
        {
            string responseText = memberbll.detailProfessionalInfo(user_id,profession);
            return Content(responseText);
        }
        /// <summary>
        /// 详细的会员单位信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public ActionResult MemberDetailMemberUnitInfo(string user_id)
        {
            string responseText = memberbll.detailMemberUnitInfo(user_id);
            return Content(responseText);
        }
      
        /// <summary>
        /// 删除个人会员
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
         [CustomerResource("MemberDeleteMember","7")]
        public ActionResult MemberDeleteMember(string user_id)
        {
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            string responseText = memberbll.delMember(user_id,username,ip);
            return Content(responseText);
        
        }
        /// <summary>
        /// 删除企业会员
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
          [CustomerResource("EnterMemberDeleteMember","9")]
        public ActionResult EnterMemberDeleteMember(string user_id)
        {
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            string responseText = memberbll.delEnterMember(user_id, username, ip);
            return Content(responseText);

        }
        /// <summary>
        /// 删除自由职业
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
          [CustomerResource("ProfessionMemberDeleteMember","11")]
          public ActionResult ProfessionMemberDeleteMember(string user_id, string profession)
        {
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            string responseText = memberbll.delProfessionMember(user_id,profession, username, ip);
            return Content(responseText);

        }
        /// <summary>
        /// 审核自由职业是否注册成功
        /// </summary>
        /// <param name="check_id"></param>
        /// <returns></returns>
         [CustomerResource("ProfessionMemberCheck", "18")]
          public ActionResult ProfessionMemberCheck(string check_id, string user_id, string profession)
          {
              if (Request.Cookies["ip"] != null)
                  ip = Request.Cookies["ip"].Value;
              if (Request.Cookies["userName"] != null)
                  username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
              string responseText = memberbll.professionMemberCheck(check_id,user_id,profession,username,ip);
              return Content(responseText);
          }
        /// <summary>
        /// 会员分类管理视图
        /// </summary>
        /// <returns></returns>
         [CustomerResource("MemberRankManage","17")]
        public ActionResult MemberRankManage()
        {
            return View();
        }
        /// <summary>
        /// 获取各等级数据进行饼状图分析
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberRankCountToPie()
        {
            string responseText = memberbll.memberRankCountToPie();
            return Content(responseText);
        }
        /// <summary>
        /// 获取各等级新增数据进行折线/柱状图分析
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberRankCountToLine()
        {
            string responseText = "";
            responseText = memberbll.memberRankCountToLine();
            return Content(responseText);
        }
        /// <summary>
        /// 统计会员分类数据
        /// </summary>
        /// <returns></returns>
        public ActionResult StasticCount()
        {
            string responseText = "";
            responseText = memberbll.stasticCount();
            return Content(responseText);
        }

    }
}
