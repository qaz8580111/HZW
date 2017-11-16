Ext.define('TianZun.store.NewsBigClassStore', {
    extend: 'Ext.data.Store',
    pageSize: 0,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/Categors/GetBigCategors'
    },
    fields: ['ID', 'Name']
});