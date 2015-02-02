var tools = {
    //获取指定区间的随机数
    getRandomNumber: function (beginNum, endNum) {
        if (beginNum < 0) beginNum = 0;
        if (endNum == undefined) {
            endNum = beginNum;
            beginNum = 0;
        }
        var iChoices = endNum - beginNum + 1;
        return Math.floor(Math.random() * iChoices + beginNum)
    },
    postdata:function(url,data,callback,type){
        $.ajax({
            url: url.replace('{dns}','/'),
            data: data,
            method:'post',
            dataType:type||'json',
            success:callback
        });
    },
    initBackgroundHeight:function(){
        $(".s-skin-container").css("height", $(document).height());//设置遮罩层高度为整个网页高度
    },
    initPasswordModel: function (valicateUrl, paramer, callback) {
        callback=callback ||function (data) {
            if (data.success) {
                tools.handlePasswordError.clearError();
                window.location.href = data.url;
            }
            else {
                tools.handlePasswordError.addError("密码错误！");
            }
        };
         return new jBox('Confirm', {
            id:'pwd-box',
            autoClose: false,
            autoPadding: false,
            paddingValue:"20px 35px",
            minWidth:'220px',
            title: '请输入密码',
            confirmButton: '确定',
            cancelButton: '取消',
            content: '<input id="txtpassword" class="p-password" type="text"  /><br/><span id="errortips" class="error"></span>',
            confirm: function () {
                var password=$("#txtpassword").val();
                if ($.trim(password).length == 0) {
                    tools.handlePasswordError.addError("请输入密码！");
                    return;
                }
                paramer.password = password;
                tools.postdata(valicateUrl, paramer, callback);

            },
            onClose: function () {
                // tools.handlePasswordError.clearError()
            }
        });
    },
    handlePasswordError:{
        addError:function(msg){
            if (!$("#txtpassword").hasClass("errortxt")) { $("#txtpassword").addClass("errortxt"); }
            $("#errortips").html(msg);
        },
        clearError:function(){
            $("#errortips").html("");
            $("#txtpassword").removeClass("errortxt");
        }
    }

};
