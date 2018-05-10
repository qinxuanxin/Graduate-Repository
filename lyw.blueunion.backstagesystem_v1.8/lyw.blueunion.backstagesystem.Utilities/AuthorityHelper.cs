using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace lyw.blueunion.backstagesystem.Utilities
{
   public class AuthorityHelper
    {
     
       public static bool validateAuthority(string info)
       {
           // 获取用户信息
           HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["ADMIN_ID"];

         
           JObject jo = (JObject)JsonConvert.DeserializeObject(info);
           string roleid = jo["ADMIN_ID"].ToString();
           string[] role = roleid.Split(',');
           bool hasflag = false;
           foreach (string i in role)
           {
               if (i.Equals(cookie.Value))
               {
                   hasflag = true;
                   break;
               }
               else
               {
                   hasflag = false;
               }
           }
           return hasflag;
       }
    }
}
