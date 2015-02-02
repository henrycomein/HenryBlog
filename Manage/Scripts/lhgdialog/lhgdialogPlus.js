/// <reference path="../common.js" />
var api = (function () {
    if (frameElement) {
        if (frameElement.api)
            return frameElement.api;
    }
    return null;
})(),
W = (function () {
    if (api) return api.opener;
    return null;
})(),
$$ = (function () {
    if (W) return W.$; else return $;
})(),
winWidth = '500px', winHeight = '400px';
$(function () {
    initDialog();
});
function initDialog() {
    $(document).on("click", '.closeDialog', function () {
        $confirm($(this), function () { api.close(); },'确定关闭吗？');
        return false;
    })
    .on("click", '[data-rel="delete"]', function () {
        var obj=$(this);
        $confirm(obj, function () {
            tools.postData(['{dns}api/', obj.attr("data-category"), '/delete'].join(''),
                { id: obj.attr("data-id") },
                function (data) {
                    if (data.success) {
                        winAlert(data.message, false, true, 'this', 3);
                    }
                    else {
                        winAlert(data.message, false, true, false, 3);
                    }
                });
            
        },'确定删除吗？');
        return false;
    })
    .on("click", '[data-rel="popup"]', function () {
        var winId = "winFrame" +  $$.dialog.setting.zIndex;
        var title = $(this).attr("data-title") || false;
        var content = $(this).attr("data-content") || "";
        //var keyid = $(this).attr("data-id") || '';
        content = content.replace("{dns}", '/');
        if (content.indexOf("url:") > -1) {
            if (content.indexOf("?") > -1) {
                content += "&wid=" + winId;
            } else {
                content += "?wid=" + winId;
            }
            //content += "&id=" + keyid;
        }
        var width = $(this).attr("data-width") || winWidth;
        var height = $(this).attr("data-height") || winHeight;
        var resize = true, max = true;//, drag = true;
        //if (navigator.userAgent.indexOf("MSIE 10.0") > 0) resize = false, max = false, drag = false;
        if ($(this).attr("data-max") != undefined) {
            max = $(this).attr("data-max") === "1";
            resize = max;
        }
        var lock = api == null || false;
        var pid = (function () {
            if (api) return api.config.id;
            return undefined;
        })();
        var option = {
            id: winId,
            title: title,
            width: width,
            height: height,
            resize: resize,
            max: max,
            drag: true,
            content: content,
            lock: lock,
            parent: api,
            min: false,
            cache: false,
            data: { pid: pid },
            padding: 0,
            close: function() {
             
            },
            modal:true
        };
         $$.dialog(option);
        //$$.dialog(option).max();
        return false;
    });
    $('.readonly').attr('readonly', 'readonly');
    $('.Wdate').attr('autocomplete', 'off');
    $('[data-rel="confirm"]').each(function() {
        var current = $(this);
        var currentClone = current.clone();
        switch (current.get(0).tagName) {
        case "INPUT":
            currentClone.removeAttr('id').removeAttr('name').removeAttr('data-block').removeAttr('onclick').removeClass('closeDialog').addClass('confirm').attr('data-id', current.attr('id')).insertBefore(current);
            break;
        default:
            currentClone.removeAttr('id').removeAttr('name').removeAttr('data-block').removeClass('closeDialog').addClass('confirm').attr('data-id', current.attr('id')).attr('href', 'javascript:void(0);').insertBefore(current);
            if (current.attr('onclick') == undefined) {
                var href = current.attr('href') + "";
                if (href.indexOf('javascript:') != -1) current.attr('onclick', current.attr('href')).removeAttr('href');
            }
            break;
        }
        current.hide();
    });
    $('[data-rel="print"]').each(function() {
        var current = $(this);
        var currentClone = current.clone();
        switch (current.get(0).tagName) {
        case "INPUT":
            currentClone.removeAttr('id').removeAttr('name').removeClass('closeDialog').addClass('print').attr('data-id', current.attr('id')).insertBefore(current);
            break;
        default:
            currentClone.removeAttr('id').removeAttr('name').removeClass('closeDialog').addClass('print').attr('data-id', current.attr('id')).attr('href', 'javascript:void(0);').insertBefore(current);
            if (current.attr('onclick') == undefined) {
                var href = current.attr('href') + "";
                if (href.indexOf('javascript:') != -1) current.attr('onclick', current.attr('href')).removeAttr('href');
            }
            break;
        }
        current.hide();
    });
    $(document).on("click", '.confirm', function () {
        var $this = $(this);
        var title = $this.attr("data-title") || '确认', Yes = '确定', No = '取消';
        var content = $this.attr("data-content") || "";
        var lock = api == null || false;
        $$.dialog({
            id:'confirm',
            title: title,
            width: '280px',
            height: '130px',
            content: content,
            okVal: Yes,
            lock: lock,
            parent: api,
            resize: false,
            max: false,
            min: false,
            ok: function () {
                $("#" + $this.attr('data-id')).trigger("click");
            },
            cancelVal: No,
            cancel: true,
            modal: true
        });
        return false;
    });
    $(document).on("click", '.print', function () {
        var $this = $(this);
        var title = $this.attr("data-title") || $('#Confirm_Title').val(), print = $this.attr("data-btn-print"), excel = $this.attr("data-btn-excel"), cancel = $this.attr("data-btn-cancel");
        var content = $this.attr("data-content") || "";
        $$.dialog({
            title: title,
            width: '280px',
            height: '130px',
            content: content,
            lock: true,
            parent: api,
            resize: false,
            max: false,
            min: false,
            button: [{ name: print, focus: true, callback: function() { window.print(); } },
                { name: excel, callback: function() { $("#" + $this.attr('data-id')).trigger("click"); } }, { name: cancel }]
        });
        return false;
    });
}

function $confirm($obj, callback,confirmMsg) {
    var title = $obj.attr("data-title") || "确认", Yes = "确定", No = "取消";
    var content = confirmMsg || $obj.attr("data-content") || "";
    var lock = api == null || false;
    $$.dialog({
        id: 'confirm',
        title: title,
        width: '280px',
        height: '130px',
        content: content,
        okVal: Yes,
        lock: lock,
        parent: api,
        resize: false,
        max: false,
        min: false,
        ok: function () {
            this.close();
            if (typeof callback == "function") callback.call();
        },
        cancelVal: No,
        cancel: true,
        modal: true
    });
    return false;
}

function $popup(id, data) {
    var $this = (typeof id == "string") ? $('#' + id) : $(id);
    var winId = "winFrame" + ($this.attr("data-id") || $$.dialog.setting.zIndex);
    var title = $this.attr("data-title") || false;
    var content = $this.attr("data-content") || "";
    var other = $this.attr("data-keyid") || '';
    content = content.replace("{dns}", '/');
    if (content.indexOf("url:") > -1) {
        if (content.indexOf("?") > -1) content += "&wid=" + winId;
        else content += "?wid=" + winId;
        content += "&keyid=" + other;
    }
    var width = $this.attr("data-width") || winWidth;
    var height = $this.attr("data-height") || winHeight;
    var resize = true, max = true, drag = true;
    //if (navigator.userAgent.indexOf("MSIE 10.0") > -1) resize = false, max = false, drag = false;
    if ($this.attr("data-max") != undefined) {
        max = $this.attr("data-max") === "1";
        resize = max;
    }
    var pid = (function () {
        if (api) return api.config.id;
        return undefined;
    })();
    data = data || {};
    data.pid = pid;
    var option = {
        id: winId,
        title: title,
        width: width,
        height: height,
        resize: resize,
        max: max,
        drag: true,
        content: content,
        lock: true,
        parent: api,
        min: false,
        cache: false,
        data: data,
        padding: 0,
        close: function() {
            try {
                if (pid) {
                    api.get(pid, 1).zindex();
                }
            } catch(e) {

            }
        },
        modal: true
    };
    if ($this.attr("data-maxs") == undefined || $this.attr("data-maxs") == 'false') $$.dialog(option);
    else $$.dialog(option).max();
    return false;
}

function winAlert(content, lock, close, refresh,closetime) {
    var title = '提示' || false, closeVal ='关闭' || false;
    lock = api == null || false;
    if (!close) { closetime = null;}
    var pid = (function () {
        if (api) return api.config.id;
        return undefined;
    })();
    var option = {
        id: 'winAlert',
        title: title,
        content: content,
        width: '280px',
        height: '130px',
        lock: lock,
        resize: false,
        max: false,
        min: false,
        okVal: closeVal,
        ok:true,
        close: function() {
            if (refresh) {
                var hostUrl = '/';
                switch (refresh) {
                case "top":
                    top.window.frames['framecontent'].location.replace(top.window.frames['framecontent'].location);//.reload(true);
                    break;
                case "this":
                    document.location.reload();
                    break;
                case "noparam":
                    var url = document.location + "", index = url.lastIndexOf("?");
                    if (index !== -1) url = url.substring(0, index);
                    document.location = url;
                    break;
                    case "parent":
                        api.opener.document.location.reload();
                    break;
                default:
                    break;
                }
            } else {
                if (pid) {
                    api.get(pid, 1).zindex();
                }
            }
            if (typeof close == "function") {
                close.call();
            }
        },
        time: closetime,
        parent: api,
        modal: true
    };
    $$.dialog(option);
}