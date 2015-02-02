/// <reference path="jquery-1.11.0.js" />
var getArticleEvent = {
    category:function(){ 
        var paramer = window.location.href.split('/');
        if (paramer.length > 0) { return paramer[paramer.length-1]; }
        return '';
    }(),
    isAjax: 0,
    page:0,
    hasAddLoadBtn:0,
    showloading: function () {
        if (this.hasAddLoadBtn) {
           /* setTimeout(function () {
                $("#btn-loading").fadeIn(100);
            }, 0) */
            $("#btn-loading").show();
        }
        else {
            hasAddLoadBtn = 1;
            $(".blog-list").append('<a href="javascript:void(0)" class="js-next btn-next btn-loading" id="btn-loading"></a>');
        }
    },
    hideloading: function () {
        this.isAjax = 0;
        /* setTimeout(function () {
            $("#btn-loading").fadeOut(300);
        }, 0) */
        $("#btn-loading").hide();
    },
    getarticles: function () {
        if (this.isAjax) return;
        else {
            this.isAjax = 1;
            this.page += 1;
            this.showloading();
            this.hasAddLoadBtn = 1;
            var categoryHtml = [];
            $.ajax({
                url: '/api/category/list',
                method: 'post',
                dataType: 'json',
                data: { key: getArticleEvent.category, page: getArticleEvent.page, pagesize: 6 },
                success: function (articles) {
                    $.each(articles.items, function (i,item) {
                        categoryHtml.push('<li>');
                        categoryHtml.push('<a href="/blog/article/list/' + item.id + '.html">');
                        categoryHtml.push('<div class="blog-list-img"><img width="280" height="160" alt="' + item.name + '" src="' + item.image ||'' + '"></div>');
                        categoryHtml.push('<h5><span>' + item.name + '</span></h5>');
                        categoryHtml.push('<div class="intro">');
                        categoryHtml.push('<p>' + item.desc + '</p>');
                        categoryHtml.push('<span class="l"></span>');
                        categoryHtml.push('<span class="r">共有文章 ' + item.itemcount + ' 篇</span>');
                        categoryHtml.push('</div>');
                        categoryHtml.push('<div class="tips">');

                        categoryHtml.push(' <span class="l">' + item.complete == 1 ? "已完结" : item.lasttime + '</span>');
                        categoryHtml.push('<span class="r">' + item.readtimes + '人阅读过</span>');
                        categoryHtml.push('</div>');
                        categoryHtml.push('</a>');
                        categoryHtml.push('</li>');
                    });
                    getArticleEvent.hideloading();
                    $("#article-total").html('(' + articles.total + ')');
                    if (categoryHtml.length > 0) { $("#category-content").append(categoryHtml.join('')); }
                    //判断当前页面加载元素个数是否达到total值
                    if ($("#category-content li").length >= parseInt(articles.total)) { $("#category-content").attr("data-load-complete", "1"); }
                    tools.initBackgroundHeight();
                },
                error: function () {
                    alert('request data failed,please try again!');
                },
                complete: function () {
                    
                }
            });
        }
    }
};
