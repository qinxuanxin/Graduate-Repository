var rentinstshareurl = rootUrl + "OrderManage/RentInstrumentShareListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var detailurl = rootUrl + "OrderManage/RentInstrumentDetailInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "OrderManage/RentInstrumentDelOrder?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var orderstatuscounturl = rootUrl + "OrderManage/OrderOrderStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var paystatuscounturl = rootUrl + "OrderManage/OrderPayStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var shippstatuscounturl = rootUrl + "OrderManage/OrderShippStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var savesendgoodsurl = rootUrl + "OrderManage/SelfHelpSendGoods?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var returngoodsrul = rootUrl + "OrderManage/SelfHelpReturnGoods?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var instrumentdetailurl = rootUrl + "OrderManage/AddInstrumentDetailInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var getuserinfourl = rootUrl + "OrderManage/GetMemberInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var count1 = 1;
var count1max;
var pagetype = "instrument";
$(document).ready(function () {

    requestOrderListData();
    $(".search-instrument-data").on("click", requestOrderListData);//收藏
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
    $(".backprepage").on("click", backpreinfo);
    $(".getinstrumentinfo").on("click", getinfo);
    $(".getuserinfo").on("click", getuserinfo);
    //requestOrderStatusAmount('0');//未确认
    //requestOrderStatusAmount('1');//已确认
    //requestOrderStatusAmount('2');//取消
    //requestOrderStatusAmount('3');//无效

    //requestPayStatusAmount('0');//未付款
    //requestPayStatusAmount('1');//已付款

    //requestShippStatusAmount('0');//未发货
    //requestShippStatusAmount('1');//已发货
    //requestShippStatusAmount('2');//已收货
    //requestShippStatusAmount('3');//已退货
    /**
  **查询当前仪器共享下单的详细信息
  **/
    $('.search-instrumentorderdetailinfo').on('click', function () {
        var instrumentorderid = GetCheckedValues("instrumentorderid");
        if (instrumentorderid == "" || instrumentorderid == null) {
            alert("请选择需要查看的订单!")
            return false;
        }

        requestDetailInfo(instrumentorderid);
        $(".basicorderinfo").css("display", "none");
        $(".instrumentofpcbinfo").css("display", "block");

    });
    /******
    ***删除当前自助下单的订单
    ********/
    $('.del-thisorderofpcb').on('click', function () {

        var instrumentorderid = GetCheckedValues("instrumentorderid");
        if (instrumentorderid == "" || instrumentorderid == null) {
            alert("请选择需要删除的订单!");
            return false;
        }
        requestDelOrder(instrumentorderid);


    });
    /***********
    ***发货弹框
    ********/
    $(".send-goods-btn").on('click', function () {
        var instrumentorderid = GetCheckedValues("instrumentorderid");
        if (instrumentorderid == "" || instrumentorderid == null) {
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
        var instrumentorderid = GetCheckedValues("instrumentorderid");
        if (instrumentorderid == "" || instrumentorderid == null) {
            alert("请选择需要退货的订单!");
            return false;
        }

        cancelGoodsFun(instrumentorderid);
    })

});

/****
***请求仪器共享订单的数据列表
************/
function requestOrderListData() {


    var postdata = {};
    postdata["Order_num"] = $("#order_num").val();
    postdata["User_phone"] = $("#order_phone").val();
    postdata["Order_regtime_start"] = $("#order_regtime_start").val();
    postdata["Order_regtime_end"] = $("#order_regtime_end").val();
  //  postdata["Order_status"] = $("#order_status").val();
 //   postdata["Pay_status"] = $("#pay_status").val();
  //  postdata["Shipping_status"] = $("#shipping_status").val();

    postdata["Pagesize"] = $(".ui-pg-selbox").val();
    postdata["Nowpage"] = count1;
   // postdata["Type"] = pagetype;//

    $.ajax({
        type: "post",
        url: rentinstshareurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            $("#table_list").html("");
            $("#pager_list").html("");
            var jsonRecord = data;
            if (jsonRecord.msg != "success") {
                $('.toastdiv').toast({
                    content: data.status,
                    duration: 1000
                });
                $("#table_list").html(

                      "<tr>" +
                            "<th>" + jsonRecord.status + "</th>" +
                        "</tr>"
                );
                return false;
            }

            if (jsonRecord.count == 0) {

                $("#table_list").html(

                    "<tr>" +
                          "<th>查无数据</th>" +
                      "</tr>"
              );
                $('.toastdiv').toast({
                    content: '查无数据',
                    duration: 1000
                });
                return false;
            }

            count1max = jsonRecord.count / postdata["Pagesize"];
            count1max = Math.ceil(count1max);


            $("#sp_1_pager_list_2").text("共" + count1max + "页");
            $("#pagenowshow").val(count1);
            $(".totalnum").text("共" + jsonRecord.count + "条");
            var jsonRecords = jsonRecord.servers;

            $("#table_list").html(

                          "<tr>" +
                          //$(this).attr(\"checked\",!$(this).attr(\"checked\"));alert(\"睡觉了\")
                       //   !$(this).attr(\"checked\")
                          '<th><a name="all" onclick="selectall($(this))">全选</a></th>' +
                            //"<th><a onclick='alert(\"是垃圾地方\");'>全选</a></th>"+
                             "<th>订单号</th>" +
                                 "<th>用户</th>" +
                                "<th>下单时间</th>" +
                                 "<th>订单总金额</th>" +
                                 "<th>实际付款金额</th>" +
                                "<th>订单状态</th>" +
                                "<th>支付状态</th>" +

                                "<th>发货状态</th>" +
                                 //"<th></th>" +
                                 // "<th></th>" +
                            "</tr>"
                    );
            var orderstatus_text = "";
            var paystatus_text = "";
            var shipstatus_text = "";
            for (var i = 0; i < jsonRecords.length; i++) {
                {
                    if (jsonRecords[i]['ORDER_FLAG'] == '0')
                        orderstatus_text = "未确认";
                    else if (jsonRecords[i]['ORDER_FLAG'] == '1')
                        orderstatus_text = "确认";
                    else if (jsonRecords[i]['ORDER_FLAG'] == '2')
                        orderstatus_text = "取消";
                    else if (jsonRecords[i]['ORDER_FLAG'] == '3')
                        orderstatus_text = "无效";
                    else
                        orderstatus_text = "未知";
                }
                {
                    if (jsonRecords[i]['PAY_FLAG'] == '0')
                        paystatus_text = "未付款";
                    else if (jsonRecords[i]['PAY_FLAG'] == '1')
                        paystatus_text = "已付款";
                    else
                        paystatus_text = "未知";
                }
                {
                    if (jsonRecords[i]['SEND_FLAG'] == '0')
                        shipstatus_text = "未发货";
                    else if (jsonRecords[i]['SEND_FLAG'] == '1')
                        shipstatus_text = "已发货";
                    else if (jsonRecords[i]['SEND_FLAG'] == '2')
                        shipstatus_text = "已收货";
                    else if (jsonRecords[i]['SEND_FLAG'] == '3')
                        shipstatus_text = "已退货";
                    else
                        shipstatus_text = "未知";
                }
                $("#table_list").append(
                                "<tr>" +
                                   "<td>" +

                                 "<input type='checkbox' class='i-checks' name='instrumentorderid' value='" + jsonRecords[i]['ORDER_ID'] + "'>" +

                                "</td>" +
                                    "<td>" + jsonRecords[i]['ORDER_SN'] + "</td>" +
                                       "<td>" + jsonRecords[i]['USER_TEL'] + "</td>" +
                                     "<td>" + jsonRecords[i]['ADD_TIME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['GOODS_AMOUNT'] + "</td>" +

                                    "<td>" + jsonRecords[i]['MONEY_PAID'] + "</td>" +
                                    "<td>" + orderstatus_text + "</td>" +
                                    "<td>" + paystatus_text + "</td>" +
                                    "<td>" + shipstatus_text + "</td>" +
                                    //"<td><button type='button'  data-instrumentorderid='" + jsonRecords[i]['ORDER_ID'] + "' class='btn btn-primary search-instrumentorderdetailinfo'>查看</button></td>" +
                                   // "<td><button type='button' data-instrumentorderid='" + jsonRecords[i]['ORDER_ID'] + "' class='btn btn-danger del-thisorderofpcb'>删除</button></td>" +
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
***查看当前仪器共享订单的详细信息
*********/
function requestDetailInfo(instrumentorderid) {

    $.ajax({
        type: "post",
        url: detailurl,
        data: { 'order_id': instrumentorderid},
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data.msg != "success") {
                alert(data.status);
                return false;
            }
            var jsonRecords = data.servers;
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
            $("#detailorder_instrumentid").val(jsonRecords[0]['INSTRUMENT_ID']);
            
            $("#detailorder_placeorderuser").val(jsonRecords[0]['USER_TEL']);

            $("#detailorder_instrumentway").val(jsonRecords[0]['INSTRUMENT_WAY']);
            $("#detailorder_deposite").val(jsonRecords[0]['DEPOSIT']);
            $("#detailorder_rentfee").val(jsonRecords[0]['FEE']);

            $("#detailorder_usestarttime").val(jsonRecords[0]['BEGIN_TIME']);
            $("#detailorder_useendtime").val(jsonRecords[0]['END_TIME']);

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
    $(".instrumentofpcbinfo").css("display", "none");
}
/*****
******删除当前订单
*************/
function requestDelOrder(instrumentorderid) {

    $.ajax({
        type: "post",
        url: delurl,
        data: { 'order_id': instrumentorderid },
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
        url: returngoodsrul,
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
/*******
****获取设备详细信息
********/
function getinfo()
{
    var instrumentid = $("#detailorder_instrumentid").val();
    $.ajax({
        type: "post",
        url: instrumentdetailurl,
        data: { 'instrument_id': instrumentid },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data.msg != "success") {
                alert(data.status);
                return false;
            }
            var jsonRecords = data.servers;
            var check_text = "";
          
            var borrow_text = "";
            
            {
                if (jsonRecords[0]['REV_FLAG'] == '0')
                    check_text = "审核中";
                else if (jsonRecords[0]['REV_FLAG'] == '1')
                    check_text = "审核失败";
                else if (jsonRecords[0]['REV_FLAG'] == '2')
                    check_text = "审核成功";

                else
                    check_text = "未知";
            }
            {
                if (jsonRecords[0]['BOR_FALG'] == '0')
                    borrow_text = "未租借";
                else if (jsonRecords[0]['BOR_FALG'] == '1')
                    borrow_text = "已租借";
              

                else
                    borrow_text = "未知";
            }
            $("#detailorder_instrumentid1").val(jsonRecords[0]['INSTRUMENT_ID']);
            $("#detailorder_instrumentname").val(jsonRecords[0]['NAME']);
            $("#detailorder_placeorderuser").val(jsonRecords[0]['USER_TEL']);
            
            var image = domain_name + instrument_image + jsonRecords[0]['INSTRUMENT_PIC'];
            $("#detailinfo_instrumentimage").attr("src",image);
            $("#detailorder_instrumenttype").val(jsonRecords[0]['TYPE']);

            $("#detailorder_instrumentbrands").val(jsonRecords[0]['BRANDS']);
            $("#detailorder_instrumentmodel").val(jsonRecords[0]['MODEL']);
            $("#detailorder_capability").text(jsonRecords[0]['CAPABILITY']);

            $("#detailorder_isdate").val(jsonRecords[0]['IS_DATE']);
            $("#detailorder_attribution").val(jsonRecords[0]['ATTRIBUTION']);

            $("#detailorder_company").val(jsonRecords[0]['COMPANY']);
            var location="";
          
            location = jsonRecords[0]['CITY']+ jsonRecords[0]['DISTRICT']+ jsonRecords[0]['STREET'];

            $("#detailorder_location").val(jsonRecords[0]['location']);

            $("#detailorder_usetime").val(jsonRecords[0]['USER_TIME']);
            $("#detailorder_instrumentway").val(jsonRecords[0]['USER_WAY']);
            $("#detailorder_deposite").val(jsonRecords[0]['DEPOSIT']);
            $("#detailorder_rentfee").val(jsonRecords[0]['FEE']);
            $("#detailorder_addtime").val(jsonRecords[0]['ADD_TIME']);
            $("#detailorder_localornot").val(jsonRecords[0]['INSTRUMENT_SUOZAIDI']);
            $("#detailorder_localfee").val(jsonRecords[0]['INSTRUMENT_SUOZAIDI_FEE']);

            $("#detailorder_borrowornot").val(jsonRecords[0]['INSTRUMENT_WAIJIE']);
            $("#detailorder_borrowdeposit").val(jsonRecords[0]['INSTRUMENT_WAIJIE_DEPOSIT']);
            $("#detailorder_rentfee").val(jsonRecords[0]['INSTRUMENT_WAIJIE_FEE']);


            $("#detailorder_turnornot").val(jsonRecords[0]['INSTRUMENT_ZHUANRANG']);
            $("#detailorder_checkstatus").val(check_text);
            $("#detailorder_borrowstatus").val(borrow_text);
        
        }
    });

    $("#instrumentInfo").modal("show");
 
}

/******
****获取用户详细信息
*********/
function getuserinfo() {
    var userphone = $("#detailorder_placeorderuser").val();
    $.ajax({
        type: "post",
        url: getuserinfourl,
        data: { 'member_phone': userphone },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data.msg != "success") {
                alert(data.status);
                return false;
            }
            var jsonRecords = data.servers;
          
            $("#detailorder_memberid").val(jsonRecords[0]['USER_ID']);
            $("#detailorder_phone").val(jsonRecords[0]['USER_TEL']);
            $("#detailorder_membertype").val(jsonRecords[0]['MEMBER_TYPE']);
            if (jsonRecords[0]['MEMBER_TYPE'] == "企业会员")
            {
                $(".personinfo").css("display", "none");
                $(".enterinfo").css("display", "block");
                $("#detailorder_enterpersonnickname").val(jsonRecords[0]['NICK_NAME']);
                $("#detailorder_enterpersonname").val(jsonRecords[0]['NAME']);
                $("#detailorder_entername").val(jsonRecords[0]['COMPANY_NAME']);
            }
            else if (jsonRecords[0]['MEMBER_TYPE'] == "个人会员") {
                $(".personinfo").css("display", "block");
                $(".enterinfo").css("display", "none");
                $("#detailorder_nickname").val(jsonRecords[0]['NICK_NAME']);
                $("#detailorder_name").val(jsonRecords[0]['NAME']);

            }
            else {
                $(".personinfo").css("display", "none");
                $(".enterinfo").css("display", "none");
            }
              
               
         

        }
    });

    $("#userinfo").modal("show");

}