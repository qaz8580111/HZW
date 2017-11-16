Ext.define('TianZun.store.NewsSmallClassStore', {
    extend: 'Ext.data.Store',
    pageSize: 0,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/Categors/GetSmallCategors'
    },
    fields: ['ID', 'Name']
});