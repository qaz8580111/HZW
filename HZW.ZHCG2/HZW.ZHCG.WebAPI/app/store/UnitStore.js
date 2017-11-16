Ext.define('TianZun.store.UnitStore', {
    extend: 'Ext.data.Store',
    pageSize: 0,
    proxy: {
        type: 'ajax',
        method: "Get",
        url: configs.WebApi + 'api/Unit/GetUnits'
    },
    fields: ['ID', 'Name']
});