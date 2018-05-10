using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lyw.blueunion.backstagesystem.Dal.NewsManage;
namespace lyw.blueunion.backstagesystem.Bll.NewsManage
{
  public  class NewsManageBll
    {
      NewsManageDal newsdal = new NewsManageDal();
      
      /// <summary>
      /// 查询所有事件列表
      /// </summary>
      /// <returns></returns>
      public string smsEventListData()
      {
          string responseText = "";
          responseText = newsdal.smsEventListData();
          return responseText;
      }
      /// <summary>
      /// 设置事件状态
      /// </summary>
      /// <param name="status"></param>
      /// <returns></returns>
      public string setEventStatus(string status,string eventid,string username,string ip)
      {
          string responseText = "";
          responseText = newsdal.setEventStatus(status, eventid,username,ip);
          return responseText;
      }
      /// <summary>
      /// 所有sms模板
      /// </summary>
      /// <returns></returns>
      public string allSmsTemplate()
      {
          string responseText = "";
          responseText = newsdal.allSmsTemplate();
          return responseText;
      }

      /// <summary>
      /// 新建事件
      /// </summary>
      /// <param name="eventname"></param>
      /// <param name="eventtemplateid"></param>
      /// <returns></returns>
      public string newEvent(string eventname, string eventtemplateid,string username,string ip)
      {
          string responseText = "";
          responseText = newsdal.newEvent(eventname, eventtemplateid,username,ip);
          return responseText;
      }
      /// <summary>
      /// 删除事件
      /// </summary>
      /// <param name="eventid"></param>
      /// <returns></returns>
      public string delEvent(string eventid,string username,string ip)
      {
          string responseText = "";
          responseText = newsdal.delEvent(eventid,username,ip);
          return responseText;
      }
      /// <summary>
      /// 查询所有短信模板
      /// </summary>
      /// <param name="keyword"></param>
      /// <returns></returns>
      public string smsTemplateListData(string keyword)
      {
          string responseText = "";
          responseText = newsdal.smsTemplateListData(keyword);
          return responseText;
      }
      /// <summary>
      /// 新建模板
      /// </summary>
      /// <param name="templatetheme"></param>
      /// <param name="templatecontent"></param>
      /// <returns></returns>
      public string newTemplate(string templateid, string templatetheme, string templatecontent,string username,string ip)
      {
          string responseText = "";
          responseText = newsdal.newTemplate(templateid,templatetheme,templatecontent,username,ip);
          return responseText;
      }
      /// <summary>
      /// 编辑模板
      /// </summary>
      /// <param name="templateid"></param>
      /// <param name="templatetheme"></param>
      /// <param name="templatecontent"></param>
      /// <returns></returns>
      public string editTemplate(string templateid, string templatetheme, string templatecontent,string username,string ip)
      {
          string responseText = "";
          responseText = newsdal.editTemplate(templateid, templatetheme, templatecontent,username,ip);
          return responseText;
      }
      /// <summary>
      /// 删除当前模板
      /// </summary>
      /// <param name="templateid"></param>
      /// <returns></returns>
      public string delTemplate(string templateid,string username,string ip)
      {
          string responseText = "";
          responseText = newsdal.delTemplate(templateid,username,ip);
          return responseText;
      }
      /// <summary>
      /// 请求短信记录
      /// </summary>
      /// <param name="phone_number"></param>
      /// <param name="regtime_start"></param>
      /// <param name="regtime_end"></param>
      /// <param name="Pagesize"></param>
      /// <param name="Nowpage"></param>
      /// <returns></returns>
      public string requestSmsRecord(string phone_number, string regtime_start, string regtime_end, string Pagesize, string Nowpage)
      {
          string responseText = "";
          responseText = newsdal.requestSmsRecord(phone_number,regtime_start,regtime_end,Pagesize,Nowpage);
          return responseText;
      }

    }
}
