using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Data;
namespace lyw.blueunion.backstagesystem.Utilities
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        /// <summary>
        /// 将datatable数据拼接json格式字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string getRecordJson(DataTable dt)
        {
            string responseText = "";
            string[] message = new string[dt.Rows.Count];
            int i = 0, j = 0;
            //封装每个message元素为json格式
            for (i = 0; i < dt.Rows.Count; i++)
            {
                for (j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == dt.Columns.Count - 1)
                    {
                        message[i] += "\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"";
                    }
                    else
                    {
                        message[i] += "\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\",";
                    }
                }
                message[i] = "{" + message[i] + "}";
            }
            //封装整个message数组成responseText   
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (i == dt.Rows.Count - 1)
                {
                    responseText += message[i];
                }
                else
                {
                    responseText += message[i] + ",";
                }
            }
            return responseText;
        }
        public static string getSortsNameJson(DataTable dt)
        {
            string responseText = "";
            string[] message = new string[dt.Rows.Count];
            int i = 0, j = 0;
            //封装每个message元素为json格式
            for (i = 0; i < dt.Rows.Count; i++)
            {
              
                        //single_item:"↵<li class="fl">↵<div class="product-div">↵  <a href="/v2/index.php?m=default&c=goods&a=index&id=19933&u=0">↵  		<img class="lazy" src="http://gi3.mlist.alicdn.com/bao/uploaded/i3/TB1OP39FFXXXXcTXpXXXXXXXXXX_!!0-item_pic.jpg" alt="澳芙园 孕妇洁面乳 纯天然保湿控油孕期专用补水孕妇洗面奶 正品">↵  </a>↵  <a href="/v2/index.php?m=default&c=goods&a=index&id=19933&u=0"><h4>澳芙园 孕妇洁面乳 纯天然保湿控油孕期专用补水孕妇洗面奶 正品</h4></a>↵  <p><span>￥36.00元</span></p>↵  </div>↵</li>↵"

                        message[i]+="\"single_item\":\"<li data-cat_id="+dt.Rows[i]["CAT_ID"].ToString()+">"+dt.Rows[i]["CAT_NAME"].ToString()+"↵</li>↵\"";
                        //message[i] += "\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"";
                  
               
                message[i] = "{" + message[i] + "}";
            }
            //封装整个message数组成responseText   
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (i == dt.Rows.Count - 1)
                {
                    responseText += message[i];
                }
                else
                {
                    responseText += message[i] + ",";
                }
            }
            return responseText;
        }

    }
}
