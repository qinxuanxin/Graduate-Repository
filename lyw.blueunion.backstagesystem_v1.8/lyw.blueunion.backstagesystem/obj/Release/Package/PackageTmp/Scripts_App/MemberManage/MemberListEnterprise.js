var memberurl = rootUrl + "MemberManage/EnterMemberListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var detailurl = rootUrl + "MemberManage/MemberDetailEnterpriseInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl = rootUrl + "MemberManage/EnterMemberDeleteMember?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址

var count1 = 1;
var count1max;
//var pagetype = "enterprise";
var pagetype = "enterprise";
$(document).ready(function () {
    requestMemberListData();
    $(".search-enterprise-data").on("click", requestMemberListData);//收藏
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
    $(".backprepage").on("click", backpreinfo);
    
    /**
    **查询当前企业会员的详细信息
    **/
    $('#table_list').on('click', '.search-enterdetailinfo', function () {
    
      var member_id=$(this).data('memberid');
      requestDetailInfo(member_id);
      $(".basicenterprise").css("display", "none");
      $(".detailenterprise").css("display", "block");
      
    })
    /******
    ***删除当前企业会员
    ********/
    $('#table_list').on('click', '.del-thisenterprise', function () {

        var member_id = $(this).data('memberid');
        requestDelMember(member_id);
      

    })
    
});

/****
***请求企业会员的数据列表
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

                                "<th>会员名字</th>" +
                                
                                 "<th>手机号</th>" +
                                "<th>邮箱</th>" +
                                "<th>企业名称</th>" +

                                "<th>注册时间</th>" +
                                 "<th>企业地址</th>" +
                                 "<th></th>" +
                                "<th></th>" +
                            "</tr>"
                    );
            for (var i = 0; i < jsonRecords.length; i++) {

                $("#table_list").append(
                                "<tr>" +
                                    "<td>" + jsonRecords[i]['USER_ID'] + "</td>" +
                                    "<td>" + jsonRecords[i]['NAME'] + "</td>" +
                                  

                                    "<td>" + jsonRecords[i]['USER_TEL'] + "</td>" +
                                    "<td>" + jsonRecords[i]['EMAIL'] + "</td>" +
                                    "<td>" + jsonRecords[i]['COMPANY_NAME'] + "</td>" +
                                    "<td>" + jsonRecords[i]['REG_TIME'] + "</td>" +
                                      "<td>" + jsonRecords[i]['ADDRESS'] + "</td>" +
                                   "<td><button type='button'  data-memberid='" + jsonRecords[i]['USER_ID'] + "' class='btn btn-primary search-enterdetailinfo'>查看</button></td>" +
                                    "<td><button type='button' data-memberid='" + jsonRecords[i]['USER_ID'] + "' class='btn btn-danger del-thisenterprise'>删除</button></td>" +
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
***查看企业会员详细信息
*********/
function requestDetailInfo(userid)
{

    $.ajax({
        type: "post",
        url: detailurl,
        data: {'user_id':userid},
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success") {
                alert(data[0].status);
                return false;
            }
            var jsonRecords = data[0].servers;
            var chat_head = domain_name + enter_chat_head + jsonRecords[0]['CHAT_HEAD'];
            var license = domain_name + enter_busi_license + jsonRecords[0]['BUSINESS_LICENSE'];
            var credential = domain_name + enter_credential + jsonRecords[0]['CREDENTIAL'];
            var honor = domain_name + enter_honor + jsonRecords[0]['HONOR'];
            var quality = domain_name + enter_qual_gua + jsonRecords[0]['QUALITY_GUARANTEE'];
            var related = domain_name + enter_related + jsonRecords[0]['RELATED_PICTURE'];
            $("#detailinfo_num").val(jsonRecords[0]['USER_ID']);
            if (jsonRecords[0]['CHAT_HEAD'] != "")
                $("#detailinfo_head").attr('data-original', chat_head);
            else
                $("#detailinfo_head").attr('data-original', "/img/default-head.jpg");
            $("#detailinfo_phone").val(jsonRecords[0]['USER_TEL']);
            $("#detailinfo_name").val(jsonRecords[0]['NAME']);
            $("#detailinfo_nickname").val(jsonRecords[0]['NICK_NAME']);
            $("#detailinfo_entername").val(jsonRecords[0]['COMPANY_NAME']);
            $("#detailinfo_enterofindustry").val(jsonRecords[0]['COMPANY_INDUSTRY_INVOLVED']);
            $("#detailinfo_enterintroduce").val(jsonRecords[0]['COMPANY_INTRODUCE']);
            if (jsonRecords[0]['BUSINESS_LICENSE']!="")
                $("#detailinfo_license").attr('data-original', license);
            else
                $("#detailinfo_license").attr('data-original', "/img/id-card.png");
            $("#detailinfo_producintroduce").val(jsonRecords[0]['COMPANY_CAPABILITY']);
            if (jsonRecords[0]['CREDENTIAL'] != "")
                $("#detailinfo_certificate").attr('data-original', credential);
            else
                $("#detailinfo_certificate").attr('data-original', "/img/id-card.png");
            if (jsonRecords[0]['HONOR'] != "")
                $("#detailinfo_honor").attr('data-original', honor);
            else
                $("#detailinfo_honor").attr('data-original', "/img/id-card.png");
            $("#detailinfo_relateintroduce").val(jsonRecords[0]['ARCHITECTURE_RELATED']);
            if (jsonRecords[0]['RELATED_PICTURE'] != "")
                $("#detailinfo_relatedimages").attr('data-original', related);
            else
                $("#detailinfo_relatedimages").attr('data-original', "/img/id-card.png");
            if (jsonRecords[0]['QUALITY_GUARANTEE'] != "")
                $("#detailinfo_QA").attr('data-original', quality);
            else
                $("#detailinfo_QA").attr('data-original', "/img/id-card.png");
           

            $("#detailinfo_email").val(jsonRecords[0]['EMAIL']);
            $("#detailinfo_regtime").val(jsonRecords[0]['REG_TIME']);
           
            $("#detailinfo_enterphone").val(jsonRecords[0]['TEL']);
            $("#detailinfo_fax").val(jsonRecords[0]['FAX']);
            $("#detailinfo_enteraddress").val(jsonRecords[0]['ADDRESS']);
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
function backpreinfo()
{
    $(".basicenterprise").css("display", "block");
    $(".detailenterprise").css("display", "none"); 
    
    //var indexenterurl = rootUrl + "MemberManage/MemberListEnterprise?random=" + Math.floor(Math.random() * (100000 + 1));
    //$(window.parent.document).find(".J_iframe").attr("src", indexenterurl);
}
/*****
******删除当前企业会员
*************/
function requestDelMember(userid)
{
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
            window.location.reload();
        }
    });
}