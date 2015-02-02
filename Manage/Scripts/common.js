var tools = {
    valicate: function () {
        var valid = true;
        $(".required").each(function () {
            var fieldVal = '';
            var fieldName=$(this).attr("data-valicate-field");
            fieldVal = !!fieldName ? $(this).attr(fieldName):$(this).val();
            if($.trim(fieldVal).length == 0) {
                valid = false;
                alert($(this).attr("data-required-msg"));
                return false;
            }
        })
        return valid;
    },
    postData: function (url, data, callback, lockObj, datatype) {
        url = url.replace('{dns}', '/');
        data = data || {};
        datatype = datatype || 'json';
        var isLock = !!lockObj;
        $.ajax({
            url: url,
            data: data,
            method:'post',
            dataType: datatype,
            cache: false,
            beforeSend: function () {
                  isLock && $(lockObj).block(tools.blockuiops);
            },
            success: callback,
            error:function(){
                alert('请求发送失败！');
            },
            complete: function () {
                isLock && $(lockObj).unblock();
            }
        });
    },
    pagedefaultops:{
        callback: function (pageindex) { },
        items_per_page: 10,
        num_display_entries: 5,
        num_edge_entries: 1,
        current_page:0,
        link_to:'javascript:;',
        prev_text: '上一页',
        next_text: '下一页'
    },
    blockuiops:{
        message: '<i class="fa  fa-spinner fa-2x fa-spin"></i>',
        css: { backgroundColor: 'transparent', border: 0 },
        overlayCSS: { opacity: 0.4, backgroundColor: '#fff' }
    },
    ztreeops: {
        view: {
            showIcon:false
        },
        data: {
            simpleData: {
                enable: true,
                idKey: "id",
                pIdKey: "pid"
            }
        },
        check: {
            enable: true,
            chkStyle: "radio"
        }

    }
}