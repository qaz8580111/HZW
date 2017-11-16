Ext.define('TianZun.ux.Window', {
    extend: 'Ext.Window',
    xtype: 'uxwindow',
    layout: 'fit',
    listeners: {
        show: function (win) {
            var w = win.getWidth();
            var h = win.getHeight();
          
            var x = window.innerWidth
                    || document.documentElement.clientWidth
                    || document.body.clientWidth;

            var y = window.innerHeight
                    || document.documentElement.clientHeight
                    || document.body.clientHeight;

            win.setX((x-w)/2);
            win.setY((y-h)/2);
            Mask();
        },
        close: function (win) {
            UnMask();
        },
        hide: function (win) {
            UnMask();
        }
    }
});