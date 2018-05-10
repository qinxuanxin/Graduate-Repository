var memberurl = rootUrl + "MemberManage/ProfessionMemberListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var detailurl = rootUrl + "MemberManage/MemberDetailProfessionalInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "MemberManage/ProfessionMemberDeleteMember?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var checkurl = rootUrl + "MemberManage/ProfessionMemberCheck?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址 
var count1 = 1;
var count1max;
var pagetype = "professional";
$(document).ready(function () {
    requestMemberListData();
    $(".search-professional-data").on("click", requestMemberListData);//收藏
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
    $(".backprepage").on("click", backpreinfo);
    $(".checkinfo").on("click", function () {
        var id = $(this).attr("data-id");
        btncheck(id);
    });
    /**
  **查询当前自由职业会员的详细信息
  **/
    $('#table_list').on('click', '.search-professionaldetailinfo', function () {

        var member_id = $(this).data('memberid');
        var profession = $(this).data('profession');
        
        requestDetailInfo(member_id,profession);
        $(".basicprofessional").css("display", "none");
        $(".detailprofessional").css("display", "block");

    })
    /******
    ***删除当前自由职业会员
    ********/
    $('#table_list').on('click', '.del-thisprofessional', function () {

        var member_id = $(this).data('memberid');
        var profession = $(this).data('profession');
        requestDelMember(member_id, profession);


    })

});

/****
***请求自由职业会员的数据列表
************/
function requestMemberListData() {


    var postdata = {};
    postdata["memberName"] = $("#membername").val();
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
            if (jsonRecord.msg != "success") {
                $('.toastdiv').toast({
                    content: jsonRecord.status,
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
            count1max = jsonRecord.count / postdata["pagesize"];
            count1max = Math.ceil(count1max);


            $("#sp_1_pager_list_2").text("共" + count1max + "页");
            $("#pagenowshow").val(count1);
            $(".totalnum").text("共" + jsonRecord.count + "条");
            var jsonRecords = jsonRecord.servers;

            $("#table_list").html(

                          "<tr>" +
                                "<th>会员编号</th>" +
                                "<th>手机号</th>" +
                                "<th>会员名字</th>" +
                                 "<th>性别</th>" +
                                 "<th>职业</th>" +
                                "<th>职能</th>" +
                                "<th>学历</th>" +
                                 "<th>专注领域</th>" +
                                 "<th>审核状态</th>" +
                                 "<th></th>" +
                                "<th></th>" +
                            "</tr>"
                    );
            var checkstatus = "";
            for (var i = 0; i < jsonRecords.length; i++) {
                if (jsonRecords[i]['SUCCESS'] == '0')
                    checkstatus = "申请中";
                else if (jsonRecords[i]['SUCCESS'] == '1')
                    checkstatus = "注册成功";
                else
                    checkstatus = "未知状态";
                $("#table_list").append(
                                "<tr>" +
                                    "<td>" + jsonRecords[i]['USER_ID'] + "</td>" +
                                       "<td>" + jsonRecords[i]['USER_TEL'] + "</td>" +
                                    "<td>" + jsonRecords[i]['NAME'] + "</td>" +
                                    "<td>" + jsonRecords[i]['SEX'] + "</td>" +

                                    "<td>" + jsonRecords[i]['PROFESSION'] + "</td>" +
                                    "<td>" + jsonRecords[i]['FUNCTION'] + "</td>" +
                                    "<td>" + jsonRecords[i]['EDUCATION'] + "</td>" +
                                     "<td>" + jsonRecords[i]['FIELD'] + "</td>" +
                                  "<td>" + checkstatus + "</td>" +
                                    "<td><button type='button' data-profession='" + jsonRecords[i]['PROFESSION'] + "'  data-memberid='" + jsonRecords[i]['USER_ID'] + "' class='btn btn-primary search-professionaldetailinfo'>查看</button></td>" +
                                        "<td><button type='button' data-profession='" + jsonRecords[i]['PROFESSION'] + "'  data-memberid='" + jsonRecords[i]['USER_ID'] + "' class='btn btn-danger del-thisprofessional'>删除</button></td>" +
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
***查看自由职业会员详细信息
*********/
function requestDetailInfo(userid, profession) {

    $.ajax({
        type: "post",
        url: detailurl,
        data: { 'user_id': userid, 'profession': profession },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success") {
                alert(data[0].status);
                return false;
            }
            var jsonRecords = data[0].servers;
            $("#detailinfo_num").val(jsonRecords[0]['USER_ID']);
          //  $("#detailinfo_name").val(jsonRecords[0]['NAME']);
            $("#detailinfo_sex").val(jsonRecords[0]['SEX']);
            $("#detailinfo_profession").val(jsonRecords[0]['PROFESSION']);
            $("#detailinfo_function").val(jsonRecords[0]['FUNCTION']);
            $("#detailinfo_education").val(jsonRecords[0]['EDUCATION']);
            $("#detailinfo_field").val(jsonRecords[0]['FIELD']);
            $("#detailinfo_selfintroduction").val(jsonRecords[0]['SELFINTRODUCTION']);
           

          


        }
    });
}
/*******
*****返回上一页
******************/
function backpreinfo() {
    $(".basicprofessional").css("display", "block");
    $(".detailprofessional").css("display", "none");

}
/*****
******删除当前自由职业会员
*************/
function requestDelMember(userid, profession) {
    $.ajax({
        type: "post",
        url: delurl,
        data: { 'user_id': userid, 'profession': profession },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            $('.toastdiv').toast({
                content: data.status,
                duration: 1000
            });
       //     window.location.reload();
        }
    });
}
/****
***审核自由职业
*******/
function btncheck(id) {
    var userid = $("#detailinfo_num").val();
    var profession = $("#detailinfo_profession").val();
    $.ajax({
        type: "post",
        url: checkurl,
        data: { 'check_id': id, 'user_id': userid,'profession':profession },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            $('.toastdiv1').toast({
                content: data.status,
                duration: 1000
            });
        }

    });
}