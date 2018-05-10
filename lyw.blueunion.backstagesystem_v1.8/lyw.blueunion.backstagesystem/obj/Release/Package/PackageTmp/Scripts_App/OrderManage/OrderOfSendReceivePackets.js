var orderofpcburl = rootUrl + "OrderManage/OrderSendTaskListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var detailurl = rootUrl + "OrderManage/OrderDetailInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var bidurl = rootUrl + "OrderManage/OrderToBidSuccess?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "OrderManage/SendReceivePacketDelOrder?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var orderstatuscounturl = rootUrl + "OrderManage/OrderOrderStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var savesendgoodsurl = rootUrl + "OrderManage/SendGoods?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址


var count1 = 1;
var count1max;
var pagetype = "sendreceivetask";
$(document).ready(function () {

    requestOrderListData();
    $(".search-orderpcb-data").on("click", requestOrderListData);//收藏
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
    $(".backprepage").on("click", backpreinfo);
    requestOrderStatusAmount('0');//未接包
    requestOrderStatusAmount('1');//竞标中
    requestOrderStatusAmount('2');//已选中
    requestOrderStatusAmount('3');//托管中
    requestOrderStatusAmount('4');//已完成
  
    /**
  **查询当前任务的详细信息
  **/
    $('.search-taskdetailinfo').on('click', function () {
        var taskid = GetCheckedValues("taskid");
        if (taskid == "" || taskid == null) {
            alert("请选择需要查看的任务单号!")
            return false;
        }

        requestDetailInfo(taskid);
        $(".basictaskinfo").css("display", "none");
        $(".task-detail").css("display", "block");
    });
    /******
    ***删除当前任务单号
    ********/
    $('.del-thistask').on('click', function () {

        var taskid = GetCheckedValues("taskid");
        if (taskid == "" || taskid == null) {
            alert("请选择需要删除的任务单号!");
            return false;
        }
        requestDelOrder(taskid);


    })
   
  
  /**********审核任务单号通过**********/
    $(".bidauditor").on('click', '.auditor-bidin', function () {
        var userid = $(this).data('userid');
        var taskid = $(this).data('taskid');
        signPosition('1', userid, taskid);
    });
    /********审核任务单号为托管中********/
    $(".bidauditor").on('click', '.auditor-deposit', function () {
        var userid = $(this).data('userid');
        var taskid = $(this).data('taskid');
        signPosition('3', userid, taskid);
    });
  
    /******竞标成功********/
    $(".bidinfoin").on('click', '.tobidsucess', function () {
        var userid = $(this).data('userid');
        var taskid = $(this).data('taskid');
        signPosition('2', userid, taskid);
    });
   
});


/********
***任务号状态标志位置位
*******/
function signPosition(flag,userid,taskid)
{
  //  var taskid = $("." + classtype + "").data('taskid');
  //  var userid = $("." + classtype + "").data('userid');
    alert(taskid+":"+userid);
    $.ajax({
        type: "post",
        url: bidurl,
        data: {'task_id':taskid,'user_id':userid,'flag':flag},
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            alert(data[0].status);
            location.reload();
        }
    });
}

/****
***请求接发包的数据列表
************/
function requestOrderListData() {


    var postdata = {};
    postdata["Order_num"] = $("#task_num").val();
  
    postdata["Order_regtime_start"] = $("#order_regtime_start").val();
    postdata["Order_regtime_end"] = $("#order_regtime_end").val();
    postdata["Order_phone"] = $("#task_phone").val();
    postdata["Order_status"] = $("#task_status").val();
  

    postdata["Pagesize"] = $(".ui-pg-selbox").val();
    postdata["Nowpage"] = count1;
    postdata["Type"] = pagetype;//

    $.ajax({
        type: "post",
        url: orderofpcburl,
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
                      
                          '<th><a name="all" onclick="selectall()">全选</a></th>' +
                           
                             "<th>任务单号</th>" +
                                 "<th>发包用户</th>" +
                                 "<th>任务名称</th>" +
                                "<th>发布时间</th>" +
                                 "<th>截止时间</th>" +
                                 "<th>手机号</th>" +
                               
                                "<th>应用领域</th>" +

                                "<th>技术分类</th>" +
                                 "<th>竞标人数</th>" +
                                  "<th>任务状态</th>" +
                            "</tr>"
                    );
            var statustext = "";
            for (var i = 0; i < jsonRecords.length; i++) {
                if (jsonRecords[i]['IS_RECEIVED'] == '0')
                    statustext = "未接包";
                else if (jsonRecords[i]['IS_RECEIVED'] == '1')
                    statustext = "竞标中";
                else if (jsonRecords[i]['IS_RECEIVED'] == '2')
                    statustext = "已选中";
                else if (jsonRecords[i]['IS_RECEIVED'] == '3')
                    statustext = "托管中";
                else if (jsonRecords[i]['IS_RECEIVED'] == '4')
                    statustext = "已完成";
                $("#table_list").append(
                                "<tr>" +
                                   "<td>" +

                                 "<input type='checkbox' class='i-checks' name='taskid' value='" + jsonRecords[i]['TASK_ID'] + "'>" +

                                "</td>" +
                                    "<td>" + jsonRecords[i]['TASK_ID'] + "</td>" +
                                       "<td>" + jsonRecords[i]['USER_NAME'] + "</td>" +
                                     "<td>" + jsonRecords[i]['TASK_NAME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['SEND_TIME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['DEADLINE'] + "</td>" +
                                    "<td>" + jsonRecords[i]['PHONE'] + "</td>" +
                                    "<td>" + jsonRecords[i]['APPLICATION_AREA'] + "</td>" +
                                    "<td>" + jsonRecords[i]['TECHNICAL_CLASSIFICATIONCLASS'] + "</td>" +
                                      "<td>" + jsonRecords[i]['APPLY_NUMBER'] + "</td>" +
                                        "<td>" + statustext + "</td>" +
                                    //"<td><button type='button'  data-pcborderid='" + jsonRecords[i]['ORDER_ID'] + "' class='btn btn-primary search-pcborderdetailinfo'>查看</button></td>" +
                                      //  "<td><button type='button' data-pcborderid='" + jsonRecords[i]['ORDER_ID'] + "' class='btn btn-danger del-thisorderofpcb'>删除</button></td>" +
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
***查看当前任务单号的详细信息
*********/
function requestDetailInfo(taskid) {

    $.ajax({
        type: "post",
        url: detailurl,
        data: { 'order_id': taskid, 'type': pagetype },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            //if (data[0].msg != "success") {
            //    alert(data[0].status);
            //    return false;
            //}
            var send = data[0];
            var jsonsendpackage = send.sendpackage;//发包信息  一个
            var jsonsenduserinfo = send.userinfo_response;//发包人信息 一个
            var receive = data[1];
            var jsonreceivepackage = receive.receivepackage;//接包信息，包括user_id; 多个
            var jsonreceiveuserinfo = receive.userinfo_response;//接包人信息  多个
            var jsonreceive = data[0].receive;//接包人信息
            var jsonsend = data[0].send;//发包人信息
            var statustext = "";
            if (jsonsendpackage['IS_RECEIVED'] == '0')
                statustext = "未接包";
            else if (jsonsendpackage['IS_RECEIVED'] == '1')
                statustext = "竞标中";
            else if (jsonsendpackage['IS_RECEIVED'] == '2')
                statustext = "已选中";
            else if (jsonsendpackage['IS_RECEIVED'] == '3')
                statustext = "托管中";
            else if (jsonsendpackage['IS_RECEIVED'] == '4')
                statustext = "已完成";
            else
                statustext = "未知";
          
            /************发包信息**********/
            $(".taskstatustext").text("任务状态："+statustext);
            $(".bidnum").text("竞标人数:" + jsonsendpackage['APPLY_NUMBER']);
            $(".price").text("报价:" + jsonsendpackage['PRICE']);
            $(".sendphone").text("手机号:" + jsonsendpackage['PHONE']);
            $(".senduser").text("发包用户:" + jsonsenduserinfo['NICK_NAME']);
            $(".taskname").text("任务名称:" + jsonsendpackage['TASK_NAME']);
            $(".usefield").text("应用领域:" + jsonsendpackage['APPLICATION_AREA']);
            $(".tecclass").text("技术分类:" + jsonsendpackage['TECHNICAL_CLASSIFICATIONCLASS']);
            $(".deadline").text("截止时间:" + jsonsendpackage['DEADLINE']);
            $(".sendtime").text("发布时间:" + jsonsendpackage['SEND_TIME']);
            $(".demanddetail").text(jsonsendpackage['DEMAND_DETAIL']);

            $(".demanddescribe").text(jsonsendpackage['DEMAND_DESCRIPTION']);
            /*******审核此发包通过按钮********/

            if (jsonsendpackage['IS_RECEIVED'] == '0')
            {
                var auditordiv = "";
                auditordiv += ' <button class="btn btn-sm btn-primary btn-block m auditor-bidin" data-userid=' + jsonsendpackage.USER_ID + ' data-taskid=' + jsonsendpackage.TASK_ID + '><i class="fa fa-arrow-down"></i> 审核通过</button>';
                $(".bidauditor").html(auditordiv);
            }
          
            /*******审核此"发包通过"按钮********/

          
         
        

            var jsonreceivepackage = receive.receivepackage;//接包信息，包括user_id; 多个
            var jsonreceiveuserinfo = receive.userinfo_response;//接包人信息  多个
            var imageurl="";
                /**************接包信息**************/
                var templatediv = '<p>'
                               + ' <span><i class="fa fa-paperclip"></i> 竞标人信息 </span>'
                               + '</p>';
                var templatedivsuccess = '<p>'
                               + ' <span><i class="fa fa-paperclip"></i> 成功竞标用户信息 </span>'
                               + '</p>';
                for (var i = 0; i < jsonreceivepackage.length; i++) {

                    if (jsonreceiveuserinfo[i].MEMBER_TYPE == "个人会员") {
                        if (jsonreceiveuserinfo[i].CHAT_HEAD == "")
                            imageurl = ""
                        else
                        imageurl = domain_name + chat_head_image + jsonreceiveuserinfo[i].CHAT_HEAD;
                    }
                    else if (jsonreceiveuserinfo[i].MEMBER_TYPE == "企业会员") {
                        if (jsonreceiveuserinfo[i].CHAT_HEAD == "")
                            imageurl = "";
                        else
                        imageurl = domain_name + enter_chat_head + jsonreceiveuserinfo[i].CHAT_HEAD;
                    }
                    else { }

                    if (jsonreceivepackage[i].IS_ACCEPTED != "0" && jsonreceivepackage[i].IS_ACCEPTED!="2") {
                        /*******审核此任务"在托管中"按钮********/
                        if (jsonsendpackage['IS_RECEIVED'] == '2') {
                            var auditordiv = "";
                            auditordiv += ' <button class="btn btn-sm btn-primary btn-block m auditor-deposit" data-userid=' + jsonreceivepackage[i].USER_ID + ' data-taskid=' + jsonsendpackage.TASK_ID + '><i class="fa fa-arrow-down"></i>托管</button>';
                            $(".bidauditor").html(auditordiv);
                        }

                        /*******审核此任务在托管中按钮********/

                        var receivefinishid = "";
                        if (jsonreceivepackage[i].IS_FINISHID == '0')
                            receivefinishid = "未完成";
                        else if (jsonreceivepackage[i].IS_FINISHID == '1')
                            receivefinishid = "完成";
                        var ss = jsonreceiveuserinfo[i].NICK_NAME;
                        templatedivsuccess += '<div class="col-sm-6">'
                                          + '<div class="contact-box">'
                                          + '<a href="profile.html">'
                                          + '<div class="col-sm-4">'
                                          + '<div class="text-center">'
                                          + '<img alt="image" class="img-circle m-t-xs img-responsive" src="' + imageurl + '">'
                                          + '<div class="m-t-xs font-bold"></div>'
                                          + '</div>'
                                          + '</div>'
                                          + '</a>'
                                          + '<div class="col-sm-8">'
                                          + '<a>'
                                          + '<h3><strong>' + jsonreceiveuserinfo[i].NICK_NAME + '</strong></h3>'
                                          + '<p><i class="fa fa-map-marker"></i>' + jsonreceiveuserinfo[i].MEMBER_TYPE + '</p>'
                                          + '</a>'
                                          + '<span title="Phone">Tel:' + jsonreceiveuserinfo[i].USER_TEL + '</span>'
                                          + '<p title="time">成功竞标时间:' + jsonreceivepackage[i].RECEIVE_SUCCESS_TIME + '</p>'
                                          + '<span title="success">是否完成:' + receivefinishid + '</span>'
                                          + '</div>'
                                          + ' <div class="clearfix"></div>'
                                          + ' </div>'
                                          + '</div>';


                    }



                    templatediv += '<div class="col-sm-6">'
                               + '<div class="contact-box">'
                               + '<a href="profile.html">'
                               + '<div class="col-sm-4">'
                               + '<div class="text-center">'
                               + '<img alt="image" class="img-circle m-t-xs img-responsive" src="' + imageurl + '">'
                               + '<div class="m-t-xs font-bold">CTO</div>'
                               + '</div>'
                               + '</div>'
                               + '</a>'
                               + '<div class="col-sm-8">'
                               + '<a>'
                               + '<h3><strong>' + jsonreceiveuserinfo[i].NICK_NAME + '</strong></h3>'
                               + '<p><i class="fa fa-map-marker"></i>' + jsonreceiveuserinfo[i].MEMBER_TYPE + '</p>'
                               + '</a>'
                               + '<span title="Phone">Tel:' + jsonreceiveuserinfo[i].USER_TEL + '</span>'
                                  + '<p>申请接包时间:' + jsonreceivepackage[i].RECEIVE_TIME + '</p>'
                               + '  <a  class="btn btn-white btn-sm tobidsucess" data-userid=' + jsonreceivepackage[i].USER_ID + ' data-taskid=' + jsonreceivepackage[i].TASK_ID + ' data-toggle="tooltip" data-placement="top" title="选中此用户">'
                               + '选中此用户竞标成功</a>'
                               + '</div>'
                               + ' <div class="clearfix"></div>'
                               + ' </div>'
                               + '</div>';





                }
                $(".bidinfoin").html(templatediv);

                $(".bidinfosuccess").html(templatedivsuccess);

                /**************接包信息*******************/
            
            /*****************发包信息************/
                if (jsonsendpackage['IS_RECEIVED'] == '0') {
                    $(".bidauditor").css("display", "block");
                    $(".bidinfosuccess").css("display", "none");
                    $(".bidinfoin").css("display", "none");

                    return false;
                }
                else if (jsonsendpackage['IS_RECEIVED'] == '2') {
                    $(".bidauditor").css("display", "block");
                    $(".bidinfosuccess").css("display", "block");
                    $(".bidinfoin").css("display", "block");
                }
                else {
                    $(".bidauditor").css("display", "none");
                    $(".bidinfosuccess").css("display", "block");
                    $(".bidinfoin").css("display", "block");
                }

        }
    });
}
/*******
*****返回上一页
******************/
function backpreinfo() {
    $(".basictaskinfo").css("display", "block");
    $(".task-detail").css("display", "none");

}
/*****
******删除当前任务单号
*************/
function requestDelOrder(taskid) {

    $.ajax({
        type: "post",
        url: delurl,
        data: { 'order_id': taskid, 'type': pagetype },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            alert(data[0].status);
            window.location.reload();
        }
    });
}

/******任务各状态单号数量*******/
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
                    $(".task-unconfirmed").text(counts);
                else if (orderstatus == '1')
                    $(".task-bidin").text(counts);
                else if (orderstatus == '2')
                    $(".task-bided").text(counts);
                else if (orderstatus == '3')
                    $(".task-deposit").text(counts);
                else if (orderstatus == '4')
                    $(".task-finished").text(counts);
                else
                    return false;
                //document.getElementById("").innerHTML=


            }
        }
    });
}


/******任务各状态单号数量*******/

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
    postdata["order_id"] = GetCheckedValues("taskid");
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
