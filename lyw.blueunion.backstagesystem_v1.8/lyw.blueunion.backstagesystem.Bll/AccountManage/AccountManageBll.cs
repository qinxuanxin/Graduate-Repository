using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using lyw.blueunion.backstagesystem.Model.Login;
using lyw.blueunion.backstagesystem.Dal.AccountManage;
namespace lyw.blueunion.backstagesystem.Bll.AccountManage
{
   public class AccountManageBll
    {
        AccountManageDal accountdal = new AccountManageDal();
        /// <summary>
        /// 账号注册
        /// </summary>
        /// <param name="police"></param>
        /// <returns></returns>
        public bool AccountRegister(Login_AdminModel adminmodel, string filenames,string username,string ip)
        {
            bool bl = false;
            bl = accountdal.AccountRegister(adminmodel, filenames,username,ip);
            return bl;
        }

        /// <summary>
        /// 显示登陆的账号信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Login_AdminModel AccountEdit(string guid)
        {
            Login_AdminModel usfo = new Login_AdminModel();
            DataTable dt = accountdal.AccountEdit(guid);
            if (dt.Rows.Count == 0)
            {
                usfo.bl =false;
                usfo.backmessage = "查无此用户";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                usfo.Admin_name = dt.Rows[i]["ADMIN_NAME"].ToString();
                usfo.Admin_password = dt.Rows[i]["ADMIN_PASSWORD"].ToString();
                usfo.Admin_id = dt.Rows[i]["ADMIN_ID"].ToString();
                usfo.Guid = dt.Rows[i]["GUID"].ToString();
                usfo.bl = true;
                usfo.backmessage = "查到正确信息";
            }
            return usfo;
        }

        /// <summary>
        /// 修改账号信息
        /// </summary>
        /// <param name="police"></param>
        /// <returns></returns>
        public bool AccountEdit(Login_AdminModel adminmodel,string username,string ip)
        {
            bool bl = false;
            int flag = accountdal.AccountEdit(adminmodel,username,ip);
            if (flag != 0)
                bl = true;
            return bl;
        }



        /// <summary>
        /// 删除账号信息
        /// </summary>
        /// <param name="police_name"></param>
        /// <returns></returns>
        public bool AccountDelete(string guid,string username,string ip)
        {
            bool bl = false;
            int flag = accountdal.AccountDelete(guid,username,ip);
            if (flag != 0)
                bl = true;
            return bl;
        }
        /// <summary>
        /// 带条件的查询账号信息
        /// </summary>
        /// <param name="policenum"></param>
        /// <param name="roleid"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public string SearchAccountInfo(string admin_name, string admin_id)
        {
            string responseText = "";
            responseText = accountdal.SearchAccountInfo(admin_name, admin_id);
            return responseText;
        }
       /// <summary>
        /// 获取所有权限描述
       /// </summary>
       /// <returns></returns>
        public string accountAuthorityExplain()
        {
            string responseText = "";
            responseText = accountdal.accountAuthorityExplain();
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
            responseText = accountdal.accountHasAuthority(user_id);
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
            responseText = accountdal.accountSaveAuthority(authority_id, role_id);
            return responseText;
        }
    }
}
