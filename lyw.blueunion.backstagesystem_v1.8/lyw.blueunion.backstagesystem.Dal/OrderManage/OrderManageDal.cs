using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using lyw.blueunion.backstagesystem.Model.Order;
using lyw.blueunion.backstagesystem.Utilities;
using lyw.blueunion.backstagesystem.Dal.Log;
namespace lyw.blueunion.backstagesystem.Dal.OrderManage
{
   public  class OrderManageDal
    {
       LogDal lgdal = new LogDal();
       /// <summary>
       /// 订单列表信息
       /// </summary>
       /// <param name="order"></param>
       /// <param name="tableViewName"></param>
       /// <returns></returns>
       public string orderListData(OrderOfPcb order, string tableViewName)
       {
           string responseText = "";
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数
           int pagesize = 0;
           int nowpage = 0;
           if (order.Pagesize == "" || order.Pagesize==null)
               pagesize = 10;
           else    
            pagesize = Convert.ToInt32(order.Pagesize);
       
           if (order.Nowpage == "" || order.Nowpage ==null)
               nowpage = 1;
         else
             nowpage = Convert.ToInt32(order.Nowpage);
           if (order.Order_num != "" && order.Order_num != null)
               Select_Sql += string.Format(" and (ORDER_SN = '{0}')", order.Order_num);

           if (order.Order_regtime_start != "" && order.Order_regtime_start != null)
               Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') >='{0}'", order.Order_regtime_start);
           if (order.Order_regtime_end != "" && order.Order_regtime_end != null)
               Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') <='{0}'", order.Order_regtime_end);

           if ( order.Order_status != "00" && order.Order_status != "" && order.Order_status != null)
               Select_Sql += string.Format(" and (ORDER_FLAG = '{0}')", order.Order_status);
           if (order.Pay_status != "00" && order.Pay_status != "" && order.Pay_status != null)
               Select_Sql += string.Format(" and (PAY_FLAG = '{0}')", order.Pay_status);
           if (order.Shipping_status != "00" && order.Shipping_status != "" && order.Shipping_status != null)
               Select_Sql += string.Format(" and (SEND_FLAG = '{0}')", order.Shipping_status);

           string sql0 = "select count(*) from " + tableViewName + " where 1=1 and DEL_FLAG='0' " + Select_Sql;
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
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from " + tableViewName + "  where 1=1 and DEL_FLAG='0' {0} order by REC_ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);
           DataTable dt = OracleHelper.GetTable(sql, null);
            responseText = JsonHelper.getRecordJson(dt);
           responseText = "{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}";
           return responseText;
       }
       /// <summary>
       /// 接发包任务列表信息
       /// </summary>
       /// <param name="order"></param>
       /// <param name="tableViewName"></param>
       /// <returns></returns>
       public string orderListTaskData(OrderOfPcb order, string tableViewName)
       {
           string responseText = "";
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数
           int pagesize = 0;
           int nowpage = 0;
           if (order.Pagesize == "" || order.Pagesize == null)
               pagesize = 10;
           else
               pagesize = Convert.ToInt32(order.Pagesize);

           if (order.Nowpage == "" || order.Nowpage == null)
               nowpage = 1;
           else
               nowpage = Convert.ToInt32(order.Nowpage);

           if (order.Order_num != "" && order.Order_num != null)
               Select_Sql += string.Format(" and (TASK_ID = '{0}')", order.Order_num);
           if (order.Order_phone != "" && order.Order_phone != null)
               Select_Sql += string.Format(" and (PHONE = '{0}')", order.Order_phone);
           if (order.Order_regtime_start != "" && order.Order_regtime_start != null)
               Select_Sql += string.Format(" and to_char(to_date(SEND_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') >='{0}'", order.Order_regtime_start);
           if (order.Order_regtime_end != "" && order.Order_regtime_end != null)
               Select_Sql += string.Format(" and to_char(to_date(SEND_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') <='{0}'", order.Order_regtime_end);
           

           if (order.Order_status != "00" && order.Order_status != "" && order.Order_status != null)
           {
               Select_Sql += string.Format(" and (IS_RECEIVED = '{0}')", order.Order_status);
           }
             
         

           string sql0 = "select count(*) from " + tableViewName + " where 1=1 and DEL_FLAG='0' " + Select_Sql;
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
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from " + tableViewName + "  where 1=1 and DEL_FLAG='0' {0} order by TASK_ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);
           DataTable dt = OracleHelper.GetTable(sql, null);
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}";
           return responseText;
       }
       /// <summary>
       /// 当前订单详细信息
       /// </summary>
       /// <param name="order_id"></param>
       /// <param name="tableview"></param>
       /// <returns></returns>
       public string orderDetailInfo(string order_id, string tableview)     
       {
           string responseText = "";
           string sql = "select * from " + tableview + " where ORDER_ID='" + order_id + "'";
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
               return "[{\"msg\":\"fail\",\"status\":\"查无数据\"}]";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"status\":\"正确查到数据\",\"servers\":[" + responseText + "]}]";
           return responseText;
       }
       /// <summary>
       /// 当前任务单的接包人信息详细信息
       /// </summary>
       /// <param name="order_id"></param>
       /// <returns></returns>
       public string taskDetailReceiveUserInfo(string task_id)
       {
           string responseText = "";
           string sql = "";
           string userinfo_response = "";
           string response = "";
           string receivepackage = "";
           sql = string.Format("select * from GXFW_RECEIVE_TASK where TASK_ID='{0}'", task_id);
           DataTable dt = OracleHelper.GetTable(sql, null);
           receivepackage = JsonHelper.getRecordJson(dt);
           for ( int i = 0; i < dt.Rows.Count; i++)
           {
               string user_id = dt.Rows[i]["USER_ID"].ToString();
               string sql1 = string.Format("select * from GX_USER where USER_ID='{0}'", user_id);
               DataTable dt1 = OracleHelper.GetTable(sql1, null);
               string member_type = dt1.Rows[0]["MEMBER_TYPE"].ToString();
               if (member_type == "个人会员")
               {
                   string sql2 = string.Format("select a.MEMBER_TYPE,a.USER_TEL,b.* from  (select * from GX_USER) a, (select * from GX_USER_MEMBER_PERSONAL) b where a.USER_ID=b.USER_ID and b.USER_ID='{0}'", user_id);
                   DataTable dt2 = OracleHelper.GetTable(sql2, null);
                   response = JsonHelper.getRecordJson(dt2);
               }
               else if (member_type == "企业会员")
               {
                   string sql2 = string.Format("select a.MEMBER_TYPE,b.* from  (select * from GX_USER) a, (select * from GX_USER_MEMBER_FACTORY) b where a.USER_ID=b.USER_ID and b.USER_ID='{0}'", user_id);
                  
                   DataTable dt2 = OracleHelper.GetTable(sql2, null);
                   response = JsonHelper.getRecordJson(dt2);
               }
               else { }
               if (i == dt.Rows.Count - 1)
               {
                   userinfo_response += response;
               }
               else
               {
                   userinfo_response += response + ",";
               }
              
           }


           responseText = "{\"receivepackage\":[" + receivepackage + "],\"userinfo_response\":[" + userinfo_response + "]}";
          // responseText = "[" + receivepackage + "," + userinfo_response + "]";
           return responseText;
       }
       /// <summary>
       /// 当前任务单详细的发包人信息
       /// </summary>
       /// <param name="task_id"></param>
       public string  taskDetailSendUserInfo(string task_id)
       {
       
             string responseText = "";
            string sql="";
           string userinfo_response="";
           string sendpackage="";
           sql=string.Format("select * from GXFW_SEND_TASK where TASK_ID='{0}'",task_id);
            DataTable dt = OracleHelper.GetTable(sql, null);
           sendpackage=JsonHelper.getRecordJson(dt);
           string user_id=dt.Rows[0]["USER_ID"].ToString();
           string sql1=string.Format("select * from GX_USER where USER_ID='{0}'",user_id);
           DataTable dt1=OracleHelper.GetTable(sql1,null);
           string member_type=dt1.Rows[0]["MEMBER_TYPE"].ToString();
           if(member_type=="个人会员")
           {
              string sql2=string.Format("select * from GX_USER_MEMBER_PERSONAL where USER_ID='{0}'",user_id);
                DataTable dt2=OracleHelper.GetTable(sql2,null);
               userinfo_response=JsonHelper.getRecordJson(dt2);
           }
           else if(member_type=="企业会员")
           {
                string sql2=string.Format("select * from GX_USER_MEMBER_FACTORY where USER_ID='{0}'",user_id);
                DataTable dt2=OracleHelper.GetTable(sql2,null);
               userinfo_response=JsonHelper.getRecordJson(dt2);
           }
           else{}
        
           responseText = "{\"sendpackage\":" + sendpackage + ",\"userinfo_response\":" + userinfo_response + "}";
          return responseText;
          
       }
       /// <summary>
       /// 当前用户竞标成功
       /// </summary>
       /// <param name="task_id"></param>
       /// <param name="user_id"></param>
       /// <returns></returns>
       public string orderToBidSuccess(string task_id, string user_id,string status_flag,string username,string ip)
       {
           string responseText = "";
           string log = "";
           bool logFlag = false;
           int flag;
           string nowtime = DateTime.Now.ToString();
          
          if (status_flag == "1")//审核此人发包成功
           {
               string sql1 = string.Format("update GXFW_SEND_TASK set IS_RECEIVED ='1' where   TASK_ID='{0}'", task_id);
               int flag0 = OracleHelper.ExecuteNonQuery(sql1, null);
               if (flag0 > 0)
               {
                   logFlag = true;
                   responseText = "[{\"msg\":\"success\",\"status\":\"审核成功\"}]";
               }
                  
               else
                   responseText = "[{\"msg\":\"fail\",\"status\":\"选中失败,发包表标志位无法更新\"}]";
               if (logFlag)
               {
                   log = username + " 成功审核id为" + user_id + "的会员发任务id为" + task_id + "的包";
               }
               else
                   log = username + " 失败审核id为" + user_id + "的会员发任务id为" + task_id + "的包";
           }
          else if (status_flag == "2")//选中此人为接包人
          {
              string sql = string.Format("update GXFW_RECEIVE_TASK set IS_ACCEPTED =(case USER_ID when '{0}' then '1' else '2' end) where 1=1 and TASK_ID='{1}'", user_id, task_id);
           //  update GXFW_RECEIVE_TASK set IS_ACCEPTED =case USER_ID when '109' then '1' else '0'  where TASK_ID='51' 
              flag = OracleHelper.ExecuteNonQuery(sql, null);
              if (flag > 0)
              {
                  string sql0 = string.Format("update GXFW_RECEIVE_TASK set RECEIVE_SUCCESS_TIME ='{0}' where 1=1 and TASK_ID='{1}' and USER_ID='{2}'", nowtime, task_id,user_id);
                  int fl = OracleHelper.ExecuteNonQuery(sql0, null);
                  string sql1 = string.Format("update GXFW_SEND_TASK set IS_RECEIVED ='2' where   TASK_ID='{0}'", task_id);
                  int flag0 = OracleHelper.ExecuteNonQuery(sql1, null);
                  if (flag0 > 0)
                  {
                      logFlag = true;
                      responseText = "[{\"msg\":\"success\",\"status\":\"选中成功\"}]";
                  }
                      
                  else
                      responseText = "[{\"msg\":\"fail\",\"status\":\"选中失败,发包表标志位无法更新\"}]";
                  if (logFlag)
                  {
                      log = username + " 成功审核id为" + user_id + "的会员接任务id为" + task_id + "的包";
                  }
                  else
                      log = username + " 失败审核id为" + user_id + "的会员接任务id为" + task_id + "的包";
              }

              else
                  responseText = "[{\"msg\":\"fail\",\"status\":\"选中失败,接包表标志位无法更新\"}]";
          }
          else if (status_flag == "3")//对此任务进行托管
          {
              string sql = string.Format("update GXFW_RECEIVE_TASK set IS_ACCEPTED ='3',RECEIVE_SUCCESS_TIME='{0}' where USER_ID='{1}' and TASK_ID='{2}'", nowtime, user_id, task_id);
              flag = OracleHelper.ExecuteNonQuery(sql, null);
              if (flag > 0)
              {
                  string sql1 = string.Format("update GXFW_SEND_TASK set IS_RECEIVED ='3' where   TASK_ID='{0}'", task_id);
                  int flag0 = OracleHelper.ExecuteNonQuery(sql1, null);
                  if (flag0 > 0)
                  {
                      responseText = "[{\"msg\":\"success\",\"status\":\"托管成功\"}]";
                      logFlag = true;
                  }
                    
                  else
                      responseText = "[{\"msg\":\"fail\",\"status\":\"托管失败,发包表标志位无法更新\"}]";
                  if (logFlag)
                  {
                      log = username + " 成功审核id为" + user_id + "的会员托管任务id为" + task_id + "的包";
                  }
                  else
                      log = username + " 失败审核id为" + user_id + "的会员托管任务id为" + task_id + "的包";
              }

              else
                  responseText = "[{\"msg\":\"fail\",\"status\":\"托管失败,接包表标志位无法更新\"}]";
          }

           else
           {
               responseText = "[{\"msg\":\"fail\",\"status\":\"不存在此功能标志位值\"}]";
           }
          lgdal.setOperationLog(username,log, ip);
           return responseText;
       }
       /// <summary>
       /// 删除当前订单
       /// </summary>
       /// <param name="order_id"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       public string orderDelOrder(string order_id, string tableview,string username,string ip)
       {
         
           string responseText = "";
           string  orderdinumwhen="";
           string orderidnumwhere = "";
           string log = "";
           bool logFlag = false;
           string[] orderIdArray=order_id.Split(',') ;
          foreach(string i in orderIdArray)
            orderdinumwhen+="WHEN "+"\'"+i.ToString()+"\'"+" THEN '1'  ";
          for (int iwhere=0; iwhere < orderIdArray.Length; iwhere++)
          {
              if (iwhere > 0)
              orderidnumwhere += ",";
              orderidnumwhere += "\'" + orderIdArray[iwhere].ToString() + "\'";
          }
            
          
           //string sql = "update " + tableview + " set DEL_FLAG ='1' where ORDER_ID='" + order_id + "'";
          string sql = "UPDATE  " + tableview + " set DEL_FLAG=CASE ORDER_ID  " + orderdinumwhen + " END  WHERE ORDER_ID IN (" + orderidnumwhere + ")";
                  

          int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               responseText = "[{\"msg\":\"success\",\"status\":\"删除成功\"}]";
               logFlag = true;
           }
              
           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"删除失败\"}]";
           if (logFlag)
           {
               log = username + " 成功删除id为" + order_id + "的订单";
           }
           else
               log = username + " 失败删除id为" + order_id + "的订单";
           lgdal.setOperationLog(username,log, ip);
           return responseText;
       }
       /// <summary>
       /// 删除发包任务单号
       /// </summary>
       /// <param name="task_id"></param>
       /// <param name="username"></param>
       /// <param name="ip"></param>
       /// <returns></returns>
       public string orderDelOrder(string task_id,string username,string ip)
       {

           string responseText = "";
           string orderdinumwhen = "";
           string orderidnumwhere = "";
           string log = "";
           bool logFlag = false;
           string[] orderIdArray = task_id.Split(',');
           foreach (string i in orderIdArray)
               orderdinumwhen += "WHEN " + "\'" + i.ToString() + "\'" + " THEN '1'  ";
           for (int iwhere = 0; iwhere < orderIdArray.Length; iwhere++)
           {
               if (iwhere > 0)
                   orderidnumwhere += ",";
               orderidnumwhere += "\'" + orderIdArray[iwhere].ToString() + "\'";
           }


           //string sql = "update " + tableview + " set DEL_FLAG ='1' where ORDER_ID='" + order_id + "'";
           string sql = "UPDATE  GXFW_SEND_TASK set DEL_FLAG=CASE TASK_ID  " + orderdinumwhen + " END  WHERE TASK_ID IN (" + orderidnumwhere + ")";


           int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
            logFlag=true;
            responseText = "[{\"msg\":\"success\",\"status\":\"删除成功\"}]";
           }
              
           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"删除失败\"}]"; 
             if (logFlag)
           {
               log = username + " 成功删除id为" + task_id + "的任务";
           }
           else
                 log = username + " 失败删除id为" + task_id + "的任务";
           lgdal.setOperationLog(username,log, ip);
           return responseText;
       }
       /// <summary>
       /// 查询订单状态数量
       /// </summary>
       /// <param name="order_status"></param>
       /// <param name="tableview"></param>
       /// <returns></returns>
       public string orderOrderStatusCount(string order_status, string tableview)
       {
           string responseText = "";
           string Select_Sql = "";
           if (order_status != "" && order_status != null)
               Select_Sql += string.Format(" and (ORDER_FLAG = '{0}')", order_status);
         

           string sql = "select count(*) from " + tableview + " where 1=1 and DEL_FLAG='0' " + Select_Sql;

           DataTable dt = OracleHelper.GetTable(sql, null);
           string count = dt.Rows[0][0].ToString();
           
           responseText = "[{\"msg\":\"success\",\"counts\":\""+count+"\"}]";
           return responseText;
          
       }
       /// <summary>
       /// 查询任务各状态单号数量
       /// </summary>
       /// <param name="task_id"></param>
       /// <returns></returns>
       public string orderOrderStatusCount(string order_status)
       {
           string responseText = "";
           string Select_Sql = "";
           if (order_status != "" && order_status != null)
               Select_Sql += string.Format(" and (IS_RECEIVED = '{0}')", order_status);


           string sql = "select count(*) from  GXFW_SEND_TASK where 1=1 and DEL_FLAG='0' " + Select_Sql;

           DataTable dt = OracleHelper.GetTable(sql, null);
           string count = dt.Rows[0][0].ToString();

           responseText = "[{\"msg\":\"success\",\"counts\":\"" + count + "\"}]";
           return responseText;

       }
       /// <summary>
       /// 查询支付状态数量
       /// </summary>
       /// <param name="order_status"></param>
       /// <param name="tableview"></param>
       /// <returns></returns>
       public string orderPayStatusCount(string pay_status, string tableview)
       {
           string responseText = "";
           string Select_Sql = "";
           if (pay_status != "" && pay_status != null)
               Select_Sql += string.Format(" and (PAY_FLAG = '{0}')", pay_status);
         

           string sql = "select count(*) from " + tableview + " where 1=1 and DEL_FLAG='0' " + Select_Sql;

           DataTable dt = OracleHelper.GetTable(sql, null);
           string count = dt.Rows[0][0].ToString();
           
           responseText = "[{\"msg\":\"success\",\"counts\":\""+count+"\"}]";
           return responseText;
          
       }
       /// <summary>
       /// 查询配送状态数量
       /// </summary>
       /// <param name="pay_status"></param>
       /// <param name="tableview"></param>
       /// <returns></returns>
       public string orderShippStatusCount(string shipp_status, string tableview)
       {
           string responseText = "";
           string Select_Sql = "";
           if (shipp_status != "" && shipp_status != null)
               Select_Sql += string.Format(" and (SEND_FLAG = '{0}')", shipp_status);


           string sql = "select count(*) from " + tableview + " where 1=1 and DEL_FLAG='0' " + Select_Sql;

           DataTable dt = OracleHelper.GetTable(sql, null);
           string count = dt.Rows[0][0].ToString();

           responseText = "[{\"msg\":\"success\",\"counts\":\"" + count + "\"}]";
           return responseText;

       }
   /// <summary>
   /// 发货/退货
   /// </summary>
   /// <param name="send_type_id"></param>
   /// <param name="track_num"></param>
   /// <param name="order_id"></param>
   /// <param name="tableview"></param>
   /// <param name="returnorsendtype"></param>
   /// <returns></returns>
       public string sendGoods(string send_type_id, string track_num, string order_id, string tableview,string returnorsendtype,string username,string ip)
       {
           string responseText = "";
           string orderidnumwhere = "";
           string log = "";
           bool logFlag = false;
           string functype = "";
           string[] orderIdArray = order_id.Split(',');
           string sql = "";
           for (int iwhere = 0; iwhere < orderIdArray.Length; iwhere++)
           {
               if (iwhere > 0)
                   orderidnumwhere += ",";
               orderidnumwhere += "\'" + orderIdArray[iwhere].ToString() + "\'";
           }
            
           if(returnorsendtype=="1")//发货
           //string sql = "update " + tableview + " set DEL_FLAG ='1' where ORDER_ID='" + order_id + "'";
           {
               sql = "UPDATE  " + tableview + " set SEND_FLAG='" + returnorsendtype + "'  WHERE ORDER_ID IN (" + orderidnumwhere + ")";
               functype = "发货";
           }


           else if (returnorsendtype == "2")//退货
           {
               sql = "UPDATE  " + tableview + " set SEND_FLAG='3'  WHERE ORDER_ID IN (" + orderidnumwhere + ")";
               functype = "退货";
           }
              
           int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               logFlag = true;
               responseText = "[{\"msg\":\"success\",\"status\":\"\"}]";
           }
              
           else
               responseText ="[{\"msg\":\"fail\",\"status\":\"\"}]";
           if (logFlag)
           {
               log = username + " 成功" + functype + "id为" + order_id + "的订单";
           }
           else
               log = username + " 失败" + functype + "id为" + order_id + "的订单";
           lgdal.setOperationLog(username,log, ip);
           return responseText;
       }
       /// <summary>
       /// 租用仪器共享订单列表信息
       /// </summary>
       /// <returns></returns>
       public string rentInstrumentShareListData(InstrumentShare instshare)
       {
           string responseText = "";
          
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数
           int pagesize = 0;
           int nowpage = 0;
           if (instshare.Pagesize == "" || instshare.Pagesize == null)
               pagesize = 10;
           else
               pagesize = Convert.ToInt32(instshare.Pagesize);

           if (instshare.Nowpage == "" || instshare.Nowpage == null)
               nowpage = 1;
           else
               nowpage = Convert.ToInt32(instshare.Nowpage);

           if (instshare.Order_num != "" && instshare.Order_num != null)
               Select_Sql += string.Format(" and (ORDER_SN = '{0}')", instshare.Order_num);
           if (instshare.User_phone != "" && instshare.User_phone != null)
               Select_Sql += string.Format(" and (USER_TEL = '{0}')", instshare.User_phone);
           if (instshare.Order_regtime_start != "" && instshare.Order_regtime_start != null)
               Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') >='{0}'", instshare.Order_regtime_start);
           if (instshare.Order_regtime_end != "" && instshare.Order_regtime_end != null)
               Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') <='{0}'", instshare.Order_regtime_end);


           //if (order.Order_status != "00" && order.Order_status != "" && order.Order_status != null)
           //{
           //    Select_Sql += string.Format(" and (IS_RECEIVED = '{0}')", order.Order_status);
           //}



           string sql0 = "select count(*) from GXIS_BORROW_VIEW  where 1=1 and DEL_FLAG='0' " + Select_Sql;
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
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GXIS_BORROW_VIEW  where 1=1 and DEL_FLAG='0' {0} order by REC_ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);
           DataTable dt = OracleHelper.GetTable(sql, null);
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}";
           return responseText;
         
       }
       /// <summary>
       /// 租用仪器订单详细信息
       /// </summary>
       /// <param name="order_id"></param>
       /// <returns></returns>
       public string rentInstrumentDetailInfo(string order_id)
       {
           string responseText = "";
           string sql = "select * from GXIS_BORROW_VIEW  where ORDER_ID='" + order_id + "'";
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
               return "{\"msg\":\"fail\",\"status\":\"查无数据\"}";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "{\"msg\":\"success\",\"status\":\"正确查到数据\",\"servers\":[" + responseText + "]}";
           return responseText;
       }
       /// <summary>
       /// 删除共享订单
       /// </summary>
       /// <param name="order_id"></param>
       /// <returns></returns>
       public string rentInstrumentDelOrder(string order_id, string username, string ip)
       {
           string responseText = "";
           string orderdinumwhen = "";
           string orderidnumwhere = "";
           string log = "";
           bool logFlag = false;
           string[] orderIdArray = order_id.Split(',');
           foreach (string i in orderIdArray)
               orderdinumwhen += "WHEN " + "\'" + i.ToString() + "\'" + " THEN '1'  ";
           for (int iwhere = 0; iwhere < orderIdArray.Length; iwhere++)
           {
               if (iwhere > 0)
                   orderidnumwhere += ",";
               orderidnumwhere += "\'" + orderIdArray[iwhere].ToString() + "\'";
           }


           //string sql = "update " + tableview + " set DEL_FLAG ='1' where ORDER_ID='" + order_id + "'";
           string sql = "UPDATE GXIS_BORROW_INFO set DEL_FLAG=CASE ORDER_ID  " + orderdinumwhen + " END  WHERE ORDER_ID IN (" + orderidnumwhere + ")";


           int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               responseText = "[{\"msg\":\"success\",\"status\":\"删除成功\"}]";
               logFlag = true;
           }

           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"删除失败\"}]";
           if (logFlag)
           {
               log = username + " 成功删除id为" + order_id + "的订单";
           }
           else
               log = username + " 失败删除id为" + order_id + "的订单";
           lgdal.setOperationLog(username, log, ip);
           return responseText;
       }
       /// <summary>
       /// 加入仪器共享数据列表
       /// </summary>
       /// <param name="instrument_num"></param>
       /// <param name="instrument_phone"></param>
       /// <returns></returns>
       public string addInstrumentShareListData(AddInstrument instshare)
       { 
         string responseText = "";
          
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数
           int pagesize = 0;
           int nowpage = 0;
           if (instshare.Pagesize == "" || instshare.Pagesize == null)
               pagesize = 10;
           else
               pagesize = Convert.ToInt32(instshare.Pagesize);

           if (instshare.Nowpage == "" || instshare.Nowpage == null)
               nowpage = 1;
           else
               nowpage = Convert.ToInt32(instshare.Nowpage);

           if (instshare.Instrument_id != "" && instshare.Instrument_id != null)
               Select_Sql += string.Format(" and (INSTRUMENT_ID = '{0}')", instshare.Instrument_id);
           if (instshare.User_phone != "" && instshare.User_phone != null)
               Select_Sql += string.Format(" and (USER_TEL = '{0}')", instshare.User_phone);
           if (instshare.Regtime_start != "" && instshare.Regtime_start != null)
               Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') >='{0}'", instshare.Regtime_start);
           if (instshare.Regtime_end != "" && instshare.Regtime_end != null)
               Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') <='{0}'", instshare.Regtime_end);


           //if (order.Order_status != "00" && order.Order_status != "" && order.Order_status != null)
           //{
           //    Select_Sql += string.Format(" and (IS_RECEIVED = '{0}')", order.Order_status);
           //}



           string sql0 = "select count(*) from GXIS_ADD_VIEW  where 1=1 and DEL_FLAG='0' " + Select_Sql;
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
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GXIS_ADD_VIEW  where 1=1 and DEL_FLAG='0' {0} order by INSTRUMENT_ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);
           DataTable dt = OracleHelper.GetTable(sql, null);
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}";
           return responseText;
         
       }
      /// <summary>
       /// 加入仪器设备详细信息
      /// </summary>
      /// <param name="instrument_id"></param>
      /// <returns></returns>
       public string addInstrumentDetailInfo(string instrument_id)
       {
           string responseText = "";
           string sql = "select * from GXIS_ADD_VIEW  where INSTRUMENT_ID='" + instrument_id + "'";
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
               return "{\"msg\":\"fail\",\"status\":\"查无数据\"}";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "{\"msg\":\"success\",\"status\":\"正确查到数据\",\"servers\":[" + responseText + "]}";
           return responseText;
       }
       /// <summary>
       /// 审核加入仪器设备
       /// </summary>
       /// <param name="instrumentorderid"></param>
       /// <param name="status"></param>
       /// <param name="username"></param>
       /// <param name="ip"></param>
       /// <returns></returns>
       public string addInstrumentCheck(string instrumentorderid, string status, string username, string ip)
       {
           string responseText = "";
          
           string log = "";
           bool logFlag = false;
        


           //string sql = "update " + tableview + " set DEL_FLAG ='1' where ORDER_ID='" + order_id + "'";
           string sql = string.Format("UPDATE GXIS_INFO set REV_FLAG='{0}'  WHERE INSTRUMENT_ID='{1}'", status, instrumentorderid);


           int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               responseText = "{\"msg\":\"success\",\"status\":\"审核成功\"}";
               logFlag = true;
           }

           else
               responseText = "{\"msg\":\"fail\",\"status\":\"审核失败\"}";
           if (logFlag)
           {
               if (status == "2")

                   log = username + " 成功更新id为" + instrumentorderid + "的设备审核状态为成功";
               else
                   log = username + " 成功更新id为" + instrumentorderid + "的设备审核状态为失败";
           }
           else
           {
               if (status == "2")

                   log = username + " 失败更新id为" + instrumentorderid + "的设备审核状态为成功";
               else
                   log = username + " 失败更新id为" + instrumentorderid + "的设备审核状态为失败";
           }
              
           lgdal.setOperationLog(username, log, ip);
           return responseText;
       }
       /// <summary>
       /// 删除加入的仪器设备
       /// </summary>
       /// <param name="instrumentorderid"></param>
       /// <param name="username"></param>
       /// <param name="ip"></param>
       /// <returns></returns>
       public string addInstrumentDel(string instrumentorderid, string username, string ip)
       {
           string responseText = "";
           string orderdinumwhen = "";
           string orderidnumwhere = "";
           string log = "";
           bool logFlag = false;
           string[] orderIdArray = instrumentorderid.Split(',');
           foreach (string i in orderIdArray)
               orderdinumwhen += "WHEN " + "\'" + i.ToString() + "\'" + " THEN '1'  ";
           for (int iwhere = 0; iwhere < orderIdArray.Length; iwhere++)
           {
               if (iwhere > 0)
                   orderidnumwhere += ",";
               orderidnumwhere += "\'" + orderIdArray[iwhere].ToString() + "\'";
           }


           //string sql = "update " + tableview + " set DEL_FLAG ='1' where ORDER_ID='" + order_id + "'";
           string sql = "UPDATE GXIS_INFO set DEL_FLAG=CASE INSTRUMENT_ID  " + orderdinumwhen + " END  WHERE INSTRUMENT_ID IN (" + orderidnumwhere + ")";


           int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               responseText = "{\"msg\":\"success\",\"status\":\"删除成功\"}";
               logFlag = true;
           }

           else
               responseText = "{\"msg\":\"fail\",\"status\":\"删除失败\"}";
           if (logFlag)
           {
               log = username + " 成功删除id为" + instrumentorderid + "的订单";
           }
           else
               log = username + " 失败删除id为" + instrumentorderid + "的订单";
           lgdal.setOperationLog(username, log, ip);
           return responseText;
       }
       /// <summary>
       /// 获取下单会员信息
       /// </summary>
       /// <param name="member_phone"></param>
       /// <returns></returns>
       public string getMemberInfo(string member_phone)
       {
           string responseText = "";
           string sql = "";
           DataTable dt = null;
       
           sql  = "select * from GX_FACTORY_USER_VIEW  where USER_TEL='" + member_phone + "'";
            dt = OracleHelper.GetTable(sql, null);
            if (dt.Rows.Count > 0)
            {
                responseText = JsonHelper.getRecordJson(dt);
                responseText = "{\"msg\":\"success\",\"status\":\"正确查到数据\",\"servers\":[" + responseText + "]}";
    
            }
            else
            {
                sql = "select * from GX_PERSON_USER_VIEW  where USER_TEL='" + member_phone + "'";
                dt = OracleHelper.GetTable(sql, null);
                responseText = JsonHelper.getRecordJson(dt);
                responseText = "{\"msg\":\"success\",\"status\":\"正确查到数据\",\"servers\":[" + responseText + "]}";
            }
               //return "[{\"msg\":\"fail\",\"status\":\"查无数据\"}]";
       
           return responseText;
       }
    }
}
