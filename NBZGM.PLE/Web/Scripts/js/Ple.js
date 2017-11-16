$(function () {

    var n = function () {
        if ($("#sidebar.sidebar-fixed").size() == 0) {
            $("#sidebar .nav").css("height", "auto");
            return
        }
        if ($("#sidebar.sidebar-fixed.sidebar-collapsed").size() > 0) {
            $("#sidebar .nav").css("height", "auto");
            return
        }
        var e = $(window).height() - 90;
        $("#sidebar.sidebar-fixed .nav").css("height", e + "px").niceScroll({
            railalign: "left",
            railoffset: { left: 3 },
            cursoropacitymax: .7
        });
        setTimeout(function () {
            $("#sidebar.sidebar-fixed .nav").getNiceScroll().doScrollPos(0,
                $("#sidebar .nav").scrollTop() + 40, 900)
        }, 9)
    };
    n();

    $("#sidebar.sidebar-collapsed #sidebar-collapse > i").attr("class",
    "icon-double-angle-right");

    $("#sidebar-collapse").click(function () {
        $("#sidebar").toggleClass("sidebar-collapsed");
        if ($("#sidebar").hasClass("sidebar-collapsed")) {
            $("#sidebar-collapse > i").attr("class", "icon-double-angle-right");
        } else {
            $("#sidebar-collapse > i").attr("class", "icon-double-angle-left");
            if ($.cookie) {
                $.cookie("sidebar-collapsed", "false")
            }
        }
        n()
    });
    $("#sidebar .search-form").click(function () {
        $('#sidebar .search-form input[type="text"]').focus()
    });
    $("#sidebar .nav > li.active > a > .arrow").removeClass("icon-angle-right").addClass("icon-angle-down");
    $("#theme-setting > a").click(function () {
        $(this).next().animate({ width: "toggle" }, 500, function () {
            if ($(this).is(":hidden")) {
                $("#theme-setting > a > i").attr("class", "icon-gears icon-2x")
            } else {
                $("#theme-setting > a > i").attr("class", "icon-remove icon-2x")
            }
        });
        $(this).next().css("display", "inline-block")
    });
    $("#theme-setting ul.colors a").click(function () {
        var e = $(this).parent().get(0);
        var t = $(e).parent().get(0);
        var n = $(t).data("target");
        var r = $(t).data("prefix");
        var i = $(this).attr("class");
        var s = new RegExp("\\b" + r + ".*\\b", "g");
        $(t).children("li").removeClass("active"); $(e).addClass("active");
        if ($(n).attr("class") != undefined) {
            $(n).attr("class", $(n).attr("class").replace(s, "").trim())
        } $(n).addClass(r + i);
        if (n == "body") {
            var o = $(t).parent().get(0);
            var u = $(o).nextAll("li:lt(2)");
            $(u).find("li.active").removeClass("active");
            $(u).find("a." + i).parent().addClass("active");
            $("#navbar").attr("class", $("#navbar").attr("class").replace(/\bnavbar-.*\b/g, "").trim());
            $("#main-container").attr("class", $("#main-container").attr("class").replace(/\bsidebar-.*\b/g, "").trim())
        } $.cookie(r + "color", i)
    });


    var r = ["blue", "red", "green", "orange", "yellow", "pink", "magenta", "gray", "black"]; $.each(r, function (e, t) { if ($("body").hasClass("skin-" + t)) { $("#theme-setting ul.colors > li").removeClass("active"); $("#theme-setting ul.colors > li:has(a." + t + ")").addClass("active") } }); $.each(r, function (e, t) { if ($("#navbar").hasClass("navbar-" + t)) { $('#theme-setting ul[data-prefix="navbar-"] > li').removeClass("active"); $('#theme-setting ul[data-prefix="navbar-"] > li:has(a.' + t + ")").addClass("active") } if ($("#main-container").hasClass("sidebar-" + t)) { $('#theme-setting ul[data-prefix="sidebar-"] > li').removeClass("active"); $('#theme-setting ul[data-prefix="sidebar-"] > li:has(a.' + t + ")").addClass("active") } }); if ($("#sidebar").hasClass("sidebar-fixed")) { $('#theme-setting > ul > li > a[data-target="sidebar"] > i').attr("class", "icon-check green") } if ($("#navbar").hasClass("navbar-fixed")) { $('#theme-setting > ul > li > a[data-target="navbar"] > i').attr("class", "icon-check green") } $("#theme-setting > ul > li > a").click(function () { var e = $(this).data("target"); var t = $(this).children("i"); if (t.hasClass("icon-check-empty")) { t.attr("class", "icon-check green"); $("#" + e).addClass(e + "-fixed"); $.cookie(e + "-fixed", "true") } else { t.attr("class", "icon-check-empty"); $("#" + e).removeClass(e + "-fixed"); $.cookie(e + "-fixed", "false") } if (e == "sidebar") { n() } }); $(".box .box-tool > a").click(function (e) { if ($(this).data("action") == undefined) { return } var t = $(this).data("action"); var n = $(this); switch (t) { case "collapse": $(n).children("i").addClass("anim-turn180"); $(this).parents(".box").children(".box-content").slideToggle(500, function () { if ($(this).is(":hidden")) { $(n).children("i").attr("class", "icon-chevron-down") } else { $(n).children("i").attr("class", "icon-chevron-up") } }); break; case "close": $(this).parents(".box").fadeOut(500, function () { $(this).parent().remove() }); break; case "config": $("#" + $(this).data("modal")).modal("show"); break } e.preventDefault() }); $(window).scroll(function () { if ($(this).scrollTop() > 100) { $("#btn-scrollup").fadeIn() } else { $("#btn-scrollup").fadeOut() } }); $("#btn-scrollup").click(function () { $("html, body").animate({ scrollTop: 0 }, 600); return false }); if ($(".tile-active").size() > 0) { var i = 1500; var s = 5e3; var o = function (e, t, n, r) { $(e).children(".tile").animate({ top: "-=" + r + "px" }, i); setTimeout(function () { u(e, t, n, r) }, n + i) }; var u = function (e, t, n, r) { $(e).children(".tile").animate({ top: "+=" + r + "px" }, i); setTimeout(function () { o(e, t, n, r) }, t + i) }; $(".tile-active").each(function (e, t) { var n, r, i, u, a; n = $(this).children(".tile").first(); r = $(this).children(".tile").last(); i = $(n).data("stop"); u = $(r).data("stop"); a = $(n).outerHeight(); if (i == undefined) { i = s } if (u == undefined) { u = s } setTimeout(function () { o(t, i, u, a) }, i) }) } $("#gritter-sticky").click(function () { var e = $.gritter.add({ title: "This is a sticky notice!", text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus eget tincidunt velit. Cum sociis natoque penatibus et <a href="#" style="color:#ccc">magnis dis parturient</a> montes, nascetur ridiculus mus.', image: "./img/demo/avatar/avatar1.jpg", sticky: true, time: "", class_name: "my-sticky-class" }); return false }); $("#gritter-regular").click(function () { $.gritter.add({ title: "This is a regular notice!", text: 'This will fade out after a certain amount of time. Vivamus eget tincidunt velit. Cum sociis natoque penatibus et <a href="#" style="color:#ccc">magnis dis parturient</a> montes, nascetur ridiculus mus.', image: "./img/demo/avatar/avatar1.jpg", sticky: false, time: "" }); return false }); $("#gritter-max").click(function () { $.gritter.add({ title: "This is a notice with a max of 3 on screen at one time!", text: 'This will fade out after a certain amount of time. Vivamus eget tincidunt velit. Cum sociis natoque penatibus et <a href="#" style="color:#ccc">magnis dis parturient</a> montes, nascetur ridiculus mus.', image: "./img/demo/avatar/avatar1.jpg", sticky: false, before_open: function () { if ($(".gritter-item-wrapper").length == 3) { return false } } }); return false }); $("#gritter-without-image").click(function () { $.gritter.add({ title: "This is a notice without an image!", text: 'This will fade out after a certain amount of time. Vivamus eget tincidunt velit. Cum sociis natoque penatibus et <a href="#" style="color:#ccc">magnis dis parturient</a> montes, nascetur ridiculus mus.' }); return false }); $("#gritter-light").click(function () { $.gritter.add({ title: "This is a light notification", text: 'Just add a "gritter-light" class_name to your $.gritter.add or globally to $.gritter.options.class_name', class_name: "gritter-light" }); return false }); $("#gritter-remove-all").click(function () { $.gritter.removeAll(); return false }); if (jQuery().slider) { $(".slider-basic").slider(); $("#slider-snap-inc").slider({ value: 100, min: 0, max: 1e3, step: 100, slide: function (e, t) { $("#slider-snap-inc-amount").text("$" + t.value) } }); $("#slider-snap-inc-amount").text("$" + $("#slider-snap-inc").slider("value")); $("#slider-range").slider({ range: true, min: 0, max: 500, values: [75, 300], slide: function (e, t) { $("#slider-range-amount").text("$" + t.values[0] + " - $" + t.values[1]) } }); $("#slider-range-amount").text("$" + $("#slider-range").slider("values", 0) + " - $" + $("#slider-range").slider("values", 1)); $("#slider-range-max").slider({ range: "max", min: 1, max: 10, value: 2, slide: function (e, t) { $("#slider-range-max-amount").text(t.value) } }); $("#slider-range-max-amount").text($("#slider-range-max").slider("value")); $("#slider-range-min").slider({ range: "min", value: 37, min: 1, max: 700, slide: function (e, t) { $("#slider-range-min-amount").text("$" + t.value) } }); $("#slider-range-min-amount").text("$" + $("#slider-range-min").slider("value")); $("#slider-eq > span").each(function () { var e = parseInt($(this).text(), 10); $(this).empty().slider({ value: e, range: "min", animate: true, orientation: "vertical" }) }); $("#slider-vertical").slider({ orientation: "vertical", range: "min", min: 0, max: 100, value: 60, slide: function (e, t) { $("#slider-vertical-amount").text(t.value) } }); $("#slider-vertical-amount").text($("#slider-vertical").slider("value")); $("#slider-range-vertical").slider({ orientation: "vertical", range: true, values: [17, 67], slide: function (e, t) { $("#slider-range-vertical-amount").text("$" + t.values[0] + " - $" + t.values[1]) } }); $("#slider-range-vertical-amount").text("$" + $("#slider-range-vertical").slider("values", 0) + " - $" + $("#slider-range-vertical").slider("values", 1)); $(".slider-color-preview").slider({ range: "min", value: 106, min: 1, max: 700 }) } if (jQuery().knob) { $(".knob").knob({ dynamicDraw: true, thickness: .2, tickColorizeValues: true, skin: "tron" }); $(".circle-stats-item > input").knob({ readOnly: true, width: 120, height: 120, dynamicDraw: true, thickness: .2, tickColorizeValues: true, skin: "tron" }) } $('.table > thead > tr > th:first-child > input[type="checkbox"]').change(function () { var e = false; if ($(this).is(":checked")) { e = true } $(this).parents("thead").next().find('tr > td:first-child > input[type="checkbox"]').prop("checked", e) });


})