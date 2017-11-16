Ext.define('TianZun.view.mh.NewsAddMid', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.NewsAddMid',
    title: '新增新闻',
    layout: 'fit',
    initComponent: function () {
        var win = Ext.create('TianZun.view.mh.NewsAdd');
        win.show();
        this.callParent();
    },

});