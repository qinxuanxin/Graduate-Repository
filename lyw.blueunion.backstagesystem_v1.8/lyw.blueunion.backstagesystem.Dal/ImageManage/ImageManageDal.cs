using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using lyw.blueunion.backstagesystem.Model.ImageManage;
using lyw.blueunion.backstagesystem.Utilities;
using lyw.blueunion.backstagesystem.Dal.Log;
namespace lyw.blueunion.backstagesystem.Dal.ImageManage
{
  public  class ImageManageDal
  {
      LogDal lgdal = new LogDal();
      #region
      /// <summary>
      /// 获取所有分类
      /// </summary>
      /// <returns></returns>
      public string adverClassData()
      {
          string responseText = "";
          string sql = string.Format("select * from GX_AD_DIC  where CLASS_IS_SHOW='0'");
          DataTable dt = OracleHelper.GetTable(sql, null);
          if (dt.Rows.Count == 0)
              return "[{\"msg\":\"fail\",\"status\":\"分类数为零,请先添加分类\"}]";
          responseText = JsonHelper.getRecordJson(dt);
          responseText = "[{\"msg\":\"success\",\"status\":\"正确获取当前分类信息\",\"servers\":[" + responseText + "]}]";

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
          string Select_Sql = "";//条件语句
          int count = 0;                    //条件查询到的记录数
          int start = 0;  //起始查询条数
          int last = 0;      //最终查询条数
          int pagecount = 0;//总页数
          int pagesize = 0;
          int nowpage = 0;
          if (adverterm.Pagesize == "" || adverterm.Pagesize == null)
              pagesize = 10;
          else
              pagesize = Convert.ToInt32(adverterm.Pagesize);

          if (adverterm.Nowpage == "" || adverterm.Nowpage == null)
              nowpage = 1;
          else
              nowpage = Convert.ToInt32(adverterm.Nowpage);
          if (adverterm.Adver_class != "00" && adverterm.Adver_class != "" && adverterm.Adver_class != null)
              Select_Sql += string.Format(" and (CLASS_ID = '{0}')", adverterm.Adver_class);

          if (adverterm.Adver_addtime_start != "" && adverterm.Adver_addtime_start != null)
              Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') >='{0}'", adverterm.Adver_addtime_start);
          if (adverterm.Adver_addtime_end != "" && adverterm.Adver_addtime_end != null)
              Select_Sql += string.Format(" and to_char(to_date(ADD_TIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') <='{0}'", adverterm.Adver_addtime_end);

          if (adverterm.Adver_status != "00" && adverterm.Adver_status != "" && adverterm.Adver_status != null)
              Select_Sql += string.Format(" and (IS_SHOW = '{0}')", adverterm.Adver_status);


          string sql0 = "select count(*) from  GX_AD_VIEW where 1=1 and DEL_FLAG='0' and CLASS_IS_SHOW='0' " + Select_Sql;
          DataTable dt0 = OracleHelper.GetTable(sql0, null);
          count = Convert.ToInt32(dt0.Rows[0][0]);   //得到了全部的记录数
          if (count % pagesize == 0)                 //计算得到全部页数
              pagecount = count / pagesize;
          else
              pagecount = count / pagesize + 1;
          if (count == 0)
              start = 0;
          else
              start = (nowpage - 1) * pagesize + 1;         //计算该显示记录的行数范围;

          if (start <= count)
          {
              if (count - start >= pagesize)             //start起始 last结束
                  last = start + pagesize - 1;
              else
                  last = count;
          }
          string sql = string.Format("select * from(select a.*,rownum row_num from (select * from GX_AD_VIEW  where 1=1 and DEL_FLAG='0' and CLASS_IS_SHOW='0'  {0} order by ID asc) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);
          DataTable dt = OracleHelper.GetTable(sql, null);
          responseText = JsonHelper.getRecordJson(dt);
          responseText = "[{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}]";
          return responseText;
       
      }
      /// <summary>
      /// 添加广告位图片
      /// </summary>
      /// <param name="addimage"></param>
      /// <param name="image_name"></param>
      /// <returns></returns>
      public string adverAddImage(AdverAddImage addimage,string image_name,string username,string ip)
      {
          string responseText = "";
          string sql = "";
          string newsort = "";
          int flag = 0;
          DataTable dt;
          string log = "";
          bool logFlag = false;
          sql = string.Format("select * from GX_AD_POSITION where 1=1 and DEL_FLAG='0' and AD_IMAGE='{0}'", image_name);
           dt = OracleHelper.GetTable(sql, null);
          if (dt.Rows.Count>0)
              return "[{\"msg\":\"fail\",\"status\":\"该图片已存在服务器,可修改图片名字重新上传\"}]";
          sql = string.Format("select nvl(max(SORT),'0')  from GX_AD_POSITION where  DEL_FLAG='0' and CLASS_ID='{0}'", addimage.Adver_class);
           dt= OracleHelper.GetTable(sql, null);
           newsort = (Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString();
           string nowtime = DateTime.Now.ToString();
           sql = string.Format("insert into GX_AD_POSITION(AD_IMAGE,WIDTH,HIGH,CLASS_ID,ADD_TIME,SORT,LINK_ADDRESS,IS_SHOW) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", image_name, addimage.Width, addimage.High, addimage.Adver_class, nowtime,newsort,addimage.Link_address,addimage.Is_show);
          flag = OracleHelper.ExecuteNonQuery(sql, null);
          if (flag > 0)
          {
              logFlag = true;
              responseText = "[{\"msg\":\"success\",\"status\":\"上传成功\"}]";
          }
              
          else
              responseText = "[{\"msg\":\"fail\",\"status\":\"插入数据库失败\"}]";
          if (logFlag)
          {
              log = username + " 上传一张" + addimage.Adver_class + "分类且名字为" + image_name + "的图片成功";
          }
          else
              log = username + " 上传一张" + addimage.Adver_class + "分类且名字为" + image_name + "的图片失败";
          lgdal.setOperationLog(username,log, ip);
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
           string idnumwhen="";
           string idnumwhere = "";
           string log = "";
           bool logFlag = false;
           string[] imageIdArray=image_id.Split(',') ;
          //foreach(string i in imageIdArray)
          //  idnumwhen+="WHEN ID="+"\'"+i.ToString()+"\'"+" THEN '1'  ";
           for (int iwhere = 0; iwhere < imageIdArray.Length; iwhere++)
           {
               if (iwhere > 0)
                   idnumwhere += ",";
               idnumwhere += "\'" + imageIdArray[iwhere].ToString() + "\'";
           }
            
          
           //string sql = "update " + tableview + " set DEL_FLAG ='1' where ORDER_ID='" + order_id + "'";
           string sql = "UPDATE  GX_AD_POSITION set DEL_FLAG='1'   WHERE ID IN (" + idnumwhere + ")";
                 

          int flag = OracleHelper.ExecuteNonQuery(sql, null);
           if (flag > 0)
           {
               logFlag = true;
               responseText = "[{\"msg\":\"success\",\"status\":\"删除成功\"}]";
           }
             
           else
               responseText = "[{\"msg\":\"fail\",\"status\":\"删除失败\"}]";
           if (logFlag)
           {
               log = username + " 删除id为" + image_id + "的图片成功";
           }
           else
               log = username + " 删除id为" + image_id + "的图片失败";
           lgdal.setOperationLog(username,log, ip);
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
          string sql = "";
          string show = "";
          string log = "";
          bool logFlag = false;
          DataTable dt;
          sql = string.Format("select * from GX_AD_POSITION where 1=1 and DEL_FLAG='0' and ID='{0}'", image_id);       
          dt = OracleHelper.GetTable(sql, null);
          show = dt.Rows[0]["IS_SHOW"].ToString();
          if (show == "0")
              show = "1";
          else
              show = "0";
          sql = string.Format("update GX_AD_POSITION set IS_SHOW ='{0}' where ID='{1}'", show, image_id);
          int flag = OracleHelper.ExecuteNonQuery(sql, null);
          if (flag > 0)
          {
              responseText = "[{\"msg\":\"success\",\"status\":\"设置成功\"}]";
              logFlag = true;
          }
            
          else
              responseText = "[{\"msg\":\"fail\",\"status\":\"设置失败\"}]";
          if (logFlag)
          {
              log = username + " 失败设置了id为" + image_id+"的图片显示状态";
          }
          else
              log = username + " 成功设置了id为" + image_id + "的图片显示状态";
          lgdal.setOperationLog(username,log, ip);
          return responseText;
      }
      /// <summary>
      /// 各类型广告图片数量
      /// </summary>
      /// <returns></returns>
      public string allClassCount()
      {
          string responseText = "";
          string sql = "";
          DataTable dt;
          sql = string.Format("select CLASS_NAME, nvl(count(CLASS_NAME),0) as COUNT from GX_AD_VIEW where  DEL_FLAG='0' and CLASS_IS_SHOW='0' group by CLASS_NAME");
          dt = OracleHelper.GetTable(sql, null);
          if (dt.Rows.Count == 0)
          {
              responseText = "[{\"msg\":\"fail\",\"status\":\"查无数据\"}]";
          }
          responseText = JsonHelper.getRecordJson(dt);

          responseText = "[{\"msg\":\"success\",\"status\":\"查询成功\",\"servers\":[" + responseText + "]}]";
        
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
      public string adverClassListData(string class_name, string adver_addtime_start, string adver_addtime_end, string Pagesize, string Nowpage)
      {
        
          string responseText = "";
          string Select_Sql = "";//条件语句
          int count = 0;                    //条件查询到的记录数
          int start = 0;  //起始查询条数
          int last = 0;      //最终查询条数
          int pagecount = 0;//总页数
          int pagesize = 0;
          int nowpage = 0;
          if (Pagesize == "" || Pagesize == null)
              pagesize = 10;
          else
              pagesize = Convert.ToInt32(Pagesize);

          if (Nowpage == "" || Nowpage == null)
              nowpage = 1;
          else
              nowpage = Convert.ToInt32(Nowpage);


          if (adver_addtime_start != "" && adver_addtime_start != null)
              Select_Sql += string.Format(" and to_char(to_date(CLASS_ADDTIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') >='{0}'", adver_addtime_start);
          if (adver_addtime_end != "" && adver_addtime_end != null)
              Select_Sql += string.Format(" and to_char(to_date(CLASS_ADDTIME,'yyyy-MM-dd HH24:mi:ss'),'yyyy-MM-dd') <='{0}'", adver_addtime_end);

          if (class_name != "00" && class_name != "" && class_name != null)
              Select_Sql += string.Format(" and (CLASS_NAME = '{0}')", class_name);


          string sql0 = "select count(*) from  GX_AD_DIC where 1=1 and CLASS_IS_SHOW='0' " + Select_Sql;
          DataTable dt0 = OracleHelper.GetTable(sql0, null);
          count = Convert.ToInt32(dt0.Rows[0][0]);   //得到了全部的记录数
          if (count % pagesize == 0)                 //计算得到全部页数
              pagecount = count / pagesize;
          else
              pagecount = count / pagesize + 1;
          if (count == 0)
              start = 0;
          else
              start = (nowpage - 1) * pagesize + 1;         //计算该显示记录的行数范围;

          if (start <= count)
          {
              if (count - start >= pagesize)             //start起始 last结束
                  last = start + pagesize - 1;
              else
                  last = count;
          }
          string sqlcount = string.Format("select CLASS_NAME, nvl(count(CLASS_NAME),0) as CLASS_IMAGECOUNT from GX_AD_VIEW where DEL_FLAG='0' and CLASS_IS_SHOW='0'  group by CLASS_NAME ");
          DataTable dtcount = OracleHelper.GetTable(sqlcount, null);
          if (dtcount.Rows.Count != 0)
          {
              for (int i = 0; i < dtcount.Rows.Count; i++)
              {
                  string cname = dtcount.Rows[i]["CLASS_NAME"].ToString();
                  string ccount = dtcount.Rows[i]["CLASS_IMAGECOUNT"].ToString();
                  string sqlup = string.Format("update GX_AD_DIC set CLASS_IMAGE_COUNT ='{0}' where CLASS_NAME='{1}'", ccount, cname);
                  int flagup = OracleHelper.ExecuteNonQuery(sqlup, null);
              }
          }
       //   string sql = string.Format("select * from(select a.*,rownum row_num from (select a2.*,a1.CLASS_IMAGECOUNT from (select CLASS_NAME, nvl(count(CLASS_NAME),0) as CLASS_IMAGECOUNT from GX_AD_VIEW where DEL_FLAG='0' group by CLASS_NAME) a1,(select * from  GX_AD_DIC where 1=1 and CLASS_IS_SHOW='0' {0}) a2 where a1.CLASS_NAME=a2.CLASS_NAME order by a2.CLASS_ID asc ) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);
          string sql = string.Format("select * from(select a.*,rownum row_num from (select * from  GX_AD_DIC where 1=1 and CLASS_IS_SHOW='0' {0}   order by CLASS_ID asc ) a) b  where b.row_num between {1} and {2}", Select_Sql, start, last);
          DataTable dt = OracleHelper.GetTable(sql, null);
          responseText = JsonHelper.getRecordJson(dt);
          responseText = "[{\"msg\":\"success\",\"count\":" + count.ToString() + ",\"servers\":[" + responseText + "]}]";
         
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
          string sql = "";
          string newsort = "";
          int flag = 0;
          string log = "";
          bool logFlag = false;
          DataTable dt;
          sql = string.Format("select * from GX_AD_DIC where 1=1 and CLASS_IS_SHOW='0' and CLASS_NAME='{0}'", class_name);
          dt = OracleHelper.GetTable(sql, null);
          if (dt.Rows.Count > 0)
              return "[{\"msg\":\"fail\",\"status\":\"该分类名字已存在服务器,可修改分类名字重新添加\"}]";
          //sql = string.Format("select nvl(max(SORT),'0')  from GX_AD_POSITION where  DEL_FLAG='0' and CLASS_ID='{0}'", addimage.Adver_class);
          //dt = OracleHelper.GetTable(sql, null);
          //newsort = (Convert.ToInt32(dt.Rows[0][0].ToString()) + 1).ToString();
          string nowtime = DateTime.Now.ToString();
          sql = string.Format("insert into GX_AD_DIC(CLASS_NAME,CLASS_EXPLAIN,CLASS_ADDTIME) values('{0}','{1}','{2}')", class_name,class_explain,nowtime);
          flag = OracleHelper.ExecuteNonQuery(sql, null);
          if (flag > 0)
          {
              responseText = "[{\"msg\":\"success\",\"status\":\"添加成功\"}]";
              logFlag = true;
          }            
          else
              responseText = "[{\"msg\":\"fail\",\"status\":\"插入数据库失败\"}]";
          if (logFlag)
          {
              log = username + " 成功添加了名称为" + class_name+"的分类";
          }
          else
              log = username + " 失败添加名称为" + class_name + "的分类";
          lgdal.setOperationLog(username,log, ip);
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
          string log = "";
          bool logFlag = false;
          string classidnumwhere = "";
          string[] classIdArray = class_id.Split(',');
          //foreach(string i in imageIdArray)
          //  idnumwhen+="WHEN ID="+"\'"+i.ToString()+"\'"+" THEN '1'  ";
          for (int iwhere = 0; iwhere < classIdArray.Length; iwhere++)
          {
              if (iwhere > 0)
                  classidnumwhere += ",";
              classidnumwhere += "\'" + classIdArray[iwhere].ToString() + "\'";
          }


          //string sql = "update " + tableview + " set DEL_FLAG ='1' where ORDER_ID='" + order_id + "'";
          string sql = "UPDATE  GX_AD_DIC set CLASS_IS_SHOW='1'   WHERE CLASS_ID IN (" + classidnumwhere + ")";


          int flag = OracleHelper.ExecuteNonQuery(sql, null);
          if (flag > 0)
          {
              responseText = "[{\"msg\":\"success\",\"status\":\"删除成功\"}]";
              logFlag = true;
          }
          
          else
              responseText = "[{\"msg\":\"fail\",\"status\":\"删除失败\"}]";
          if (logFlag)
          {
              log = username + " 成功删除了id为" + class_id+"的分类";
          }
          else
              log = username + " 失败删除id为" + class_id + "的分类";
          lgdal.setOperationLog(username,log, ip);
          return responseText;
      }
      #endregion
  }
}
