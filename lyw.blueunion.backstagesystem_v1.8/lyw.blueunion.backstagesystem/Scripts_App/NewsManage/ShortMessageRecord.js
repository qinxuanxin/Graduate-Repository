var smsrecordurl = rootUrl + "NewsManage/RequestSmsRecord?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var detailurl = rootUrl + "OrderManage/OrderDetailInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "OrderManage/OrderDelOrder?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var orderstatuscounturl = rootUrl + "OrderManage/OrderOrderStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var paystatuscounturl = rootUrl + "OrderManage/OrderPayStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var shippstatuscounturl = rootUrl + "OrderManage/OrderShippStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var savesendgoodsurl = rootUrl + "OrderManage/SendGoods?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址


var count1 = 1;
var count1max;
var pagetype = "orderpcb";
$(document).ready(function () {

    requestOrderListData();
    $(".search-sms-record").on("click", requestOrderListData);//收藏
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
    $(".backprepage").on("click", backpreinfo);
    requestOrderStatusAmount('0');//未确认
    requestOrderStatusAmount('1');//已确认
    requestOrderStatusAmount('2');//取消
    requestOrderStatusAmount('3');//无效

    requestPayStatusAmount('0');//未付款
    requestPayStatusAmount('1');//已付款

    requestShippStatusAmount('0');//未发货
    requestShippStatusAmount('1');//已发货
    requestShippStatusAmount('2');//已收货
    requestShippStatusAmount('3');//已退货
    /**
  **查询当前自助下单的详细信息
  **/
    $('.search-pcborderdetailinfo').on('click', function () {
        var pcborderid = GetCheckedValues("orderid");
        if (pcborderid == "" || pcborderid == null) {
            alert("请选择需要查看的订单!")
            return false;
        }

        requestDetailInfo(pcborderid);
        $(".basicorderinfo").css("display", "none");
        $(".orderofpcbinfo").css("display", "block");

    })
    /******
    ***删除当前自助下单的订单
    ********/
    $('.del-thisorderofpcb').on('click', function () {

        var pcborderid = GetCheckedValues("orderid");
        if (pcborderid == "" || pcborderid == null) {
            alert("请选择需要删除的订单!");
            return false;
        }
        requestDelOrder(pcborderid);


    })
    /***********
    ***发货弹框
    ********/
    $(".send-goods-btn").on('click', function () {
        var pcborderid = GetCheckedValues("orderid");
        if (pcborderid == "" || pcborderid == null) {
            alert("请选择需要发货的订单!");
            return false;
        }
        $("#sendModel").modal("show");
    });
    /********
    ******保存发货信息
    **************/
    $(".save-sendgoods-modal").on('click', function () {
        savesendgoodsfun();


    });
    /*****
    ***退货
    *****/
    $(".cancel-goods").on('click', function () {
        var pcborderid = GetCheckedValues("orderid");
        if (pcborderid == "" || pcborderid == null) {
            alert("请选择需要退货的订单!");
            return false;
        }

        cancelGoodsFun(pcborderid);
    })

});

/****
***请求短信记录的数据列表
************/
function requestOrderListData() {


    var postdata = {};
    postdata["phone_number"] = $("#phonenumber").val();
    postdata["regtime_start"] = $("#regtime_start").val();
    postdata["regtime_end"] = $("#regtime_end").val();
 
    postdata["Pagesize"] = $(".ui-pg-selbox").val();
    postdata["Nowpage"] = count1;
    //postdata["Type"] = pagetype;//

    $.ajax({
        type: "post",
        url: smsrecordurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            $("#table_list").html("");
            $("#pager_list").html("");
            var jsonRecord = data;
            if (jsonRecord[0].msg != "success") {

                $("#table_list").html(

                      "<tr>" +
                            "<th>" + jsonRecord[0].status + "</th>" +
                        "</tr>"
                );
                return false;
            }

            if (jsonRecord[0].count == 0) {

                $("#table_list").html(

                    "<tr>" +
                          "<th>查无数据</th>" +
                      "</tr>"
              );
                return false;
            }

            count1max = jsonRecord[0].count / postdata["Pagesize"];
            count1max = Math.ceil(count1max);


            $("#sp_1_pager_list_2").text("共" + count1max + "页");
            $("#pagenowshow").val(count1);
            $(".totalnum").text("共" + jsonRecord[0].count + "条");
            var jsonRecords = jsonRecord[0].servers;

            $("#table_list").html(

                          "<tr>" +
                       
                             "<th>接收短信号码</th>" +
                                 "<th>短信内容</th>" +
                                "<th>发送时间</th>" +
                                 "<th>发送状态</th>" +
                               
                            "</tr>"
                    );
            var orderstatus_text = "";
            var paystatus_text = "";
            var shipstatus_text = "";
            for (var i = 0; i < jsonRecords.length; i++) {
               
                $("#table_list").append(
                                "<tr>" +
                         
                                    "<td>" + jsonRecords[i]['PHONE_NUM'] + "</td>" +
                                       "<td>" + jsonRecords[i]['SMS_CONTENT'] + "</td>" +
                                     "<td>" + jsonRecords[i]['SEND_TIME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['SEND_STATUS'] + "</td>" +
       
                                "</tr>"
                            );

            }
            $(".i-checks").iCheck({ checkboxClass: "icheckbox_square-green", radioClass: "iradio_square-green", })





        }
    });
}
/*******
****返回首页
********/
function btnfirstpage() {
    count1 = 1;
    //$(".pagination").find("li").removeClass("active");
    ////  obj.closest("ul").find("li:active").removeAttr("class");

    //obj.parent().addClass("active");
    requestOrderListData();
    $("#pagenowshow").val(count1);
};
/*******
****返回上一页
********/
function btnprepage() {
    if (--count1 == 0) {
        count1 = 1;
    }

    //$(".pagination").find("li").removeClass("active");

    //obj.parent().addClass("active");
    requestOrderListData();
    $("#pagenowshow").val(count1);
};
/*******
****进入下一页
********/
function btnnextpage() {
    if (count1++ == count1max) {
        count1 = count1max;
    }
    //$(".pagination").find("li").removeClass("active");
    ////   obj.closest("ul").find("li:active").removeAttr("class");

    //obj.parent().addClass("active");
    requestOrderListData();
    $("#pagenowshow").val(count1);
};
/*******
****跳至尾页
********/
function btnlastpate() {
    count1 = count1max;
    //$(".pagination").find("li").removeClass("active");
    ////   obj.closest("ul").find("li:active").removeAttr("class");

    //obj.parent().addClass("active");
    requestOrderListData();
    $("#pagenowshow").val(count1);
};
/********* 获取checkbox的id*/
function GetCheckedValues(id) {
    //if (id == undefined)
    //    id = 'ChkSingleID';
    var ids = "";
    //var chks = $("input[type='checkbox'][name='" + id + "']:checked");

    var chks = $("div.checked input[type='checkbox'][name='" + id + "']");
    for (var index = 0; index < chks.length; index++) {
        if (index > 0)
            ids += ",";
        ids += $(chks[index]).val();
    }
    return ids;
}

/****
***查看当前PCB订单的详细信息
*********/
function requestDetailInfo(pcborderid) {

    $.ajax({
        type: "post",
        url: detailurl,
        data: { 'order_id': pcborderid, 'type': pagetype },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success") {
                alert(data[0].status);
                return false;
            }
            var jsonRecords = data[0].servers;
            var orderstatus_text = "";
            var paystatus_text = "";
            var shipstatus_text = "";
            {
                if (jsonRecords[0]['ORDER_FLAG'] == '0')
                    orderstatus_text = "未确认";
                else if (jsonRecords[0]['ORDER_FLAG'] == '1')
                    orderstatus_text = "确认";
                else if (jsonRecords[0]['ORDER_FLAG'] == '2')
                    orderstatus_text = "取消";
                else if (jsonRecords[0]['ORDER_FLAG'] == '3')
                    orderstatus_text = "无效";
                else
                    orderstatus_text = "未知";
            }
            {
                if (jsonRecords[0]['PAY_FLAG'] == '0')
                    paystatus_text = "未付款";
                else if (jsonRecords[0]['PAY_FLAG'] == '1')
                    paystatus_text = "已付款";
                else
                    paystatus_text = "未知";
            }
            {
                if (jsonRecords[0]['SEND_FLAG'] == '0')
                    shipstatus_text = "未发货";
                else if (jsonRecords[0]['SEND_FLAG'] == '1')
                    shipstatus_text = "已发货";
                else if (jsonRecords[0]['SEND_FLAG'] == '2')
                    shipstatus_text = "已收货";
                else if (jsonRecords[0]['SEND_FLAG'] == '3')
                    shipstatus_text = "已退货";
                else
                    shipstatus_text = "未知";
            }
            $("#detailorder_num").val(jsonRecords[0]['ORDER_SN']);
            $("#detailorder_placeorderuser").val(jsonRecords[0]['USER_TEL']);
            $("#detailorder_placeordertime").val(jsonRecords[0]['ADD_TIME']);
            $("#detailorder_actualmoney").val(jsonRecords[0]['MONEY_PAID']);
            $("#detailorder_freight").val(jsonRecords[0]['SEND_FEE']);
            $("#detailorder_orderstatus").val(orderstatus_text);
            $("#detailorder_paystatus").val(paystatus_text);
            $("#detailorder_shippingstatus").val(shipstatus_text);
            $("#detailorder_payway").val(jsonRecords[0]['PAY_NAME']);
            $("#detailorder_shippingway").val(jsonRecords[0]['SEND_NAME']);
            $("#detailorder_consignee").val(jsonRecords[0]['CONSIGNEE']);

            $("#detailorder_consigneeaddress").val(jsonRecords[0]['ADDRESS']);
            $("#detailorder_consigneezipcode").val(jsonRecords[0]['ZIPCODE']);
            $("#detailorder_consigneemobile").val(jsonRecords[0]['MOBILE']);


        }
    });
}
/*******
*****返回上一页
******************/
function backpreinfo() {
    $(".basicorderinfo").css("display", "block");
    $(".orderofpcbinfo").css("display", "none");

}
/*****
******删除当前订单
*************/
function requestDelOrder(pcborderid) {

    $.ajax({
        type: "post",
        url: delurl,
        data: { 'order_id': pcborderid, 'type': pagetype },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            alert(data[0].status);

            window.location.reload();
        }
    });
}

/******订单、支付、发货状态订单数量显示*******/
function requestOrderStatusAmount(orderstatus) {
    //订单状态数量
    $.ajax({
        type: "post",
        url: orderstatuscounturl,
        data: { 'order_status': orderstatus, 'type': pagetype },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success")
                return false;
            else {
                var counts = data[0].counts;
                if (orderstatus == '0')
                    $(".order-unconfirmed").text(counts);
                else if (orderstatus == '1')
                    $(".order-confirmed").text(counts);
                else if (orderstatus == '2')
                    $(".order-cancel").text(counts);
                else if (orderstatus == '3')
                    $(".order-invalid").text(counts);
                else
                    return false;
                //document.getElementById("").innerHTML=


            }
        }
    });
}
function requestPayStatusAmount(paystatus) {

    //支付状态数量
    $.ajax({
        type: "post",
        url: paystatuscounturl,
        data: { 'pay_status': paystatus, 'type': pagetype },
        dataType: 'json',
        async: true,//异步
        success: function (data) {

            if (data[0].msg != "success")
                return false;
            else {
                var counts = data[0].counts;
                if (paystatus == '0')
                    $(".pay-no").text(counts);
                else if (paystatus == '1')
                    $(".pay-had").text(counts);



                else
                    return false;
                //document.getElementById("").innerHTML=


            }

        }
    });
}
function requestShippStatusAmount(shippstatus) {
    //发货状态数量
    $.ajax({
        type: "post",
        url: shippstatuscounturl,
        data: { 'shipp_status': shippstatus, 'type': pagetype },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success")
                return false;
            else {
                var counts = data[0].counts;
                if (shippstatus == '0')
                    $(".shipp-no").text(counts);
                else if (shippstatus == '1')
                    $(".shipp-in").text(counts);
                else if (shippstatus == '2')
                    $(".shipp-had").text(counts);
                else if (shippstatus == '3')
                    $(".shipp-return").text(counts);
                else
                    return false;
                //document.getElementById("").innerHTML=


            }
        }
    });
}
/******订单、支付、发货状态订单数量显示*******/

/********
*****选择所有的checkbox
**********/
function selectall() {
    var icheckboxdiv = $("div.icheckbox_square-green");
    if (icheckboxdiv.hasClass("checked")) {
        $("div.icheckbox_square-green").removeClass("checked");
        $("input[name='orderid']").attr("checked", "false");
    }
    else {
        $("div.icheckbox_square-green").addClass("checked");
        $("input[name='orderid']").attr("checked", "true");
    }

    //var all = $("input[name='id']");


    //$("input[name='id']").attr('checked', !obj.attr('checked')); obj.attr('checked', !obj.attr('checked'));
    //alert(obj.val());
}
/*********
**发货
****/
function savesendgoodsfun() {
    var postdata = {};
    postdata["send_type_id"] = $("#shipping_status").val();
    postdata["track_num"] = $("#tracknum").val();
    postdata["order_id"] = GetCheckedValues("orderid");
    postdata["type"] = pagetype;
    postdata["returnorsendtype"] = "1";
    $.ajax({
        type: "post",
        url: savesendgoodsurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success")
                return false;
            else
                alert("发货成功!");
            window.location.reload();
        }
    });
}
/****
****退货
******/
function cancelGoodsFun(orderid) {
    var postdata = {};
    postdata["send_type_id"] = "";
    postdata["track_num"] = "";
    postdata["order_id"] = orderid;
    postdata["type"] = pagetype;
    postdata["returnorsendtype"] = "2";
    $.ajax({
        type: "post",
        url: savesendgoodsurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success")
                return false;
            else
                alert("退货成功!");
            window.location.reload();
        }
    });
}
