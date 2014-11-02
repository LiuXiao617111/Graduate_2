var nowPage = 0;
var maxPage = 0;
function PageJump__First(uri) {
    if (nowPage != 0) {
        nowPage = 0;
        PostUri(uri);
    }
}

function PageJump_Forward(uri) {
    if (nowPage > 0) {
        nowPage = nowPage - 1;
        PostUri(uri, nowPage)
    }
}

function PageJump_Back(uri) {
    if (nowPage < maxPage) {
        nowPage = nowPage + 1;
        PostUri(uri)
    }
}

function PageJump_End(uri) {
    if (nowPage != maxPage) {
        nowPage = maxPage;
        PostUri(uri)
    }
}

function PostUri(uri) {
    $.post(
        uri,
        { index: nowPage },
        function (data) {
            $('.navigation_centre_right').html(data);
        });
}
function ChangePageCss()
{
    if (nowPage == 0) {
        $('.page div div:eq(2)').removeClass("btn_first");
        $('.page div div:eq(2)').addClass("btn_first0");
        $('.page div div:eq(3)').removeClass("btn_forward");
        $('.page div div:eq(3)').addClass("btn_forward0");

        $('.page div div:eq(4)').removeClass("btn_back0");
        $('.page div div:eq(5)').removeClass("btn_end0");
        $('.page div div:eq(4)').addClass("btn_back");
        $('.page div div:eq(5)').addClass("btn_end");
    } else if (nowPage < maxPage) {
        $('.page div div:eq(2)').removeClass("btn_first0");
        $('.page div div:eq(3)').removeClass("btn_forward0");
        $('.page div div:eq(4)').removeClass("btn_back0");
        $('.page div div:eq(5)').removeClass("btn_end0");

        $('.page div div:eq(2)').addClass("btn_first");
        $('.page div div:eq(3)').addClass("btn_forward");
        $('.page div div:eq(4)').addClass("btn_back");
        $('.page div div:eq(5)').addClass("btn_end");
    }
    if (nowPage == maxPage) {
        if (nowPage != 0) {
            $('.page div div:eq(2)').removeClass("btn_first0");
            $('.page div div:eq(3)').removeClass("btn_forward0");
            $('.page div div:eq(3)').addClass("btn_forward");
            $('.page div div:eq(2)').addClass("btn_first");
        }

        $('.page div div:eq(4)').removeClass("btn_back");
        $('.page div div:eq(5)').removeClass("btn_end");
        $('.page div div:eq(4)').addClass("btn_back0");
        $('.page div div:eq(5)').addClass("btn_end0");
    }
}