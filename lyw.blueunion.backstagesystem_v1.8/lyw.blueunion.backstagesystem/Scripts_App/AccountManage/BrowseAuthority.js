var getauthority = rootUrl + "AccountManage/AccountHasAuthority?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var authorityexplain=rootUrl + "AccountManage/AccountAuthorityExplain?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址



$(document).ready(function () {
    getauthorityexplain();
    //   alert("似的发射点");
    getHasAuthority();
    $(".browse-author").click(function () {
        getauthorityexplain();
        //   alert("似的发射点");
        getHasAuthority();
    })
});


/*********
***获取所有权限描述
**********/
function getauthorityexplain()
{
    $.ajax({
        type: "post",
        url: authorityexplain,
        data: { },
        dataType: 'json',
        async: false,//同步
        success: function (data) {
            $("#table_list").html("");
            if (data.msg != "success") {
                $('.toastdiv').toast({
                    content: data.status,
                    duration: 1000
                });
                return false;
            }
            var jsonRecords = data.content;
            var trtemplate = "";
            var status = "";
            for (var i = 0; i < jsonRecords.length; i++) {
                trtemplate += "<tr>";


                //if (jsonRecords[i]['SMS_EVENT_STATUS'] == '0') {
                    trtemplate += '<td class="project-status">'
                              + '<span class="label label-default span-id" id="' + jsonRecords[i].AUTHORITY_ID + '">'
                              + '未拥有'
                              + '</span>'
                              + '</td>';

                //}

                //else if (jsonRecords[i]['SMS_EVENT_STATUS'] == '1') {
                //    trtemplate += '<td class="project-status">'
                //              + '<span class="label label-primary">'
                //              + '已拥有'
                //              + '</span>'
                //              + '</td>';
                //}

                //else
                //    trtemplate += "";



                trtemplate += '<td class="project-title">'
                           + '<a>控制器</a>'
                            + '<br>'
                            + '<small>' + jsonRecords[i]["CONTROLLNAME"] + '</small>'
                           + '</td>'
                           + '<td class="project-completion">'
                           + '<a>行为</a>'
                           + '<br>'
                           + '<small>' + jsonRecords[i]["ACTION"] + '</small>'
                           + '</td>'
                           + '<td class="project-people">'
                           + '<small>' + jsonRecords[i]["EXPLAIN"] + '</small>'
                           + '</td>';
                trtemplate += '<td class="project-actions">';
                //if (jsonRecords[i]['SMS_EVENT_STATUS'] == '0') {

                //    trtemplate += '<a style="margin-right: 5px;" class="btn btn-primary btn-sm event-btn" data-status="1" data-eventid=' + jsonRecords[i]['SMS_EVENT_ID'] + '><i class="fa fa-cancle"></i> 点击开启 </a>'

                //}
                //else if (jsonRecords[i]['SMS_EVENT_STATUS'] == '1') {

                //    trtemplate += '<a style="margin-right: 5px;" class="btn  btn-white btn-sm event-btn" data-status="0" data-eventid=' + jsonRecords[i]['SMS_EVENT_ID'] + '><i class="fa fa-cancle"></i> 点击关闭 </a>'

                //}
                //else
                //    trtemplate += '';

                //    <a href="projects.html#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i> 查看 </a>
                //    <a href="projects.html#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i> 编辑 </a>
                //</td>

                //trtemplate += '<a class="btn  btn-info btn-sm del-event " data-eventid=' + jsonRecords[i]['SMS_EVENT_ID'] + '><i class="fa "></i> 删除</a>'



                trtemplate += '</td>'
                           + '</tr>';



            }
            $("#table_list").append(trtemplate);
        }
    });
}
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
            var jsonauthority = data.content.AUTHORITY;
            var authority = jsonauthority.split(',');
            for (var i = 0; i < authority.length; i++) {

                var authorityobject = $('#' + authority[i]);
                if (authority != null && authority != "")
                {
                    authorityobject.text("已拥有");
                    authorityobject.removeClass("label-default").addClass("label-primary");
                }
              
            }
        }
    });
}



