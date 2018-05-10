using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lyw.blueunion.backstagesystem.Model.ImageManage
{
   public class AdverSearchTerm
    {
        private string _adver_class;
        private string _adver_addtime_start;

        public string Adver_addtime_start
        {
            get { return _adver_addtime_start; }
            set { _adver_addtime_start = value; }
        }
        private string _adver_addtime_end;

        public string Adver_addtime_end
        {
            get { return _adver_addtime_end; }
            set { _adver_addtime_end = value; }
        }
        private string _adver_status;
       
     

        public string Adver_class
        {
            get { return _adver_class; }
            set { _adver_class = value; }
        }
       

      
        public string Adver_status
        {
            get { return _adver_status; }
            set { _adver_status = value; }
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
