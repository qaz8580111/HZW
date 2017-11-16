Ext.define('TianZun.store.ShopStore', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.Shop',
    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "GET",
        url: configs.WebApi + 'api/storebases/Getstore',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'store_id'
        }
    },
    autoLoad: true

});
