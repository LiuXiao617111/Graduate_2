$().ready(function () {
    //首页--课程表特效
    $('.Scheduletable td').bind("mouseover", function () {
        $(this).addClass("onmouseoverScheduletableTd");
    });
    $('.Scheduletable td').bind("mouseleave", function () {
        $(this).removeClass("onmouseoverScheduletableTd");
    });
    //添加导航点击的背景图片样式  navigation_menu_bg
    $('.navigation_centre_left div').bind('click', function () {
        $('.navigation_centre_left div').removeClass('navigation_menu_bg');
        $(this).addClass('navigation_menu_bg');
    });

    //我的班级  图片上显示信息
    $('.des').live('mouseover', function () {
        $(this).children('.des2').animate({
            opacity: 1,
            top: '0px'
        },130);
        $(this).children('.course').animate({
            top: '130px'
        }, 130);
    })
    $('.des').live('mouseleave', function () {
        $(this).children('.des2').animate({
            opacity: 1,
            top: '-130px'
        }, 100);
        $(this).children('.course').animate({
            top: '103px'
        },100);
    })
});