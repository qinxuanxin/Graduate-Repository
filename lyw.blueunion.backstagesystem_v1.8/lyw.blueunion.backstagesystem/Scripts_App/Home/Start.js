var todayurl = rootUrl + "Home/TodayCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var logurl = rootUrl + "Home/RequestOperationLog?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var allcounturl = rootUrl + "Home/AllCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
$(function () {
    todaycount();
    requestlog();
    allcount();
    $(".load-more").click(requestLoadMore);
    $('#loading').hide();
   
});


/********
**会员总数量
****/
function allcount() {
   
    $.ajax({
        type: "post",
        url: allcounturl,
        data: '',
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success")
                return false;
            else
            {
                var membercount = data[0].membercount;
                var ordercount = data[0].ordercount;
                $(".allcount h1").text(membercount);
            }
        }
    });
 
   
}


/********
**今天的会员数量
****/
function todaycount() {
    //var postdata = {};
    //postdata["initsecret"] = $("#adminfirstsecret").val();
    //postdata["newsecret"] = $("#adminconfirmnewsecret").val();
    $.ajax({
        type: "post",
        url: todayurl,
        data: '',
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            if (data[0].msg != "success")
                return false;
            else
            {
                var membercount = data[0].membercount;
                var ordercount = data[0].ordercount;
                $(".membercounth1").text(membercount);
            }
        }
    });
 
   
}
/************
**最新日志
********/
function requestlog()
{
    $.ajax({
        type: "post",
        url: logurl,
        data: '',
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            $(".feed-activity-list").html("");
            if (data[0].msg != "success")
            {
                $(".feed-activity-list").html('<div class="feed-element">' + data[0].status + '</div>')
            }
            else
            {
                var json=data[0].record;
                var tempdiv="";
                for (var i = 0; i < json.length; i++)
                {
                    tempdiv += '<div class="feed-element">'
                           + '<div class="media-body ">'
                           //+ '<small class="pull-right">' + json[i].IP+ '</small>'
                           + '<strong>' + json[i].LOG + '</strong>'
                           + '<br>'
                           + '<small class="text-muted">' + json[i].ADD_TIME + '</small>'
                           + '<small class="pull-right">IP:' + json[i].IP + '</small>'
                           + '</div>'
                           + '</div>';
                }
                $(".feed-activity-list").html(tempdiv);
               
              
            }
           
       
                
        }
    });
}
/******
**加载更多**
*******/
function requestLoadMore()
{
    var indexlogurl = rootUrl + "Home/OperationLog?random=" + Math.floor(Math.random() * (100000 + 1));
   // $("#myId").attr("href", "www.xxx.com");
   // $(window.parent.document).find(".oper").attr("href", indexlogurl);
   // $(".oper").on("click", n)
  //  $(window.parent.document).find(".J_iframe").attr('src', indexlogurl);
    $(window.parent.document).find(".J_menuItem").trigger('click');
    
    //$(window).scrollTop(0);
    //if (navid != "") {
    //    var $objTr = $("#" + navid); //找到要定位的地方  tr 
    //    //    $objTr.css("background-color", "lightgray"); //设置要定位地方的css 
    //    var objTr = $objTr[0]; //转化为dom对象 
    //    $("#sidebar").animate({ scrollTop: objTr.offsetTop }, "slow"); //定位tr 
        
    //}
    //$('#' + navid).trigger('click');
}