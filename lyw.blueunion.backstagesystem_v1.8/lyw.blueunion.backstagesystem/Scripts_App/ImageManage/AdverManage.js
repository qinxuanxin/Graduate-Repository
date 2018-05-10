var adverinfourl = rootUrl + "ImageManage/AdverListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var classurl = rootUrl + "ImageManage/AdverClassData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "ImageManage/AdverDelImage?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var addimageurl = rootUrl + "ImageManage/AdverAddImage?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var setupurl = rootUrl + "ImageManage/AdverSetUpStatus?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var allclasscounturl = rootUrl + "ImageManage/AllClassCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址


var count1 = 1;
var count1max;

$(document).ready(function () {
    allcount();
    requestAllClass();
    requestOrderListData();
    $(".search-adverlist-data").on("click", requestOrderListData);
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
   
 
  
    /******
    ***删除广告位图片
    ********/
    $('.del-imagerecord').on('click', function () {

        var imageid = GetCheckedValues("id");
        if (imageid == "" || imageid == null) {
            alert("请选择需要删除的记录!");
            return false;
        }
        requestDelOrder(imageid);


    });
    /********
    ****弹出添加图片信息框
    ***/
    $(".add-image").on('click', function () {
       
        $("#AddImageModel").modal("show");
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
window.onload = function ()
{
    /********
   ******保存新增图片信息
   **************/
    $(".save-add-image").on('click', function () {
        upLoadFile();


    });
}

/*****
****所有广告位分类的数量
**********/
function allcount()
{
    $.ajax({
        type: "post",
        url: allclasscounturl,
        data: {},
        dataType: 'json',
        async: false,//同步
        success: function (data) {
            var lidiv="";
            if (data[0].msg == "fail")
            {
                lidiv+= '<li>'
                     +'<a>'
                     +'<i class="fa fa-inbox "></i> 暂无分类，请添加分类'
                     +'</a>'
                     +'</li>';
                $(".adverul").append(lidiv);
            }
            else
            {
                var jsondata=data[0].servers;
                for(var i=0;i<jsondata.length;i++)
                {
                    lidiv += '<li>'
                         + '<a>'
                         + '<i class="fa fa-inbox "></i>' + jsondata[i].CLASS_NAME
                         + '<span class="label label-warning pull-right">' + jsondata[i].COUNT + '</span>'
                         + '</a>'
                         + '</li>';
                }
                $(".adverul").append(lidiv);
            }
          
        }
    });
    
}

/********
****查询所有分类
****************/
function requestAllClass()
{
    $.ajax({
        type: "post",
        url: classurl,
        data: {},
        dataType: 'json',
        async: false,//同步
        success: function (data) {
            if (data[0].msg != "success")
                alert(data[0].status);
            var classobj = data[0].servers;
            var optioncontent = "";
            for (var i = 0; i < classobj.length; i++)
            {
                optioncontent += '<option value="' + classobj[i]['CLASS_ID'] + '">' + classobj[i]['CLASS_NAME'] + '</option>';
           //     $("#adver_class").append('<option value="' + classobj[i]['CLASS_ID'] + '">' + classobj[i]['CLASS_NAME'] + '</option>');
            }
            $("#adver_class").append(optioncontent);
            $("#model_adver_class").append(optioncontent);

        }
    });
}
/****
***请求广告位的数据列表
************/
function requestOrderListData() {


    var postdata = {};
    postdata["Adver_class"] = $("#adver_class").val();
    postdata["Adver_addtime_start"] = $("#adver_addtime_start").val();
    postdata["Adver_addtime_end"] = $("#adver_addtime_end").val();
    postdata["Adver_status"] = $("#adver_status").val();
   

    postdata["Pagesize"] = $(".ui-pg-selbox").val();
    postdata["Nowpage"] = count1;
   

    $.ajax({
        type: "post",
        url: adverinfourl,
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
                          //$(this).attr(\"checked\",!$(this).attr(\"checked\"));alert(\"睡觉了\")
                       //   !$(this).attr(\"checked\")
                          '<th><a name="all" onclick="selectall($(this))">全选</a></th>' +
                            //"<th><a onclick='alert(\"是垃圾地方\");'>全选</a></th>"+
                             "<th>ID</th>" +
                                 "<th>图片</th>" +
                                "<th>尺寸(大小)</th>" +
                                 "<th>加入时间</th>" +
                                 "<th>所在分类</th>" +
                                 "<th>链接地址</th>" +
                                "<th>状态</th>" +
                                //"<th>排序</th>" +

                           

                            "</tr>"
                    );
            var orderstatus_text = "";
            var size = "";
            var status = "";
            for (var i = 0; i < jsonRecords.length; i++) {
                {
                    if (jsonRecords[i]['IS_SHOW'] == '0')
                        status = "隐藏";
                    else if (jsonRecords[i]['IS_SHOW'] == '1')
                        status = "显示";
                  
                    else
                        status = "未知";
                }
                size = jsonRecords[i]['WIDTH'] + "x" + jsonRecords[i]['HIGH'];
                $("#table_list").append(
                                "<tr>" +
                                   "<td>" +

                                 "<input type='checkbox' class='i-checks' name='id' value='" + jsonRecords[i]['ID'] + "'>" +

                                "</td>" +
                                    "<td>" + jsonRecords[i]['ID'] + "</td>" +
                                       "<td> <span class='ad_img'><a target='_blank' href='" + domain_name + carouselfigure_image + jsonRecords[i]['AD_IMAGE'] + "'><img src='" + domain_name + carouselfigure_image + jsonRecords[i]['AD_IMAGE'] + "' width='100%' height='100%'></a></span></td>" +
                                    //    "<td> <span>https://www.weby.cc/demo/meil88/2017/houtai/01/img/p_big1.jpg</span></td>" +
                                     "<td>" + size+ "</td>" +

                                    "<td>" + jsonRecords[i]['ADD_TIME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['CLASS_NAME'] + "</td>" +
                                      "<td>" + jsonRecords[i]['LINK_ADDRESS'] + "</td>" +
                                    "<td>" + status + "</td>" +
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

//提交新增图片
function upLoadFile() {
    var ajaxdata={};
    ajaxdata["Adver_class"] = $("#model_adver_class").val();
    ajaxdata["Width"] = $("#image-width").val();
    ajaxdata["High"] = $("#image-high").val();
    ajaxdata["Link_address"] = $("#model_adver_linkaddress").val();
    ajaxdata["Is_show"] = $(".radio-status input[type='radio']:checked").val();
    for (var key in ajaxdata) {
        if (ajaxdata[key] == "")
        {
            alert("请填写完信息");
            return false;
        }
    }
    //var addImgName = $("#addImgName").val();
  
        var options = {
            type: "POST",
            url: addimageurl,
            data: ajaxdata,
            dataType: "json",
            success: function (response) {

                alert(response[0].status);
                location.reload();
            }
        };
        // 将options传给ajaxForm
        $('#myForm').ajaxSubmit(options);
  
  
}

/*****
******删除当前记录
*************/
function requestDelOrder(imageid) {

    $.ajax({
        type: "post",
        url: delurl,
        data: { 'image_id': imageid },
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
function setUpStatus(imageid)
{
    var id = imageid.split(",");
    if (id.length > 1)
    {
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

