var url = rootUrl+"Login/LoginCheckUser?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
$(function () {
    //$('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
    //$(window).resize(function () {
    //    $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
    //});
    /*********
    ***回车键事件
    ********/
    $(document).keypress(function (e) {
        if (e.which == 13)
        {
            $(".loginbtn").click();
        }

    })
});


function signIn() {
    var ajaxPram = {};
    ajaxPram['user_name'] = $("#name").val();
    ajaxPram['user_password'] = $("#password").val();
    //        ajaxPram['USER_NAME'] = "gly";
    //        ajaxPram['USER_MM'] = "123";
    if (ajaxPram['user_name'] == "" || ajaxPram['user_password'] == "") {
        alert("请填写用户名、密码！"); return;
    }
    $.ajax({
        type: "post",
        url: url,
        data: ajaxPram,
        dataType: 'json',
        async: true,//异步
        success: function (data) {


            if (data[0].msg == "success") {
                window.location.href = rootUrl + "home/homeindex";
            }
            if (data[0].msg == "fail") {
                var ss = data[0].status;
                $(".response-content").text(ss);;
            }

        }
    });
}