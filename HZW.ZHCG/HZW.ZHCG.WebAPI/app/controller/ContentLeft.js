Ext.define('TianZun.controller.ContentLeft', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.contentLeft',

    onShow: function (obj, eOpts) {
        configs.Menu = obj.down('menu');
        
        UnMask();
    },

    onMenuItemClick: function (obj, eOpts) {
        var content = Ext.create('TianZun.view.Shop.ShopList');
        var view = Ext.ComponentQuery.query('viewport')[0];
        var panel = view.items.getAt(3)
        view.remove(panel)
        content.region = 'center';
        view.add(content);
        obj.up().query('menuitem')[0].addCls("MenuChange");
        obj.up().query('menuitem')[1].addCls("MenuInit");
    },
    onAddShopMenu: function (obj, eOpts) {
        var content = Ext.create('TianZun.view.Shop.AddShop');
        var view = Ext.ComponentQuery.query('viewport')[0];
        var panel = view.items.getAt(3)
        view.remove(panel)
        content.region = 'center';
        view.add(content);
        obj.up().query('menuitem')[0].addCls("MenuInit");
        obj.up().query('menuitem')[1].addCls("MenuChange");
    },
});
