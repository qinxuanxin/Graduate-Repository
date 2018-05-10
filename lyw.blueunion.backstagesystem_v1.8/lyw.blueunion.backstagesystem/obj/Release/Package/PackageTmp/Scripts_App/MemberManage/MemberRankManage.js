var pieurl = rootUrl + "MemberManage/MemberRankCountToPie?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var lineurl = rootUrl + "MemberManage/MemberRankCountToLine?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
var stasticurl = rootUrl + "MemberManage/StasticCount?random=" + Math.floor(Math.random() * (100000 + 1));//请求地址
$(document).ready(function () {

    requestMemberListDataToPie();
    requestMemberListDataToLine();
    stasticCount();
});
window.onload = function ()
{
   
}

/*************
**请求数据到饼状图
******/
function requestMemberListDataToPie() {
    $.ajax({
        type: "post",
        url: pieurl,
        data: {},
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            var jsondata = data;

            var l = echarts.init(document.getElementById("echarts-pie-chart")),
        u = {
            title: {
                text: "会员各分类注册数量",
                subtext: "已注册",
                x: "center"
            },
            tooltip: {
                trigger: "item",
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: "vertical",
                x: "left",
                data: ["个人会员", "企业会员", "个人/自由职业", "企业/会员单位"]
            },
            calculable: !0,
            series: [{
                name: "会员各分类注册数量",
                type: "pie",
                radius: "55%",
                center: ["50%", "60%"],
                data: jsondata
            }],
            color: ['rgb(254,67,101)', 'rgb(252,157,154)', 'rgb(249,205,173)', 'rgb(200,200,169)', 'rgb(131,175,155)']
        };
                    l.setOption(u), $(window).resize(l.resize);
        }

    });

}
/********
***请求数据到折线/柱状图
********/
function requestMemberListDataToLine() {

    var datearray = [];   
    var perarray = [];        
    var enterarray = [];       
    var professionarray = [];        
    var unitarray = [];       
 
    $.ajax({
        type: "post",
        url: lineurl,
        data: {},
        dataType: 'json',
        async: true,//异步
        success: function (result) {
          
          
          
                for (var i = 0; i < result.length; i++) {
                    datearray.push(result[i].date);       
                    perarray.push(result[i].person);
                    enterarray.push(result[i].enterprise);
                    professionarray.push(result[i].profession);
                    unitarray.push(result[i].unit);

                }
            
            // 基于准备好的dom，初始化echarts图表
            var myChart = echarts.init(document.getElementById("main"));
            var option = {
                title: {
                    text: '各分类新增',
                    subtext: ''
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: ['个人会员', '企业会员', '个人/自由职业', '企业/会员单位']
                },
                //右上角工具条
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                xAxis: [
                    {
                        type: 'category',
                        boundaryGap: false,
                        //['周一', '周二', '周三', '周四', '周五', '周六', '周日']
                        data: datearray
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        axisLabel: {
                            formatter: '{value} 个'
                        }
                    }
                ],
                series: [
                    {
                        name: '个人会员',
                        type: 'line',
                        data: perarray,
                        //    [1, 6, 10, 15, 22, 33, 20],
                        markPoint: {
                            data: [
                                { type: 'max', name: '最大值' },
                                { type: 'min', name: '最小值' }
                            ]
                        },
                        markLine: {
                            data: [
                                { type: 'average', name: '平均值' }
                            ]
                        }
                    },
                    {
                        name: '企业会员',
                        type: 'line',
                        data: enterarray,
                          //  [0, 2, 4, 8, 4, 5, 17],
                        markPoint: {
                            data: [
        //                        {name : '周最低', value : -2, xAxis: 1, yAxis: -1.5}
                                { type: 'min', name: '周最低' }
                            ]
                        },
                        markLine: {
                            data: [
                                { type: 'average', name: '平均值' }
                            ]
                        }
                    },
                     {
                         name: '个人/自由职业',
                         type: 'line',
                         data: professionarray,
                            // [3, 2, 6, 8, 2, 12, 22],
                         markPoint: {
                             data: [
         //                        {name : '周最低', value : -2, xAxis: 1, yAxis: -1.5}
                                 { type: 'min', name: '周最低' }
                             ]
                         },
                         markLine: {
                             data: [
                                 { type: 'average', name: '平均值' }
                             ]
                         }
                     },
                      {
                          name: '企业/会员单位',
                          type: 'line',
                          data: unitarray,
                             // [1, 12, 20, 4, 12, 2, 26],
                          markPoint: {
                              data: [
          //                        {name : '周最低', value : -2, xAxis: 1, yAxis: -1.5}
                                  { type: 'min', name: '周最低' }
                              ]
                          },
                          markLine: {
                              data: [
                                  { type: 'average', name: '平均值' }
                              ]
                          }
                      }
                ]
            };

            // 为echarts对象加载数据
            myChart.setOption(option);
        }
    });
}
function test()
{
    $.ajax({
        type: "post",
        url: lineurl,
        data: {},
        dataType: 'json',
        async: true,//异步
        success: function (data) {
            alert("success");
        }
    });
}
/***
***统计会员分类数据
*********/
function stasticCount()
{
    $.ajax({
        type: "post",
        url: stasticurl,
        data: {},
        dataType: 'json',
        async: true,//异步
        success: function (result) {
            $(".person-count").text(result.person);
            $(".enterprise-count").text(result.enterprise);
            $(".professional-count").text(result.profession);
            $(".unit-count").text(result.unit);
             
        }
    });
}