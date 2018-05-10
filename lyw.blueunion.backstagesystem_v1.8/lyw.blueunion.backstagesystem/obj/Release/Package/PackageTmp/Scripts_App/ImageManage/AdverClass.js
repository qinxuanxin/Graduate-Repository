var adverclassinfourl = rootUrl + "ImageManage/AdverClassListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var classurl = rootUrl + "ImageManage/AdverClassData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "ImageManage/AdverDelClass?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var addclassurl = rootUrl + "ImageManage/AdverAddClass?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var setupurl = rootUrl + "ImageManage/AdverSetUpStatus?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var allclasscounturl = rootUrl + "ImageManage/AllClassCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址


var count1 = 1;
var count1max;

$(document).ready(function () {
  
   
    requestOrderListData();
    $(".search-adverclass-data").on("click", requestOrderListData);
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);

    /********
   ****弹出添加分类信息框
   ***/
    $(".add-class").on('click', function () {

        $("#AddClassModel").modal("show");
    });
    /********
  ******保存新增分类信息
  **************/
    $(".save-add-class").on('click', function () {
        upLoadClass();


    });
    /******
    ***删除广告位图片
    ********/
    $('.del-classrecord').on('click', function () {

        var classid = GetCheckedValues("classid");
        if (classid == "" || classid == null) {
            alert("请选择需要删除的记录!");
            return false;
        }
        requestDelOrder(classid);


    });
   
    /*********
    ****设置图片显示状态
    *********/
    $('.set-up').on('click', function () {

        var imageid = GetCheckedValues("id");
        if (imageid == "" || imageid == null) {
            alert("请选择需要设置的记录!");
            return false;
        }
        setUpStatus(imageid);


    })

});



/****
***请求广告位分类的数据列表
************/
function requestOrderListData() {


    var postdata = {};
    postdata["class_name"] = $("#adver_classname").val();
    postdata["adver_addtime_start"] = $("#adver_addtime_start").val();
    postdata["adver_addtime_end"] = $("#adver_addtime_end").val();
    postdata["pagesize"] = $(".ui-pg-selbox").val();
    postdata["nowpage"] = count1;


    $.ajax({
        type: "post",
        url: adverclassinfourl,
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
                          //$(this).attr(\"checked\",!$(this).attr(\"checked\"));alert(\"睡觉了\")
                       //   !$(this).attr(\"checked\")
                          '<th><a name="all" onclick="selectall($(this))">全选</a></th>' +
                            //"<th><a onclick='alert(\"是垃圾地方\");'>全选</a></th>"+
                             "<th>ID</th>" +
                                 "<th>分类名称</th>" +
                                "<th>分类描述</th>" +
                                 "<th>加入时间</th>" +
                                 "<th>图片数量</th>" +
                             
                                //"<th>排序</th>" +



                            "</tr>"
                    );
            var orderstatus_text = "";
            var size = "";
            var status = "";
            for (var i = 0; i < jsonRecords.length; i++) {
              
                
                $("#table_list").append(
                                "<tr>" +
                                   "<td>" +

                                 "<input type='checkbox' class='i-checks' name='classid' value='" + jsonRecords[i]['CLASS_ID'] + "'>" +

                                "</td>" +
                                    "<td>" + jsonRecords[i]['CLASS_ID'] + "</td>" +
                                   
                                    

                                    "<td>" + jsonRecords[i]['CLASS_NAME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['CLASS_EXPLAIN'] + "</td>" +
                                      "<td>" + jsonRecords[i]['CLASS_ADDTIME'] + "</td>" +
                                    "<td>" + jsonRecords[i]['CLASS_IMAGE_COUNT'] + "</td>" +
                                    //"<td>" + jsonRecords[i]['SORT'] + "</td>" +

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

    requestOrderListData();
    $("#pagenowshow").val(count1);
};
/*******
****跳至尾页
********/
function btnlastpate() {
    count1 = count1max;

    requestOrderListData();
    $("#pagenowshow").val(count1);
};
/********* 获取checkbox的id*/
function GetCheckedValues(id) {

    var ids = "";


    var chks = $("div.checked input[type='checkbox'][name='" + id + "']");
    for (var index = 0; index < chks.length; index++) {
        if (index > 0)
            ids += ",";
        ids += $(chks[index]).val();
    }
    return ids;
}

/******
***保存新增的分类信息
****/
function upLoadClass()
{
    var postdata = {};
    postdata["class_name"] = $("#model_adver_classname").val();
    postdata["class_explain"] = $("#model_adver_explain").val();
  
    $.ajax({
        type: "post",
        url: addclassurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            alert(data[0].status);
            window.location.reload();
        }
    });
}


/*****
******删除当前记录
*************/
function requestDelOrder(classid) {

    $.ajax({
        type: "post",
        url: delurl,
        data: { 'class_id': classid },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            alert(data[0].status);
            requestOrderListData();
            // window.location.reload();
        }
    });
}

/******
***设置图片显示状态
*******/
function setUpStatus(imageid) {
    var id = imageid.split(",");
    if (id.length > 1) {
        alert("当前只能操作单条记录");
        return false;
    }
    $.ajax({
        type: "post",
        url: setupurl,
        data: { 'image_id': imageid },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            alert(data[0].status);
            requestOrderListData();
            //  window.location.reload();
        }
    });
}


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
}

