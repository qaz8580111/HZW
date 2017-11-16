Ext.define('TianZun.store.StoreclassesStore', {
    extend: 'Ext.data.Store',

    pageSize: 0,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/StoreClasses/GetAllStoreClasses'
    },
    fields: ['type_id', 'type_name']

});