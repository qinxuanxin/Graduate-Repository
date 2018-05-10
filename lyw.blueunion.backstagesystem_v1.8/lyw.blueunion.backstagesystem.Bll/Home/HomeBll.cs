using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lyw.blueunion.backstagesystem.Dal.Home;
namespace lyw.blueunion.backstagesystem.Bll.Home
{
  public  class HomeBll
    {
      HomeDal hdal = new HomeDal();
    /// <summary>
      /// 保存修改的密码
    /// </summary>
    /// <param name="initsecret"></param>
    /// <param name="newsecret"></param>
    /// <param name="guid"></param>
    /// <param name="username"></param>
    /// <returns></returns>
      public string saveSecret(string initsecret, string newsecret, string guid, string username,string ip)
      {
          string responseText = "";
            responseText=hdal.saveSecret(initsecret,newsecret,guid,username,ip);
          return responseText;
      }
      /// <summary>
      /// 查询用户登录记录
      /// </summary>
      /// <param name="guid"></param>
      /// <returns></returns>
      public string searchUserRecord(string guid, string nowpage, string pagesize)
      {
          string responseText = "";
          int in_nowpage = Convert.ToInt32(nowpage);
          int in_pagesize = Convert.ToInt32(pagesize);
          responseText = hdal.searchUserRecord(guid, in_nowpage, in_pagesize);
          return responseText;
      }
      /// <summary>
      /// 会员总数量
      /// </summary>
      /// <returns></returns>
      public string allCount()
      {
          string responseText = "";
          responseText = hdal.allCount();
          return responseText;
      }
      /// <summary>
      /// 今天的新增的会员/订单数量
      /// </summary>
      /// <returns></returns>
      public string todayCount()
      {
          string responseText = "";
          responseText = hdal.todayCount();
          return responseText;
      }
      /// <summary>
      /// 请求操作日志
      /// </summary>
      /// <returns></returns>
      public string requestOperationLog()
      {
          string responseText = "";
          responseText = hdal.requestOperationLog();
          return responseText;
      }
      /// <summary>
      /// 获得所有用户
      /// </summary>
      /// <returns></returns>
      public string requestAllAccount()
      {
          string responseText = "";
          responseText = hdal.requestAllAccount();
          return responseText;
      
      }
      /// <summary>
      /// 请求待条件的操作日志
      /// </summary>
      /// <param name="admin_name"></param>
      /// <param name="oper_time_start"></param>
      /// <param name="oper_time_end"></param>
      /// <param name="nowpage"></param>
      /// <param name="pagesize"></param>
      /// <returns></returns>
      public string requestMoreOperationLog(string admin_name, string oper_time_start, string oper_time_end, string nowpage, string pagesize)
      {
          string responseText = "";
          int nowpage1 = 0;
          int pagesize1 = 0;
          if (string.IsNullOrEmpty(pagesize))
              pagesize1 = 10;
          else
              pagesize1 = Convert.ToInt32(pagesize);

          if (string.IsNullOrEmpty(nowpage))
              nowpage1 = 1;
          else
              nowpage1 = Convert.ToInt32(nowpage);

          responseText = hdal.requestMoreOperationLog(admin_name, oper_time_start, oper_time_end, nowpage1, pagesize1);
          return responseText;
      }

    }
}
