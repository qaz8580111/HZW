﻿document.onkeydown = function (event) { var e = event || window.event || arguments.callee.caller.arguments[0]; var d = e.srcElement || e.target; if (e && e.keyCode == 8) { return d.tagName.toUpperCase() == 'INPUT' || d.tagName.toUpperCase() == 'TEXTAREA' ? true : false } }