domain_name = "http://egov.jinyuc.com";
/*******************个人会员注册图片所在路径*************************/
 carouselfigure_image = "/gxdzbackstage/image/Carouselfigure/";
 chat_head_image = "/gxdzwx/gxdzimages/gxdzwxlogin/personal/chat_head/";
 id_card_image = "/gxdzwx/gxdzimages/gxdzwxlogin/personal/id_card/";
/****************个人会员注册图片所在路径**************************/
/****************企业会员注册图片所在路径**************************/
 enter_chat_head = "/gxdzwx/gxdzimages/gxdzwxlogin/factory/chat_head/";
 enter_busi_license = "/gxdzwx/gxdzimages/gxdzwxlogin/factory/business_license/";
 enter_credential = "/gxdzwx/gxdzimages/gxdzwxlogin/factory/credential/";
 enter_honor = "/gxdzwx/gxdzimages/gxdzwxlogin/factory/honor/";
 enter_qual_gua = "/gxdzwx/gxdzimages/gxdzwxlogin/factory/quality_guaratee/";
 enter_related = "/gxdzwx/gxdzimages/gxdzwxlogin/factory/related_picture/";
/****************企业会员注册图片所在路径**************************/
 $(function () {
    

     //监听加载状态改变  
     document.onreadystatechange = completeLoading;

   
 })
 




 //加载状态为complete时移除loading效果  
 function completeLoading() {
     if (document.readyState == "complete") {
         //setTimeout("hideloading()", "6000");
       $(".spiner-example").hide();
     }
 }
 function hideloading()
 {
     $(".spiner-example").hide();
 }