using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using lyw.blueunion.backstagesystem.Model.Order;
using lyw.blueunion.backstagesystem.Utilities;
using lyw.blueunion.backstagesystem.Dal.Log;
namespace lyw.blueunion.backstagesystem.Dal.NewsManage
{
   public class NewsManageDal
    {
       LogDal lgdal = new LogDal();
       /// <summary>
       /// 查询所有的事件列表
       /// </summary>
       /// <returns></returns>
       public string smsEventListData()
       {
           string responseText = "";

           string sql = string.Format("select a1.SMS_EVENT_ID,a1.SMS_EVENT_NAME,a1.SMS_EVENT_STATUS,a2.SMS_TEMPLATE_ID,a2.SMS_THEME,a2.SMS_TEMPLATE_CONTENT  from GX_SMS_EVENT a1, GX_SMS_TEMPLATE a2 where a1.SMS_TEMPLATE_ID=a2.SMS_TEMPLATE_ID and a2.DEL_FLAG='0'");

           DataTable dt = OracleHelper.GetTable(sql, null);
           if(dt.Rows.Count==0)
               return "[{\"msg\":\"fail\",\"status\":\"查无数据\"}]";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"servers\":[" + responseText + "]}]";
           return responseText;

       }
       /// <summary>
       /// 设置事件状态
       /// </summary>
       /// <param name="status"></param>
       /// <returns></returns>
       public string setEventStatus(string status,string eventid,string username,string ip)
       {
           string responseText="";
           string log = "";
           bool logFlag = false;
           string sql = string.Format("update GX_SMS_EVENT set SMS_EVENT_STATUS ='{0}' where SMS_EVENT_ID='{1}'", status, eventid);
          int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if(flag>0)
           {
               logFlag = true;
               responseText = "[{\"msg\":\"success\",\"status\":\"修改成功\"}]";
           }
               
           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"修改失败,数据库没有正确更新\"}]";
           if (logFlag)
           {
               log = username + " 成功设置id为" + eventid+"的事件状态";
           }
           else
               log = username + " 失败设置id为" + eventid + "的事件状态";
           lgdal.setOperationLog(username,log, ip);
           return responseText;
       }
       //public string editEvent(string eventid,string eventname,string eventtemplateid,)
       //{ 
       
       //}
       /// <summary>
       /// 所有sms模板
       /// </summary>
       /// <returns></returns>
       public string allSmsTemplate()
       {
           string responseText = "";

           string sql = string.Format("select * from GX_SMS_TEMPLATE where DEL_FLAG='0'");

           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
               return "[{\"msg\":\"fail\",\"status\":\"查无数据\"}]";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"servers\":[" + responseText + "]}]";
           return responseText;
       }
       /// <summary>
       /// 新建事件
       /// </summary>
       /// <param name="eventid"></param>
       /// <param name="eventname"></param>
       /// <param name="eventtemplateid"></param>
       /// <returns></returns>
       public string newEvent(string eventname, string eventtemplateid,string username,string ip)
       {
           string responseText="";
           string log = "";
           bool logFlag = false;
           string guid=Guid.NewGuid().ToString("D");
           string oracle =string.Format("insert into GX_SMS_EVENT(SMS_EVENT_ID,SMS_EVENT_NAME,SMS_TEMPLATE_ID) values('{0}','{1}','{2}')",guid,eventname,eventtemplateid);
           int flag = OracleHelper.ExecuteNonQuery(oracle, null);
           if (flag > 0)
           {
               responseText = "[{\"msg\":\"success\",\"status\":\"新建成功\"}]";
               logFlag = true;
           }
             
           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"新建失败,数据库没有正确更新\"}]";
           if (logFlag)
           {
               log = username + " 成功新建名称为" + eventname+"的事件";
           }
           else
               log = username + " 失败新建名称为" + eventname + "的事件";
           lgdal.setOperationLog(username,log, ip);
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
           string log = "";
           bool logFlag = false;
           string sql = string.Format("delete from GX_SMS_EVENT where SMS_EVENT_ID='{0}'", eventid);
           int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               logFlag = true;
               responseText = "[{\"msg\":\"success\",\"status\":\"删除成功\"}]";
           }
              
           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"删除失败,数据库没有正确更新\"}]";

           if (logFlag)
           {
               log = username + " 成功删除id为" + eventid+"的事件";
           }
           else
               log = username + " 失败删除id为" + eventid + "的事件";
           lgdal.setOperationLog(username,log, ip);
           return responseText;

       }

       /// <summary>
       /// 查询所有短信模板
       /// </summary>
       /// <returns></returns>
       public string smsTemplateListData(string keyword)
       {
           string responseText = "";


           string sql = string.Format("SELECT * from GX_SMS_TEMPLATE where DEL_FLAG='0' and SMS_THEME LIKE '%{0}%'", keyword);
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
               return "[{\"msg\":\"fail\",\"status\":\"查无数据\"}]";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"servers\":[" + responseText + "]}]";
           responseText = responseText.Replace("\n", " ");
           responseText = responseText.Replace("\r", " ");
           responseText = responseText.Replace("\\", "/");

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
           string log = "";
           bool logFlag = false;
           string oracle = string.Format("insert into GX_SMS_TEMPLATE(SMS_TEMPLATE_ID,SMS_THEME,SMS_TEMPLATE_CONTENT) values('{0}','{1}','{2}')", templateid, templatetheme, templatecontent);
           int flag = OracleHelper.ExecuteNonQuery(oracle, null);
           if (flag > 0)
           {
               logFlag = true;
               responseText = "[{\"msg\":\"success\",\"status\":\"新建成功\"}]";
           }
               
           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"新建失败,数据库没有正确更新\"}]";
           if (logFlag)
           {
               log = username + " 成功新建id为" + templateid + "的模板";
           }
           else
               log = username + " 失败新建id为" + templateid + "的模板";
           lgdal.setOperationLog(username,log, ip);
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
           string log = "";
           bool logFlag = false;
           string sql = string.Format("update GX_SMS_TEMPLATE set SMS_THEME ='{0}',SMS_TEMPLATE_CONTENT='{1}' where SMS_TEMPLATE_ID='{2}'", templatetheme, templatecontent, templateid);
           int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               logFlag = true;
               responseText = "[{\"msg\":\"success\",\"status\":\"修改成功\"}]";
           }
              
           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"修改失败,数据库没有正确更新\"}]";
           if (logFlag)
           {
               log = username + " 成功修改id为" + templateid + "的模板";
           }
           else
               log = username + " 失败修改id为" + templateid + "的模板";
           lgdal.setOperationLog(username,log, ip);
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
           string log = "";
           bool logFlag = false;
           string sql = string.Format("update GX_SMS_TEMPLATE set DEL_FLAG ='1' where SMS_TEMPLATE_ID='{0}'", templateid);
           int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               logFlag = true;
               responseText = "[{\"msg\":\"success\",\"status\":\"删除成功\"}]";
           }
             
           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"删除失败,数据库没有正确更新\"}]";
           if (logFlag)
           {
               log = username + " 成功删除id为" + templateid + "的模板";
           }
           else
               log = username + " 失败删除id为" + templateid + "的模板";
           lgdal.setOperationLog(username,log, ip);
           return responseText;
       }
       /// <summary>
       /// 请求短信记录
       /// </summary>
       /// <param name="phone_number"></param>
       /// <param name="regtime_start"></param>
       /// <param name="regtime_end"></param>
       /// <returns></returns>
       public string requestSmsRecord(string phone_number, string regtime_start, string regtime_end,string Pagesize,string Nowpage)
       {
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数
           int pagesize = 0;
           int nowpage = 0;
           if (Pagesize == "" || Pagesize == null)
               pagesize = 10;
           else
               pagesize = Convert.ToInt32(Pagesize);

           if (Nowpage == "" || Nowpage == null)
               nowpage = 1;
           else
               nowpage = Convert.ToInt32(Nowpage);
           if (phone_number != "" && phone_number != null)
               Select_Sql += string.Format(" and (PHONE_NUM = '{0}')", phone_number);
           if (regtime_start != "" && regtime_start != null)

               Select_Sql += string.Format(" and to_char(to_date(SEND_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') >='{0}'", regtime_start);

           if (regtime_end != "" && regtime_end != null)

               Select_Sql += string.Format(" and to_char(to_date(SEND_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') <='{0}'", regtime_end);



           string sql0 = "select count(*) from GX_SMS_RECORD where 1=1 and DEL_FLAG='0' " + Select_Sql;
           DataTable dt0 = OracleHelper.GetTable(sql0, null);
           count = Convert.ToInt32(dt0.Rows[0][0]);   //得到了全部的记录数
           if (count % pagesize == 0)                 //计算得到全部页数
               pagecount = count / pagesize;
           else
               pagecount = count / pagesize + 1;
           if (count == 0)
               start = 0;
           else
               start = (nowpage - 1) * pagesize + 1;         //计算该显示记录的行数范围;

           if (start <= count)
           {
               if (count - start >= pagesize)             //start起始 last结束
                   last = start + pagesize - 1;
               else
                   last = count;
           }
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GX_SMS_RECORD  where 1=1 and DEL_FLAG='0' {0} order by ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);

           DataTable dt = OracleHelper.GetTable(sql, null);
           string responseText = JsonHelper.getRecordJson(dt);

           responseText = "[{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}]";
           responseText = responseText.Replace("\n", " ");
           responseText = responseText.Replace("\r", " ");
           responseText = responseText.Replace("\\", "/");
           return responseText;
       }
    }
}
