var operlogurl = rootUrl + "Home/RequestMoreOperationLog?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var allaccounturl = rootUrl + "Home/RequestAllAccount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "OrderManage/OrderDelOrder?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var orderstatuscounturl = rootUrl + "OrderManage/OrderOrderStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var paystatuscounturl = rootUrl + "OrderManage/OrderPayStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var shippstatuscounturl = rootUrl + "OrderManage/OrderShippStatusCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var savesendgoodsurl = rootUrl + "OrderManage/SendGoods?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址


var count1 = 1;
var count1max;

$(document).ready(function () {
    requestAllAccount();
    requestoperationlog();
    $(".search-log-record").on("click", requestoperationlog);//
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
    
   
});

/****
***请求系统记录的数据列表
************/
function requestoperationlog() {


    var postdata = {};
    postdata["admin_name"] = $("#adminname").val();
    postdata["oper_time_start"] = $("#log_start").val();
    postdata["oper_time_end"] = $("#log_end").val();
  

    postdata["pagesize"] = $(".ui-pg-selbox").val();
    postdata["nowpage"] = count1;
 

    $.ajax({
        type: "post",
        url: operlogurl,
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

            count1max = jsonRecord[0].count / postdata["pagesize"];
            count1max = Math.ceil(count1max);


            $("#sp_1_pager_list_2").text("共" + count1max + "页");
            $("#pagenowshow").val(count1);
            $(".totalnum").text("共" + jsonRecord[0].count + "条");
            var jsonRecords = jsonRecord[0].servers;

            $("#table_list").html(

                          "<tr>" +
                     
                             "<th>编号</th>" +
                                 "<th>操作用户</th>" +
                                "<th>操作日期</th>" +
                                 "<th>IP地址</th>" +
                                 "<th>操作记录</th>" +
                            "</tr>"
                    );
         
            for (var i = 0; i < jsonRecords.length; i++) {
             
                $("#table_list").append(
                                "<tr>" +
                               
                                    "<td>" + jsonRecords[i]['ID'] + "</td>" +
                                       "<td>" + jsonRecords[i]['USER_NAME'] + "</td>" +
                                     "<td>" + jsonRecords[i]['ADD_TIME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['IP'] + "</td>" +

                                    "<td>" + jsonRecords[i]['LOG'] + "</td>" +         
                                "</tr>"
                            );

            }
       





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
    requestoperationlog();
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
    requestoperationlog();
    $("#pagenowshow").val(count1);
};
/*******
****进入下一页
********/
function btnnextpage() {
    if (count1++ == count1max) {
        count1 = count1max;
    }
   
    requestoperationlog();
    $("#pagenowshow").val(count1);
};
/*******
****跳至尾页
********/
function btnlastpate() {
    count1 = count1max;
  
    requestoperationlog();
    $("#pagenowshow").val(count1);
};


/****
***请求所有用户
*********/
function requestAllAccount() {

    $.ajax({
        type: "post",
        url: allaccounturl,
        data: {},
        dataType: 'json',
        async: false,//同步
        success: function (data) {
            if (data[0].msg != "success") {
              
                return false;
            }
            var jsonRecords = data[0].servers;
            var optiondiv = "";

            for (var i = 0; i < jsonRecords.length; i++)
            {
                optiondiv += '<option value="'+jsonRecords[i].ADMIN_NAME+'">' + jsonRecords[i].ADMIN_NAME + '</option>';
            }
            $("#adminname").append(optiondiv);


        }
    });
}

