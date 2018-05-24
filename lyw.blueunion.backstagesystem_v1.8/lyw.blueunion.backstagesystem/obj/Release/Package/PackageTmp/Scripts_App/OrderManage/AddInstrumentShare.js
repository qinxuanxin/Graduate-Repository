var addinstshareurl = rootUrl + "OrderManage/AddInstrumentShareListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var detailurl = rootUrl + "OrderManage/AddInstrumentDetailInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "OrderManage/AddInstrumentDel?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var checkUrl = rootUrl + "OrderManage/AddInstrumentCheck?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var paystatuscounturl = rootUrl + "OrderManage/OrderPayStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var shippstatuscounturl = rootUrl + "OrderManage/OrderShippStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var savesendgoodsurl = rootUrl + "OrderManage/SelfHelpSendGoods?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var returngoodsrul = rootUrl + "OrderManage/SelfHelpReturnGoods?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址

var count1 = 1;
var count1max;
var pagetype = "instrument";
$(document).ready(function () {

    requestInstrumentListData();
    $(".search-instrument-info").on("click", requestInstrumentListData);//收藏
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
    $(".backprepage").on("click", backpreinfo);
    /*********审核设备*******/
    $(".checkbtn").click(function () {
        var status = $(this).attr("data-status");
        var instrumentid = $("#detailorder_instrumentid").val();
        checkInstrument(instrumentid, status);
    });
    /*********审核设备*******/
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
            alert("请选择需要查看的设备!")
            return false;
        }

        requestDetailInfo(instrumentorderid);
        $(".basicorderinfo").css("display", "none");
        $(".instrumentofpcbinfo").css("display", "block");

    });
    /******
    ***删除当前加入仪器设备
    ********/
    $('.del-thisinstrument').on('click', function () {

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
***请求加入仪器共享设备的数据列表
************/
function requestInstrumentListData() {


    var postdata = {};
    postdata["Instrument_id"] = $("#instrument_num").val();
    postdata["User_phone"] = $("#instrument_phone").val();
    //postdata["Order_regtime_start"] = $("#order_regtime_start").val();
    //postdata["Order_regtime_end"] = $("#order_regtime_end").val();
  //  postdata["Order_status"] = $("#order_status").val();
 //   postdata["Pay_status"] = $("#pay_status").val();
  //  postdata["Shipping_status"] = $("#shipping_status").val();

    postdata["Pagesize"] = $(".ui-pg-selbox").val();
    postdata["Nowpage"] = count1;
   // postdata["Type"] = pagetype;//

    $.ajax({
        type: "post",
        url: addinstshareurl,
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
                             "<th>设备编号</th>" +
                                 "<th>用户</th>" +
                                "<th>添加时间</th>" +
                                 "<th>设备名称</th>" +
                                 "<th>设备类型</th>" +
                                "<th>审核状态</th>" +
                            
                            "</tr>"
                    );
            var status_text = "";
            var paystatus_text = "";
            var shipstatus_text = "";
            for (var i = 0; i < jsonRecords.length; i++) {
                {
                    if (jsonRecords[i]['REV_FLAG'] == '0')
                        status_text = "审核中";
                    else if (jsonRecords[i]['REV_FLAG'] == '1')
                        status_text = "审核失败";
                    else if (jsonRecords[i]['REV_FLAG'] == '2')
                        status_text = "审核成功";
                  
                    else
                        status_text = "未知";
                }
            
                $("#table_list").append(
                                "<tr>" +
                                   "<td>" +

                                 "<input type='checkbox' class='i-checks' name='instrumentorderid' value='" + jsonRecords[i]['INSTRUMENT_ID'] + "'>" +

                                "</td>" +
                                    "<td>" + jsonRecords[i]['INSTRUMENT_ID'] + "</td>" +
                                       "<td>" + jsonRecords[i]['USER_TEL'] + "</td>" +
                                     "<td>" + jsonRecords[i]['ADD_TIME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['NAME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['TYPE'] + "</td>" +
                                    "<td>" + status_text + "</td>" +
                                   
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
    requestInstrumentListData();
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
    requestInstrumentListData();
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
    requestInstrumentListData();
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
    requestInstrumentListData();
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
***查看当前设备的详细信息
*********/
function requestDetailInfo(instrumentorderid) {

    $.ajax({
        type: "post",
        url: detailurl,
        data: { 'instrument_id': instrumentorderid},
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
            $("#detailorder_instrumentid").val(jsonRecords[0]['INSTRUMENT_ID']);
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
}
/*******
*****返回上一页
******************/
function backpreinfo() {
   
    $(".basicorderinfo").css("display", "block");
    $(".instrumentofpcbinfo").css("display", "none");
}
/*****
******删除当前仪器设备
*************/
function requestDelOrder(instrumentorderid) {

    $.ajax({
        type: "post",
        url: delurl,
        data: { 'instrumentorderid': instrumentorderid },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            alert(data.status);

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
/********审核设备*********/
function checkInstrument(instrumentorderid, status)
{
    $.ajax({
        type: "post",
        url: checkUrl,
        data: { instrumentorderid: instrumentorderid, status: status },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data.msg != "success")
                return false;
            else
                alert(data.status);
            window.location.reload();
        }
    });
}
