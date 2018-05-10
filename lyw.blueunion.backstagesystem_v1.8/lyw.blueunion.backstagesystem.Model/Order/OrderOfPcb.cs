using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lyw.blueunion.backstagesystem.Model.Order
{
    public class OrderOfPcb
    {


        private string _order_num;

        public string Order_num
        {
            get { return _order_num; }
            set { _order_num = value; }
        }


        private string _order_phone;

        public string Order_phone
        {
            get { return _order_phone; }
            set { _order_phone = value; }
        }


        private string _order_regtime_start;

        public string Order_regtime_start
        {
            get { return _order_regtime_start; }
            set { _order_regtime_start = value; }
        }

        private string _order_regtime_end;

        public string Order_regtime_end
        {
            get { return _order_regtime_end; }
            set { _order_regtime_end = value; }
        }


        private string _order_status;

        public string Order_status
        {
            get { return _order_status; }
            set { _order_status = value; }
        }
        private string _pay_status;

        public string Pay_status
        {
            get { return _pay_status; }
            set { _pay_status = value; }
        }
        private string _shipping_status;

        public string Shipping_status
        {
            get { return _shipping_status; }
            set { _shipping_status = value; }
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
        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
       
    }
}
