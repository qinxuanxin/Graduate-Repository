using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lyw.blueunion.backstagesystem.Model.Order;
using lyw.blueunion.backstagesystem.Dal.OrderManage;
namespace lyw.blueunion.backstagesystem.Bll.OrderManage
{
   public class OrderManageBll
    {
       OrderManageDal orderdal = new OrderManageDal();
       /// <summary>
       /// 订单列表信息
       /// </summary>
       /// <param name="order"></param>
       /// <returns></returns>
       public string orderListData(OrderOfPcb order)
       {
           string responseText = "";
           string tableViewName = "";
           if (order.Type == "orderpcb")
               tableViewName = "GXPCB_ORDER__VIEW";
           else if (order.Type == "ordershareproduction")
               tableViewName = "GXSHAREPRODUCTION_ORDER__VIEW";
           else if (order.Type == "sendreceivetask")
           {
               tableViewName = "GXFW_SEND_TASK";
           }
           else
           {
               responseText="{\"msg\":\"fail\",\"status\":\"当前type类型不存在\"}";
               return responseText;
           }
              
           if (order.Type == "sendreceivetask")
           {
               responseText = orderdal.orderListTaskData(order, tableViewName);
           }
           else
           {
               responseText = orderdal.orderListData(order, tableViewName);
           }
          
           return responseText;
       }
       /// <summary>
       /// 当前订单详细信息
       /// </summary>
       /// <param name="order_id"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       public string orderDetailInfo(string order_id, string type)
       { 
            string responseText = "";
           string tableview="";
           if (type == "orderpcb")
               tableview = "GXPCB_ORDER__VIEW";
           else if (type == "ordershareproduction")
               tableview = "GXSHAREPRODUCTION_ORDER__VIEW";
           else if (type == "sendreceivetask")
               tableview = "";
           else
           {
               responseText = "[{\"msg\":\"fail\",\"status\":\"当前type类型不存在\"}]";
               return responseText;
           }
           if (type == "sendreceivetask")
           {
               string sendresponse = orderdal.taskDetailSendUserInfo(order_id);
               string receiveresponse = orderdal.taskDetailReceiveUserInfo(order_id);
               responseText = "[" + sendresponse + "," + receiveresponse + "]";
           }
           else
           {
               responseText = orderdal.orderDetailInfo(order_id, tableview);
           }
          
           return responseText;
       }
       /// <summary>
       /// 当前用户竞标成功
       /// </summary>
       /// <param name="task_id"></param>
       /// <param name="user_id"></param>
       /// <returns></returns>
       public string orderToBidSuccess(string task_id, string user_id,string flag,string username,string ip)
       {
           string responseText = "";
           responseText = orderdal.orderToBidSuccess(task_id, user_id, flag,username,ip);
           return responseText;
       }
       /// <summary>
       /// 删除当前订单
       /// </summary>
       /// <param name="order_id"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       public string orderDelOrder(string order_id, string type,string username,string ip)
       {
           string responseText = "";
           string tableview = "";
           if (type == "orderpcb")
               tableview = "GXPCB_ORDER_INFO";
           else if (type == "ordershareproduction")
               tableview = "GXPRODUCTION_ORDER_INFO";
           else if (type == "sendreceivetask")
               tableview = "GXFW_SEND_TASK";
           else
           {
               responseText = "[{\"msg\":\"fail\",\"status\":\"不存在当前type类型值\"}]"; 
               return responseText;
           }
              
           if (type == "sendreceivetask")
               responseText = orderdal.orderDelOrder(order_id,username,ip);
           else
           responseText = orderdal.orderDelOrder(order_id, tableview,username,ip);
           return responseText;
       }
       /// <summary>
       /// 查询订单状态数量
       /// </summary>
       /// <param name="order_status"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       public string orderOrderStatusCount(string order_status, string type)
       {
           string responseText = "";
           string tableview = "";
           if (type == "orderpcb")
               tableview = "GXPCB_ORDER__VIEW";
           else if (type == "ordershareproduction")
               tableview = "GXSHAREPRODUCTION_ORDER__VIEW";
           else if (type == "sendreceivetask")
               tableview = "GXFW_SEND_TASK";
           else
               return responseText;
           if (type == "sendreceivetask")             
           responseText = orderdal.orderOrderStatusCount(order_status);
           else
           responseText = orderdal.orderOrderStatusCount(order_status, tableview);
           return responseText;
       }
       /// <summary>
       /// 查询支付状态数量
       /// </summary>
       /// <param name="order_status"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       public string orderPayStatusCount(string pay_status, string type)
       {
           string responseText = "";
           string tableview = "";
           if (type == "orderpcb")
               tableview = "GXPCB_ORDER__VIEW";
           else if (type == "ordershareproduction")
               tableview = "GXSHAREPRODUCTION_ORDER__VIEW";
           else
               return responseText;
           responseText = orderdal.orderPayStatusCount(pay_status, tableview);
           return responseText;
       }
       /// <summary>
       /// 查询配送状态数量
       /// </summary>
       /// <param name="shipp_status"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       public string orderShippStatusCount(string shipp_status, string type)
       {
           string responseText = "";
           string tableview = "";
           if (type == "orderpcb")
               tableview = "GXPCB_ORDER__VIEW";
           else if (type == "ordershareproduction")
               tableview = "GXSHAREPRODUCTION_ORDER__VIEW";
           else
               return responseText;
           responseText = orderdal.orderShippStatusCount(shipp_status, tableview);
           return responseText;
       }
       /// <summary>
       /// 发货
       /// </summary>
       /// <param name="send_type_id"></param>
       /// <param name="track_num"></param>
       /// <param name="order_id"></param>
       /// <param name="type"></param>
       /// <returns></returns>
       public string sendGoods(string send_type_id, string track_num, string order_id, string type, string returnorsendtype,string username,string ip)
       {
           string responseText = "";
           string tableview = "";
           string operation = "";
              if (type == "orderpcb")
               tableview = "GXPCB_ORDER_INFO";
           else if (type == "ordershareproduction")
               tableview = "GXPRODUCTION_ORDER_INFO";
           
           else
               return responseText;
            
              responseText = orderdal.sendGoods(send_type_id, track_num, order_id, tableview, returnorsendtype,username,ip);
           return responseText;
       }

       /// <summary>
       /// 租用仪器共享订单列表信息
       /// </summary>
       /// <param name="instshare"></param>
       /// <returns></returns>
       public string rentInstrumentShareListData(InstrumentShare instshare)
       {
           string responseText = "";
           responseText = orderdal.rentInstrumentShareListData(instshare);
           return responseText;
       }

       /// <summary>
       /// 租用仪器订单详细信息
       /// </summary>
       /// <param name="order_id"></param>
       /// <returns></returns>
       public string rentInstrumentDetailInfo(string order_id)
       {
           string responseText = "";
           responseText = orderdal.rentInstrumentDetailInfo(order_id);
           return responseText;
       }
       /// <summary>
       /// 删除共享订单
       /// </summary>
       /// <param name="order_id"></param>
       /// <returns></returns>
       public string rentInstrumentDelOrder(string order_id, string username, string ip)
       {
           string responseText = "";
           responseText = orderdal.rentInstrumentDelOrder(order_id,username,ip);
           return responseText;
       }
    /// <summary>
       /// 加入仪器共享数据列表
    /// </summary>
    /// <param name="instrument_num"></param>
    /// <param name="instrument_phone"></param>
    /// <returns></returns>
       public string addInstrumentShareListData(AddInstrument addinst)
       {
           string responseText = "";
           responseText = orderdal.addInstrumentShareListData(addinst);
           return responseText;
       }
       /// <summary>
       /// 加入仪器设备详细信息
       /// </summary>
       /// <param name="instrument_id"></param>
       /// <returns></returns>
       public string addInstrumentDetailInfo(string instrument_id)
       {
           string responseText = "";
           responseText = orderdal.addInstrumentDetailInfo(instrument_id);
           return responseText;
       }
       /// <summary>
       /// 审核加入仪器设备
       /// </summary>
       /// <param name="instrumentorderid"></param>
       /// <param name="status"></param>
       /// <param name="username"></param>
       /// <param name="ip"></param>
       /// <returns></returns>
       public string addInstrumentCheck(string instrumentorderid,string  status,string  username,string ip)
       {
           string responseText = "";
           responseText = orderdal.addInstrumentCheck(instrumentorderid,status,username,ip);
           return responseText;
       }
       /// <summary>
       /// 删除加入的仪器设备
       /// </summary>
       /// <param name="instrumentorderid"></param>
       /// <param name="username"></param>
       /// <param name="ip"></param>
       /// <returns></returns>
       public string addInstrumentDel(string instrumentorderid,string username,string ip)
       {
           string responseText = "";
           responseText = orderdal.addInstrumentDel(instrumentorderid, username, ip);
           return responseText;
       }
       /// <summary>
       /// 获取下单会员信息
       /// </summary>
       /// <param name="member_phone"></param>
       /// <returns></returns>
       public string getMemberInfo(string member_phone)
       {
           string responseText = "";
           responseText = orderdal.getMemberInfo(member_phone);
           return responseText;
       }
    }
}
