using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lyw.blueunion.backstagesystem.Dal.Log;
namespace lyw.blueunion.backstagesystem.Bll.Log
{
   public class LogBll
    {
       LogDal lgdal = new LogDal();
       public bool setOperationLog(string username,string log, string ip)
       {
           bool flag = lgdal.setOperationLog(username,log, ip);
           return flag;
       }
    }
}
