Ext.define('TianZun.store.NewsStore', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.News',

    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/News/GetAllNews',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'ID'
        }
    },
    autoLoad: false,
 
});