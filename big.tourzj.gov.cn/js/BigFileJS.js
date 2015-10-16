function BigUpLoad(options) {
    var domform = $("#" + options.id);
    if (domform[0].files[0] == null) {
        var msg = "请选择文件";
        options.failed && options.failed(function (msg) {
            //用户调用msg 显示执行失败的提示
            return;
        });
    }

    var formdata = new FormData();
    formdata.append("file", domform[0].files[0]);
    formdata.append("sysid", options.sysid);

    $.ajax({
        url: "http://big.tourzj.gov.cn/BFInfo/UpLoad",
        type: "Post",
        data: formdata,
        processData: false,
        contentType: false,
        success: function (data) {
            domform.after(domform.clone().val(""));
            domform.remove();
            options.success && options.success(function (data) {
                //执行成功，返回bfid
            });
        },
        error: function (e) {
            options.failed && options.failed(function (e) {
                //用户调用msg 显示执行失败的提示
            });
        }
    });
}

