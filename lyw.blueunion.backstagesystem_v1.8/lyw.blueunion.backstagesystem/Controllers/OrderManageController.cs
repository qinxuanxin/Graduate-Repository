using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lyw.blueunion.backstagesystem.Model.Order;
using lyw.blueunion.backstagesystem.Bll.OrderManage;
using lyw.blueunion.backstagesystem.Filters;
namespace lyw.blueunion.backstagesystem.Controllers
{
    [CheckLoginFilter]
    public class OrderManageController : Controller
    {
        OrderManageBll orderbll = new OrderManageBll();
        string username = "";
        string ip = "";
        //
        // GET: /OrderManage/
        /// <summary>
        /// pcb订单视图
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderOfPcb()
        {
            return View();
        }
        /// <summary>
        /// 共享生产订单视图
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderOfShareProduction()
        {
            return View();
        }
        /// <summary>
        /// 接发包订单视图
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderOfSendReceivePackets()
        {
            return View();
        }
        /// <summary>
        /// 订单列表信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
         [CustomerResource("OrderListData","12")]
        public ActionResult OrderListData(OrderOfPcb order)
        {
            string responseText = "";
            responseText = orderbll.orderListData(order);
            return Content(responseText);
        }
        /// <summary>
        /// 共享生产订单列表信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
         [CustomerResource("OrderShareListData","13")]
        public ActionResult OrderShareListData(OrderOfPcb order)
        {
            string responseText = "";
            responseText = orderbll.orderListData(order);
            return Content(responseText);
        }
        /// <summary>
        /// 设计发包订单列表信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
           [CustomerResource("OrderSendTaskListData","14")]
        public ActionResult OrderSendTaskListData(OrderOfPcb order)
        {
            string responseText = "";
            responseText = orderbll.orderListData(order);
            return Content(responseText);
        }
       /// <summary>
       /// 当前订单详细信息
       /// </summary>
       /// <param name="order_id"></param>
       /// <param name="type"></param>
       /// <returns></returns>
        public ActionResult OrderDetailInfo(string order_id,string type)
        {
            string responseText = "";
            responseText = orderbll.orderDetailInfo(order_id,type);
            return Content(responseText);
        }
      
        /// <summary>
        /// 审核此任务
        /// </summary>
        /// <param name="task_id"></param>
        /// <returns></returns>
         [CustomerResource("OrderToBidSuccess","28")]
        public ActionResult OrderToBidSuccess(string task_id, string user_id, string flag)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = orderbll.orderToBidSuccess(task_id, user_id,flag,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 删除自助下单订单
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        [CustomerResource("OrderDelOrder", "23")]
        public ActionResult OrderDelOrder(string order_id, string type)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = orderbll.orderDelOrder(order_id,type,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 删除共享生产订单
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
         [CustomerResource("ShareDelOrder", "27")]
        public ActionResult ShareDelOrder(string order_id, string type)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = orderbll.orderDelOrder(order_id, type, username, ip);
            return Content(responseText);
        }
        /// <summary>
        /// 删除接发包订单
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
         [CustomerResource("SendReceivePacketDelOrder", "31")]
        public ActionResult SendReceivePacketDelOrder(string order_id, string type)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = orderbll.orderDelOrder(order_id, type, username, ip);
            return Content(responseText);
        }
        /// <summary>
        /// 查询订单状态数量
        /// </summary>
        /// <param name="order_status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult OrderOrderStatusCount(string order_status,string type)
        {
            string responseText = "";
            responseText = orderbll.orderOrderStatusCount(order_status, type);
            return Content(responseText);
        }
        /// <summary>
        /// 查询支付状态数量
        /// </summary>
        /// <param name="pay_status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult OrderPayStatusCount(string pay_status, string type)
        {
            string responseText = "";
            responseText = orderbll.orderPayStatusCount(pay_status,type);
            return Content(responseText);
        }
        /// <summary>
        /// 查询配送状态数量
        /// </summary>
        /// <param name="shipp_status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult OrderShippStatusCount(string shipp_status, string type)
        {

            string responseText = "";
            responseText = orderbll.orderShippStatusCount(shipp_status, type);
            return Content(responseText);
        }
       /// <summary>
       /// 自助下单发货
       /// </summary>
       /// <param name="send_type_id"></param>
       /// <param name="track_num"></param>
       /// <param name="order_id"></param>
       /// <param name="type"></param>
       /// <returns></returns>
        [CustomerResource("SelfHelpSendGoods", "21")]
        public ActionResult SelfHelpSendGoods(string send_type_id, string track_num, string order_id, string type, string returnorsendtype)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = orderbll.sendGoods(send_type_id, track_num, order_id, type,returnorsendtype,username,ip);
            return Content(responseText);
        }
        /// <summary>
        /// 共享生产发货
        /// </summary>
        /// <param name="send_type_id"></param>
        /// <param name="track_num"></param>
        /// <param name="order_id"></param>
        /// <param name="type"></param>
        /// <param name="returnorsendtype"></param>
        /// <returns></returns>
         [CustomerResource("ShareProductSendGoods", "25")]
        public ActionResult ShareProductSendGoods(string send_type_id, string track_num, string order_id, string type, string returnorsendtype)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = orderbll.sendGoods(send_type_id, track_num, order_id, type, returnorsendtype, username, ip);
            return Content(responseText);
        }
        /// <summary>
        /// 自助下单退货
        /// </summary>
        /// <param name="send_type_id"></param>
        /// <param name="track_num"></param>
        /// <param name="order_id"></param>
        /// <param name="type"></param>
        /// <param name="returnorsendtype"></param>
        /// <returns></returns>
        [CustomerResource("SelfHelpReturnGoods", "22")]
        public ActionResult SelfHelpReturnGoods(string send_type_id, string track_num, string order_id, string type, string returnorsendtype)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = orderbll.sendGoods(send_type_id, track_num, order_id, type, returnorsendtype, username, ip);
            return Content(responseText);
        }
        /// <summary>
        /// 共享生产退货
        /// </summary>
        /// <param name="send_type_id"></param>
        /// <param name="track_num"></param>
        /// <param name="order_id"></param>
        /// <param name="type"></param>
        /// <param name="returnorsendtype"></param>
        /// <returns></returns>
           [CustomerResource("ShareProductReturnGoods", "26")]
        public ActionResult ShareProductReturnGoods(string send_type_id, string track_num, string order_id, string type, string returnorsendtype)
        {
            string responseText = "";
            if (Request.Cookies["ip"] != null)
                ip = Request.Cookies["ip"].Value;
            if (Request.Cookies["userName"] != null)
                username = System.Web.HttpContext.Current.Server.UrlDecode(Request.Cookies["userName"].Value);
            responseText = orderbll.sendGoods(send_type_id, track_num, order_id, type, returnorsendtype, username, ip);
            return Content(responseText);
        }
        /// <summary>
        /// 租用仪器共享
        /// </summary>
        /// <returns></returns>
           public ActionResult RentInstrumentShare()
           {
               return View();           
           }
        /// <summary>
        /// 租用仪器共享订单列表信息
        /// </summary>
        /// <param name="instshare"></param>
        /// <returns></returns>
           public ActionResult RentInstrumentShareListData(InstrumentShare instshare) 
           {
               string responseText = "";
               responseText = orderbll.rentInstrumentShareListData(instshare);
               return Content(responseText);
           }
        /// <summary>
        /// 加入仪器共享
        /// </summary>
        /// <returns></returns>
           public ActionResult AddInstrumentShare()
           {
               return View();
           }
    }
}
