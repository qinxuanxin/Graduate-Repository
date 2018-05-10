using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lyw.blueunion.backstagesystem.Model.Login
{
   public class Login_Record
    {
        private string _guid;
        private string _user_name;
        private string _admin_id;
        private string _ip;
        private string _address;
        private string _sigin_content;
        private string _sigin_time;

        public string Sigin_time
        {
            get { return _sigin_time; }
            set { _sigin_time = value; }
        }

        public string Sigin_content
        {
            get { return _sigin_content; }
            set { _sigin_content = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        public string Admin_id
        {
            get { return _admin_id; }
            set { _admin_id = value; }
        }
        public string User_name
        {
            get { return _user_name; }
            set { _user_name = value; }
        }

        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
    }
}
