var memberurl = rootUrl + "MemberManage/MemberListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var detailurl = rootUrl + "MemberManage/MemberDetailPersonalInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "MemberManage/MemberDeleteMember?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址

var count1 = 1;
var count1max;

//window.onload = function () {

$(document).ready(function () {
  
    requestMemberListData();
    $(".search-person-data").on("click", requestMemberListData);//收藏
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
    $(".backprepage").on("click", backpreinfo);
    /**
  **查询当前企业会员的详细信息
  **/
    $('#table_list').on('click', '.search-persondetailinfo', function () {

        var member_id = $(this).data('memberid');
        requestDetailInfo(member_id);
        $(".basicpersonprise").css("display", "none");
        $(".detailpersonprise").css("display", "block");

    })
    /******
    ***删除当前企业会员
    ********/
    $('#table_list').on('click', '.del-thispersonprise', function () {

        var member_id = $(this).data('memberid');
        requestDelMember(member_id);


    })
    
});

/****
***请求个人会员的数据列表
************/
function requestMemberListData()
{
  

    var postdata = {};
    postdata["memberName"] = $("#membername").val();
    postdata["registerTime"] = $("#memberregtime").val();
   
    postdata["pagesize"] = $(".ui-pg-selbox").val();
    postdata["nowpage"] = count1;
    postdata["type"] = "personal";//
    
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
            if (jsonRecord.msg != "success")
            {
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
              
            if (jsonRecord.count == 0)
            {
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
                      
                        "<th>会员名字</th>" +
                         "<th>性别</th>" +
                         "<th>手机号</th>" +
                        "<th>邮箱</th>" +
                        "<th>身份证号</th>" +
                       
                        "<th>注册时间</th>" +
                         "<th></th>" +
                        "<th></th>" +
                    "</tr>"
            );
        for (var i = 0; i < jsonRecords.length; i++) {
         
            $("#table_list").append(
                            "<tr>" +
                                "<td>" + jsonRecords[i]['USER_ID'] + "</td>" +
                                "<td>" + jsonRecords[i]['NAME'] + "</td>" +
                                "<td>" + jsonRecords[i]['SEX'] + "</td>" +
                              
                                "<td>" + jsonRecords[i]['USER_TEL'] + "</td>" +
                                "<td>" + jsonRecords[i]['EMAIL'] + "</td>" +
                                "<td>" + jsonRecords[i]['ID_NUMBER'] + "</td>" +
                                "<td>" + jsonRecords[i]['REG_TIME'] + "</td>" +
                                "<td><button type='button'  data-memberid='" + jsonRecords[i]['USER_ID'] + "' class='btn btn-primary search-persondetailinfo'>查看</button></td>" +
                                    "<td><button type='button' data-memberid='" + jsonRecords[i]['USER_ID'] + "' class='btn btn-danger del-thispersonprise'>删除</button></td>" +
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
***查看个人会员详细信息
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
            
            var chat_head = domain_name + chat_head_image + jsonRecords[0]['CHAT_HEAD'];
            var id_card = domain_name + id_card_image + jsonRecords[0]['ID_CARD'];
            $("#detailinfo_num").val(jsonRecords[0]['USER_ID']);
            if (jsonRecords[0]['CHAT_HEAD']!="")
                $("#detailinfo_head").attr('data-original', chat_head);
            else
               $("#detailinfo_head").attr('data-original', "/img/default-head.jpg");
            
            $("#detailinfo_name").val(jsonRecords[0]['NAME']);
            $("#detailinfo_nickname").val(jsonRecords[0]['NICK_NAME']);
            
            $("#detailinfo_sex").val(jsonRecords[0]['SEX']);
            $("#detailinfo_phone").val(jsonRecords[0]['USER_TEL']);
            $("#detailinfo_email").val(jsonRecords[0]['EMAIL']);
            $("#detailinfo_regtime").val(jsonRecords[0]['REG_TIME']);
            $("#detailinfo_idcard").val(jsonRecords[0]['ID_NUMBER']);
            if (jsonRecords[0]['ID_CARD']!="")
                $("#detailinfo_certificate").attr('data-original', id_card);
           else
               $("#detailinfo_certificate").attr('data-original', "/img/id-card.png");
            $("#detailinfo_age").val(jsonRecords[0]['AGE']);
            $("#detailinfo_ofindustry").val(jsonRecords[0]['INDUSTRY_INVOLVED']);
            $("#detailinfo_company").val(jsonRecords[0]['COMPANY']);
            $("#detailinfo_profession").val(jsonRecords[0]['PROFESSION']);
             $("#detailinfo_profession").val(jsonRecords[0]['PROFESSION']);
             $("#detailinfo_signature").val(jsonRecords[0]['SIGNATURE']);
            $("#detailinfo_fax").val(jsonRecords[0]['FAX']);
          
            $("img").lazyload({ 
                placeholder: "/images/loading.gif",
                threshold: 20,
                effect: "fadeIn"
            });  
        }
    });
}
/*******
*****返回上一页
******************/
function backpreinfo() {
    
    $(".basicpersonprise").css("display", "block");
    $(".detailpersonprise").css("display", "none");
    //var indexpersonurl = rootUrl + "MemberManage/MemberList?random=" + Math.floor(Math.random() * (100000 + 1));
    //$(window.parent.document).find(".J_iframe").attr("src", indexpersonurl);
 
}
/*****
******删除当前个人会员
*************/
function requestDelMember(userid) {
    $.ajax({
        type: "post",
        url: delurl,
        data: { 'user_id': userid },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            
            $('.toastdiv').toast({
                content: data.status,
                duration: 1000
            });
              
            requestMemberListData();
           // window.location.reload();
        }
    });
}