function hasPdfPlugin() {
    //detect in mimeTypes array
    if (navigator.mimeTypes != null && navigator.mimeTypes.length > 0) {
        for (i = 0; i < navigator.mimeTypes.length; i++) {
            var mtype = navigator.mimeTypes[i];
            if (mtype.type == "application/pdf" && mtype.enabledPlugin)
                return true;
        }
    }

    //detect in plugins array
    if (navigator.plugins != null && navigator.plugins.length > 0) {
        for (i = 0; i < navigator.plugins.length; i++) {
            var plugin = navigator.plugins[i];
            if (plugin.name.indexOf("Adobe Acrobat") > -1
                    || plugin.name.indexOf("Adobe Reader") > -1) {
                return true;
            }
        }
    }

    // detect IE plugin
    if (isMSIE()) {
        // check for presence of newer object       
        try {
            var oAcro7 = new ActiveXObject('AcroPDF.PDF.1');
            if (oAcro7) {
                return true;
            }
        } catch (e) {
        }

        // iterate through version and attempt to create object 
        for (x = 1; x < 14; x++) {
            try {
                var oAcro = eval("new ActiveXObject('PDF.PdfCtrl." + x + "');");
                if (oAcro) {
                    return true;
                }
            } catch (e) {
            }
        }

        // check if you can create a generic acrobat document
        try {
            var p = new ActiveXObject('AcroExch.Document');
            if (p) {
                return true;
            }
        } catch (e) {
        }

    }

    // Can't detect in all other cases
    return false;
}

function isMSIE() {
    var ua = navigator.userAgent.toLowerCase();
    return ((/msie/.test(ua) || /trident/.test(ua)) && !/opera/.test(ua));
}