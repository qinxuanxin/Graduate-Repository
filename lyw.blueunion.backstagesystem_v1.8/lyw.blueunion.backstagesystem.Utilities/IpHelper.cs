using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace lyw.blueunion.backstagesystem.Utilities
{
  public  class IpHelper
    {
      public class ipInfo
      {
          public string address { get; set; }
          public location content { get; set; }
          public string status { get; set; }
       
      }
      public class location
      {
          public string address { get; set; }
          public detail_address address_detail { get; set; }
          public axreiy point { get; set; } 
      }
      public class detail_address
      {
          public string city { get; set; }
          public string city_code { get; set; }
          public string district { get; set; }
          public string street_number { get; set; }
          public string street { get; set; }
          public string province { get; set; }
      }
      public class axreiy
      {
          public string x { get; set; }
          public string y { get; set; }
      }
        /// <summary>  
        /// 获取真ip  
        /// </summary>  
        /// <returns></returns>  
        public string GetRealIP()
        {
            string result = String.Empty;
            result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //可能有代理   
            if (!string.IsNullOrWhiteSpace(result))
            {
                //没有"." 肯定是非IP格式  
                if (result.IndexOf(".") == -1)
                {
                    result = null;
                }
                else
                {
                    //有","，估计多个代理。取第一个不是内网的IP。  
                    if (result.IndexOf(",") != -1)
                    {
                        result = result.Replace(" ", string.Empty).Replace("\"", string.Empty);

                        string[] temparyip = result.Split(",;".ToCharArray());

                        if (temparyip != null && temparyip.Length > 0)
                        {
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                //找到不是内网的地址  
                                if (IsIPAddress(temparyip[i]) && temparyip[i].Substring(0, 3) != "10." && temparyip[i].Substring(0, 7) != "192.168" && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];
                                }
                            }
                        }
                    }
                    //代理即是IP格式  
                    else if (IsIPAddress(result))
                    {
                        return result;
                    }
                    //代理中的内容非IP  
                    else
                    {
                        result = null;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
        public bool IsIPAddress(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < 7 || str.Length > 15)
                return false;

            string regformat = @"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);

            return regex.IsMatch(str);
        }
        public string GetIp()
        {
            string result = String.Empty;
            HttpRequest  request=HttpContext.Current.Request;
            result =request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "0.0.0.0";
            }
            return result;
        }
      /// <summary>
      /// 获取ip地址所在地理位置
      /// </summary>
      /// <returns></returns>
        public string GetPosition(string ip)
        {
            HttpWebRequest httpRequest;
            HttpWebResponse httpResponse;
            StreamReader sr;
            string result = "";
            string key = "G2OTsRBuwNYPj7Ivi0a3a8r64rYvwdvW";//不知道这个应用ak是用来干嘛的
          //  string key = "kuKm6aVbirgcBHP9pCBBNi6E6sihrY4u";
            ipInfo ipfo;
        

          

            string requestUriString = string.Format(
                "https://api.map.baidu.com/location/ip?ip={0}&ak={1}&coor=bd09ll", ip, key
                );

            httpRequest = (HttpWebRequest)HttpWebRequest.Create(requestUriString);
            httpRequest.Method = "GET";

            using (WebResponse response = httpRequest.GetResponse())
            {
                httpResponse = (HttpWebResponse)response;
                sr = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd();

            }
             ipfo = JsonConvert.DeserializeObject<ipInfo>(result);
            string resul="";
            if (ipfo.content!=null)
                resul = ipfo.content.address_detail.province + ipfo.content.address_detail.city;
            else
                resul = "查不到次用户地址";
            return resul;
        }
    }
}
