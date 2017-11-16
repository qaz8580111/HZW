Ext.define('TianZun.store.AdvertTypeStore', {
    extend: 'Ext.data.Store',
    pageSize: 0,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/Advert/GetAdvertTypes'
    },
    fields: ['ID', 'Name']
});