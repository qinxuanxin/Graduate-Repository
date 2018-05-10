using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using lyw.blueunion.backstagesystem.Utilities;
namespace lyw.blueunion.backstagesystem.Dal.Log
{
   public class LogDal
    {
       public bool setOperationLog(string username,string log,string ip)
       {
           bool temp;
           string addtime = DateTime.Now.ToString();
           string oracle = string.Format("insert into GX_OPERATION_LOG(LOG,IP,ADD_TIME,USER_NAME) values('{0}','{1}','{2}','{3}')", log, ip, addtime,username);
           int flag = OracleHelper.ExecuteNonQuery(oracle, null);
           if (flag != 0)
               temp = true;
           else
               temp = false;
           return temp;
       }
    }
}
