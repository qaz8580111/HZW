Ext.define('TianZun.view.shop.ShopAdd', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.shopAdd',
    title: '新增店家',
    layout: 'fit',
    initComponent: function () {
        var win = Ext.create('TianZun.view.shop.AddShop');
        win.show();
        this.callParent();
    },

});