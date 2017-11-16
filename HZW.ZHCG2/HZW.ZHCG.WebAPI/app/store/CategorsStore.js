Ext.define('TianZun.store.CategorsStore', {
    extend: 'Ext.data.Store',
    model: 'TianZun.model.Categors',

    pageSize: configs.PageSize,
    remoteFilter: true,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/Categors/GetAllSmallCategors',
        reader: {
            type: 'json',
            rootProperty: 'Items',
            totalProperty: 'Total',
            idProperty: 'ID'
        }
    },
});