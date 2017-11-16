Ext.define('TianZun.view.outad.AddAd', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.addAd',
    title: '新增广告',
    layout: 'fit',
    initComponent: function () {
        var win = Ext.create('TianZun.view.outad.AddAdvert');
        win.show();
        this.callParent();
    },

});