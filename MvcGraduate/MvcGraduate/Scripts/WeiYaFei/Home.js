$(function () {
    $("#zhuye_centre_centre_1 .title li:eq(0)").mouseover(function () {
        $('#JS_ShareArtDiv').hide();
        $('#JS_myArtDiv').show();
    });
    $("#zhuye_centre_centre_1 .title li:eq(2)").mouseover(function () {
        $('#JS_myArtDiv').hide();
        $('#JS_ShareArtDiv').show();
    });
    $("#zhuye_centre_centre_2 .title li:eq(0)").mouseover(function () {
        $('#JS_ShareImageDiv').hide();
        $('#JS_MyImageDiv').show();
    });
    $("#zhuye_centre_centre_2 .title li:eq(2)").mouseover(function () {
        $('#JS_MyImageDiv').hide();
        $('#JS_ShareImageDiv').show();
    });
});
//,#zhuye_centre_centre_2 .title li:eq(0),#zhuye_centre_centre_1 .title li:eq(2)