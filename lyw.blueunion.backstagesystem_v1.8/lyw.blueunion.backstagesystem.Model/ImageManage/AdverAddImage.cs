using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lyw.blueunion.backstagesystem.Model.ImageManage
{
  public  class AdverAddImage
    {
        private string _adver_class;
        private string _width;
        private string _high;
        private string _link_address;
        private string _is_show;
        private string _image_name;

        public string Image_name
        {
            get { return _image_name; }
            set { _image_name = value; }
        }
        public string Is_show
        {
            get { return _is_show; }
            set { _is_show = value; }
        } 
        public string Link_address
        {
            get { return _link_address; }
            set { _link_address = value; }
        } 
        public string High
        {
            get { return _high; }
            set { _high = value; }
        }
        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public string Adver_class
        {
            get { return _adver_class; }
            set { _adver_class = value; }
        }
    }
}
