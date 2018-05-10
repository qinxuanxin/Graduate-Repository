using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using lyw.blueunion.backstagesystem.Dal;
using lyw.blueunion.backstagesystem.Utilities;
using lyw.blueunion.backstagesystem.Model.Login;
namespace lyw.blueunion.backstagesystem.Dal.Login
{
  public  class LoginDal
    {
      public DataTable loginCheckUser(string admin_name)
      {
          string sql = "select * from GX_ADMIN_ACCOUNT where ADMIN_NAME=:admin_name";

          DataTable dt = OracleHelper.GetTable(sql, new OracleParameter(":admin_name", admin_name));

          return dt;
      }
      /// <summary>
      /// 记录客户端用户登录日志
      /// </summary>
      /// <param name="record"></param>
      /// <returns></returns>
      public string loginRecord(Login_Record record)
      {
          string responseText = "";
          int flag;
       
          string sql = string.Format("insert into GX_ADMIN_ACCOUNT_RECORD(GUID,USER_NAME,ADMIN_ID,IP,ADDRESS,SIGIN_CONTENT,SIGIN_TIME) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",record.Guid,record.User_name,record.Admin_id,record.Ip,record.Address,record.Sigin_content,record.Sigin_time);
          flag = OracleHelper.ExecuteNonQuery(sql, null);
          if (flag > 0)
              responseText = "[{\"msg\":\"success\"}]";
          else
              responseText = "[{\"msg\":\"fail\"}]";
          return responseText;
      }
    }
}
