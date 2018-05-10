var saveurl = rootUrl + "Home/SaveSecret?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var searchlurl = rootUrl + "Home/SearchUserRecord?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var count1 = 1;
var count1max;

$(function () {

    $(".btn-editsecret").on('click', function () {
        $(".secret-div").css("display", "block");
    });
    $(".btn-savesecret").on('click', savesecretfunc);

    searchRecord();
    $("#first_pager_list_2").on("click", btnfirstpage);
    $("#prev_pager_list_2").on("click", btnprepage);
    $("#next_pager_list_2").on("click", btnnextpage);
    $("#last_pager_list_2").on("click", btnlastpate);
});
///保存密码
function savesecretfunc()
{
    if ($("#adminfirstsecret").val() != "" && $("#adminnewsecret").val() != "" && $("#adminconfirmnewsecret").val() != "") {
        if ($("#adminnewsecret").val() != $("#adminconfirmnewsecret").val()) {
            $(".response-content").text("新密码两次输入不一样");
       
            return false;
        }
        else
            ;
    }
    else {
        $(".response-content").text("存在未填写的选项");
       
        return false;
    }

    var postdata = {};
    postdata["initsecret"] = $("#adminfirstsecret").val();
    postdata["newsecret"] = $("#adminconfirmnewsecret").val();

    $.ajax({
        type: "post",
        url: saveurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            $(".response-content").text(data.status);
           
            location.reload();

        }
    });

    $(".secret-div").css("display", "none");

}
/*******
***查询用户登录记录
*********/
function searchRecord()
{
    var postdata = {};
   
    postdata["pagesize"] = $(".ui-pg-selbox").val();
    postdata["nowpage"] = count1;
    $.ajax({
        type: "post",
        url: searchlurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            $("#table_list").html("");
            $("#pager_list").html("");
            var jsonRecord = data;
            if (jsonRecord[0].msg != "success")
            {
                alert(jsonRecord[0].status)

                return false;
            }
               
            if (jsonRecord[0].count == 0)
            {
                $("#table_list").html(
                    "<tr>" +
                    "<th>无数据</th>"
                    );
                return false;
            }
              
            count1max = jsonRecord[0].count / postdata["pagesize"];
            count1max = Math.ceil(count1max);

         
            $("#sp_1_pager_list_2").text("共" + count1max + "页");
            $("#pagenowshow").val(count1);
            $(".totalnum").text("共" + jsonRecord[0].count + "条");
            var jsonRecords = jsonRecord[0].record;

            $("#table_list").html(

                          "<tr>" +
                                "<th>会员编号</th>" +

                                "<th>用户名称</th>" +
                                 "<th>用户类型</th>" +
                                 "<th>IP</th>" +
                                "<th>登录所在地</th>" +
                                "<th>登录内容</th>" +

                                "<th>登录时间</th>" +
                               
                            "</tr>"
                    );
            var usertype = "";
            for (var i = 0; i < jsonRecords.length; i++) {
                if (jsonRecords[i]['ADMIN_ID'] == '1')
                    usertype = "超级管理员";
                else if (jsonRecords[i]['ADMIN_ID'] == '2')
                    usertype = "普通用户";
                else
                    usertype = "未知";
                $("#table_list").append(
                                "<tr>" +
                                    "<td>" + jsonRecords[i]['GUID'] + "</td>" +
                                    "<td>" + jsonRecords[i]['USER_NAME'] + "</td>" +
                                    "<td>" + usertype + "</td>" +

                                    "<td>" + jsonRecords[i]['IP'] + "</td>" +
                                    "<td>" + jsonRecords[i]['ADDRESS'] + "</td>" +
                                    "<td>" + jsonRecords[i]['SIGIN_CONTENT'] + "</td>" +
                                    "<td>" + jsonRecords[i]['SIGIN_TIME'] + "</td>" +
                                  
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
    searchRecord();
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
    searchRecord();
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
    searchRecord();
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
    searchRecord();
    $("#pagenowshow").val(count1);
};