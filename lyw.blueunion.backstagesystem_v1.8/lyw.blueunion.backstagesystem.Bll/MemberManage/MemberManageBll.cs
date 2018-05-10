using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using lyw.blueunion.backstagesystem.Dal.MemberManage;
namespace lyw.blueunion.backstagesystem.Bll.MemberManage
{
   public class MemberManageBll
    {
        MemberManageDal memberbll=new MemberManageDal();
       /// <summary>
       /// 会员信息列表
       /// </summary>
       /// <param name="memberName"></param>
       /// <param name="registerTime"></param>
       /// <param name="nowpage"></param>
       /// <param name="pagesize"></param>
       /// <param name="type"></param>
       /// <returns></returns>
        public string memberListData(string memberName, string registerTime, string nowpage, string pagesize,string type)
        {
            string responseText="";
            if (nowpage == "")
                nowpage = "1";
            if(pagesize=="")
                pagesize="10";
            int in_nowpage = Convert.ToInt32(nowpage);
            int in_pagesize = Convert.ToInt32(pagesize);
            if (type == "personal")
                responseText = memberbll.memberListDataPersonal(memberName, registerTime, in_nowpage, in_pagesize);
            else if (type == "enterprise")
                responseText=memberbll.memberListDataEnterprise(memberName,registerTime,in_nowpage,in_pagesize);
            else if (type == "memberunit")
                responseText = memberbll.memberListDataMemberUnit(memberName, registerTime, in_nowpage, in_pagesize);
            else if (type == "professional")
            {
                responseText = memberbll.memberListDataMemberProfessional(memberName, registerTime, in_nowpage, in_pagesize);
            }

            else   
            return "{\"msg\":\"fail\",\"status\":\"当前type类型不存在\"}";

            return responseText;
        }
       /// <summary>
       /// 详细的个人会员信息
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
        public string detailpersonalInfo(string user_id)
        {
            string responseText = "";
            if (user_id == "")
            {
                return "[{\"msg\":\"fail\",\"status\":\"当前会员的编号获取失败\"}]";
            }
            responseText = memberbll.detailpersonalInfo(user_id);
            return responseText;
        }
       /// <summary>
       /// 详细企业会员信息
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
        public string detailEnterpriseInfo(string user_id)
        {
            string responseText = "";
            if (user_id == "")
            {
                return "[{\"msg\":\"fail\",\"status\":\"当前会员的编号获取失败\"}]";
            }
            responseText = memberbll.detailEnterpriseInfo(user_id);
            return responseText;
        }
       /// <summary>
       /// 详细的自由职业信息
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
        public string detailProfessionalInfo(string user_id, string profession)
        {
            string responseText = "";
            if (string.IsNullOrEmpty(user_id) || string.IsNullOrEmpty(profession))
            {
                return "[{\"msg\":\"fail\",\"status\":\"当前会员的信息获取失败\"}]";
            }
            responseText = memberbll.detailProfessionalInfo(user_id,profession);
            return responseText;
        }
       /// <summary>
       /// 详细会员单位信息
       /// </summary>
       /// <param name="user_id"></param>
       /// <returns></returns>
        public string detailMemberUnitInfo(string user_id)
        {
            string responseText = "";
            if (user_id == "")
            {
                return "[{\"msg\":\"fail\",\"status\":\"当前会员的编号获取失败\"}]";
            }
            responseText = memberbll.detailMemberUnitInfo(user_id);
            return responseText;
        }
       
          /// <summary>
          /// 删除会员
          /// </summary>
          /// <param name="user_id"></param>
          /// <returns></returns>
        public string delMember(string user_id,string username,string ip)
        {
            string responseText = "";
            if (user_id == "")
            {
                return "{\"msg\":\"fail\",\"status\":\"当前会员的编号获取失败\"}";
            }
            responseText = memberbll.delmember(user_id,username,ip);
            return responseText;
        }
       /// <summary>
       /// 删除企业会员
       /// </summary>
       /// <param name="user_id"></param>
       /// <param name="username"></param>
       /// <param name="ip"></param>
       /// <returns></returns>
        public string delEnterMember(string user_id, string username, string ip)
        {
            string responseText = "";
            if (user_id == "")
            {
                return "{\"msg\":\"fail\",\"status\":\"当前会员的编号获取失败\"}";
            }
            responseText = memberbll.delEnterMember(user_id, username, ip);
            return responseText;
        }
       /// <summary>
       /// 删除自由职业
       /// </summary>
       /// <param name="user_id"></param>
       /// <param name="username"></param>
       /// <param name="ip"></param>
       /// <returns></returns>
        public string delProfessionMember(string user_id,string profession,string username, string ip)
        {
            string responseText = "";
            if (user_id == "")
            {
                return "{\"msg\":\"fail\",\"status\":\"当前会员的编号获取失败\"}";
            }
            responseText = memberbll.delProfessionMember(user_id,profession,username, ip);
            return responseText;
        }
       /// <summary>
       /// 审核自由职业是否注册成功
       /// </summary>
       /// <param name="check_id"></param>
       /// <returns></returns>
        public string professionMemberCheck(string check_id, string user_id, string profession, string username, string ip) 
        {
            string responseText = "";
            //if (user_id == "")
            //{
            //    return "{\"msg\":\"fail\",\"status\":\"当前会员的编号获取失败\"}";
            //}
            responseText = memberbll.professionMemberCheck(check_id,user_id,profession,username,ip);
            return responseText;
          
        }       
       /// <summary>
        /// 获取各分类数据进行饼状图分析
       /// </summary>
       /// <returns></returns>
        public string memberRankCountToPie()
        {

            string responseText = "";
            responseText = memberbll.memberRankCountToPie();
            return responseText;
        }
       /// <summary>
       /// 获取各分类数据进行折线/柱状图分析
       /// </summary>
       /// <returns></returns>
        public string memberRankCountToLine()
        {
            string responseText = "";
            responseText = memberbll.memberRankCountToLine();
            return responseText;
        }
       /// <summary>
       /// 统计会员分类数据
       /// </summary>
       /// <returns></returns>
        public string stasticCount()
        {
            string responseText = "";
            responseText = memberbll.stasticCount();
            return responseText;
        
        }
    }
}
