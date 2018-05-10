var templatelisturl = rootUrl + "NewsManage/SmsTemplateListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var newtemplateurl = rootUrl + "NewsManage/NewTemplate?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var alltemplateurl = rootUrl + "NewsManage/AllSmsTemplate?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var deltemplateurl = rootUrl + "NewsManage/DelTemplate?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var edittemplateurl = rootUrl + "NewsManage/EditTemplate?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
    
$(document).ready(function () {

    requestTemplateListData();
    alltemplatefunc();
    $("#table_list").on('click', '.event-btn', function () {
        var status = $(this).attr("data-status");
        var eventid = $(this).data("eventid");
        requestSetStatus(status, eventid);
    })
    $("#loading-example-btn").click(function () {
        requestTemplateListData();
    });
    $(".search-template").click(function () {
        requestTemplateListData();
    })

    /****显示新建模板视图****/
    $(".new-template-btn").click(function () {
        $(".template-info").css("display", "none ");
        $(".new-template").css("display", "block");
        $(".pre-message").text("");
    });
    /*****返回上一页*****/
    $(".pre-button").click(function () {
        $(".template-info").css("display", "block ");
        $(".new-template").css("display", "none");
        $(".edit-template").css("display", "none");
        
    });
    /*****确认新建模板***/
    $(".confirm-template-button").click(function () {
        confirmnewtemplate();
    });
    /*******删除模板*********/
    $("#table_list").on('click', '.del-template', function () {
        var template_id = $(this).attr("data-templateid");
        delTemplate(template_id);
    });
    /*********跳转编辑模板视图**********/
    $("#table_list").on('click', '.template-btn', function () {
        var template_id = $(this).attr("data-templateid");
        var template_theme = $(this).attr("data-template_theme");
        var template_content = $(this).attr("data-template_content");
        $("#edit_template_id").val(template_id);
        $("#edit_template_theme").val(template_theme);
        $("#edit_template_content").val(template_content);
        $(".template-info").css("display", "none ");
        $(".edit-template").css("display", "block");
    });
    $(".refresh-template-button").click(function () {
        saveedittemplate();
    })
    // requestMemberListDataToLine();
    //  stasticCount();
});


/*************
**请求短信模板
******/
function requestTemplateListData() {
    var keyword=$("#keyword").val();
    $.ajax({
        type: "post",
        url: templatelisturl,
        data: { 'keyword': keyword },
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            $("#table_list").html("");
            var jsonRedata = data;
            if (jsonRedata[0].msg != "success") {
                alert(jsonRedata[0].status);

                return false;
            }
            var jsonRecords = jsonRedata[0].servers;
            var trtemplate = "";
            var status = "";
            for (var i = 0; i < jsonRecords.length; i++) {
                trtemplate += "<tr>";


                //if (jsonRecords[i]['SMS_EVENT_STATUS'] == '0') {
                //    trtemplate += '<td class="project-status">'
                //              + '<span class="label label-default">'
                //              + '已关闭'
                //              + '</span>'
                //              + '</td>';

                //}

                //else if (jsonRecords[i]['SMS_EVENT_STATUS'] == '1') {
                //    trtemplate += '<td class="project-status">'
                //              + '<span class="label label-primary">'
                //              + '开启中'
                //              + '</span>'
                //              + '</td>';
                //}

                //else
                //    trtemplate += "";



                trtemplate += '<td class="project-title">'
                           + '<a href="project_detail.html">模板编号</a>'
                           + '<br>'
                           + '<small>' + jsonRecords[i]["SMS_TEMPLATE_ID"] + '</small>'
                           + '</td>'
                           + '<td class="project-completion">'
                           + '<a href="project_detail.html">模板主题</a>'
                           + '<br>'
                           + '<small>' + jsonRecords[i]["SMS_THEME"] + '</small>'
                           + '</td>'
                           + '<td class="project-people">'
                           + '<a>模板内容</a>'
                           + '<br>'
                           + '<small>' + jsonRecords[i]["SMS_TEMPLATE_CONTENT"] + '</small>'
                           + '</td>';
                trtemplate += '<td class="project-actions">';
            

                trtemplate += '<a style="margin-right: 5px;" class="btn  btn-white btn-sm template-btn" data-template_content=' + jsonRecords[i]["SMS_TEMPLATE_CONTENT"] + ' data-template_theme=' + jsonRecords[i]["SMS_THEME"] + '  data-templateid=' + jsonRecords[i]['SMS_TEMPLATE_ID'] + '><i class="fa fa-pencil"></i> 编辑模板 </a>'

             
          

                //    <a href="projects.html#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i> 查看 </a>
                //    <a href="projects.html#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i> 编辑 </a>
                //</td>

                trtemplate += '<a class="btn  btn-info btn-sm del-template" data-templateid=' + jsonRecords[i]['SMS_TEMPLATE_ID'] + '><i class="fa "></i> 删除 </a>'



                trtemplate += '</td>'
                           + '</tr>';



            }
            $("#table_list").append(trtemplate);
        }

    });

}

/******
***所有模板
***********/
function alltemplatefunc() {

    $.ajax({
        type: "post",
        url: alltemplateurl,
        data: {},
        dataType: 'json',
        async: true,//异步
        success: function (result) {
            var jsonRedata = result;
            if (jsonRedata[0].msg != "success") {
                alert(jsonRedata[0].status);

                return false;
            }
            var jsonRecords = jsonRedata[0].servers;
            var templatediv = "";
            for (var i = 0; i < jsonRecords.length; i++)
                templatediv += '<option value="' + jsonRecords[i]["SMS_TEMPLATE_ID"] + '">' + jsonRecords[i]["SMS_THEME"] + '</option>';

            $("#template_id").append(templatediv);

        }
    });
}
/********
***确认新建事件
*****/

function confirmnewtemplate() {
    var postdata = {};
    postdata["templateid"] = $("#template_id").val();
    postdata["templatetheme"] = $("#template_theme").val();
    postdata["templatecontent"] = $("#template-content").val();
    if (postdata["templatetheme"] == "" || postdata["templatecontent"] == "" || postdata["templateid"] == "") {
        $(".pre-message").text("请将信息填写完整");
        return false;
    }
    $.ajax({
        type: "post",
        url: newtemplateurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (result) {
            var jsonRedata = result;
            //if (jsonRedata[0].msg != "success") {
            //    alert(jsonRedata[0].status);

            //    return false;
            //}
            $(".pre-message").text(jsonRedata[0].status);
          


        }
    });
}
/*******
**删除模板
****/
function delTemplate(template_id)
{
    
    $.ajax({
        type: "post",
        url: deltemplateurl,
        data: {'templateid':template_id},
        dataType: 'json',
        async: true,//异步
        success: function (result) {
            var jsonRedata = result;
            if (jsonRedata[0].msg != "success") {
              
                $(".permessage-info").text(jsonRedata[0].status);
                return false;
            }
            requestTemplateListData();
        }
    });
}

/*******
**编辑模板
*****/
function saveedittemplate()
{
    var postdata = {};
    postdata["templateid"] = $("#edit_template_id").val();
    postdata["templatetheme"] = $("#edit_template_theme").val();
    postdata["templatecontent"] = $("#edit_template_content").val();
    if (postdata["templatetheme"] == "" || postdata["templatecontent"] == "" || postdata["templateid"] == "") {
        $(".edit-pre-message").text("请将信息填写完整");
        return false;
    }
    $.ajax({
        type: "post",
        url: edittemplateurl,
        data: postdata,
        dataType: 'json',
        async: true,//异步
        success: function (result) {
            var jsonRedata = result;
            //if (jsonRedata[0].msg != "success") {
            //    alert(jsonRedata[0].status);

            //    return false;
            //}
            $(".edit-pre-message").text(jsonRedata[0].status);
        }
    });
}