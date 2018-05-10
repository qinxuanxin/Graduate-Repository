var memberurl = rootUrl + "MemberManage/MemberListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var detailurl = rootUrl + "MemberManage/MemberDetailMemberUnitInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "MemberManage/MemberDeleteMember?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址

var count1 = 1;
var count1max;
var pagetype = "memberunit";
$(document).ready(function () {
    requestMemberListData();
    $(".search-memberunit-data").on("click", requestMemberListData);//收藏
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
    $(".backprepage").on("click", backpreinfo);
    /**
  **查询当前会员单位的详细信息
  **/
    $('#table_list').on('click', '.search-memberunitdetailinfo', function () {

        var member_id = $(this).data('memberid');
        requestDetailInfo(member_id);
        $(".basicpersonprise").css("display", "none");
        $(".detailpersonprise").css("display", "block");

    })
    /******
    ***删除当前会员单位
    ********/
    $('#table_list').on('click', '.del-thismemberunitprise', function () {

        var member_id = $(this).data('memberid');
        requestDelMember(member_id);


    })

});

/****
***请求会员单位的数据列表
************/
function requestMemberListData() {


    var postdata = {};
    postdata["memberName"] = $("#enterprisename").val();
    postdata["registerTime"] = $("#memberregtime").val();

    postdata["pagesize"] = $(".ui-pg-selbox").val();
    postdata["nowpage"] = count1;
    postdata["type"] = pagetype;//

    $.ajax({
        type: "post",
        url: memberurl,
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
                                "<th>会员编号</th>" +

                                "<th>企业名称</th>" +
                                 "<th>注册时间</th>" +
                                 "<th>成立时间</th>" +
                                "<th>员工人数</th>" +
                                //"<th></th>" +

                                //"<th>注册时间</th>" +
                                 "<th></th>" +
                                "<th></th>" +
                            "</tr>"
                    );
            for (var i = 0; i < jsonRecords.length; i++) {

                $("#table_list").append(
                                "<tr>" +
                                    "<td>" + jsonRecords[i]['USER_ID'] + "</td>" +
                                    "<td>" + jsonRecords[i]['MEMBER_NAME'] + "</td>" +
                                    "<td>" + jsonRecords[i]['ENTERPRISE_REG_TIME'] + "</td>" +

                                    "<td>" + jsonRecords[i]['ESTABLISHED'] + "</td>" +
                                    "<td>" + jsonRecords[i]['EMPLOYEES_NUMBER'] + "</td>" +
                                    //"<td>" + jsonRecords[i]['ID_NUMBER'] + "</td>" +
                                    //"<td>" + jsonRecords[i]['REG_TIME'] + "</td>" +
                                    "<td><button type='button'  data-memberid='" + jsonRecords[i]['USER_ID'] + "' class='btn btn-primary search-memberunitdetailinfo'>查看</button></td>" +
                                        "<td><button type='button' data-memberid='" + jsonRecords[i]['USER_ID'] + "' class='btn btn-danger del-thismemberunitprise'>删除</button></td>" +
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
    requestMemberListData();
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
    requestMemberListData();
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
    requestMemberListData();
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
    requestMemberListData();
    $("#pagenowshow").val(count1);
};
/****
***查看会员单位详细信息
*********/
function requestDetailInfo(userid) {

    $.ajax({
        type: "post",
        url: detailurl,
        data: { 'user_id': userid },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success") {
                alert(data[0].status);
                return false;
            }
            var jsonRecords = data[0].servers;
            $("#memberunitinfo_num").val(jsonRecords[0]['USER_ID']);
            $("#memberunitinfo_regtime").val(jsonRecords[0]['ENTERPRISE_REG_TIME']);
            $("#memberunitinfo_name").val(jsonRecords[0]['MEMBER_NAME']);
            $("#memberunitinfo_establish").val(jsonRecords[0]['ESTABLISHED']);
            $("#memberunitinfo_membernum").val(jsonRecords[0]['EMPLOYEES_NUMBER']);
            $("#memberunitinfo_system").val(jsonRecords[0]['SYSTEM_CERTIFICATION']);
            $("#memberunitinfo_service").val(jsonRecords[0]['SERVICE_ITEMS']);
            $("#memberunitinfo_equipmentlist").val(jsonRecords[0]['EQUIPMENT_LIST']);
           


        }
    });
}
/*******
*****返回上一页
******************/
function backpreinfo() {
    $(".basicpersonprise").css("display", "block");
    $(".detailpersonprise").css("display", "none");

}
/*****
******删除当前会员单位
*************/
function requestDelMember(userid) {
    $.ajax({
        type: "post",
        url: delurl,
        data: { 'user_id': userid },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            alert(data[0].status);
            window.location.reload();
        }
    });
}