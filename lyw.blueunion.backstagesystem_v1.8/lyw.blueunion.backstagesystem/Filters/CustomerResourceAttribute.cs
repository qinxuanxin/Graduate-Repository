using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lyw.blueunion.backstagesystem.Filters
{
    /// <summary>
    /// CustomerResourceAttribute
    /// 
    /// 自定义的对方法应用的属性，在Action上标注权限菜单对应的Code
    /// 
    /// 修改纪录
    /// 
    /// 2015-10-11 版本：1.0 SongBiao 创建文件。   
    /// 
    /// <author>
    ///     <name>dan</name>
    ///     <date>2015-10-11</date>
    /// </author>
    /// </summary>

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class CustomerResourceAttribute : Attribute
    {
        private readonly string _resourceName;
        private readonly string _resourceId;

      


        public CustomerResourceAttribute(string resourceName,string resourceId)
        {
            _resourceName = resourceName;
            _resourceId = resourceId;
        }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName
        {
            get { return _resourceName; }
        }
        /// <summary>
        /// 资源id
        /// </summary>
        public string ResourceId
        {
            get { return _resourceId; }
        } 
        /// <summary>
        /// 资源描述
        /// </summary>
        public string Descript { get; set; }
    }
}