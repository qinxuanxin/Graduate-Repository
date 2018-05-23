using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lyw.blueunion.backstagesystem.Model.Order
{
   public class AddInstrument
    {
        private string _instrument_id;

        public string Instrument_id
        {
            get { return _instrument_id; }
            set { _instrument_id = value; }
        }

   


        private string _user_phone;

        public string User_phone
        {
            get { return _user_phone; }
            set { _user_phone = value; }
        }

        private string _regtime_start;

        public string Regtime_start
        {
            get { return _regtime_start; }
            set { _regtime_start = value; }
        }



        private string _regtime_end;

        public string Regtime_end
        {
            get { return _regtime_end; }
            set { _regtime_end = value; }
        }




        private string _auditor_status;

        public string Auditor_status
        {
            get { return _auditor_status; }
            set { _auditor_status = value; }
        }

     
      
     
        private string _pagesize;

        public string Pagesize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        private string _nowpage;

        public string Nowpage
        {
            get { return _nowpage; }
            set { _nowpage = value; }
        }
    }
}
