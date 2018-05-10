using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lyw.blueunion.backstagesystem.Model.Login
{
   public class Login_AdminModel
    {
//       GUID	VARCHAR2(38 BYTE)	No		1	1	
//ADMIN_NAME	VARCHAR2(20 BYTE)	Yes		2		用户姓名
//ADMIIN_PASSWORD	VARCHAR2(20 BYTE)	Yes		3		用户密码
//ADMIN_ID	VARCHAR2(20 BYTE)	Yes		4		用户角色，权限

     private string _guid;

        public string Guid
        {
          get { return _guid; }
          set { _guid = value; }
        }
       private string _admin_name;

        public string Admin_name
        {
          get { return _admin_name; }
          set { _admin_name = value; }
        }
       private string _admin_password;

        public string Admin_password
        {
          get { return _admin_password; }
          set { _admin_password = value; }
        }
       private string _admin_id;

        public string Admin_id
        {
          get { return _admin_id; }
          set { _admin_id = value; }
        }
        public bool bl;
        public string backmessage;
    }
}
