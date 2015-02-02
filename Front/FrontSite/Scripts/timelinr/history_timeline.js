﻿// jquery timelinr
jQuery.fn.timelinr = function (a) { settings = jQuery.extend({ orientation: "horizontal", containerDiv: "#timeline", datesDiv: "#dates", datesSelectedClass: "selected", datesSpeed: 500, issuesDiv: "#issues", issuesSelectedClass: "selected", issuesSpeed: 200, issuesTransparency: 0.2, issuesTransparencySpeed: 500, prevButton: "#prev", nextButton: "#next", arrowKeys: "false", startAt: 1, autoPlay: "false", autoPlayDirection: "forward", autoPlayPause: 2000 }, a); $(function () { var j = $(settings.datesDiv + " li").length; var f = $(settings.issuesDiv + " div.year_content").length; var d = $(settings.datesDiv).find("a." + settings.datesSelectedClass); var n = $(settings.issuesDiv).find("div." + settings.issuesSelectedClass); var g = $(settings.containerDiv).width(); var i = $(settings.containerDiv).height(); var h = $(settings.issuesDiv).width(); var e = $(settings.issuesDiv).height(); var m = $(settings.issuesDiv + " div.year_content").width(); var k = $(settings.issuesDiv + " div.year_content").height(); var c = $(settings.datesDiv).width(); var b = $(settings.datesDiv).height(); var o = $(settings.datesDiv + " li").width(); var l = $(settings.datesDiv + " li").height(); if (settings.orientation == "horizontal") { $(settings.issuesDiv).width(m * f); $(settings.datesDiv).width(o * j).css("marginLeft", g / 2 - o / 2); var p = parseInt($(settings.datesDiv).css("marginLeft").substring(0, $(settings.datesDiv).css("marginLeft").indexOf("px"))); } else { if (settings.orientation == "vertical") { $(settings.issuesDiv).height(k * f); $(settings.datesDiv).height(l * j).css("marginTop", i / 2 - l / 2); var p = parseInt($(settings.datesDiv).css("marginTop").substring(0, $(settings.datesDiv).css("marginTop").indexOf("px"))); } } $(settings.datesDiv + " a").click(function (r) { r.preventDefault(); var s = $(this).text(); var q = $(this).parent().prevAll().length; if (settings.orientation == "horizontal") { $(settings.issuesDiv).animate({ marginLeft: -m * q }, { queue: false, duration: settings.issuesSpeed }); } else { if (settings.orientation == "vertical") { $(settings.issuesDiv).animate({ marginTop: -k * q }, { queue: false, duration: settings.issuesSpeed }); } } $(settings.issuesDiv + " div.year_content").animate({ opacity: settings.issuesTransparency }, { queue: false, duration: settings.issuesSpeed }).removeClass(settings.issuesSelectedClass).eq(q).addClass(settings.issuesSelectedClass).fadeTo(settings.issuesTransparencySpeed, 1); $(settings.datesDiv + " a").removeClass(settings.datesSelectedClass); $(this).addClass(settings.datesSelectedClass); if (settings.orientation == "horizontal") { $(settings.datesDiv).animate({ marginLeft: p - (o * q) }, { queue: false, duration: settings.datesSpeed }); } else { if (settings.orientation == "vertical") { $(settings.datesDiv).animate({ marginTop: p - (l * q) }, { queue: false, duration: settings.datesSpeed }); } } }); $(settings.nextButton).bind("click", function (q) { q.preventDefault(); if (settings.orientation == "horizontal") { var u = parseInt($(settings.issuesDiv).css("marginLeft").substring(0, $(settings.issuesDiv).css("marginLeft").indexOf("px"))); var t = u / m; var r = parseInt($(settings.datesDiv).css("marginLeft").substring(0, $(settings.datesDiv).css("marginLeft").indexOf("px"))); var s = r - o; if (u <= -(m * f - (m))) { $(settings.issuesDiv).stop(); $(settings.datesDiv + " li:last-child a").click(); } else { if (!$(settings.issuesDiv).is(":animated")) { $(settings.issuesDiv).animate({ marginLeft: u - m }, { queue: false, duration: settings.issuesSpeed }); $(settings.issuesDiv + " div.year_content").animate({ opacity: settings.issuesTransparency }, { queue: false, duration: settings.issuesSpeed }); $(settings.issuesDiv + " div." + settings.issuesSelectedClass).removeClass(settings.issuesSelectedClass).next().fadeTo(settings.issuesTransparencySpeed, 1).addClass(settings.issuesSelectedClass); $(settings.datesDiv).animate({ marginLeft: s }, { queue: false, duration: settings.datesSpeed }); $(settings.datesDiv + " a." + settings.datesSelectedClass).removeClass(settings.datesSelectedClass).parent().next().children().addClass(settings.datesSelectedClass); } } } else { if (settings.orientation == "vertical") { var u = parseInt($(settings.issuesDiv).css("marginTop").substring(0, $(settings.issuesDiv).css("marginTop").indexOf("px"))); var t = u / k; var r = parseInt($(settings.datesDiv).css("marginTop").substring(0, $(settings.datesDiv).css("marginTop").indexOf("px"))); var s = r - l; if (u <= -(k * f - (k))) { $(settings.issuesDiv).stop(); $(settings.datesDiv + " li:last-child a").click(); } else { if (!$(settings.issuesDiv).is(":animated")) { $(settings.issuesDiv).animate({ marginTop: u - k }, { queue: false, duration: settings.issuesSpeed }); $(settings.issuesDiv + " div.year_content").animate({ opacity: settings.issuesTransparency }, { queue: false, duration: settings.issuesSpeed }); $(settings.issuesDiv + " div." + settings.issuesSelectedClass).removeClass(settings.issuesSelectedClass).next().fadeTo(settings.issuesTransparencySpeed, 1).addClass(settings.issuesSelectedClass); $(settings.datesDiv).animate({ marginTop: s }, { queue: false, duration: settings.datesSpeed }); $(settings.datesDiv + " a." + settings.datesSelectedClass).removeClass(settings.datesSelectedClass).parent().next().children().addClass(settings.datesSelectedClass); } } } } }); $(settings.prevButton).click(function (q) { q.preventDefault(); if (settings.orientation == "horizontal") { var u = parseInt($(settings.issuesDiv).css("marginLeft").substring(0, $(settings.issuesDiv).css("marginLeft").indexOf("px"))); var t = u / m; var r = parseInt($(settings.datesDiv).css("marginLeft").substring(0, $(settings.datesDiv).css("marginLeft").indexOf("px"))); var s = r + o; if (u >= 0) { $(settings.issuesDiv).stop(); $(settings.datesDiv + " li:first-child a").click(); } else { if (!$(settings.issuesDiv).is(":animated")) { $(settings.issuesDiv).animate({ marginLeft: u + m }, { queue: false, duration: settings.issuesSpeed }); $(settings.issuesDiv + " div.year_content").animate({ opacity: settings.issuesTransparency }, { queue: false, duration: settings.issuesSpeed }); $(settings.issuesDiv + " div." + settings.issuesSelectedClass).removeClass(settings.issuesSelectedClass).prev().fadeTo(settings.issuesTransparencySpeed, 1).addClass(settings.issuesSelectedClass); $(settings.datesDiv).animate({ marginLeft: s }, { queue: false, duration: settings.datesSpeed }); $(settings.datesDiv + " a." + settings.datesSelectedClass).removeClass(settings.datesSelectedClass).parent().prev().children().addClass(settings.datesSelectedClass); } } } else { if (settings.orientation == "vertical") { var u = parseInt($(settings.issuesDiv).css("marginTop").substring(0, $(settings.issuesDiv).css("marginTop").indexOf("px"))); var t = u / k; var r = parseInt($(settings.datesDiv).css("marginTop").substring(0, $(settings.datesDiv).css("marginTop").indexOf("px"))); var s = r + l; if (u >= 0) { $(settings.issuesDiv).stop(); $(settings.datesDiv + " li:first-child a").click(); } else { if (!$(settings.issuesDiv).is(":animated")) { $(settings.issuesDiv).animate({ marginTop: u + k }, { queue: false, duration: settings.issuesSpeed }); $(settings.issuesDiv + " div.year_content").animate({ opacity: settings.issuesTransparency }, { queue: false, duration: settings.issuesSpeed }); $(settings.issuesDiv + " div." + settings.issuesSelectedClass).removeClass(settings.issuesSelectedClass).prev().fadeTo(settings.issuesTransparencySpeed, 1).addClass(settings.issuesSelectedClass); $(settings.datesDiv).animate({ marginTop: s }, { queue: false, duration: settings.datesSpeed }, { queue: false, duration: settings.issuesSpeed }); $(settings.datesDiv + " a." + settings.datesSelectedClass).removeClass(settings.datesSelectedClass).parent().prev().children().addClass(settings.datesSelectedClass); } } } } }); if (settings.arrowKeys == "true") { if (settings.orientation == "horizontal") { $(document).keydown(function (q) { if (q.keyCode == 39) { $(settings.nextButton).click(); } if (q.keyCode == 37) { $(settings.prevButton).click(); } }); } else { if (settings.orientation == "vertical") { $(document).keydown(function (q) { if (q.keyCode == 40) { $(settings.nextButton).click(); } if (q.keyCode == 38) { $(settings.prevButton).click(); } }); } } } $(settings.datesDiv + " li").eq(settings.startAt - 1).find("a").trigger("click"); if (settings.autoPlay == "true") { setInterval("autoPlay()", settings.autoPlayPause); } }); }; function autoPlay() { var a = $(settings.datesDiv).find("a." + settings.datesSelectedClass); if (settings.autoPlayDirection == "forward") { if (a.parent().is("li:last-child")) { $(settings.datesDiv + " li:first-child").find("a").trigger("click"); } else { a.parent().next().find("a").trigger("click"); } } else { if (settings.autoPlayDirection == "backward") { if (a.parent().is("li:first-child")) { $(settings.datesDiv + " li:last-child").find("a").trigger("click"); } else { a.parent().prev().find("a").trigger("click"); } } } }


// Lightmaker js

function Timeline(opts) {
    this.wrap = opts.wrap;
    this.years = this.wrap.find('div.year_content');
    this.items = [];
    (function (t) { t.init(); })(this);
}

Timeline.prototype = {
    init: function() {
        var t = this;
        // setup vertical sliders
        this.years.each(function(i) {
            var $t = $(this);
            t.items[i] = new VerticalSlider({ wrap: $t.find('.pad'), items: '.row' });
        });
        // init timelinr
        this.settings = {
            orientation: 'horizontal',
            containerDiv: t.wrap,
            datesDiv: '#dates',
            datesSelectedClass: 'selected',
            datesSpeed: 500,
            prevButton: '#prev',
            nextButton: '#next',
            arrowKeys: 'true',
            startAt: this.years.length,
            autoPlay: 'false',
            autoPlayDirection: 'forward',
            autoPlayPause: 2000
        };
        this.timelinr = $().timelinr(t.settings);
    }
};
function VerticalSlider(opts) {
    this.wrap = opts.wrap;
    this.index = 0;
    this.items = this.wrap.find(opts.items);
    this.total = this.items.length;
    this.scroll = this.wrap.find('.scroll_content');
    this.speed = opts.speed || 200;
    this.controlsMu = '<div class="controls"><ul><li><a href="#" class="vPrev"><span>Previous</span></a></li> <li><a href="#" class="vNext"><span>Next</span></a></li></ul></div>';
    (function (t) { t.init(); })(this);
}
VerticalSlider.prototype = {
    init: function () {
        var t = this;
        if (this.items.length > 1) {
            this.wrap.prepend(t.controlsMu);
            this.btn_next = this.wrap.find('a.vNext');
            this.btn_prev = this.wrap.find('a.vPrev');

            this.btn_next.click(function () { t.next(); return false; });
            this.btn_prev.click(function () { t.prev(); return false; });

            this.setBtnStatus();
        }
    },
    goTo: function (n) {
        if (this.scroll.is(':animated')) { return false; }
        var y = $(this.items[n]).position().top, t = this;
        this.scroll.animate({ top: -y }, t.speed, function () { t.index = parseInt(n); t.setBtnStatus(); });
    },
    next: function () {
        this.index++;
        this.goTo(this.getIndex());
    },
    prev: function () {
        this.index--;
        this.goTo(this.getIndex());
    },
    getIndex: function () {
        var len = this.total - 1, i = this.index;
        if (i > len) { i = len; }
        else if (i < 0) { i = 0; }
        return i;
    },
    setBtnStatus: function () {
        var t = this;
        this.btn_next.add(t.btn_prev).removeClass('disabled');
        if (this.index === this.total - 1) { this.btn_next.addClass('disabled'); }
        else if (this.index === 0) { this.btn_prev.addClass('disabled'); }
    }
}