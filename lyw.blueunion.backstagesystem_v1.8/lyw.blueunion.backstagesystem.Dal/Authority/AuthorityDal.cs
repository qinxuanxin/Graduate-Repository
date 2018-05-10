using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using lyw.blueunion.backstagesystem.Utilities;
namespace lyw.blueunion.backstagesystem.Dal.Authority
{
   public class AuthorityDal
    {
       /// <summary>
       /// 获取当前行为存在哪些角色可以访问
       /// </summary>
       /// <param name="actionName"></param>
       /// <returns></returns>
       public string VerificationAuthority(string actionName)
       {
           string sql = string.Format("select * from GX_ADMIN_ROLE_AUTHORITY where ROLE_ID='{0}'", actionName);
           DataTable dt = OracleHelper.GetTable(sql);
           string responseText = JsonHelper.getRecordJson(dt);
           return responseText;
       }
    }
}
