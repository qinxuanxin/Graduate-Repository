using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using lyw.blueunion.backstagesystem.Utilities;
using lyw.blueunion.backstagesystem.Dal.Log;
namespace lyw.blueunion.backstagesystem.Dal.Home
{
   public class HomeDal
    {
       LogDal lgdal = new LogDal();
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public string dataStatistics()
       {
           string responseText = "";
           //if (memberName != "" && memberName != null)
           //    Select_Sql += string.Format(" and (NAME = '{0}')", memberName);
           //if (registerTime != "" && registerTime != null)

           //    Select_Sql += string.Format(" and SEARCH_TIME <= to_date('{0}','yyyy/mm/dd hh24:mi:ss') ", registerTime);


           //string sql0 = "select count(*) from GX_PERSON_USER_VIEW where 1=1 and DEL_FLAG='0' " + Select_Sql;
           return responseText;
       }
       /// <summary>
       /// 设置新密码
       /// </summary>
       /// <param name="initsecret"></param>
       /// <param name="newsecret"></param>
       /// <returns></returns>
       public string saveSecret(string initsecret, string newsecret, string guid, string username,string ip)       
       {
           string responseText = "";
           string log = "";
         
           bool logFlag = false;
           if (guid == "")
           {
               responseText = "{\"msg\":\"fail\",\"status\":\"未获取到当前用户编号\"}";
            
           }
           string sql0 = string.Format("select * from GX_ADMIN_ACCOUNT  where GUID='{0}'",guid);
           DataTable dt = OracleHelper.GetTable(sql0, null);
           string originalsecret = dt.Rows[0]["ADMIN_PASSWORD"].ToString();
           if (initsecret == originalsecret)
           {
               string sql = string.Format("update GX_ADMIN_ACCOUNT set ADMIN_PASSWORD='{0}' where GUID='{1}'", newsecret, guid);
               int flag = OracleHelper.ExecuteNonQuery(sql, null);
               if(flag>0)
               {
                   responseText = "{\"msg\":\"success\",\"status\":\"更新密码成功\"}";
                   logFlag = true;
               }
              
               else
             responseText = "{\"msg\":\"fail\",\"status\":\"更新密码失败\"}";
           }
           else
               responseText = "{\"msg\":\"fail\",\"status\":\"输入的原密码不正确\"}";

           if (logFlag)
           {
               log = username + " 修改id为" + guid + "的密码成功";
           }
           else
               log = username + " 修改id为" + guid + "的密码失败";
           lgdal.setOperationLog(username,log, ip);
           return responseText;
              
       
       }
       /// <summary>
       /// 查询用户登录记录
       /// </summary>
       /// <returns></returns>
       public string searchUserRecord(string guid, int nowpage, int pagesize)
       {
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数
           string responseText = "";
           if (guid == "")
           {
               responseText = "[{\"msg\":\"fail\",\"count\":0,\"record\":\"\",\"status\":\"未获取当前用户编号\"}]";
           }
           string sql0 = string.Format("select count(*) from GX_ADMIN_ACCOUNT_RECORD  where GUID='{0}'", guid);
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
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GX_ADMIN_ACCOUNT_RECORD  where 1=1  and GUID='{0}' order by ORDER_TIME desc) a) b  where b.row_num between {1} and {2}", guid, start, last);

           DataTable dt = OracleHelper.GetTable(sql, null);
            responseText = JsonHelper.getRecordJson(dt);
        
         
           if (dt.Rows.Count!=0)
               responseText = "[{\"msg\":\"success\",\"count\":\"" + count.ToString() + "\",\"record\":[" + responseText + "]}]";
           else
               responseText = "[{\"msg\":\"fail\",\"count\":0,\"record\":\"\",\"status\":\"没有数据\"}]";
         
           return responseText;
       }
       /// <summary>
       /// 会员总数量
       /// </summary>
       /// <returns></returns>
       public string allCount()
       {
           string responseText = "";

           // string todaydate = DateTime.Now.ToShortDateString().ToString();

           string sql = string.Format("select count(*) from GX_USER where 1=1");
           DataTable dt = OracleHelper.GetTable(sql, null);
           string count = dt.Rows[0][0].ToString();

           responseText = "[{\"msg\":\"success\",\"membercount\":\"" + count + "\",\"ordercount\":\"0\"}]";
           return responseText;
       }

       /// <summary>
       /// 今天的新增的会员/订单数量
       /// </summary>
       /// <returns></returns>
       public string todayCount()
       { 
             string responseText = "";

            // string todaydate = DateTime.Now.ToShortDateString().ToString();
           
           string sql = string.Format("select count(*) from GX_USER where trunc(SEARCH_TIME)=trunc(sysdate)");
           DataTable dt = OracleHelper.GetTable(sql, null);
           string count = dt.Rows[0][0].ToString() ;
         
         responseText="[{\"msg\":\"success\",\"membercount\":\""+count+"\",\"ordercount\":\"0\"}]";
         return responseText;
       }
       /// <summary>
       /// 请求操作日志
       /// </summary>
       public string  requestOperationLog()
       {
         string responseText="";
         string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GX_OPERATION_LOG where 1=1 and DEL_FLAG='0' order by ADD_TIME desc) a) b  where b.row_num between {0} and {1}", 0, 7);
            
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
           {
               responseText = "[{\"msg\":\"fail\",\"status\":\"暂无日志\",\"record\":[]}]";
           }
           else
           {
               responseText = JsonHelper.getRecordJson(dt);
               responseText = "[{\"msg\":\"success\",\"record\":[" + responseText + "]}]";
           }
        
         return responseText;
       }
       /// <summary>
       /// 请求所有的用户名
       /// </summary>
       /// <returns></returns>
       public string requestAllAccount()
       {
           string responseText = "";

           // string todaydate = DateTime.Now.ToShortDateString().ToString();

           string sql = string.Format("select GUID,ADMIN_NAME from GX_ADMIN_ACCOUNT where 1=1");
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
           {
               responseText = "[{\"msg\":\"fail\",\"status\":\"查无用户\"}]";
           }
           else
           {
               responseText = JsonHelper.getRecordJson(dt);

               responseText = "[{\"msg\":\"success\",\"servers\":[" + responseText + "]}]";
           }
         
           return responseText;
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
       public string requestMoreOperationLog(string admin_name, string oper_time_start, string oper_time_end, int nowpage, int pagesize)
       {
           string responseText = "";
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数


           if (!(string.IsNullOrEmpty(admin_name)) && admin_name!="00")
               Select_Sql += string.Format(" and (USER_NAME = '{0}')", admin_name);

           if (!(string.IsNullOrEmpty(oper_time_start)))
               Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') >='{0}'", oper_time_start);
           if (!(string.IsNullOrEmpty(oper_time_end)))
               Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') <='{0}'", oper_time_end);


        
           string sql0 = "select count(*) from GX_OPERATION_LOG where 1=1 and DEL_FLAG='0' " + Select_Sql;
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

           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GX_OPERATION_LOG where 1=1 and DEL_FLAG='0' {0} order by ADD_TIME desc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);
           DataTable dt = OracleHelper.GetTable(sql, null);
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}]";
           return responseText;
       }
      

    }
}
