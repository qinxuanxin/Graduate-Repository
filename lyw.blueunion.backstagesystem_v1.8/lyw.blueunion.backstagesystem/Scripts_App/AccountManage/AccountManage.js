var registreurl = rootUrl + "AccountManage/AccountSaveRegister?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var searchurl= rootUrl + "AccountManage/SearchAccountInfo?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var modifyurl = rootUrl + "AccountManage/AccountSaveModify?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var delurl=rootUrl + "AccountManage/AccountDelete?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址

window.onload = function ()
{
    $(".confirm-reg-button").on("click", request_register);
    $(".search-accountinfo").on("click", searchaccountinfo);
    $(".modify-account-button").on("click", confirmModify);
    $(".preto-edit").on("click", perto);
   
   // searchaccountinfo();
    
}


/*********
**账号注册请求
**********/
function request_register()
{
    var postdata = {};
    postdata["Admin_id"] = $("#adminid").val();
    postdata["Admin_name"] = $("#adminname").val();

    postdata["Admin_password"] = $("#adminpassword").val();

    for (var key in postdata) {
        if (postdata[key] == "") {
            $(".hint-message").text("请将信息填写完整！");
       
            return false;
        }
    }
    if (postdata["Admin_password"] != $("#confirmadminpassword").val()) {
        $(".hint-message").text("密码填写不一致！");
   
        return false;
    }
    //var options = {
    //    type: "POST",
    //    url: registreurl,
    //    data: postdata,
    //    dataType: "text",
    //    success: function (data) {

    //        if (data == "success")
    //            alert("注册账号成功!");
    //        else
    //            alert("注册账号失败!");
    //    }
    //};
    
    //$('#myForm').ajaxSubmit(options);



    $.ajax({
        type: "post",
        url: registreurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (data) {
           
                $(".hint-message").text(data.status);
                $('.toastdiv').toast({
                    content: data.status,
                    duration: 1000
                });
               
             
        }
    });
}

/***********
**搜索带条件的账号查询
***********/

function searchaccountinfo()
{
    var parml = {};
    parml['admin_name'] = $("#SearchAdminName").val();
    parml['admin_id'] = $("#SearchAdminId").val();

    $.ajax({
        type: "post",
        url: searchurl,
        data: parml,
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            $("#btnShowDeletetable").html("");
            
            if (data.msg != "success")
            {
                $('.toastdiv').toast({
                    content: data.status,
                    duration: 1000
                });
              //  alert();
                $("#btnShowDeletetable").html(
                 '<tr>' +
                     '<th style="">' + data.status + '</th>' +
                 '</tr>'

             );

                return false;
            }
           
          
            var accountinfo = data.content;
            $("#btnShowDeletetable").html(
                   '<tr>' +
                       '<th style="">用户类型</th>' +
                       '<th>用户名</th>' +
                       '<th>密码</th>' +
                     
                         '<th style="display:none"></th>' +
                           '<th></th>' +
                             '<th></th>' +

                   '</tr>'

               );
            for (var i = 0; i < accountinfo.length; i++) {
                var userJS = "";
                if (accountinfo[i]['ADMIN_ID'] == '1')
                    userJS = "超级管理员";
                else if (accountinfo[i]['ADMIN_ID'] == '2')
                    userJS = "管理员";
                else if (accountinfo[i]['ADMIN_ID'] == '3')
                    userJS = "普通用户";
                else
                    userJS = "未知";
                $("#btnShowDeletetable").append(
                           '<tr>' +
                            '<td>' + userJS + '</td>' +
                           
                              '<td>' + accountinfo[i]['ADMIN_NAME'] + '</td>' +
                               '<td>' + accountinfo[i]['ADMIN_PASSWORD'] + '</td>' +
                                  
                        
                            '<td style="display: none">' + accountinfo[i]['GUID'] + '</td>' +
                             '<td><button class="btn  btn-success btn-sm" onclick="editaccount(\'' + accountinfo[i]['GUID'] + '\')">修改</button></td>' +
                            '<td><button class="btn btn-danger btn-sm" onclick="deleteaccount(\'' + accountinfo[i]['GUID'] + '\')">删除</button></td>' +
                          '</tr>'
                    );
            }
        }
    });
   
  
}

///跳转到修改账号信息页面
function editaccount(guid)
{

    window.location.href = rootUrl + "AccountManage/AccountModify?guid=" + guid +"&random="+Math.floor(Math.random() * (100000 + 1));
}
//删除账号信息
function deleteaccount(guid) {

    var r = confirm("是否确认删除!");
    if (r == true) {
        $.ajax({
            type: "post",
            url: delurl,
            data: { guid: guid },
            dataType: 'json',
            async: true,//异步
            success: function (data) {
                $('.toastdiv').toast({
                    content: data.status,
                    duration: 1000
                });
                //if (data == "success")
                //    alert("账号删除成功!");
                //else
                //    alert("账号删除失败!");
                //  location.reload();
                searchaccountinfo();
            }
        });
    }
    else
        alert("取消删除操作!");
 
 
}
/*****
****确认修改账号请求
*************/
function confirmModify() {

    if ($("#modifyadpassword").val() != $("#modifyconfirmadpassword").val()) {
        alert("填写新密码不一致!");

        return false;//单独使用return会导致页面刷新
    }
    else {
        var postdata = {};
        postdata["Admin_id"] = $("#modifyadminid").val();
        postdata["Admin_name"] = $("#modifyadminname").val();
        postdata["Guid"] = modifyguid;
        postdata["Admin_password"] = $("#modifyadpassword").val();
        for (var key in postdata) {
            if (postdata[key] == "") {
                alert("存在信息未填写！");
                return false;
            }
        }
        $.ajax({
            type: "post",
            url: modifyurl,
            data: postdata,
            dataType: 'json',
            async: true,//异步
            success: function (data) {
                $('.toastdiv').toast({
                    content: data.status,
                    duration: 1000
                });
                //if (data == "success")
                //    alert("账号修改成功!");
                //else
                //    alert("账号修改失败!");
                //   window.history.go(-1);
               
            }
        });



    }
}
function perto() {
    window.location.href = rootUrl + "AccountManage/AccountEdit";
}
