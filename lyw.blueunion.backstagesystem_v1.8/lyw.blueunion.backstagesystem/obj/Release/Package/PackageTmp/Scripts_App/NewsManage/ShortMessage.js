var eventlisturl = rootUrl + "NewsManage/SmsEventListData?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var seteventstatusurl = rootUrl + "NewsManage/SetEventStatus?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var alltemplateurl = rootUrl + "NewsManage/AllSmsTemplate?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var neweventurl = rootUrl + "NewsManage/NewEvent?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var deleventurl = rootUrl + "NewsManage/DelEvent?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址

$(document).ready(function () {

    requestEventListData();
    alltemplatefunc();
    /********设置事件状态***********/
    $("#table_list").on('click', '.event-btn', function () {
        var status = $(this).attr("data-status");
        var eventid = $(this).data("eventid");
        requestSetStatus(status, eventid);
    });

    /*******删除事件*********/
    $("#table_list").on('click', '.del-event', function () {
        var event_id = $(this).attr("data-eventid");
        delEvent(event_id);
    });

    /***刷新事件*******/
    $("#loading-example-btn").click(function () {
        requestEventListData();
    })
    /****显示新建事件视图****/
    $(".new-event-btn").click(function () {
        $(".event-info").css("display", "none ");
        $(".new-event").css("display", "block");
        $(".pre-message").text("");
    });
    /*****返回上一页*****/
    $(".pre-button").click(function () {
        $(".event-info").css("display", "block ");
        $(".new-event").css("display", "none");
    });
    /*****确认新建事件***/
    $(".confirm-reg-button").click(function () {
        confirmnewevent();
    });

   // requestMemberListDataToLine();
  //  stasticCount();
});


/*************
**请求事件列表事件
******/
function requestEventListData() {
    $.ajax({
        type: "post",
        url: eventlisturl,
        data: {},
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
            var trtemplate="";
            var status="";
            for (var i = 0; i < jsonRecords.length; i++) {
                trtemplate+="<tr>";
               
              
                if (jsonRecords[i]['SMS_EVENT_STATUS'] == '0')
                {
                    trtemplate+='<td class="project-status">'
                              +'<span class="label label-default">'
                              +'已关闭'
                              +'</span>'
                              +'</td>';                           
                                      
                }
   
                else if (jsonRecords[i]['SMS_EVENT_STATUS'] == '1')
                {
                    trtemplate+='<td class="project-status">'
                              +'<span class="label label-primary">'
                              +'开启中'
                              +'</span>'
                              +'</td>';   
                }
                   
                else
                    trtemplate+="";
               
              

                trtemplate +='<td class="project-title">'
                           +'<a href="project_detail.html">'+jsonRecords[i]["SMS_THEME"]+'</a>'
                           +'</td>'
                           +'<td class="project-completion">'
                           +'<a href="project_detail.html">模板编号</a>'
                           +'<br>'
                           +'<small>'+jsonRecords[i]["SMS_TEMPLATE_ID"]+'</small>'
                           +'</td>'
                           +'<td class="project-people">'
                           +'<small>'+jsonRecords[i]["SMS_EVENT_NAME"]+'</small>'
                           +'</td>';
                trtemplate+='<td class="project-actions">';
                if (jsonRecords[i]['SMS_EVENT_STATUS'] == '0')
                {
                   
                    trtemplate += '<a style="margin-right: 5px;" class="btn btn-primary btn-sm event-btn" data-status="1" data-eventid=' + jsonRecords[i]['SMS_EVENT_ID'] + '><i class="fa fa-cancle"></i> 点击开启 </a>'
                                                                      
                }
                else if (jsonRecords[i]['SMS_EVENT_STATUS'] == '1')
                {
                  
                    trtemplate += '<a style="margin-right: 5px;" class="btn  btn-white btn-sm event-btn" data-status="0" data-eventid=' + jsonRecords[i]['SMS_EVENT_ID'] + '><i class="fa fa-cancle"></i> 点击关闭 </a>'
                            
                }     
                else
                    trtemplate += '';
         
                                        //    <a href="projects.html#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i> 查看 </a>
                                        //    <a href="projects.html#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i> 编辑 </a>
                                        //</td>

                trtemplate+=  '<a class="btn  btn-info btn-sm del-event " data-eventid=' + jsonRecords[i]['SMS_EVENT_ID'] + '><i class="fa "></i> 删除</a>'
                        
     

                trtemplate +='</td>'
                           +'</tr>';
                                  
               

            }
            $("#table_list").append(trtemplate);
        }

    });

}
/********
***设置事件开启或者关闭
********/
function requestSetStatus(status,eventid) {

   

    $.ajax({
        type: "post",
        url: seteventstatusurl,
        data: {'status':status,'eventid':eventid},
        dataType: 'json',
        async: true,//异步
        success: function (result) {

            var jsonRedata = result;
            if (jsonRedata[0].msg != "success") {
                alert(jsonRedata[0].status);

                return false;
            }

            requestEventListData();

        }
    });
}
/******
***所有模板
***********/
function alltemplatefunc()
{
    
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

function confirmnewevent() {
    var postdata = {};
    postdata["eventname"] = $("#event_name").val();
    postdata["eventtemplateid"] = $("#template_id").val();
    if (postdata["eventname"] == "" || postdata["eventtemplateid"] == "00")
    {
        $(".pre-message").text("请将信息填写完整");
        return false;
    }
    $.ajax({
        type: "post",
        url: neweventurl,
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
         //   alert();

           
        }
    });
}
/******删除事件*********/
function delEvent(eventid) {
    $.ajax({
        type: "post",
        url: deleventurl,
        data: { 'eventid': eventid },
        dataType: 'json',
        async: true,//异步
        success: function (result) {
            var jsonRedata = result;
            if (jsonRedata[0].msg != "success") {

                $(".permessage-info").text(jsonRedata[0].status);
                return false;
            }
            requestEventListData();
        }
    });
}