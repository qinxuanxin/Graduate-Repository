using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lyw.blueunion.backstagesystem.Model.ImageManage;
using lyw.blueunion.backstagesystem.Dal.ImageManage;
namespace lyw.blueunion.backstagesystem.Bll.ImageManage
{
  public   class ImageManageBll
    {
      ImageManageDal imagedal = new ImageManageDal();


      /// <summary>
      /// 获取所有分类
      /// </summary>
      /// <returns></returns>
      public string adverClassData()
      {

          string responseText = "";
          responseText = imagedal.adverClassData();
          return responseText;
      }


      /// <summary>
      /// 获取带条件的广告位信息
      /// </summary>
      /// <param name="adverterm"></param>
      /// <returns></returns>
      public string adverListData(AdverSearchTerm adverterm)
      {
          string responseText = "";
          responseText = imagedal.adverListData(adverterm);
          return responseText;
      }
      /// <summary>
      /// 添加广告位图片
      /// </summary>
      /// <param name="addimage"></param>
      /// <param name="image_name"></param>
      /// <returns></returns>
      public string adverAddImage(AdverAddImage addimage, string image_name,string username,string ip)
      {
          string responseText = "";
          responseText = imagedal.adverAddImage(addimage,image_name,username,ip);
          return responseText;
      
      }
      /// <summary>
      /// 删除图片记录
      /// </summary>
      /// <param name="image_id"></param>
      /// <returns></returns>
      public string adverDelImage(string image_id,string username,string ip)
      {
          string responseText = "";
          responseText = imagedal.adverDelImage(image_id,username,ip);
          return responseText;
      }
      /// <summary>
      /// 设置图片状态
      /// </summary>
      /// <param name="image_id"></param>
      /// <returns></returns>
      public string adverSetUpStatus(string image_id,string username,string ip)
      {
          string responseText = "";
          responseText = imagedal.adverSetUpStatus(image_id,username,ip);
          return responseText;
      }
      /// <summary>
      /// 各类型广告图片数量
      /// </summary>
      /// <returns></returns>
      public string allClassCount()
      {
          string responseText = "";
          responseText = imagedal.allClassCount();
          return responseText;
      }
      /// <summary>
      /// 获取广告位分类数据
      /// </summary>
      /// <param name="class_name"></param>
      /// <param name="add_time"></param>
      /// <param name="pagesize"></param>
      /// <param name="nowpage"></param>
      /// <returns></returns>
      public string adverClassListData(string class_name, string adver_addtime_start, string adver_addtime_end, string pagesize, string nowpage)
      {
          string responseText = "";
          responseText = imagedal.adverClassListData(class_name, adver_addtime_start, adver_addtime_end, pagesize, nowpage);
          return responseText;
      }
      /// <summary>
      /// 添加分类
      /// </summary>
      /// <param name="class_name"></param>
      /// <param name="class_explain"></param>
      /// <returns></returns>
      public string adverAddClass(string class_name, string class_explain,string username,string ip)
      {
          string responseText = "";
          responseText = imagedal.adverAddClass(class_name,class_explain,username,ip);
          return responseText;
      }
      /// <summary>
      /// 删除分类记录
      /// </summary>
      /// <param name="class_id"></param>
      /// <returns></returns>
      public string adverDelClass(string class_id,string username,string ip)
      {
          string responseText = "";
          responseText = imagedal.adverDelClass(class_id,username,ip);
          return responseText;
      }
      
    }
}
