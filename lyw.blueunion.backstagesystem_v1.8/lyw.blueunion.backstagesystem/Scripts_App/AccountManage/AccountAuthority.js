var authoritysave = rootUrl + "AccountManage/AccountSaveAuthority?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var getauthority = rootUrl + "AccountManage/AccountHasAuthority?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址














$(document).ready(function () {
    $(".confirm-author-button").click(function () {

        getAuthority();
    });
  //**********icheck.js自带ifChecked和ifChanged事件***************/
    $("input.checkbox-click").on('ifChanged', function () {
        var id = $(this).attr('name-id');
        selectall(id);
    })
});



/****
****获取当前角色的权限***
**/
function getHasAuthority() {
    var user_id = $("#adminid").val();//角色id
    $.ajax({
        type: "post",
        url: getauthority,
        data: { 'user_id': user_id },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data.msg != "success") {
                $('.toastdiv').toast({
                    content: data.status,
                    duration: 1000
                });
                return false;
            }
            var jsonauthority = data.content;
            var authority = jsonauthority.split(',');
            for (var i = 0; i < authority.length; i++) {
                var chks = $("div.checked input[type='checkbox'][name='child']");
                var icheckboxdiv = $("label." + id + " div.icheckbox_square-green");
                if (icheckboxdiv.hasClass("checked")) {
                    icheckboxdiv.removeClass("checked");
                    //    $("input[name='"+id+"']").attr("checked", "false");
                }
                else {
                    icheckboxdiv.addClass("checked");
                }

            }
        }
    });
}

/******
*** 获取checkbox的id*****
**/
function GetCheckedValues() {
    //if (id == undefined)
    //    id = 'ChkSingleID';
    var ids = "";
    //var chks = $("input[type='checkbox'][name='" + id + "']:checked");

    var chks = $("div.checked input[type='checkbox'][name='child']");
    for (var index = 0; index < chks.length; index++) {
        if (index > 0)
            ids += ",";
        ids += $(chks[index]).val();
    }
    return ids;
}
/********
*****选择所有的checkbox
**********/
function selectall(id) {
    var icheckboxdiv = $("label."+id+" div.icheckbox_square-green");
    if (icheckboxdiv.hasClass("checked")) {
        icheckboxdiv.removeClass("checked");
    //    $("input[name='"+id+"']").attr("checked", "false");
    }
    else {
        icheckboxdiv.addClass("checked");
      //  $("input[name='"+id+"']").attr("checked", "true");
    }

}

/********
***获取选中权限
**********/
function getAuthority()
{
    var authorityid = GetCheckedValues();
    requestSaveAuthority(authorityid);
}
function requestSaveAuthority(authorityid)
{
    var roleid = $("#adminid").val();
    $.ajax({
        type: "post",
        url: authoritysave,
        data: { 'authority_id': authorityid, 'role_id': roleid },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
          
                $('.toastdiv').toast({
                    content: data.status,
                    duration: 1000
                });
        }
    });
}