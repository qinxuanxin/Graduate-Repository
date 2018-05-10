using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using lyw.blueunion.backstagesystem.Model.Login;
using lyw.blueunion.backstagesystem.Dal.Login;

namespace lyw.blueunion.backstagesystem.Bll.Login
{
   public class LoginBll
   {
       LoginDal lgDal = new LoginDal();
       public Login_AdminModel loginCheckUser(string user_name, string user_password)
       {
           Login_AdminModel usfo = new Login_AdminModel();
         

         
           DataTable dt = lgDal.loginCheckUser(user_name);
           if (dt.Rows.Count==0)
           {
               usfo.bl = false;
               usfo.backmessage = "查无此用户";
               return usfo;
           }
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               usfo.Admin_name = dt.Rows[i]["ADMIN_NAME"].ToString();

               usfo.Admin_password = dt.Rows[i]["ADMIN_PASSWORD"].ToString();
               usfo.Admin_id = dt.Rows[i]["ADMIN_ID"].ToString();
               usfo.Guid = dt.Rows[i]["GUID"].ToString();
              
           }
           if (usfo.Admin_name == user_name)
           {
               if (usfo.Admin_password == user_password)
               {
                   usfo.bl = true;
                   usfo.backmessage = "正确";
               }
               else
               {
                   usfo.bl = false;
                   usfo.backmessage = "密码输入不正确";
               }

           }
           else
           {
               usfo.bl = false;
               usfo.backmessage = "用户名输入不正确";
           }
              
           return usfo;
       }
       /// <summary>
       /// 记录客户端用户登录日志
       /// </summary>
       /// <param name="record"></param>
       /// <returns></returns>
       public string loginRecord(Login_Record record)
       {
           string responseText = "";
          responseText=lgDal.loginRecord(record);
          return responseText; 
              
       }

    }
}
