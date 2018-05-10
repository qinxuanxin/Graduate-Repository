using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using lyw.blueunion.backstagesystem.Dal.Log;
using lyw.blueunion.backstagesystem.Utilities;
namespace lyw.blueunion.backstagesystem.Dal.MemberManage
{
   public  class MemberManageDal
    {
       LogDal lgdal = new LogDal();
       /// <summary>
       /// 个人会员数据列表
       /// </summary>
       /// <param name="memberName"></param>
       /// <param name="registerTime"></param>
       /// <param name="nowpage"></param>
       /// <param name="pagesize"></param>
       /// <returns></returns>
       public string memberListDataPersonal(string memberName,string registerTime,int nowpage,int pagesize)
       {
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数
          
           if (memberName != "" && memberName!=null)
               Select_Sql += string.Format(" and (NAME = '{0}')", memberName);
           if(registerTime != "" && registerTime!=null)
              
           Select_Sql += string.Format(" and SEARCH_TIME <= to_date('{0}','yyyy/mm/dd') ", registerTime);


           string sql0 = "select count(*) from GX_PERSON_USER_VIEW where 1=1 and DEL_FLAG='0' " + Select_Sql;
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
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GX_PERSON_USER_VIEW  where 1=1 and DEL_FLAG='0' {0} order by USER_ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);
        
           DataTable dt = OracleHelper.GetTable(sql, null);
           string responseText = JsonHelper.getRecordJson(dt);
         
           responseText = "{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}";
           return responseText;
       }

       /// <summary>
       /// 企业会员数据列表
       /// </summary>
       /// <param name="memberName"></param>
       /// <param name="registerTime"></param>
       /// <param name="nowpage"></param>
       /// <param name="pagesize"></param>
       /// <returns></returns>
       public string memberListDataEnterprise(string memberName, string registerTime, int nowpage, int pagesize)
       {
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数

           if (memberName != "" && memberName != null)
               Select_Sql += string.Format(" and (NAME = '{0}')", memberName);
           if (registerTime != "" && registerTime != null)

               Select_Sql += string.Format(" and SEARCH_TIME <= to_date('{0}','yyyy/mm/dd') ", registerTime);


           string sql0 = "select count(*) from GX_FACTORY_USER_VIEW where 1=1 and DEL_FLAG='0' " + Select_Sql;
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
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GX_FACTORY_USER_VIEW  where 1=1 and DEL_FLAG='0' {0} order by USER_ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);

           DataTable dt = OracleHelper.GetTable(sql, null);
           string responseText = JsonHelper.getRecordJson(dt);
           responseText = "{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}";
           return responseText;
       }
       /// <summary>
       /// 会员单位数据列表
       /// </summary>
       /// <param name="EnterpriseName"></param>
       /// <param name="registerTime"></param>
       /// <param name="nowpage"></param>
       /// <param name="pagesize"></param>
       /// <returns></returns>
       public string memberListDataMemberUnit(string EnterpriseName, string registerTime, int nowpage, int pagesize)
       {
           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数

           if (EnterpriseName != "" && EnterpriseName != null)
               Select_Sql += string.Format(" and (MEMBER_NAME = '{0}')", EnterpriseName);
           if (registerTime != "" && registerTime != null)

               Select_Sql += string.Format(" and ENTERPRISE_SEARCH_TIME <= to_date('{0}','yyyy/mm/dd hh24:mi:ss') ", registerTime);


           string sql0 = "select count(*) from GX_MC_VIEW where 1=1 and DEL_FLAG='0' " + Select_Sql;
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
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GX_MC_VIEW  where 1=1 and DEL_FLAG='0' {0} order by USER_ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);

           DataTable dt = OracleHelper.GetTable(sql, null);
           string responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}]";
           return responseText;
       }

       /// <summary>
       /// 自由职业数据列表
       /// </summary>
       /// <param name="EnterpriseName"></param>
       /// <param name="registerTime"></param>
       /// <param name="nowpage"></param>
       /// <param name="pagesize"></param>
       /// <returns></returns>
       public string memberListDataMemberProfessional(string ProfessionalName, string registerTime, int nowpage, int pagesize)
       {

           string Select_Sql = "";//条件语句
           int count = 0;                    //条件查询到的记录数
           int start = 0;  //起始查询条数
           int last = 0;      //最终查询条数
           int pagecount = 0;//总页数

           if (ProfessionalName != "" && ProfessionalName != null)
               Select_Sql += string.Format(" and (MEMBER_NAME = '{0}')", ProfessionalName);
           if (registerTime != "" && registerTime != null)

               Select_Sql += string.Format(" and PROFESSION_SEARCH_TIME <= to_date('{0}','yyyy/mm/dd') ", registerTime);


           string sql0 = "select count(*) from GX_FW_VIEW where 1=1 and DEL_FLAG='0' and PROFESSION_DEL_FLAG='0' " + Select_Sql;
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
           string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GX_FW_VIEW  where 1=1 and DEL_FLAG='0' and PROFESSION_DEL_FLAG='0' {0} order by USER_ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);

           DataTable dt = OracleHelper.GetTable(sql, null);
           string responseText = JsonHelper.getRecordJson(dt);
           responseText = "{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}";
           return responseText;
       }

       /// <summary>
       /// 详细的个人会员信息
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
       public string detailpersonalInfo(string user_id)
       {
           string responseText = "";
           string sql = string.Format("select * from GX_PERSON_USER_VIEW where USER_ID='{0}'", user_id);
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
               return "[{\"msg\":\"fail\",\"status\":\"获取不到当前会员的信息\"}]";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"status\":\"正确获取当前会员的信息\",\"servers\":[" + responseText + "]}]";
           return responseText;
       }
       /// <summary>
       /// 详细的企业会员信息
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
       public string detailEnterpriseInfo(string user_id)
       {
           string responseText = "";
           string sql =string.Format("select * from GX_FACTORY_USER_VIEW where USER_ID='{0}'",user_id);
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
               return "[{\"msg\":\"fail\",\"status\":\"获取不到当前会员的信息\"}]";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"status\":\"正确获取当前会员的信息\",\"servers\":[" + responseText + "]}]";
           return responseText;
       }
     
       /// <summary>
       /// 详细的自由职业信息
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
       public string detailProfessionalInfo(string user_id, string profession)
       {
           string responseText = "";
           string sql = string.Format("select * from GX_FW_VIEW where USER_ID='{0}' and PROFESSION='{1}'", user_id,profession);
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
               return "[{\"msg\":\"fail\",\"status\":\"获取不到当前会员的信息\"}]";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"status\":\"正确获取当前会员的信息\",\"servers\":[" + responseText + "]}]";
           return responseText;
       }
       /// <summary>
       /// 详细的会员单位信息
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
       public string detailMemberUnitInfo(string user_id)
       {
           string responseText = "";
           string sql = string.Format("select * from GX_MC_VIEW where USER_ID='{0}'", user_id);
           DataTable dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count == 0)
               return "[{\"msg\":\"fail\",\"status\":\"获取不到当前会员的信息\"}]";
           responseText = JsonHelper.getRecordJson(dt);
           responseText = "[{\"msg\":\"success\",\"status\":\"正确获取当前会员的信息\",\"servers\":[" + responseText + "]}]";
           return responseText;
       }
       /// <summary>
       /// 删除会员
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
       public string delmember(string user_id,string username,string ip)
       {
           string responseText = "";
           int flag;
           string log = "";
           bool logFlag = false;
           string sql = string.Format("update GX_USER set DEL_FLAG ='1' where USER_ID='{0}'", user_id);
           flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {

               string sql1 = string.Format("update GX_USER_MEMBER_PERSONAL set PERSON_DEL_FLAG ='1' where USER_ID='{0}'", user_id);
               int flag1 = OracleHelper.ExecuteNonQuery(sql1, null);
               if (flag1 > 0)
               {
                   logFlag = true;
                   responseText = "{\"msg\":\"success\",\"status\":\"删除成功\"}";
               }
             else
                   responseText = "{\"msg\":\"fail\",\"status\":\"删除失败\"}";
           }           
           else
               responseText = "{\"msg\":\"fail\",\"status\":\"删除失败\"}";
           if (logFlag)
           {
               log = username + " 成功删除id为" + user_id+"的会员";
           }
           else
               log = username + " 失败删除id为" + user_id + "的会员";
           lgdal.setOperationLog(username,log, ip);
           return responseText;
       }
       /// <summary>
       /// 删除企业会员
       /// </summary>
       /// <param name="user_id"></param>
       /// <param name="username"></param>
       /// <param name="ip"></param>
       /// <returns></returns>
       public string delEnterMember(string user_id, string username, string ip)
       {
           string responseText = "";
           int flag;
           string log = "";
           bool logFlag = false;
           string sql = string.Format("update GX_USER set DEL_FLAG ='1' where USER_ID='{0}'", user_id);
           flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               string sql1 = string.Format("update GX_USER_MEMBER_FACTORY set FACTORY_DEL_FLAG ='1' where USER_ID='{0}'", user_id);
               int flag1 = OracleHelper.ExecuteNonQuery(sql1, null);
               if (flag1 > 0)
               {
                   logFlag = true;
                   responseText = "{\"msg\":\"success\",\"status\":\"删除成功\"}";
               }
            else
                   responseText = "{\"msg\":\"fail\",\"status\":\"删除失败\"}";

           }           
           else
               responseText = "{\"msg\":\"fail\",\"status\":\"删除失败\"}";
           if (logFlag)
           {
               log = username + " 成功删除id为" + user_id+"的会员";
           }
           else
               log = username + " 失败删除id为" + user_id + "的会员";
           lgdal.setOperationLog(username,log, ip);
           return responseText;
       }
       /// <summary>
       /// 删除自由职业
       /// </summary>
       /// <param name="user_id"></param>
       /// <param name="username"></param>
       /// <param name="ip"></param>
       /// <returns></returns>
       public string delProfessionMember(string user_id, string profession, string username, string ip)
       {
           string responseText = "";
           int flag;
           string log = "";
           bool logFlag = false;
          // string sql = string.Format("update GX_USER set DEL_FLAG ='1' where USER_ID='{0}'", user_id);
         //  flag = OracleHelper.ExecuteNonQuery(sql, null);
        //   if (flag > 0)
          // {
           string sql1 = string.Format("update GXFW_INFO set PROFESSION_DEL_FLAG ='1' where USER_ID='{0}' and PROFESSION='{1}'", user_id,profession);
               int flag1 = OracleHelper.ExecuteNonQuery(sql1, null);
               if (flag1 > 0)
               {
                   logFlag = true;
                   responseText = "{\"msg\":\"success\",\"status\":\"删除成功\"}";
               }
            else
                   responseText = "{\"msg\":\"fail\",\"status\":\"删除失败\"}";

        //   }           
         //  else
          //     responseText = "{\"msg\":\"fail\",\"status\":\"删除失败\"}";
           if (logFlag)
           {
               log = username + " 成功删除id为" + user_id+"的会员";
           }
           else
               log = username + " 失败删除id为" + user_id + "的会员";
           lgdal.setOperationLog(username,log, ip);
           return responseText;
       }
       /// <summary>
       /// 审核自由职业是否注册成功
       /// </summary>
       /// <param name="check_id"></param>
       /// <param name="user_id"></param>
       /// <returns></returns>
       public string professionMemberCheck(string check_id, string user_id, string profession, string username, string ip)
       { 
         string responseText = "";
           int flag;
           string log = "";
           bool logFlag = false;
           string sql = string.Format("update GXFW_INFO set SUCCESS ='{0}' where USER_ID='{1}' and PROFESSION='{2}' ", check_id, user_id, profession);
           flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               logFlag = true;
               responseText = "{\"msg\":\"success\",\"status\":\"审核成功\"}";
           }
           else
               responseText = "{\"msg\":\"fail\",\"status\":\"审核失败\"}";
           if (logFlag)
           {
               log = username + " 成功审核id为" + user_id + "的自由职业注册状态为"+check_id;
           }
           else
               log = username + " 失败审核id为" + user_id + "的自由职业注册状态为"+check_id;
           lgdal.setOperationLog(username, log, ip);
           return responseText;
       }
       /// <summary>
       /// 获取各分类数据进行饼状图分析
       /// </summary>
       /// <returns></returns>
       public string memberRankCountToPie()
       {
           string responseText = "";
           string sql = "";
           string personKey="", personValue="", enterpriseKey="", enterpriseValue="";
           string professionalKey="", professionalValue="", unitKey="", unitValue="";
           DataTable dt;
           sql = string.Format("select MEMBER_TYPE,count(MEMBER_TYPE) from GX_USER where MEMBER_TYPE='个人会员' group by MEMBER_TYPE");
           dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count != 0)
           {
               personKey = dt.Rows[0][0].ToString();
               personValue = dt.Rows[0][1].ToString();
           }
           else
           {
               personKey = "个人会员";
               personValue ="0";
           }
           sql = string.Format("select MEMBER_TYPE,count(MEMBER_TYPE) from GX_USER where MEMBER_TYPE='企业会员' group by MEMBER_TYPE");
           dt = OracleHelper.GetTable(sql, null);
           if (dt.Rows.Count != 0)
           {
               enterpriseKey = dt.Rows[0][0].ToString();
               enterpriseValue = dt.Rows[0][1].ToString();
           }
           else
           {
               enterpriseKey = "企业会员";
               enterpriseValue = "0";
           }
         
           sql = string.Format("select count(*) from GXMC_INFO");
           dt = OracleHelper.GetTable(sql, null);
           unitKey = "企业/会员单位";
           unitValue = dt.Rows[0][0].ToString();
           sql = string.Format("select count(*) from GXFW_INFO");
           dt = OracleHelper.GetTable(sql, null);
           professionalKey = "个人/自由职业";
           professionalValue = dt.Rows[0][0].ToString();
           responseText=
             "["+
             "{\"value\":\""+personValue+"\",\"name\":\""+personKey+"\"}"+
               ","+
             "{\"value\":\""+enterpriseValue+"\",\"name\":\""+enterpriseKey+"\"}"+
               ","+
             "{\"value\":\""+professionalValue+"\",\"name\":\""+professionalKey+"\"}"+
               ","+
             "{\"value\":\""+unitValue+"\",\"name\":\""+unitKey+"\"}"+
             "]";
            
           return responseText;
       }
       /// <summary>
       /// 获取各分类数据进行折线/柱状图分析
       /// </summary>
       /// <returns></returns>
       public string memberRankCountToLine()
       {
           string responseText = "";
           string sql = "";
           DataTable dt;
           int[] personal= new int[7];
           int[] enterprise = new int[7];
           int[] profession = new int[7];
           int[] unit = new int[7];
           string[] sevenTime = new string[7];
           /***************个人近七天数据获取*************/
           sql =
               "SELECT " +
  "days.createTime SEVEN_TIME ," +
  "nvl (m.COUNT, 0) COUNT_NUM" +
" FROM " +
  "(SELECT " +
    "to_char (SYSDATE- LEVEL + 1, 'yyyy-mm-dd') createTime" +
  " FROM " +
    "DUAL connect BY LEVEL <= 7) days" +
  " LEFT JOIN " +
    " (SELECT " +
      "to_char (o.SEARCH_TIME, 'yyyy-mm-dd') createTime," +
      "nvl (COUNT(MEMBER_TYPE), 0) count" +
    " FROM " +
      "GX_USER o" +
    " WHERE o.MEMBER_TYPE='个人会员' " +
    " GROUP BY to_char (o.SEARCH_TIME, 'yyyy-mm-dd')) m" +
    " ON days.createTime  = m.createTime " +
    " GROUP BY days.createTime, m.count " +
    " ORDER BY days.createTime";
           dt = OracleHelper.GetTable(sql,null);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               sevenTime[i] = dt.Rows[i]["SEVEN_TIME"].ToString();
               personal[i] = Convert.ToInt32(dt.Rows[i]["COUNT_NUM"].ToString());
             
           }
           /***************个人近七天数据获取*************/


           /***************企业近七天数据获取*************/
           sql =
             "SELECT " +
"days.createTime SEVEN_TIME ," +
"nvl (m.COUNT, 0) COUNT_NUM" +
" FROM " +
"( SELECT " +
  "to_char (SYSDATE- LEVEL + 1, 'yyyy-mm-dd') createTime" +
" FROM " +
  "DUAL connect BY LEVEL <= 7) days" +
" LEFT JOIN " +
  "(SELECT " +
    "to_char (o.SEARCH_TIME, 'yyyy-mm-dd') createTime," +
    "nvl (COUNT(MEMBER_TYPE), 0) count" +
  " FROM " +
    "GX_USER o" +
  " WHERE o.MEMBER_TYPE='企业会员' " +
  " GROUP BY to_char (o.SEARCH_TIME, 'yyyy-mm-dd')) m " +
  " ON days.createTime  = m.createTime " +
  " GROUP BY days.createTime, m.count" +
  " ORDER BY days.createTime";
           dt = OracleHelper.GetTable(sql, null);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
              // sevenTime[i] = dt.Rows[i]["SEVEN_TIME"].ToString();
               enterprise[i] = Convert.ToInt32(dt.Rows[i]["COUNT_NUM"].ToString());

           }
           /***************企业近七天数据获取*************/

           /***************会员单位职业近七天数据获取*************/
             sql =
             "SELECT " +
            " days.createTime SEVEN_TIME ," +
            " nvl (m.COUNT, 0) COUNT_NUM" +
            " FROM " +
            "(SELECT " +
              "to_char (SYSDATE- LEVEL + 1, 'yyyy-mm-dd') createTime" +
            " FROM " +
              " DUAL connect BY LEVEL <= 7) days" +
            " LEFT JOIN " +
              "(SELECT " +
                " to_char (o.SEARCH_TIME, 'yyyy-mm-dd') createTime," +
                "nvl (COUNT(1), 0) COUNT" +
              " FROM " +
                "GXMC_INFO o" +
              " WHERE 1=1" +
              " GROUP BY to_char (o.SEARCH_TIME, 'yyyy-mm-dd')) m" +
              " ON days.createTime  = m.createTime " +
              " GROUP BY days.createTime, m.count" +
              " ORDER BY days.createTime";
           dt = OracleHelper.GetTable(sql, null);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
              // sevenTime[i] = dt.Rows[i]["SEVEN_TIME"].ToString();
               unit[i] = Convert.ToInt32(dt.Rows[i]["COUNT_NUM"].ToString());

           }
           
           /***************会员单位近七天数据获取*************/


           /***************自由职业近七天数据获取*************/

           sql =
           "SELECT " +
          " days.createTime SEVEN_TIME ," +
          " nvl (m.COUNT, 0) COUNT_NUM" +
          " FROM " +
          "(SELECT " +
            "to_char (SYSDATE- LEVEL + 1, 'yyyy-mm-dd') createTime" +
          " FROM " +
            " DUAL connect BY LEVEL <= 7) days" +
          " LEFT JOIN " +
            "(SELECT " +
              " to_char (o.SEARCH_TIME, 'yyyy-mm-dd') createTime," +
              "nvl (COUNT(1), 0) COUNT" +
            " FROM " +
              "GXFW_INFO o" +
            " WHERE 1=1" +
            " GROUP BY to_char (o.SEARCH_TIME, 'yyyy-mm-dd')) m" +
            " ON days.createTime  = m.createTime " +
            " GROUP BY days.createTime, m.count" +
            " ORDER BY days.createTime";
           dt = OracleHelper.GetTable(sql, null);
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               // sevenTime[i] = dt.Rows[i]["SEVEN_TIME"].ToString();
               profession[i] = Convert.ToInt32(dt.Rows[i]["COUNT_NUM"].ToString());

           }

           /***************自由职业近七天数据获取*************/


           responseText = "[";
           for (int j = 0; j < sevenTime.Length; j++)
           {
               if (j == sevenTime.Length - 1)
               {
                   responseText += "{\"date\":\"" + sevenTime[j] + "\",\"person\":\"" + personal[j] + "\",\"enterprise\":\"" + enterprise[j] + "\",\"profession\":\"" + profession[j] + "\",\"unit\":\"" + unit[j] + "\"}";
               }
               else
               {
                   responseText += "{\"date\":\"" + sevenTime[j] + "\",\"person\":\"" + personal[j] + "\",\"enterprise\":\"" + enterprise[j] + "\",\"profession\":\"" + profession[j] + "\",\"unit\":\"" + unit[j] + "\"},";
               }
           }
           //    responseText +=
                   
           //        "{\"date\":\"3月15日\",\"person\":\"12\",\"enterprise\":\"2\",\"profession\":\"4\",\"unit\":\"7\"}" +
           //         "," +
           //          "{\"date\":\"3月16日\",\"person\":\"16\",\"enterprise\":\"4\",\"profession\":\"6\",\"unit\":\"9\"}" +
           //         "," +
           //          "{\"date\":\"3月17日\",\"person\":\"13\",\"enterprise\":\"1\",\"profession\":\"3\",\"unit\":\"6\"}" +
           //         "," +
           //          "{\"date\":\"3月18日\",\"person\":\"18\",\"enterprise\":\"6\",\"profession\":\"8\",\"unit\":\"11\"}" +
           //         "," +
           //          "{\"date\":\"3月19日\",\"person\":\"23\",\"enterprise\":\"11\",\"profession\":\"13\",\"unit\":\"16\"}" +
           //         "," +
           //          "{\"date\":\"3月20日\",\"person\":\"13\",\"enterprise\":\"1\",\"profession\":\"3\",\"unit\":\"6\"}" +
           //         "," +
           //          "{\"date\":\"3月21日\",\"person\":\"20\",\"enterprise\":\"9\",\"profession\":\"11\",\"unit\":\"14\"}" ;

                  
           //};
              responseText+="]";
           

           return responseText;
          
       }
       /// <summary>
       /// 统计会员分类数据
       /// </summary>
       /// <returns></returns>
       public string stasticCount()
       {
           string responseText = "";
           string sql = "";
           DataTable dt;
           string personcount = "";
           string enterprisecount = "";
           string professionalcount = "";
           string unitcount = "";
           /********个人会员数量*********/
           sql = string.Format("select count(*) from GX_USER where MEMBER_TYPE='个人会员'");
            dt = OracleHelper.GetTable(sql, null);
           personcount = dt.Rows[0][0].ToString();
           /********个人会员数量************/
           /********企业会员数量*********/
           sql = string.Format("select count(*) from GX_USER where MEMBER_TYPE='企业会员'");
            dt = OracleHelper.GetTable(sql, null);
            enterprisecount = dt.Rows[0][0].ToString();
           /********企业会员数量************/
            /********自由职业会员数量*********/
            sql = string.Format("select count(*) from GXFW_INFO");
            dt = OracleHelper.GetTable(sql, null);
            professionalcount = dt.Rows[0][0].ToString();
           /********自由职业会员数量************/
            /********会员单位会员数量*********/
            sql = string.Format("select count(*) from GXMC_INFO");
            dt = OracleHelper.GetTable(sql, null);
            unitcount = dt.Rows[0][0].ToString();
           /********会员单位会员数量************/
            responseText += "{\"person\":\"" + personcount + "\",\"enterprise\":\"" + enterprisecount + "\",\"profession\":\"" + professionalcount + "\",\"unit\":\"" + unitcount + "\"}";
            return responseText;
       }

    }
}
