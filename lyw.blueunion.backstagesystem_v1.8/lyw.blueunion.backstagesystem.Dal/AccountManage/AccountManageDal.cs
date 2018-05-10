using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using lyw.blueunion.backstagesystem.Model.Login;
using lyw.blueunion.backstagesystem.Dal.Log;
using lyw.blueunion.backstagesystem.Utilities;

namespace lyw.blueunion.backstagesystem.Dal.AccountManage
{
   public class AccountManageDal
    {
       LogDal lgdal = new LogDal();
        /// <summary>
        /// 账号注册
        /// </summary>
        /// <param name="police"></param>
        /// <returns></returns>
       public bool AccountRegister(Login_AdminModel adminmodel, string filenames,string username,string ip)
        {
            bool bl = false;
            string sql = "select * from GX_ADMIN_ACCOUNT where ADMIN_NAME=:admin_name";
            string log = "";
            string time = DateTime.Now.ToString();
            bool logFlag =false;
            DataTable dt = OracleHelper.GetTable(sql, new OracleParameter(":admin_name",adminmodel.Admin_name));
            if (dt.Rows.Count != 0)
            {
                bl = false;
              
            }
            else
            {
                string oracle = "insert into GX_ADMIN_ACCOUNT(GUID,ADMIN_NAME,ADMIN_PASSWORD,ADMIN_ID,HEAD,REG_TIME) " +
                         "values(:guid,:adminname,:adminpassword,:adminid,:head,:reg_time)";
                OracleParameter[] orpa ={
                 new   OracleParameter( ":guid",OracleType.VarChar,400),
                  new   OracleParameter( ":adminname",OracleType.VarChar,400),
                    new   OracleParameter( ":adminpassword",OracleType.VarChar,400),
                    new   OracleParameter( ":adminid",OracleType.VarChar,400),
                      new   OracleParameter( ":head",OracleType.VarChar,400),
                    new   OracleParameter( ":reg_time",OracleType.VarChar,400)
              };
                orpa[0].Value = Guid.NewGuid().ToString("D");
                orpa[1].Value = adminmodel.Admin_name;
                orpa[2].Value = adminmodel.Admin_password;
                orpa[3].Value = adminmodel.Admin_id;
                orpa[4].Value = filenames;
                orpa[5].Value = time;
                int flag = OracleHelper.ExecuteNonQuery(oracle, orpa);
                if (flag != 0)
                {
                    bl = true;
                    logFlag = true;
                }
               
            }
            if (logFlag)
            {
                log = username + " 成功注册了一个名字为" + adminmodel.Admin_name + "的新用户";
            }
           else
                log = username + " 注册新用户失败";
            lgdal.setOperationLog(username,log, ip);
            return bl;
        }
        /// <summary>
        /// 显示需要修改的账号信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable AccountEdit(string guid)
        {
            string sql = "select * from GX_ADMIN_ACCOUNT where GUID=:guid";
            DataTable dt = OracleHelper.GetTable(sql, new OracleParameter(":guid", guid));
            return dt;
        }
        /// <summary>
        /// 修改账号信息
        /// </summary>
        /// <param name="police"></param>
        /// <returns></returns>
        public int AccountEdit(Login_AdminModel adminmodel,string username,string ip)
        {
            int flag = 0;
            string log = "";
            bool logFlag = false;
            string sql = "update GX_ADMIN_ACCOUNT set ADMIN_NAME=:adminname,ADMIN_PASSWORD=:adminpassword,ADMIN_ID=:adminid where GUID=:guid";
            OracleParameter[] orpa ={

                 new   OracleParameter( ":adminname",OracleType.VarChar,400),
                  new   OracleParameter( ":adminpassword",OracleType.VarChar,400),
                    new   OracleParameter( ":adminid",OracleType.VarChar,400),   
                    new   OracleParameter( ":guid",OracleType.VarChar,400)
                
                 
                 
              };

            orpa[0].Value = adminmodel.Admin_name;
            orpa[1].Value = adminmodel.Admin_password;
            orpa[2].Value = adminmodel.Admin_id;
            orpa[3].Value = adminmodel.Guid;
          

            flag = OracleHelper.ExecuteNonQuery(sql, orpa);
            if (flag > 0)
                logFlag = true;
            if (logFlag)
            {
                log = username + " 修改id为" + adminmodel.Guid + "的用户信息成功";
            }
            else
                log = username + " 修改id为" + adminmodel.Guid + "的用户信息失败";
            lgdal.setOperationLog(username,log, ip);
            return flag;

        }



        /// <summary>
        /// 删除账号信息
        /// </summary>
        /// <param name="police_name"></param>
        /// <returns></returns>
        public int AccountDelete(string guid,string username,string ip)
        {
            int flag = 0;
            bool logFlag = false;
            string log = "";
            string sql = "delete from GX_ADMIN_ACCOUNT where GUID=:guid";
            flag = OracleHelper.ExecuteNonQuery(sql, new OracleParameter(":guid", guid));
            if (flag > 0)
                logFlag = true;
            if (logFlag)
            {
                log = username + " 删除id为" + guid + "的用户信息成功";
            }
            else
                log = username + " 删除id为" + guid + "的用户信息失败";
            lgdal.setOperationLog(username,log, ip);
            return flag;
        }
        /// <summary>
        /// 带条件查询账号信息
        /// </summary>
        /// <returns></returns>
        public string SearchAccountInfo(string admin_name, string admin_id)
        {

            string responseText = "";
            string Select_Sql_Con = "";
            if (admin_name != "")
            {
                Select_Sql_Con += string.Format("and ADMIN_NAME='{0}'", admin_name);

            }
            if (admin_id != "00")
            {
                Select_Sql_Con += string.Format("and ADMIN_ID='{0}'", admin_id);

            }

            string sql = "select * from GX_ADMIN_ACCOUNT  where 1=1 " + Select_Sql_Con;
            DataTable dt = OracleHelper.GetTable(sql, null);
            responseText = JsonHelper.getRecordJson(dt);
            if (dt.Rows.Count != 0)
            {
                responseText = "{\"msg\":\"success\",\"status\":\"正确找出数据\",\"content\":[" + responseText + "]}";
            }
            else
            {
                responseText = "{\"msg\":\"fail\",\"status\":\"查无数据\",\"content\":[" + responseText + "]}";
            }
          
            return responseText;

        }
       /// <summary>
       /// 获取所有权限描述
       /// </summary>
       /// <returns></returns>
        public string accountAuthorityExplain()
        {
            string responseText = "";
            string sql = string.Format("select * from GX_ADMIN_AUTHORITY  where 1=1");
            DataTable dt = OracleHelper.GetTable(sql, null);
            responseText = JsonHelper.getRecordJson(dt);
            if (dt.Rows.Count != 0)
            {
                responseText = "{\"msg\":\"success\",\"status\":\"正确找出权限\",\"content\":[" + responseText + "]}";
            }
            else
            {
                responseText = "{\"msg\":\"fail\",\"status\":\"查无权限\",\"content\":[" + responseText + "]}";
            }

            return responseText;
        }
       /// <summary>
        /// 获取角色所拥有的权限
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
        public string accountHasAuthority(string user_id)
        {
            string responseText = "";
            string sql =string.Format("select * from GX_ADMIN_ROLE_AUTHORITY  where ROLE_ID='{0}'",user_id);
            DataTable dt = OracleHelper.GetTable(sql, null);
            responseText = JsonHelper.getRecordJson(dt);
            if (dt.Rows.Count != 0)
            {
                responseText = "{\"msg\":\"success\",\"status\":\"正确找出权限\",\"content\":" + responseText + "}";
            }
            else
            {
                responseText = "{\"msg\":\"fail\",\"status\":\"查无权限\",\"content\":" + responseText + "}";
            }

            return responseText;
        }
       /// <summary>
       /// 保存设置的权限
       /// </summary>
       /// <param name="authority_id"></param>
       /// <param name="role_id"></param>
       /// <returns></returns>
        public string accountSaveAuthority(string authority_id, string role_id)
        {
            string responseText = "";
            string sql = string.Format("update GX_ADMIN_ROLE_AUTHORITY set AUTHORITY ='{0}' where ROLE_ID='{1}'", authority_id, role_id);
            int flag = OracleHelper.ExecuteNonQuery(sql, null);
            if(flag>0)
                responseText = "{\"msg\":\"success\",\"status\":\"设置权限成功\"}";
            else
                responseText = "{\"msg\":\"fail\",\"status\":\"设置权限失败,数据库没有正确更新\"}";
            return responseText;
        }
    }
}
